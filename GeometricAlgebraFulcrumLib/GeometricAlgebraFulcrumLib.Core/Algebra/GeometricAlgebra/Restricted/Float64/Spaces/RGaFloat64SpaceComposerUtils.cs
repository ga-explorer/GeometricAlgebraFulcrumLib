using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Spaces.Conformal;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Spaces;

public static class RGaFloat64SpaceComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64EuclideanSpace CreateSpace(this RGaFloat64EuclideanProcessor processor, int vSpaceDimensions)
    {
        return new RGaFloat64EuclideanSpace(vSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ProjectiveSpace CreateSpace(this RGaFloat64ProjectiveProcessor processor, int vSpaceDimensions)
    {
        return new RGaFloat64ProjectiveSpace(vSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ConformalSpace CreateSpace(this RGaFloat64ConformalProcessor processor, int vSpaceDimensions)
    {
        return new RGaFloat64ConformalSpace(vSpaceDimensions);
    }

}