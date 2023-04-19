namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Curves
{
    public sealed record GrVisualDashedLineSpecs(int DashOn, int DashOff, int DashPerLine);

    public class GrVisualCurveDashedLineStyle3D :
        GrVisualCurveStyle3D
    {
        public Color Color { get; }

        public GrVisualDashedLineSpecs Dash { get; }

        public int DashOn 
            => Dash.DashOn;

        public int DashOff 
            => Dash.DashOff;

        public int DashPerLine 
            => Dash.DashPerLine;

    
        public GrVisualCurveDashedLineStyle3D(Color color, GrVisualDashedLineSpecs dashSpecs)
        {
            Color = color;
            Dash = dashSpecs;
        }

        public GrVisualCurveDashedLineStyle3D(Color color, int dashOn, int dashOff, int dashPerLine)
        {
            Color = color;
            Dash = new GrVisualDashedLineSpecs(dashOn, dashOff, dashPerLine);
        }
    }
}