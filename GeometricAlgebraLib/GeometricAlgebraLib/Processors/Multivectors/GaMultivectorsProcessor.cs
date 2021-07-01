using System;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraLib.Multivectors.Signatures;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage;
using GeometricAlgebraLib.Storage.Composers;

namespace GeometricAlgebraLib.Processors.Multivectors
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


        private GaMultivectorsProcessor(IGaScalarProcessor<T> scalarProcessor, [NotNull] IGaSignature basesSignature) 
            : base(scalarProcessor)
        {
            Signature = basesSignature;
        }


        public override IGaMultivectorStorage<T> Gp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            var composer = new GaMultivectorTermsStorageComposer<T>(ScalarProcessor);

            var idScalarPairs1 = storage1.GetIdScalarPairs();
            var idScalarDictionary2 = storage1.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                foreach (var (id2, scalar2) in idScalarDictionary2)
                {
                    var signature = 
                        Signature.GpSignature(id1, id2);

                    if (signature == 0)
                        continue;

                    composer.AddTerm(
                        id1 ^ id2, 
                        signature < 0
                            ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                            : ScalarProcessor.Times(scalar1, scalar2)
                    );
                }    
            }

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public override T Sp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<T> Lcp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<T> Rcp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<T> Hip(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<T> Fdp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<T> Acp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<T> Cp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<T> GpSquared(IGaMultivectorStorage<T> storage1)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<T> GpReverse(IGaMultivectorStorage<T> storage1)
        {
            throw new NotImplementedException();
        }

        public override T SpSquared(IGaMultivectorStorage<T> storage1)
        {
            throw new NotImplementedException();
        }

        public override T SpReverse(IGaMultivectorStorage<T> storage1)
        {
            throw new NotImplementedException();
        }

        public override T NormSquared(IGaMultivectorStorage<T> storage1)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<T> VersorInverse(IGaMultivectorStorage<T> storage1)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<T> BladeInverse(IGaMultivectorStorage<T> storage1)
        {
            throw new NotImplementedException();
        }
    }
}