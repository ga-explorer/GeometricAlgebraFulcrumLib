using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Visualizer;

public enum GrBabylonJsGeometryVisualizerSurfaceStyleKind
{
    Thin,
    Thick
}

public sealed record GrBabylonJsGeometryVisualizerSurfaceStyle :
    GrBabylonJsGeometryVisualizerElementStyle
{
    public GrBabylonJsGeometryVisualizerSurfaceStyleKind Kind { get; private set; }

    public bool Thin
        => Kind == GrBabylonJsGeometryVisualizerSurfaceStyleKind.Thin;

    public bool Thick
        => Kind == GrBabylonJsGeometryVisualizerSurfaceStyleKind.Thick;


    internal GrBabylonJsGeometryVisualizerSurfaceStyle(GrBabylonJsGeometryAnimationComposer visualizer, double thickness)
        : base(visualizer, thickness)
    {
        Kind = GrBabylonJsGeometryVisualizerSurfaceStyleKind.Thick;
    }

    internal GrBabylonJsGeometryVisualizerSurfaceStyle(GrBabylonJsGeometryAnimationComposer visualizer)
        : base(visualizer)
    {
        Kind = GrBabylonJsGeometryVisualizerSurfaceStyleKind.Thin;
    }


    public GrBabylonJsGeometryAnimationComposer SetThickStyle(double thickness)
    {
        Kind = GrBabylonJsGeometryVisualizerSurfaceStyleKind.Thick;
        Thickness = thickness;

        return AnimationComposer;
    }

    public GrBabylonJsGeometryAnimationComposer SetThinStyle()
    {
        Kind = GrBabylonJsGeometryVisualizerSurfaceStyleKind.Thin;

        return AnimationComposer;
    }

    public GrVisualSurfaceStyle3D GetVisualStyle(Color color)
    {
        if (Thick)
            return new GrVisualSurfaceThickStyle3D(
                AnimationComposer.SceneComposer.AddOrGetColorMaterial(color),
                Thickness
            );

        return new GrVisualSurfaceThinStyle3D(
            AnimationComposer.SceneComposer.AddOrGetColorMaterial(color)
        );
    }
}