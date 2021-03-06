using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    internal static class MultivectorStorageRcpEucUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ERcp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T mv1, T mv2)
        {
            return scalarProcessor.Times(mv1, mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T VectorsERcp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyList<T> vector1, IReadOnlyList<T> vector2)
        {
            return scalarProcessor.VectorsESp(vector1, vector2);
        }
        
        public static T ERcp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv1, VectorStorage<T> mv2)
        {
            var indexScalarPairs1 = 
                mv1.GetLinVectorIndexScalarStorage();

            var indexScalarPairs2 = 
                mv2.GetLinVectorIndexScalarStorage();

            var lcpScalar = scalarProcessor.ScalarZero;

            foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                if (!indexScalarPairs2.TryGetScalar(index, out var scalar2))
                    continue;

                var id = index.BasisVectorIndexToId();
                var scalar = scalarProcessor.Times(scalar1, scalar2);

                lcpScalar = BasisBladeProductUtils.EGpSquaredIsPositive(id) 
                    ? scalarProcessor.Add(lcpScalar, scalar) 
                    : scalarProcessor.Subtract(lcpScalar, scalar);
            }

            return lcpScalar;
        }

        private static KVectorStorage<T> ERcpAsKVector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
        {
            if (mv2.Grade > mv1.Grade)
                return KVectorStorage<T>.ZeroScalar;

            if (mv2.Grade == mv1.Grade)
                return scalarProcessor.CreateKVectorStorageScalar(scalarProcessor.ESp(mv1, mv2));

            var grade1 = mv1.Grade;
            var grade2 = mv2.Grade;
            var grade = grade1 - grade2;

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
                        BasisBladeProductUtils.ERcpSign(id1, id2);

                    if (signature == 0) 
                        continue;

                    var index = (id1 ^ id2).BasisBladeIdToIndex();
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(index, scalar);
                    else
                        composer.SubtractTerm(index, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateKVectorStorage(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> ERcp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
        {
            if (mv2.Grade > mv1.Grade)
                return KVectorStorage<T>.ZeroScalar;

            if (mv1.Grade == mv2.Grade)
                return scalarProcessor.CreateKVectorStorageScalar(scalarProcessor.ESp(mv1, mv2));

            return mv1 switch
            {
                T s1 when mv2 is T s2 => 
                    ERcp(scalarProcessor, s1, s2).CreateKVectorStorageScalar(),

                VectorStorage<T> vt1 when mv2 is VectorStorage<T> vt2 => 
                    ERcp(scalarProcessor, vt1, vt2).CreateKVectorStorageScalar(),

                _ => 
                    ERcpAsKVector(scalarProcessor, mv1, mv2)
            };
        }

        private static IMultivectorGradedStorage<T> ERcpAsMultivectorGraded<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv1, IMultivectorGradedStorage<T> mv2)
        {
            var composer = 
                scalarProcessor.CreateMultivectorGradedStorageComposer();

            var gradeIndexScalarDictionary1 = mv1.GetLinVectorGradedStorage();
            var gradeIndexScalarDictionary2 = mv2.GetLinVectorGradedStorage();

            foreach (var (grade1, indexScalarPairs1) in gradeIndexScalarDictionary1.GetGradeStorageRecords())
            {
                foreach (var (grade2, indexScalarPairs2) in gradeIndexScalarDictionary2.GetGradeStorageRecords())
                {
                    if (grade2 > grade1)
                        continue;

                    if (grade2 == grade1)
                    {
                        foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
                        {
                            var id = index.BasisBladeIndexToId(grade1);

                            if (!indexScalarPairs2.TryGetScalar(index, out var scalar2))
                                continue;
                            
                            var signature = 
                                BasisBladeProductUtils.EGpSquaredSign(id);

                            if (signature == 0) 
                                continue;

                            var scalar = scalarProcessor.Times(scalar1, scalar2);

                            if (signature > 0)
                                composer.AddTerm(0, scalar);
                            else
                                composer.SubtractTerm(0, scalar);
                        }

                        continue;
                    }

                    var grade = grade1 - grade2;

                    foreach (var (index1, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
                    {
                        var id1 = index1.BasisBladeIndexToId(grade1);

                        foreach (var (index2, scalar2) in indexScalarPairs2.GetIndexScalarRecords())
                        {
                            var id2 = index2.BasisBladeIndexToId(grade2);

                            var signature = 
                                BasisBladeProductUtils.ERcpSign(id1, id2);

                            if (signature == 0) 
                                continue;

                            var index = (id1 ^ id2).BasisBladeIdToIndex();
                            var scalar = scalarProcessor.Times(scalar1, scalar2);

                            if (signature > 0)
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
        public static IMultivectorGradedStorage<T> ERcp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv1, IMultivectorGradedStorage<T> mv2)
        {
            return mv1 switch
            {
                T s1 when mv2 is T s2 => 
                    ERcp(scalarProcessor, s1, s2).CreateKVectorStorageScalar(),

                VectorStorage<T> vt1 when mv2 is VectorStorage<T> vt2 => 
                    ERcp(scalarProcessor, vt1, vt2).CreateKVectorStorageScalar(),

                KVectorStorage<T> kv1 when mv2 is KVectorStorage<T> kv2 => 
                    ERcpAsKVector(scalarProcessor, kv1, kv2),

                _ => 
                    ERcpAsMultivectorGraded(scalarProcessor, mv1, mv2)
            };
        }

        private static MultivectorStorage<T> ERcpAsTermsMultivector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
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
                        BasisBladeProductUtils.ERcpSign(id1, id2);

                    if (signature == 0) 
                        continue;

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateMultivectorStorageSparse();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> ERcp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv1, MultivectorStorage<T> mv2)
        {
            return ERcpAsTermsMultivector(scalarProcessor, mv1, mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> ERcp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return mv1 switch
            {
                T s1 when mv2 is T s2 => 
                    ERcp(scalarProcessor, s1, s2).CreateKVectorStorageScalar(),

                VectorStorage<T> vt1 when mv2 is VectorStorage<T> vt2 => 
                    ERcp(scalarProcessor, vt1, vt2).CreateKVectorStorageScalar(),

                IMultivectorGradedStorage<T> gmv1 when mv2 is IMultivectorGradedStorage<T> gmv2 => 
                    ERcpAsMultivectorGraded(scalarProcessor, gmv1, gmv2),

                _ =>
                    ERcpAsTermsMultivector(scalarProcessor, mv1, mv2)
            };
        }
    }
}