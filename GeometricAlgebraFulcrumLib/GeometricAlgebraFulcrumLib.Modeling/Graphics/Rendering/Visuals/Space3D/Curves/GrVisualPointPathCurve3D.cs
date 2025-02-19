﻿using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Signals;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Curves;

public sealed class GrVisualPointPathCurve3D :
    GrVisualCurveWithAnimation3D
{
    public sealed record KeyFrameRecord(
        int FrameIndex, 
        double Time,
        double Visibility,
        IPointsPath3D PositionPath
    ) : GrVisualAnimatedGeometryKeyFrameRecord(
        FrameIndex,
        Time,
        Visibility
    );


    public static GrVisualPointPathCurve3D CreateStatic(string name, GrVisualCurveStyle3D style, params ILinFloat64Vector3D[] positionList)
    {
        return new GrVisualPointPathCurve3D(
            name,
            style,
            new ArrayPointsPath3D(positionList),
            Float64SamplingSpecs.Static
        );
    }

    public static GrVisualPointPathCurve3D CreateStatic(string name, GrVisualCurveStyle3D style, IReadOnlyList<ILinFloat64Vector3D> positionList)
    {
        return new GrVisualPointPathCurve3D(
            name,
            style,
            new ArrayPointsPath3D(positionList),
            Float64SamplingSpecs.Static
        );
    }
        
    public static GrVisualPointPathCurve3D CreateStatic(string name, GrVisualCurveStyle3D style, IPointsPath3D positionList)
    {
        return new GrVisualPointPathCurve3D(
            name,
            style,
            positionList,
            Float64SamplingSpecs.Static
        );
    }

    public static GrVisualPointPathCurve3D Create(string name, GrVisualCurveStyle3D style, IEnumerable<ILinFloat64Vector3D> positionList, Float64SamplingSpecs samplingSpecs)
    {
        return new GrVisualPointPathCurve3D(
            name,
            style,
            new ArrayPointsPath3D(positionList),
            samplingSpecs
        );
    }
        
    public static GrVisualPointPathCurve3D Create(string name, GrVisualCurveStyle3D style, IPointsPath3D positionList, Float64SamplingSpecs samplingSpecs)
    {
        return new GrVisualPointPathCurve3D(
            name,
            style,
            positionList,
            samplingSpecs
        );
    }
        
    public static GrVisualPointPathCurve3D CreateAnimated(string name, GrVisualCurveStyle3D style, GrVisualAnimatedVectorPath3D positionPath)
    {
        return new GrVisualPointPathCurve3D(
            name,
            style,
            positionPath.GetPointsPath(positionPath.SamplingSpecs.MinTime),
            positionPath.SamplingSpecs
        ).SetAnimatedPositionPath(positionPath);
    }
        
        
    public int PointCount 
        => PositionPath.Count;

    public IPointsPath3D PositionPath { get; }

    public override int PathPointCount 
        => PositionPath.Count;

    public override double Length
    {
        get
        {
            var length = 0d;
            var position1 = PositionPath[0];

            for (var i = 1; i < PositionPath.Count; i++)
            {
                var position2 = PositionPath[i];

                length += position1.GetDistanceToPoint(position2);

                position1 = position2;
            }

            return length;
        }
    }

    public GrVisualAnimatedVectorPath3D? AnimatedPositionPath { get; set; }

        
    private GrVisualPointPathCurve3D(string name, GrVisualCurveStyle3D style, int pointCount, Float64SamplingSpecs samplingSpecs)
        : base(name, style, samplingSpecs)
    {
        PositionPath = new ArrayPointsPath3D(pointCount);

        Debug.Assert(IsValid());
    }

    private GrVisualPointPathCurve3D(string name, GrVisualCurveStyle3D style, IPointsPath3D positionPath, Float64SamplingSpecs samplingSpecs)
        : base(name, style, samplingSpecs)
    {
        PositionPath = positionPath;

        //Debug.Assert(IsValid());
    }
        
        
    public override bool IsValid()
    {
        return PositionPath.All(position => position.IsValid()) &&
               AnimatedPositionPath.IsNullOrValid(SamplingSpecs.TimeRange);
    }


    public GrVisualPointPathCurve3D SetAnimatedVisibility(GrVisualAnimatedScalar visibility)
    {
        AnimatedVisibility = visibility;

        return this;
    }

    public GrVisualPointPathCurve3D SetAnimatedPositionPath(GrVisualAnimatedVectorPath3D positionPath)
    {
        AnimatedPositionPath = positionPath;

        return this;
    }
        
    public override IReadOnlyList<GrVisualAnimatedGeometry> GetAnimatedGeometries()
    {
        var animatedGeometries = new List<GrVisualAnimatedGeometry>(2);

        if (AnimatedVisibility is not null)
            animatedGeometries.Add(AnimatedVisibility);

        if (AnimatedPositionPath is not null)
            animatedGeometries.Add(AnimatedPositionPath);

        return animatedGeometries;
    }
        
    public override IPointsPath3D GetPositionsPath()
    {
        return PositionPath;
    }

    public override IPointsPath3D GetPositionsPath(double time)
    {
        return SamplingSpecs.IsStatic || AnimatedPositionPath is null
            ? PositionPath
            : AnimatedPositionPath.GetPointsPath(time);
    }

    public IEnumerable<KeyFrameRecord> GetKeyFrameRecords()
    {
        Debug.Assert(IsValid());

        foreach (var frameIndex in GetValidFrameIndexSet())
        {
            var time = (double)frameIndex / SamplingSpecs.SamplingRate;
                
            yield return new KeyFrameRecord(
                frameIndex, 
                time, 
                GetVisibility(time),
                GetPositionsPath(time)
            );
        }
    }
        
}