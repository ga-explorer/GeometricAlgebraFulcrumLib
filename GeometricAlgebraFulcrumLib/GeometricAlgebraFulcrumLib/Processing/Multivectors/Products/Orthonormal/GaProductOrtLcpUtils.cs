using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal
{
    public static class GaProductOrtLcpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Lcp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaScalarStorage<T> mv1, IGaScalarStorage<T> mv2)
        {
            return scalarProcessor.CreateStorageScalar(
                scalarProcessor.Times(
                    mv1.GetScalar(scalarProcessor), 
                    mv2.GetScalar(scalarProcessor)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> VectorsLcp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IReadOnlyList<T> vector1, IReadOnlyList<T> vector2)
        {
            return scalarProcessor.CreateStorageScalar(
                scalarProcessor.VectorsESp(vector1, vector2)
            );
        }
        
        private static IGaScalarStorage<T> LcpAsScalar<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaVectorStorage<T> mv1, IGaVectorStorage<T> mv2)
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
                var sig = signature.SpSignature(id);

                if (sig == 0)
                    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar2);

                lcpScalar = sig > 0 
                    ? scalarProcessor.Add(lcpScalar, scalar) 
                    : scalarProcessor.Subtract(lcpScalar, scalar);
            }

            return scalarProcessor.CreateStorageScalar(lcpScalar);
        }

        public static IGaScalarStorage<T> Lcp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaVectorStorage<T> mv1, IGaVectorStorage<T> mv2)
        {
            return LcpAsScalar(scalarProcessor, signature, mv1, mv2);
        }

        private static IGaKVectorStorage<T> LcpAsKVector<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaKVectorStorage<T> mv1, IGaKVectorStorage<T> mv2)
        {
            if (mv2.Grade < mv1.Grade)
                return scalarProcessor.CreateStorageZeroScalar();

            if (mv2.Grade == mv1.Grade)
                return scalarProcessor.CreateStorageScalar(scalarProcessor.Sp(signature, mv1, mv2));

            var grade1 = mv1.Grade;
            var grade2 = mv2.Grade;
            var grade = grade2 - grade1;

            var composer = 
                scalarProcessor.CreateKVectorStorageComposer();

            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            var indexScalarPairs2 = 
                mv2.IndexScalarList;

            foreach (var (index1, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var id1 = signature.BasisBladeId(grade1, index1);

                foreach (var (index2, scalar2) in indexScalarPairs2.GetIndexScalarRecords())
                {
                    var id2 = signature.BasisBladeId(grade2, index2);

                    var sig = 
                        signature.LcpSignature(id1, id2);

                    if (sig == 0) 
                        continue;

                    var index = (id1 ^ id2).BasisBladeIdToIndex();
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (sig > 0)
                        composer.AddTerm(index, scalar);
                    else
                        composer.SubtractTerm(index, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateGaKVectorStorage(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Lcp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaKVectorStorage<T> mv1, IGaKVectorStorage<T> mv2)
        {
            if (mv2.Grade < mv1.Grade)
                return scalarProcessor.CreateStorageZeroScalar();

            if (mv1.Grade == mv2.Grade)
                return scalarProcessor.CreateStorageScalar(scalarProcessor.Sp(signature, mv1, mv2));

            return mv1 switch
            {
                IGaScalarStorage<T> s1 when mv2 is IGaScalarStorage<T> s2 => 
                    Lcp(scalarProcessor, signature, s1, s2),

                IGaVectorStorage<T> vt1 when mv2 is IGaVectorStorage<T> vt2 => 
                    Lcp(scalarProcessor, signature, vt1, vt2),

                _ => 
                    LcpAsKVector(scalarProcessor, signature, mv1, mv2)
            };
        }

        private static IGaMultivectorGradedStorage<T> LcpAsGaMultivectorGraded<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaMultivectorGradedStorage<T> mv1, IGaMultivectorGradedStorage<T> mv2)
        {
            var composer = 
                scalarProcessor.CreateStorageGradedMultivectorComposer();

            var gradeIndexScalarDictionary1 = mv1.GetGradeIndexScalarList();
            var gradeIndexScalarDictionary2 = mv2.GetGradeIndexScalarList();

            foreach (var (grade1, indexScalarPairs1) in gradeIndexScalarDictionary1.GetGradeStorageRecords())
            {
                foreach (var (grade2, indexScalarPairs2) in gradeIndexScalarDictionary2.GetGradeStorageRecords())
                {
                    if (grade2 < grade1) 
                        continue;

                    if (grade2 == grade1)
                    {
                        foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
                        {
                            var id = signature.BasisBladeId(grade1, index);

                            if (!indexScalarPairs2.TryGetScalar(index, out var scalar2))
                                continue;
                            
                            var sig = signature.SpSignature(id);

                            if (sig == 0) 
                                continue;

                            var scalar = scalarProcessor.Times(scalar1, scalar2);

                            if (sig > 0)
                                composer.AddTerm(0, scalar);
                            else
                                composer.SubtractTerm(0, scalar);
                        }

                        continue;
                    }

                    var grade = grade2 - grade1;

                    foreach (var (index1, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
                    {
                        var id1 = signature.BasisBladeId(grade1, index1);

                        foreach (var (index2, scalar2) in indexScalarPairs2.GetIndexScalarRecords())
                        {
                            var id2 = signature.BasisBladeId(grade2, index2);

                            var sig = 
                                signature.LcpSignature(id1, id2);

                            if (sig == 0) 
                                continue;

                            var index = (id1 ^ id2).BasisBladeIdToIndex();
                            var scalar = scalarProcessor.Times(scalar1, scalar2);

                            if (sig > 0)
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
        public static IGaMultivectorGradedStorage<T> Lcp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaMultivectorGradedStorage<T> mv1, IGaMultivectorGradedStorage<T> mv2)
        {
            return mv1 switch
            {
                IGaScalarStorage<T> s1 when mv2 is IGaScalarStorage<T> s2 => 
                    Lcp(scalarProcessor, signature, s1, s2),

                IGaVectorStorage<T> vt1 when mv2 is IGaVectorStorage<T> vt2 => 
                    Lcp(scalarProcessor, signature, vt1, vt2),

                IGaKVectorStorage<T> kv1 when mv2 is IGaKVectorStorage<T> kv2 => 
                    LcpAsKVector(scalarProcessor, signature, kv1, kv2),

                _ => 
                    LcpAsGaMultivectorGraded(scalarProcessor, signature, mv1, mv2)
            };
        }

        private static IGaMultivectorSparseStorage<T> LcpAsTermsMultivector<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
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
                    var sig = 
                        signature.LcpSignature(id1, id2);

                    if (sig == 0) 
                        continue;

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (sig > 0)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateGaMultivectorSparseStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorSparseStorage<T> Lcp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaMultivectorSparseStorage<T> mv1, IGaMultivectorSparseStorage<T> mv2)
        {
            return LcpAsTermsMultivector(scalarProcessor, signature, mv1, mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Lcp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return mv1 switch
            {
                IGaScalarStorage<T> s1 when mv2 is IGaScalarStorage<T> s2 => 
                    Lcp(scalarProcessor, signature, s1, s2),

                IGaVectorStorage<T> vt1 when mv2 is IGaVectorStorage<T> vt2 => 
                    Lcp(scalarProcessor, signature, vt1, vt2),

                IGaKVectorStorage<T> kv1 when mv2 is IGaKVectorStorage<T> kv2 => 
                    LcpAsKVector(scalarProcessor, signature, kv1, kv2),

                IGaMultivectorGradedStorage<T> gmv1 when mv2 is IGaMultivectorGradedStorage<T> gmv2 => 
                    LcpAsGaMultivectorGraded(scalarProcessor, signature, gmv1, gmv2),

                _ =>
                    LcpAsTermsMultivector(scalarProcessor, signature, mv1, mv2)
            };
        }
    }
}