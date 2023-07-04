using GraphicsComposerLib.Rendering.KonvaJs.Styles;

namespace GraphicsComposerLib.Rendering.KonvaJs.Filters;

public class GrKonvaFilterInvert :
    GrKonvaFilter
{
    public override string FilterName 
        => "Invert";


    public GrKonvaFilterInvert(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}