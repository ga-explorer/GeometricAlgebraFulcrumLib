using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Samples.Algebra.GeometricAlgebra;

public static class CharacteristicPolynomialFromMinimalPoly
{
    public static Vector<double> GetCharacteristicPolynomial(this XGaFloat64Multivector multivector)
    {
        var vSpaceDimensions = multivector.VSpaceDimensions;
        
        if (vSpaceDimensions > 31)
            throw new ArgumentException();

        var gaSpaceDimensions = 1 << vSpaceDimensions;

        // Step 1: Compute minimal polynomial of A
        var (minPoly, deg) = GetMinimalPolynomial(multivector);

        // Step 2: Exponent
        var exponent = gaSpaceDimensions / deg;

        // Step 3: Compute (minPoly)^exponent
        return PowerPolynomial(minPoly, exponent);
    }

    public static (Vector<double> coeffs, int degree) GetMinimalPolynomial(this XGaFloat64Multivector multivector)
    {
        var vSpaceDimensions = multivector.VSpaceDimensions;
        
        if (vSpaceDimensions > 31)
            throw new ArgumentException();

        var gaSpaceDimensions = 1 << vSpaceDimensions;
        var processor = multivector.Processor;

        // Krylov: {1, A, A^2, ..., A^k}
        var powers = new List<XGaFloat64Multivector>();
        var one = processor.ScalarOne;
        powers.Add(one);

        // Use Gram-Schmidt-like independence
        var basis = new List<XGaFloat64Multivector>();
        var matrix = new List<Vector<double>>
        {
            one.MultivectorToToMathNetVector(gaSpaceDimensions)
        }; // coefficients for linear algebra

        basis.Add(one);

        XGaFloat64Multivector prevCoeffs = one;

        for (var k = 1; k <= gaSpaceDimensions; k++)
        {
            var current = multivector.Gp(prevCoeffs);
            var coeffs = current.MultivectorToToMathNetVector(gaSpaceDimensions);
            powers.Add(current);

            // Orthogonalize against basis
            //var proj = new double[GaSpaceDimensions];
            var residual = coeffs.Clone();

            for (var i = 0; i < basis.Count; i++)
            {
                var dot = 0.0;
                for (var j = 0; j < gaSpaceDimensions; j++)
                    dot += matrix[i][j] * coeffs[j];

                var norm2 = matrix[i].Select(x => x * x).Sum();
                if (norm2 > 1e-12)
                {
                    var c = dot / norm2;
                    for (var j = 0; j < gaSpaceDimensions; j++)
                        residual[j] -= c * matrix[i][j];
                }
            }

            var residualNorm = residual.Select(x => x * x).Sum();
            if (residualNorm < 1e-10)
            {
                // Linearly dependent: solve for coefficients
                // coeffs ≈ sum c_i * powers[i]
                // So: A^k - sum_{i=0}^{k-1} c_i A^i = 0
                var linearComb = SolveLinearCombination(powers, coeffs);
                return (linearComb, k);
            }
            else
            {
                basis.Add(current);
                matrix.Add(residual.Clone());
                prevCoeffs = current; //.MultivectorToArray1D(GaSpaceDimensions);
            }
        }

        // Fallback: minimal poly = characteristic poly
        return (new[]{1.0d}.ToMathNetDenseVector(), 1); // should not happen
    }


    private static Vector<double> SolveLinearCombination(List<XGaFloat64Multivector> powers, Vector<double> target)
    {
        var n = target.Count;
        var k = powers.Count;
        var matrixA = new double[n, k];
        var matrixB = new double[n];
        for (var j = 0; j < n; j++)
            matrixB[j] = target[j];

        for (var col = 0; col < k; col++)
        {
            var mvArray = powers[col].MultivectorToToMathNetVector(n);
            for (var row = 0; row < n; row++)
                matrixA[row, col] = mvArray[row];
        }

        // Solve A x = b (last column is A^k)
        // Use SVD or Gaussian elimination
        // Here: simple Gaussian elimination (for small k)
        //var x = SolveLinearSystem(matrixA, b);
        var x = 
            Matrix
                .Build
                .DenseOfArray(matrixA)
                .Solve(Vector.Build.DenseOfArray(matrixB));

        var poly = new double[k + 1];
        for (var i = 0; i < k; i++)
            poly[i] = -x[i];
        poly[k] = 1.0; // monic
        return poly.ToMathNetDenseVector();
    }

    //private static double[] SolveLinearSystem(double[,] matrixA, double[] matrixB)
    //{
    //    var rowCount = matrixA.GetLength(0);
    //    var colCount = matrixA.GetLength(1);

    //    var matrix = new double[rowCount, colCount + 1];
    //    for (var rowIdx = 0; rowIdx < rowCount; rowIdx++)
    //    {
    //        for (var colIdx = 0; colIdx < colCount; colIdx++)
    //            matrix[rowIdx, colIdx] = matrixA[rowIdx, colIdx];

    //        matrix[rowIdx, colCount] = matrixB[rowIdx];
    //    }

    //    Console.WriteLine(matrix.GetArrayText());
    //    Console.WriteLine();

    //    // Gaussian elimination
    //    for (var pivotIdx = 0; pivotIdx < colCount; pivotIdx++)
    //    {
    //        // Find max row
    //        var maxRowIdx = pivotIdx;
    //        for (var rowIdx = pivotIdx + 1; rowIdx < rowCount; rowIdx++)
    //            if (Math.Abs(matrix[rowIdx, pivotIdx]) > Math.Abs(matrix[maxRowIdx, pivotIdx]))
    //                maxRowIdx = rowIdx;

    //        // Swap
    //        if (maxRowIdx != pivotIdx)
    //        {
    //            for (var colIdx = 0; colIdx <= colCount; colIdx++)
    //                (matrix[pivotIdx, colIdx], matrix[maxRowIdx, colIdx]) = (matrix[maxRowIdx, colIdx], matrix[pivotIdx, colIdx]);
    //        }

    //        // Eliminate
    //        for (var rowIdx = pivotIdx + 1; rowIdx < rowCount; rowIdx++)
    //        {
    //            var factor = matrix[rowIdx, pivotIdx] / matrix[pivotIdx, pivotIdx];

    //            for (var colIdx = pivotIdx; colIdx <= colCount; colIdx++)
    //                matrix[rowIdx, colIdx] -= factor * matrix[pivotIdx, colIdx];
    //        }

    //        Console.WriteLine(matrix.GetArrayText());
    //        Console.WriteLine();


    //    }

    //    // Back-substitute
    //    var x = new double[colCount];
    //    for (var rowIdx = colCount - 1; rowIdx >= 0; rowIdx--)
    //    {
    //        x[rowIdx] = matrix[rowIdx, colCount];
    //        for (var colIdx = rowIdx + 1; colIdx < colCount; colIdx++)
    //            x[rowIdx] -= matrix[rowIdx, colIdx] * x[colIdx];
    //        x[rowIdx] /= matrix[rowIdx, rowIdx];
    //    }
    //    return x;
    //}

    private static Vector<double> PowerPolynomial(Vector<double> poly, int exponent)
    {
        Vector<double> result = new[]{1.0}.ToMathNetDenseVector();
        for (var i = 0; i < exponent; i++)
        {
            result = MultiplyPolynomials(result, poly);
        }
        return result.ToMathNetDenseVector();
    }

    private static Vector<double> MultiplyPolynomials(Vector<double> a, Vector<double> b)
    {
        var c = new double[a.Count + b.Count - 1];
        for (var i = 0; i < a.Count; i++)
        for (var j = 0; j < b.Count; j++)
            c[i + j] += a[i] * b[j];
        return c.ToMathNetDenseVector();
    }


    /// <summary>
    /// Validates that m(A) = 0 in the Clifford algebra.
    /// </summary>
    /// <param name="multivector">The multivector</param>
    /// <param name="minPoly">Coefficients of m(t): [c0, c1, ..., cd] where m(t) = c0 + c1*t + ... + cd*t^d</param>
    /// <param name="tolerance">Numerical tolerance (e.g., 1e-10)</param>
    /// <returns>True if m(A) ≈ 0</returns>
    public static bool ValidateMinimalPolynomial(this XGaFloat64Multivector multivector, Vector<double> minPoly, double tolerance = Float64Utils.DefaultTolerance)
    {
        var vSpaceDimensions = multivector.VSpaceDimensions;
        
        if (vSpaceDimensions > 31)
            throw new ArgumentException();

        var processor = multivector.Processor;

        var degree = minPoly.Count - 1;
        if (degree < 1) return Math.Abs(minPoly[0]) < tolerance;

        XGaFloat64Multivector result = processor.ScalarZero; // m(A) = sum c_k A^k

        // Compute powers of A: A^0 = 1, A^1 = A, A^2, ..., A^d
        var powers = new List<XGaFloat64Multivector>
        {
            processor.ScalarOne,
            multivector // A^1
        };

        for (var k = 2; k <= degree; k++)
        {
            var prev = powers[k - 1];
            var next = multivector.Gp(prev);
            powers.Add(next);
        }

        // Accumulate: result = sum_{k=0}^d minPoly[k] * A^k
        for (var k = 0; k <= degree; k++)
        {
            result += minPoly[k] * powers[k]; // scalar multiplication
        }

        // Check if ||result|| < tolerance
        var norm = result.ENormSquared().Sqrt();

        return norm < tolerance;
    }
}