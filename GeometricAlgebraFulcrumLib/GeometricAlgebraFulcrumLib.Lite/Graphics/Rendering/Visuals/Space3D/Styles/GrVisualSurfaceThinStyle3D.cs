namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Styles
{
    public class GrVisualSurfaceThinStyle3D :
        GrVisualSurfaceStyle3D
    {
        public Color EdgeColor { get; }


        internal GrVisualSurfaceThinStyle3D(IGrVisualElementMaterial3D material)
            : base(material)
        {
            EdgeColor = Color.Bisque;
        }

        internal GrVisualSurfaceThinStyle3D(IGrVisualElementMaterial3D material, Color edgeColor)
            : base(material)
        {
            EdgeColor = edgeColor;
        }
    }
}