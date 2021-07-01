using System;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage;
using GeometricAlgebraLib.Storage.Composers;

namespace GeometricAlgebraLib.Multivectors.Signatures
{
    public static class GaSignatureUtils
    {
        public static GaSignatureLookup CreateLookupSignature(this IGaSignature baseSignature)
        {
            return GaSignatureLookup.Create(baseSignature);
        }

        
        private static IGaMultivectorStorage<T> BilinearProduct<T>(this IGaScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, Func<ulong, ulong, int> basisSignatureFunction)
        {
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
                    var scalar = signature > 0
                        ? scalarProcessor.Times(scalar1, scalar2)
                        : scalarProcessor.NegativeTimes(scalar1, scalar2);

                    composer.AddTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public static IGaMultivectorStorage<T> Gp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaSignature basisSignature)
        {
            return BilinearProduct(scalarProcessor, mv1, mv2, basisSignature.GpSignature);
        }

        public static IGaMultivectorStorage<T> Sp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaSignature basisSignature)
        {
            return BilinearProduct(scalarProcessor, mv1, mv2, basisSignature.SpSignature);
        }

        public static IGaMultivectorStorage<T> Lcp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaSignature basisSignature)
        {
            return BilinearProduct(scalarProcessor, mv1, mv2, basisSignature.LcpSignature);
        }

        public static IGaMultivectorStorage<T> Rcp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaSignature basisSignature)
        {
            return BilinearProduct(scalarProcessor, mv1, mv2, basisSignature.RcpSignature);
        }

        public static IGaMultivectorStorage<T> Fdp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaSignature basisSignature)
        {
            return BilinearProduct(scalarProcessor, mv1, mv2, basisSignature.FdpSignature);
        }

        public static IGaMultivectorStorage<T> Hip<T>(this IGaScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaSignature basisSignature)
        {
            return BilinearProduct(scalarProcessor, mv1, mv2, basisSignature.HipSignature);
        }

        public static IGaMultivectorStorage<T> Acp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaSignature basisSignature)
        {
            return BilinearProduct(scalarProcessor, mv1, mv2, basisSignature.AcpSignature);
        }

        public static IGaMultivectorStorage<T> Cp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaSignature basisSignature)
        {
            return BilinearProduct(scalarProcessor, mv1, mv2, basisSignature.CpSignature);
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
        
        public static IGaMultivectorStorage<double> Sp(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1, IGaMultivectorStorage<double> mv2)
        {
            var composer = 
                new GaMultivectorStorageFloat64Composer(basisSignature);

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id, scalar1) in idScalarPairs1)
            {
                if (idScalarPairs2.TryGetValue(id, out var scalar2))
                    composer.AddGpTerm(id, id, scalar1, scalar2);
            }

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
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

        
        public static GaBasisBilinearProductResult Gp(this IGaSignature signature, ulong id1, ulong id2)
        {
            return new GaBasisBilinearProductResult(
                signature.GpSignature(id1, id2), 
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

        public static GaBasisBilinearProductResult Sp(this IGaSignature signature, ulong id1, ulong id2)
        {
            return new GaBasisBilinearProductResult(
                signature.SpSignature(id1, id2), 
                id1 ^ id2
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
