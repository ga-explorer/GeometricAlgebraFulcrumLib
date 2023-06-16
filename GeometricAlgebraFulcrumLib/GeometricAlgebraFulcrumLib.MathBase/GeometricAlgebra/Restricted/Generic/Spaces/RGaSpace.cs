using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Spaces
{
    public abstract class RGaSpace<T>
    {
        public abstract int VSpaceDimensions { get; }

        public abstract RGaProcessor<T> Processor { get; }

        public IEnumerable<int> Grades 
            => (1 + VSpaceDimensions).GetRange();

        public RGaMetric Metric 
            => Processor;

        public IScalarProcessor<T> ScalarProcessor 
            => Processor.ScalarProcessor;
        
    }
}
