﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.Signatures;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorStorageLcpOrtUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Lcp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, T mv1, T mv2)
        {
            return scalarProcessor.Times(mv1, mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T VectorsLcp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, IReadOnlyList<T> vector1, IReadOnlyList<T> vector2)
        {
            return scalarProcessor.VectorsESp(vector1, vector2);
        }
        
        private static T LcpAsScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, VectorStorage<T> mv1, VectorStorage<T> mv2)
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
                var sig = signature.SpSignature(id);

                if (sig == 0)
                    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar2);

                lcpScalar = sig > 0 
                    ? scalarProcessor.Add(lcpScalar, scalar) 
                    : scalarProcessor.Subtract(lcpScalar, scalar);
            }

            return lcpScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Lcp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, VectorStorage<T> mv1, VectorStorage<T> mv2)
        {
            return LcpAsScalar(scalarProcessor, signature, mv1, mv2);
        }

        private static KVectorStorage<T> LcpAsKVector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
        {
            if (mv2.Grade < mv1.Grade)
                return KVectorStorage<T>.ZeroScalar;

            if (mv2.Grade == mv1.Grade)
                return scalarProcessor.CreateKVectorScalarStorage(scalarProcessor.Sp(signature, mv1, mv2));

            var grade1 = mv1.Grade;
            var grade2 = mv2.Grade;
            var grade = grade2 - grade1;

            var composer = 
                scalarProcessor.CreateVectorStorageComposer();

            var indexScalarPairs1 = 
                mv1.GetLinVectorIndexScalarStorage();

            var indexScalarPairs2 = 
                mv2.GetLinVectorIndexScalarStorage();

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

            return composer.CreateKVectorStorage(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Lcp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
        {
            if (mv2.Grade < mv1.Grade)
                return KVectorStorage<T>.ZeroScalar;

            if (mv1.Grade == mv2.Grade)
                return scalarProcessor.CreateKVectorScalarStorage(scalarProcessor.Sp(signature, mv1, mv2));

            return mv1 switch
            {
                VectorStorage<T> vt1 when mv2 is VectorStorage<T> vt2 => 
                    Lcp(scalarProcessor, signature, vt1, vt2).CreateKVectorScalarStorage(),

                _ => 
                    LcpAsKVector(scalarProcessor, signature, mv1, mv2)
            };
        }

        private static IMultivectorGradedStorage<T> LcpAsMultivectorGraded<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, IMultivectorGradedStorage<T> mv1, IMultivectorGradedStorage<T> mv2)
        {
            var composer = 
                scalarProcessor.CreateMultivectorGradedStorageComposer();

            var gradeIndexScalarDictionary1 = mv1.GetLinVectorGradedStorage();
            var gradeIndexScalarDictionary2 = mv2.GetLinVectorGradedStorage();

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

            return composer.CreateMultivectorGradedStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Lcp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, IMultivectorGradedStorage<T> mv1, IMultivectorGradedStorage<T> mv2)
        {
            return mv1 switch
            {
                VectorStorage<T> vt1 when mv2 is VectorStorage<T> vt2 => 
                    Lcp(scalarProcessor, signature, vt1, vt2).CreateKVectorScalarStorage(),

                KVectorStorage<T> kv1 when mv2 is KVectorStorage<T> kv2 => 
                    LcpAsKVector(scalarProcessor, signature, kv1, kv2),

                _ => 
                    LcpAsMultivectorGraded(scalarProcessor, signature, mv1, mv2)
            };
        }

        private static MultivectorStorage<T> LcpAsTermsMultivector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
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

            return composer.CreateMultivectorSparseStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> Lcp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, MultivectorStorage<T> mv1, MultivectorStorage<T> mv2)
        {
            return LcpAsTermsMultivector(scalarProcessor, signature, mv1, mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Lcp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return mv1 switch
            {
                VectorStorage<T> vt1 when mv2 is VectorStorage<T> vt2 => 
                    Lcp(scalarProcessor, signature, vt1, vt2).CreateKVectorScalarStorage(),

                KVectorStorage<T> kv1 when mv2 is KVectorStorage<T> kv2 => 
                    LcpAsKVector(scalarProcessor, signature, kv1, kv2),

                IMultivectorGradedStorage<T> gmv1 when mv2 is IMultivectorGradedStorage<T> gmv2 => 
                    LcpAsMultivectorGraded(scalarProcessor, signature, gmv1, gmv2),

                _ =>
                    LcpAsTermsMultivector(scalarProcessor, signature, mv1, mv2)
            };
        }
    }
}