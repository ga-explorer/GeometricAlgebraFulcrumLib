using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Spaces;

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