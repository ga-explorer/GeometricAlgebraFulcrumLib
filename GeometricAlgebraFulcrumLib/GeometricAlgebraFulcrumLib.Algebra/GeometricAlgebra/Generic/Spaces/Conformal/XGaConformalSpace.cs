using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Spaces.Conformal;

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

        var scalarOne = ScalarProcessor.OneValue;

        OriginBasisVector = 
            processor
                .CreateComposer()
                .SetVectorTerm(0, ScalarProcessor.DivideTwo(scalarOne).ScalarValue)
                .SetVectorTerm(1, ScalarProcessor.DivideMinusTwo(scalarOne).ScalarValue)
                .GetVector();
        
        InfinityBasisVector = 
            processor
                .CreateComposer()
                .SetVectorTerm(0, scalarOne)
                .SetVectorTerm(1, scalarOne)
                .GetVector();
    }

}