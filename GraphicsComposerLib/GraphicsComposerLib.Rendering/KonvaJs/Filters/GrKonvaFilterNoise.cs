using GraphicsComposerLib.Rendering.KonvaJs.Styles;
using GraphicsComposerLib.Rendering.KonvaJs.Values;

namespace GraphicsComposerLib.Rendering.KonvaJs.Filters;

public class GrKonvaFilterNoise :
    GrKonvaFilter
{
    public override string FilterName 
        => "Noise";

    
    public GrKonvaJsFloat32Value? Noise
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Noise");
        init => ParentStyle.SetAttributeValue("Noise", value);
    }

    public GrKonvaFilterNoise(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}