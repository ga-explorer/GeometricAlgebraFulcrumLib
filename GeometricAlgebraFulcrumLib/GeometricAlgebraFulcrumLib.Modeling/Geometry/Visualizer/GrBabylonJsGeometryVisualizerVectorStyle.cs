using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Visualizer;

public sealed record GrBabylonJsGeometryVisualizerVectorStyle :
    GrBabylonJsGeometryVisualizerElementStyle
{
    internal GrBabylonJsGeometryVisualizerVectorStyle(GrBabylonJsGeometryAnimationComposer visualizer, double thickness)
        : base(visualizer, thickness)
    {
    }


    public GrBabylonJsGeometryAnimationComposer SetStyle(double thickness)
    {
        Thickness = thickness;

        return AnimationComposer;
    }

    public GrVisualCurveTubeStyle3D GetVisualStyle(Color color)
    {
        return new GrVisualCurveTubeStyle3D(
            AnimationComposer.SceneComposer.AddOrGetColorMaterial(color),
            Thickness
        );
    }
}