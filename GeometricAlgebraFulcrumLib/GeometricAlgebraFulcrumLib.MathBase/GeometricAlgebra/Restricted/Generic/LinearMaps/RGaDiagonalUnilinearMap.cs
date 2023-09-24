using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps
{
    public sealed class RGaDiagonalUnilinearMap<T> :
        IRGaUnilinearMap<T>,
        IReadOnlyDictionary<ulong, T>
    {
        public RGaMultivector<T> DiagonalMultivector { get; }
        
        public RGaProcessor<T> Processor 
            => DiagonalMultivector.Processor;

        public RGaMetric Metric 
            => DiagonalMultivector.Metric;

        public IScalarProcessor<T> ScalarProcessor 
            => DiagonalMultivector.ScalarProcessor;

        public int Count 
            => DiagonalMultivector.Count;

        public IEnumerable<ulong> Keys 
            => DiagonalMultivector.Ids;

        public IEnumerable<T> Values 
            => DiagonalMultivector.Scalars;

        public T this[ulong key] 
            => DiagonalMultivector.GetBasisBladeScalar(key);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaDiagonalUnilinearMap(RGaMultivector<T> diagonalMultivector)
        {
            DiagonalMultivector = diagonalMultivector;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return DiagonalMultivector.IsValid();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(ulong key)
        {
            return DiagonalMultivector.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(ulong key, out T value)
        {
            return DiagonalMultivector.TryGetValue(key, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IRGaUnilinearMap<T> GetAdjoint()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaMultivector<T> MapBasisBlade(ulong id)
        {
            return DiagonalMultivector.TryGetValue(id, out var scalar)
                ? Processor.CreateTermKVector(id, scalar)
                : Processor.CreateZeroScalar();
        }

        public RGaMultivector<T> Map(RGaMultivector<T> multivector)
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
        public IEnumerable<KeyValuePair<ulong, RGaMultivector<T>>> GetMappedBasisBlades(int vSpaceDimensions)
        {
            return DiagonalMultivector
                .Where(p => p.Key.VSpaceDimensions() <= vSpaceDimensions)
                .Select(p => 
                    new KeyValuePair<ulong, RGaMultivector<T>>(
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
            
            foreach (var (index, scalar) in DiagonalMultivector)
                mapArray[index, index] = scalar;

            return mapArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<KeyValuePair<ulong, T>> GetEnumerator()
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