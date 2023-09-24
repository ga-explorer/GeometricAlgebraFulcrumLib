namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Styles;

public abstract class GrVisualCurveThinLineStyle3D :
    GrVisualCurveStyle3D
{
    public Color Color { get; }


    protected GrVisualCurveThinLineStyle3D(Color color)
    {
        Color = color;
    }
}