using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Spaces;

public class RGaEuclideanSpace<T> :
    RGaSpace<T>
{
    public override int VSpaceDimensions { get; }

    public RGaEuclideanProcessor<T> EuclideanProcessor { get; }

    public override RGaProcessor<T> Processor 
        => EuclideanProcessor;


    internal RGaEuclideanSpace(RGaEuclideanProcessor<T> processor, int vSpaceDimensions) 
    {
        if (vSpaceDimensions < 0)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        EuclideanProcessor = processor;
        VSpaceDimensions = vSpaceDimensions;
    }

}