using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Visualizer;

public sealed record RGaConformalVisualizerRoundStyle :
    RGaConformalVisualizerElementStyle
{
    public bool DrawCenter 
        => DrawPosition;
    
    public bool DrawSphere { get; private set; }
    

    internal RGaConformalVisualizerRoundStyle(RGaConformalVisualizer visualizer, double thickness, bool drawCenter = true, bool drawSphere = true, double directionRadius = 1, double normalDirectionRadius = 1, double auxGeometryColorAlpha = 0.5)
        : base(visualizer, thickness, drawCenter, directionRadius, normalDirectionRadius, auxGeometryColorAlpha)
    {
        DrawSphere = drawSphere;
    }

    
    public RGaConformalVisualizer SetStyle(double thickness, bool drawCenter = true, bool drawSphere = true, double directionRadius = 1, double normalDirectionRadius = 1, double auxGeometryColorAlpha = 0.5)
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