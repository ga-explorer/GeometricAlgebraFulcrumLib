using System.Data;

namespace GeometricAlgebraFulcrumLib.Algebra.TensorAlgebra.Generic.Core.Exceptions;

/// <summary>
/// Thrown when a wrong determinant tensor was provided
/// </summary>
public class InvalidDeterminantException :
    DataException
{
    internal InvalidDeterminantException(string msg)
        : base(msg) { }

    internal InvalidDeterminantException()
    { }
}