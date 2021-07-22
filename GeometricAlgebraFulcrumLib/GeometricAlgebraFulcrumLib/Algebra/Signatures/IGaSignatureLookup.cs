namespace GeometricAlgebraFulcrumLib.Algebra.Signatures
{
    public interface IGaSignatureLookup
        : IGaSignature
    {
        IGaSignatureComputed BaseSignature { get; }
    }
}