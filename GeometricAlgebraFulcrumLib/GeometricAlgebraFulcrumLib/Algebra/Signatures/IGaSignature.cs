using GeometricAlgebraFulcrumLib.Algebra.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra.Signatures
{
    public interface IGaSignature : 
        IGaSpace
    {
        uint SignatureId { get; }

        uint PositiveCount { get; }

        uint NegativeCount { get; }

        uint ZeroCount { get; }

        bool IsEuclidean { get; }

        bool IsProjective { get; }

        bool IsConformal { get; }

        bool IsMotherAlgebra { get; }

        int GetBasisVectorSignature(int index);

        int GetBasisVectorSignature(ulong index);

        int GetBasisBivectorSignature(int index1, int index2);

        int GetBasisBivectorSignature(ulong index1, ulong index2);

        int GetBasisBladeSignature(uint grade, ulong index);

        int GetBasisBladeSignature(ulong id);

        int GetBasisBladeSignature(IGaBasisBlade basisBlade);

        int GpSignature(ulong id);

        int GpSignature(ulong id1, ulong id2);

        int GpReverseSignature(ulong id1, ulong id2);

        int OpSignature(ulong id1, ulong id2);

        int SpSignature(ulong id);

        int SpSignature(ulong id1, ulong id2);

        int NormSquaredSignature(ulong id);

        int LcpSignature(ulong id1, ulong id2);

        int RcpSignature(ulong id1, ulong id2);

        int FdpSignature(ulong id1, ulong id2);

        int HipSignature(ulong id1, ulong id2);

        int AcpSignature(ulong id1, ulong id2);

        int CpSignature(ulong id1, ulong id2);
    }
}
