using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Filters;

public class GrKonvaFilterEnhance :
    GrKonvaFilter
{
    public override string FilterName 
        => "Enhance";

    
    public GrKonvaJsFloat32Value? Enhance
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Enhance");
        init => ParentStyle.SetAttributeValue("Enhance", value);
    }


    public GrKonvaFilterEnhance(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}