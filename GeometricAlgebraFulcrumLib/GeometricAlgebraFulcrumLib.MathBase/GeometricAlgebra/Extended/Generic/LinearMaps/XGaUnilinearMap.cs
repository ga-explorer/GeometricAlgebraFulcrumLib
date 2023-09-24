using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps
{
    public class XGaUnilinearMap<T> :
        IXGaUnilinearMap<T>,
        IReadOnlyDictionary<IIndexSet, XGaMultivector<T>>
    {
        private readonly IReadOnlyDictionary<IIndexSet, XGaMultivector<T>> _idMultivectorDictionary;

        public XGaProcessor<T> Processor { get; }

        public XGaMetric Metric 
            => Processor;

        public IScalarProcessor<T> ScalarProcessor 
            => Processor.ScalarProcessor;

        public int Count 
            => _idMultivectorDictionary.Count;
    
        public IEnumerable<IIndexSet> Keys 
            => _idMultivectorDictionary.Keys;

        public IEnumerable<XGaMultivector<T>> Values 
            => _idMultivectorDictionary.Values;
    
        public XGaMultivector<T> this[IIndexSet key] 
            => _idMultivectorDictionary.TryGetValue(key, out var mv)
                ? mv : Processor.CreateZeroScalar();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaUnilinearMap(XGaProcessor<T> processor, IReadOnlyDictionary<IIndexSet, XGaMultivector<T>> idMultivectorDictionary)
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
                d => d.IsValid()
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(IIndexSet key)
        {
            return _idMultivectorDictionary.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(IIndexSet key, out XGaMultivector<T> value)
        {
            return _idMultivectorDictionary.TryGetValue(key, out value);
        }
    
        public IXGaUnilinearMap<T> GetAdjoint()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> MapBasisBlade(IIndexSet id)
        {
            return _idMultivectorDictionary.TryGetValue(id, out var mv)
                ? mv
                : Processor.CreateZeroScalar();
        }

        public XGaMultivector<T> Map(XGaMultivector<T> multivector)
        {
            var composer = Processor.CreateComposer();

            if (Count <= multivector.Count)
            {
                foreach (var (id, mv) in _idMultivectorDictionary)
                {
                    if (!multivector.TryGetBasisBladeScalarValue(id, out var scalar))
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
        public IEnumerable<KeyValuePair<IIndexSet, XGaMultivector<T>>> GetMappedBasisBlades(int vSpaceDimensions)
        {
            return _idMultivectorDictionary
                .Where(p => p.Key.VSpaceDimensions() <= vSpaceDimensions)
                .Select(p => 
                    new KeyValuePair<IIndexSet, XGaMultivector<T>>(p.Key, p.Value)
                );
        }
        
        public T[,] GetMultivectorMapArray(int rowCount, int colCount)
        {
            var mapArray = 
                ScalarProcessor.CreateArrayZero2D(rowCount, colCount);

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
        public IEnumerator<KeyValuePair<IIndexSet, XGaMultivector<T>>> GetEnumerator()
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