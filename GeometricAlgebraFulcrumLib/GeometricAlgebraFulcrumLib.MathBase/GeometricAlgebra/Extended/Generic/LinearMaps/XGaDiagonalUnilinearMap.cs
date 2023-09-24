using System.Collections;
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
    public sealed class XGaDiagonalUnilinearMap<T> :
        IXGaUnilinearMap<T>,
        IReadOnlyDictionary<IIndexSet, T>
    {
        public XGaMultivector<T> DiagonalMultivector { get; }
        
        public XGaProcessor<T> Processor 
            => DiagonalMultivector.Processor;

        public XGaMetric Metric 
            => DiagonalMultivector.Metric;

        public IScalarProcessor<T> ScalarProcessor 
            => DiagonalMultivector.ScalarProcessor;

        public int Count 
            => DiagonalMultivector.Count;

        public IEnumerable<IIndexSet> Keys 
            => DiagonalMultivector.Ids;

        public IEnumerable<T> Values 
            => DiagonalMultivector.Scalars;

        public T this[IIndexSet key] 
            => DiagonalMultivector[key];


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaDiagonalUnilinearMap(XGaMultivector<T> diagonalMultivector)
        {
            DiagonalMultivector = diagonalMultivector;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return DiagonalMultivector.IsValid();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(IIndexSet key)
        {
            return DiagonalMultivector.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(IIndexSet key, out T value)
        {
            return DiagonalMultivector.TryGetValue(key, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaUnilinearMap<T> GetAdjoint()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> MapBasisBlade(IIndexSet id)
        {
            return DiagonalMultivector.TryGetValue(id, out var scalar)
                ? Processor.CreateTermKVector(id, scalar)
                : Processor.CreateZeroScalar();
        }

        public XGaMultivector<T> Map(XGaMultivector<T> multivector)
        {
            var composer = Processor.CreateComposer();

            if (Count <= multivector.Count)
            {
                foreach (var (id, mv) in DiagonalMultivector)
                {
                    if (!multivector.TryGetBasisBladeScalarValue(id, out var scalar))
                        continue;

                    composer.AddTerm(id, mv, scalar);
                }
            }
            else
            {
                foreach (var (id, scalar) in multivector)
                {
                    if (!DiagonalMultivector.TryGetValue(id, out var mv))
                        continue;

                    composer.AddTerm(id, mv, scalar);
                }
            }

            return composer.GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<KeyValuePair<IIndexSet, XGaMultivector<T>>> GetMappedBasisBlades(int vSpaceDimensions)
        {
            return DiagonalMultivector
                .Where(p => p.Key.VSpaceDimensions() <= vSpaceDimensions)
                .Select(p => 
                    new KeyValuePair<IIndexSet, XGaMultivector<T>>(
                        p.Key, 
                        Processor.CreateTermKVector(p.Key, p.Value)
                    )
                );
        }
        
        public T[,] GetMultivectorMapArray(int rowCount, int colCount)
        {
            var minSize = DiagonalMultivector.VSpaceDimensions;

            if (rowCount < minSize || colCount < minSize)
                throw new InvalidOperationException();

            var mapArray = 
                ScalarProcessor.CreateArrayZero2D(rowCount, colCount);
            
            foreach (var (id, scalar) in DiagonalMultivector)
            {
                var index = id.ToInt32();

                mapArray[index, index] = scalar;
            }

            return mapArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<KeyValuePair<IIndexSet, T>> GetEnumerator()
        {
            return DiagonalMultivector.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}