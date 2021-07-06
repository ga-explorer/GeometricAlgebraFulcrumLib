using GeometricAlgebraLib.Algebra.Basis;
using GeometricAlgebraLib.Algebra.Outermorphisms;
using GeometricAlgebraLib.Algebra.Signatures;
using GeometricAlgebraLib.Processing.Scalars;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Processing.Multivectors
{
    public interface IGaMultivectorProcessor<T>
    {
        IGaScalarProcessor<T> ScalarProcessor { get; }

        IGaSignature Signature { get; }

        GaBasisSet BasisSet { get; }

        IGaKVectorStorage<T> PseudoScalar { get; }

        IGaKVectorStorage<T> PseudoScalarInverse { get; }

        IGaKVectorStorage<T> PseudoScalarReverse { get; }

        GaMultivectorsProcessorChangeOfBasis<T> CreateChangeOfBasisProcessor(GaOmChangeOfBasis<T> outermorphism);

        IGaMultivectorStorage<T> Normalize(IGaMultivectorStorage<T> storage1);

        IGaMultivectorStorage<T> Gp(IGaMultivectorStorage<T> storage1);

        IGaMultivectorStorage<T> GpReverse(IGaMultivectorStorage<T> storage1);

        IGaMultivectorStorage<T> Gp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        IGaMultivectorStorage<T> GpReverse(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        IGaMultivectorStorage<T> Gp(params IGaMultivectorStorage<T>[] storagesList);

        T Sp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        IGaMultivectorStorage<T> Lcp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        IGaMultivectorStorage<T> Rcp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        IGaMultivectorStorage<T> Hip(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        IGaMultivectorStorage<T> Fdp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        IGaMultivectorStorage<T> Acp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        IGaMultivectorStorage<T> Cp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        T Sp(IGaMultivectorStorage<T> storage1);

        T NormSquared(IGaMultivectorStorage<T> storage1);

        T Norm(IGaMultivectorStorage<T> storage1);

        IGaMultivectorStorage<T> VersorInverse(IGaMultivectorStorage<T> storage1);

        IGaMultivectorStorage<T> BladeInverse(IGaMultivectorStorage<T> storage1);

        IGaMultivectorStorage<T> Dual(IGaMultivectorStorage<T> storage1);

        IGaMultivectorStorage<T> UnDual(IGaMultivectorStorage<T> storage1);
    }
}