using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Products.Euclidean
{
    public static class GaProductEucNormUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBilinearProductResult ENormSquared(this IGaSignature signature, ulong id1)
        {
            return new GaBasisBilinearProductResult(
                GaBasisUtils.ENormSquaredSignature(id1), 
                0
            );
        }

        public static double ENorm(this GaSignatureLookup basisSignature, IGasMultivector<double> mv1)
        {
            if (!basisSignature.IsEuclidean)
                throw new InvalidOperationException();

            var spScalar = 0d;

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                var scalar = scalar1 * scalar1;

                spScalar = GaBasisUtils.ENormSquaredSignature(id1) > 0
                    ? spScalar + scalar
                    : spScalar - scalar;
            }

            return Math.Sqrt(Math.Abs(spScalar));
        }

        public static double ENormSquared(this GaSignatureLookup basisSignature, IGasMultivector<double> mv1)
        {
            if (!basisSignature.IsEuclidean)
                throw new InvalidOperationException();

            var spScalar = 0d;

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                var scalar = scalar1 * scalar1;

                spScalar = GaBasisUtils.ENormSquaredSignature(id1) > 0
                    ? spScalar + scalar
                    : spScalar - scalar;
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ENorm<T>(this IGasScalar<T> mv1)
        {
            return mv1.ScalarProcessor.SqrtOfAbs(
                mv1.ScalarProcessor.Times(mv1.Scalar, mv1.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ENormSquared<T>(this IGasScalar<T> mv1)
        {
            return mv1.ScalarProcessor.Times(mv1.Scalar, mv1.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ENorm<T>(this IGasVectorTerm<T> mv1)
        {
            return mv1.ScalarProcessor.SqrtOfAbs(
                mv1.ScalarProcessor.Times(mv1.Scalar, mv1.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ENormSquared<T>(this IGasVectorTerm<T> mv1)
        {
            var scalar = 
                mv1.ScalarProcessor.Times(mv1.Scalar, mv1.Scalar);

            return GaBasisUtils.ENormSquaredSignature(1UL << (int) mv1.Index) > 0
                ? scalar
                : mv1.ScalarProcessor.Negative(scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T ENormAsScalar<T>(this IGasKVectorTerm<T> mv1)
        {
            //var signature = 
            //    GaBasisUtils.ENormSquaredSignature(mv1.Id);

            //if (signature == 0)
            //    return mv1.ScalarProcessor.ZeroScalar;

            return mv1.ScalarProcessor.SqrtOfAbs(
                mv1.ScalarProcessor.Times(mv1.Scalar, mv1.Scalar)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T ENormSquaredAsScalar<T>(this IGasKVectorTerm<T> mv1)
        {
            var signature = 
                GaBasisUtils.ENormSquaredSignature(mv1.Id);

            return signature switch
            {
                //0 => mv1.ScalarProcessor.ZeroScalar,
                1 => mv1.ScalarProcessor.Times(mv1.Scalar, mv1.Scalar),
                _ => mv1.ScalarProcessor.NegativeTimes(mv1.Scalar, mv1.Scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ENorm<T>(this IGasKVectorTerm<T> mv1)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 => 
                    ENorm(s1),

                IGasVectorTerm<T> vt1 => 
                    ENorm(vt1),

                _ => 
                    ENormAsScalar(mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ENormSquared<T>(this IGasKVectorTerm<T> mv1)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 => 
                    ENormSquared(s1),

                IGasVectorTerm<T> vt1 => 
                    ENormSquared(vt1),

                _ => 
                    ENormSquaredAsScalar(mv1)
            };
        }
        
        public static T VectorsENorm<T>(this IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector1)
        {
            var spScalar = scalarProcessor.ZeroScalar;

            var count = vector1.Count;

            for (var index = 0; index < count; index++)
            {
                var id = 1UL << index;
                var scalar1 = vector1[index];

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = GaBasisUtils.ENormSquaredSignature(id) > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return scalarProcessor.SqrtOfAbs(spScalar);
        }
        
        public static T VectorsENormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector1)
        {
            var spScalar = scalarProcessor.ZeroScalar;

            var count = vector1.Count;

            for (var index = 0; index < count; index++)
            {
                var id = 1UL << index;
                var scalar1 = vector1[index];

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = GaBasisUtils.ENormSquaredSignature(id) > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }
        
        private static T ENormAsScalar<T>(this IGasVector<T> mv1)
        {
            var scalarProcessor = mv1.ScalarProcessor;

            var indexScalarPairs1 = 
                mv1.GetIndexScalarPairs();

            var spScalar = scalarProcessor.ZeroScalar;

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                var id = 1UL << (int) index;
                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = GaBasisUtils.ENormSquaredSignature(id) > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return scalarProcessor.SqrtOfAbs(spScalar);
        }
        
        private static T ENormSquaredAsScalar<T>(this IGasVector<T> mv1)
        {
            var scalarProcessor = mv1.ScalarProcessor;

            var indexScalarPairs1 = 
                mv1.GetIndexScalarPairs();

            var spScalar = scalarProcessor.ZeroScalar;

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                var id = 1UL << (int) index;
                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = GaBasisUtils.ENormSquaredSignature(id) > 0
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ENorm<T>(this IGasVector<T> mv1)
        {
            return mv1 is IGasVectorTerm<T> vt1
                ? ENorm(vt1)
                : ENormAsScalar(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ENormSquared<T>(this IGasVector<T> mv1)
        {
            return mv1 is IGasVectorTerm<T> vt1
                ? ENormSquared(vt1)
                : ENormSquaredAsScalar(mv1);
        }

        private static T ENormAsScalar<T>(this IGasKVector<T> mv1)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            var grade = mv1.Grade;
            var spScalar = scalarProcessor.ZeroScalar;
            
            var indexScalarPairs1 = 
                mv1.GetIndexScalarPairs();

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                var id = 
                    GaBasisUtils.BasisBladeId(grade, index);

                var signature = 
                    GaBasisUtils.ENormSquaredSignature(id);

                //if (signature == 0) 
                //    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = signature > 0
                    ? scalarProcessor.Add(spScalar, scalar)
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return scalarProcessor.SqrtOfAbs(spScalar);
        }

        private static T ENormSquaredAsScalar<T>(this IGasKVector<T> mv1)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            var grade = mv1.Grade;
            var spScalar = scalarProcessor.ZeroScalar;
            
            var indexScalarPairs1 = 
                mv1.GetIndexScalarPairs();

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                var id = 
                    GaBasisUtils.BasisBladeId(grade, index);

                var signature = 
                    GaBasisUtils.ENormSquaredSignature(id);

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
        public static T ENormSquared<T>(this IGasKVector<T> mv1)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 => ENormSquared(s1),
                IGasVectorTerm<T> vt1 => ENormSquared(vt1),
                IGasKVectorTerm<T> kvt1 => ENormSquared(kvt1),
                IGasVector<T> v1 => ENormSquaredAsScalar(v1),
                _ => ENormSquaredAsScalar(mv1)
            };
        }

        private static T ENormAsScalar<T>(this IGasMultivector<T> mv1)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            var spScalar = scalarProcessor.ZeroScalar;

            var idScalarDictionary1 = mv1.GetIdScalarDictionary();

            foreach (var (id, scalar1) in idScalarDictionary1)
            {
                var signature = 
                    GaBasisUtils.ENormSquaredSignature(id);

                //if (signature == 0) 
                //    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = signature > 0
                    ? scalarProcessor.Add(spScalar, scalar)
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return scalarProcessor.SqrtOfAbs(spScalar);
        }

        private static T ENormSquaredAsScalar<T>(this IGasMultivector<T> mv1)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            var spScalar = scalarProcessor.ZeroScalar;

            var idScalarDictionary1 = mv1.GetIdScalarDictionary();

            foreach (var (id, scalar1) in idScalarDictionary1)
            {
                var signature = 
                    GaBasisUtils.ENormSquaredSignature(id);

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
        public static T ENorm<T>(this IGasMultivector<T> mv1)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 => ENorm(s1),
                IGasVectorTerm<T> vt1 => ENorm(vt1),
                IGasKVectorTerm<T> kvt1 => ENorm(kvt1),
                IGasVector<T> v1 => ENormAsScalar(v1),
                IGasKVector<T> kv1 => ENormAsScalar(kv1),
                _ => ENormAsScalar(mv1)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ENormSquared<T>(this IGasMultivector<T> mv1)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 => ENormSquared(s1),
                IGasVectorTerm<T> vt1 => ENormSquared(vt1),
                IGasKVectorTerm<T> kvt1 => ENormSquared(kvt1),
                IGasVector<T> v1 => ENormSquaredAsScalar(v1),
                IGasKVector<T> kv1 => ENormSquaredAsScalar(kv1),
                _ => ENormSquaredAsScalar(mv1)
            };
        }
        
    }
}