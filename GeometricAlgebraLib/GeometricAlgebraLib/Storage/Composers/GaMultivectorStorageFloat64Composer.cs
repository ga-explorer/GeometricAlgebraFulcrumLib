using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraLib.Implementations.Float64;
using GeometricAlgebraLib.Multivectors.Signatures;
using GeometricAlgebraLib.Processors.Scalars;

namespace GeometricAlgebraLib.Storage.Composers
{
    /// <summary>
    /// This composer can efficiently handle up to 12 dimensions, for more dimensions
    /// use the generic composer instead
    /// </summary>
    public sealed class GaMultivectorStorageFloat64Composer
    {
        private readonly Dictionary<ulong, double> _idScalarDictionary
            = new Dictionary<ulong, double>();

        public static IGaScalarProcessor<double> ScalarProcessor 
            => GaScalarProcessorFloat64.DefaultProcessor;

        public GaSignatureLookup Signature { get; }


        public GaMultivectorStorageFloat64Composer([NotNull] GaSignatureLookup signature)
        {
            Signature = signature;
        }

        public GaMultivectorStorageFloat64Composer([NotNull] IGaSignature baseSignature)
        {
            Signature = GaSignatureLookup.Create(baseSignature);
        }


        public bool IsEmpty()
        {
            return _idScalarDictionary.Count == 0;
        }

        public void AddTerm(ulong id, double scalar)
        {
            if (_idScalarDictionary.TryGetValue(id, out var oldScalar))
                _idScalarDictionary[id] = oldScalar + scalar;
            else
                _idScalarDictionary.Add(id, scalar);
        }

        public void AddGpTerm(ulong id1, ulong id2, double scalar1, double scalar2)
        {
            var signature = 
                Signature.GpSignature(id1, id2);

            if (signature == 0)
                return;

            var id = id1 ^ id2;
            var scalar = scalar1 * scalar2;
            
            if (signature < 0)
                scalar = -scalar;

            if (_idScalarDictionary.TryGetValue(id, out var oldScalar))
                _idScalarDictionary[id] = oldScalar + scalar;
            else
                _idScalarDictionary.Add(id, scalar);
        }

        public void AddOpTerm(ulong id1, ulong id2, double scalar1, double scalar2)
        {
            var signature = 
                Signature.OpSignature(id1, id2);

            if (signature == 0)
                return;

            var id = id1 ^ id2;
            var scalar = scalar1 * scalar2;
            
            if (signature < 0)
                scalar = -scalar;

            if (_idScalarDictionary.TryGetValue(id, out var oldScalar))
                _idScalarDictionary[id] = oldScalar + scalar;
            else
                _idScalarDictionary.Add(id, scalar);
        }

        public void AddSpTerm(ulong id1, ulong id2, double scalar1, double scalar2)
        {
            var signature = 
                Signature.SpSignature(id1, id2);

            if (signature == 0)
                return;

            var id = id1 ^ id2;
            var scalar = scalar1 * scalar2;
            
            if (signature < 0)
                scalar = -scalar;

            if (_idScalarDictionary.TryGetValue(id, out var oldScalar))
                _idScalarDictionary[id] = oldScalar + scalar;
            else
                _idScalarDictionary.Add(id, scalar);
        }

        public void AddLcpTerm(ulong id1, ulong id2, double scalar1, double scalar2)
        {
            var signature = 
                Signature.LcpSignature(id1, id2);

            if (signature == 0)
                return;

            var id = id1 ^ id2;
            var scalar = scalar1 * scalar2;
            
            if (signature < 0)
                scalar = -scalar;

            if (_idScalarDictionary.TryGetValue(id, out var oldScalar))
                _idScalarDictionary[id] = oldScalar + scalar;
            else
                _idScalarDictionary.Add(id, scalar);
        }

        public void AddRcpTerm(ulong id1, ulong id2, double scalar1, double scalar2)
        {
            var signature = 
                Signature.RcpSignature(id1, id2);

            if (signature == 0)
                return;

            var id = id1 ^ id2;
            var scalar = scalar1 * scalar2;
            
            if (signature < 0)
                scalar = -scalar;

            if (_idScalarDictionary.TryGetValue(id, out var oldScalar))
                _idScalarDictionary[id] = oldScalar + scalar;
            else
                _idScalarDictionary.Add(id, scalar);
        }

        public void AddFdpTerm(ulong id1, ulong id2, double scalar1, double scalar2)
        {
            var signature = 
                Signature.FdpSignature(id1, id2);

            if (signature == 0)
                return;

            var id = id1 ^ id2;
            var scalar = scalar1 * scalar2;
            
            if (signature < 0)
                scalar = -scalar;

            if (_idScalarDictionary.TryGetValue(id, out var oldScalar))
                _idScalarDictionary[id] = oldScalar + scalar;
            else
                _idScalarDictionary.Add(id, scalar);
        }

        public void AddHipTerm(ulong id1, ulong id2, double scalar1, double scalar2)
        {
            var signature = 
                Signature.HipSignature(id1, id2);

            if (signature == 0)
                return;

            var id = id1 ^ id2;
            var scalar = scalar1 * scalar2;
            
            if (signature < 0)
                scalar = -scalar;

            if (_idScalarDictionary.TryGetValue(id, out var oldScalar))
                _idScalarDictionary[id] = oldScalar + scalar;
            else
                _idScalarDictionary.Add(id, scalar);
        }

        public void AddAcpTerm(ulong id1, ulong id2, double scalar1, double scalar2)
        {
            var signature = 
                Signature.AcpSignature(id1, id2);

            if (signature == 0)
                return;

            var id = id1 ^ id2;
            var scalar = scalar1 * scalar2;
            
            if (signature < 0)
                scalar = -scalar;

            if (_idScalarDictionary.TryGetValue(id, out var oldScalar))
                _idScalarDictionary[id] = oldScalar + scalar;
            else
                _idScalarDictionary.Add(id, scalar);
        }

        public void AddCpTerm(ulong id1, ulong id2, double scalar1, double scalar2)
        {
            var signature = 
                Signature.CpSignature(id1, id2);

            if (signature == 0)
                return;

            var id = id1 ^ id2;
            var scalar = scalar1 * scalar2;
            
            if (signature < 0)
                scalar = -scalar;

            if (_idScalarDictionary.TryGetValue(id, out var oldScalar))
                _idScalarDictionary[id] = oldScalar + scalar;
            else
                _idScalarDictionary.Add(id, scalar);
        }

        
        public void RemoveTerms(params ulong[] indexList)
        {
            foreach (var key in indexList)
                _idScalarDictionary.Remove(key);
        }

        public void RemoveZeroTerms()
        {
            var idsArray = 
                _idScalarDictionary
                    .Where(pair => ScalarProcessor.IsZero(pair.Value))
                    .Select(pair => pair.Key)
                    .ToArray();

            RemoveTerms(idsArray);
        }

        public void RemoveNearZeroTerms()
        {
            var idsArray = 
                _idScalarDictionary
                    .Where(pair => ScalarProcessor.IsNearZero(pair.Value))
                    .Select(pair => pair.Key)
                    .ToArray();

            RemoveTerms(idsArray);
        }

        public void RemoveZeroTerms(bool nearZeroFlag)
        {
            if (nearZeroFlag)
                RemoveNearZeroTerms();
            else
                RemoveZeroTerms();
        }

        public IGaMultivectorStorage<double> GetCompactStorage()
        {
            var termsCount = _idScalarDictionary.Count;

            if (termsCount == 0)
                return GaScalarTermStorage<double>.CreateZero(ScalarProcessor);

            if (termsCount > 1) 
                return GaMultivectorTermsStorage<double>.Create(
                    ScalarProcessor, 
                    _idScalarDictionary
                );

            var (id, scalar) = _idScalarDictionary.First();

            return id == 0UL
                ? GaScalarTermStorage<double>.Create(ScalarProcessor, scalar)
                : GaKVectorTermStorage<double>.Create(ScalarProcessor, id, scalar);
        }
        
        public IGaMultivectorStorage<double> GetStorage()
        {
            return GaMultivectorTermsStorage<double>.Create(
                ScalarProcessor, 
                _idScalarDictionary
            );
        }

    }
}