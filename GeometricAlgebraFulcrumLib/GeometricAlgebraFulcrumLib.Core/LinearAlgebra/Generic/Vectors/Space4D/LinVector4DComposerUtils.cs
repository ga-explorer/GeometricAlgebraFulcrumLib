using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space4D;

public static class LinVector4DComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> Vector4D<T>(this IScalarProcessor<T> scalarProcessor, T x, T y, T z, T w)
    {
        return LinVector4D<T>.Create(scalarProcessor, x, y, z, w);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> Vector4D<T>(this IScalarProcessor<T> scalarProcessor, IScalar<T> x, IScalar<T> y, IScalar<T> z, IScalar<T> w)
    {
        return LinVector4D<T>.Create(x, y, z, w);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> ZeroVector4D<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return LinVector4D<T>.Create(
            scalarProcessor, 
            scalarProcessor.ZeroValue, 
            scalarProcessor.ZeroValue, 
            scalarProcessor.ZeroValue,
            scalarProcessor.ZeroValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> E1Vector4D<T>(this IScalarProcessor<T> scalarProcessor, IScalar<T> scalingFactor)
    {
        return LinVector4D<T>.Create(
            scalarProcessor, 
            scalingFactor.ScalarValue,
            scalarProcessor.ZeroValue, 
            scalarProcessor.ZeroValue,
            scalarProcessor.ZeroValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> E2Vector4D<T>(this IScalarProcessor<T> scalarProcessor, IScalar<T> scalingFactor)
    {
        return LinVector4D<T>.Create(
            scalarProcessor, 
            scalarProcessor.ZeroValue, 
            scalingFactor.ScalarValue,
            scalarProcessor.ZeroValue,
            scalarProcessor.ZeroValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> E3Vector4D<T>(this IScalarProcessor<T> scalarProcessor, IScalar<T> scalingFactor)
    {
        return LinVector4D<T>.Create(
            scalarProcessor, 
            scalarProcessor.ZeroValue, 
            scalarProcessor.ZeroValue, 
            scalingFactor.ScalarValue,
            scalarProcessor.ZeroValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> E4Vector4D<T>(this IScalarProcessor<T> scalarProcessor, IScalar<T> scalingFactor)
    {
        return LinVector4D<T>.Create(
            scalarProcessor, 
            scalarProcessor.ZeroValue, 
            scalarProcessor.ZeroValue, 
            scalarProcessor.ZeroValue,
            scalingFactor.ScalarValue
        );
    }



    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> ToVector4D<T>(this IQuad<Scalar<T>> vector)
    {
        return vector as LinVector4D<T>
               ?? LinVector4D<T>.Create(vector.Item1, vector.Item2, vector.Item3, vector.Item4);
    }

}