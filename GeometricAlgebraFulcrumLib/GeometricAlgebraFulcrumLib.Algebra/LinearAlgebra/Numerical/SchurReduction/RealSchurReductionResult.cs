using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.SchurReduction;

/// <summary>
/// https://en.wikipedia.org/wiki/Schur_decomposition
/// </summary>
public sealed class RealSchurReductionResult
{
    public Matrix<double> OriginalMatrix { get; } 

    public Matrix<double> SchurVectors { get; } 
        
    public Matrix<double> QuasiTriangularMatrix { get; }

    public bool Success { get; }

    public int Size { get; }
        
    public double Tolerance { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RealSchurReductionResult(Matrix<double> originalMatrix, Matrix<double> schurVectors, Matrix<double> quasiTriangularMatrix, bool success, double tolerance = Float64Utils.DefaultTolerance)
    {
        Success = success;
        OriginalMatrix = originalMatrix;
        SchurVectors = schurVectors;
        QuasiTriangularMatrix = quasiTriangularMatrix;
        Tolerance = tolerance;
        Size = originalMatrix.Size();
    }


}