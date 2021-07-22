using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Processing.Products.Orthonormal
{
    public static class GaProductOrtUtils
    {
        internal static IGasMultivector<T> BilinearProduct<T>(this IGasMultivector<T> mv1, Func<ulong, ulong, int> basisSignatureFunction)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            var composer = 
                new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            var idScalarPairs = 
                mv1.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs)
            {
                foreach (var (id2, scalar2) in idScalarPairs)
                {
                    var signature = 
                        basisSignatureFunction(id1, id2);

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

            return composer.GetCompactMultivector();
        }

        internal static IGasMultivector<T> BilinearProduct<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2, Func<ulong, ulong, int> basisSignatureFunction)
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
                        basisSignatureFunction(id1, id2);

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

            return composer.GetCompactMultivector();
        }

        internal static IGasGradedMultivector<T> BilinearProduct<T>(this IGasKVector<T> mv1, Func<ulong, ulong, int> basisSignatureFunction)
        {
            var grade1 = mv1.Grade;

            var scalarProcessor = 
                mv1.ScalarProcessor;

            var composer = 
                new GaMultivectorGradedStorageComposer<T>(scalarProcessor);

            var indexScalarPairs1 = 
                mv1.GetIndexScalarDictionary();

            foreach (var (index1, scalar1) in indexScalarPairs1)
            {
                var id1 = GaBasisUtils.BasisBladeId(grade1, index1);

                foreach (var (index2, scalar2) in indexScalarPairs1)
                {
                    var id2 = GaBasisUtils.BasisBladeId(grade1, index2);

                    var signature = 
                        basisSignatureFunction(id1, id2);

                    if (signature == 0) 
                        continue;

                    var (grade, index) = (id1 ^ id2).BasisBladeGradeIndex();
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(grade, index, scalar);
                    else
                        composer.SubtractTerm(grade, index, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.GetCompactGradedMultivector();
        }

        internal static IGasGradedMultivector<T> BilinearProduct<T>(this IGasKVector<T> mv1, IGasKVector<T> mv2, Func<ulong, ulong, int> basisSignatureFunction)
        {
            var grade1 = mv1.Grade;
            var grade2 = mv2.Grade;

            var scalarProcessor = 
                mv1.ScalarProcessor;

            var composer = 
                new GaMultivectorGradedStorageComposer<T>(scalarProcessor);

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
                        basisSignatureFunction(id1, id2);

                    if (signature == 0) 
                        continue;

                    var (grade, index) = (id1 ^ id2).BasisBladeGradeIndex();
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(grade, index, scalar);
                    else
                        composer.SubtractTerm(grade, index, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.GetCompactGradedMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<double> Dual(this GaSignatureLookup basisSignature, IGasMultivector<double> mv1)
        {
            var pseudoScalarInverse =
                basisSignature.CreatePseudoScalarInverse(mv1.ScalarProcessor);

            return basisSignature.Lcp(mv1, pseudoScalarInverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Dual<T>(this IGaSignature basisSignature, IGasMultivector<T> mv1)
        {
            var pseudoScalarInverse =
                basisSignature.CreatePseudoScalarInverse(mv1.ScalarProcessor);

            return basisSignature.Lcp(mv1, pseudoScalarInverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Dual<T>(this IGasMultivector<T> mv1, IGaSignature basisSignature)
        {
            var pseudoScalarInverse =
                basisSignature.CreatePseudoScalarInverse(mv1.ScalarProcessor);

            return basisSignature.Lcp(mv1, pseudoScalarInverse);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<double> UnDual(this GaSignatureLookup basisSignature, IGasMultivector<double> mv1)
        {
            var pseudoScalarReverse =
                mv1.ScalarProcessor.CreatePseudoScalarReverse(basisSignature.VSpaceDimension);

            return basisSignature.Lcp(mv1, pseudoScalarReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> UnDual<T>(this IGaSignature basisSignature, IGasMultivector<T> mv1)
        {
            var pseudoScalarReverse =
                mv1.ScalarProcessor.CreatePseudoScalarReverse(basisSignature.VSpaceDimension);

            return basisSignature.Lcp(mv1, pseudoScalarReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> UnDual<T>(this IGasMultivector<T> mv1, IGaSignature basisSignature)
        {
            var pseudoScalarReverse =
                mv1.ScalarProcessor.CreatePseudoScalarReverse(basisSignature.VSpaceDimension);

            return basisSignature.Lcp(mv1, pseudoScalarReverse);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<double> BladeInverse(this GaSignatureLookup basisSignature, IGasMultivector<double> mv1)
        {
            var bladeSpSquared = basisSignature.Sp(mv1);

            return mv1.Divide(bladeSpSquared);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVector<T> BladeInverse<T>(this IGaSignature basisSignature, IGasKVector<T> kVector)
        {
            var bladeSpSquared = basisSignature.Sp(kVector);

            return kVector.Divide(bladeSpSquared).GetKVectorPart(kVector.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> BladeInverse<T>(this IGasMultivector<T> mv1, IGaSignature basisSignature)
        {
            var bladeSpSquared = basisSignature.Sp(mv1);

            return mv1.Divide(bladeSpSquared);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<double> VersorInverse(this GaSignatureLookup basisSignature, IGasMultivector<double> mv1)
        {
            var versorSpReverse = basisSignature.NormSquared(mv1);

            return mv1.GetReverse().Divide(versorSpReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> VersorInverse<T>(this IGaSignature basisSignature, IGasMultivector<T> mv1)
        {
            var versorSpReverse = basisSignature.NormSquared(mv1);

            return mv1.GetReverse().Divide(versorSpReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> VersorInverse<T>(this IGasMultivector<T> mv1, IGaSignature basisSignature)
        {
            var versorSpReverse = basisSignature.NormSquared(mv1);

            return mv1.GetReverse().Divide(versorSpReverse);
        }
    }
}