﻿using System.Diagnostics;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Curves;

public sealed class GrVisualCircleArcCurve3D :
    GrVisualCurveWithAnimation3D
{
    public sealed record KeyFrameRecord(
        int FrameIndex, 
        double Time,
        double Visibility,
        Float64Vector3D Center, 
        Float64Vector3D Direction1,
        Float64Vector3D Direction2,
        double Angle,
        double Radius
    ) : GrVisualAnimatedGeometryKeyFrameRecord(
        FrameIndex,
        Time,
        Visibility
    );

    public static GrVisualCircleArcCurve3D CreateStatic(string name, GrVisualCurveStyle3D style, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, Float64PlanarAngle angle)
    {
        return new GrVisualCircleArcCurve3D(
            name,
            style,
            center,
            direction1,
            direction2,
            angle,
            radius, 
            GrVisualAnimationSpecs.Static
        );
    }

    public static GrVisualCircleArcCurve3D CreateStatic(string name, GrVisualCurveStyle3D style, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, bool innerArc)
    {
        var angle =
            direction1.GetAngle(direction2).Radians.ClampAngleInRadians();

        return innerArc
            ? new GrVisualCircleArcCurve3D(
                name,
                style,
                center,
                direction1,
                direction2,
                angle,
                radius, GrVisualAnimationSpecs.Static) 
            : new GrVisualCircleArcCurve3D(
                name,
                style,
                center,
                direction1,
                direction2.Negative(),
                2d * Math.PI - angle,
                radius, GrVisualAnimationSpecs.Static);
    }

    public static GrVisualCircleArcCurve3D Create(string name, GrVisualCurveStyle3D style, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double angle, double radius, GrVisualAnimationSpecs animationSpecs)
    {
        return new GrVisualCircleArcCurve3D(
            name,
            style,
            center,
            direction1,
            direction2,
            angle,
            radius, animationSpecs);
    }
        
    public static GrVisualCircleArcCurve3D CreateAnimated(string name, GrVisualCurveStyle3D style, GrVisualAnimatedVector3D direction1, GrVisualAnimatedVector3D direction2, GrVisualAnimatedScalar angle, GrVisualAnimatedScalar radius)
    {
        return new GrVisualCircleArcCurve3D(
                name,
                style,
                Float64Vector3D.Zero, 
                Float64Vector3D.E1,
                Float64Vector3D.E2,
                2d * Math.PI,
                1, 
                direction1.AnimationSpecs
            ).SetAnimatedDirection1(direction1)
            .SetAnimatedDirection2(direction2)
            .SetAnimatedAngle(angle)
            .SetAnimatedRadius(radius);
    }

    public static GrVisualCircleArcCurve3D CreateAnimated(string name, GrVisualCurveStyle3D style, GrVisualAnimatedVector3D center, GrVisualAnimatedVector3D direction1, GrVisualAnimatedVector3D direction2, GrVisualAnimatedScalar angle, GrVisualAnimatedScalar radius)
    {
        return new GrVisualCircleArcCurve3D(
                name,
                style,
                Float64Vector3D.Zero, 
                Float64Vector3D.E1,
                Float64Vector3D.E2,
                2d * Math.PI,
                1, 
                center.AnimationSpecs
            ).SetAnimatedCenter(center)
            .SetAnimatedDirection1(direction1)
            .SetAnimatedDirection2(direction2)
            .SetAnimatedAngle(angle)
            .SetAnimatedRadius(radius);
    }


    /// <summary>
    /// The center of the circular arc
    /// </summary>
    public IFloat64Vector3D Center { get; }

    /// <summary>
    /// The unit direction from the center where the arc starts
    /// </summary>
    public Float64Vector3D Direction1 { get; }

    /// <summary>
    /// A unit direction from the center normal to Direction1 in the plane
    /// of the circular arc, these two direction define an orthonormal basis
    /// for the plane of the arc
    /// </summary>
    public Float64Vector3D Direction2 { get; }

    /// <summary>
    /// The radius of the circular arc
    /// </summary>
    public double Radius { get; }

    /// <summary>
    /// The angle of the circular arc starting from Direction1 and rotating in the
    /// direction of Direction2, between 0 and 2*Pi
    /// </summary>
    public Float64PlanarAngle Angle { get; }
        
    public override int PathPointCount 
        => (360 * ArcRatio).RoundToInt32();

    public Float64Vector3D Position1
        => Center + Radius * Direction1;

    public Float64Vector3D Position2
        => Center + Radius * (Angle.Cos() * Direction1 + Angle.Sin() * Direction2);
        
    public Float64Vector3D Normal 
        => Direction1.VectorUnitCross(Direction2);

    public double ArcRatio 
        => Angle / (2d * Math.PI);
        
    public override double Length 
        => Angle * Radius;
        
    public GrVisualAnimatedVector3D? AnimatedCenter { get; set; }

    public GrVisualAnimatedVector3D? AnimatedDirection1 { get; set; }

    public GrVisualAnimatedVector3D? AnimatedDirection2 { get; set; }
        
    public GrVisualAnimatedScalar? AnimatedAngle { get; set; }

    public GrVisualAnimatedScalar? AnimatedRadius { get; set; }


    private GrVisualCircleArcCurve3D(string name, GrVisualCurveStyle3D style, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double angle, double radius, GrVisualAnimationSpecs animationSpecs)
        : base(name, style, animationSpecs)
    {
        Center = center;
        Direction1 = direction1.ToUnitVector();
        Direction2 = direction2.RejectOnUnitVector(Direction1).ToUnitVector();
        Angle = angle.ClampAngleInRadians();
        Radius = radius;

        Debug.Assert(IsValid());
    }

        
    public override bool IsValid()
    {
        return Center.IsValid() &&
               Direction1.IsValid() &&
               Direction2.IsValid() &&
               Radius.IsValid() &&
               Angle.IsValid() &&
               Radius > 0 &&
               Direction1.IsNearUnitVector() &&
               Direction2.IsNearUnitVector() &&
               Direction1.ESp(Direction2).IsNearZero() &&
               GetAnimatedGeometries().All(g => g.IsValid());
    }

    public Triplet<Float64Vector3D> GetArcPointsTriplet()
    {
        var point1 = Position1;
        var point3 = Position2;

        var angle = Angle * 0.5;

        var point2 =
            Center + Radius * (angle.Cos() * Direction1 + angle.Sin() * Direction2);

        return new Triplet<Float64Vector3D>(point1, point2, point3);
    }

    public override IReadOnlyList<GrVisualAnimatedGeometry> GetAnimatedGeometries()
    {
        var animatedGeometries = new List<GrVisualAnimatedGeometry>(2);

        if (AnimatedVisibility is not null)
            animatedGeometries.Add(AnimatedVisibility);

        if (AnimatedCenter is not null)
            animatedGeometries.Add(AnimatedCenter);
            
        if (AnimatedDirection1 is not null)
            animatedGeometries.Add(AnimatedDirection1);
            
        if (AnimatedDirection2 is not null)
            animatedGeometries.Add(AnimatedDirection2);
            
        if (AnimatedAngle is not null)
            animatedGeometries.Add(AnimatedAngle);

        if (AnimatedRadius is not null)
            animatedGeometries.Add(AnimatedRadius);

        return animatedGeometries;
    }

        
    public GrVisualCircleArcCurve3D SetAnimatedVisibility(GrVisualAnimatedScalar visibility)
    {
        AnimatedVisibility = visibility;
            
        return this;
    }

    public GrVisualCircleArcCurve3D SetAnimatedCenter(GrVisualAnimatedVector3D center)
    {
        AnimatedCenter = center;
            
        return this;
    }
        
    public GrVisualCircleArcCurve3D SetAnimatedDirection1(GrVisualAnimatedVector3D direction1)
    {
        AnimatedDirection1 = direction1;
            
        return this;
    }
        
    public GrVisualCircleArcCurve3D SetAnimatedDirection2(GrVisualAnimatedVector3D direction2)
    {
        AnimatedDirection2 = direction2;
            
        return this;
    }
        
    public GrVisualCircleArcCurve3D SetAnimatedAngle(GrVisualAnimatedScalar angle)
    {
        AnimatedAngle = angle;
            
        return this;
    }

    public GrVisualCircleArcCurve3D SetAnimatedRadius(GrVisualAnimatedScalar radius)
    {
        AnimatedRadius = radius;
            
        return this;
    }


    public Float64Vector3D GetCenter(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedCenter is null
            ? Center.ToVector3D()
            : AnimatedCenter.GetPoint(time);
    }
        
    public Float64Vector3D GetDirection1(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedDirection1 is null
            ? Direction1
            : AnimatedDirection1.GetPoint(time).ToUnitVector();
    }
        
    public Float64Vector3D GetDirection2(double time)
    {
        var direction1 = 
            GetDirection1(time);

        var direction2 = 
            AnimationSpecs.IsStatic || AnimatedDirection2 is null
                ? Direction2
                : AnimatedDirection2.GetPoint(time);

        return direction2.RejectOnUnitVector(direction1).ToUnitVector();
    }
        
    public double GetAngle(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedAngle is null
            ? Angle
            : AnimatedAngle.GetValue(time).ClampAngleInRadians();
    }

    public double GetRadius(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedRadius is null
            ? Radius
            : AnimatedRadius.GetValue(time);
    }
        
    public override IPointsPath3D GetPositionsPath()
    {
        var angles =
            0d.GetLinearRange(Angle, PathPointCount, false);

        var points =
            angles.Select(angle => 
                Center + Radius * (angle.Cos() * Direction1 + angle.Sin() * Direction2)
            );

        return new ArrayPointsPath3D(points);
    }

    public override IPointsPath3D GetPositionsPath(double time)
    {
        var center = GetCenter(time);
        var direction1 = GetDirection1(time);
        var direction2 = GetDirection2(time);
        var angle = GetAngle(time);
        var radius = GetRadius(time);
            
        var angles =
            0d.GetLinearRange(angle, PathPointCount, false);

        var points =
            angles.Select(a => 
                center + radius * (a.Cos() * direction1 + a.Sin() * direction2)
            );

        return new ArrayPointsPath3D(points);
    }

    public IEnumerable<KeyFrameRecord> GetKeyFrameRecords()
    {
        Debug.Assert(IsValid());

        foreach (var frameIndex in GetValidFrameIndexSet())
        {
            var time = (double)frameIndex / AnimationSpecs.FrameRate;
                
            yield return new KeyFrameRecord(
                frameIndex, 
                time, 
                GetVisibility(time),
                GetCenter(time), 
                GetDirection1(time),
                GetDirection2(time),
                GetAngle(time),
                GetRadius(time)
            );
        }
    }
        
}