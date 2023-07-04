using GraphicsComposerLib.Rendering.KonvaJs.Styles;

namespace GraphicsComposerLib.Rendering.KonvaJs.Filters;

public class GrKonvaFilterSepia :
    GrKonvaFilter
{
    public override string FilterName 
        => "Sepia";


    public GrKonvaFilterSepia(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}