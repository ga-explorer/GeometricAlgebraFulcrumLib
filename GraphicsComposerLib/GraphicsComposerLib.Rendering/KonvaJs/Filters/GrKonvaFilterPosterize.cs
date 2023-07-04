using GraphicsComposerLib.Rendering.KonvaJs.Styles;
using GraphicsComposerLib.Rendering.KonvaJs.Values;

namespace GraphicsComposerLib.Rendering.KonvaJs.Filters;

public class GrKonvaFilterPosterize :
    GrKonvaFilter
{
    public override string FilterName 
        => "Posterize";

    
    public GrKonvaJsFloat32Value? Levels
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Levels");
        init => ParentStyle.SetAttributeValue("Levels", value);
    }

    public GrKonvaFilterPosterize(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}