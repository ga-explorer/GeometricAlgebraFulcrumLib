using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Styles;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Visualizer;

public sealed record GeometryVisualizerVectorStyle :
    GeometryVisualizerElementStyle
{
    internal GeometryVisualizerVectorStyle(GeometryVisualizer visualizer, double thickness)
        : base(visualizer, thickness)
    {
    }


    public GeometryVisualizer SetStyle(double thickness)
    {
        Thickness = thickness;

        return Visualizer;
    }

    public GrVisualCurveTubeStyle3D GetVisualStyle(Color color)
    {
        return new GrVisualCurveTubeStyle3D(
            Visualizer.MainSceneComposer.AddOrGetColorMaterial(color),
            Thickness
        );
    }
}