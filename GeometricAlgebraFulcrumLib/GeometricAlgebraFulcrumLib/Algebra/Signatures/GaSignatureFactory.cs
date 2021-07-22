using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.Signatures
{
    public static class GaSignatureFactory
    {
        private static readonly Dictionary<uint, IGaSignatureComputed> ComputedSignaturesDictionary
            = new Dictionary<uint, IGaSignatureComputed>();

        private static readonly Dictionary<uint, IGaSignatureLookup> LookupSignaturesDictionary
            = new Dictionary<uint, IGaSignatureLookup>();
        

        private static IGaSignatureComputed CreateComputedSignature(uint positiveCount)
        {
            var signature = new GaSignatureEuclidean(positiveCount);
            
            ComputedSignaturesDictionary.Add(
                signature.SignatureId, 
                signature
            );

            return signature;
        }

        private static IGaSignatureComputed CreateComputedSignature(uint positiveCount, uint negativeCount)
        {
            var n = positiveCount + negativeCount;

            IGaSignatureComputed signature = negativeCount switch
            {
                0 => new GaSignatureEuclidean(n),
                1 => new GaSignatureConformal(n),
                _ => new GaSignature(positiveCount, negativeCount, 0)
            };

            ComputedSignaturesDictionary.Add(
                signature.SignatureId, 
                signature
            );

            return signature;
        }

        private static IGaSignatureComputed CreateComputedSignature(uint positiveCount, uint negativeCount, uint zeroCount)
        {
            var n = positiveCount + negativeCount + zeroCount;

            IGaSignatureComputed signature = zeroCount switch
            {
                0 => negativeCount switch
                {
                    0 => new GaSignatureEuclidean(n),
                    1 => new GaSignatureConformal(n),
                    _ => new GaSignature(positiveCount, negativeCount, 0)
                },
                1 when negativeCount == 0 => new GaSignatureProjective(n),
                _ => new GaSignature(positiveCount, negativeCount, zeroCount)
            };

            ComputedSignaturesDictionary.Add(
                signature.SignatureId, 
                signature
            );

            return signature;
        }

        private static IGaSignatureLookup CreateLookupSignature(uint positiveCount)
        {
            var baseSignature = 
                (IGaSignatureComputed) CreateEuclidean(positiveCount);

            var signature = new GaSignatureLookup(baseSignature);
            
            LookupSignaturesDictionary.Add(signature.SignatureId, signature);

            return signature;
        }

        private static IGaSignatureLookup CreateLookupSignature(uint positiveCount, uint negativeCount)
        {
            var baseSignature = 
                (IGaSignatureComputed) Create(positiveCount, negativeCount);

            var signature = new GaSignatureLookup(baseSignature);
            
            LookupSignaturesDictionary.Add(signature.SignatureId, signature);

            return signature;
        }

        private static IGaSignatureLookup CreateLookupSignature(uint positiveCount, uint negativeCount, uint zeroCount)
        {
            var baseSignature = 
                (IGaSignatureComputed) Create(positiveCount, negativeCount, zeroCount);

            var signature = new GaSignatureLookup(baseSignature);
            
            LookupSignaturesDictionary.Add(signature.SignatureId, signature);

            return signature;
        }


        public static IGaSignature CreateEuclidean(uint vSpaceDimension, bool lookup = false)
        {
            var signatureId = vSpaceDimension;

            if (lookup)
                return LookupSignaturesDictionary.TryGetValue(signatureId, out var lookupSignature) 
                    ? lookupSignature 
                    : CreateLookupSignature(vSpaceDimension); 

            return ComputedSignaturesDictionary.TryGetValue(signatureId, out var computedSignature) 
                ? (GaSignatureEuclidean) computedSignature 
                : (GaSignatureEuclidean) CreateComputedSignature(vSpaceDimension);
        }

        public static IGaSignature CreateConformal(uint vSpaceDimension, bool lookup = false)
        {
            var signatureId = (vSpaceDimension - 1) | (1u << 6);

            if (lookup)
                return LookupSignaturesDictionary.TryGetValue(signatureId, out var lookupSignature) 
                    ? lookupSignature 
                    : CreateLookupSignature(vSpaceDimension - 1, 1); 

            return ComputedSignaturesDictionary.TryGetValue(signatureId, out var computedSignature) 
                ? (GaSignatureConformal) computedSignature 
                : (GaSignatureConformal) CreateComputedSignature(vSpaceDimension - 1, 1);
        }

        public static IGaSignature CreateProjective(uint vSpaceDimension, bool lookup = false)
        {
            var signatureId = (vSpaceDimension - 1) | (1u << 12);

            if (lookup)
                return LookupSignaturesDictionary.TryGetValue(signatureId, out var lookupSignature) 
                    ? lookupSignature 
                    : CreateLookupSignature(vSpaceDimension - 1, 0, 1); 

            return ComputedSignaturesDictionary.TryGetValue(signatureId, out var computedSignature) 
                ? (GaSignatureProjective) computedSignature 
                : (GaSignatureProjective) CreateComputedSignature(vSpaceDimension - 1, 0, 1);
        }

        public static IGaSignature Create(uint positiveCount, uint negativeCount, bool lookup = false)
        {
            var signatureId = positiveCount | (negativeCount << 6);

            if (lookup)
                return LookupSignaturesDictionary.TryGetValue(signatureId, out var lookupSignature) 
                    ? lookupSignature 
                    : CreateLookupSignature(positiveCount, negativeCount); 

            return ComputedSignaturesDictionary.TryGetValue(signatureId, out var computedSignature) 
                ? computedSignature 
                : CreateComputedSignature(positiveCount, negativeCount);
        }

        public static IGaSignature Create(uint positiveCount, uint negativeCount, uint zeroCount, bool lookup = false)
        {
            var signatureId = positiveCount | (negativeCount << 6) | (zeroCount << 12);

            if (lookup)
                return LookupSignaturesDictionary.TryGetValue(signatureId, out var lookupSignature) 
                    ? lookupSignature 
                    : CreateLookupSignature(positiveCount, negativeCount, zeroCount); 
            
            return ComputedSignaturesDictionary.TryGetValue(signatureId, out var computedSignature) 
                ? computedSignature 
                : CreateComputedSignature(positiveCount, negativeCount, zeroCount);
        }
    }
}