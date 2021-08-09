using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean
{
    public static class GaProductEucGpUtils
    {
        public static GaBasisBilinearProductResult Gp(this IGaSignature signature, ulong id)
        {
            return new GaBasisBilinearProductResult(
                signature.GpSignature(id), 
                0
            );
        }

        public static GaBasisBilinearProductResult GpReverse(this IGaSignature signature, ulong id)
        {
            return new GaBasisBilinearProductResult(
                signature.GpReverseSignature(id, id), 
                0
            );
        }

        public static GaBasisBilinearProductResult Gp(this IGaSignature signature, ulong id1, ulong id2)
        {
            return new GaBasisBilinearProductResult(
                signature.GpSignature(id1, id2), 
                id1 ^ id2
            );
        }

        public static GaBasisBilinearProductResult GpReverse(this IGaSignature signature, ulong id1, ulong id2)
        {
            return new GaBasisBilinearProductResult(
                signature.GpReverseSignature(id1, id2), 
                id1 ^ id2
            );
        }

        public static IGaStorageMultivector<T> EGp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1)
        {
            var composer = 
                new GaStorageComposerMultivectorSparse<T>(scalarProcessor);

            var idScalarPairs = 
                mv1.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs)
            {
                foreach (var (id2, scalar2) in idScalarPairs)
                {
                    var signature = 
                        GaBasisUtils.EGpSignature(id1, id2);

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.GetMultivector();
        }

        public static IGaStorageMultivector<T> EGp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            var composer = 
                new GaStorageComposerMultivectorSparse<T>(scalarProcessor);

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                foreach (var (id2, scalar2) in idScalarPairs2)
                {
                    var signature = 
                        GaBasisUtils.EGpSignature(id1, id2);

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> EGp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2, IGaStorageMultivector<T> mv3)
        {
            return scalarProcessor.EGp(scalarProcessor.EGp(mv1, mv2), mv3);
        }

        public static IGaStorageMultivector<T> EGpReverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1)
        {
            var composer = 
                new GaStorageComposerMultivectorSparse<T>(scalarProcessor);

            var idScalarPairs = 
                mv1.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs)
            {
                foreach (var (id2, scalar2) in idScalarPairs)
                {
                    var signature = 
                        GaBasisUtils.EGpReverseSignature(id1, id2);

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.GetMultivector();
        }

        public static IGaStorageMultivector<T> EGpReverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            var composer = 
                new GaStorageComposerMultivectorSparse<T>(scalarProcessor);

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                foreach (var (id2, scalar2) in idScalarPairs2)
                {
                    var signature = 
                        GaBasisUtils.EGpSignature(id1, id2);

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.GetMultivector();
        }


    }
}
