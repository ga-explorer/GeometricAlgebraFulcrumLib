

using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Curves;

public class GrVisualCurveDashedLineStyle3D :
    GrVisualCurveStyle3D
{
    public Color Color { get; set; } = Color.Bisque;

    public int DashOn { get; set; } = 3;

    public int DashOff { get; set; } = 1;

    public int DashPerLine { get; set; } = 2;
}