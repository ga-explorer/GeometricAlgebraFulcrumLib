using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.LinearMaps;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms;

public static class RGaOutermorphismComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaLinearMapOutermorphism<T> CreateOutermorphism<T>(this RGaProcessor<T> processor, LinUnilinearMap<T> linearMap)
    {
        return new RGaLinearMapOutermorphism<T>(processor, linearMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaLinearMapOutermorphism<T> ToOutermorphism<T>(this LinUnilinearMap<T> linearMap, RGaProcessor<T> processor)
    {
        return new RGaLinearMapOutermorphism<T>(processor, linearMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaDiagonalOutermorphism<T> CreateDiagonalAutomorphism<T>(this RGaVector<T> diagonalVector)
    {
        return new RGaDiagonalOutermorphism<T>(diagonalVector);
    }
        
        
}