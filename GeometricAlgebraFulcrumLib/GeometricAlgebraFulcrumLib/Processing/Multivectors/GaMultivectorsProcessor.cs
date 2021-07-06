using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors
{
    public sealed class GaMultivectorsProcessor<T>
        : GaMultivectorsProcessorBase<T>
    {
        public static GaMultivectorsProcessor<T> CreateEuclidean(IGaScalarProcessor<T> scalarProcessor)
        {
            return new GaMultivectorsProcessor<T>(
                scalarProcessor, 
                GaSignatureEuclidean.Create()
            );
        }

        public static GaMultivectorsProcessor<T> CreateEuclidean(IGaScalarProcessor<T> scalarProcessor, int vSpaceDimension)
        {
            return new GaMultivectorsProcessor<T>(
                scalarProcessor, 
                GaSignatureEuclidean.Create(vSpaceDimension)
            );
        }

        public static GaMultivectorsProcessor<T> CreateProjective(IGaScalarProcessor<T> scalarProcessor, int euclideanVSpaceDimension)
        {
            return new GaMultivectorsProcessor<T>(
                scalarProcessor, 
                GaSignatureProjective.Create(euclideanVSpaceDimension)
            );
        }

        public static GaMultivectorsProcessor<T> CreateConformal(IGaScalarProcessor<T> scalarProcessor, int euclideanVSpaceDimension)
        {
            return new GaMultivectorsProcessor<T>(
                scalarProcessor, 
                GaSignatureConformal.Create(euclideanVSpaceDimension)
            );
        }


        public override IGaSignature Signature { get; }

        public override IGaKVectorStorage<T> PseudoScalar { get; }

        public override IGaKVectorStorage<T> PseudoScalarInverse { get; }

        public override IGaKVectorStorage<T> PseudoScalarReverse { get; }


        private GaMultivectorsProcessor(IGaScalarProcessor<T> scalarProcessor, [NotNull] IGaSignature basesSignature) 
            : base(scalarProcessor)
        {
            Signature = basesSignature;

            PseudoScalar = GaKVectorTermStorage<T>.CreatePseudoScalar(
                ScalarProcessor, 
                Signature.VSpaceDimension
            );

            PseudoScalarInverse = 
                Signature
                    .BladeInverse(PseudoScalar)
                    .GetKVectorPart(Signature.VSpaceDimension);

            PseudoScalarReverse = 
                PseudoScalar
                    .GetReverse()
                    .GetKVectorPart(Signature.VSpaceDimension);
        }


        public override IGaMultivectorStorage<T> Gp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return Signature.Gp(mv1, mv2);
        }

        public override IGaMultivectorStorage<T> GpReverse(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return Signature.GpReverse(mv1, mv2);
        }

        public override T Sp(IGaMultivectorStorage<T> mv1)
        {
            return Signature.Sp(mv1);
        }

        public override T Sp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return Signature.Sp(mv1, mv2);
        }

        public override IGaMultivectorStorage<T> Lcp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return Signature.Lcp(mv1, mv2);
        }

        public override IGaMultivectorStorage<T> Rcp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return Signature.Rcp(mv1, mv2);
        }

        public override IGaMultivectorStorage<T> Hip(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return Signature.Hip(mv1, mv2);
        }

        public override IGaMultivectorStorage<T> Fdp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return Signature.Fdp(mv1, mv2);
        }

        public override IGaMultivectorStorage<T> Acp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return Signature.Acp(mv1, mv2);
        }

        public override IGaMultivectorStorage<T> Cp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return Signature.Cp(mv1, mv2);
        }

        public override IGaMultivectorStorage<T> Gp(IGaMultivectorStorage<T> mv1)
        {
            return Signature.Gp(mv1);
        }

        public override IGaMultivectorStorage<T> GpReverse(IGaMultivectorStorage<T> mv1)
        {
            return Signature.GpReverse(mv1);
        }

        public override T NormSquared(IGaMultivectorStorage<T> mv1)
        {
            return Signature.NormSquared(mv1);
        }
    }
}