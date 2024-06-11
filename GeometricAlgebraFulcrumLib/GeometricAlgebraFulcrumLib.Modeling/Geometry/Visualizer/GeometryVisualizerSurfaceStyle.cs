using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Visualizer;

public enum GeometryVisualizerSurfaceStyleKind
{
    Thin,
    Thick
}

public sealed record GeometryVisualizerSurfaceStyle :
    GeometryVisualizerElementStyle
{
    public GeometryVisualizerSurfaceStyleKind Kind { get; private set; }

    public bool Thin
        => Kind == GeometryVisualizerSurfaceStyleKind.Thin;

    public bool Thick
        => Kind == GeometryVisualizerSurfaceStyleKind.Thick;


    internal GeometryVisualizerSurfaceStyle(GeometryVisualizer visualizer, double thickness)
        : base(visualizer, thickness)
    {
        Kind = GeometryVisualizerSurfaceStyleKind.Thick;
    }

    internal GeometryVisualizerSurfaceStyle(GeometryVisualizer visualizer)
        : base(visualizer)
    {
        Kind = GeometryVisualizerSurfaceStyleKind.Thin;
    }


    public GeometryVisualizer SetThickStyle(double thickness)
    {
        Kind = GeometryVisualizerSurfaceStyleKind.Thick;
        Thickness = thickness;

        return Visualizer;
    }

    public GeometryVisualizer SetThinStyle()
    {
        Kind = GeometryVisualizerSurfaceStyleKind.Thin;

        return Visualizer;
    }

    public GrVisualSurfaceStyle3D GetVisualStyle(Color color)
    {
        if (Thick)
            return new GrVisualSurfaceThickStyle3D(
                Visualizer.MainSceneComposer.AddOrGetColorMaterial(color),
                Thickness
            );

        return new GrVisualSurfaceThinStyle3D(
            Visualizer.MainSceneComposer.AddOrGetColorMaterial(color)
        );
    }
}