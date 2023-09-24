using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps
{
    public sealed class RGaIdentityUnilinearMap<T> :
        IRGaUnilinearMap<T>
    {
        public RGaProcessor<T> Processor { get; }

        public RGaMetric Metric 
            => Processor;

        public IScalarProcessor<T> ScalarProcessor 
            => Processor.ScalarProcessor;
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaIdentityUnilinearMap(RGaProcessor<T> processor)
        {
            Processor = processor;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return true;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IRGaUnilinearMap<T> GetAdjoint()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaMultivector<T> MapBasisBlade(ulong id)
        {
            return Processor.CreateTermKVector(id, ScalarProcessor.ScalarOne);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaMultivector<T> Map(RGaMultivector<T> multivector)
        {
            return multivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<KeyValuePair<ulong, RGaMultivector<T>>> GetMappedBasisBlades(int vSpaceDimensions)
        {
            return Processor
                .GetBasisBladeIds(vSpaceDimensions)
                .Select(id => 
                    new KeyValuePair<ulong, RGaMultivector<T>>(
                        id, 
                        Processor.CreateTermKVector(id, ScalarProcessor.ScalarOne)
                    )
                );
        }
        
        public T[,] GetMultivectorMapArray(int rowCount, int colCount)
        {
            var mapArray = 
                ScalarProcessor.CreateArrayZero2D(rowCount, colCount);

            var n = Math.Min(rowCount, colCount);

            for (var i = 0; i < n; i++)
                mapArray[i, i] = ScalarProcessor.ScalarOne;

            return mapArray;
        }
    }
}