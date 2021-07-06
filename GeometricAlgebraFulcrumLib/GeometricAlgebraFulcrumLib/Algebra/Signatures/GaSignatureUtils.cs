using System;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Algebra.Signatures
{
    public static class GaSignatureUtils
    {
        public static GaKVectorTermStorage<T> GetPseudoScalar<T>(this IGaSignature baseSignature, IGaScalarProcessor<T> scalarProcessor)
        {
            return GaKVectorTermStorage<T>.CreatePseudoScalar(
                scalarProcessor,
                baseSignature.VSpaceDimension
            );
        }

        public static GaKVectorTermStorage<T> GetPseudoScalarReverse<T>(this IGaSignature baseSignature, IGaScalarProcessor<T> scalarProcessor)
        {
            var scalar =
                baseSignature.VSpaceDimension.GradeHasNegativeReverse()
                    ? scalarProcessor.MinusOneScalar
                    : scalarProcessor.OneScalar;

            return GaKVectorTermStorage<T>.Create(
                scalarProcessor,
                1UL << baseSignature.VSpaceDimension,
                scalar
            );
        }

        public static IGaKVectorStorage<T> GetPseudoScalarInverse<T>(this IGaSignature baseSignature, IGaScalarProcessor<T> scalarProcessor)
        {
            return baseSignature.BladeInverse(
                GaKVectorTermStorage<T>.CreatePseudoScalar(
                    scalarProcessor,
                    baseSignature.VSpaceDimension
                )
            ).GetKVectorPart(baseSignature.VSpaceDimension);
        }

        public static GaKVectorTermStorage<T> GetEuclideanPseudoScalar<T>(this IGaScalarProcessor<T> scalarProcessor, int vSpaceDimension)
        {
            return GaKVectorTermStorage<T>.CreatePseudoScalar(
                scalarProcessor,
                vSpaceDimension
            );
        }

        public static GaKVectorTermStorage<T> GetEuclideanPseudoScalarReverse<T>(this IGaScalarProcessor<T> scalarProcessor, int vSpaceDimension)
        {
            var scalar =
                vSpaceDimension.GradeHasNegativeReverse()
                    ? scalarProcessor.MinusOneScalar
                    : scalarProcessor.OneScalar;

            return GaKVectorTermStorage<T>.Create(
                scalarProcessor,
                1UL << vSpaceDimension,
                scalar
            );
        }

        public static IGaKVectorStorage<T> GetEuclideanPseudoScalarInverse<T>(this IGaScalarProcessor<T> scalarProcessor, int vSpaceDimension)
        {
            return EBladeInverse(
                GaKVectorTermStorage<T>.CreatePseudoScalar(
                    scalarProcessor,
                    vSpaceDimension
                )
            ).GetKVectorPart(vSpaceDimension);
        }


        public static GaSignatureLookup CreateLookupSignature(this IGaSignature baseSignature)
        {
            return GaSignatureLookup.Create(baseSignature);
        }

        
        private static IGaMultivectorStorage<T> BilinearProduct<T>(IGaMultivectorStorage<T> mv1, Func<ulong, ulong, int> basisSignatureFunction)
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

            return composer.GetCompactStorage();
        }

        private static IGaMultivectorStorage<T> BilinearProduct<T>(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, Func<ulong, ulong, int> basisSignatureFunction)
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

            return composer.GetCompactStorage();
        }


        public static IGaMultivectorStorage<T> Gp<T>(this IGaSignature basisSignature, IGaMultivectorStorage<T> mv)
        {
            return basisSignature.IsEuclidean
                ? BilinearProduct(mv, GaBasisUtils.EGpSignature)
                : BilinearProduct(mv, basisSignature.GpSignature);
        }

        public static IGaMultivectorStorage<T> GpReverse<T>(this IGaSignature basisSignature, IGaMultivectorStorage<T> mv)
        {
            return basisSignature.IsEuclidean
                ? BilinearProduct(mv, GaBasisUtils.EGpReverseSignature)
                : BilinearProduct(mv, basisSignature.GpReverseSignature);
        }

        public static IGaMultivectorStorage<T> Gp<T>(this IGaSignature basisSignature, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return basisSignature.IsEuclidean
                ? BilinearProduct(mv1, mv2, GaBasisUtils.EGpSignature)
                : BilinearProduct(mv1, mv2, basisSignature.GpSignature);
        }

        public static IGaMultivectorStorage<T> GpReverse<T>(this IGaSignature basisSignature, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return basisSignature.IsEuclidean
                ? BilinearProduct(mv1, mv2, GaBasisUtils.EGpReverseSignature)
                : BilinearProduct(mv1, mv2, basisSignature.GpReverseSignature);
        }

        public static T Sp<T>(this IGaSignature basisSignature, IGaMultivectorStorage<T> mv)
        {
            if (basisSignature.IsEuclidean)
                return ESp(mv);

            var scalarProcessor = 
                mv.ScalarProcessor;

            var idScalarPairs1 = 
                mv.GetIdScalarPairs();

            var scalar = 
                scalarProcessor.ZeroScalar;

            foreach (var (id, scalar1) in idScalarPairs1)
            {
                var signature = 
                    basisSignature.SpSignature(id);

                if (signature == 0)
                    continue;

                var scalarTimes = 
                    scalarProcessor.Times(scalar1, scalar1);

                scalar = signature > 0
                    ? scalarProcessor.Add(scalar, scalarTimes)
                    : scalarProcessor.Subtract(scalar, scalarTimes);
            }

            return scalar;
        }

        public static T Sp<T>(this IGaSignature basisSignature, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            if (basisSignature.IsEuclidean)
                return mv1.ESp(mv2);
            
            var scalarProcessor = 
                mv1.ScalarProcessor;

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            var scalar = 
                scalarProcessor.ZeroScalar;

            foreach (var (id, scalar1) in idScalarPairs1)
            {
                if (!idScalarPairs2.TryGetValue(id, out var scalar2))
                    continue;

                var signature = 
                    basisSignature.SpSignature(id);

                if (signature == 0)
                    continue;

                var scalarTimes = 
                    scalarProcessor.Times(scalar1, scalar2);

                scalar = signature > 0
                    ? scalarProcessor.Add(scalar, scalarTimes)
                    : scalarProcessor.Subtract(scalar, scalarTimes);
            }

            return scalar;
        }

        public static T NormSquared<T>(this IGaSignature basisSignature, IGaMultivectorStorage<T> mv)
        {
            if (basisSignature.IsEuclidean)
                return mv.ENormSquared();

            var scalarProcessor = 
                mv.ScalarProcessor;

            var idScalarPairs1 = 
                mv.GetIdScalarPairs();

            var scalar = 
                scalarProcessor.ZeroScalar;

            foreach (var (id, scalar1) in idScalarPairs1)
            {
                var signature = 
                    basisSignature.NormSquaredSignature(id);

                if (signature == 0)
                    continue;

                var scalarTimes = 
                    scalarProcessor.Times(scalar1, scalar1);

                scalar = signature > 0
                    ? scalarProcessor.Add(scalar, scalarTimes)
                    : scalarProcessor.Subtract(scalar, scalarTimes);
            }

            return scalar;
        }

        public static IGaMultivectorStorage<T> Lcp<T>(this IGaSignature basisSignature, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return basisSignature.IsEuclidean
                ? BilinearProduct(mv1, mv2, GaBasisUtils.ELcpSignature)
                : BilinearProduct(mv1, mv2, basisSignature.LcpSignature);
        }

        public static IGaMultivectorStorage<T> Rcp<T>(this IGaSignature basisSignature, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return basisSignature.IsEuclidean
                ? BilinearProduct(mv1, mv2, GaBasisUtils.ERcpSignature)
                : BilinearProduct(mv1, mv2, basisSignature.RcpSignature);
        }

        public static IGaMultivectorStorage<T> Fdp<T>(this IGaSignature basisSignature, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return basisSignature.IsEuclidean
                ? BilinearProduct(mv1, mv2, GaBasisUtils.EFdpSignature)
                : BilinearProduct(mv1, mv2, basisSignature.FdpSignature);
        }

        public static IGaMultivectorStorage<T> Hip<T>(this IGaSignature basisSignature, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return basisSignature.IsEuclidean
                ? BilinearProduct(mv1, mv2, GaBasisUtils.EHipSignature)
                : BilinearProduct(mv1, mv2, basisSignature.HipSignature);
        }

        public static IGaMultivectorStorage<T> Acp<T>(this IGaSignature basisSignature, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return basisSignature.IsEuclidean
                ? BilinearProduct(mv1, mv2, GaBasisUtils.EAcpSignature)
                : BilinearProduct(mv1, mv2, basisSignature.AcpSignature);
        }

        public static IGaMultivectorStorage<T> Cp<T>(this IGaSignature basisSignature, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return basisSignature.IsEuclidean
                ? BilinearProduct(mv1, mv2, GaBasisUtils.ECpSignature)
                : BilinearProduct(mv1, mv2, basisSignature.CpSignature);
        }

        public static IGaMultivectorStorage<T> Dual<T>(this IGaSignature basisSignature, IGaMultivectorStorage<T> mv1)
        {
            var pseudoScalarInverse =
                basisSignature.GetPseudoScalarInverse(mv1.ScalarProcessor);

            return Lcp(basisSignature, mv1, pseudoScalarInverse);
        }

        public static IGaMultivectorStorage<T> UnDual<T>(this IGaSignature basisSignature, IGaMultivectorStorage<T> mv1)
        {
            var pseudoScalarReverse =
                basisSignature.GetPseudoScalarReverse(mv1.ScalarProcessor);

            return Lcp(basisSignature, mv1, pseudoScalarReverse);
        }
        
        public static IGaMultivectorStorage<T> BladeInverse<T>(this IGaSignature basisSignature, IGaMultivectorStorage<T> mv1)
        {
            var bladeSpSquared = Sp(basisSignature, mv1);

            return mv1.Divide(bladeSpSquared);
        }

        public static IGaMultivectorStorage<T> VersorInverse<T>(this IGaSignature basisSignature, IGaMultivectorStorage<T> mv1)
        {
            var versorSpReverse = NormSquared(basisSignature, mv1);

            return mv1.GetReverse().Divide(versorSpReverse);
        }


        public static IGaMultivectorStorage<T> Gp<T>(this IGaMultivectorStorage<T> mv1, IGaSignature basisSignature)
        {
            return basisSignature.IsEuclidean
                ? BilinearProduct(mv1, GaBasisUtils.EGpSignature)
                : BilinearProduct(mv1, basisSignature.GpSignature);
        }

        public static IGaMultivectorStorage<T> GpReverse<T>(this IGaMultivectorStorage<T> mv1, IGaSignature basisSignature)
        {
            return basisSignature.IsEuclidean
                ? BilinearProduct(mv1, GaBasisUtils.EGpReverseSignature)
                : BilinearProduct(mv1, basisSignature.GpReverseSignature);
        }

        public static IGaMultivectorStorage<T> Gp<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaSignature basisSignature)
        {
            return basisSignature.IsEuclidean
                ? BilinearProduct(mv1, mv2, GaBasisUtils.EGpSignature)
                : BilinearProduct(mv1, mv2, basisSignature.GpSignature);
        }

        public static IGaMultivectorStorage<T> GpReverse<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaSignature basisSignature)
        {
            return basisSignature.IsEuclidean
                ? BilinearProduct(mv1, mv2, GaBasisUtils.EGpReverseSignature)
                : BilinearProduct(mv1, mv2, basisSignature.GpReverseSignature);
        }

        public static T Sp<T>(this IGaMultivectorStorage<T> mv, IGaSignature basisSignature)
        {
            return Sp(basisSignature, mv);
        }

        public static T Sp<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaSignature basisSignature)
        {
            return Sp(basisSignature, mv1, mv2);
        }

        public static IGaMultivectorStorage<T> Lcp<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaSignature basisSignature)
        {
            return basisSignature.IsEuclidean
                ? BilinearProduct(mv1, mv2, GaBasisUtils.ELcpSignature)
                : BilinearProduct(mv1, mv2, basisSignature.LcpSignature);
        }

        public static IGaMultivectorStorage<T> Rcp<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaSignature basisSignature)
        {
            return basisSignature.IsEuclidean
                ? BilinearProduct(mv1, mv2, GaBasisUtils.ERcpSignature)
                : BilinearProduct(mv1, mv2, basisSignature.RcpSignature);
        }

        public static IGaMultivectorStorage<T> Fdp<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaSignature basisSignature)
        {
            return basisSignature.IsEuclidean
                ? BilinearProduct(mv1, mv2, GaBasisUtils.EFdpSignature)
                : BilinearProduct(mv1, mv2, basisSignature.FdpSignature);
        }

        public static IGaMultivectorStorage<T> Hip<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaSignature basisSignature)
        {
            return basisSignature.IsEuclidean
                ? BilinearProduct(mv1, mv2, GaBasisUtils.EHipSignature)
                : BilinearProduct(mv1, mv2, basisSignature.HipSignature);
        }

        public static IGaMultivectorStorage<T> Acp<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaSignature basisSignature)
        {
            return basisSignature.IsEuclidean
                ? BilinearProduct(mv1, mv2, GaBasisUtils.EAcpSignature)
                : BilinearProduct(mv1, mv2, basisSignature.AcpSignature);
        }

        public static IGaMultivectorStorage<T> Cp<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaSignature basisSignature)
        {
            return basisSignature.IsEuclidean
                ? BilinearProduct(mv1, mv2, GaBasisUtils.ECpSignature)
                : BilinearProduct(mv1, mv2, basisSignature.CpSignature);
        }

        public static IGaMultivectorStorage<T> Dual<T>(this IGaMultivectorStorage<T> mv1, IGaSignature basisSignature)
        {
            var pseudoScalarInverse =
                basisSignature.GetPseudoScalarInverse(mv1.ScalarProcessor);

            return Lcp(basisSignature, mv1, pseudoScalarInverse);
        }

        public static IGaMultivectorStorage<T> UnDual<T>(this IGaMultivectorStorage<T> mv1, IGaSignature basisSignature)
        {
            var pseudoScalarReverse =
                basisSignature.GetPseudoScalarReverse(mv1.ScalarProcessor);

            return Lcp(basisSignature, mv1, pseudoScalarReverse);
        }

        public static IGaMultivectorStorage<T> BladeInverse<T>(this IGaMultivectorStorage<T> mv1, IGaSignature basisSignature)
        {
            var bladeSpSquared = Sp(basisSignature, mv1);

            return mv1.Divide(bladeSpSquared);
        }

        public static IGaMultivectorStorage<T> VersorInverse<T>(this IGaMultivectorStorage<T> mv1, IGaSignature basisSignature)
        {
            var versorSpReverse = NormSquared(basisSignature, mv1);

            return mv1.GetReverse().Divide(versorSpReverse);
        }


        public static IGaMultivectorStorage<double> Gp(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv)
        {
            var composer = 
                new GaMultivectorStorageFloat64Composer(basisSignature);

            var idScalarPairs = 
                mv.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs)
            foreach (var (id2, scalar2) in idScalarPairs)
                composer.AddGpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
        
        public static IGaMultivectorStorage<double> GpReverse(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv)
        {
            var composer = 
                new GaMultivectorStorageFloat64Composer(basisSignature);

            var idScalarPairs = 
                mv.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs)
            foreach (var (id2, scalar2) in idScalarPairs)
                composer.AddGpReverseTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
        
        public static IGaMultivectorStorage<double> Gp(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1, IGaMultivectorStorage<double> mv2)
        {
            var composer = 
                new GaMultivectorStorageFloat64Composer(basisSignature);

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs1)
            foreach (var (id2, scalar2) in idScalarPairs2)
                composer.AddGpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
        
        public static IGaMultivectorStorage<double> GpReverse(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1, IGaMultivectorStorage<double> mv2)
        {
            var composer = 
                new GaMultivectorStorageFloat64Composer(basisSignature);

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs1)
            foreach (var (id2, scalar2) in idScalarPairs2)
                composer.AddGpReverseTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
        
        public static IGaMultivectorStorage<double> Op(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1, IGaMultivectorStorage<double> mv2)
        {
            var composer = 
                new GaMultivectorStorageFloat64Composer(basisSignature);

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs1)
            foreach (var (id2, scalar2) in idScalarPairs2)
                composer.AddOpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
        
        public static double Sp(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv)
        {
            var idScalarPairs = 
                mv.GetIdScalarPairs();

            var scalar = 0d;

            foreach (var (id1, scalar1) in idScalarPairs) 
                scalar += basisSignature.SpSignature(id1) * scalar1 * scalar1;

            return scalar;
        }
        
        public static double NormSquared(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv)
        {
            var idScalarPairs = 
                mv.GetIdScalarPairs();

            var scalar = 0d;

            foreach (var (id1, scalar1) in idScalarPairs) 
                scalar += basisSignature.NormSquaredSignature(id1) * scalar1 * scalar1;

            return scalar;
        }
        
        public static double Sp(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1, IGaMultivectorStorage<double> mv2)
        {
            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            var scalar = 0d;

            foreach (var (id, scalar1) in idScalarPairs1)
            {
                if (idScalarPairs2.TryGetValue(id, out var scalar2)) 
                    scalar += basisSignature.SpSignature(id) * scalar1 * scalar2;
            }

            return scalar;
        }

        public static IGaMultivectorStorage<double> Lcp(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1, IGaMultivectorStorage<double> mv2)
        {
            var composer = 
                new GaMultivectorStorageFloat64Composer(basisSignature);

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs1)
            foreach (var (id2, scalar2) in idScalarPairs2)
                composer.AddLcpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public static IGaMultivectorStorage<double> Rcp(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1, IGaMultivectorStorage<double> mv2)
        {
            var composer = 
                new GaMultivectorStorageFloat64Composer(basisSignature);

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs1)
            foreach (var (id2, scalar2) in idScalarPairs2)
                composer.AddRcpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public static IGaMultivectorStorage<double> Fdp(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1, IGaMultivectorStorage<double> mv2)
        {
            var composer = 
                new GaMultivectorStorageFloat64Composer(basisSignature);

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs1)
            foreach (var (id2, scalar2) in idScalarPairs2)
                composer.AddFdpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public static IGaMultivectorStorage<double> Hip(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1, IGaMultivectorStorage<double> mv2)
        {
            var composer = 
                new GaMultivectorStorageFloat64Composer(basisSignature);

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs1)
            foreach (var (id2, scalar2) in idScalarPairs2)
                composer.AddHipTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public static IGaMultivectorStorage<double> Acp(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1, IGaMultivectorStorage<double> mv2)
        {
            var composer = 
                new GaMultivectorStorageFloat64Composer(basisSignature);

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs1)
            foreach (var (id2, scalar2) in idScalarPairs2)
                composer.AddAcpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public static IGaMultivectorStorage<double> Cp(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1, IGaMultivectorStorage<double> mv2)
        {
            var composer = 
                new GaMultivectorStorageFloat64Composer(basisSignature);

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs1)
            foreach (var (id2, scalar2) in idScalarPairs2)
                composer.AddCpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public static IGaMultivectorStorage<double> Dual(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1)
        {
            var pseudoScalarInverse =
                basisSignature.GetPseudoScalarInverse(mv1.ScalarProcessor);

            return Lcp(basisSignature, mv1, pseudoScalarInverse);
        }

        public static IGaMultivectorStorage<double> UnDual(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1)
        {
            var pseudoScalarReverse =
                basisSignature.GetPseudoScalarReverse(mv1.ScalarProcessor);

            return Lcp(basisSignature, mv1, pseudoScalarReverse);
        }

        public static IGaMultivectorStorage<double> BladeInverse(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1)
        {
            var bladeSpSquared = Sp(basisSignature, mv1);

            return mv1.Divide(bladeSpSquared);
        }

        public static IGaMultivectorStorage<double> VersorInverse(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1)
        {
            var versorSpReverse = NormSquared(basisSignature, mv1);

            return mv1.GetReverse().Divide(versorSpReverse);
        }
        

        public static IGaMultivectorStorage<T> EGp<T>(this IGaMultivectorStorage<T> mv1)
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
                        GaBasisUtils.EGpSignature(id1, id2);

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public static IGaMultivectorStorage<T> EGpReverse<T>(this IGaMultivectorStorage<T> mv1)
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
                        GaBasisUtils.EGpReverseSignature(id1, id2);

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public static IGaMultivectorStorage<T> EGp<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
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
                        GaBasisUtils.EGpSignature(id1, id2);

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public static IGaMultivectorStorage<T> EGpReverse<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
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
                        GaBasisUtils.EGpSignature(id1, id2);

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public static T ESp<T>(this IGaMultivectorStorage<T> mv)
        {
            var scalarProcessor = 
                mv.ScalarProcessor;

            var idScalarPairs1 = 
                mv.GetIdScalarPairs();

            var scalar = 
                scalarProcessor.ZeroScalar;

            foreach (var (id, scalar1) in idScalarPairs1)
            {
                var signature = 
                    GaBasisUtils.EGpSignature(id);

                var scalarTimes = 
                    scalarProcessor.Times(scalar1, scalar1);

                scalar = signature > 0
                    ? scalarProcessor.Add(scalar, scalarTimes)
                    : scalarProcessor.Subtract(scalar, scalarTimes);
            }

            return scalar;
        }

        public static T ESp<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            var scalar = 
                scalarProcessor.ZeroScalar;

            foreach (var (id, scalar1) in idScalarPairs1)
            {
                if (!idScalarPairs2.TryGetValue(id, out var scalar2))
                    continue;

                var signature = 
                    GaBasisUtils.EGpSignature(id);

                var scalarTimes = 
                    scalarProcessor.Times(scalar1, scalar2);

                scalar = signature > 0
                    ? scalarProcessor.Add(scalar, scalarTimes)
                    : scalarProcessor.Subtract(scalar, scalarTimes);
            }

            return scalar;
        }

        public static T ENormSquared<T>(this IGaMultivectorStorage<T> mv)
        {
            var scalarProcessor = 
                mv.ScalarProcessor;

            var idScalarPairs1 = 
                mv.GetIdScalarPairs();

            var scalar = 
                scalarProcessor.ZeroScalar;

            foreach (var (id, scalar1) in idScalarPairs1)
            {
                var signature = 
                    GaBasisUtils.ENormSquaredSignature(id);

                if (signature == 0)
                    continue;

                var scalarTimes = 
                    scalarProcessor.Times(scalar1, scalar1);

                scalar = signature > 0
                    ? scalarProcessor.Add(scalar, scalarTimes)
                    : scalarProcessor.Subtract(scalar, scalarTimes);
            }

            return scalar;
        }

        public static T ENorm<T>(this IGaMultivectorStorage<T> mv)
        {
            return mv.ScalarProcessor.SqrtOfAbs(
                ENormSquared(mv)
            );
        }

        public static IGaMultivectorStorage<T> ELcp<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return BilinearProduct(mv1, mv2, GaBasisUtils.ELcpSignature);
        }

        public static IGaMultivectorStorage<T> ERcp<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return BilinearProduct(mv1, mv2, GaBasisUtils.ERcpSignature);
        }

        public static IGaMultivectorStorage<T> EFdp<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return BilinearProduct(mv1, mv2, GaBasisUtils.EFdpSignature);
        }

        public static IGaMultivectorStorage<T> EHip<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return BilinearProduct(mv1, mv2, GaBasisUtils.EHipSignature);
        }

        public static IGaMultivectorStorage<T> EAcp<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return BilinearProduct(mv1, mv2, GaBasisUtils.EAcpSignature);
        }

        public static IGaMultivectorStorage<T> ECp<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return BilinearProduct(mv1, mv2, GaBasisUtils.ECpSignature);
        }

        public static IGaMultivectorStorage<T> EDual<T>(this IGaMultivectorStorage<T> mv1, int vSpaceDimension)
        {
            var pseudoScalarInverse =
                GetEuclideanPseudoScalarInverse(mv1.ScalarProcessor, vSpaceDimension);

            return ELcp(mv1, pseudoScalarInverse);
        }

        public static IGaMultivectorStorage<T> EUnDual<T>(this IGaMultivectorStorage<T> mv1, int vSpaceDimension)
        {
            var pseudoScalarReverse =
                GetEuclideanPseudoScalarReverse(mv1.ScalarProcessor, vSpaceDimension);

            return ELcp(mv1, pseudoScalarReverse);
        }

        public static IGaMultivectorStorage<T> EBladeInverse<T>(this IGaMultivectorStorage<T> mv1)
        {
            var bladeSpSquared = ESp(mv1);

            return mv1.Divide(bladeSpSquared);
        }

        public static IGaMultivectorStorage<T> EVersorInverse<T>(this IGaMultivectorStorage<T> mv1)
        {
            var versorSpReverse = ENormSquared(mv1);

            return mv1.GetReverse().Divide(versorSpReverse);
        }


        public static GaBasisBilinearProductResult Gp(this IGaSignature signature, ulong id)
        {
            return new GaBasisBilinearProductResult(
                signature.GpSignature(id), 
                0
            );
        }

        public static GaBasisBilinearProductResult GpReverse(this IGaSignature signature, ulong id)
        {
            return new GaBasisBilinearProductResult(
                signature.GpReverseSignature(id, id), 
                0
            );
        }

        public static GaBasisBilinearProductResult Gp(this IGaSignature signature, ulong id1, ulong id2)
        {
            return new GaBasisBilinearProductResult(
                signature.GpSignature(id1, id2), 
                id1 ^ id2
            );
        }

        public static GaBasisBilinearProductResult GpReverse(this IGaSignature signature, ulong id1, ulong id2)
        {
            return new GaBasisBilinearProductResult(
                signature.GpReverseSignature(id1, id2), 
                id1 ^ id2
            );
        }

        public static GaBasisBilinearProductResult Op(this IGaSignature signature, ulong id1, ulong id2)
        {
            return new GaBasisBilinearProductResult(
                signature.OpSignature(id1, id2), 
                id1 ^ id2
            );
        }

        public static GaBasisBilinearProductResult Sp(this IGaSignature signature, ulong id)
        {
            return new GaBasisBilinearProductResult(
                signature.SpSignature(id), 
                0
            );
        }

        public static GaBasisBilinearProductResult Sp(this IGaSignature signature, ulong id1, ulong id2)
        {
            return new GaBasisBilinearProductResult(
                signature.SpSignature(id1, id2), 
                id1 ^ id2
            );
        }

        public static GaBasisBilinearProductResult NormSquared(this IGaSignature signature, ulong id)
        {
            return new GaBasisBilinearProductResult(
                signature.NormSquaredSignature(id), 
                0
            );
        }

        public static GaBasisBilinearProductResult Lcp(this IGaSignature signature, ulong id1, ulong id2)
        {
            return new GaBasisBilinearProductResult(
                signature.LcpSignature(id1, id2), 
                id1 ^ id2
            );
        }

        public static GaBasisBilinearProductResult Rcp(this IGaSignature signature, ulong id1, ulong id2)
        {
            return new GaBasisBilinearProductResult(
                signature.RcpSignature(id1, id2), 
                id1 ^ id2
            );
        }

        public static GaBasisBilinearProductResult Fdp(this IGaSignature signature, ulong id1, ulong id2)
        {
            return new GaBasisBilinearProductResult(
                signature.FdpSignature(id1, id2), 
                id1 ^ id2
            );
        }

        public static GaBasisBilinearProductResult Hip(this IGaSignature signature, ulong id1, ulong id2)
        {
            return new GaBasisBilinearProductResult(
                signature.HipSignature(id1, id2), 
                id1 ^ id2
            );
        }

        public static GaBasisBilinearProductResult Acp(this IGaSignature signature, ulong id1, ulong id2)
        {
            return new GaBasisBilinearProductResult(
                signature.AcpSignature(id1, id2), 
                id1 ^ id2
            );
        }

        public static GaBasisBilinearProductResult Cp(this IGaSignature signature, ulong id1, ulong id2)
        {
            return new GaBasisBilinearProductResult(
                signature.CpSignature(id1, id2), 
                id1 ^ id2
            );
        }
    }
}
