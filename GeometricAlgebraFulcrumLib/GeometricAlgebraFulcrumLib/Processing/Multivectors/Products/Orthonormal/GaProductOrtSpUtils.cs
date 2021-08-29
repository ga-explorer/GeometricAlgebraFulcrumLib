using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal
{
    public static class GaProductOrtSpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBilinearProductResult Sp(this IGaSignature signature, ulong id1)
        {
            return new GaBasisBilinearProductResult(
                signature.SpSignature(id1), 
                0
            );
        }

        public static double Sp(this GaSignatureLookup signature, IGaMultivectorStorage<double> mv1)
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
        public static T Sp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaScalarStorage<T> mv1)
        {
            return scalarProcessor.Times(
                mv1.GetScalar(scalarProcessor), 
                mv1.GetScalar(scalarProcessor)
            );
        }
        
        public static T VectorsSp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IReadOnlyList<T> vector1)
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
        
        private static T SpAsScalar<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaVectorStorage<T> mv1)
        {
            var indexScalarPairs1 = 
                mv1.IndexScalarList;

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

        public static T Sp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaVectorStorage<T> mv1)
        {
            return scalarProcessor.SpAsScalar(signature, mv1);
        }

        private static T SpAsScalar<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaKVectorStorage<T> mv1)
        {
            var spScalar = scalarProcessor.ScalarZero;
            
            var indexScalarPairs1 = 
                mv1.IndexScalarList;

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
        public static T Sp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaKVectorStorage<T> mv1)
        {
            return mv1 switch
            {
                IGaScalarStorage<T> s1 => Sp(scalarProcessor, signature, s1),
                IGaVectorStorage<T> vt1 => Sp(scalarProcessor, signature, vt1),
                _ => SpAsScalar(scalarProcessor, signature, mv1)
            };
        }

        private static T SpAsScalar<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaMultivectorStorage<T> mv1)
        {
            var spScalar = scalarProcessor.ScalarZero;

            var idScalarDictionary1 = mv1.GetIdScalarList();

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
        public static T Sp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaMultivectorStorage<T> mv1)
        {
            return mv1 switch
            {
                IGaScalarStorage<T> s1 => Sp(scalarProcessor, signature, s1),
                IGaVectorStorage<T> vt1 => Sp(scalarProcessor, signature, vt1),
                IGaKVectorStorage<T> kvt1 => Sp(scalarProcessor, signature, kvt1),
                _ => SpAsScalar(scalarProcessor, signature, mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBilinearProductResult Sp(this IGaSignature signature, ulong id1, ulong id2)
        {
            return new GaBasisBilinearProductResult(
                signature.SpSignature(id1, id2), 
                id1 ^ id2
            );
        }

        public static double Sp(this GaSignatureLookup signature, IGaMultivectorStorage<double> mv1, IGaMultivectorStorage<double> mv2)
        {
            if (!signature.IsEuclidean)
                throw new InvalidOperationException();

            var spScalar = 0d;

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetIdScalarList();

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
        public static T Sp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaScalarStorage<T> mv1, IGaScalarStorage<T> mv2)
        {
            return scalarProcessor.Times(
                mv1.GetScalar(scalarProcessor), 
                mv2.GetScalar(scalarProcessor)
            );
        }
        
        public static T VectorsSp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IReadOnlyList<T> vector1, IReadOnlyList<T> vector2)
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
        
        private static T SpAsScalar<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaVectorStorage<T> mv1, IGaVectorStorage<T> mv2)
        {
            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            var indexScalarPairs2 = 
                mv2.IndexScalarList;

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

        public static T Sp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaVectorStorage<T> mv1, IGaVectorStorage<T> mv2)
        {
            return scalarProcessor.SpAsScalar(signature, mv1, mv2);
        }

        private static T SpAsScalar<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaKVectorStorage<T> mv1, IGaKVectorStorage<T> mv2)
        {
            if (mv1.Grade != mv2.Grade)
                return scalarProcessor.ScalarZero;

            var grade = mv1.Grade;
            var spScalar = scalarProcessor.ScalarZero;
            
            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            var indexScalarPairs2 = 
                mv2.IndexScalarList;

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
        public static T Sp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaKVectorStorage<T> mv1, IGaKVectorStorage<T> mv2)
        {
            return mv1 switch
            {
                IGaScalarStorage<T> s1 when mv2 is IGaScalarStorage<T> s2 => 
                    Sp(scalarProcessor, signature, s1, s2),

                IGaVectorStorage<T> vt1 when mv2 is IGaVectorStorage<T> vt2 => 
                    Sp(scalarProcessor, signature, vt1, vt2),

                _ => SpAsScalar(scalarProcessor, signature, mv1, mv2)
            };
        }

        private static T SpAsScalar<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaMultivectorGradedStorage<T> mv1, IGaMultivectorGradedStorage<T> mv2)
        {
            var spScalar = scalarProcessor.ScalarZero;

            var gradeIndexScalarDictionary1 = mv1.GetGradeIndexScalarList();
            var gradeIndexScalarDictionary2 = mv2.GetGradeIndexScalarList();

            foreach (var (grade, indexScalarPairs1) in gradeIndexScalarDictionary1.GetGradeStorageRecords())
            {
                if (!gradeIndexScalarDictionary2.TryGetEvenStorage(grade, out var indexScalarPairs2))
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
        public static T Sp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaMultivectorGradedStorage<T> mv1, IGaMultivectorGradedStorage<T> mv2)
        {
            return mv1 switch
            {
                IGaScalarStorage<T> s1 when mv2 is IGaScalarStorage<T> s2 => 
                    Sp(scalarProcessor, signature, s1, s2),

                IGaVectorStorage<T> vt1 when mv2 is IGaVectorStorage<T> vt2 => 
                    Sp(scalarProcessor, signature, vt1, vt2),

                IGaKVectorStorage<T> kvt1 when mv2 is IGaKVectorStorage<T> kvt2 => 
                    Sp(scalarProcessor, signature, kvt1, kvt2),

                _ => SpAsScalar(scalarProcessor, signature, mv1, mv2)
            };
        }

        private static T SpAsScalar<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            var spScalar = scalarProcessor.ScalarZero;

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetIdScalarList();

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
        public static T Sp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaMultivectorSparseStorage<T> mv1, IGaMultivectorSparseStorage<T> mv2)
        {
            return SpAsScalar(scalarProcessor, signature, mv1, mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return mv1 switch
            {
                IGaScalarStorage<T> s1 when mv2 is IGaScalarStorage<T> s2 => 
                    Sp(scalarProcessor, signature, s1, s2),

                IGaVectorStorage<T> vt1 when mv2 is IGaVectorStorage<T> vt2 => 
                    Sp(scalarProcessor, signature, vt1, vt2),

                IGaKVectorStorage<T> kvt1 when mv2 is IGaKVectorStorage<T> kvt2 => 
                    Sp(scalarProcessor, signature, kvt1, kvt2),

                IGaMultivectorGradedStorage<T> gmv1 when mv2 is IGaMultivectorGradedStorage<T> gmv2 => 
                    SpAsScalar(scalarProcessor, signature, gmv1, gmv2),

                _ => SpAsScalar(scalarProcessor, signature, mv1, mv2)
            };
        }
    }
}