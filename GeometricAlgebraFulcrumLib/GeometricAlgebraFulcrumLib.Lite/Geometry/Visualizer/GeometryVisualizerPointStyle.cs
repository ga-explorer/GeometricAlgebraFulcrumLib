using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Styles;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Visualizer;

public sealed record GeometryVisualizerPointStyle :
    GeometryVisualizerElementStyle
{
    internal GeometryVisualizerPointStyle(GeometryVisualizer visualizer, double thickness)
        : base(visualizer, thickness)
    {
    }


    public GeometryVisualizer SetStyle(double thickness)
    {
        Thickness = thickness;

        return Visualizer;
    }

    public GrVisualSurfaceThickStyle3D GetVisualStyle(Color color)
    {
        return new GrVisualSurfaceThickStyle3D(
            Visualizer.MainSceneComposer.AddOrGetColorMaterial(color),
            Thickness
        );
    }
}