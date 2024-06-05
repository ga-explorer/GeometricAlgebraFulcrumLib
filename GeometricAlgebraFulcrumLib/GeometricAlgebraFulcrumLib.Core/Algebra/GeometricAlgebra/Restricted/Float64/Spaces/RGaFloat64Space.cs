using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Spaces;

public abstract class RGaFloat64Space
{
    public abstract int VSpaceDimensions { get; }

    public abstract RGaFloat64Processor Processor { get; }

    public IEnumerable<int> Grades 
        => (1 + VSpaceDimensions).GetRange();

    public RGaMetric Metric 
        => Processor;
}