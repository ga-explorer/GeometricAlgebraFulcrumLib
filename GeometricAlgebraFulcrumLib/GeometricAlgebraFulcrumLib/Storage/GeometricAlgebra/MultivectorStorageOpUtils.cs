using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    internal static class MultivectorStorageOpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Op<T>(this IScalarProcessor<T> scalarProcessor, T mv1, T mv2)
        {
            return scalarProcessor.Times(mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> VectorsOp<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<ILinVectorStorage<T>> vectorsList)
        {
            return vectorsList
                    .Select(v => v.CreateVectorStorage())
                    .Aggregate(
                    scalarProcessor.CreateKVectorStorageBasisScalar(),
                    scalarProcessor.Op
                );
        }

        public static BivectorStorage<T> VectorsOp<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> vector1, ILinVectorStorage<T> vector2)
        {
            var storage = scalarProcessor.CreateVectorStorageComposer();

            foreach (var (index1, scalar1) in vector1.GetIndexScalarRecords())
            {
                foreach (var (index2, scalar2) in vector2.GetIndexScalarRecords())
                {
                    if (index1 == index2)
                        continue;

                    storage.AddBivectorTerm(
                        index1,
                        index2,
                        scalarProcessor.Times(scalar1, scalar2)
                    );
                }
            }

            storage.RemoveZeroTerms();

            return storage.CreateBivectorStorage();
        }

        private static BivectorStorage<T> OpAsBivectorStorage<T>(IScalarProcessor<T> scalarProcessor, VectorStorage<T> mv1, VectorStorage<T> mv2)
        {
            var composer =
                scalarProcessor.CreateVectorStorageComposer();

            var indexScalarPairs1 =
                mv1.GetLinVectorIndexScalarStorage();

            var indexScalarPairs2 =
                mv2.GetLinVectorIndexScalarStorage();

            foreach (var (index1, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                foreach (var (index2, scalar2) in indexScalarPairs2.GetIndexScalarRecords())
                {
                    if (index1 == index2)
                        continue;

                    composer.AddBivectorTerm(
                        index1,
                        index2,
                        scalarProcessor.Times(scalar1, scalar2)
                    );
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateBivectorStorage();
        }

        public static BivectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, VectorStorage<T> mv1, VectorStorage<T> mv2)
        {
            return OpAsBivectorStorage(scalarProcessor, mv1, mv2);
        }

        private static KVectorStorage<T> OpAsKVectorStorage<T>(IScalarProcessor<T> scalarProcessor, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
        {
            var grade1 = mv1.Grade;
            var grade2 = mv2.Grade;
            var grade = grade2 + grade1;

            if (grade > GeometricAlgebraSpaceUtils.MaxVSpaceDimension)
                return KVectorStorage<T>.ZeroScalar;

            var composer =
                scalarProcessor.CreateVectorStorageComposer();

            var indexScalarPairs1 =
                mv1.GetLinVectorIndexScalarStorage();

            var indexScalarPairs2 =
                mv2.GetLinVectorIndexScalarStorage();

            foreach (var (index1, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var id1 = index1.BasisBladeIndexToId(grade1);

                foreach (var (index2, scalar2) in indexScalarPairs2.GetIndexScalarRecords())
                {
                    var id2 = index2.BasisBladeIndexToId(grade2);

                    var signature =
                        id1.OpSign(id2);

                    if (signature.IsZero)
                        continue;

                    var index = (id1 ^ id2).BasisBladeIdToIndex();
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature.IsPositive)
                        composer.AddTerm(index, scalar);
                    else
                        composer.SubtractTerm(index, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateKVectorStorage(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, VectorStorage<T> mv1, T mv2)
        {
            return scalarProcessor.Times(mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, T mv1, VectorStorage<T> mv2)
        {
            return scalarProcessor.Times(mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, BivectorStorage<T> mv1, T mv2)
        {
            return scalarProcessor.Times(mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, T mv1, BivectorStorage<T> mv2)
        {
            return scalarProcessor.Times(mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, KVectorStorage<T> mv1, T mv2)
        {
            return scalarProcessor.Times(mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, T mv1, KVectorStorage<T> mv2)
        {
            return scalarProcessor.Times(mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
        {
            return mv1 switch
            {
                T s1 when mv2 is T s2 =>
                    scalarProcessor.Op(s1, s2).CreateKVectorStorageScalar(),

                VectorStorage<T> vt1 when mv2 is VectorStorage<T> vt2 =>
                    scalarProcessor.Op(vt1, vt2),

                _ =>
                    OpAsKVectorStorage(scalarProcessor, mv1, mv2)
            };
        }

        private static IMultivectorGradedStorage<T> OpAsMultivectorGradedStorage<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv1, IMultivectorGradedStorage<T> mv2)
        {
            var composer =
                scalarProcessor.CreateMultivectorGradedStorageComposer();

            var gradeIndexScalarDictionary1 = mv1.GetLinVectorGradedStorage();
            var gradeIndexScalarDictionary2 = mv2.GetLinVectorGradedStorage();

            foreach (var (grade1, indexScalarPairs1) in gradeIndexScalarDictionary1.GetGradeStorageRecords())
            {
                foreach (var (grade2, indexScalarPairs2) in gradeIndexScalarDictionary2.GetGradeStorageRecords())
                {
                    var grade = grade2 + grade1;

                    if (grade > GeometricAlgebraSpaceUtils.MaxVSpaceDimension)
                        continue;

                    foreach (var (index1, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
                    {
                        var id1 = index1.BasisBladeIndexToId(grade1);

                        foreach (var (index2, scalar2) in indexScalarPairs2.GetIndexScalarRecords())
                        {
                            var id2 = index2.BasisBladeIndexToId(grade2);

                            var signature =
                                id1.OpSign(id2);

                            if (signature.IsZero)
                                continue;

                            var index = (id1 ^ id2).BasisBladeIdToIndex();
                            var scalar = scalarProcessor.Times(scalar1, scalar2);

                            if (signature.IsPositive)
                                composer.AddTerm(grade, index, scalar);
                            else
                                composer.SubtractTerm(grade, index, scalar);
                        }
                    }
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateMultivectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv1, IMultivectorGradedStorage<T> mv2)
        {
            return mv1 switch
            {
                T s1 when mv2 is T s2 =>
                    scalarProcessor.Op(s1, s2).CreateKVectorStorageScalar(),

                VectorStorage<T> vt1 when mv2 is VectorStorage<T> vt2 =>
                    scalarProcessor.Op(vt1, vt2),

                KVectorStorage<T> kvt1 when mv2 is KVectorStorage<T> kvt2 =>
                    scalarProcessor.Op(kvt1, kvt2),

                _ =>
                    scalarProcessor.OpAsMultivectorGradedStorage(mv1, mv2)
            };
        }

        private static MultivectorStorage<T> OpAsMultivectorTermsStorage<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
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
                    var signature =
                        id1.OpSign(id2);

                    if (signature.IsZero)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature.IsPositive)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateMultivectorStorageSparse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, MultivectorStorage<T> mv1, MultivectorStorage<T> mv2)
        {
            return scalarProcessor.OpAsMultivectorTermsStorage(mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return mv1 switch
            {
                T s1 when mv2 is T s2 =>
                    scalarProcessor.Op(s1, s2).CreateKVectorStorageScalar(),

                VectorStorage<T> vt1 when mv2 is VectorStorage<T> vt2 =>
                    scalarProcessor.Op(vt1, vt2),

                KVectorStorage<T> kvt1 when mv2 is KVectorStorage<T> kvt2 =>
                    scalarProcessor.Op(kvt1, kvt2),

                IMultivectorGradedStorage<T> gmv1 when mv2 is IMultivectorGradedStorage<T> gmv2 =>
                    scalarProcessor.OpAsMultivectorGradedStorage(gmv1, gmv2),

                _ =>
                    scalarProcessor.OpAsMultivectorTermsStorage(mv1, mv2)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, params VectorStorage<T>[] vectorStorageList)
        {
            return vectorStorageList
                .Aggregate(
                    scalarProcessor.CreateKVectorStorageBasisScalar(),
                    scalarProcessor.Op
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<VectorStorage<T>> vectorStorageList)
        {
            return vectorStorageList
                .Aggregate(
                    scalarProcessor.CreateKVectorStorageBasisScalar(),
                    scalarProcessor.Op
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, params KVectorStorage<T>[] kVectorStorageList)
        {
            return kVectorStorageList
                .Aggregate(
                    scalarProcessor.CreateKVectorStorageBasisScalar(),
                    scalarProcessor.Op
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<KVectorStorage<T>> kVectorStorageList)
        {
            return kVectorStorageList
                .Aggregate(
                    scalarProcessor.CreateKVectorStorageBasisScalar(),
                    scalarProcessor.Op
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, params IMultivectorStorage<T>[] mvStoragesList)
        {
            return mvStoragesList
                .Aggregate(
                    (IMultivectorStorage<T>)scalarProcessor.CreateKVectorStorageBasisScalar(),
                    scalarProcessor.Op
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<IMultivectorStorage<T>> mvStoragesList)
        {
            return mvStoragesList
                .Aggregate(
                    (IMultivectorStorage<T>)scalarProcessor.CreateKVectorStorageBasisScalar(),
                    scalarProcessor.Op
                );
        }
    }
}