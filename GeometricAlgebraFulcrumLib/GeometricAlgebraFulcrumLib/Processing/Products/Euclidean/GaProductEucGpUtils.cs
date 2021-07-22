using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Processing.Products.Euclidean
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

        public static IGasMultivector<T> EGp<T>(this IGasMultivector<T> mv1)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            var composer = 
                new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

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

            return composer.GetCompactMultivector();
        }

        public static IGasMultivector<T> EGp<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            var composer = 
                new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

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

            return composer.GetCompactMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> EGp<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2, IGasMultivector<T> mv3)
        {
            return mv1.EGp(mv2).EGp(mv3);
        }

        public static IGasMultivector<T> EGpReverse<T>(this IGasMultivector<T> mv1)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            var composer = 
                new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

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

            return composer.GetCompactMultivector();
        }

        public static IGasMultivector<T> EGpReverse<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            var composer = 
                new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

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

            return composer.GetCompactMultivector();
        }


    }
}
