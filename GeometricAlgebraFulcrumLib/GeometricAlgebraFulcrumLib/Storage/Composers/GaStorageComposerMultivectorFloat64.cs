using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Processing.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    /// <summary>
    /// This composer can efficiently handle up to 12 dimensions, for more dimensions
    /// use the generic composer instead
    /// </summary>
    public sealed class GaStorageComposerMultivectorFloat64 :
        IReadOnlyDictionary<ulong, double>
    {
        private readonly Dictionary<ulong, double> _idScalarDictionary
            = new Dictionary<ulong, double>();

        public static IGaScalarProcessor<double> ScalarProcessor
            => GaScalarProcessorFloat64.DefaultProcessor;

        public IGaSignatureLookup Signature { get; }

        public int Count
            => _idScalarDictionary.Count;

        public IEnumerable<ulong> Keys
            => _idScalarDictionary.Keys;

        public IEnumerable<double> Values
            => _idScalarDictionary.Values;

        public double this[ulong id]
            => _idScalarDictionary.TryGetValue(id, out var value)
                ? value
                : 0d;


        public GaStorageComposerMultivectorFloat64(uint vSpaceDimension)
        {
            Signature = (IGaSignatureLookup)GaSignatureFactory.CreateEuclidean(vSpaceDimension, true);
        }

        public GaStorageComposerMultivectorFloat64(uint positiveCount, uint negativeCount)
        {
            Signature = (IGaSignatureLookup)GaSignatureFactory.Create(positiveCount, negativeCount, true);
        }

        public GaStorageComposerMultivectorFloat64(uint positiveCount, uint negativeCount, uint zeroCount)
        {
            Signature = (IGaSignatureLookup)GaSignatureFactory.Create(positiveCount, negativeCount, zeroCount, true);
        }

        public GaStorageComposerMultivectorFloat64([NotNull] IGaSignatureLookup signature)
        {
            Signature = signature;
        }


        public bool IsEmpty()
        {
            return _idScalarDictionary.Count == 0;
        }

        public bool ContainsKey(ulong id)
        {
            return _idScalarDictionary.ContainsKey(id);
        }

        public bool TryGetValue(ulong id, out double value)
        {
            return _idScalarDictionary.TryGetValue(id, out value);
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

        public void AddGpReverseTerm(ulong id1, ulong id2, double scalar1, double scalar2)
        {
            var signature =
                Signature.GpSignature(id1, id2);

            if (id2.BasisBladeIdHasNegativeReverse())
                signature = -signature;

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

        public void AddSpTerm(ulong id, double scalar1, double scalar2)
        {
            var signature =
                Signature.SpSignature(id);

            if (signature == 0)
                return;

            var scalar = scalar1 * scalar2;

            if (signature < 0)
                scalar = -scalar;

            if (_idScalarDictionary.TryGetValue(0, out var oldScalar))
                _idScalarDictionary[0] = oldScalar + scalar;
            else
                _idScalarDictionary.Add(0, scalar);
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

        public IGaStorageMultivector<double> GetCompactStorage()
        {
            var termsCount = _idScalarDictionary.Count;

            if (termsCount == 0)
                return GaStorageScalar<double>.ZeroScalar;

            if (termsCount > 1)
                return GaStorageMultivectorSparse<double>.Create(_idScalarDictionary);

            var (id, scalar) =
                _idScalarDictionary.First();

            return id == 0UL
                ? GaStorageScalar<double>.Create(scalar)
                : GaStorageKVector<double>.Create(id, scalar);
        }

        public IGaStorageMultivector<double> GetStorage()
        {
            return GaStorageMultivectorSparse<double>.Create(_idScalarDictionary);
        }

        public IEnumerator<KeyValuePair<ulong, double>> GetEnumerator()
        {
            return _idScalarDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}