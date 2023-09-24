namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Styles
{
    public class GrVisualSurfaceThickStyle3D :
        GrVisualSurfaceStyle3D
    {
        public double Thickness { get; }


        internal GrVisualSurfaceThickStyle3D(IGrVisualElementMaterial3D material, double thickness)
            : base(material)
        {
            Thickness = thickness;
        }

        internal GrVisualSurfaceThickStyle3D(IGrVisualElementMaterial3D material, IGrVisualElementMaterial3D edgeMaterial, double thickness)
            : base(material, edgeMaterial)
        {
            Thickness = thickness;
        }
    }
}