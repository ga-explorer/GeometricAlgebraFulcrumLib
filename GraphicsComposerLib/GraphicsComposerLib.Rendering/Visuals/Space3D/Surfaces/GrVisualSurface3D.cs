namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;

public abstract class GrVisualSurface3D :
    GrVisualElement3D
{
    public GrVisualSurfaceStyle3D Style { get; set; }


    protected GrVisualSurface3D(string name) 
        : base(name)
    {
    }
}