using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Spaces.Conformal;

public class RGaFloat64ConformalSpace :
    RGaFloat64Space
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ConformalSpace Create(int vSpaceDimensions)
    {
        return new RGaFloat64ConformalSpace(vSpaceDimensions);
    }


    public override int VSpaceDimensions { get; }

    public RGaFloat64ConformalProcessor ConformalProcessor
        => RGaFloat64Processor.Conformal;

    public override RGaFloat64Processor Processor
        => RGaFloat64Processor.Conformal;

    public RGaFloat64Vector OriginBasisVector { get; }

    public RGaFloat64Vector InfinityBasisVector { get; }


    internal RGaFloat64ConformalSpace(int vSpaceDimensions)
    {
        if (vSpaceDimensions < 2)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        VSpaceDimensions = vSpaceDimensions;
        
        OriginBasisVector =
            RGaFloat64Processor
                .Conformal
                .CreateComposer()
                .SetVectorTerm(0, 0.5d)
                .SetVectorTerm(1, -0.5d)
                .GetVector();
        
        InfinityBasisVector = 
            RGaFloat64Processor
                .Conformal
                .CreateComposer()
                .SetVectorTerm(0, 1d)
                .SetVectorTerm(1, 1d)
                .GetVector();
    }


}