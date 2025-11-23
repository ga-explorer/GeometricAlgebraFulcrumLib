using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Orthogonal;

/// <summary>
/// The results of reducing a matrix Q using k Householder reflections:
/// Q: H0 * H1 * ... * H_{k-1} * R * H_{k-1} * ... * H1 * H0
/// </summary>
public sealed class HouseholderReductionResults
{
    public Matrix<double> OriginalMatrix { get; }
        
    public Matrix<double> ReducedMatrix { get; }

    public IReadOnlyList<Vector<double>> HouseholderVectors { get; }

    public int Size { get; }

    public double Tolerance { get; }
    

    internal HouseholderReductionResults(Matrix<double> originalMatrix, Matrix<double> finalMatrix, IReadOnlyList<Vector<double>> householderVectors, double tolerance = Float64Utils.DefaultTolerance)
    {
        OriginalMatrix = originalMatrix;
        ReducedMatrix = finalMatrix;
        HouseholderVectors = householderVectors;
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
        return ReducedMatrix.IsNearUnitDiagonal(Tolerance);
    }
    
    /// <summary>
    /// Reconstruct the matrix from the factorization data
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Matrix<double> ReconstructMatrix()
    {
        // Reconstruct Q: H0 * H1 * ... * H_{k-1} * R
        var matrixR = HouseholderVectors
            .AsEnumerable()
            .Reverse()
            .Select(v => v.ToDenseHouseholderMatrix(true))
            .Aggregate(
                ReducedMatrix, 
                (matrixR, householderMatrix) => householderMatrix * matrixR * householderMatrix
            ); 
        
        matrixR.RemoveSmallScalars(Tolerance);

        return matrixR;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<XGaFloat64Vector> GetHouseholderXGaVectors(XGaFloat64Processor processor)
    {
        return HouseholderVectors.Select(processor.Vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector GetHouseholderVersor(XGaFloat64Processor processor)
    {
        return GetHouseholderXGaVectors(processor)
            .AsEnumerable()
            .Aggregate(
                (XGaFloat64Multivector)processor.ScalarOne, 
                (versor, householderVector) => householderVector.EGp(versor)
            );
    }
}