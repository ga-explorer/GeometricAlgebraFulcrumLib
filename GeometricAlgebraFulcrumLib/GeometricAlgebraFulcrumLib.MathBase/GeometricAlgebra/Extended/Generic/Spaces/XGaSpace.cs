using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Spaces;

public abstract class XGaSpace<T>
{
    public abstract int VSpaceDimensions { get; }

    public abstract XGaProcessor<T> Processor { get; }

    public IEnumerable<int> Grades 
        => (1 + VSpaceDimensions).GetRange();

    public XGaMetric Metric 
        => Processor;

    public IScalarProcessor<T> ScalarProcessor 
        => Processor.ScalarProcessor;
        
}