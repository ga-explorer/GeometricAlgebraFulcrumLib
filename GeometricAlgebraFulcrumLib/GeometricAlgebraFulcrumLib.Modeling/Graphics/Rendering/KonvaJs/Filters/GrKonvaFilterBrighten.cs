using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Filters;

public class GrKonvaFilterBrighten :
    GrKonvaFilter
{
    public override string FilterName 
        => "Brighten";

    
    public GrKonvaJsFloat32Value? Brightness
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Brightness");
        set => ParentStyle.SetAttributeValue("Brightness", value);
    }


    internal GrKonvaFilterBrighten(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}