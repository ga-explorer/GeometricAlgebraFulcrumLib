using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;

public class GrVisualCurveDashedLineStyle3D :
    GrVisualCurveThinLineStyle3D
{
    public GrVisualDashedLineSpecs DashSpecs { get; }

    public int DashOn
        => DashSpecs.DashOn;

    public int DashOff
        => DashSpecs.DashOff;

    public int DashPerLine
        => DashSpecs.DashPerLine;


    internal GrVisualCurveDashedLineStyle3D(Color color, GrVisualDashedLineSpecs dashSpecs)
        : base(color) 
    {
        DashSpecs = dashSpecs;
    }
}