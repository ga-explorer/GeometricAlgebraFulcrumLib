using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Styles;

public class GrKonvaShapeStrokeColor :
    GrKonvaShapeStroke
{
    public GrKonvaJsColorValue? Color
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsColorValue>("Stroke");
        set => ParentStyle.SetAttributeValue("Stroke", value);
    }


    public GrKonvaShapeStrokeColor(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}