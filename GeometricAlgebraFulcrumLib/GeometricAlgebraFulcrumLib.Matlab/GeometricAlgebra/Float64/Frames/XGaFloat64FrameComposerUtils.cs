using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Frames;

public static class XGaFloat64FrameComposerUtils
{
     
    
    public static XGaFloat64BasisVectorFrame CreateBasisVectorFrame(params XGaFloat64Vector[] vectorArray)
    {
        return XGaFloat64BasisVectorFrame.Create(vectorArray);
    }

        
    
    public static XGaFloat64BasisKVectorFrame CreateBasisKVectorFrame(this IEnumerable<XGaFloat64KVector> kVectorList)
    {
        return XGaFloat64BasisKVectorFrame.Create(kVectorList);
    }

        

    
    public static XGaFloat64VectorFrameFixed CreateFixedFrame(this IXGaFloat64VectorFrame frame)
    {
        return XGaFloat64VectorFrameFixed.Create(
            frame.Processor.VectorZero,
            frame
        );
    }

    
    public static XGaFloat64VectorFrameFixed CreateFixedFrame(this IXGaFloat64VectorFrame frame, XGaFloat64Vector point)
    {
        return XGaFloat64VectorFrameFixed.Create(point, frame);
    }
        
}