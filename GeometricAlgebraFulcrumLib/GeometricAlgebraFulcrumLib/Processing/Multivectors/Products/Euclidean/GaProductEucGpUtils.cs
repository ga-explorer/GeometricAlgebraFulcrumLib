using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

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

        public static IGaMultivectorStorage<T> EGp<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1)
        {
            var composer = 
                scalarProcessor.CreateStorageSparseMultivectorComposer();

            var idScalarPairs = 
                mv1.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs.GetIndexScalarRecords())
            {
                foreach (var (id2, scalar2) in idScalarPairs.GetIndexScalarRecords())
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

            return composer.CreateGaMultivectorSparseStorage();
        }

        public static IGaMultivectorStorage<T> EGp<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            var composer = 
                scalarProcessor.CreateStorageSparseMultivectorComposer();

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                foreach (var (id2, scalar2) in idScalarPairs2.GetIndexScalarRecords())
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

            return composer.CreateGaMultivectorSparseStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> EGp<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaMultivectorStorage<T> mv3)
        {
            return scalarProcessor.EGp(scalarProcessor.EGp(mv1, mv2), mv3);
        }

        public static IGaMultivectorStorage<T> EGpReverse<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1)
        {
            var composer = 
                scalarProcessor.CreateStorageSparseMultivectorComposer();

            var idScalarPairs = 
                mv1.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs.GetIndexScalarRecords())
            {
                foreach (var (id2, scalar2) in idScalarPairs.GetIndexScalarRecords())
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

            return composer.CreateGaMultivectorSparseStorage();
        }

        public static IGaMultivectorStorage<T> EGpReverse<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            var composer = 
                scalarProcessor.CreateStorageSparseMultivectorComposer();

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                foreach (var (id2, scalar2) in idScalarPairs2.GetIndexScalarRecords())
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

            return composer.CreateGaMultivectorSparseStorage();
        }


    }
}
