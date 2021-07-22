using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Processing.Products.Orthonormal
{
    public static class GaProductOrtLcpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Lcp<T>(this IGaSignature signature, IGasScalar<T> mv1, IGasScalar<T> mv2)
        {
            return mv1.ScalarProcessor.CreateScalar(
                mv1.ScalarProcessor.Times(mv1.Scalar, mv2.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Lcp<T>(this IGaSignature signature, IGasVectorTerm<T> mv1, IGasVectorTerm<T> mv2)
        {
            if (mv1.Index != mv2.Index)
                return mv1.ScalarProcessor.CreateZeroScalar();

            var sig = signature.LcpSignature(
                1UL << (int) mv1.Index, 
                1UL << (int) mv2.Index
            );

            if (sig == 0)
                return mv1.ScalarProcessor.CreateZeroScalar();

            var scalar = 
                mv1.ScalarProcessor.Times(mv1.Scalar, mv2.Scalar);

            return mv1.ScalarProcessor.CreateScalar(
                sig > 0
                    ? scalar
                    : mv1.ScalarProcessor.Negative(scalar)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGasKVectorTerm<T> LcpAsKVectorTerm<T>(this IGaSignature signature, IGasKVectorTerm<T> mv1, IGasKVectorTerm<T> mv2)
        {
            var scalarProcessor = mv1.ScalarProcessor;

            if (mv2.Grade < mv1.Grade)
                return scalarProcessor.CreateZeroScalar();

            var sig = 
                signature.LcpSignature(mv1.Id, mv2.Id);

            return sig switch
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
        public static IGasKVectorTerm<T> Lcp<T>(this IGaSignature signature, IGasKVectorTerm<T> mv1, IGasKVectorTerm<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => 
                    Lcp(signature, s1, s2),

                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => 
                    Lcp(signature, vt1, vt2),

                _ => 
                    LcpAsKVectorTerm(signature, mv1, mv2)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> VectorsLcp<T>(this IGaSignature signature, IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector1, IReadOnlyList<T> vector2)
        {
            return scalarProcessor.CreateScalar(
                scalarProcessor.VectorsESp(vector1, vector2)
            );
        }
        
        private static IGasScalar<T> LcpAsScalar<T>(this IGaSignature signature, IGasVector<T> mv1, IGasVector<T> mv2)
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
                var sig = signature.SpSignature(id);

                if (sig == 0)
                    continue;

                var scalar = mv1.ScalarProcessor.Times(scalar1, scalar2);

                lcpScalar = sig > 0 
                    ? scalarProcessor.Add(lcpScalar, scalar) 
                    : scalarProcessor.Subtract(lcpScalar, scalar);
            }

            return scalarProcessor.CreateScalar(lcpScalar);
        }

        public static IGasScalar<T> Lcp<T>(this IGaSignature signature, IGasVector<T> mv1, IGasVector<T> mv2)
        {
            return mv1 is IGasVectorTerm<T> vt1 && mv2 is IGasVectorTerm<T> vt2
                ? Lcp(signature, vt1, vt2)
                : LcpAsScalar(signature, mv1, mv2);
        }

        private static IGasKVector<T> LcpAsKVector<T>(this IGaSignature signature, IGasKVector<T> mv1, IGasKVector<T> mv2)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            if (mv2.Grade < mv1.Grade)
                return scalarProcessor.CreateZeroScalar();

            if (mv2.Grade == mv1.Grade)
                return scalarProcessor.CreateScalar(signature.Sp(mv1, mv2));

            var grade1 = mv1.Grade;
            var grade2 = mv2.Grade;
            var grade = grade2 - grade1;

            var composer = 
                new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            var indexScalarPairs1 = 
                mv1.GetIndexScalarPairs();

            var indexScalarPairs2 = 
                mv2.GetIndexScalarDictionary();

            foreach (var (index1, scalar1) in indexScalarPairs1)
            {
                var id1 = signature.BasisBladeId(grade1, index1);

                foreach (var (index2, scalar2) in indexScalarPairs2)
                {
                    var id2 = signature.BasisBladeId(grade2, index2);

                    var sig = 
                        signature.LcpSignature(id1, id2);

                    if (sig == 0) 
                        continue;

                    var index = (id1 ^ id2).BasisBladeIndex();
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (sig > 0)
                        composer.AddTerm(index, scalar);
                    else
                        composer.SubtractTerm(index, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.GetKVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVector<T> Lcp<T>(this IGaSignature signature, IGasKVector<T> mv1, IGasKVector<T> mv2)
        {
            if (mv2.Grade < mv1.Grade)
                return mv1.ScalarProcessor.CreateZeroScalar();

            if (mv1.Grade == mv2.Grade)
                return mv1.ScalarProcessor.CreateScalar(signature.Sp(mv1, mv2));

            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => 
                    Lcp(signature, s1, s2),

                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => 
                    Lcp(signature, vt1, vt2),

                IGasKVectorTerm<T> kvt1 when mv2 is IGasKVectorTerm<T> kvt2 => 
                    LcpAsKVectorTerm(signature, kvt1, kvt2),

                IGasVector<T> v1 when mv2 is IGasVector<T> v2 => 
                    LcpAsScalar(signature, v1, v2),

                _ => 
                    LcpAsKVector(signature, mv1, mv2)
            };
        }

        private static IGasGradedMultivector<T> LcpAsGaMultivectorGraded<T>(this IGaSignature signature, IGasGradedMultivector<T> mv1, IGasGradedMultivector<T> mv2)
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
                    if (grade2 < grade1) 
                        continue;

                    if (grade2 == grade1)
                    {
                        foreach (var (index, scalar1) in indexScalarPairs1)
                        {
                            var id = signature.BasisBladeId(grade1, index);

                            if (!indexScalarPairs2.TryGetValue(index, out var scalar2))
                                continue;
                            
                            var sig = signature.SpSignature(id);

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

                    foreach (var (index1, scalar1) in indexScalarPairs1)
                    {
                        var id1 = signature.BasisBladeId(grade1, index1);

                        foreach (var (index2, scalar2) in indexScalarPairs2)
                        {
                            var id2 = signature.BasisBladeId(grade2, index2);

                            var sig = 
                                signature.LcpSignature(id1, id2);

                            if (sig == 0) 
                                continue;

                            var index = (id1 ^ id2).BasisBladeIndex();
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

            return composer.GetCompactGradedMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasGradedMultivector<T> Lcp<T>(this IGaSignature signature, IGasGradedMultivector<T> mv1, IGasGradedMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => 
                    Lcp(signature, s1, s2),

                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => 
                    Lcp(signature, vt1, vt2),

                IGasKVectorTerm<T> kvt1 when mv2 is IGasKVectorTerm<T> kvt2 => 
                    LcpAsKVectorTerm(signature, kvt1, kvt2),

                IGasVector<T> v1 when mv2 is IGasVector<T> v2 => 
                    LcpAsScalar(signature, v1, v2),

                IGasKVector<T> kv1 when mv2 is IGasKVector<T> kv2 => 
                    LcpAsKVector(signature, kv1, kv2),

                _ => 
                    LcpAsGaMultivectorGraded(signature, mv1, mv2)
            };
        }

        private static IGasTermsMultivector<T> LcpAsTermsMultivector<T>(this IGaSignature signature, IGasMultivector<T> mv1, IGasMultivector<T> mv2)
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
                    var sig = 
                        signature.LcpSignature(id1, id2);

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

            return composer.GetCompactTermsStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasTermsMultivector<T> Lcp<T>(this IGaSignature signature, IGasTermsMultivector<T> mv1, IGasTermsMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => 
                    Lcp(signature, s1, s2),

                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => 
                    Lcp(signature, vt1, vt2),

                IGasKVectorTerm<T> kvt1 when mv2 is IGasKVectorTerm<T> kvt2 => 
                    LcpAsKVectorTerm(signature, kvt1, kvt2),

                _ => 
                    LcpAsTermsMultivector(signature, mv1, mv2)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Lcp<T>(this IGaSignature signature, IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => 
                    Lcp(signature, s1, s2),

                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => 
                    Lcp(signature, vt1, vt2),

                IGasKVectorTerm<T> kvt1 when mv2 is IGasKVectorTerm<T> kvt2 => 
                    LcpAsKVectorTerm(signature, kvt1, kvt2),

                IGasVector<T> v1 when mv2 is IGasVector<T> v2 => 
                    LcpAsScalar(signature, v1, v2),

                IGasKVector<T> kv1 when mv2 is IGasKVector<T> kv2 => 
                    LcpAsKVector(signature, kv1, kv2),

                IGasGradedMultivector<T> gmv1 when mv2 is IGasGradedMultivector<T> gmv2 => 
                    LcpAsGaMultivectorGraded(signature, gmv1, gmv2),

                _ =>
                    LcpAsTermsMultivector(signature, mv1, mv2)
            };
        }
    }
}