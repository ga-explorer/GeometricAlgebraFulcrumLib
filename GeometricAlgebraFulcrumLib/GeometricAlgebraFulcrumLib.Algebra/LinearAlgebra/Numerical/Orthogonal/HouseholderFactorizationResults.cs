using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Orthogonal;

/// <summary>
/// The results of factorizing a matrix Q using k Householder reflections:
/// Q: H0 * H1 * ... * H_{k-1} * R
/// </summary>
public sealed class HouseholderFactorizationResults
{
    private readonly IReadOnlyList<Vector<double>> _mainHouseholderVectors;


    public Matrix<double> OriginalMatrix { get; }
        
    public Matrix<double> FinalMatrix { get; }

    public int Size { get; }

    public double Tolerance { get; }
    

    internal HouseholderFactorizationResults(Matrix<double> originalMatrix, Matrix<double> finalMatrix, IReadOnlyList<Vector<double>> householderVectors, double tolerance = Float64Utils.DefaultTolerance)
    {
        OriginalMatrix = originalMatrix;
        FinalMatrix = finalMatrix;
        _mainHouseholderVectors = householderVectors;
        Tolerance = tolerance;
        Size = originalMatrix.Size();
    }

    /// <summary>
    /// True if the original and final matrices are orthogonal
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsOrthogonal()
    {
        return FinalMatrix.IsNearUnitDiagonal(Tolerance);
    }
    
    /// <summary>
    /// Reconstruct the matrix from the factorization data
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Matrix<double> ReconstructMatrix()
    {
        // Reconstruct Q: H0 * H1 * ... * H_{k-1} * R
        var matrixR = _mainHouseholderVectors
            .AsEnumerable()
            .Reverse()
            .Select(v => v.ToDenseHouseholderMatrix(true))
            .Aggregate(
                FinalMatrix, 
                (matrixR, householderMatrix) => householderMatrix * matrixR
            ); 
        
        matrixR.RemoveSmallScalars(Tolerance);

        return matrixR;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetHouseholderVectorsCount(bool includeBasisReflections = true)
    {
        var c = _mainHouseholderVectors.Count;

        if (includeBasisReflections && IsOrthogonal())
            c += FinalMatrix.GetNegativeDiagonalItemsCount();
        
        return c;
    }

    public IEnumerable<Vector<double>> GetHouseholderVectors(bool includeBasisReflections = true)
    {
        foreach (var v in _mainHouseholderVectors)
            yield return v;

        if (!includeBasisReflections || !IsOrthogonal())
            yield break;

        foreach (var i in FinalMatrix.GetNegativeDiagonalItemIndices())
        {
            var v = SparseVector.Create(Size, 0);
            v.At(i, 1);

            yield return v;
        }
    }

    public IEnumerable<XGaFloat64Vector> GetHouseholderXGaVectors(XGaFloat64Processor processor, bool includeBasisReflections = true)
    {
        foreach (var v in _mainHouseholderVectors)
            yield return processor.Vector(v);

        if (!includeBasisReflections || !IsOrthogonal())
            yield break;

        foreach (var i in FinalMatrix.GetNegativeDiagonalItemIndices())
            yield return processor.VectorTerm(i);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector GetHouseholderVersor(XGaFloat64Processor processor, bool includeBasisReflections = true)
    {
        return GetHouseholderXGaVectors(processor, includeBasisReflections)
            .AsEnumerable()
            .Aggregate(
                (XGaFloat64Multivector)processor.ScalarOne, 
                (versor, householderVector) => householderVector.EGp(versor)
            );
    }
}