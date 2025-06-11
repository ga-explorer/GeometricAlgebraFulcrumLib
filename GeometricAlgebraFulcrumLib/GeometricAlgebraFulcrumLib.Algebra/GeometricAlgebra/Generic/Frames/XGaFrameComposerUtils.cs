using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Frames;

public static class XGaFrameComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBasisVectorFrame<T> CreateBasisVectorFrame<T>(params XGaVector<T>[] vectorArray)
    {
        return XGaBasisVectorFrame<T>.Create(vectorArray);
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBasisKVectorFrame<T> CreateBasisKVectorFrame<T>(this IEnumerable<XGaKVector<T>> kVectorList)
    {
        return XGaBasisKVectorFrame<T>.Create(kVectorList);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVectorFrameFixed<T> CreateFixedFrame<T>(this IXGaVectorFrame<T> frame)
    {
        return XGaVectorFrameFixed<T>.Create(
            frame.Processor.VectorZero,
            frame
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVectorFrameFixed<T> CreateFixedFrame<T>(this IXGaVectorFrame<T> frame, XGaVector<T> point)
    {
        return XGaVectorFrameFixed<T>.Create(point, frame);
    }
        

        
}