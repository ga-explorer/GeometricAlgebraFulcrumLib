using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Visualizer;

public enum GeometryVisualizerCurveStyleKind
{
    Tube,
    Solid,
    Dashed
}

public sealed record GeometryVisualizerCurveStyle :
    GeometryVisualizerElementStyle
{
    public GeometryVisualizerCurveStyleKind Kind { get; private set; }

    public bool Tube
        => Kind == GeometryVisualizerCurveStyleKind.Tube;

    public bool Solid
        => Kind == GeometryVisualizerCurveStyleKind.Solid;

    public bool Dashed
        => Kind == GeometryVisualizerCurveStyleKind.Dashed;

    public int DashOn { get; private set; }

    public int DashOff { get; private set; }

    public int DashPerLine { get; private set; }


    internal GeometryVisualizerCurveStyle(GeometryVisualizer visualizer, double thickness)
        : base(visualizer, thickness)
    {
        Kind = GeometryVisualizerCurveStyleKind.Tube;

        DashOn = 0;
        DashOff = 0;
        DashPerLine = 0;
    }

    internal GeometryVisualizerCurveStyle(GeometryVisualizer visualizer)
        : base(visualizer)
    {
        Kind = GeometryVisualizerCurveStyleKind.Solid;

        DashOn = 0;
        DashOff = 0;
        DashPerLine = 0;
    }

    internal GeometryVisualizerCurveStyle(GeometryVisualizer visualizer, int dashOn, int dashOff, int dashPerLine)
        : base(visualizer)
    {
        Debug.Assert(dashOn > 0 && dashOff > 0 && dashPerLine > 0);

        Kind = GeometryVisualizerCurveStyleKind.Dashed;

        DashOn = dashOn;
        DashOff = dashOff;
        DashPerLine = dashPerLine;
    }


    public GeometryVisualizer SetTubeStyle(double thickness)
    {
        Kind = GeometryVisualizerCurveStyleKind.Tube;
        Thickness = thickness;

        return Visualizer;
    }

    public GeometryVisualizer SetSolidStyle()
    {
        Kind = GeometryVisualizerCurveStyleKind.Solid;

        return Visualizer;
    }

    public GeometryVisualizer SetDashedStyle(int dashOn, int dashOff, int dashPerLine)
    {
        Debug.Assert(dashOn > 0 && dashOff > 0 && dashPerLine > 0);

        Kind = GeometryVisualizerCurveStyleKind.Dashed;

        DashOn = dashOn;
        DashOff = dashOff;
        DashPerLine = dashPerLine;

        return Visualizer;
    }

    public GrVisualCurveStyle3D GetVisualStyle(Color color)
    {
        if (Tube)
            return new GrVisualCurveTubeStyle3D(
                Visualizer.MainSceneComposer.AddOrGetColorMaterial(color),
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