using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Visualizer;

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