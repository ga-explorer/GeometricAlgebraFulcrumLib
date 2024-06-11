namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;

public class GrVisualFrameStyle3D :
    GrVisualElementStyle3D
{
    public GrVisualSurfaceThickStyle3D OriginStyle { get; init; }

    public GrVisualCurveTubeStyle3D Direction1Style { get; init; }

    public GrVisualCurveTubeStyle3D Direction2Style { get; init; }

    public GrVisualCurveTubeStyle3D Direction3Style { get; init; }
}