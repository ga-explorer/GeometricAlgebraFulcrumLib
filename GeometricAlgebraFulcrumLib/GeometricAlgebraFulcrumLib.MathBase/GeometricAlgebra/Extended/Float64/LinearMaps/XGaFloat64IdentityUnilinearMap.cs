using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.LinearMaps
{
    public sealed class XGaFloat64IdentityUnilinearMap :
        IXGaFloat64UnilinearMap
    {
        public XGaFloat64Processor Processor { get; }
        
        public XGaMetric Metric 
            => Processor;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64IdentityUnilinearMap(XGaFloat64Processor processor)
        {
            Processor = processor;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return true;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaFloat64UnilinearMap GetAdjoint()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector MapBasisBlade(IIndexSet id)
        {
            return Processor.CreateKVector(id, 1d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector Map(XGaFloat64Multivector multivector)
        {
            return multivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<KeyValuePair<IIndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
        {
            return Processor
                .GetBasisBladeIds(vSpaceDimensions)
                .Select(id => 
                    new KeyValuePair<IIndexSet, XGaFloat64Multivector>(
                        id, 
                        Processor.CreateKVector(id, 1d)
                    )
                );
        }
        
        public double[,] GetMultivectorMapArray(int rowCount, int colCount)
        {
            var mapArray = 
                new double[rowCount, colCount];

            var n = Math.Min(rowCount, colCount);

            for (var i = 0; i < n; i++)
                mapArray[i, i] = 1d;

            return mapArray;
        }
    }
}