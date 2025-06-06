﻿using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Curves;

public sealed class GrVisualParametricCurve3D :
    GrVisualCurve3D
{
    public static GrVisualParametricCurve3D Create(string name, GrVisualCurveStyle3D style, Float64Path3D curve, IReadOnlyList<double> parameterValues, IReadOnlyList<double> frameParameterValues)
    {
        return new GrVisualParametricCurve3D(
            name,
            style, 
            curve, 
            parameterValues,
            frameParameterValues
        );
    }


    public Float64Path3D Curve { get; }

    public IReadOnlyList<double> ParameterValues { get; }

    public IReadOnlyList<double> FrameParameterValues { get; }

    public bool ShowCurve { get; set; }
        = true;

    public bool ShowFrames { get; set; }
        = false;

    public double FrameSize { get; set; } 
        = 1;

    public override double ArcLength 
        => throw new NotImplementedException();


    private GrVisualParametricCurve3D(string name, GrVisualCurveStyle3D style, Float64Path3D curve, IReadOnlyList<double> parameterValues, IReadOnlyList<double> frameParameterValues)
        : base(name, style)
    {
        Curve = curve;
        ParameterValues = parameterValues;
        FrameParameterValues = frameParameterValues;
    }


    public override IPointsPath3D GetPointsPath(int count)
    {
        return new ParametricPointsPath3D(
            ParameterValues,
            Curve.GetValue
        );
    }

    public override bool IsValid()
    {
        throw new NotImplementedException();
    }
}