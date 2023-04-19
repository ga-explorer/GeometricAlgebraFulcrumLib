using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Spaces.Conformal;

public class XGaFloat64ConformalSpace :
    XGaFloat64Space
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalSpace Create(int vSpaceDimensions)
    {
        return new XGaFloat64ConformalSpace(vSpaceDimensions);
    }


    public override int VSpaceDimensions { get; }

    public XGaFloat64ConformalProcessor ConformalProcessor
        => XGaFloat64Processor.Conformal;

    public override XGaFloat64Processor Processor
        => XGaFloat64Processor.Conformal;

    public XGaFloat64Vector OriginBasisVector { get; }

    public XGaFloat64Vector InfinityBasisVector { get; }


    internal XGaFloat64ConformalSpace(int vSpaceDimensions)
    {
        if (vSpaceDimensions < 2)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        VSpaceDimensions = vSpaceDimensions;
        
        OriginBasisVector =
            XGaFloat64Processor
                .Conformal
                .CreateComposer()
                .SetVectorTerm(0, 0.5d)
                .SetVectorTerm(1, -0.5d)
                .GetVector();
        
        InfinityBasisVector = 
            XGaFloat64Processor
                .Conformal
                .CreateComposer()
                .SetVectorTerm(0, 1d)
                .SetVectorTerm(1, 1d)
                .GetVector();
    }


}