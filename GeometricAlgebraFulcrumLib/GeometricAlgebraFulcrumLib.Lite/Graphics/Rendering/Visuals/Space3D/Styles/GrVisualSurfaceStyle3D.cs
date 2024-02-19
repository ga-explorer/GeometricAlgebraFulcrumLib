namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Styles;

public abstract class GrVisualSurfaceStyle3D :
    GrVisualElementStyle3D
{
    public IGrVisualElementMaterial3D Material { get; }

    public IGrVisualElementMaterial3D EdgeMaterial { get; }


    protected GrVisualSurfaceStyle3D(IGrVisualElementMaterial3D material)
    {
        Material = material;
        EdgeMaterial = material;
    }

    protected GrVisualSurfaceStyle3D(IGrVisualElementMaterial3D material, IGrVisualElementMaterial3D edgeMaterial)
    {
        Material = material;
        EdgeMaterial = edgeMaterial;
    }
}