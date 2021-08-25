using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean
{
    public static class GaProductEucRcpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> ERcp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> mv1, IGaStorageScalar<T> mv2)
        {
            return scalarProcessor.CreateStorageScalar(
                scalarProcessor.Times(
                    mv1.GetScalar(scalarProcessor), 
                    mv2.GetScalar(scalarProcessor)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> VectorsERcp<T>(this IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector1, IReadOnlyList<T> vector2)
        {
            return scalarProcessor.CreateStorageScalar(
                scalarProcessor.VectorsESp(vector1, vector2)
            );
        }
        
        public static IGaStorageScalar<T> ERcp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> mv1, IGaStorageVector<T> mv2)
        {
            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            var indexScalarPairs2 = 
                mv2.IndexScalarList;

            var lcpScalar = scalarProcessor.GetZeroScalar();

            foreach (var (index, scalar1) in indexScalarPairs1.GetKeyValueRecords())
            {
                if (!indexScalarPairs2.TryGetValue(index, out var scalar2))
                    continue;

                var id = index.BasisVectorIndexToId();
                var scalar = scalarProcessor.Times(scalar1, scalar2);

                lcpScalar = GaBasisBladeProductUtils.IsPositiveEGp(id) 
                    ? scalarProcessor.Add(lcpScalar, scalar) 
                    : scalarProcessor.Subtract(lcpScalar, scalar);
            }

            return scalarProcessor.CreateStorageScalar(lcpScalar);
        }

        private static IGaStorageKVector<T> ERcpAsKVector<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv1, IGaStorageKVector<T> mv2)
        {
            if (mv2.Grade > mv1.Grade)
                return scalarProcessor.CreateStorageZeroScalar();

            if (mv2.Grade == mv1.Grade)
                return scalarProcessor.CreateStorageScalar(scalarProcessor.ESp(mv1, mv2));

            var grade1 = mv1.Grade;
            var grade2 = mv2.Grade;
            var grade = grade1 - grade2;

            var composer = 
                scalarProcessor.CreateStorageKVectorComposer();

            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            var indexScalarPairs2 = 
                mv2.IndexScalarList;

            foreach (var (index1, scalar1) in indexScalarPairs1.GetKeyValueRecords())
            {
                var id1 = index1.BasisBladeIndexToId(grade1);

                foreach (var (index2, scalar2) in indexScalarPairs2.GetKeyValueRecords())
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

            return composer.CreateStorageKVector(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> ERcp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv1, IGaStorageKVector<T> mv2)
        {
            if (mv2.Grade > mv1.Grade)
                return scalarProcessor.CreateStorageZeroScalar();

            if (mv1.Grade == mv2.Grade)
                return scalarProcessor.CreateStorageScalar(scalarProcessor.ESp(mv1, mv2));

            return mv1 switch
            {
                IGaStorageScalar<T> s1 when mv2 is IGaStorageScalar<T> s2 => 
                    ERcp(scalarProcessor, s1, s2),

                IGaStorageVector<T> vt1 when mv2 is IGaStorageVector<T> vt2 => 
                    ERcp(scalarProcessor, vt1, vt2),

                _ => 
                    ERcpAsKVector(scalarProcessor, mv1, mv2)
            };
        }

        private static IGaStorageMultivectorGraded<T> ERcpAsGaMultivectorGraded<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv1, IGaStorageMultivectorGraded<T> mv2)
        {
            var composer = 
                scalarProcessor.CreateStorageGradedMultivectorComposer();

            var gradeIndexScalarDictionary1 = mv1.GetGradeIndexScalarList();
            var gradeIndexScalarDictionary2 = mv2.GetGradeIndexScalarList();

            foreach (var (grade1, indexScalarPairs1) in gradeIndexScalarDictionary1.GetGradeListRecords())
            {
                foreach (var (grade2, indexScalarPairs2) in gradeIndexScalarDictionary2.GetGradeListRecords())
                {
                    if (grade2 > grade1)
                        continue;

                    if (grade2 == grade1)
                    {
                        foreach (var (index, scalar1) in indexScalarPairs1.GetKeyValueRecords())
                        {
                            var id = index.BasisBladeIndexToId(grade1);

                            if (!indexScalarPairs2.TryGetValue(index, out var scalar2))
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

                    foreach (var (index1, scalar1) in indexScalarPairs1.GetKeyValueRecords())
                    {
                        var id1 = index1.BasisBladeIndexToId(grade1);

                        foreach (var (index2, scalar2) in indexScalarPairs2.GetKeyValueRecords())
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

            return composer.CreateStorageGradedMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> ERcp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv1, IGaStorageMultivectorGraded<T> mv2)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 when mv2 is IGaStorageScalar<T> s2 => 
                    ERcp(scalarProcessor, s1, s2),

                IGaStorageVector<T> vt1 when mv2 is IGaStorageVector<T> vt2 => 
                    ERcp(scalarProcessor, vt1, vt2),

                IGaStorageKVector<T> kv1 when mv2 is IGaStorageKVector<T> kv2 => 
                    ERcpAsKVector(scalarProcessor, kv1, kv2),

                _ => 
                    ERcpAsGaMultivectorGraded(scalarProcessor, mv1, mv2)
            };
        }

        private static IGaStorageMultivectorSparse<T> ERcpAsTermsMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
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

            return composer.CreateStorageSparseMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorSparse<T> ERcp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorSparse<T> mv1, IGaStorageMultivectorSparse<T> mv2)
        {
            return ERcpAsTermsMultivector(scalarProcessor, mv1, mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> ERcp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 when mv2 is IGaStorageScalar<T> s2 => 
                    ERcp(scalarProcessor, s1, s2),

                IGaStorageVector<T> vt1 when mv2 is IGaStorageVector<T> vt2 => 
                    ERcp(scalarProcessor, vt1, vt2),

                IGaStorageMultivectorGraded<T> gmv1 when mv2 is IGaStorageMultivectorGraded<T> gmv2 => 
                    ERcpAsGaMultivectorGraded(scalarProcessor, gmv1, gmv2),

                _ =>
                    ERcpAsTermsMultivector(scalarProcessor, mv1, mv2)
            };
        }
    }
}