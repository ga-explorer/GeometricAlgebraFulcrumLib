using GraphicsComposerLib.Rendering.KonvaJs.Styles;

namespace GraphicsComposerLib.Rendering.KonvaJs.Filters;

public class GrKonvaFilterGrayscale :
    GrKonvaFilter
{
    public override string FilterName 
        => "Grayscale";


    public GrKonvaFilterGrayscale(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}