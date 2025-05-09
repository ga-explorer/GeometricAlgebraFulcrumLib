using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Spaces;

public class RGaProjectiveSpace<T> :
    RGaSpace<T>
{
    public override int VSpaceDimensions { get; }

    public RGaProjectiveProcessor<T> ProjectiveProcessor { get; }

    public override RGaProcessor<T> Processor 
        => ProjectiveProcessor;


    internal RGaProjectiveSpace(RGaProjectiveProcessor<T> processor, int vSpaceDimensions) 
    {
        if (vSpaceDimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        ProjectiveProcessor = processor;
        VSpaceDimensions = vSpaceDimensions;
    }

}