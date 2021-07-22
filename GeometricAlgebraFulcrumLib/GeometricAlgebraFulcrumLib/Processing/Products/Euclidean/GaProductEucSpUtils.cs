using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Products.Euclidean
{
    public static class GaProductEucSpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IGasScalar<T> mv1)
        {
            return mv1.ScalarProcessor.Times(mv1.Scalar, mv1.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IGasVectorTerm<T> mv1)
        {
            var scalar = 
                mv1.ScalarProcessor.Times(mv1.Scalar, mv1.Scalar);

            return GaBasisUtils.IsPositiveEGp(1UL << (int) mv1.Index)
                ? scalar
                : mv1.ScalarProcessor.Negative(scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T ESpAsScalar<T>(this IGasKVectorTerm<T> mv1)
        {
            var signature = 
                GaBasisUtils.ESpSignature(mv1.Id);

            return signature switch
            {
                //0 => mv1.ScalarProcessor.ZeroScalar,
                1 => mv1.ScalarProcessor.Times(mv1.Scalar, mv1.Scalar),
                _ => mv1.ScalarProcessor.NegativeTimes(mv1.Scalar, mv1.Scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IGasKVectorTerm<T> mv1)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 => ESp(s1),
                IGasVectorTerm<T> vt1 => ESp(vt1),
                _ => ESpAsScalar(mv1)
            };
        }
        
        public static T VectorsESp<T>(this IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector1)
        {
            var lcpScalar = scalarProcessor.ZeroScalar;

            var count = vector1.Count;

            for (var index = 0; index < count; index++)
            {
                var id = 1UL << index;
                var scalar1 = vector1[index];

                var scalar = scalarProcessor.Times(scalar1, scalar1);

                lcpScalar = GaBasisUtils.IsPositiveEGp(id) 
                    ? scalarProcessor.Add(lcpScalar, scalar) 
                    : scalarProcessor.Subtract(lcpScalar, scalar);
            }

            return lcpScalar;
        }
        
        private static T ESpAsScalar<T>(this IGasVector<T> mv1)
        {
            var scalarProcessor = mv1.ScalarProcessor;

            var indexScalarPairs1 = 
                mv1.GetIndexScalarPairs();

            var spScalar = scalarProcessor.ZeroScalar;

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                var id = 1UL << (int) index;
                var scalar = scalarProcessor.Times(scalar1, scalar1);

                spScalar = GaBasisUtils.IsPositiveEGp(id) 
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        public static T ESp<T>(this IGasVector<T> mv1)
        {
            return mv1 is IGasVectorTerm<T> vt1
                ? ESp(vt1)
                : ESpAsScalar(mv1);
        }

        private static T ESpAsScalar<T>(this IGasKVector<T> mv1)
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
                    GaBasisUtils.EGpSignature(id);

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
        public static T ESp<T>(this IGasKVector<T> mv1)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 => ESp(s1),
                IGasVectorTerm<T> vt1 => ESp(vt1),
                IGasKVectorTerm<T> kvt1 => ESp(kvt1),
                IGasVector<T> v1 => ESpAsScalar(v1),
                _ => ESpAsScalar(mv1)
            };
        }

        private static T ESpAsScalar<T>(this IGasMultivector<T> mv1)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            var spScalar = scalarProcessor.ZeroScalar;

            var idScalarDictionary1 = mv1.GetIdScalarDictionary();

            foreach (var (id, scalar1) in idScalarDictionary1)
            {
                var signature = 
                    GaBasisUtils.EGpSignature(id);

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
        public static T ESp<T>(this IGasMultivector<T> mv1)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 => ESp(s1),
                IGasVectorTerm<T> vt1 => ESp(vt1),
                IGasKVectorTerm<T> kvt1 => ESp(kvt1),
                IGasVector<T> v1 => ESpAsScalar(v1),
                IGasKVector<T> kv1 => ESpAsScalar(kv1),
                _ => ESpAsScalar(mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBilinearProductResult ESp(this IGaSignature signature, ulong id1, ulong id2)
        {
            return new GaBasisBilinearProductResult(
                GaBasisUtils.ESpSignature(id1, id2), 
                id1 ^ id2
            );
        }

        public static double ESp(this GaSignatureLookup basisSignature, IGasMultivector<double> mv1, IGasMultivector<double> mv2)
        {
            if (!basisSignature.IsEuclidean)
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

                var scalar = scalar1 * scalar2;

                spScalar = GaBasisUtils.IsPositiveEGp(id1)
                    ? spScalar + scalar
                    : spScalar - scalar;
            }

            return spScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IGasScalar<T> mv1, IGasScalar<T> mv2)
        {
            return mv1.ScalarProcessor.Times(mv1.Scalar, mv2.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IGasVectorTerm<T> mv1, IGasVectorTerm<T> mv2)
        {
            if (mv1.Index != mv2.Index)
                return mv1.ScalarProcessor.ZeroScalar;

            var scalar = 
                mv1.ScalarProcessor.Times(mv1.Scalar, mv2.Scalar);

            return GaBasisUtils.IsPositiveEGp(1UL << (int) mv1.Index)
                ? scalar
                : mv1.ScalarProcessor.Negative(scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T ESpAsScalar<T>(this IGasKVectorTerm<T> mv1, IGasKVectorTerm<T> mv2)
        {
            var signature = 
                GaBasisUtils.ESpSignature(mv1.Id, mv2.Id);

            return signature switch
            {
                0 => mv1.ScalarProcessor.ZeroScalar,
                1 => mv1.ScalarProcessor.Times(mv1.Scalar, mv2.Scalar),
                _ => mv1.ScalarProcessor.NegativeTimes(mv1.Scalar, mv2.Scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IGasKVectorTerm<T> mv1, IGasKVectorTerm<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => 
                    ESp(s1, s2),

                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => 
                    ESp(vt1, vt2),

                _ => 
                    ESpAsScalar(mv1, mv2)
            };
        }
        
        public static T VectorsESp<T>(this IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector1, IReadOnlyList<T> vector2)
        {
            var lcpScalar = scalarProcessor.ZeroScalar;

            var count = Math.Min(vector1.Count, vector2.Count);

            for (var index = 0; index < count; index++)
            {
                var id = 1UL << index;
                var scalar1 = vector1[index];
                var scalar2 = vector2[index];

                var scalar = scalarProcessor.Times(scalar1, scalar2);

                lcpScalar = GaBasisUtils.IsPositiveEGp(id) 
                    ? scalarProcessor.Add(lcpScalar, scalar) 
                    : scalarProcessor.Subtract(lcpScalar, scalar);
            }

            return lcpScalar;
        }
        
        private static T ESpAsScalar<T>(this IGasVector<T> mv1, IGasVector<T> mv2)
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

                var id = 1UL << (int) index;
                var scalar = scalarProcessor.Times(scalar1, scalar2);

                spScalar = GaBasisUtils.IsPositiveEGp(id) 
                    ? scalarProcessor.Add(spScalar, scalar) 
                    : scalarProcessor.Subtract(spScalar, scalar);
            }

            return spScalar;
        }

        public static T ESp<T>(this IGasVector<T> mv1, IGasVector<T> mv2)
        {
            return mv1 is IGasVectorTerm<T> vt1 && mv2 is IGasVectorTerm<T> vt2
                ? ESp(vt1, vt2)
                : ESpAsScalar(mv1, mv2);
        }

        private static T ESpAsScalar<T>(this IGasKVector<T> mv1, IGasKVector<T> mv2)
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

                var id = 
                    GaBasisUtils.BasisBladeId(grade, index);

                var signature = 
                    GaBasisUtils.EGpSignature(id);

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
        public static T ESp<T>(this IGasKVector<T> mv1, IGasKVector<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => ESp(s1, s2),
                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => ESp(vt1, vt2),
                IGasKVectorTerm<T> kvt1 when mv2 is IGasKVectorTerm<T> kvt2 => ESp(kvt1, kvt2),
                IGasVector<T> v1 when mv2 is IGasVector<T> v2 => ESpAsScalar(v1, v2),
                _ => ESpAsScalar(mv1, mv2)
            };
        }

        private static T ESpAsScalar<T>(this IGasGradedMultivector<T> mv1, IGasGradedMultivector<T> mv2)
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

                    var signature = 
                        GaBasisUtils.EGpSignature(id);

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
        public static T ESp<T>(this IGasGradedMultivector<T> mv1, IGasGradedMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => ESp(s1, s2),
                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => ESp(vt1, vt2),
                IGasKVectorTerm<T> kvt1 when mv2 is IGasKVectorTerm<T> kvt2 => ESp(kvt1, kvt2),
                IGasVector<T> v1 when mv2 is IGasVector<T> v2 => ESpAsScalar(v1, v2),
                IGasKVector<T> kv1 when mv2 is IGasKVector<T> kv2 => ESpAsScalar(kv1, kv2),
                _ => ESpAsScalar(mv1, mv2)
            };
        }

        private static T ESpAsScalar<T>(IGasMultivector<T> mv1, IGasMultivector<T> mv2)
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
                
                var signature = 
                    GaBasisUtils.ESpSignature(id);

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
        public static T ESp<T>(this IGasTermsMultivector<T> mv1, IGasTermsMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => ESp(s1, s2),
                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => ESp(vt1, vt2),
                IGasKVectorTerm<T> kvt1 when mv2 is IGasKVectorTerm<T> kvt2 => ESp(kvt1, kvt2),
                _ => ESpAsScalar(mv1, mv2)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ESp<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => ESp(s1, s2),
                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => ESp(vt1, vt2),
                IGasKVectorTerm<T> kvt1 when mv2 is IGasKVectorTerm<T> kvt2 => ESp(kvt1, kvt2),
                IGasVector<T> v1 when mv2 is IGasVector<T> v2 => ESpAsScalar(v1, v2),
                IGasKVector<T> kv1 when mv2 is IGasKVector<T> kv2 => ESpAsScalar(kv1, kv2),
                IGasGradedMultivector<T> gmv1 when mv2 is IGasGradedMultivector<T> gmv2 => ESpAsScalar(gmv1, gmv2),
                _ => ESpAsScalar(mv1, mv2)
            };
        }
    }
}