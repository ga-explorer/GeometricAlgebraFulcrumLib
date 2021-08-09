using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

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

        public static double Sp(this GaSignatureLookup signature, IGaStorageMultivector<double> mv1)
        {
            if (!signature.IsEuclidean)
                throw new InvalidOperationException();

            var spScalar = 0d;

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

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
        public static T Sp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageScalar<T> mv1)
        {
            return scalarProcessor.Times(
                mv1.GetScalar(scalarProcessor), 
                mv1.GetScalar(scalarProcessor)
            );
        }
        
        public static T VectorsSp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IReadOnlyList<T> vector1)
        {
            var spScalar = scalarProcessor.ZeroScalar;

            var count = vector1.Count;

            for (var index = 0; index < count; index++)
            {
                var sig = signature.SpSignature(1UL << index);

                if (sig == 0)
                    return scalarProcessor.ZeroScalar;
            
                var scalar1 = vector1[index];
                var scalar = scalarProcessor.Times(scalar1, scalar1);
                
                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }
        
        private static T SpAsScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageVector<T> mv1)
        {
            var indexScalarPairs1 = 
                mv1.IndexScalarDictionary;

            var spScalar = scalarProcessor.ZeroScalar;

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                var sig = signature.SpSignature(1UL << (int) index);

                if (sig == 0)
                    return scalarProcessor.ZeroScalar;
            
                var scalar = scalarProcessor.Times(scalar1, scalar1);
                
                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        public static T Sp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageVector<T> mv1)
        {
            return scalarProcessor.SpAsScalar(signature, mv1);
        }

        private static T SpAsScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageKVector<T> mv1)
        {
            var spScalar = scalarProcessor.ZeroScalar;
            
            var indexScalarPairs1 = 
                mv1.IndexScalarDictionary;

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                var sig = signature.SpSignature(1UL << (int) index);

                if (sig == 0)
                    return scalarProcessor.ZeroScalar;
            
                var scalar = scalarProcessor.Times(scalar1, scalar1);
                
                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageKVector<T> mv1)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 => Sp(scalarProcessor, signature, s1),
                IGaStorageVector<T> vt1 => Sp(scalarProcessor, signature, vt1),
                _ => SpAsScalar(scalarProcessor, signature, mv1)
            };
        }

        private static T SpAsScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageMultivector<T> mv1)
        {
            var spScalar = scalarProcessor.ZeroScalar;

            var idScalarDictionary1 = mv1.GetIdScalarDictionary();

            foreach (var (id, scalar1) in idScalarDictionary1)
            {
                var sig = signature.SpSignature(id);

                if (sig == 0)
                    return scalarProcessor.ZeroScalar;
            
                var scalar = scalarProcessor.Times(scalar1, scalar1);
                
                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageMultivector<T> mv1)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 => Sp(scalarProcessor, signature, s1),
                IGaStorageVector<T> vt1 => Sp(scalarProcessor, signature, vt1),
                IGaStorageKVector<T> kvt1 => Sp(scalarProcessor, signature, kvt1),
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

        public static double Sp(this GaSignatureLookup signature, IGaStorageMultivector<double> mv1, IGaStorageMultivector<double> mv2)
        {
            if (!signature.IsEuclidean)
                throw new InvalidOperationException();

            var spScalar = 0d;

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                if (!idScalarPairs2.TryGetValue(id1, out var scalar2))
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
        public static T Sp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageScalar<T> mv1, IGaStorageScalar<T> mv2)
        {
            return scalarProcessor.Times(
                mv1.GetScalar(scalarProcessor), 
                mv2.GetScalar(scalarProcessor)
            );
        }
        
        public static T VectorsSp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IReadOnlyList<T> vector1, IReadOnlyList<T> vector2)
        {
            var spScalar = scalarProcessor.ZeroScalar;

            var count = Math.Min(vector1.Count, vector2.Count);

            for (var index = 0; index < count; index++)
            {
                var sig = signature.SpSignature(1UL << index);

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
        
        private static T SpAsScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageVector<T> mv1, IGaStorageVector<T> mv2)
        {
            var indexScalarPairs1 = 
                mv1.IndexScalarDictionary;

            var indexScalarPairs2 = 
                mv2.IndexScalarDictionary;

            var spScalar = scalarProcessor.ZeroScalar;

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                if (!indexScalarPairs2.TryGetValue(index, out var scalar2))
                    continue;

                var sig = signature.SpSignature(1UL << (int) index);

                if (sig == 0)
                    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar2);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        public static T Sp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageVector<T> mv1, IGaStorageVector<T> mv2)
        {
            return scalarProcessor.SpAsScalar(signature, mv1, mv2);
        }

        private static T SpAsScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageKVector<T> mv1, IGaStorageKVector<T> mv2)
        {
            if (mv1.Grade != mv2.Grade)
                return scalarProcessor.ZeroScalar;

            var grade = mv1.Grade;
            var spScalar = scalarProcessor.ZeroScalar;
            
            var indexScalarPairs1 = 
                mv1.IndexScalarDictionary;

            var indexScalarPairs2 = 
                mv2.IndexScalarDictionary;

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                if (!indexScalarPairs2.TryGetValue(index, out var scalar2))
                    continue;

                var id = GaBasisUtils.BasisBladeId(grade, index);

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
        public static T Sp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageKVector<T> mv1, IGaStorageKVector<T> mv2)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 when mv2 is IGaStorageScalar<T> s2 => 
                    Sp(scalarProcessor, signature, s1, s2),

                IGaStorageVector<T> vt1 when mv2 is IGaStorageVector<T> vt2 => 
                    Sp(scalarProcessor, signature, vt1, vt2),

                _ => SpAsScalar(scalarProcessor, signature, mv1, mv2)
            };
        }

        private static T SpAsScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageMultivectorGraded<T> mv1, IGaStorageMultivectorGraded<T> mv2)
        {
            var spScalar = scalarProcessor.ZeroScalar;

            var gradeIndexScalarDictionary1 = mv1.GetGradeIndexScalarDictionary();
            var gradeIndexScalarDictionary2 = mv2.GetGradeIndexScalarDictionary();

            foreach (var (grade, indexScalarPairs1) in gradeIndexScalarDictionary1)
            {
                if (!gradeIndexScalarDictionary2.TryGetValue(grade, out var indexScalarPairs2))
                    continue;

                foreach (var (index, scalar1) in indexScalarPairs1)
                {
                    if (!indexScalarPairs2.TryGetValue(index, out var scalar2))
                        continue;

                    var id = 
                        GaBasisUtils.BasisBladeId(grade, index);

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
        public static T Sp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageMultivectorGraded<T> mv1, IGaStorageMultivectorGraded<T> mv2)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 when mv2 is IGaStorageScalar<T> s2 => 
                    Sp(scalarProcessor, signature, s1, s2),

                IGaStorageVector<T> vt1 when mv2 is IGaStorageVector<T> vt2 => 
                    Sp(scalarProcessor, signature, vt1, vt2),

                IGaStorageKVector<T> kvt1 when mv2 is IGaStorageKVector<T> kvt2 => 
                    Sp(scalarProcessor, signature, kvt1, kvt2),

                _ => SpAsScalar(scalarProcessor, signature, mv1, mv2)
            };
        }

        private static T SpAsScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            var spScalar = scalarProcessor.ZeroScalar;

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id, scalar1) in idScalarPairs1)
            {
                if (!idScalarPairs2.TryGetValue(id, out var scalar2))
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
        public static T Sp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageMultivectorSparse<T> mv1, IGaStorageMultivectorSparse<T> mv2)
        {
            return SpAsScalar(scalarProcessor, signature, mv1, mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 when mv2 is IGaStorageScalar<T> s2 => 
                    Sp(scalarProcessor, signature, s1, s2),

                IGaStorageVector<T> vt1 when mv2 is IGaStorageVector<T> vt2 => 
                    Sp(scalarProcessor, signature, vt1, vt2),

                IGaStorageKVector<T> kvt1 when mv2 is IGaStorageKVector<T> kvt2 => 
                    Sp(scalarProcessor, signature, kvt1, kvt2),

                IGaStorageMultivectorGraded<T> gmv1 when mv2 is IGaStorageMultivectorGraded<T> gmv2 => 
                    SpAsScalar(scalarProcessor, signature, gmv1, gmv2),

                _ => SpAsScalar(scalarProcessor, signature, mv1, mv2)
            };
        }
    }
}