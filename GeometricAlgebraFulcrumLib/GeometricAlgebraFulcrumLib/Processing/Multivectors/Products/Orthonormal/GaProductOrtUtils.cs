using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Processing.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal
{
    public static class GaProductOrtUtils
    {
        internal static IGaStorageMultivector<T> BilinearProduct<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, Func<ulong, ulong, int> basisSignatureFunction)
        {
            var composer = 
                scalarProcessor.CreateStorageSparseMultivectorComposer();

            var idScalarPairs = 
                mv1.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs.GetKeyValueRecords())
            {
                foreach (var (id2, scalar2) in idScalarPairs.GetKeyValueRecords())
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

            return composer.CreateStorageSparseMultivector();
        }

        internal static IGaStorageMultivector<T> BilinearProduct<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2, Func<ulong, ulong, int> basisSignatureFunction)
        {
            var composer = 
                scalarProcessor.CreateStorageSparseMultivectorComposer();

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                foreach (var (id2, scalar2) in idScalarPairs2.GetKeyValueRecords())
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

            return composer.CreateStorageSparseMultivector();
        }

        internal static IGaStorageMultivectorGraded<T> BilinearProduct<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv1, Func<ulong, ulong, int> basisSignatureFunction)
        {
            var grade1 = mv1.Grade;

            var composer = 
                scalarProcessor.CreateStorageGradedMultivectorComposer();

            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            foreach (var (index1, scalar1) in indexScalarPairs1.GetKeyValueRecords())
            {
                var id1 = index1.BasisBladeIndexToId(grade1);

                foreach (var (index2, scalar2) in indexScalarPairs1.GetKeyValueRecords())
                {
                    var id2 = index2.BasisBladeIndexToId(grade1);

                    var signature = 
                        basisSignatureFunction(id1, id2);

                    if (signature == 0) 
                        continue;

                    var (grade, index) = (id1 ^ id2).BasisBladeIdToGradeIndex();
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(grade, index, scalar);
                    else
                        composer.SubtractTerm(grade, index, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateStorageGradedMultivector();
        }

        internal static IGaStorageMultivectorGraded<T> BilinearProduct<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv1, IGaStorageKVector<T> mv2, Func<ulong, ulong, int> basisSignatureFunction)
        {
            var grade1 = mv1.Grade;
            var grade2 = mv2.Grade;

            var composer = 
                scalarProcessor.CreateStorageGradedMultivectorComposer();

            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            var indexScalarPairs2 = 
                mv2.IndexScalarList;

            foreach (var (index1, scalar1) in indexScalarPairs1.GetKeyValueRecords())
            {
                var id1 = index1.BasisBladeIndexToId(grade1);

                foreach (var (index2, scalar2) in indexScalarPairs2.GetKeyValueRecords())
                {
                    var id2 = index2.BasisBladeIndexToId(grade2);

                    var signature = 
                        basisSignatureFunction(id1, id2);

                    if (signature == 0) 
                        continue;

                    var (grade, index) = (id1 ^ id2).BasisBladeIdToGradeIndex();
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(grade, index, scalar);
                    else
                        composer.SubtractTerm(grade, index, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateStorageGradedMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<double> Dual(this GaSignatureLookup basisSignature, IGaStorageMultivector<double> mv1)
        {
            var scalarProcessor = GaScalarProcessorFloat64.DefaultProcessor;

            var pseudoScalarInverse =
                scalarProcessor.CreateStoragePseudoScalarInverse(basisSignature);
            
            return scalarProcessor.Lcp(basisSignature, mv1, pseudoScalarInverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Dual<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaStorageMultivector<T> mv1)
        {
            var pseudoScalarInverse =
                scalarProcessor.CreateStoragePseudoScalarInverse(basisSignature);

            return scalarProcessor.Lcp(basisSignature, mv1, pseudoScalarInverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Dual<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaSignature basisSignature)
        {
            var pseudoScalarInverse =
                scalarProcessor.CreateStoragePseudoScalarInverse(basisSignature);

            return scalarProcessor.Lcp(basisSignature, mv1, pseudoScalarInverse);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<double> UnDual(this GaSignatureLookup basisSignature, IGaStorageMultivector<double> mv1)
        {
            var pseudoScalarReverse =
                GaScalarProcessorFloat64.DefaultProcessor.CreateStoragePseudoScalarReverse(basisSignature.VSpaceDimension);

            return GaScalarProcessorFloat64.DefaultProcessor.Lcp(basisSignature, mv1, pseudoScalarReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> UnDual<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaStorageMultivector<T> mv1)
        {
            var pseudoScalarReverse =
                scalarProcessor.CreateStoragePseudoScalarReverse(basisSignature.VSpaceDimension);

            return scalarProcessor.Lcp(basisSignature, mv1, pseudoScalarReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> UnDual<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaSignature basisSignature)
        {
            var pseudoScalarReverse =
                scalarProcessor.CreateStoragePseudoScalarReverse(basisSignature.VSpaceDimension);

            return scalarProcessor.Lcp(basisSignature, mv1, pseudoScalarReverse);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<double> BladeInverse(this GaSignatureLookup basisSignature, IGaStorageMultivector<double> mv1)
        {
            var bladeSpSquared = basisSignature.Sp(mv1);

            return GaScalarProcessorFloat64.DefaultProcessor.Divide(mv1, bladeSpSquared);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> BladeInverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaStorageKVector<T> kVector)
        {
            var bladeSpSquared = scalarProcessor.Sp(basisSignature, kVector);

            return scalarProcessor.Divide(kVector, bladeSpSquared).GetKVectorPart(kVector.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> BladeInverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaSignature basisSignature)
        {
            var bladeSpSquared = scalarProcessor.Sp(basisSignature, mv1);

            return scalarProcessor.Divide(mv1, bladeSpSquared);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<double> VersorInverse(this GaSignatureLookup basisSignature, IGaStorageMultivector<double> mv1)
        {
            var versorSpReverse = basisSignature.NormSquared(mv1);

            return GaScalarProcessorFloat64.DefaultProcessor.Divide(GaScalarProcessorFloat64.DefaultProcessor.Reverse(mv1), versorSpReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> VersorInverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaStorageMultivector<T> mv1)
        {
            var versorSpReverse = scalarProcessor.NormSquared(basisSignature, mv1);

            return scalarProcessor.Divide(scalarProcessor.Reverse(mv1), versorSpReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> VersorInverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaSignature basisSignature)
        {
            var versorSpReverse = scalarProcessor.NormSquared(basisSignature, mv1);

            return scalarProcessor.Divide(scalarProcessor.Reverse(mv1), versorSpReverse);
        }
    }
}