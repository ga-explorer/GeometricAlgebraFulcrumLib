using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

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
                scalarProcessor.CreateStorageSparseMultivectorComposer();

            var idScalarPairs = 
                mv1.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs.GetKeyValueRecords())
            {
                foreach (var (id2, scalar2) in idScalarPairs.GetKeyValueRecords())
                {
                    var signature = 
                        GaBasisBladeProductUtils.EGpSignature(id1, id2);

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateStorageSparseMultivector();
        }

        public static IGaStorageMultivector<T> EGp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            var composer = 
                scalarProcessor.CreateStorageSparseMultivectorComposer();

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                foreach (var (id2, scalar2) in idScalarPairs2.GetKeyValueRecords())
                {
                    var signature = 
                        GaBasisBladeProductUtils.EGpSignature(id1, id2);

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateStorageSparseMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> EGp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2, IGaStorageMultivector<T> mv3)
        {
            return scalarProcessor.EGp(scalarProcessor.EGp(mv1, mv2), mv3);
        }

        public static IGaStorageMultivector<T> EGpReverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1)
        {
            var composer = 
                scalarProcessor.CreateStorageSparseMultivectorComposer();

            var idScalarPairs = 
                mv1.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs.GetKeyValueRecords())
            {
                foreach (var (id2, scalar2) in idScalarPairs.GetKeyValueRecords())
                {
                    var signature = 
                        GaBasisBladeProductUtils.EGpReverseSignature(id1, id2);

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateStorageSparseMultivector();
        }

        public static IGaStorageMultivector<T> EGpReverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            var composer = 
                scalarProcessor.CreateStorageSparseMultivectorComposer();

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                foreach (var (id2, scalar2) in idScalarPairs2.GetKeyValueRecords())
                {
                    var signature = 
                        GaBasisBladeProductUtils.EGpSignature(id1, id2);

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateStorageSparseMultivector();
        }


    }
}
