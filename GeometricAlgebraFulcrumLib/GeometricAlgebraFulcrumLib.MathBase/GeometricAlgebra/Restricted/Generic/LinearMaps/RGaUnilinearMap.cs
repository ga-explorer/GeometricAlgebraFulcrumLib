using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps
{
    public class RGaUnilinearMap<T> :
        IRGaUnilinearMap<T>,
        IReadOnlyDictionary<ulong, RGaMultivector<T>>
    {
        private readonly IReadOnlyDictionary<ulong, RGaMultivector<T>> _idMultivectorDictionary;
        
        public RGaProcessor<T> Processor { get; }

        public RGaMetric Metric 
            => Processor;

        public IScalarProcessor<T> ScalarProcessor
            => Processor.ScalarProcessor;

        public int Count 
            => _idMultivectorDictionary.Count;
    
        public IEnumerable<ulong> Keys 
            => _idMultivectorDictionary.Keys;

        public IEnumerable<RGaMultivector<T>> Values 
            => _idMultivectorDictionary.Values;
    
        public RGaMultivector<T> this[ulong key] 
            => _idMultivectorDictionary.TryGetValue(key, out var mv)
                ? mv : Processor.CreateZeroScalar();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaUnilinearMap(RGaProcessor<T> processor, IReadOnlyDictionary<ulong, RGaMultivector<T>> idMultivectorDictionary)
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
                d => d.IsValidMultivectorDictionary(ScalarProcessor)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(ulong key)
        {
            return _idMultivectorDictionary.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(ulong key, out RGaMultivector<T> value)
        {
            return _idMultivectorDictionary.TryGetValue(key, out value);
        }
    
        public IRGaUnilinearMap<T> GetAdjoint()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaMultivector<T> MapBasisBlade(ulong id)
        {
            return _idMultivectorDictionary.TryGetValue(id, out var mv)
                ? mv
                : Processor.CreateZeroScalar();
        }

        public RGaMultivector<T> Map(RGaMultivector<T> multivector)
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
        public IEnumerable<KeyValuePair<ulong, RGaMultivector<T>>> GetMappedBasisBlades(int vSpaceDimensions)
        {
            return _idMultivectorDictionary
                .Where(p => p.Key.VSpaceDimensions() <= vSpaceDimensions)
                .Select(p => 
                    new KeyValuePair<ulong, RGaMultivector<T>>(p.Key, p.Value)
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

            var minColCount = _idMultivectorDictionary.Keys.Max();

            if ((ulong) colCount < minColCount)
                throw new InvalidOperationException();

            foreach (var (colIndex, vector) in _idMultivectorDictionary)
            foreach (var (rowIndex, scalar) in vector)
                mapArray[rowIndex, colIndex] = scalar;

            return mapArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<KeyValuePair<ulong, RGaMultivector<T>>> GetEnumerator()
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