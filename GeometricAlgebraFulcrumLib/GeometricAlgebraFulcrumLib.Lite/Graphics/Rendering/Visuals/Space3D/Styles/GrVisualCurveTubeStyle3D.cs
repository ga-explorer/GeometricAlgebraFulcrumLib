namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Styles;

public class GrVisualCurveTubeStyle3D :
    GrVisualCurveStyle3D
{
    public IGrVisualElementMaterial3D Material { get; }

    public double Thickness { get; }


    internal GrVisualCurveTubeStyle3D(IGrVisualElementMaterial3D material, double thickness)
    {
        Material = material;
        Thickness = thickness;
    }
}