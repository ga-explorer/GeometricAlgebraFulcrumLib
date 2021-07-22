using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Products.Orthonormal
{
    public static class GaProductOrtNormUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBilinearProductResult NormSquared(this IGaSignature signature, ulong id1)
        {
            return new GaBasisBilinearProductResult(
                signature.NormSquaredSignature(id1), 
                0
            );
        }

        public static double Norm(this GaSignatureLookup signature, IGasMultivector<double> mv1)
        {
            if (!signature.IsEuclidean)
                throw new InvalidOperationException();

            var spScalar = 0d;

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                var sig = signature.NormSquaredSignature(id1);

                if (sig == 0)
                    continue;

                var scalar = scalar1 * scalar1;

                spScalar = sig > 0
                    ? spScalar + scalar
                    : spScalar - scalar;
            }

            return Math.Sqrt(Math.Abs(spScalar));
        }

        public static double NormSquared(this GaSignatureLookup signature, IGasMultivector<double> mv1)
        {
            if (!signature.IsEuclidean)
                throw new InvalidOperationException();

            var spScalar = 0d;

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                var sig = signature.NormSquaredSignature(id1);

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
        public static T Norm<T>(this IGaSignature signature, IGasScalar<T> mv1)
        {
            return mv1.ScalarProcessor.SqrtOfAbs(
                mv1.ScalarProcessor.Times(mv1.Scalar, mv1.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaSignature signature, IGasScalar<T> mv1)
        {
            return mv1.ScalarProcessor.Times(mv1.Scalar, mv1.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Norm<T>(this IGaSignature signature, IGasVectorTerm<T> mv1)
        {
            var sig =
                signature.NormSquaredSignature(1UL << (int) mv1.Index);

            if (sig == 0)
                return mv1.ScalarProcessor.ZeroScalar;

            return mv1.ScalarProcessor.SqrtOfAbs(
                mv1.ScalarProcessor.Times(mv1.Scalar, mv1.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaSignature signature, IGasVectorTerm<T> mv1)
        {
            var sig =
                signature.NormSquaredSignature(1UL << (int) mv1.Index);

            if (sig == 0)
                return mv1.ScalarProcessor.ZeroScalar;

            return mv1.ScalarProcessor.SqrtOfAbs(
                mv1.ScalarProcessor.Times(mv1.Scalar, mv1.Scalar)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T NormAsScalar<T>(this IGaSignature signature, IGasKVectorTerm<T> mv1)
        {
            var sig =
                signature.NormSquaredSignature(mv1.Id);

            if (sig == 0)
                return mv1.ScalarProcessor.ZeroScalar;

            var scalar = 
                mv1.ScalarProcessor.Times(mv1.Scalar, mv1.Scalar);

            return sig > 0
                ? scalar
                : mv1.ScalarProcessor.Negative(scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T NormSquaredAsScalar<T>(this IGaSignature signature, IGasKVectorTerm<T> mv1)
        {
            var sig =
                signature.NormSquaredSignature(mv1.Id);

            if (sig == 0)
                return mv1.ScalarProcessor.ZeroScalar;

            var scalar = 
                mv1.ScalarProcessor.Times(mv1.Scalar, mv1.Scalar);

            return sig > 0
                ? scalar
                : mv1.ScalarProcessor.Negative(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Norm<T>(this IGaSignature signature, IGasKVectorTerm<T> mv1)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 => 
                    Norm(signature, s1),

                IGasVectorTerm<T> vt1 => 
                    Norm(signature, vt1),

                _ => 
                    NormAsScalar(signature, mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaSignature signature, IGasKVectorTerm<T> mv1)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 => 
                    NormSquared(signature, s1),

                IGasVectorTerm<T> vt1 => 
                    NormSquared(signature, vt1),

                _ => 
                    NormSquaredAsScalar(signature, mv1)
            };
        }
        
        public static T VectorsNorm<T>(this IGaSignature signature, IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector1)
        {
            var spScalar = scalarProcessor.ZeroScalar;

            var count = vector1.Count;

            for (var index = 0; index < count; index++)
            {
                var sig = signature.NormSquaredSignature(1UL << index);

                if (sig == 0)
                    continue;

                var scalar1 = vector1[index];
                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return scalarProcessor.SqrtOfAbs(spScalar);
        }
        
        public static T VectorsNormSquared<T>(this IGaSignature signature, IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector1)
        {
            var spScalar = scalarProcessor.ZeroScalar;

            var count = vector1.Count;

            for (var index = 0; index < count; index++)
            {
                var sig = signature.NormSquaredSignature(1UL << index);

                if (sig == 0)
                    continue;

                var scalar1 = vector1[index];
                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }
        
        private static T NormAsScalar<T>(this IGaSignature signature, IGasVector<T> mv1)
        {
            var scalarProcessor = mv1.ScalarProcessor;

            var indexScalarPairs1 = 
                mv1.GetIndexScalarPairs();

            var spScalar = scalarProcessor.ZeroScalar;

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                var sig = signature.NormSquaredSignature(1UL << (int) index);

                if (sig == 0)
                    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return scalarProcessor.SqrtOfAbs(spScalar);
        }
        
        private static T NormSquaredAsScalar<T>(this IGaSignature signature, IGasVector<T> mv1)
        {
            var scalarProcessor = mv1.ScalarProcessor;

            var indexScalarPairs1 = 
                mv1.GetIndexScalarPairs();

            var spScalar = scalarProcessor.ZeroScalar;

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                var sig = signature.NormSquaredSignature(1UL << (int) index);

                if (sig == 0)
                    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Norm<T>(this IGaSignature signature, IGasVector<T> mv1)
        {
            return mv1 is IGasVectorTerm<T> vt1
                ? Norm(signature, vt1)
                : NormAsScalar(signature, mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaSignature signature, IGasVector<T> mv1)
        {
            return mv1 is IGasVectorTerm<T> vt1
                ? NormSquared(signature, vt1)
                : NormSquaredAsScalar(signature, mv1);
        }

        private static T NormAsScalar<T>(this IGaSignature signature, IGasKVector<T> mv1)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            var grade = mv1.Grade;
            var spScalar = scalarProcessor.ZeroScalar;
            
            var indexScalarPairs1 = 
                mv1.GetIndexScalarPairs();

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);
                var sig = signature.NormSquaredSignature(id);

                if (sig == 0)
                    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return scalarProcessor.SqrtOfAbs(spScalar);
        }

        private static T NormSquaredAsScalar<T>(this IGaSignature signature, IGasKVector<T> mv1)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            var grade = mv1.Grade;
            var spScalar = scalarProcessor.ZeroScalar;
            
            var indexScalarPairs1 = 
                mv1.GetIndexScalarPairs();

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);
                var sig = signature.NormSquaredSignature(id);

                if (sig == 0)
                    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaSignature signature, IGasKVector<T> mv1)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 => NormSquared(signature, s1),
                IGasVectorTerm<T> vt1 => NormSquared(signature, vt1),
                IGasKVectorTerm<T> kvt1 => NormSquared(signature, kvt1),
                IGasVector<T> v1 => NormSquaredAsScalar(signature, v1),
                _ => NormSquaredAsScalar(signature, mv1)
            };
        }

        private static T NormAsScalar<T>(this IGaSignature signature, IGasMultivector<T> mv1)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            var spScalar = scalarProcessor.ZeroScalar;

            var idScalarDictionary1 = mv1.GetIdScalarDictionary();

            foreach (var (id, scalar1) in idScalarDictionary1)
            {
                var sig = signature.NormSquaredSignature(id);

                if (sig == 0)
                    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return scalarProcessor.SqrtOfAbs(spScalar);
        }

        private static T NormSquaredAsScalar<T>(this IGaSignature signature, IGasMultivector<T> mv1)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            var spScalar = scalarProcessor.ZeroScalar;

            var idScalarDictionary1 = mv1.GetIdScalarDictionary();

            foreach (var (id, scalar1) in idScalarDictionary1)
            {
                var sig = signature.NormSquaredSignature(id);

                if (sig == 0)
                    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = sig > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Norm<T>(this IGaSignature signature, IGasMultivector<T> mv1)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 => Norm(signature, s1),
                IGasVectorTerm<T> vt1 => Norm(signature, vt1),
                IGasKVectorTerm<T> kvt1 => Norm(signature, kvt1),
                IGasVector<T> v1 => NormAsScalar(signature, v1),
                IGasKVector<T> kv1 => NormAsScalar(signature, kv1),
                _ => NormAsScalar(signature, mv1)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaSignature signature, IGasMultivector<T> mv1)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 => NormSquared(signature, s1),
                IGasVectorTerm<T> vt1 => NormSquared(signature, vt1),
                IGasKVectorTerm<T> kvt1 => NormSquared(signature, kvt1),
                IGasVector<T> v1 => NormSquaredAsScalar(signature, v1),
                IGasKVector<T> kv1 => NormSquaredAsScalar(signature, kv1),
                _ => NormSquaredAsScalar(signature, mv1)
            };
        }
        
    }
}