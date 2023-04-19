namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Curves
{
    public class GrVisualCurveSolidLineStyle3D :
        GrVisualCurveStyle3D
    {
        public Color Color { get; }


        public GrVisualCurveSolidLineStyle3D(Color color)
        {
            Color = color;
        }
    }
}