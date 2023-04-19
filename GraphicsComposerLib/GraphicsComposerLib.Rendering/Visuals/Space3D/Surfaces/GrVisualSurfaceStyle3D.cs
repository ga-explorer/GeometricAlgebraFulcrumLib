namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces
{
    public abstract class GrVisualSurfaceStyle3D :
        GrVisualElementStyle3D
    {
        public IGrVisualElementMaterial3D Material { get; }

        public IGrVisualElementMaterial3D EdgeMaterial { get; }


        public GrVisualSurfaceStyle3D(IGrVisualElementMaterial3D material)
        {
            Material = material;
            EdgeMaterial = material;
        }

        public GrVisualSurfaceStyle3D(IGrVisualElementMaterial3D material, IGrVisualElementMaterial3D edgeMaterial)
        {
            Material = material;
            EdgeMaterial = edgeMaterial;
        }
    }
}