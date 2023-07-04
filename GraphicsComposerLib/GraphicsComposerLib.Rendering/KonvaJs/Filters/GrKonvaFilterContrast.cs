using GraphicsComposerLib.Rendering.KonvaJs.Styles;
using GraphicsComposerLib.Rendering.KonvaJs.Values;

namespace GraphicsComposerLib.Rendering.KonvaJs.Filters;

public class GrKonvaFilterContrast :
    GrKonvaFilter
{
    public override string FilterName 
        => "Contrast";

    
    public GrKonvaJsFloat32Value? Contrast
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Contrast");
        set => ParentStyle.SetAttributeValue("Contrast", value);
    }


    internal GrKonvaFilterContrast(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}