using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Spaces.Conformal;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Spaces;

public static class XGaFloat64SpaceComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64EuclideanSpace CreateSpace(this XGaFloat64EuclideanProcessor processor, int vSpaceDimensions)
    {
        return new XGaFloat64EuclideanSpace(vSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ProjectiveSpace CreateSpace(this XGaFloat64ProjectiveProcessor processor, int vSpaceDimensions)
    {
        return new XGaFloat64ProjectiveSpace(vSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalSpace CreateSpace(this XGaFloat64ConformalProcessor processor, int vSpaceDimensions)
    {
        return new XGaFloat64ConformalSpace(vSpaceDimensions);
    }

}