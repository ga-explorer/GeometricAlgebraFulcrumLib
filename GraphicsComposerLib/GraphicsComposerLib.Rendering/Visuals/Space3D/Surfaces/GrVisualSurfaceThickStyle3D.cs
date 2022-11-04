namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;

public class GrVisualSurfaceThickStyle3D :
    GrVisualSurfaceStyle3D
{
    public double Thickness { get; }


    public GrVisualSurfaceThickStyle3D(IGrVisualElementMaterial3D material, double thickness) 
        : base(material)
    {
        Thickness = thickness;
    }

    public GrVisualSurfaceThickStyle3D(IGrVisualElementMaterial3D material, IGrVisualElementMaterial3D edgeMaterial, double thickness) 
        : base(material, edgeMaterial)
    {
        Thickness = thickness;
    }
}