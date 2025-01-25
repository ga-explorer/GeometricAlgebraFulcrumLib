using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Visualizer;

public sealed record GrBabylonJsGeometryVisualizerPointStyle :
    GrBabylonJsGeometryVisualizerElementStyle
{
    internal GrBabylonJsGeometryVisualizerPointStyle(GrBabylonJsGeometryAnimationComposer visualizer, double thickness)
        : base(visualizer, thickness)
    {
    }


    public GrBabylonJsGeometryAnimationComposer SetStyle(double thickness)
    {
        Thickness = thickness;

        return AnimationComposer;
    }

    public GrVisualSurfaceThickStyle3D GetVisualStyle(Color color)
    {
        return new GrVisualSurfaceThickStyle3D(
            AnimationComposer.SceneComposer.AddOrGetColorMaterial(color),
            Thickness
        );
    }
}