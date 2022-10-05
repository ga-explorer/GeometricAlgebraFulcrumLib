using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Rendering.Visuals.Space2D;

public sealed class GrVisualLineVector2D :
    GrVisualElement2D
{
    public ITuple2D Position { get; set; }

    public ITuple2D Direction { get; set; }


    public GrVisualLineVector2D(string name) 
        : base(name)
    {
    }
}