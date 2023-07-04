using GraphicsComposerLib.Rendering.KonvaJs.Styles;

namespace GraphicsComposerLib.Rendering.KonvaJs.Filters;

public class GrKonvaFilterSolarize :
    GrKonvaFilter
{
    public override string FilterName 
        => "Solarize";


    public GrKonvaFilterSolarize(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}