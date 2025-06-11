using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Frames;

public static class XGaFloat64FrameComposerUtils
{
     
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64BasisVectorFrame CreateBasisVectorFrame(params XGaFloat64Vector[] vectorArray)
    {
        return XGaFloat64BasisVectorFrame.Create(vectorArray);
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64BasisKVectorFrame CreateBasisKVectorFrame(this IEnumerable<XGaFloat64KVector> kVectorList)
    {
        return XGaFloat64BasisKVectorFrame.Create(kVectorList);
    }

        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64VectorFrameFixed CreateFixedFrame(this IXGaFloat64VectorFrame frame)
    {
        return XGaFloat64VectorFrameFixed.Create(
            frame.Processor.VectorZero,
            frame
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64VectorFrameFixed CreateFixedFrame(this IXGaFloat64VectorFrame frame, XGaFloat64Vector point)
    {
        return XGaFloat64VectorFrameFixed.Create(point, frame);
    }
        
}