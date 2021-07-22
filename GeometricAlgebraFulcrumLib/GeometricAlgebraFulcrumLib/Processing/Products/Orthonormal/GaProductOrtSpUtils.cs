using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Products.Orthonormal
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

        public static double Sp(this GaSignatureLookup signature, IGasMultivector<double> mv1)
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
        public static T Sp<T>(this IGaSignature signature, IGasScalar<T> mv1)
        {
            return mv1.ScalarProcessor.Times(mv1.Scalar, mv1.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGaSignature signature, IGasVectorTerm<T> mv1)
        {

            var sig = signature.SpSignature(1UL << (int) mv1.Index);

            if (sig == 0)
                return mv1.ScalarProcessor.ZeroScalar;
            
            var scalar = 
                mv1.ScalarProcessor.Times(mv1.Scalar, mv1.Scalar);

            return sig > 0
                ? scalar
                : mv1.ScalarProcessor.Negative(scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T SpAsScalar<T>(this IGaSignature signature, IGasKVectorTerm<T> mv1)
        {
            var sig = signature.SpSignature(1UL << (int) mv1.Index);

            if (sig == 0)
                return mv1.ScalarProcessor.ZeroScalar;
            
            var scalar = 
                mv1.ScalarProcessor.Times(mv1.Scalar, mv1.Scalar);

            return sig > 0
                ? scalar
                : mv1.ScalarProcessor.Negative(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGaSignature signature, IGasKVectorTerm<T> mv1)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 => Sp(signature, s1),
                IGasVectorTerm<T> vt1 => Sp(signature, vt1),
                _ => SpAsScalar(signature, mv1)
            };
        }
        
        public static T VectorsSp<T>(this IGaSignature signature, IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector1)
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
        
        private static T SpAsScalar<T>(this IGaSignature signature, IGasVector<T> mv1)
        {
            var scalarProcessor = mv1.ScalarProcessor;

            var indexScalarPairs1 = 
                mv1.GetIndexScalarPairs();

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

        public static T Sp<T>(this IGaSignature signature, IGasVector<T> mv1)
        {
            return mv1 is IGasVectorTerm<T> vt1
                ? Sp(signature, vt1)
                : SpAsScalar(signature, mv1);
        }

        private static T SpAsScalar<T>(this IGaSignature signature, IGasKVector<T> mv1)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            var spScalar = scalarProcessor.ZeroScalar;
            
            var indexScalarPairs1 = 
                mv1.GetIndexScalarPairs();

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
        public static T Sp<T>(this IGaSignature signature, IGasKVector<T> mv1)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 => Sp(signature, s1),
                IGasVectorTerm<T> vt1 => Sp(signature, vt1),
                IGasKVectorTerm<T> kvt1 => Sp(signature, kvt1),
                IGasVector<T> v1 => SpAsScalar(signature, v1),
                _ => SpAsScalar(signature, mv1)
            };
        }

        private static T SpAsScalar<T>(this IGaSignature signature, IGasMultivector<T> mv1)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

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
        public static T Sp<T>(this IGaSignature signature, IGasMultivector<T> mv1)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 => Sp(signature, s1),
                IGasVectorTerm<T> vt1 => Sp(signature, vt1),
                IGasKVectorTerm<T> kvt1 => Sp(signature, kvt1),
                IGasVector<T> v1 => SpAsScalar(signature, v1),
                IGasKVector<T> kv1 => SpAsScalar(signature, kv1),
                _ => SpAsScalar(signature, mv1)
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

        public static double Sp(this GaSignatureLookup signature, IGasMultivector<double> mv1, IGasMultivector<double> mv2)
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
        public static T Sp<T>(this IGaSignature signature, IGasScalar<T> mv1, IGasScalar<T> mv2)
        {
            return mv1.ScalarProcessor.Times(mv1.Scalar, mv2.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGaSignature signature, IGasVectorTerm<T> mv1, IGasVectorTerm<T> mv2)
        {
            if (mv1.Index != mv2.Index)
                return mv1.ScalarProcessor.ZeroScalar;

            var sig = signature.SpSignature(1UL << (int) mv1.Index);

            return sig switch
            {
                0 => mv1.ScalarProcessor.ZeroScalar,
                1 => mv1.ScalarProcessor.Times(mv1.Scalar, mv2.Scalar),
                _ => mv1.ScalarProcessor.NegativeTimes(mv1.Scalar, mv2.Scalar)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T SpAsScalar<T>(this IGaSignature signature, IGasKVectorTerm<T> mv1, IGasKVectorTerm<T> mv2)
        {
            var sig = signature.SpSignature(mv1.Id, mv2.Id);

            return sig switch
            {
                0 => mv1.ScalarProcessor.ZeroScalar,
                1 => mv1.ScalarProcessor.Times(mv1.Scalar, mv2.Scalar),
                _ => mv1.ScalarProcessor.NegativeTimes(mv1.Scalar, mv2.Scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGaSignature signature, IGasKVectorTerm<T> mv1, IGasKVectorTerm<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => 
                    Sp(signature, s1, s2),

                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => 
                    Sp(signature, vt1, vt2),

                _ => 
                    SpAsScalar(signature, mv1, mv2)
            };
        }
        
        public static T VectorsSp<T>(this IGaSignature signature, IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector1, IReadOnlyList<T> vector2)
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
        
        private static T SpAsScalar<T>(this IGaSignature signature, IGasVector<T> mv1, IGasVector<T> mv2)
        {
            var scalarProcessor = mv1.ScalarProcessor;

            var indexScalarPairs1 = 
                mv1.GetIndexScalarPairs();

            var indexScalarPairs2 = 
                mv2.GetIndexScalarDictionary();

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

        public static T Sp<T>(this IGaSignature signature, IGasVector<T> mv1, IGasVector<T> mv2)
        {
            return mv1 is IGasVectorTerm<T> vt1 && mv2 is IGasVectorTerm<T> vt2
                ? Sp(signature, vt1, vt2)
                : SpAsScalar(signature, mv1, mv2);
        }

        private static T SpAsScalar<T>(this IGaSignature signature, IGasKVector<T> mv1, IGasKVector<T> mv2)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            if (mv1.Grade != mv2.Grade)
                return scalarProcessor.ZeroScalar;

            var grade = mv1.Grade;
            var spScalar = scalarProcessor.ZeroScalar;
            
            var indexScalarPairs1 = 
                mv1.GetIndexScalarPairs();

            var indexScalarPairs2 = 
                mv2.GetIndexScalarDictionary();

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
        public static T Sp<T>(this IGaSignature signature, IGasKVector<T> mv1, IGasKVector<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => Sp(signature, s1, s2),
                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => Sp(signature, vt1, vt2),
                IGasKVectorTerm<T> kvt1 when mv2 is IGasKVectorTerm<T> kvt2 => Sp(signature, kvt1, kvt2),
                IGasVector<T> v1 when mv2 is IGasVector<T> v2 => SpAsScalar(signature, v1, v2),
                _ => SpAsScalar(signature, mv1, mv2)
            };
        }

        private static T SpAsScalar<T>(this IGaSignature signature, IGasGradedMultivector<T> mv1, IGasGradedMultivector<T> mv2)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

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
        public static T Sp<T>(this IGaSignature signature, IGasGradedMultivector<T> mv1, IGasGradedMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => Sp(signature, s1, s2),
                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => Sp(signature, vt1, vt2),
                IGasKVectorTerm<T> kvt1 when mv2 is IGasKVectorTerm<T> kvt2 => Sp(signature, kvt1, kvt2),
                IGasVector<T> v1 when mv2 is IGasVector<T> v2 => SpAsScalar(signature, v1, v2),
                IGasKVector<T> kv1 when mv2 is IGasKVector<T> kv2 => SpAsScalar(signature, kv1, kv2),
                _ => SpAsScalar(signature, mv1, mv2)
            };
        }

        private static T SpAsScalar<T>(IGaSignature signature, IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

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
        public static T Sp<T>(this IGaSignature signature, IGasTermsMultivector<T> mv1, IGasTermsMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => Sp(signature, s1, s2),
                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => Sp(signature, vt1, vt2),
                IGasKVectorTerm<T> kvt1 when mv2 is IGasKVectorTerm<T> kvt2 => Sp(signature, kvt1, kvt2),
                _ => SpAsScalar(signature, mv1, mv2)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGaSignature signature, IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => Sp(signature, s1, s2),
                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => Sp(signature, vt1, vt2),
                IGasKVectorTerm<T> kvt1 when mv2 is IGasKVectorTerm<T> kvt2 => Sp(signature, kvt1, kvt2),
                IGasVector<T> v1 when mv2 is IGasVector<T> v2 => SpAsScalar(signature, v1, v2),
                IGasKVector<T> kv1 when mv2 is IGasKVector<T> kv2 => SpAsScalar(signature, kv1, kv2),
                IGasGradedMultivector<T> gmv1 when mv2 is IGasGradedMultivector<T> gmv2 => SpAsScalar(signature, gmv1, gmv2),
                _ => SpAsScalar(signature, mv1, mv2)
            };
        }
    }
}