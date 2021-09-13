using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.Signatures;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorStorageSpOrtUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBilinearProductResult Sp(this IGeometricAlgebraSignature signature, ulong id1)
        {
            return new BasisBilinearProductResult(
                signature.SpSignature(id1), 
                0
            );
        }

        public static double Sp(this GeometricAlgebraSignatureLookup signature, IMultivectorStorage<double> mv1)
        {
            if (!signature.IsEuclidean)
                throw new InvalidOperationException();

            var spScalar = 0d;

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                var sig = signature.SpSignature(id1);

                if (sig == 0)
                    continue;

                var scalar = scalar1 * scalar1;

                spScalar = sig > 0
                    ? spScalar + scalar
                    : spScalar - scalar;
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, T mv1)
        {
            return scalarProcessor.Times(mv1, mv1);
        }
        
        public static T VectorsSp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, IReadOnlyList<T> vector1)
        {
            var spScalar = scalarProcessor.ScalarZero;

            var count = vector1.Count;

            for (var index = 0; index < count; index++)
            {
                var sig = signature.SpSignature(index.BasisVectorIndexToId());

                if (sig == 0)
                    return scalarProcessor.ScalarZero;
            
                var scalar1 = vector1[index];
                var scalar = scalarProcessor.Times(scalar1, scalar1);
                
                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }
        
        private static T SpAsScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, VectorStorage<T> mv1)
        {
            var indexScalarPairs1 = 
                mv1.GetLinVectorIndexScalarStorage();

            var spScalar = scalarProcessor.ScalarZero;

            foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var sig = signature.SpSignature(index.BasisVectorIndexToId());

                if (sig == 0)
                    return scalarProcessor.ScalarZero;
            
                var scalar = scalarProcessor.Times(scalar1, scalar1);
                
                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        public static T Sp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, VectorStorage<T> mv1)
        {
            return scalarProcessor.SpAsScalar(signature, mv1);
        }

        private static T SpAsScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, KVectorStorage<T> mv1)
        {
            var spScalar = scalarProcessor.ScalarZero;
            
            var indexScalarPairs1 = 
                mv1.GetLinVectorIndexScalarStorage();

            foreach (var (index, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var sig = signature.SpSignature(index.BasisVectorIndexToId());

                if (sig == 0)
                    return scalarProcessor.ScalarZero;
            
                var scalar = scalarProcessor.Times(scalar1, scalar1);
                
                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, KVectorStorage<T> mv1)
        {
            return mv1 switch
            {
                T s1 => Sp(scalarProcessor, signature, s1),
                VectorStorage<T> vt1 => Sp(scalarProcessor, signature, vt1),
                _ => SpAsScalar(scalarProcessor, signature, mv1)
            };
        }

        private static T SpAsScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, IMultivectorStorage<T> mv1)
        {
            var spScalar = scalarProcessor.ScalarZero;

            var idScalarDictionary1 = mv1.GetLinVectorIdScalarStorage();

            foreach (var (id, scalar1) in idScalarDictionary1.GetIndexScalarRecords())
            {
                var sig = signature.SpSignature(id);

                if (sig == 0)
                    return scalarProcessor.ScalarZero;
            
                var scalar = scalarProcessor.Times(scalar1, scalar1);
                
                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, IMultivectorStorage<T> mv1)
        {
            return mv1 switch
            {
                T s1 => Sp(scalarProcessor, signature, s1),
                VectorStorage<T> vt1 => Sp(scalarProcessor, signature, vt1),
                KVectorStorage<T> kvt1 => Sp(scalarProcessor, signature, kvt1),
                _ => SpAsScalar(scalarProcessor, signature, mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBilinearProductResult Sp(this IGeometricAlgebraSignature signature, ulong id1, ulong id2)
        {
            return new BasisBilinearProductResult(
                signature.SpSignature(id1, id2), 
                id1 ^ id2
            );
        }

        public static double Sp(this GeometricAlgebraSignatureLookup signature, IMultivectorStorage<double> mv1, IMultivectorStorage<double> mv2)
        {
            if (!signature.IsEuclidean)
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

                var sig = signature.SpSignature(id1);

                if (sig == 0)
                    continue;

                var scalar = scalar1 * scalar2;

                spScalar = sig > 0
                    ? spScalar + scalar
                    : spScalar - scalar;
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, T mv1, T mv2)
        {
            return scalarProcessor.Times(mv1, mv2);
        }
        
        public static T VectorsSp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, IReadOnlyList<T> vector1, IReadOnlyList<T> vector2)
        {
            var spScalar = scalarProcessor.ScalarZero;

            var count = Math.Min(vector1.Count, vector2.Count);

            for (var index = 0; index < count; index++)
            {
                var sig = signature.SpSignature(index.BasisVectorIndexToId());

                if (sig == 0)
                    continue;

                var scalar1 = vector1[index];
                var scalar2 = vector2[index];

                var scalar = scalarProcessor.Times(scalar1, scalar2);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }
        
        private static T SpAsScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, VectorStorage<T> mv1, VectorStorage<T> mv2)
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

                var sig = signature.SpSignature(index.BasisVectorIndexToId());

                if (sig == 0)
                    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar2);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        public static T Sp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, VectorStorage<T> mv1, VectorStorage<T> mv2)
        {
            return scalarProcessor.SpAsScalar(signature, mv1, mv2);
        }

        private static T SpAsScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
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

                var id = index.BasisBladeIndexToId(grade);

                var sig = signature.SpSignature(id);

                if (sig == 0)
                    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar2);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
        {
            return mv1 switch
            {
                T s1 when mv2 is T s2 => 
                    Sp(scalarProcessor, signature, s1, s2),

                VectorStorage<T> vt1 when mv2 is VectorStorage<T> vt2 => 
                    Sp(scalarProcessor, signature, vt1, vt2),

                _ => SpAsScalar(scalarProcessor, signature, mv1, mv2)
            };
        }

        private static T SpAsScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, IMultivectorGradedStorage<T> mv1, IMultivectorGradedStorage<T> mv2)
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

                    var sig = signature.SpSignature(id);

                    if (sig == 0)
                        continue;

                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    spScalar = sig > 0
                        ? scalarProcessor.Add(spScalar, scalar) 
                        : scalarProcessor.Subtract(spScalar, scalar);
                }
            }

            return spScalar;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, IMultivectorGradedStorage<T> mv1, IMultivectorGradedStorage<T> mv2)
        {
            return mv1 switch
            {
                T s1 when mv2 is T s2 => 
                    Sp(scalarProcessor, signature, s1, s2),

                VectorStorage<T> vt1 when mv2 is VectorStorage<T> vt2 => 
                    Sp(scalarProcessor, signature, vt1, vt2),

                KVectorStorage<T> kvt1 when mv2 is KVectorStorage<T> kvt2 => 
                    Sp(scalarProcessor, signature, kvt1, kvt2),

                _ => SpAsScalar(scalarProcessor, signature, mv1, mv2)
            };
        }

        private static T SpAsScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
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
                
                var sig = signature.SpSignature(id);

                if (sig == 0)
                    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar2);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, MultivectorStorage<T> mv1, MultivectorStorage<T> mv2)
        {
            return SpAsScalar(scalarProcessor, signature, mv1, mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature signature, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return mv1 switch
            {
                T s1 when mv2 is T s2 => 
                    Sp(scalarProcessor, signature, s1, s2),

                VectorStorage<T> vt1 when mv2 is VectorStorage<T> vt2 => 
                    Sp(scalarProcessor, signature, vt1, vt2),

                KVectorStorage<T> kvt1 when mv2 is KVectorStorage<T> kvt2 => 
                    Sp(scalarProcessor, signature, kvt1, kvt2),

                IMultivectorGradedStorage<T> gmv1 when mv2 is IMultivectorGradedStorage<T> gmv2 => 
                    SpAsScalar(scalarProcessor, signature, gmv1, gmv2),

                _ => SpAsScalar(scalarProcessor, signature, mv1, mv2)
            };
        }
    }
}