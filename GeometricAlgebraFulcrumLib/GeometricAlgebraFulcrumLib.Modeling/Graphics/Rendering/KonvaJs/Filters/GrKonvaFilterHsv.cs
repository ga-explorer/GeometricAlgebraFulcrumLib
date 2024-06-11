using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Filters;

public class GrKonvaFilterHsv :
    GrKonvaFilter
{
    public override string FilterName 
        => "HSV";

    
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

    public GrKonvaJsFloat32Value? Value
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Value");
        init => ParentStyle.SetAttributeValue("Value", value);
    }

    public GrKonvaFilterHsv(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}