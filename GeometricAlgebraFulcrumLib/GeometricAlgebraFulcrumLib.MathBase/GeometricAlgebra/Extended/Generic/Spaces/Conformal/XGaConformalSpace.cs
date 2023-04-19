using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Spaces.Conformal;

public class XGaConformalSpace<T> :
    XGaSpace<T>
{
    public override int VSpaceDimensions { get; }

    public XGaConformalProcessor<T> ConformalProcessor { get; }

    public override XGaProcessor<T> Processor
        => ConformalProcessor;
    
    public XGaVector<T> OriginBasisVector { get; }

    public XGaVector<T> InfinityBasisVector { get; }


    internal XGaConformalSpace(XGaConformalProcessor<T> processor, int vSpaceDimensions)
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