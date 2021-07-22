using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Processing.Products.Euclidean
{
    public static class GaProductEucRcpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> ERcp<T>(this IGasScalar<T> mv1, IGasScalar<T> mv2)
        {
            return mv1.ScalarProcessor.CreateScalar(
                mv1.ScalarProcessor.Times(mv1.Scalar, mv2.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> ERcp<T>(this IGasVectorTerm<T> mv1, IGasVectorTerm<T> mv2)
        {
            if (mv1.Index != mv2.Index)
                return mv1.ScalarProcessor.CreateZeroScalar();

            var scalar = 
                mv1.ScalarProcessor.Times(mv1.Scalar, mv2.Scalar);

            return mv1.ScalarProcessor.CreateScalar(
                GaBasisUtils.IsPositiveEGp(1UL << (int) mv1.Index)
                    ? scalar
                    : mv1.ScalarProcessor.Negative(scalar)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGasKVectorTerm<T> ERcpAsKVectorTerm<T>(this IGasKVectorTerm<T> mv1, IGasKVectorTerm<T> mv2)
        {
            var scalarProcessor = mv1.ScalarProcessor;

            if (mv2.Grade > mv1.Grade)
                return scalarProcessor.CreateZeroScalar();

            var signature = 
                GaBasisUtils.ERcpSignature(mv1.Id, mv2.Id);

            return signature switch
            {
                0 => scalarProcessor.CreateZeroScalar(),
                1 => scalarProcessor.CreateKVector(
                        mv1.Id ^ mv2.Id,
                        scalarProcessor.Times(mv1.Scalar, mv2.Scalar)
                    ),
                _ => scalarProcessor.CreateKVector(
                        mv1.Id ^ mv2.Id,
                        scalarProcessor.NegativeTimes(mv1.Scalar, mv2.Scalar)
                    )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVectorTerm<T> ERcp<T>(this IGasKVectorTerm<T> mv1, IGasKVectorTerm<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => 
                    ERcp(s1, s2),

                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => 
                    ERcp(vt1, vt2),

                _ => 
                    ERcpAsKVectorTerm(mv1, mv2)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> VectorsERcp<T>(this IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector1, IReadOnlyList<T> vector2)
        {
            return scalarProcessor.CreateScalar(
                scalarProcessor.VectorsESp(vector1, vector2)
            );
        }
        
        private static IGasScalar<T> ERcpAsScalar<T>(this IGasVector<T> mv1, IGasVector<T> mv2)
        {
            var scalarProcessor = mv1.ScalarProcessor;

            var indexScalarPairs1 = 
                mv1.GetIndexScalarPairs();

            var indexScalarPairs2 = 
                mv2.GetIndexScalarDictionary();

            var lcpScalar = scalarProcessor.ZeroScalar;

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                if (!indexScalarPairs2.TryGetValue(index, out var scalar2))
                    continue;

                var id = 1UL << (int) index;
                var scalar = scalarProcessor.Times(scalar1, scalar2);

                lcpScalar = GaBasisUtils.IsPositiveEGp(id) 
                    ? scalarProcessor.Add(lcpScalar, scalar) 
                    : scalarProcessor.Subtract(lcpScalar, scalar);
            }

            return scalarProcessor.CreateScalar(lcpScalar);
        }

        public static IGasScalar<T> ERcp<T>(this IGasVector<T> mv1, IGasVector<T> mv2)
        {
            return mv1 is IGasVectorTerm<T> vt1 && mv2 is IGasVectorTerm<T> vt2
                ? ERcp(vt1, vt2)
                : ERcpAsScalar(mv1, mv2);
        }

        private static IGasKVector<T> ERcpAsKVector<T>(this IGasKVector<T> mv1, IGasKVector<T> mv2)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            if (mv2.Grade > mv1.Grade)
                return scalarProcessor.CreateZeroScalar();

            if (mv2.Grade == mv1.Grade)
                return scalarProcessor.CreateScalar(mv1.ESp(mv2));

            var grade1 = mv1.Grade;
            var grade2 = mv2.Grade;
            var grade = grade1 - grade2;

            var composer = 
                new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            var indexScalarPairs1 = 
                mv1.GetIndexScalarPairs();

            var indexScalarPairs2 = 
                mv2.GetIndexScalarDictionary();

            foreach (var (index1, scalar1) in indexScalarPairs1)
            {
                var id1 = GaBasisUtils.BasisBladeId(grade1, index1);

                foreach (var (index2, scalar2) in indexScalarPairs2)
                {
                    var id2 = GaBasisUtils.BasisBladeId(grade2, index2);

                    var signature = 
                        GaBasisUtils.ERcpSignature(id1, id2);

                    if (signature == 0) 
                        continue;

                    var index = (id1 ^ id2).BasisBladeIndex();
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(index, scalar);
                    else
                        composer.SubtractTerm(index, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.GetKVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVector<T> ERcp<T>(this IGasKVector<T> mv1, IGasKVector<T> mv2)
        {
            if (mv2.Grade > mv1.Grade)
                return mv1.ScalarProcessor.CreateZeroScalar();

            if (mv1.Grade == mv2.Grade)
                return mv1.ScalarProcessor.CreateScalar(mv1.ESp(mv2));

            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => 
                    ERcp(s1, s2),

                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => 
                    ERcp(vt1, vt2),

                IGasKVectorTerm<T> kvt1 when mv2 is IGasKVectorTerm<T> kvt2 => 
                    ERcpAsKVectorTerm(kvt1, kvt2),

                IGasVector<T> v1 when mv2 is IGasVector<T> v2 => 
                    ERcpAsScalar(v1, v2),

                _ => 
                    ERcpAsKVector(mv1, mv2)
            };
        }

        private static IGasGradedMultivector<T> ERcpAsGaMultivectorGraded<T>(this IGasGradedMultivector<T> mv1, IGasGradedMultivector<T> mv2)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            var composer = 
                new GaMultivectorGradedStorageComposer<T>(scalarProcessor);

            var gradeIndexScalarDictionary1 = mv1.GetGradeIndexScalarDictionary();
            var gradeIndexScalarDictionary2 = mv2.GetGradeIndexScalarDictionary();

            foreach (var (grade1, indexScalarPairs1) in gradeIndexScalarDictionary1)
            {
                foreach (var (grade2, indexScalarPairs2) in gradeIndexScalarDictionary2)
                {
                    if (grade2 > grade1)
                        continue;

                    if (grade2 == grade1)
                    {
                        foreach (var (index, scalar1) in indexScalarPairs1)
                        {
                            var id = GaBasisUtils.BasisBladeId(grade1, index);

                            if (!indexScalarPairs2.TryGetValue(index, out var scalar2))
                                continue;
                            
                            var signature = 
                                GaBasisUtils.EGpSignature(id);

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

                    var grade = grade1 - grade2;

                    foreach (var (index1, scalar1) in indexScalarPairs1)
                    {
                        var id1 = GaBasisUtils.BasisBladeId(grade1, index1);

                        foreach (var (index2, scalar2) in indexScalarPairs2)
                        {
                            var id2 = GaBasisUtils.BasisBladeId(grade2, index2);

                            var signature = 
                                GaBasisUtils.ERcpSignature(id1, id2);

                            if (signature == 0) 
                                continue;

                            var index = (id1 ^ id2).BasisBladeIndex();
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

            return composer.GetCompactGradedMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasGradedMultivector<T> ERcp<T>(this IGasGradedMultivector<T> mv1, IGasGradedMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => 
                    ERcp(s1, s2),

                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => 
                    ERcp(vt1, vt2),

                IGasKVectorTerm<T> kvt1 when mv2 is IGasKVectorTerm<T> kvt2 => 
                    ERcpAsKVectorTerm(kvt1, kvt2),

                IGasVector<T> v1 when mv2 is IGasVector<T> v2 => 
                    ERcp(v1, v2),

                IGasKVector<T> kv1 when mv2 is IGasKVector<T> kv2 => 
                    ERcpAsKVector(kv1, kv2),

                _ => 
                    ERcpAsGaMultivectorGraded(mv1, mv2)
            };
        }

        private static IGasTermsMultivector<T> ERcpAsTermsMultivector<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            var composer = 
                new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                foreach (var (id2, scalar2) in idScalarPairs2)
                {
                    var signature = 
                        GaBasisUtils.ERcpSignature(id1, id2);

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

            return composer.GetCompactTermsStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasTermsMultivector<T> ERcp<T>(this IGasTermsMultivector<T> mv1, IGasTermsMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => 
                    ERcp(s1, s2),

                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => 
                    ERcp(vt1, vt2),

                IGasKVectorTerm<T> kvt1 when mv2 is IGasKVectorTerm<T> kvt2 => 
                    ERcpAsKVectorTerm(kvt1, kvt2),

                _ => 
                    ERcpAsTermsMultivector(mv1, mv2)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> ERcp<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => 
                    ERcp(s1, s2),

                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => 
                    ERcp(vt1, vt2),

                IGasKVectorTerm<T> kvt1 when mv2 is IGasKVectorTerm<T> kvt2 => 
                    ERcpAsKVectorTerm(kvt1, kvt2),

                IGasVector<T> v1 when mv2 is IGasVector<T> v2 => 
                    ERcpAsScalar(v1, v2),

                IGasKVector<T> kv1 when mv2 is IGasKVector<T> kv2 => 
                    ERcpAsKVector(kv1, kv2),

                IGasGradedMultivector<T> gmv1 when mv2 is IGasGradedMultivector<T> gmv2 => 
                    ERcpAsGaMultivectorGraded(gmv1, gmv2),

                _ =>
                    ERcpAsTermsMultivector(mv1, mv2)
            };
        }
    }
}