using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Rendering.Visuals.Space2D;

public sealed class GrVisualLineVector2D :
    GrVisualElement2D
{
    public IFloat64Tuple2D Position { get; set; }

    public IFloat64Tuple2D Direction { get; set; }


    public GrVisualLineVector2D(string name) 
        : base(name)
    {
    }
}