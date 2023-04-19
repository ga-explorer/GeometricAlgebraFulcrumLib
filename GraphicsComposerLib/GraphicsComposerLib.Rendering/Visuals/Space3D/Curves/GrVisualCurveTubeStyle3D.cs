namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Curves
{
    public class GrVisualCurveTubeStyle3D :
        GrVisualCurveStyle3D
    {
        public IGrVisualElementMaterial3D Material { get; }
    
        public double Thickness { get; }


        public GrVisualCurveTubeStyle3D(IGrVisualElementMaterial3D material, double thickness)
        {
            Material = material;
            Thickness = thickness;
        }
    }
}