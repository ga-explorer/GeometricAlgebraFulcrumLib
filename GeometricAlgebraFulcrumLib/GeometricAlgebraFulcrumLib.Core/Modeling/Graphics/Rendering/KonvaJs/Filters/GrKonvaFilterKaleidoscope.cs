using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Styles;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Filters;

public class GrKonvaFilterKaleidoscope :
    GrKonvaFilter
{
    public override string FilterName
        => "Kaleidoscope";

    
    public GrKonvaJsInt32Value? KaleidoscopeAngle
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsInt32Value>("KaleidoscopeAngle");
        init => ParentStyle.SetAttributeValue("KaleidoscopeAngle", value);
    }
    
    public GrKonvaJsInt32Value? KaleidoscopePower
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsInt32Value>("KaleidoscopePower");
        init => ParentStyle.SetAttributeValue("KaleidoscopePower", value);
    }


    public GrKonvaFilterKaleidoscope(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}