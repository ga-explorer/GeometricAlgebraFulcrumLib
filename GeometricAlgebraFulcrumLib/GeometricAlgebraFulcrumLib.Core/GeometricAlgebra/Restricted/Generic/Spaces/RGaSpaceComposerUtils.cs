using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Spaces.Conformal;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Spaces;

public static class RGaSpaceComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaEuclideanSpace<T> CreateSpace<T>(this RGaEuclideanProcessor<T> processor, int vSpaceDimensions)
    {
        return new RGaEuclideanSpace<T>(processor, vSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaProjectiveSpace<T> CreateSpace<T>(this RGaProjectiveProcessor<T> processor, int vSpaceDimensions)
    {
        return new RGaProjectiveSpace<T>(processor, vSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalSpace<T> CreateSpace<T>(this RGaConformalProcessor<T> processor, int vSpaceDimensions)
    {
        return new RGaConformalSpace<T>(processor, vSpaceDimensions);
    }
}