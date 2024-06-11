using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Filters;

public class GrKonvaFilterHsl :
    GrKonvaFilter
{
    public override string FilterName 
        => "HSL";

    
    public GrKonvaJsFloat32Value? Hue
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Hue");
        init => ParentStyle.SetAttributeValue("Hue", value);
    }

    public GrKonvaJsFloat32Value? Saturation
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Saturation");
        init => ParentStyle.SetAttributeValue("Saturation", value);
    }

    public GrKonvaJsFloat32Value? Luminance
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Luminance");
        init => ParentStyle.SetAttributeValue("Luminance", value);
    }


    public GrKonvaFilterHsl(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}