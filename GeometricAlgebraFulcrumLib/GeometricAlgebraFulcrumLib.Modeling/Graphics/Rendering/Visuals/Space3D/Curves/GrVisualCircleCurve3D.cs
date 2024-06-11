using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Curves;

public sealed class GrVisualCircleCurve3D :
    GrVisualCurveWithAnimation3D
{
    public sealed record KeyFrameRecord(
        int FrameIndex, 
        double Time,
        double Visibility,
        LinFloat64Vector3D Center, 
        LinFloat64Vector3D Normal,
        double Radius
    ) : GrVisualAnimatedGeometryKeyFrameRecord(
        FrameIndex,
        Time,
        Visibility
    );


    public static GrVisualCircleCurve3D CreateStatic(string name, GrVisualCurveStyle3D style, ILinFloat64Vector3D normal, double radius)
    {
        return new GrVisualCircleCurve3D(
            name,
            style,
            LinFloat64Vector3D.Zero, 
            normal,
            radius,
            GrVisualAnimationSpecs.Static
        );
    }

    public static GrVisualCircleCurve3D CreateStatic(string name, GrVisualCurveStyle3D style, ILinFloat64Vector3D center, ILinFloat64Vector3D normal, double radius)
    {
        return new GrVisualCircleCurve3D(
            name,
            style,
            center,
            normal,
            radius,
            GrVisualAnimationSpecs.Static
        );
    }
        
    public static GrVisualCircleCurve3D Create(string name, GrVisualCurveStyle3D style, ILinFloat64Vector3D normal, double radius, GrVisualAnimationSpecs animationSpecs)
    {
        return new GrVisualCircleCurve3D(
            name,
            style,
            LinFloat64Vector3D.Zero, 
            normal,
            radius,
            animationSpecs
        );
    }

    public static GrVisualCircleCurve3D Create(string name, GrVisualCurveStyle3D style, ILinFloat64Vector3D center, ILinFloat64Vector3D normal, double radius, GrVisualAnimationSpecs animationSpecs)
    {
        return new GrVisualCircleCurve3D(
            name,
            style,
            center,
            normal,
            radius,
            animationSpecs
        );
    }
        
    public static GrVisualCircleCurve3D CreateAnimated(string name, GrVisualCurveStyle3D style, GrVisualAnimatedVector3D normal, GrVisualAnimatedScalar radius)
    {
        return new GrVisualCircleCurve3D(
                name,
                style,
                LinFloat64Vector3D.Zero, 
                LinFloat64Vector3D.E2, 
                1d,
                normal.AnimationSpecs
            ).SetAnimatedNormal(normal)
            .SetAnimatedRadius(radius);
    }
        
    public static GrVisualCircleCurve3D CreateAnimated(string name, GrVisualCurveStyle3D style, GrVisualAnimatedVector3D center, LinFloat64Vector3D normal, GrVisualAnimatedScalar radius)
    {
        return new GrVisualCircleCurve3D(
                name,
                style,
                LinFloat64Vector3D.Zero, 
                normal, 
                1d,
                center.AnimationSpecs
            ).SetAnimatedCenter(center)
            .SetAnimatedRadius(radius);
    }
        
    public static GrVisualCircleCurve3D CreateAnimated(string name, GrVisualCurveStyle3D style, LinFloat64Vector3D center, LinFloat64Vector3D normal, GrVisualAnimatedScalar radius)
    {
        return new GrVisualCircleCurve3D(
            name,
            style,
            center, 
            normal, 
            1d,
            radius.AnimationSpecs
        ).SetAnimatedRadius(radius);
    }

    public static GrVisualCircleCurve3D CreateAnimated(string name, GrVisualCurveStyle3D style, LinFloat64Vector3D center, GrVisualAnimatedVector3D normal, double radius)
    {
        return new GrVisualCircleCurve3D(
            name,
            style,
            center, 
            LinFloat64Vector3D.E2, 
            radius,
            normal.AnimationSpecs
        ).SetAnimatedNormal(normal);
    }
        
    public static GrVisualCircleCurve3D CreateAnimated(string name, GrVisualCurveStyle3D style, LinFloat64Vector3D center, GrVisualAnimatedVector3D normal, GrVisualAnimatedScalar radius)
    {
        return new GrVisualCircleCurve3D(
                name,
                style,
                center, 
                LinFloat64Vector3D.E2, 
                1d,
                normal.AnimationSpecs
            ).SetAnimatedNormal(normal)
            .SetAnimatedRadius(radius);
    }

    public static GrVisualCircleCurve3D CreateAnimated(string name, GrVisualCurveStyle3D style, GrVisualAnimatedVector3D center, GrVisualAnimatedVector3D normal, double radius)
    {
        return new GrVisualCircleCurve3D(
                name,
                style,
                LinFloat64Vector3D.Zero, 
                LinFloat64Vector3D.E2, 
                radius,
                center.AnimationSpecs
            ).SetAnimatedCenter(center)
            .SetAnimatedNormal(normal);
    }

    public static GrVisualCircleCurve3D CreateAnimated(string name, GrVisualCurveStyle3D style, GrVisualAnimatedVector3D center, GrVisualAnimatedVector3D normal, GrVisualAnimatedScalar radius)
    {
        return new GrVisualCircleCurve3D(
                name,
                style,
                LinFloat64Vector3D.Zero, 
                LinFloat64Vector3D.E2, 
                1d,
                center.AnimationSpecs
            ).SetAnimatedCenter(center)
            .SetAnimatedNormal(normal)
            .SetAnimatedRadius(radius);
    }


    //public GrVisualCurveStyle3D Style { get; }

    public ILinFloat64Vector3D Center { get; }

    public LinFloat64Vector3D Normal { get; }

    public double Radius { get; }

    public override int PathPointCount 
        => 361;

    public override double Length 
        => 2d * Math.PI * Radius;

    public GrVisualAnimatedVector3D? AnimatedCenter { get; set; }

    public GrVisualAnimatedVector3D? AnimatedNormal { get; set; }

    public GrVisualAnimatedScalar? AnimatedRadius { get; set; }
        
        
    private GrVisualCircleCurve3D(string name, GrVisualCurveStyle3D style, ILinFloat64Vector3D center, ILinFloat64Vector3D normal, double radius, GrVisualAnimationSpecs animationSpecs) 
        : base(name, style, animationSpecs)
    {
        //Style = style;
        Center = center;
        Normal = normal.ToUnitLinVector3D();
        Radius = radius;

        Debug.Assert(IsValid());
    }

        
    public override bool IsValid()
    {
        return Center.IsValid() &&
               Normal.IsValid() &&
               Normal.IsNearUnitVector() &&
               Radius.IsValid() &&
               Radius > 0 &&
               GetAnimatedGeometries().All(g => g.IsValid());
    }
        
    public override IReadOnlyList<GrVisualAnimatedGeometry> GetAnimatedGeometries()
    {
        var animatedGeometries = new List<GrVisualAnimatedGeometry>(2);

        if (AnimatedVisibility is not null)
            animatedGeometries.Add(AnimatedVisibility);

        if (AnimatedCenter is not null)
            animatedGeometries.Add(AnimatedCenter);
            
        if (AnimatedNormal is not null)
            animatedGeometries.Add(AnimatedNormal);

        if (AnimatedRadius is not null)
            animatedGeometries.Add(AnimatedRadius);

        return animatedGeometries;
    }
        
    public override IPointsPath3D GetPositionsPath()
    {
        var (direction1, direction2) = 
            Normal.GetUnitNormalPair();

        var angles = 
            0d.GetLinearRange(2d * Math.PI, PathPointCount, false);

        var points =
            angles.Select(angle => 
                Center + Radius * (angle.Cos() * direction1 + angle.Sin() * direction2)
            );

        return new ArrayPointsPath3D(points);
    }

    public override IPointsPath3D GetPositionsPath(double time)
    {
        var center = GetCenter(time);
        var normal = GetNormal(time);
        var radius = GetRadius(time);

        var (direction1, direction2) = 
            normal.GetUnitNormalPair();

        var angles = 
            0d.GetLinearRange(2d * Math.PI, PathPointCount, false);

        var points =
            angles.Select(angle => 
                center + radius * (angle.Cos() * direction1 + angle.Sin() * direction2)
            );

        return new ArrayPointsPath3D(points);
    }

    public GrVisualCircleCurve3D SetAnimatedVisibility(GrVisualAnimatedScalar visibility)
    {
        AnimatedVisibility = visibility;
            
        return this;
    }

    public GrVisualCircleCurve3D SetAnimatedCenter(GrVisualAnimatedVector3D center)
    {
        AnimatedCenter = center;
            
        return this;
    }
        
    public GrVisualCircleCurve3D SetAnimatedNormal(GrVisualAnimatedVector3D normal)
    {
        AnimatedNormal = normal;
            
        return this;
    }
        
    public GrVisualCircleCurve3D SetAnimatedRadius(GrVisualAnimatedScalar radius)
    {
        AnimatedRadius = radius;
            
        return this;
    }

    public LinFloat64Vector3D GetCenter(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedCenter is null
            ? Center.ToLinVector3D()
            : AnimatedCenter.GetPoint(time);
    }
        
    public LinFloat64Vector3D GetNormal(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedNormal is null
            ? Normal
            : AnimatedNormal.GetPoint(time).ToUnitLinVector3D();
    }
        
    public double GetRadius(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedRadius is null
            ? Radius
            : AnimatedRadius.GetValue(time);
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
                GetNormal(time),
                GetRadius(time)
            );
        }
    }

}