using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    internal static class MultivectorStorageSpEucUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T mv1)
        {
            return scalarProcessor.Square(mv1);
        }
        
        public static T VectorsESp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyList<T> vector1)
        {
            var lcpScalar = scalarProcessor.ScalarZero;

            var count = vector1.Count;

            for (var index = 0; index < count; index++)
            {
                var id = index.BasisVectorIndexToId();
                var scalar1 = vector1[index];

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                lcpScalar = BasisBladeProductUtils.EGpSquaredIsPositive(id) 
                    ? scalarProcessor.Add(lcpScalar, scalar) 
                    : scalarProcessor.Subtract(lcpScalar, scalar);
            }

            return lcpScalar;
        }

        public static T ESp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv1)
        {
            var indexScalarPairs1 = 
                mv1.GetLinVectorIndexScalarStorage();

            var spScalar = scalarProcessor.ScalarZero;

            foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var id = index.BasisVectorIndexToId();
                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = BasisBladeProductUtils.EGpSquaredIsPositive(id) 
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        private static T ESpAsScalar<T>(IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv1)
        {
            var grade = mv1.Grade;
            var spScalar = scalarProcessor.ScalarZero;
            
            var indexScalarPairs1 = 
                mv1.GetLinVectorIndexScalarStorage();

            foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var id = 
                    index.BasisBladeIndexToId(grade);

                var signature = 
                    BasisBladeProductUtils.EGpSquaredSign(id);

                //if (signature == 0) 
                //    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = signature > 0
                    ? scalarProcessor.Add(spScalar, scalar)
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv1)
        {
            return mv1 switch
            {
                T s1 => ESp(scalarProcessor, s1),
                VectorStorage<T> vt1 => ESp(scalarProcessor, vt1),
                _ => ESpAsScalar(scalarProcessor, mv1)
            };
        }

        private static T ESpAsScalar<T>(IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1)
        {
            var spScalar = scalarProcessor.ScalarZero;

            var idScalarDictionary1 = 
                mv1.GetLinVectorIdScalarStorage();

            foreach (var (id, scalar1) in idScalarDictionary1.GetIndexScalarRecords())
            {
                var signature = 
                    BasisBladeProductUtils.EGpSquaredSign(id);

                //if (signature == 0) 
                //    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = signature > 0
                    ? scalarProcessor.Add(spScalar, scalar)
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESpSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1)
        {
            return mv1 switch
            {
                T s1 => ESp(scalarProcessor, s1),
                VectorStorage<T> vt1 => ESp(scalarProcessor, vt1),
                KVectorStorage<T> kvt1 => ESp(scalarProcessor, kvt1),
                _ => ESpAsScalar(scalarProcessor, mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBilinearProductResult ESp(this BasisBladeSet basisSet, ulong id1, ulong id2)
        {
            return new BasisBilinearProductResult(
                BasisBladeProductUtils.ESpSign(id1, id2), 
                id1 ^ id2
            );
        }

        public static double ESp(this BasisBladeSet basisSet, IMultivectorStorage<double> mv1, IMultivectorStorage<double> mv2)
        {
            if (!basisSet.IsEuclidean)
                throw new InvalidOperationException();

            var spScalar = 0d;

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetLinVectorIdScalarStorage();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                if (!idScalarPairs2.TryGetScalar(id1, out var scalar2))
                    continue;

                var scalar = scalar1 * scalar2;

                spScalar = BasisBladeProductUtils.EGpSquaredIsPositive(id1)
                    ? spScalar + scalar
                    : spScalar - scalar;
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T mv1, T mv2)
        {
            return scalarProcessor.Times(mv1, mv2);
        }
        
        public static T VectorsESp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyList<T> vector1, IReadOnlyList<T> vector2)
        {
            var lcpScalar = scalarProcessor.ScalarZero;

            var count = Math.Min(vector1.Count, vector2.Count);

            for (var index = 0; index < count; index++)
            {
                var id = index.BasisVectorIndexToId();
                var scalar1 = vector1[index];
                var scalar2 = vector2[index];

                var scalar = scalarProcessor.Times(scalar1, scalar2);

                lcpScalar = BasisBladeProductUtils.EGpSquaredIsPositive(id) 
                    ? scalarProcessor.Add(lcpScalar, scalar) 
                    : scalarProcessor.Subtract(lcpScalar, scalar);
            }

            return lcpScalar;
        }

        public static T ESp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv1, VectorStorage<T> mv2)
        {
            var indexScalarPairs1 = 
                mv1.GetLinVectorIndexScalarStorage();

            var indexScalarPairs2 = 
                mv2.GetLinVectorIndexScalarStorage();

            var spScalar = scalarProcessor.ScalarZero;

            foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                if (!indexScalarPairs2.TryGetScalar(index, out var scalar2))
                    continue;

                var id = index.BasisVectorIndexToId();
                var scalar = scalarProcessor.Times(scalar1, scalar2);

                spScalar = BasisBladeProductUtils.EGpSquaredIsPositive(id) 
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        private static T ESpAsScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
        {
            if (mv1.Grade != mv2.Grade)
                return scalarProcessor.ScalarZero;

            var grade = mv1.Grade;
            var spScalar = scalarProcessor.ScalarZero;
            
            var indexScalarPairs1 = 
                mv1.GetLinVectorIndexScalarStorage();

            var indexScalarPairs2 = 
                mv2.GetLinVectorIndexScalarStorage();

            foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                if (!indexScalarPairs2.TryGetScalar(index, out var scalar2))
                    continue;

                var id = 
                    index.BasisBladeIndexToId(grade);

                var signature = 
                    BasisBladeProductUtils.EGpSquaredSign(id);

                //if (signature == 0) 
                //    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar2);

                spScalar = signature > 0
                    ? scalarProcessor.Add(spScalar, scalar)
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
        {
            return mv1 switch
            {
                T s1 when mv2 is T s2 => ESp(scalarProcessor, s1, s2),
                VectorStorage<T> vt1 when mv2 is VectorStorage<T> vt2 => ESp(scalarProcessor, vt1, vt2),
                _ => ESpAsScalar(scalarProcessor, mv1, mv2)
            };
        }

        private static T ESpAsScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv1, IMultivectorGradedStorage<T> mv2)
        {
            var spScalar = scalarProcessor.ScalarZero;

            var gradeIndexScalarDictionary1 = mv1.GetLinVectorGradedStorage();
            var gradeIndexScalarDictionary2 = mv2.GetLinVectorGradedStorage();

            foreach (var (grade, indexScalarPairs1) in gradeIndexScalarDictionary1.GetGradeStorageRecords())
            {
                if (!gradeIndexScalarDictionary2.TryGetVectorStorage(grade, out var indexScalarPairs2))
                    continue;

                foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
                {
                    if (!indexScalarPairs2.TryGetScalar(index, out var scalar2))
                        continue;

                    var id = 
                        index.BasisBladeIndexToId(grade);

                    var signature = 
                        BasisBladeProductUtils.EGpSquaredSign(id);

                    //if (signature == 0) 
                    //    continue;

                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    spScalar = signature > 0
                        ? scalarProcessor.Add(spScalar, scalar)
                        : scalarProcessor.Subtract(spScalar, scalar);
                }
            }

            return spScalar;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv1, IMultivectorGradedStorage<T> mv2)
        {
            return mv1 switch
            {
                T s1 when mv2 is T s2 => ESp(scalarProcessor, s1, s2),
                VectorStorage<T> vt1 when mv2 is VectorStorage<T> vt2 => ESp(scalarProcessor, vt1, vt2),
                KVectorStorage<T> kvt1 when mv2 is KVectorStorage<T> kvt2 => ESp(scalarProcessor, kvt1, kvt2),
                _ => ESpAsScalar(scalarProcessor, mv1, mv2)
            };
        }

        private static T ESpAsScalar<T>(IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            var spScalar = scalarProcessor.ScalarZero;

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetLinVectorIdScalarStorage();

            foreach (var (id, scalar1) in idScalarPairs1)
            {
                if (!idScalarPairs2.TryGetScalar(id, out var scalar2))
                    continue;
                
                var signature = 
                    BasisBladeProductUtils.EGpSquaredSign(id);

                //if (signature == 0) 
                //    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar2);

                spScalar = signature > 0
                    ? scalarProcessor.Add(spScalar, scalar)
                    : scalarProcessor.Subtract(spScalar, scalar);
                
            }

            return spScalar;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv1, MultivectorStorage<T> mv2)
        {
            return ESpAsScalar(scalarProcessor, mv1, mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return mv1 switch
            {
                T s1 when mv2 is T s2 => ESp(scalarProcessor, s1, s2),
                VectorStorage<T> vt1 when mv2 is VectorStorage<T> vt2 => ESp(scalarProcessor, vt1, vt2),
                KVectorStorage<T> kvt1 when mv2 is KVectorStorage<T> kvt2 => ESp(scalarProcessor, kvt1, kvt2),
                IMultivectorGradedStorage<T> gmv1 when mv2 is IMultivectorGradedStorage<T> gmv2 => ESpAsScalar(scalarProcessor, gmv1, gmv2),
                _ => ESpAsScalar(scalarProcessor, mv1, mv2)
            };
        }
    }
}