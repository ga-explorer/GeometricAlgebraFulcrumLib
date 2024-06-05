using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Styles;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Filters;

public class GrKonvaFilterRgba :
    GrKonvaFilter
{
    public override string FilterName 
        => "RGBA";

    
    public GrKonvaJsFloat32Value? Alpha
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Alpha");
        init => ParentStyle.SetAttributeValue("Alpha", value);
    }
        
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

    public GrKonvaFilterRgba(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}