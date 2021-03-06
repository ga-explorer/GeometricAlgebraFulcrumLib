using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    internal static class MultivectorStorageFdpOrtUtils
    {
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static BasisBilinearProductResult Fdp(this BasisBladeSet basisSet, ulong id1, ulong id2)
        //{
        //    return new BasisBilinearProductResult(
        //        basisSet.FdpSign(id1, id2), 
        //        id1 ^ id2
        //    );
        //}

        //public static IMultivectorStorage<double> Fdp(this BasisBladeSet basisSet, IMultivectorStorage<double> mv1, IMultivectorStorage<double> mv2)
        //{
        //    var composer = 
        //        new MultivectorFloat64StorageComposer(basisSet);

        //    var idScalarPairs1 = 
        //        mv1.GetIdScalarRecords();

        //    var idScalarPairs2 = 
        //        mv2.GetLinVectorIdScalarStorage();

        //    foreach (var (id1, scalar1) in idScalarPairs1)
        //    foreach (var (id2, scalar2) in idScalarPairs2.GetIndexScalarRecords())
        //        composer.AddFdpTerm(id1, id2, scalar1, scalar2);

        //    composer.RemoveZeroTerms();

        //    return composer.GetCompactStorage();
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static IMultivectorStorage<T> Fdp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        //{
        //    return basisSet.IsEuclidean
        //        ? scalarProcessor.EFdp(mv1, mv2)
        //        : scalarProcessor.BilinearProduct(mv1, mv2, basisSet.FdpSign);
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Fdp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, T mv1, T mv2)
        {
            return scalarProcessor.Times(mv1, mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T VectorsFdp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, IReadOnlyList<T> vector1, IReadOnlyList<T> vector2)
        {
            return scalarProcessor.VectorsESp(vector1, vector2);
        }
        
        private static T FdpAsScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, VectorStorage<T> mv1, VectorStorage<T> mv2)
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
                var sig = basisSet.SpSquaredSign(id);

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
        public static T Fdp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, VectorStorage<T> mv1, VectorStorage<T> mv2)
        {
            return FdpAsScalar(scalarProcessor, basisSet, mv1, mv2);
        }

        private static KVectorStorage<T> FdpAsKVector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
        {
            if (mv2.Grade < mv1.Grade)
                return KVectorStorage<T>.ZeroScalar;

            if (mv2.Grade == mv1.Grade)
                return scalarProcessor.CreateKVectorStorageScalar(scalarProcessor.Sp(basisSet, mv1, mv2));

            var grade1 = mv1.Grade;
            var grade2 = mv2.Grade;
            var grade = grade2 > grade1 ? grade2 - grade1 : grade1 - grade2;

            var composer = 
                scalarProcessor.CreateVectorStorageComposer();

            var indexScalarPairs1 = 
                mv1.GetLinVectorIndexScalarStorage();

            var indexScalarPairs2 = 
                mv2.GetLinVectorIndexScalarStorage();

            foreach (var (index1, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var id1 = BasisBladeUtils.BasisBladeGradeIndexToId(grade1, index1);

                foreach (var (index2, scalar2) in indexScalarPairs2.GetIndexScalarRecords())
                {
                    var id2 = BasisBladeUtils.BasisBladeGradeIndexToId(grade2, index2);

                    var sig = 
                        basisSet.FdpSign(id1, id2);

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
        public static KVectorStorage<T> Fdp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
        {
            if (mv2.Grade < mv1.Grade)
                return KVectorStorage<T>.ZeroScalar;

            if (mv1.Grade == mv2.Grade)
                return scalarProcessor.CreateKVectorStorageScalar(scalarProcessor.Sp(basisSet, mv1, mv2));

            return mv1 switch
            {
                VectorStorage<T> vt1 when mv2 is VectorStorage<T> vt2 => 
                    Fdp(scalarProcessor, basisSet, vt1, vt2).CreateKVectorStorageScalar(),

                _ => 
                    FdpAsKVector(scalarProcessor, basisSet, mv1, mv2)
            };
        }

        private static IMultivectorGradedStorage<T> FdpAsMultivectorGraded<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, IMultivectorGradedStorage<T> mv1, IMultivectorGradedStorage<T> mv2)
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
                            var id = BasisBladeUtils.BasisBladeGradeIndexToId(grade1, index);

                            if (!indexScalarPairs2.TryGetScalar(index, out var scalar2))
                                continue;
                            
                            var sig = basisSet.SpSquaredSign(id);

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
                        var id1 = BasisBladeUtils.BasisBladeGradeIndexToId(grade1, index1);

                        foreach (var (index2, scalar2) in indexScalarPairs2.GetIndexScalarRecords())
                        {
                            var id2 = BasisBladeUtils.BasisBladeGradeIndexToId(grade2, index2);

                            var sig = 
                                basisSet.FdpSign(id1, id2);

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
        public static IMultivectorGradedStorage<T> Fdp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, IMultivectorGradedStorage<T> mv1, IMultivectorGradedStorage<T> mv2)
        {
            return mv1 switch
            {
                VectorStorage<T> vt1 when mv2 is VectorStorage<T> vt2 => 
                    Fdp(scalarProcessor, basisSet, vt1, vt2).CreateKVectorStorageScalar(),

                KVectorStorage<T> kv1 when mv2 is KVectorStorage<T> kv2 => 
                    FdpAsKVector(scalarProcessor, basisSet, kv1, kv2),

                _ => 
                    FdpAsMultivectorGraded(scalarProcessor, basisSet, mv1, mv2)
            };
        }

        private static MultivectorStorage<T> FdpAsTermsMultivector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
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
                        basisSet.FdpSign(id1, id2);

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

            return composer.CreateMultivectorStorageSparse();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> Fdp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, MultivectorStorage<T> mv1, MultivectorStorage<T> mv2)
        {
            return FdpAsTermsMultivector(scalarProcessor, basisSet, mv1, mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Fdp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return mv1 switch
            {
                VectorStorage<T> vt1 when mv2 is VectorStorage<T> vt2 => 
                    Fdp(scalarProcessor, basisSet, vt1, vt2).CreateKVectorStorageScalar(),

                KVectorStorage<T> kv1 when mv2 is KVectorStorage<T> kv2 => 
                    FdpAsKVector(scalarProcessor, basisSet, kv1, kv2),

                IMultivectorGradedStorage<T> gmv1 when mv2 is IMultivectorGradedStorage<T> gmv2 => 
                    FdpAsMultivectorGraded(scalarProcessor, basisSet, gmv1, gmv2),

                _ =>
                    FdpAsTermsMultivector(scalarProcessor, basisSet, mv1, mv2)
            };
        }
    }
}