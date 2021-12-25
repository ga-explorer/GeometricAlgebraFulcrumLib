//using System.Collections.Generic;
//using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.Signatures;

//namespace GeometricAlgebraFulcrumLib.Utilities.Factories
//{
//    public static class GeometricAlgebraSignatureFactory
//    {
//        private static readonly Dictionary<uint, IGeometricAlgebraSignatureComputed> ComputedSignaturesDictionary
//            = new Dictionary<uint, IGeometricAlgebraSignatureComputed>();

//        private static readonly Dictionary<uint, IGeometricAlgebraSignatureLookup> LookupSignaturesDictionary
//            = new Dictionary<uint, IGeometricAlgebraSignatureLookup>();
        

//        private static IGeometricAlgebraSignatureComputed CreateComputedSignature(uint positiveCount)
//        {
//            var signature = new GeometricAlgebraSignatureEuclidean(positiveCount);
            
//            ComputedSignaturesDictionary.Add(
//                signature.SignatureId, 
//                signature
//            );

//            return signature;
//        }

//        private static IGeometricAlgebraSignatureComputed CreateComputedSignature(uint positiveCount, uint negativeCount)
//        {
//            var n = positiveCount + negativeCount;

//            IGeometricAlgebraSignatureComputed signature = negativeCount switch
//            {
//                0 => new GeometricAlgebraSignatureEuclidean(n),
//                1 => new GeometricAlgebraSignatureConformal(n),
//                _ => new GeometricAlgebraSignature(positiveCount, negativeCount, 0)
//            };

//            ComputedSignaturesDictionary.Add(
//                signature.SignatureId, 
//                signature
//            );

//            return signature;
//        }

//        private static IGeometricAlgebraSignatureComputed CreateComputedSignature(uint positiveCount, uint negativeCount, uint zeroCount)
//        {
//            var n = positiveCount + negativeCount + zeroCount;

//            IGeometricAlgebraSignatureComputed signature = zeroCount switch
//            {
//                0 => negativeCount switch
//                {
//                    0 => new GeometricAlgebraSignatureEuclidean(n),
//                    1 => new GeometricAlgebraSignatureConformal(n),
//                    _ => new GeometricAlgebraSignature(positiveCount, negativeCount, 0)
//                },
//                1 when negativeCount == 0 => new GeometricAlgebraSignatureProjective(n),
//                _ => new GeometricAlgebraSignature(positiveCount, negativeCount, zeroCount)
//            };

//            ComputedSignaturesDictionary.Add(
//                signature.SignatureId, 
//                signature
//            );

//            return signature;
//        }

//        private static IGeometricAlgebraSignatureLookup CreateLookupSignature(uint positiveCount)
//        {
//            var baseSignature = 
//                (IGeometricAlgebraSignatureComputed) CreateEuclidean(positiveCount);

//            var signature = new GeometricAlgebraSignatureLookup(baseSignature);
            
//            LookupSignaturesDictionary.Add(signature.SignatureId, signature);

//            return signature;
//        }

//        private static IGeometricAlgebraSignatureLookup CreateLookupSignature(uint positiveCount, uint negativeCount)
//        {
//            var baseSignature = 
//                (IGeometricAlgebraSignatureComputed) Create(positiveCount, negativeCount);

//            var signature = new GeometricAlgebraSignatureLookup(baseSignature);
            
//            LookupSignaturesDictionary.Add(signature.SignatureId, signature);

//            return signature;
//        }

//        private static IGeometricAlgebraSignatureLookup CreateLookupSignature(uint positiveCount, uint negativeCount, uint zeroCount)
//        {
//            var baseSignature = 
//                (IGeometricAlgebraSignatureComputed) Create(positiveCount, negativeCount, zeroCount);

//            var signature = new GeometricAlgebraSignatureLookup(baseSignature);
            
//            LookupSignaturesDictionary.Add(signature.SignatureId, signature);

//            return signature;
//        }


//        public static GaBasisSet CreateEuclidean(uint vSpaceDimension, bool lookup = false)
//        {
//            var signatureId = vSpaceDimension;

//            if (lookup)
//                return LookupSignaturesDictionary.TryGetValue(signatureId, out var lookupSignature) 
//                    ? lookupSignature 
//                    : CreateLookupSignature(vSpaceDimension); 

//            return ComputedSignaturesDictionary.TryGetValue(signatureId, out var computedSignature) 
//                ? (GeometricAlgebraSignatureEuclidean) computedSignature 
//                : (GeometricAlgebraSignatureEuclidean) CreateComputedSignature(vSpaceDimension);
//        }

//        public static GaBasisSet CreateConformal(uint vSpaceDimension, bool lookup = false)
//        {
//            var signatureId = (vSpaceDimension - 1) | (1u << 6);

//            if (lookup)
//                return LookupSignaturesDictionary.TryGetValue(signatureId, out var lookupSignature) 
//                    ? lookupSignature 
//                    : CreateLookupSignature(vSpaceDimension - 1, 1); 

//            return ComputedSignaturesDictionary.TryGetValue(signatureId, out var computedSignature) 
//                ? computedSignature 
//                : CreateComputedSignature(vSpaceDimension - 1, 1);
//        }

//        public static GaBasisSet CreateProjective(uint vSpaceDimension, bool lookup = false)
//        {
//            var signatureId = (vSpaceDimension - 1) | (1u << 12);

//            if (lookup)
//                return LookupSignaturesDictionary.TryGetValue(signatureId, out var lookupSignature) 
//                    ? lookupSignature 
//                    : CreateLookupSignature(vSpaceDimension - 1, 0, 1); 

//            return ComputedSignaturesDictionary.TryGetValue(signatureId, out var computedSignature) 
//                ? computedSignature 
//                : (GeometricAlgebraSignatureProjective) CreateComputedSignature(vSpaceDimension - 1, 0, 1);
//        }

//        public static GaBasisSet Create(uint positiveCount, uint negativeCount, bool lookup = false)
//        {
//            var signatureId = positiveCount | (negativeCount << 6);

//            if (lookup)
//                return LookupSignaturesDictionary.TryGetValue(signatureId, out var lookupSignature) 
//                    ? lookupSignature 
//                    : CreateLookupSignature(positiveCount, negativeCount); 

//            return ComputedSignaturesDictionary.TryGetValue(signatureId, out var computedSignature) 
//                ? computedSignature 
//                : CreateComputedSignature(positiveCount, negativeCount);
//        }

//        public static GaBasisSet Create(uint positiveCount, uint negativeCount, uint zeroCount, bool lookup = false)
//        {
//            var signatureId = positiveCount | (negativeCount << 6) | (zeroCount << 12);

//            if (lookup)
//                return LookupSignaturesDictionary.TryGetValue(signatureId, out var lookupSignature) 
//                    ? lookupSignature 
//                    : CreateLookupSignature(positiveCount, negativeCount, zeroCount); 
            
//            return ComputedSignaturesDictionary.TryGetValue(signatureId, out var computedSignature) 
//                ? computedSignature 
//                : CreateComputedSignature(positiveCount, negativeCount, zeroCount);
//        }
//    }
//}