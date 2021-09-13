namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.Signatures
{
    public interface IGeometricAlgebraSignatureLookup
        : IGeometricAlgebraSignature
    {
        IGeometricAlgebraSignatureComputed BaseSignature { get; }
    }
}