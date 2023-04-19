using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.LinearMaps
{
    public class XGaFloat64UnilinearMap :
        IXGaFloat64UnilinearMap,
        IReadOnlyDictionary<IIndexSet, XGaFloat64Multivector>
    {
        private readonly IReadOnlyDictionary<IIndexSet, XGaFloat64Multivector> _idMultivectorDictionary;

        public XGaFloat64Processor Processor { get; }
        
        public XGaMetric Metric 
            => Processor;

        public int Count 
            => _idMultivectorDictionary.Count;
    
        public IEnumerable<IIndexSet> Keys 
            => _idMultivectorDictionary.Keys;

        public IEnumerable<XGaFloat64Multivector> Values 
            => _idMultivectorDictionary.Values;
    
        public XGaFloat64Multivector this[IIndexSet key] 
            => _idMultivectorDictionary.TryGetValue(key, out var mv)
                ? mv : Processor.CreateZeroScalar();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64UnilinearMap(XGaFloat64Processor processor, IReadOnlyDictionary<IIndexSet, XGaFloat64Multivector> idMultivectorDictionary)
        {
            Processor = processor;
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
        public bool ContainsKey(IIndexSet key)
        {
            return _idMultivectorDictionary.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(IIndexSet key, out XGaFloat64Multivector value)
        {
            return _idMultivectorDictionary.TryGetValue(key, out value);
        }
    
        public IXGaFloat64UnilinearMap GetAdjoint()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector MapBasisBlade(IIndexSet id)
        {
            return _idMultivectorDictionary.TryGetValue(id, out var mv)
                ? mv
                : Processor.CreateZeroScalar();
        }

        public XGaFloat64Multivector Map(XGaFloat64Multivector multivector)
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
        public IEnumerable<KeyValuePair<IIndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
        {
            return _idMultivectorDictionary
                .Where(p => p.Key.VSpaceDimensions() <= vSpaceDimensions)
                .Select(p => 
                    new KeyValuePair<IIndexSet, XGaFloat64Multivector>(p.Key, p.Value)
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

            var maxId = _idMultivectorDictionary.Keys.Max() ?? EmptyIndexSet.Instance;

            if (!maxId.TryGetUInt64BitPattern(out var minColCount))
                throw new InvalidOperationException();

            if ((ulong) colCount < minColCount)
                throw new InvalidOperationException();

            foreach (var (colId, vector) in _idMultivectorDictionary)
            {
                var colIndex = colId.ToInt32();

                foreach (var (rowId, scalar) in vector)
                {
                    var rowIndex = rowId.ToInt32();

                    mapArray[rowIndex, colIndex] = scalar;
                }
            }

            return mapArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<KeyValuePair<IIndexSet, XGaFloat64Multivector>> GetEnumerator()
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