using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    internal static class MultivectorStorageGpEucUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBilinearProductResult Gp(this BasisBladeSet basisSet, ulong id)
        {
            return new BasisBilinearProductResult(
                basisSet.GpSquaredSign(id), 
                0
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBilinearProductResult GpReverse(this BasisBladeSet basisSet, ulong id)
        {
            return new BasisBilinearProductResult(
                basisSet.GpReverseSign(id, id), 
                0
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBilinearProductResult Gp(this BasisBladeSet basisSet, ulong id1, ulong id2)
        {
            return new BasisBilinearProductResult(
                basisSet.GpSign(id1, id2), 
                id1 ^ id2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBilinearProductResult GpReverse(this BasisBladeSet basisSet, ulong id1, ulong id2)
        {
            return new BasisBilinearProductResult(
                basisSet.GpReverseSign(id1, id2), 
                id1 ^ id2
            );
        }

        public static IMultivectorStorage<T> EGpSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1)
        {
            var composer = 
                scalarProcessor.CreateVectorStorageComposer();

            var idScalarPairs = 
                mv1.GetLinVectorIdScalarStorage();

            foreach (var (id1, scalar1) in idScalarPairs.GetIndexScalarRecords())
            {
                foreach (var (id2, scalar2) in idScalarPairs.GetIndexScalarRecords())
                {
                    var basisSet = 
                        BasisBladeProductUtils.EGpSign(id1, id2);

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (basisSet > 0)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateMultivectorStorageSparse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T EGp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T mv1, T mv2)
        {
            return scalarProcessor.Times(mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> EGp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv1, T mv2)
        {
            return scalarProcessor.Times(mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> EGp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T mv1, VectorStorage<T> mv2)
        {
            return scalarProcessor.Times(mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> EGp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BivectorStorage<T> mv1, T mv2)
        {
            return scalarProcessor.Times(mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> EGp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T mv1, BivectorStorage<T> mv2)
        {
            return scalarProcessor.Times(mv1, mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> EGp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv1, T mv2)
        {
            return scalarProcessor.Times(mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> EGp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T mv1, KVectorStorage<T> mv2)
        {
            return scalarProcessor.Times(mv1, mv2);
        }

        public static IMultivectorStorage<T> EGp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            var composer = 
                scalarProcessor.CreateVectorStorageComposer();

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetLinVectorIdScalarStorage();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                foreach (var (id2, scalar2) in idScalarPairs2.GetIndexScalarRecords())
                {
                    var basisSet = 
                        BasisBladeProductUtils.EGpSign(id1, id2);

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (basisSet > 0)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateMultivectorStorageSparse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> EGp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2, IMultivectorStorage<T> mv3)
        {
            return scalarProcessor.EGp(scalarProcessor.EGp(mv1, mv2), mv3);
        }

        public static IMultivectorStorage<T> EGpReverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1)
        {
            var composer = 
                scalarProcessor.CreateVectorStorageComposer();

            var idScalarPairs = 
                mv1.GetLinVectorIdScalarStorage();

            foreach (var (id1, scalar1) in idScalarPairs.GetIndexScalarRecords())
            {
                foreach (var (id2, scalar2) in idScalarPairs.GetIndexScalarRecords())
                {
                    var basisSet = 
                        BasisBladeProductUtils.EGpReverseSign(id1, id2);

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (basisSet > 0)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateMultivectorStorageSparse();
        }

        public static IMultivectorStorage<T> EGpReverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            var composer = 
                scalarProcessor.CreateVectorStorageComposer();

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetLinVectorIdScalarStorage();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                foreach (var (id2, scalar2) in idScalarPairs2.GetIndexScalarRecords())
                {
                    var basisSet = 
                        BasisBladeProductUtils.EGpSign(id1, id2);

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (basisSet > 0)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateMultivectorStorageSparse();
        }


    }
}
