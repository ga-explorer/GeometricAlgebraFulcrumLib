using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

public abstract class CGaFloat64EncoderBase
{
    public CGaFloat64GeometricSpace GeometricSpace { get; }

    public XGaFloat64ConformalProcessor ConformalProcessor 
        => GeometricSpace.ConformalProcessor;


    protected CGaFloat64EncoderBase(CGaFloat64GeometricSpace geometricSpace)
    {
        GeometricSpace = geometricSpace;
    }
}