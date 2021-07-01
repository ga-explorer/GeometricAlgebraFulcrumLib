using GeometricAlgebraLib.Multivectors.Basis;
using GeometricAlgebraLib.Multivectors.Signatures;
using GeometricAlgebraLib.Outermorphisms;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Processors.Multivectors
{
    public interface IGaMultivectorsProcessor<T>
    {
        IGaScalarProcessor<T> ScalarProcessor { get; }

        IGaSignature Signature { get; }

        GaBasisSet BasisSet { get; }

        GaMultivectorsProcessorChangeOfBasis<T> CreateChangeOfBasisProcessor(GaOmChangeOfBasis<T> outermorphism);

        IGaMultivectorStorage<T> Normalize(IGaMultivectorStorage<T> storage1);

        IGaMultivectorStorage<T> Gp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        IGaMultivectorStorage<T> Gp(params IGaMultivectorStorage<T>[] storagesList);

        T Sp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        IGaMultivectorStorage<T> Lcp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        IGaMultivectorStorage<T> Rcp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        IGaMultivectorStorage<T> Hip(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        IGaMultivectorStorage<T> Fdp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        IGaMultivectorStorage<T> Acp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        IGaMultivectorStorage<T> Cp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        IGaMultivectorStorage<T> GpSquared(IGaMultivectorStorage<T> storage1);

        IGaMultivectorStorage<T> GpReverse(IGaMultivectorStorage<T> storage1);

        T SpSquared(IGaMultivectorStorage<T> storage1);

        T SpReverse(IGaMultivectorStorage<T> storage1);

        T NormSquared(IGaMultivectorStorage<T> storage1);

        T Norm(IGaMultivectorStorage<T> storage1);

        IGaMultivectorStorage<T> VersorInverse(IGaMultivectorStorage<T> storage1);

        IGaMultivectorStorage<T> BladeInverse(IGaMultivectorStorage<T> storage1);
    }
}