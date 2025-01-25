using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Curves;

public sealed class GrVisualCircleArcCurve3D :
    GrVisualCurveWithAnimation3D
{
    public sealed record KeyFrameRecord(
        int FrameIndex, 
        double Time,
        double Visibility,
        LinFloat64Vector3D Center, 
        LinFloat64Vector3D Direction1,
        LinFloat64Vector3D Direction2,
        LinFloat64Angle Angle,
        double Radius
    ) : GrVisualAnimatedGeometryKeyFrameRecord(
        FrameIndex,
        Time,
        Visibility
    );

    public static GrVisualCircleArcCurve3D CreateStatic(string name, GrVisualCurveStyle3D style, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, LinFloat64Angle angle)
    {
        return new GrVisualCircleArcCurve3D(
            name,
            style,
            center,
            direction1,
            direction2,
            angle,
            radius, 
            Float64SamplingSpecs.Static
        );
    }

    public static GrVisualCircleArcCurve3D CreateStatic(string name, GrVisualCurveStyle3D style, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, bool innerArc)
    {
        var angle =
            direction1.GetAngle(direction2);

        return innerArc
            ? new GrVisualCircleArcCurve3D(
                name,
                style,
                center,
                direction1,
                direction2,
                angle,
                radius, Float64SamplingSpecs.Static) 
            : new GrVisualCircleArcCurve3D(
                name,
                style,
                center,
                direction1,
                direction2.VectorNegative(),
                (2d * Math.PI - angle.RadiansValue).RadiansToPolarAngle(),
                radius, Float64SamplingSpecs.Static);
    }

    public static GrVisualCircleArcCurve3D Create(string name, GrVisualCurveStyle3D style, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, LinFloat64Angle angle, double radius, Float64SamplingSpecs samplingSpecs)
    {
        return new GrVisualCircleArcCurve3D(
            name,
            style,
            center,
            direction1,
            direction2,
            angle,
            radius, samplingSpecs);
    }
        
    public static GrVisualCircleArcCurve3D CreateAnimated(string name, GrVisualCurveStyle3D style, GrVisualAnimatedVector3D direction1, GrVisualAnimatedVector3D direction2, GrVisualAnimatedScalar angle, GrVisualAnimatedScalar radius)
    {
        return new GrVisualCircleArcCurve3D(
                name,
                style,
                LinFloat64Vector3D.Zero, 
                LinFloat64Vector3D.E1,
                LinFloat64Vector3D.E2,
                LinFloat64DirectedAngle.Angle360, 
                1, 
                direction1.SamplingSpecs
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
                LinFloat64Vector3D.Zero, 
                LinFloat64Vector3D.E1,
                LinFloat64Vector3D.E2,
                LinFloat64DirectedAngle.Angle360,
                1, 
                center.SamplingSpecs
            ).SetAnimatedCenter(center)
            .SetAnimatedDirection1(direction1)
            .SetAnimatedDirection2(direction2)
            .SetAnimatedAngle(angle)
            .SetAnimatedRadius(radius);
    }


    /// <summary>
    /// The center of the circular arc
    /// </summary>
    public ILinFloat64Vector3D Center { get; }

    /// <summary>
    /// The unit direction from the center where the arc starts
    /// </summary>
    public LinFloat64Vector3D Direction1 { get; }

    /// <summary>
    /// A unit direction from the center normal to Direction1 in the plane
    /// of the circular arc, these two direction define an orthonormal basis
    /// for the plane of the arc
    /// </summary>
    public LinFloat64Vector3D Direction2 { get; }

    /// <summary>
    /// The radius of the circular arc
    /// </summary>
    public double Radius { get; }

    /// <summary>
    /// The angle of the circular arc starting from Direction1 and rotating in the
    /// direction of Direction2, between 0 and 2*Pi
    /// </summary>
    public LinFloat64Angle Angle { get; }
        
    public override int PathPointCount 
        => (360 * ArcRatio).RoundToInt32();

    public LinFloat64Vector3D Position1
        => Center + Radius * Direction1;

    public LinFloat64Vector3D Position2
        => Center + Radius * (Angle.Cos() * Direction1 + Angle.Sin() * Direction2);
        
    public LinFloat64Vector3D Normal 
        => Direction1.VectorUnitCross(Direction2);

    public double ArcRatio 
        => Angle.RadiansValue / (2d * Math.PI);
        
    public override double Length 
        => Angle.RadiansValue * Radius;
        
    public GrVisualAnimatedVector3D? AnimatedCenter { get; set; }

    public GrVisualAnimatedVector3D? AnimatedDirection1 { get; set; }

    public GrVisualAnimatedVector3D? AnimatedDirection2 { get; set; }
        
    public GrVisualAnimatedScalar? AnimatedAngle { get; set; }

    public GrVisualAnimatedScalar? AnimatedRadius { get; set; }


    private GrVisualCircleArcCurve3D(string name, GrVisualCurveStyle3D style, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, LinFloat64Angle angle, double radius, Float64SamplingSpecs samplingSpecs)
        : base(name, style, samplingSpecs)
    {
        Center = center;
        Direction1 = direction1.ToUnitLinVector3D();
        Direction2 = direction2.RejectOnUnitVector(Direction1).ToUnitLinVector3D();
        Angle = angle;
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
               Direction1.VectorESp(Direction2).IsNearZero() &&
               GetAnimatedGeometries().All(g => g.IsValid());
    }

    public Triplet<LinFloat64Vector3D> GetArcPointsTriplet()
    {
        var point1 = Position1;
        var point3 = Position2;

        var (angleCos, angleSin) = Angle.HalfPolarAngle();

        var point2 =
            Center + Radius * (angleCos * Direction1 + angleSin * Direction2);

        return new Triplet<LinFloat64Vector3D>(point1, point2, point3);
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


    public LinFloat64Vector3D GetCenter(double time)
    {
        return SamplingSpecs.IsStatic || AnimatedCenter is null
            ? Center.ToLinVector3D()
            : AnimatedCenter.GetPoint(time);
    }
        
    public LinFloat64Vector3D GetDirection1(double time)
    {
        return SamplingSpecs.IsStatic || AnimatedDirection1 is null
            ? Direction1
            : AnimatedDirection1.GetPoint(time).ToUnitLinVector3D();
    }
        
    public LinFloat64Vector3D GetDirection2(double time)
    {
        var direction1 = 
            GetDirection1(time);

        var direction2 = 
            SamplingSpecs.IsStatic || AnimatedDirection2 is null
                ? Direction2
                : AnimatedDirection2.GetPoint(time);

        return direction2.RejectOnUnitVector(direction1).ToUnitLinVector3D();
    }
        
    public LinFloat64Angle GetAngle(double time)
    {
        return SamplingSpecs.IsStatic || AnimatedAngle is null
            ? Angle
            : AnimatedAngle.GetValue(time).RadiansToPolarAngle();
    }

    public double GetRadius(double time)
    {
        return SamplingSpecs.IsStatic || AnimatedRadius is null
            ? Radius
            : AnimatedRadius.GetValue(time);
    }
        
    public override IPointsPath3D GetPositionsPath()
    {
        var angles =
            0d.GetLinearRange(Angle.RadiansValue, PathPointCount, false);

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
            0d.GetLinearRange(angle.RadiansValue, PathPointCount, false);

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
            var time = (double)frameIndex / SamplingSpecs.SamplingRate;
                
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