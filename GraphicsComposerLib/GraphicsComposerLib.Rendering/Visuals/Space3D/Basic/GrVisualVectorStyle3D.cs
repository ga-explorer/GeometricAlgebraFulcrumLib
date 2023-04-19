namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Basic
{
    public class GrVisualVectorStyle3D
    {
        //public Color OriginColor { get; set; } = Color.LightGray;

        public IGrVisualElementMaterial3D Material { get; }

        //public double OriginThickness { get; set; } = 0.075;

        public double Thickness { get; }


        public GrVisualVectorStyle3D(IGrVisualElementMaterial3D material, double thickness)
        {
            Material = material;
            Thickness = thickness;
        }
    }
}