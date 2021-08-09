namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures
{
    public interface IGaSignatureLookup
        : IGaSignature
    {
        IGaSignatureComputed BaseSignature { get; }
    }
}