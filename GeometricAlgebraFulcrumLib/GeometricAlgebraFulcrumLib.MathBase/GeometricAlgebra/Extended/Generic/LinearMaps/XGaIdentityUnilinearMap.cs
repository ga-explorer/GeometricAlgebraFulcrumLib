using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps
{
    public sealed class XGaIdentityUnilinearMap<T> :
        IXGaUnilinearMap<T>
    {
        public XGaProcessor<T> Processor { get; }

        public XGaMetric Metric 
            => Processor;

        public IScalarProcessor<T> ScalarProcessor 
            => Processor.ScalarProcessor;
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaIdentityUnilinearMap(XGaProcessor<T> processor)
        {
            Processor = processor;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return true;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaUnilinearMap<T> GetAdjoint()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> MapBasisBlade(IIndexSet id)
        {
            return Processor.CreateKVector(id, ScalarProcessor.ScalarOne);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Map(XGaMultivector<T> multivector)
        {
            return multivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<KeyValuePair<IIndexSet, XGaMultivector<T>>> GetMappedBasisBlades(int vSpaceDimensions)
        {
            return Processor
                .GetBasisBladeIds(vSpaceDimensions)
                .Select(id => 
                    new KeyValuePair<IIndexSet, XGaMultivector<T>>(
                        id, 
                        Processor.CreateKVector(id, ScalarProcessor.ScalarOne)
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