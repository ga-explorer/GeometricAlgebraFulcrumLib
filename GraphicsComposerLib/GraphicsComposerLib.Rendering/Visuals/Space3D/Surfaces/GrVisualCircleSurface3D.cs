using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;

public sealed class GrVisualCircleSurface3D :
    GrVisualSurface3D
{
    public ITuple3D Center { get; set; } = Tuple3D.Zero;

    public ITuple3D Normal { get; set; } = Tuple3D.E2;

    public double Radius { get; set; } = 1d;


    public GrVisualCircleSurface3D(string name) 
        : base(name)
    {
    }
}