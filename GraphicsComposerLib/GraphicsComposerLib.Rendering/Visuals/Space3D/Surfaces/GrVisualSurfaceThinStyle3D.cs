namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces
{
    public class GrVisualSurfaceThinStyle3D :
        GrVisualSurfaceStyle3D
    {
        public Color EdgeColor { get; }


        public GrVisualSurfaceThinStyle3D(IGrVisualElementMaterial3D material) 
            : base(material)
        {
            EdgeColor = Color.Bisque;
        }

        public GrVisualSurfaceThinStyle3D(IGrVisualElementMaterial3D material, Color edgeColor) 
            : base(material)
        {
            EdgeColor = edgeColor;
        }
    }
}