using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Spaces.Conformal;

public class RGaConformalSpace<T> :
    RGaSpace<T>
{
    public override int VSpaceDimensions { get; }

    public RGaConformalProcessor<T> ConformalProcessor { get; }

    public override RGaProcessor<T> Processor
        => ConformalProcessor;
    
    public RGaVector<T> OriginBasisVector { get; }

    public RGaVector<T> InfinityBasisVector { get; }


    internal RGaConformalSpace(RGaConformalProcessor<T> processor, int vSpaceDimensions)
    {
        if (vSpaceDimensions < 2)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        ConformalProcessor = processor;
        VSpaceDimensions = vSpaceDimensions;

        var scalarOne = ScalarProcessor.ScalarOne;

        OriginBasisVector = 
            processor
                .CreateComposer()
                .SetVectorTerm(0, ScalarProcessor.Half(scalarOne))
                .SetVectorTerm(1, ScalarProcessor.NegativeHalf(scalarOne))
                .GetVector();
        
        InfinityBasisVector = 
            processor
                .CreateComposer()
                .SetVectorTerm(0, scalarOne)
                .SetVectorTerm(1, scalarOne)
                .GetVector();
    }

}