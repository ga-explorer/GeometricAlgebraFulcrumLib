using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
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

        public static IScalarProcessor<double> ScalarProcessor
            => ScalarProcessorFloat64.DefaultProcessor;

        public RGaFloat64Processor BasisSet { get; }

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

        
        public MultivectorFloat64StorageComposer(RGaFloat64Processor basisSet)
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
                BasisSet.GpSign(id1, id2);

            if (signature.IsZero)
                return;

            var id = id1 ^ id2;
            var scalar = scalar1 * scalar2;

            if (signature.IsNegative)
                scalar = -scalar;

            if (_idScalarDictionary.TryGetValue(id, out var oldScalar))
                _idScalarDictionary[id] = oldScalar + scalar;
            else
                _idScalarDictionary.Add(id, scalar);
        }

        public void AddGpReverseTerm(ulong id1, ulong id2, double scalar1, double scalar2)
        {
            var signature =
                BasisSet.GpSign(id1, id2);

            if (id2.ReverseIsNegativeOfBasisBladeId())
                signature = -signature;

            if (signature.IsZero)
                return;

            var id = id1 ^ id2;
            var scalar = scalar1 * scalar2;

            if (signature.IsNegative)
                scalar = -scalar;

            if (_idScalarDictionary.TryGetValue(id, out var oldScalar))
                _idScalarDictionary[id] = oldScalar + scalar;
            else
                _idScalarDictionary.Add(id, scalar);
        }

        public void AddOpTerm(ulong id1, ulong id2, double scalar1, double scalar2)
        {
            var signature =
                BasisSet.OpSign(id1, id2);

            if (signature.IsZero)
                return;

            var id = id1 ^ id2;
            var scalar = scalar1 * scalar2;

            if (signature.IsNegative)
                scalar = -scalar;

            if (_idScalarDictionary.TryGetValue(id, out var oldScalar))
                _idScalarDictionary[id] = oldScalar + scalar;
            else
                _idScalarDictionary.Add(id, scalar);
        }

        public void AddSpTerm(ulong id, double scalar1, double scalar2)
        {
            var signature =
                BasisSet.SpSquaredSign(id);

            if (signature.IsZero)
                return;

            var scalar = scalar1 * scalar2;

            if (signature.IsNegative)
                scalar = -scalar;

            if (_idScalarDictionary.TryGetValue(0, out var oldScalar))
                _idScalarDictionary[0] = oldScalar + scalar;
            else
                _idScalarDictionary.Add(0, scalar);
        }

        public void AddSpTerm(ulong id1, ulong id2, double scalar1, double scalar2)
        {
            var signature =
                BasisSet.SpSign(id1, id2);

            if (signature.IsZero)
                return;

            var id = id1 ^ id2;
            var scalar = scalar1 * scalar2;

            if (signature.IsNegative)
                scalar = -scalar;

            if (_idScalarDictionary.TryGetValue(id, out var oldScalar))
                _idScalarDictionary[id] = oldScalar + scalar;
            else
                _idScalarDictionary.Add(id, scalar);
        }

        public void AddLcpTerm(ulong id1, ulong id2, double scalar1, double scalar2)
        {
            var signature =
                BasisSet.LcpSign(id1, id2);

            if (signature.IsZero)
                return;

            var id = id1 ^ id2;
            var scalar = scalar1 * scalar2;

            if (signature.IsNegative)
                scalar = -scalar;

            if (_idScalarDictionary.TryGetValue(id, out var oldScalar))
                _idScalarDictionary[id] = oldScalar + scalar;
            else
                _idScalarDictionary.Add(id, scalar);
        }

        public void AddRcpTerm(ulong id1, ulong id2, double scalar1, double scalar2)
        {
            var signature =
                BasisSet.RcpSign(id1, id2);

            if (signature.IsZero)
                return;

            var id = id1 ^ id2;
            var scalar = scalar1 * scalar2;

            if (signature.IsNegative)
                scalar = -scalar;

            if (_idScalarDictionary.TryGetValue(id, out var oldScalar))
                _idScalarDictionary[id] = oldScalar + scalar;
            else
                _idScalarDictionary.Add(id, scalar);
        }

        public void AddFdpTerm(ulong id1, ulong id2, double scalar1, double scalar2)
        {
            var signature =
                BasisSet.FdpSign(id1, id2);

            if (signature.IsZero)
                return;

            var id = id1 ^ id2;
            var scalar = scalar1 * scalar2;

            if (signature.IsNegative)
                scalar = -scalar;

            if (_idScalarDictionary.TryGetValue(id, out var oldScalar))
                _idScalarDictionary[id] = oldScalar + scalar;
            else
                _idScalarDictionary.Add(id, scalar);
        }

        public void AddHipTerm(ulong id1, ulong id2, double scalar1, double scalar2)
        {
            var signature =
                BasisSet.HipSign(id1, id2);

            if (signature.IsZero)
                return;

            var id = id1 ^ id2;
            var scalar = scalar1 * scalar2;

            if (signature.IsNegative)
                scalar = -scalar;

            if (_idScalarDictionary.TryGetValue(id, out var oldScalar))
                _idScalarDictionary[id] = oldScalar + scalar;
            else
                _idScalarDictionary.Add(id, scalar);
        }

        public void AddAcpTerm(ulong id1, ulong id2, double scalar1, double scalar2)
        {
            var signature =
                BasisSet.AcpSign(id1, id2);

            if (signature.IsZero)
                return;

            var id = id1 ^ id2;
            var scalar = scalar1 * scalar2;

            if (signature.IsNegative)
                scalar = -scalar;

            if (_idScalarDictionary.TryGetValue(id, out var oldScalar))
                _idScalarDictionary[id] = oldScalar + scalar;
            else
                _idScalarDictionary.Add(id, scalar);
        }

        public void AddCpTerm(ulong id1, ulong id2, double scalar1, double scalar2)
        {
            var signature =
                BasisSet.CpSign(id1, id2);

            if (signature.IsZero)
                return;

            var id = id1 ^ id2;
            var scalar = scalar1 * scalar2;

            if (signature.IsNegative)
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