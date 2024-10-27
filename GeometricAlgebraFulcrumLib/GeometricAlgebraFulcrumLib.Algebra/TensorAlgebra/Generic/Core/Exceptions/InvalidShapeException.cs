namespace GeometricAlgebraFulcrumLib.Algebra.TensorAlgebra.Generic.Core.Exceptions;

/// <summary>
/// Occurs when an axis mismatch happens
/// </summary>
public class InvalidShapeException :
    ArgumentException
{
    internal InvalidShapeException(string msg)
        : base(msg) { }

    internal InvalidShapeException()
    { }

    internal static void NeedTensorSquareMatrix<T, TWrapper>(GenTensor<T, TWrapper> m) where TWrapper : struct, IOperations<T>
    {
        if (m.Shape.Length <= 2)
            throw new InvalidShapeException("Should be 3+ dimensional");
        if (m.Shape.Shape[m.Shape.Length - 1] != m.Shape.Shape[m.Shape.Length - 2])
            throw new InvalidShapeException("The last two dimensions should be equal");
    }
}