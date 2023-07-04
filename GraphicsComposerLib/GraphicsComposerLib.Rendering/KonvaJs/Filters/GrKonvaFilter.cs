using GraphicsComposerLib.Rendering.KonvaJs.Styles;

namespace GraphicsComposerLib.Rendering.KonvaJs.Filters;

public abstract class GrKonvaFilter :
    GrKonvaShapeSubStyle
{
    public abstract string FilterName { get; }

    
    protected GrKonvaFilter(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}