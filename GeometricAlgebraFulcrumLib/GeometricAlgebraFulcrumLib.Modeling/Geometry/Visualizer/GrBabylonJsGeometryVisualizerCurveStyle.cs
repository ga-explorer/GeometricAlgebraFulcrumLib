using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Visualizer;

public enum GrBabylonJsGeometryVisualizerCurveStyleKind
{
    Tube,
    Solid,
    Dashed
}

public sealed record GrBabylonJsGeometryVisualizerCurveStyle :
    GrBabylonJsGeometryVisualizerElementStyle
{
    public GrBabylonJsGeometryVisualizerCurveStyleKind Kind { get; private set; }

    public bool Tube
        => Kind == GrBabylonJsGeometryVisualizerCurveStyleKind.Tube;

    public bool Solid
        => Kind == GrBabylonJsGeometryVisualizerCurveStyleKind.Solid;

    public bool Dashed
        => Kind == GrBabylonJsGeometryVisualizerCurveStyleKind.Dashed;

    public int DashOn { get; private set; }

    public int DashOff { get; private set; }

    public int DashPerLine { get; private set; }


    internal GrBabylonJsGeometryVisualizerCurveStyle(GrBabylonJsGeometryAnimationComposer visualizer, double thickness)
        : base(visualizer, thickness)
    {
        Kind = GrBabylonJsGeometryVisualizerCurveStyleKind.Tube;

        DashOn = 0;
        DashOff = 0;
        DashPerLine = 0;
    }

    internal GrBabylonJsGeometryVisualizerCurveStyle(GrBabylonJsGeometryAnimationComposer visualizer)
        : base(visualizer)
    {
        Kind = GrBabylonJsGeometryVisualizerCurveStyleKind.Solid;

        DashOn = 0;
        DashOff = 0;
        DashPerLine = 0;
    }

    internal GrBabylonJsGeometryVisualizerCurveStyle(GrBabylonJsGeometryAnimationComposer visualizer, int dashOn, int dashOff, int dashPerLine)
        : base(visualizer)
    {
        Debug.Assert(dashOn > 0 && dashOff > 0 && dashPerLine > 0);

        Kind = GrBabylonJsGeometryVisualizerCurveStyleKind.Dashed;

        DashOn = dashOn;
        DashOff = dashOff;
        DashPerLine = dashPerLine;
    }


    public GrBabylonJsGeometryAnimationComposer SetTubeStyle(double thickness)
    {
        Kind = GrBabylonJsGeometryVisualizerCurveStyleKind.Tube;
        Thickness = thickness;

        return AnimationComposer;
    }

    public GrBabylonJsGeometryAnimationComposer SetSolidStyle()
    {
        Kind = GrBabylonJsGeometryVisualizerCurveStyleKind.Solid;

        return AnimationComposer;
    }

    public GrBabylonJsGeometryAnimationComposer SetDashedStyle(int dashOn, int dashOff, int dashPerLine)
    {
        Debug.Assert(dashOn > 0 && dashOff > 0 && dashPerLine > 0);

        Kind = GrBabylonJsGeometryVisualizerCurveStyleKind.Dashed;

        DashOn = dashOn;
        DashOff = dashOff;
        DashPerLine = dashPerLine;

        return AnimationComposer;
    }

    public GrVisualCurveStyle3D GetVisualStyle(Color color)
    {
        if (Tube)
            return new GrVisualCurveTubeStyle3D(
                AnimationComposer.SceneComposer.AddOrGetColorMaterial(color),
                Thickness
            );

        if (Solid)
            return new GrVisualCurveSolidLineStyle3D(color);

        return new GrVisualCurveDashedLineStyle3D(
            color,
            new GrVisualDashedLineSpecs(DashOn, DashOff, DashPerLine)
        );
    }
}