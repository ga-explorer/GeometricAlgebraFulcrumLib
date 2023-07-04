using GraphicsComposerLib.Rendering.KonvaJs.Styles;
using GraphicsComposerLib.Rendering.KonvaJs.Values;

namespace GraphicsComposerLib.Rendering.KonvaJs.Filters;

public class GrKonvaFilterMask :
    GrKonvaFilter
{
    public override string FilterName 
        => "Mask";

    
    public GrKonvaJsFloat32Value? Threshold
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Threshold");
        init => ParentStyle.SetAttributeValue("Threshold", value);
    }

    public GrKonvaFilterMask(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}