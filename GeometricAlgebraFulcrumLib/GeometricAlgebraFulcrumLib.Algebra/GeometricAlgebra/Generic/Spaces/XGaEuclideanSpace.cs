using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Spaces;

public class XGaEuclideanSpace<T> :
    XGaSpace<T>
{
    public override int VSpaceDimensions { get; }

    public XGaEuclideanProcessor<T> EuclideanProcessor { get; }

    public override XGaProcessor<T> Processor 
        => EuclideanProcessor;


    internal XGaEuclideanSpace(XGaEuclideanProcessor<T> processor, int vSpaceDimensions) 
    {
        if (vSpaceDimensions < 0)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        EuclideanProcessor = processor;
        VSpaceDimensions = vSpaceDimensions;
    }

}