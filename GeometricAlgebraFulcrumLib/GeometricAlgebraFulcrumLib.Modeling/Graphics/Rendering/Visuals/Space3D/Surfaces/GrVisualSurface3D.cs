using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Surfaces;

public abstract class GrVisualSurface3D :
    GrVisualElement3D
{
    public GrVisualSurfaceStyle3D Style { get; init; }


    protected GrVisualSurface3D(string name) 
        : base(name)
    {
    }
}