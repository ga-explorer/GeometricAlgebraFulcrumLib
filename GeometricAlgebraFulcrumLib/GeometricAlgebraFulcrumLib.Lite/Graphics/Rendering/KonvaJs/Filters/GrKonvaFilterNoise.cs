using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Styles;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Filters;

public class GrKonvaFilterNoise :
    GrKonvaFilter
{
    public override string FilterName 
        => "Noise";

    
    public GrKonvaJsFloat32Value? Noise
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Noise");
        init => ParentStyle.SetAttributeValue("Noise", value);
    }

    public GrKonvaFilterNoise(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}