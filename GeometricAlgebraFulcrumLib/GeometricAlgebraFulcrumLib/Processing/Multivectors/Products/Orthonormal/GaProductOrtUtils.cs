using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal
{
    public static class GaProductOrtUtils
    {
        internal static IGaMultivectorStorage<T> BilinearProduct<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, Func<ulong, ulong, int> basisSignatureFunction)
        {
            var composer = 
                scalarProcessor.CreateStorageSparseMultivectorComposer();

            var idScalarPairs = 
                mv1.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs.GetIndexScalarRecords())
            {
                foreach (var (id2, scalar2) in idScalarPairs.GetIndexScalarRecords())
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

            return composer.CreateGaMultivectorSparseStorage();
        }

        internal static IGaMultivectorStorage<T> BilinearProduct<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, Func<ulong, ulong, int> basisSignatureFunction)
        {
            var composer = 
                scalarProcessor.CreateStorageSparseMultivectorComposer();

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                foreach (var (id2, scalar2) in idScalarPairs2.GetIndexScalarRecords())
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

            return composer.CreateGaMultivectorSparseStorage();
        }

        internal static IGaMultivectorGradedStorage<T> BilinearProduct<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv1, Func<ulong, ulong, int> basisSignatureFunction)
        {
            var grade1 = mv1.Grade;

            var composer = 
                scalarProcessor.CreateStorageGradedMultivectorComposer();

            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            foreach (var (index1, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var id1 = index1.BasisBladeIndexToId(grade1);

                foreach (var (index2, scalar2) in indexScalarPairs1.GetIndexScalarRecords())
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

            return composer.CreateGaMultivectorGradedStorage();
        }

        internal static IGaMultivectorGradedStorage<T> BilinearProduct<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv1, IGaKVectorStorage<T> mv2, Func<ulong, ulong, int> basisSignatureFunction)
        {
            var grade1 = mv1.Grade;
            var grade2 = mv2.Grade;

            var composer = 
                scalarProcessor.CreateStorageGradedMultivectorComposer();

            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            var indexScalarPairs2 = 
                mv2.IndexScalarList;

            foreach (var (index1, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var id1 = index1.BasisBladeIndexToId(grade1);

                foreach (var (index2, scalar2) in indexScalarPairs2.GetIndexScalarRecords())
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

            return composer.CreateGaMultivectorGradedStorage();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<double> Dual(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1)
        {
            var scalarProcessor = Float64ScalarProcessor.DefaultProcessor;

            var pseudoScalarInverse =
                scalarProcessor.CreatePseudoScalarInverseStorage(basisSignature);
            
            return scalarProcessor.Lcp(basisSignature, mv1, pseudoScalarInverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Dual<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaMultivectorStorage<T> mv1)
        {
            var pseudoScalarInverse =
                scalarProcessor.CreatePseudoScalarInverseStorage(basisSignature);

            return scalarProcessor.Lcp(basisSignature, mv1, pseudoScalarInverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Dual<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaSignature basisSignature)
        {
            var pseudoScalarInverse =
                scalarProcessor.CreatePseudoScalarInverseStorage(basisSignature);

            return scalarProcessor.Lcp(basisSignature, mv1, pseudoScalarInverse);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<double> UnDual(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1)
        {
            var pseudoScalarReverse =
                Float64ScalarProcessor.DefaultProcessor.CreatePseudoScalarReverseStorage(basisSignature.VSpaceDimension);

            return Float64ScalarProcessor.DefaultProcessor.Lcp(basisSignature, mv1, pseudoScalarReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> UnDual<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaMultivectorStorage<T> mv1)
        {
            var pseudoScalarReverse =
                scalarProcessor.CreatePseudoScalarReverseStorage(basisSignature.VSpaceDimension);

            return scalarProcessor.Lcp(basisSignature, mv1, pseudoScalarReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> UnDual<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaSignature basisSignature)
        {
            var pseudoScalarReverse =
                scalarProcessor.CreatePseudoScalarReverseStorage(basisSignature.VSpaceDimension);

            return scalarProcessor.Lcp(basisSignature, mv1, pseudoScalarReverse);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<double> BladeInverse(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1)
        {
            var bladeSpSquared = basisSignature.Sp(mv1);

            return Float64ScalarProcessor.DefaultProcessor.Divide(mv1, bladeSpSquared);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> BladeInverse<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaKVectorStorage<T> kVector)
        {
            var bladeSpSquared = scalarProcessor.Sp(basisSignature, kVector);

            return scalarProcessor.Divide(kVector, bladeSpSquared).GetKVectorPart(kVector.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> BladeInverse<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaSignature basisSignature)
        {
            var bladeSpSquared = scalarProcessor.Sp(basisSignature, mv1);

            return scalarProcessor.Divide(mv1, bladeSpSquared);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<double> VersorInverse(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1)
        {
            var versorSpReverse = basisSignature.NormSquared(mv1);

            return Float64ScalarProcessor.DefaultProcessor.Divide(Float64ScalarProcessor.DefaultProcessor.Reverse(mv1), versorSpReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> VersorInverse<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaMultivectorStorage<T> mv1)
        {
            var versorSpReverse = scalarProcessor.NormSquared(basisSignature, mv1);

            return scalarProcessor.Divide(scalarProcessor.Reverse(mv1), versorSpReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> VersorInverse<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaSignature basisSignature)
        {
            var versorSpReverse = scalarProcessor.NormSquared(basisSignature, mv1);

            return scalarProcessor.Divide(scalarProcessor.Reverse(mv1), versorSpReverse);
        }
    }
}