using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms;

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