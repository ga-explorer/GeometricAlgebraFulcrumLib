using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorStorageLcpEucUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T VectorsELcp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyList<T> vector1, IReadOnlyList<T> vector2)
        {
            return scalarProcessor.VectorsESp(vector1, vector2);
        }
        
        public static T ELcp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv1, VectorStorage<T> mv2)
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

                lcpScalar = BasisBladeProductUtils.IsPositiveEGp(id) 
                    ? scalarProcessor.Add(lcpScalar, scalar) 
                    : scalarProcessor.Subtract(lcpScalar, scalar);
            }

            return lcpScalar;
        }

        private static KVectorStorage<T> ELcpAsKVector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
        {
            if (mv2.Grade < mv1.Grade)
                return KVectorStorage<T>.ZeroScalar;

            if (mv2.Grade == mv1.Grade)
                return scalarProcessor.CreateKVectorScalarStorage(scalarProcessor.ESp(mv1, mv2));

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
                var id1 = index1.BasisBladeIndexToId(grade1);

                foreach (var (index2, scalar2) in indexScalarPairs2.GetIndexScalarRecords())
                {
                    var id2 = index2.BasisBladeIndexToId(grade2);

                    var signature = 
                        BasisBladeProductUtils.ELcpSignature(id1, id2);

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
        public static KVectorStorage<T> ELcp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
        {
            if (mv2.Grade < mv1.Grade)
                return KVectorStorage<T>.ZeroScalar;

            if (mv1.Grade == mv2.Grade)
                return scalarProcessor.CreateKVectorScalarStorage(scalarProcessor.ESp(mv1, mv2));

            return mv1 switch
            {
                VectorStorage<T> vt1 when mv2 is VectorStorage<T> vt2 => 
                    ELcp(scalarProcessor, vt1, vt2).CreateKVectorScalarStorage(),

                _ => 
                    ELcpAsKVector(scalarProcessor, mv1, mv2)
            };
        }

        private static IMultivectorGradedStorage<T> ELcpAsMultivectorGraded<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv1, IMultivectorGradedStorage<T> mv2)
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
                            var id = index.BasisBladeIndexToId(grade1);

                            if (!indexScalarPairs2.TryGetScalar(index, out var scalar2))
                                continue;
                            
                            var signature = 
                                BasisBladeProductUtils.EGpSignature(id);

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

                    var grade = grade2 - grade1;

                    foreach (var (index1, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
                    {
                        var id1 = index1.BasisBladeIndexToId(grade1);

                        foreach (var (index2, scalar2) in indexScalarPairs2.GetIndexScalarRecords())
                        {
                            var id2 = index2.BasisBladeIndexToId(grade2);

                            var signature = 
                                BasisBladeProductUtils.ELcpSignature(id1, id2);

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
        public static IMultivectorGradedStorage<T> ELcp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv1, IMultivectorGradedStorage<T> mv2)
        {
            return mv1 switch
            {
                VectorStorage<T> vt1 when mv2 is VectorStorage<T> vt2 => 
                    ELcp(scalarProcessor, vt1, vt2).CreateKVectorScalarStorage(),

                _ => 
                    ELcpAsMultivectorGraded(scalarProcessor, mv1, mv2)
            };
        }

        private static MultivectorStorage<T> ELcpAsTermsMultivector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
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
                        BasisBladeProductUtils.ELcpSignature(id1, id2);

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

            return composer.CreateMultivectorSparseStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> ELcp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv1, MultivectorStorage<T> mv2)
        {
            return ELcpAsTermsMultivector(scalarProcessor, mv1, mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> ELcp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return mv1 switch
            {
                VectorStorage<T> vt1 when mv2 is VectorStorage<T> vt2 => 
                    ELcp(scalarProcessor, vt1, vt2).CreateKVectorScalarStorage(),

                IMultivectorGradedStorage<T> gmv1 when mv2 is IMultivectorGradedStorage<T> gmv2 => 
                    ELcpAsMultivectorGraded(scalarProcessor, gmv1, gmv2),

                _ =>
                    ELcpAsTermsMultivector(scalarProcessor, mv1, mv2)
            };
        }
    }
}