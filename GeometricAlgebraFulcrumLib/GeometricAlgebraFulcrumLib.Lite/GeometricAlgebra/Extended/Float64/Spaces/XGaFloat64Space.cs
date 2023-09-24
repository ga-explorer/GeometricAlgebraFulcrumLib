using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Spaces
{
    public abstract class XGaFloat64Space
    {
        public abstract int VSpaceDimensions { get; }

        public abstract XGaFloat64Processor Processor { get; }

        public IEnumerable<int> Grades 
            => (1 + VSpaceDimensions).GetRange();

        public XGaMetric Metric 
            => Processor;
    }
}
