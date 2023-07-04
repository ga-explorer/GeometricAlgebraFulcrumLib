using GraphicsComposerLib.Rendering.KonvaJs.Styles;
using GraphicsComposerLib.Rendering.KonvaJs.Values;

namespace GraphicsComposerLib.Rendering.KonvaJs.Filters;

public class GrKonvaFilterThreshold :
    GrKonvaFilter
{
    public override string FilterName 
        => "Threshold";

    
    public GrKonvaJsFloat32Value? Threshold
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Threshold");
        init => ParentStyle.SetAttributeValue("Threshold", value);
    }


    public GrKonvaFilterThreshold(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}