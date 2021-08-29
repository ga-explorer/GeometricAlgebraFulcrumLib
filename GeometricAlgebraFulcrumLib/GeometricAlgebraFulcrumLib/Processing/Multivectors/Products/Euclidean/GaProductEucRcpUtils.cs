using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean
{
    public static class GaProductEucRcpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> ERcp<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> mv1, IGaScalarStorage<T> mv2)
        {
            return scalarProcessor.CreateStorageScalar(
                scalarProcessor.Times(
                    mv1.GetScalar(scalarProcessor), 
                    mv2.GetScalar(scalarProcessor)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> VectorsERcp<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector1, IReadOnlyList<T> vector2)
        {
            return scalarProcessor.CreateStorageScalar(
                scalarProcessor.VectorsESp(vector1, vector2)
            );
        }
        
        public static IGaScalarStorage<T> ERcp<T>(this IScalarProcessor<T> scalarProcessor, IGaVectorStorage<T> mv1, IGaVectorStorage<T> mv2)
        {
            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            var indexScalarPairs2 = 
                mv2.IndexScalarList;

            var lcpScalar = scalarProcessor.ScalarZero;

            foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                if (!indexScalarPairs2.TryGetScalar(index, out var scalar2))
                    continue;

                var id = index.BasisVectorIndexToId();
                var scalar = scalarProcessor.Times(scalar1, scalar2);

                lcpScalar = GaBasisBladeProductUtils.IsPositiveEGp(id) 
                    ? scalarProcessor.Add(lcpScalar, scalar) 
                    : scalarProcessor.Subtract(lcpScalar, scalar);
            }

            return scalarProcessor.CreateStorageScalar(lcpScalar);
        }

        private static IGaKVectorStorage<T> ERcpAsKVector<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv1, IGaKVectorStorage<T> mv2)
        {
            if (mv2.Grade > mv1.Grade)
                return scalarProcessor.CreateStorageZeroScalar();

            if (mv2.Grade == mv1.Grade)
                return scalarProcessor.CreateStorageScalar(scalarProcessor.ESp(mv1, mv2));

            var grade1 = mv1.Grade;
            var grade2 = mv2.Grade;
            var grade = grade1 - grade2;

            var composer = 
                scalarProcessor.CreateKVectorStorageComposer();

            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            var indexScalarPairs2 = 
                mv2.IndexScalarList;

            foreach (var (index1, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var id1 = index1.BasisBladeIndexToId(grade1);

                foreach (var (index2, scalar2) in indexScalarPairs2.GetIndexScalarRecords())
                {
                    var id2 = index2.BasisBladeIndexToId(grade2);

                    var signature = 
                        GaBasisBladeProductUtils.ERcpSignature(id1, id2);

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

            return composer.CreateGaKVectorStorage(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> ERcp<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv1, IGaKVectorStorage<T> mv2)
        {
            if (mv2.Grade > mv1.Grade)
                return scalarProcessor.CreateStorageZeroScalar();

            if (mv1.Grade == mv2.Grade)
                return scalarProcessor.CreateStorageScalar(scalarProcessor.ESp(mv1, mv2));

            return mv1 switch
            {
                IGaScalarStorage<T> s1 when mv2 is IGaScalarStorage<T> s2 => 
                    ERcp(scalarProcessor, s1, s2),

                IGaVectorStorage<T> vt1 when mv2 is IGaVectorStorage<T> vt2 => 
                    ERcp(scalarProcessor, vt1, vt2),

                _ => 
                    ERcpAsKVector(scalarProcessor, mv1, mv2)
            };
        }

        private static IGaMultivectorGradedStorage<T> ERcpAsGaMultivectorGraded<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorGradedStorage<T> mv1, IGaMultivectorGradedStorage<T> mv2)
        {
            var composer = 
                scalarProcessor.CreateStorageGradedMultivectorComposer();

            var gradeIndexScalarDictionary1 = mv1.GetGradeIndexScalarList();
            var gradeIndexScalarDictionary2 = mv2.GetGradeIndexScalarList();

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
                                GaBasisBladeProductUtils.EGpSignature(id);

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
                                GaBasisBladeProductUtils.ERcpSignature(id1, id2);

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

            return composer.CreateGaMultivectorGradedStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> ERcp<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorGradedStorage<T> mv1, IGaMultivectorGradedStorage<T> mv2)
        {
            return mv1 switch
            {
                IGaScalarStorage<T> s1 when mv2 is IGaScalarStorage<T> s2 => 
                    ERcp(scalarProcessor, s1, s2),

                IGaVectorStorage<T> vt1 when mv2 is IGaVectorStorage<T> vt2 => 
                    ERcp(scalarProcessor, vt1, vt2),

                IGaKVectorStorage<T> kv1 when mv2 is IGaKVectorStorage<T> kv2 => 
                    ERcpAsKVector(scalarProcessor, kv1, kv2),

                _ => 
                    ERcpAsGaMultivectorGraded(scalarProcessor, mv1, mv2)
            };
        }

        private static IGaMultivectorSparseStorage<T> ERcpAsTermsMultivector<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
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
                        GaBasisBladeProductUtils.ERcpSignature(id1, id2);

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

            return composer.CreateGaMultivectorSparseStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorSparseStorage<T> ERcp<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorSparseStorage<T> mv1, IGaMultivectorSparseStorage<T> mv2)
        {
            return ERcpAsTermsMultivector(scalarProcessor, mv1, mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> ERcp<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return mv1 switch
            {
                IGaScalarStorage<T> s1 when mv2 is IGaScalarStorage<T> s2 => 
                    ERcp(scalarProcessor, s1, s2),

                IGaVectorStorage<T> vt1 when mv2 is IGaVectorStorage<T> vt2 => 
                    ERcp(scalarProcessor, vt1, vt2),

                IGaMultivectorGradedStorage<T> gmv1 when mv2 is IGaMultivectorGradedStorage<T> gmv2 => 
                    ERcpAsGaMultivectorGraded(scalarProcessor, gmv1, gmv2),

                _ =>
                    ERcpAsTermsMultivector(scalarProcessor, mv1, mv2)
            };
        }
    }
}