namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Records;

public interface IXGaScalarRecord<out T>
{
    /// <summary>
    /// The Scalar Value
    /// </summary>
    T Scalar { get; }
}