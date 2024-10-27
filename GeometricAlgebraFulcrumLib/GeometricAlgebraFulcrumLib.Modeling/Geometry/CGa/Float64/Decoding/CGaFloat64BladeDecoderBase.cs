using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;

public abstract class CGaFloat64BladeDecoderBase
{
    public CGaFloat64Blade Blade { get; }

    public CGaFloat64GeometricSpace GeometricSpace 
        => Blade.GeometricSpace;


    protected CGaFloat64BladeDecoderBase(CGaFloat64Blade blade)
    {
        Blade = blade;
    }
}