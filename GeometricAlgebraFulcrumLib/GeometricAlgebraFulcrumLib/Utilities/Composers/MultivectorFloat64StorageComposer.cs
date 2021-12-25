using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    /// <summary>
    /// This composer can efficiently handle up to 12 dimensions, for more dimensions
    /// use the generic composer instead
    /// </summary>
    public sealed class MultivectorFloat64StorageComposer :
        IReadOnlyDictionary<ulong, double>
    {
        private readonly Dictionary<ulong, double> _idScalarDictionary
            = new Dictionary<ulong, double>();

        public static IScalarAlgebraProcessor<double> ScalarProcessor
            => ScalarAlgebraFloat64Processor.DefaultProcessor;

        public GeometricAlgebraBasisSet BasisSet { get; }

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


        public MultivectorFloat64StorageComposer(uint vSpaceDimension)
        {
            BasisSet = GeometricAlgebraBasisSet.CreateEuclidean(vSpaceDimension);
        }

        public MultivectorFloat64StorageComposer(uint positiveCount, uint negativeCount)
        {
            BasisSet = GeometricAlgebraBasisSet.Create(positiveCount, negativeCount);
        }

        public MultivectorFloat64StorageComposer(uint positiveCount, uint negativeCount, uint zeroCount)
        {
            BasisSet = GeometricAlgebraBasisSet.Create(positiveCount, negativeCount, zeroCount);
        }

        public MultivectorFloat64StorageComposer([NotNull] GeometricAlgebraBasisSet basisSet)
        {
            BasisSet = basisSet;
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
                BasisSet.GpSignature(id1, id2);

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
                BasisSet.GpSignature(id1, id2);

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
                BasisSet.OpSignature(id1, id2);

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
                BasisSet.SpSquaredSignature(id);

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
                BasisSet.SpSignature(id1, id2);

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
                BasisSet.LcpSignature(id1, id2);

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
                BasisSet.RcpSignature(id1, id2);

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
                BasisSet.FdpSignature(id1, id2);

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
                BasisSet.HipSignature(id1, id2);

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
                BasisSet.AcpSignature(id1, id2);

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
                BasisSet.CpSignature(id1, id2);

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

        public IMultivectorStorage<double> GetCompactStorage()
        {
            var termsCount = _idScalarDictionary.Count;

            if (termsCount == 0)
                return KVectorStorage<double>.ZeroScalar;

            if (termsCount > 1)
                return MultivectorStorage<double>.Create(_idScalarDictionary);

            var (id, scalar) =
                _idScalarDictionary.First();

            return id == 0UL
                ? KVectorStorage<double>.CreateKVectorScalar(scalar)
                : KVectorStorage<double>.CreateKVector(id, scalar);
        }

        public IMultivectorStorage<double> GetStorage()
        {
            return MultivectorStorage<double>.Create(_idScalarDictionary);
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