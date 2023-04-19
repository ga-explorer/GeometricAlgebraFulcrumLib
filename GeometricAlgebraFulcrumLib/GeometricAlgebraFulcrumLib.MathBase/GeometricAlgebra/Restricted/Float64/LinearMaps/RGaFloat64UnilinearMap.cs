using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.LinearMaps
{
    public class RGaFloat64UnilinearMap :
        IRGaFloat64UnilinearMap,
        IReadOnlyDictionary<ulong, RGaFloat64Multivector>
    {
        private readonly IReadOnlyDictionary<ulong, RGaFloat64Multivector> _idMultivectorDictionary;

        public RGaFloat64Processor Processor { get; }

        public RGaMetric Metric 
            => Processor;
        
        public int Count 
            => _idMultivectorDictionary.Count;
    
        public IEnumerable<ulong> Keys 
            => _idMultivectorDictionary.Keys;

        public IEnumerable<RGaFloat64Multivector> Values 
            => _idMultivectorDictionary.Values;
    
        public RGaFloat64Multivector this[ulong key] 
            => _idMultivectorDictionary.TryGetValue(key, out var mv)
                ? mv : Processor.CreateZeroScalar();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaFloat64UnilinearMap(RGaFloat64Processor metric, IReadOnlyDictionary<ulong, RGaFloat64Multivector> idMultivectorDictionary)
        {
            Processor = metric;
            _idMultivectorDictionary = idMultivectorDictionary;

            Debug.Assert(
                IsValid()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return _idMultivectorDictionary.Values.All(
                d => d.IsValidMultivectorDictionary()
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(ulong key)
        {
            return _idMultivectorDictionary.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(ulong key, out RGaFloat64Multivector value)
        {
            return _idMultivectorDictionary.TryGetValue(key, out value);
        }
    
        public IRGaFloat64UnilinearMap GetAdjoint()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Multivector MapBasisBlade(ulong id)
        {
            return _idMultivectorDictionary.TryGetValue(id, out var mv)
                ? mv
                : Processor.CreateZeroScalar();
        }

        public RGaFloat64Multivector Map(RGaFloat64Multivector multivector)
        {
            var composer = Processor.CreateComposer();

            if (Count <= multivector.Count)
            {
                foreach (var (id, mv) in _idMultivectorDictionary)
                {
                    if (!multivector.TryGetTermScalar(id, out var scalar))
                        continue;

                    composer.AddMultivector(mv, scalar);
                }
            }
            else
            {
                foreach (var (id, scalar) in multivector)
                {
                    if (!_idMultivectorDictionary.TryGetValue(id, out var mv))
                        continue;

                    composer.AddMultivector(mv, scalar);
                }
            }

            return composer.GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<KeyValuePair<ulong, RGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
        {
            return _idMultivectorDictionary
                .Where(p => p.Key.VSpaceDimensions() <= vSpaceDimensions)
                .Select(p => 
                    new KeyValuePair<ulong, RGaFloat64Multivector>(p.Key, p.Value)
                );
        }
        
        public double[,] GetMultivectorMapArray(int rowCount, int colCount)
        {
            var mapArray = 
                new double[rowCount, colCount];

            if (_idMultivectorDictionary.Count == 0)
                return mapArray;

            var minRowCount = 
                _idMultivectorDictionary.Values.Max(v => v.VSpaceDimensions);

            if (rowCount < minRowCount)
                throw new InvalidOperationException();

            var minColCount = _idMultivectorDictionary.Keys.Max();

            if ((ulong) colCount < minColCount)
                throw new InvalidOperationException();

            foreach (var (colIndex, vector) in _idMultivectorDictionary)
            foreach (var (rowIndex, scalar) in vector)
                mapArray[rowIndex, colIndex] = scalar;

            return mapArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<KeyValuePair<ulong, RGaFloat64Multivector>> GetEnumerator()
        {
            return _idMultivectorDictionary.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}