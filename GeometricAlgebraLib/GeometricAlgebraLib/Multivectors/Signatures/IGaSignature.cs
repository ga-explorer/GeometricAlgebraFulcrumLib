using GeometricAlgebraLib.Multivectors.Basis;

namespace GeometricAlgebraLib.Multivectors.Signatures
{
    public interface IGaSignature
    {
        int SignatureId { get; }

        GaBasisSet BasisSet { get; }

        int VSpaceDimension { get; }

        int PositiveCount { get; }

        int NegativeCount { get; }

        int ZeroCount { get; }

        int GetBasisVectorSignature(int index);

        int GetBasisVectorSignature(ulong index);

        int GetBasisBivectorSignature(int index1, int index2);

        int GetBasisBivectorSignature(ulong index1, ulong index2);

        int GetBasisBladeSignature(int grade, ulong index);

        int GetBasisBladeSignature(ulong id);

        int GetBasisBladeSignature(IGaBasisBlade basisBlade);

        int GpSignature(ulong id1, ulong id2);

        int OpSignature(ulong id1, ulong id2);

        int SpSignature(ulong id1, ulong id2);

        int LcpSignature(ulong id1, ulong id2);

        int RcpSignature(ulong id1, ulong id2);

        int FdpSignature(ulong id1, ulong id2);

        int HipSignature(ulong id1, ulong id2);

        int AcpSignature(ulong id1, ulong id2);

        int CpSignature(ulong id1, ulong id2);
    }
}
