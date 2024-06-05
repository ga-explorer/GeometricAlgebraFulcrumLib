using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Spaces;

public class RGaFloat64ProjectiveSpace :
    RGaFloat64Space
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ProjectiveSpace Create(int vSpaceDimensions)
    {
        return new RGaFloat64ProjectiveSpace(vSpaceDimensions);
    }


    public override int VSpaceDimensions { get; }

    public RGaFloat64ProjectiveProcessor ProjectiveProcessor 
        => RGaFloat64Processor.Projective;

    public override RGaFloat64Processor Processor 
        => RGaFloat64Processor.Projective;


    internal RGaFloat64ProjectiveSpace(int vSpaceDimensions)
    {
        if (vSpaceDimensions < 0)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        VSpaceDimensions = vSpaceDimensions;
    }


}