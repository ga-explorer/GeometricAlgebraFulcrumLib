namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Curves;

public class GrVisualCurveTubeStyle3D :
    GrVisualCurveStyle3D
{
    public IGrVisualElementMaterial3D Material { get; set; } = null;
    
    public double Thickness { get; set; } = 0.25;
}