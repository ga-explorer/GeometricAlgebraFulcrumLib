using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Filters;

public class GrKonvaFilterRgb :
    GrKonvaFilter
{
    public override string FilterName 
        => "RGB";

    
    public GrKonvaJsFloat32Value? Red
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Red");
        init => ParentStyle.SetAttributeValue("Red", value);
    }

    public GrKonvaJsFloat32Value? Green
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Green");
        init => ParentStyle.SetAttributeValue("Green", value);
    }

    public GrKonvaJsFloat32Value? Blue
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Blue");
        init => ParentStyle.SetAttributeValue("Blue", value);
    }

    public GrKonvaFilterRgb(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}