using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors
{
    public abstract class GaMultivectorsProcessorBase<T> :
        IGaMultivectorProcessor<T>
    {
        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public abstract IGaSignature Signature { get; }

        public GaBasisSet BasisSet 
            => Signature.BasisSet;

        public abstract IGaKVectorStorage<T> PseudoScalar { get; }

        public abstract IGaKVectorStorage<T> PseudoScalarInverse { get; }

        public abstract IGaKVectorStorage<T> PseudoScalarReverse { get; }


        protected GaMultivectorsProcessorBase([NotNull] IGaScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
        }


        public virtual GaMultivectorsProcessorChangeOfBasis<T> CreateChangeOfBasisProcessor(GaOmChangeOfBasis<T> outermorphism)
        {
            return new GaMultivectorsProcessorChangeOfBasis<T>(this, outermorphism);
        }

        public virtual IGaMultivectorStorage<T> Normalize(IGaMultivectorStorage<T> storage1)
        {
            return storage1.Divide(
                ScalarProcessor.SqrtOfAbs(NormSquared(storage1))
            );
        }

        public abstract IGaMultivectorStorage<T> Gp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        public abstract IGaMultivectorStorage<T> GpReverse(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        public IGaMultivectorStorage<T> Gp(params IGaMultivectorStorage<T>[] storagesList)
        {
            return storagesList.Skip(1).Aggregate(
                storagesList[0], 
                Gp
            );
        }

        public abstract T Sp(IGaMultivectorStorage<T> storage1);

        public abstract T Sp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        public abstract IGaMultivectorStorage<T> Lcp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        public abstract IGaMultivectorStorage<T> Rcp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        public abstract IGaMultivectorStorage<T> Hip(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        public abstract IGaMultivectorStorage<T> Fdp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        public abstract IGaMultivectorStorage<T> Acp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        public abstract IGaMultivectorStorage<T> Cp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        public abstract IGaMultivectorStorage<T> Gp(IGaMultivectorStorage<T> storage1);

        public abstract IGaMultivectorStorage<T> GpReverse(IGaMultivectorStorage<T> storage1);

        public abstract T NormSquared(IGaMultivectorStorage<T> storage1);

        public T Norm(IGaMultivectorStorage<T> storage1)
        {
            return ScalarProcessor.SqrtOfAbs(NormSquared(storage1));
        }

        public IGaMultivectorStorage<T> Dual(IGaMultivectorStorage<T> storage1)
        {
            return Lcp(storage1, PseudoScalarInverse);
        }

        public IGaMultivectorStorage<T> UnDual(IGaMultivectorStorage<T> storage1)
        {
            return Lcp(storage1, PseudoScalar);
        }
        
        public IGaMultivectorStorage<T> BladeInverse(IGaMultivectorStorage<T> mv1)
        {
            var bladeSpSquared = Sp(mv1);

            return mv1.Divide(bladeSpSquared);
        }

        public IGaMultivectorStorage<T> VersorInverse(IGaMultivectorStorage<T> mv1)
        {
            var versorSpReverse = NormSquared(mv1);

            return mv1.GetReverse().Divide(versorSpReverse);
        }
    }
}