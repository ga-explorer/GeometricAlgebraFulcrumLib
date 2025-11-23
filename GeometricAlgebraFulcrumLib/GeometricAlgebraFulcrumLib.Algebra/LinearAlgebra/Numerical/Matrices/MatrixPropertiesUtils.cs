using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Matrices;

/// <summary>
/// Provides methods for detecting various matrix properties that are important
/// for selecting optimal matrix exponential algorithms.
/// </summary>
public static class MatrixPropertiesUtils
{
    /// <summary>
    /// Analyzes a real matrix and returns its detected properties.
    /// </summary>
    /// <param name="matrix">The matrix to analyze</param>
    /// <param name="tolerance">Numerical tolerance for comparisons</param>
    /// <returns>Detected matrix properties</returns>
    public static MatrixProperties AnalyzeProperties(this Matrix<double> matrix, double tolerance = Float64Utils.DefaultTolerance)
    {
        if (!matrix.IsSquare())
            throw new ArgumentException("Matrix must be square for property analysis");

        var props = MatrixProperties.None;
        //var n = matrix.Size;

        // Check basic structural properties
        props |= matrix.CheckSymmetryProperty(tolerance);
        props |= matrix.CheckTriangularProperty(tolerance);
        props |= matrix.CheckDiagonalProperty(tolerance);
        
        // Check sign-based properties
        props |= matrix.CheckSignProperties(tolerance);
        
        // Check special matrix types
        props |= matrix.CheckStochasticProperty(tolerance);
        props |= matrix.CheckOrthogonalProperty(tolerance);
        
        // Check nilpotent and other special properties
        props |= matrix.CheckSpecialProperties(tolerance);

        return props;
    }

    /// <summary>
    /// Analyzes a complex matrix and returns its detected properties.
    /// </summary>
    /// <param name="matrix">The matrix to analyze</param>
    /// <param name="tolerance">Numerical tolerance for comparisons</param>
    /// <returns>Detected matrix properties</returns>
    public static MatrixProperties AnalyzeProperties(this Matrix<Complex> matrix, double tolerance = Float64Utils.DefaultTolerance)
    {
        if (!matrix.IsSquare())
            throw new ArgumentException("Matrix must be square for property analysis");

        var props = MatrixProperties.None;
        //var n = matrix.Size;

        // Check basic structural properties
        props |= matrix.CheckHermitianProperty(tolerance);
        props |= matrix.CheckTriangularProperty(tolerance);
        props |= matrix.CheckDiagonalProperty(tolerance);
        
        // Check unitary and normal properties
        props |= matrix.CheckUnitaryProperty(tolerance);
        props |= matrix.CheckNormalProperty(tolerance);

        return props;
    }

    /// <summary>
    /// Checks if a real matrix is symmetric or skew-symmetric.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MatrixProperties CheckSymmetryProperty(this Matrix<double> matrix, double tolerance = Float64Utils.DefaultTolerance)
    {
        var n = matrix.Size();
        var isSymmetric = true;
        var isSkewSymmetric = true;

        for (var i = 0; i < n && (isSymmetric || isSkewSymmetric); i++)
        {
            for (var j = 0; j < n && (isSymmetric || isSkewSymmetric); j++)
            {
                var aij = matrix[i, j];
                var aji = matrix[j, i];
                
                if (Math.Abs(aij - aji) > tolerance)
                    isSymmetric = false;
                
                if (Math.Abs(aij + aji) > tolerance)
                    isSkewSymmetric = false;
            }
        }

        // Check diagonal for skew-symmetric (must be zero)
        if (isSkewSymmetric)
        {
            for (var i = 0; i < n; i++)
            {
                if (Math.Abs(matrix[i, i]) > tolerance)
                {
                    isSkewSymmetric = false;
                    break;
                }
            }
        }

        var result = MatrixProperties.None;
        if (isSymmetric) result |= MatrixProperties.Symmetric;
        if (isSkewSymmetric) result |= MatrixProperties.SkewSymmetric;
        return result;
    }

    /// <summary>
    /// Checks if a complex matrix is Hermitian or skew-Hermitian.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MatrixProperties CheckHermitianProperty(this Matrix<Complex> matrix, double tolerance = Float64Utils.DefaultTolerance)
    {
        var n = matrix.Size();
        var isHermitian = true;
        var isSkewHermitian = true;

        for (var i = 0; i < n && (isHermitian || isSkewHermitian); i++)
        {
            for (var j = 0; j < n && (isHermitian || isSkewHermitian); j++)
            {
                var aij = matrix[i, j];
                var aji = Complex.Conjugate(matrix[j, i]);
                
                if ((aij - aji).Magnitude > tolerance)
                    isHermitian = false;
                
                if ((aij + aji).Magnitude > tolerance)
                    isSkewHermitian = false;
            }
        }

        // Check diagonal for Hermitian (must be real) and skew-Hermitian (must be imaginary)
        for (var i = 0; i < n; i++)
        {
            var aii = matrix[i, i];
            
            if (isHermitian && Math.Abs(aii.Imaginary) > tolerance)
                isHermitian = false;
            
            if (isSkewHermitian && Math.Abs(aii.Real) > tolerance)
                isSkewHermitian = false;
        }

        var result = MatrixProperties.None;
        if (isHermitian) result |= MatrixProperties.Hermitian;
        if (isSkewHermitian) result |= MatrixProperties.SkewHermitian;
        return result;
    }

    /// <summary>
    /// Checks triangular properties for real matrices.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MatrixProperties CheckTriangularProperty(this Matrix<double> matrix, double tolerance = Float64Utils.DefaultTolerance)
    {
        var n = matrix.Size();
        var isUpperTriangular = true;
        var isLowerTriangular = true;

        for (var i = 0; i < n && (isUpperTriangular || isLowerTriangular); i++)
        {
            for (var j = 0; j < n && (isUpperTriangular || isLowerTriangular); j++)
            {
                var aij = matrix[i, j];
                
                if (i > j && Math.Abs(aij) > tolerance)
                    isUpperTriangular = false;
                
                if (i < j && Math.Abs(aij) > tolerance)
                    isLowerTriangular = false;
            }
        }

        var result = MatrixProperties.None;
        if (isUpperTriangular) result |= MatrixProperties.UpperTriangular;
        if (isLowerTriangular) result |= MatrixProperties.LowerTriangular;
        return result;
    }

    /// <summary>
    /// Checks triangular properties for complex matrices.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MatrixProperties CheckTriangularProperty(this Matrix<Complex> matrix, double tolerance = Float64Utils.DefaultTolerance)
    {
        var n = matrix.Size();
        var isUpperTriangular = true;
        var isLowerTriangular = true;

        for (var i = 0; i < n && (isUpperTriangular || isLowerTriangular); i++)
        {
            for (var j = 0; j < n && (isUpperTriangular || isLowerTriangular); j++)
            {
                var aij = matrix[i, j];
                
                if (i > j && aij.Magnitude > tolerance)
                    isUpperTriangular = false;
                
                if (i < j && aij.Magnitude > tolerance)
                    isLowerTriangular = false;
            }
        }

        var result = MatrixProperties.None;
        if (isUpperTriangular) result |= MatrixProperties.UpperTriangular;
        if (isLowerTriangular) result |= MatrixProperties.LowerTriangular;
        return result;
    }

    /// <summary>
    /// Checks if a real matrix is diagonal.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MatrixProperties CheckDiagonalProperty(this Matrix<double> matrix, double tolerance = Float64Utils.DefaultTolerance)
    {
        var n = matrix.Size();
        
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                if (i != j && Math.Abs(matrix[i, j]) > tolerance)
                    return MatrixProperties.None;
            }
        }
        
        return MatrixProperties.Diagonal;
    }

    /// <summary>
    /// Checks if a complex matrix is diagonal.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MatrixProperties CheckDiagonalProperty(this Matrix<Complex> matrix, double tolerance = Float64Utils.DefaultTolerance)
    {
        var n = matrix.Size();
        
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                if (i != j && matrix[i, j].Magnitude > tolerance)
                    return MatrixProperties.None;
            }
        }
        
        return MatrixProperties.Diagonal;
    }

    /// <summary>
    /// Checks sign-based properties for real matrices.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MatrixProperties CheckSignProperties(this Matrix<double> matrix, double tolerance = Float64Utils.DefaultTolerance)
    {
        var n = matrix.Size();
        var result = MatrixProperties.None;
        
        var isPositive = true;
        var isNegative = true;
        var isNonNegative = true;
        var isEssentiallyNonnegative = true;

        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                var aij = matrix[i, j];
                
                if (aij <= tolerance) isPositive = false;
                if (aij >= -tolerance) isNegative = false;
                if (aij < -tolerance) isNonNegative = false;
                
                // Essentially nonnegative: off-diagonal elements are non-negative
                if (i != j && aij < -tolerance) isEssentiallyNonnegative = false;
            }
        }

        if (isPositive) result |= MatrixProperties.Positive;
        if (isNegative) result |= MatrixProperties.Negative;
        if (isNonNegative) result |= MatrixProperties.NonNegative;
        if (isEssentiallyNonnegative) result |= MatrixProperties.EssentiallyNonnegative;

        // Check if it's an intensity matrix (essentially nonnegative with zero row sums)
        if (isEssentiallyNonnegative)
        {
            var isIntensityMatrix = true;
            for (var i = 0; i < n; i++)
            {
                double rowSum = 0;
                for (var j = 0; j < n; j++)
                {
                    rowSum += matrix[i, j];
                }
                if (Math.Abs(rowSum) > tolerance)
                {
                    isIntensityMatrix = false;
                    break;
                }
            }
            if (isIntensityMatrix) result |= MatrixProperties.IntensityMatrix;
        }

        return result;
    }

    /// <summary>
    /// Checks stochastic properties for real matrices.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MatrixProperties CheckStochasticProperty(this Matrix<double> matrix, double tolerance = Float64Utils.DefaultTolerance)
    {
        var n = matrix.Size();
        var isRowStochastic = true;
        var isColStochastic = true;
        
        // Check if all elements are non-negative
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                if (matrix[i, j] < -tolerance)
                {
                    return MatrixProperties.None;
                }
            }
        }

        // Check row sums
        for (var i = 0; i < n; i++)
        {
            double rowSum = 0;
            for (var j = 0; j < n; j++)
            {
                rowSum += matrix[i, j];
            }
            if (Math.Abs(rowSum - 1.0) > tolerance)
            {
                isRowStochastic = false;
                break;
            }
        }

        // Check column sums
        for (var j = 0; j < n; j++)
        {
            double colSum = 0;
            for (var i = 0; i < n; i++)
            {
                colSum += matrix[i, j];
            }
            if (Math.Abs(colSum - 1.0) > tolerance)
            {
                isColStochastic = false;
                break;
            }
        }

        var result = MatrixProperties.None;
        if (isRowStochastic) result |= MatrixProperties.Stochastic;
        if (isRowStochastic && isColStochastic) result |= MatrixProperties.DoublyStochastic;
        
        return result;
    }

    /// <summary>
    /// Checks if a real matrix is orthogonal.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MatrixProperties CheckOrthogonalProperty(this Matrix<double> matrix, double tolerance = Float64Utils.DefaultTolerance)
    {
        var n = matrix.Size();
        
        // Compute matrixA^T * matrixA and check if it equals I
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                double sum = 0;
                for (var k = 0; k < n; k++)
                {
                    sum += matrix[k, i] * matrix[k, j];
                }
                
                var expected = i == j ? 1.0 : 0.0;
                if (Math.Abs(sum - expected) > tolerance)
                    return MatrixProperties.None;
            }
        }
        
        return MatrixProperties.Orthogonal;
    }

    /// <summary>
    /// Checks if a complex matrix is unitary.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MatrixProperties CheckUnitaryProperty(this Matrix<Complex> matrix, double tolerance = Float64Utils.DefaultTolerance)
    {
        var n = matrix.Size();
        
        // Compute matrixA^H * matrixA and check if it equals I
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                var sum = Complex.Zero;
                for (var k = 0; k < n; k++)
                {
                    sum += Complex.Conjugate(matrix[k, i]) * matrix[k, j];
                }
                
                var expected = i == j ? Complex.One : Complex.Zero;
                if ((sum - expected).Magnitude > tolerance)
                    return MatrixProperties.None;
            }
        }
        
        return MatrixProperties.Unitary;
    }

    /// <summary>
    /// Checks if a complex matrix is normal (matrixA*matrixA^H = matrixA^H*matrixA).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MatrixProperties CheckNormalProperty(this Matrix<Complex> matrix, double tolerance = Float64Utils.DefaultTolerance)
    {
        var n = matrix.Size();
        
        // Compute matrixA*matrixA^H and matrixA^H*matrixA and check if they're equal
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                var sum1 = Complex.Zero; // (matrixA*matrixA^H)[i,j]
                var sum2 = Complex.Zero; // (matrixA^H*matrixA)[i,j]
                
                for (var k = 0; k < n; k++)
                {
                    sum1 += matrix[i, k] * Complex.Conjugate(matrix[j, k]);
                    sum2 += Complex.Conjugate(matrix[k, i]) * matrix[k, j];
                }
                
                if ((sum1 - sum2).Magnitude > tolerance)
                    return MatrixProperties.None;
            }
        }
        
        return MatrixProperties.Normal;
    }

    /// <summary>
    /// Checks special properties like nilpotent, idempotent, and involutory.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MatrixProperties CheckSpecialProperties(this Matrix<double> matrix, double tolerance = Float64Utils.DefaultTolerance)
    {
        var result = MatrixProperties.None;
        var n = matrix.Size();
        
        // Check for nilpotent property: matrixA^k = 0 for some k
        // We check indices 2, 3, and 4 for practical cases
        var currentPower = matrix.Clone();
        var isNilpotent = false;
        
        for (var k = 2; k <= Math.Min(4, n + 1) && !isNilpotent; k++)
        {
            currentPower *= matrix;
            
            var isPowerZero = true;
            for (var i = 0; i < n && isPowerZero; i++)
            {
                for (var j = 0; j < n && isPowerZero; j++)
                {
                    if (Math.Abs(currentPower[i, j]) > tolerance)
                        isPowerZero = false;
                }
            }
            
            if (isPowerZero)
            {
                isNilpotent = true;
                result |= MatrixProperties.Nilpotent;
            }
        }

        // Check if matrixA^2 = matrixA (idempotent)
        var squared = matrix * matrix;
        var isIdempotent = true;
        for (var i = 0; i < n && isIdempotent; i++)
        {
            for (var j = 0; j < n && isIdempotent; j++)
            {
                if (Math.Abs(squared[i, j] - matrix[i, j]) > tolerance)
                    isIdempotent = false;
            }
        }
        if (isIdempotent) result |= MatrixProperties.Idempotent;

        // Check if matrixA^2 = I (involutory)
        var isInvolutory = true;
        for (var i = 0; i < n && isInvolutory; i++)
        {
            for (var j = 0; j < n && isInvolutory; j++)
            {
                var expected = i == j ? 1.0 : 0.0;
                if (Math.Abs(squared[i, j] - expected) > tolerance)
                    isInvolutory = false;
            }
        }
        if (isInvolutory) result |= MatrixProperties.Involutory;

        return result;
    }


}