using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;
using GeometricAlgebraFulcrumLib.Storage.Factories;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal
{
    public static class GaProductOrtRcpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Rcp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageScalar<T> mv1, IGaStorageScalar<T> mv2)
        {
            return scalarProcessor.CreateStorageScalar(
                scalarProcessor.Times(
                    mv1.GetScalar(scalarProcessor), 
                    mv2.GetScalar(scalarProcessor)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> VectorsRcp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IReadOnlyList<T> vector1, IReadOnlyList<T> vector2)
        {
            return scalarProcessor.CreateStorageScalar(
                scalarProcessor.VectorsESp(vector1, vector2)
            );
        }
        
        private static IGaStorageScalar<T> RcpAsScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageVector<T> mv1, IGaStorageVector<T> mv2)
        {
            var indexScalarPairs1 = 
                mv1.IndexScalarDictionary;

            var indexScalarPairs2 = 
                mv2.IndexScalarDictionary;

            var lcpScalar = scalarProcessor.ZeroScalar;

            foreach (var (index, scalar1) in indexScalarPairs1)
            {
                if (!indexScalarPairs2.TryGetValue(index, out var scalar2))
                    continue;

                var id = 1UL << (int) index;
                var sig = signature.SpSignature(id);

                if (sig == 0)
                    continue;

                var scalar = scalarProcessor.Times(scalar1, scalar2);

                lcpScalar = sig > 0 
                    ? scalarProcessor.Add(lcpScalar, scalar) 
                    : scalarProcessor.Subtract(lcpScalar, scalar);
            }

            return scalarProcessor.CreateStorageScalar(lcpScalar);
        }

        public static IGaStorageScalar<T> Rcp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageVector<T> mv1, IGaStorageVector<T> mv2)
        {
            return scalarProcessor.RcpAsScalar(signature, mv1, mv2);
        }

        private static IGaStorageKVector<T> RcpAsKVector<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageKVector<T> mv1, IGaStorageKVector<T> mv2)
        {
            if (mv2.Grade > mv1.Grade)
                return scalarProcessor.CreateStorageZeroScalar();

            if (mv2.Grade == mv1.Grade)
                return scalarProcessor.CreateStorageScalar(scalarProcessor.Sp(signature, mv1, mv2));

            var grade1 = mv1.Grade;
            var grade2 = mv2.Grade;
            var grade = grade1 - grade2;

            var composer = 
                new GaStorageComposerKVector<T>(scalarProcessor, grade);

            var indexScalarPairs1 = 
                mv1.IndexScalarDictionary;

            var indexScalarPairs2 = 
                mv2.IndexScalarDictionary;

            foreach (var (index1, scalar1) in indexScalarPairs1)
            {
                var id1 = signature.BasisBladeId(grade1, index1);

                foreach (var (index2, scalar2) in indexScalarPairs2)
                {
                    var id2 = signature.BasisBladeId(grade2, index2);

                    var sig = 
                        signature.RcpSignature(id1, id2);

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

            return composer.GetKVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Rcp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageKVector<T> mv1, IGaStorageKVector<T> mv2)
        {
            if (mv2.Grade > mv1.Grade)
                return scalarProcessor.CreateStorageZeroScalar();

            if (mv1.Grade == mv2.Grade)
                return scalarProcessor.CreateStorageScalar(scalarProcessor.Sp(signature, mv1, mv2));

            return mv1 switch
            {
                IGaStorageScalar<T> s1 when mv2 is IGaStorageScalar<T> s2 => 
                    Rcp(scalarProcessor, signature, s1, s2),

                IGaStorageVector<T> vt1 when mv2 is IGaStorageVector<T> vt2 => 
                    Rcp(scalarProcessor, signature, vt1, vt2),
                    
                _ => 
                    RcpAsKVector(scalarProcessor, signature, mv1, mv2)
            };
        }

        private static IGaStorageMultivectorGraded<T> RcpAsGaMultivectorGraded<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageMultivectorGraded<T> mv1, IGaStorageMultivectorGraded<T> mv2)
        {
            var composer = 
                new GaStorageComposerMultivectorGraded<T>(scalarProcessor);

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

                    var grade = grade1 - grade2;

                    foreach (var (index1, scalar1) in indexScalarPairs1)
                    {
                        var id1 = signature.BasisBladeId(grade1, index1);

                        foreach (var (index2, scalar2) in indexScalarPairs2)
                        {
                            var id2 = signature.BasisBladeId(grade2, index2);

                            var sig = 
                                signature.RcpSignature(id1, id2);

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

            return composer.GetGradedMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> Rcp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageMultivectorGraded<T> mv1, IGaStorageMultivectorGraded<T> mv2)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 when mv2 is IGaStorageScalar<T> s2 => 
                    Rcp(scalarProcessor, signature, s1, s2),

                IGaStorageVector<T> vt1 when mv2 is IGaStorageVector<T> vt2 => 
                    Rcp(scalarProcessor, signature, vt1, vt2),
                
                IGaStorageKVector<T> kv1 when mv2 is IGaStorageKVector<T> kv2 => 
                    RcpAsKVector(scalarProcessor, signature, kv1, kv2),

                _ => 
                    RcpAsGaMultivectorGraded(scalarProcessor, signature, mv1, mv2)
            };
        }

        private static IGaStorageMultivectorSparse<T> RcpAsTermsMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            var composer = 
                new GaStorageComposerMultivectorSparse<T>(scalarProcessor);

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                foreach (var (id2, scalar2) in idScalarPairs2)
                {
                    var sig = 
                        signature.RcpSignature(id1, id2);

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

            return composer.GetSparseMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorSparse<T> Rcp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageMultivectorSparse<T> mv1, IGaStorageMultivectorSparse<T> mv2)
        {
            return RcpAsTermsMultivector(scalarProcessor, signature, mv1, mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Rcp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature signature, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 when mv2 is IGaStorageScalar<T> s2 => 
                    Rcp(scalarProcessor, signature, s1, s2),

                IGaStorageVector<T> vt1 when mv2 is IGaStorageVector<T> vt2 => 
                    Rcp(scalarProcessor, signature, vt1, vt2),

                IGaStorageKVector<T> kvt1 when mv2 is IGaStorageKVector<T> kvt2 => 
                    Rcp(scalarProcessor, signature, kvt1, kvt2),

                IGaStorageMultivectorGraded<T> gmv1 when mv2 is IGaStorageMultivectorGraded<T> gmv2 => 
                    RcpAsGaMultivectorGraded(scalarProcessor, signature, gmv1, gmv2),

                _ =>
                    RcpAsTermsMultivector(scalarProcessor, signature, mv1, mv2)
            };
        }
    }
}