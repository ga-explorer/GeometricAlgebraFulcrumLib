using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Spaces;

public class XGaProjectiveSpace<T> :
    XGaSpace<T>
{
    public override int VSpaceDimensions { get; }

    public XGaProjectiveProcessor<T> ProjectiveProcessor { get; }

    public override XGaProcessor<T> Processor 
        => ProjectiveProcessor;


    internal XGaProjectiveSpace(XGaProjectiveProcessor<T> processor, int vSpaceDimensions) 
    {
        if (vSpaceDimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        ProjectiveProcessor = processor;
        VSpaceDimensions = vSpaceDimensions;
    }

}