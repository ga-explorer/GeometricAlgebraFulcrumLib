using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Visualizer;

public sealed record CGaFloat64VisualizerRoundStyle :
    CGaFloat64VisualizerElementStyle
{
    public bool DrawCenter
        => DrawPosition;

    public bool DrawSphere { get; private set; }


    internal CGaFloat64VisualizerRoundStyle(CGaFloat64Visualizer visualizer, double thickness, bool drawCenter = true, bool drawSphere = true, double directionRadius = 1, double normalDirectionRadius = 1, double auxGeometryColorAlpha = 0.5)
        : base(visualizer, thickness, drawCenter, directionRadius, normalDirectionRadius, auxGeometryColorAlpha)
    {
        DrawSphere = drawSphere;
    }


    public CGaFloat64Visualizer SetStyle(double thickness, bool drawCenter = true, bool drawSphere = true, double directionRadius = 1, double normalDirectionRadius = 1, double auxGeometryColorAlpha = 0.5)
    {
        DrawSphere = drawSphere;
        DrawPosition = drawCenter;
        Thickness = thickness;
        DirectionRadius = directionRadius;
        NormalDirectionRadius = normalDirectionRadius;
        AuxGeometryColorAlpha = auxGeometryColorAlpha;

        return ConformalVisualizer;
    }

    public GrVisualSurfaceThickStyle3D GetPointPairVisualStyle(Color color)
    {
        return new GrVisualSurfaceThickStyle3D(
            Visualizer.MainSceneComposer.AddOrGetColorMaterial(color),
            Thickness
        );
    }

    public GrVisualSurfaceThickStyle3D GetPointPairVisualStyle(Color color, double thickness)
    {
        return new GrVisualSurfaceThickStyle3D(
            Visualizer.MainSceneComposer.AddOrGetColorMaterial(color),
            thickness
        );
    }

    public GrVisualCurveTubeStyle3D GetCircleVisualStyle(Color color)
    {
        return new GrVisualCurveTubeStyle3D(
            Visualizer.MainSceneComposer.AddOrGetColorMaterial(color),
            Thickness
        );
    }

    public GrVisualCurveTubeStyle3D GetCircleVisualStyle(Color color, double thickness)
    {
        return new GrVisualCurveTubeStyle3D(
            Visualizer.MainSceneComposer.AddOrGetColorMaterial(color),
            thickness
        );
    }

    public GrVisualSurfaceThinStyle3D GetSphereVisualStyle(Color color)
    {
        return new GrVisualSurfaceThinStyle3D(
            Visualizer.MainSceneComposer.AddOrGetColorMaterial(color)
        );
    }

}