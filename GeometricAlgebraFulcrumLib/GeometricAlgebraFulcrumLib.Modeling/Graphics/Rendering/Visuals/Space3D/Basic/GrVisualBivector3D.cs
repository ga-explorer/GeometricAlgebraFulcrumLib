using System.Collections;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Surfaces;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Basic;

public sealed class GrVisualBivector3D :
    GrVisualElementWithAnimation3D,
    IGrVisualElementList3D
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


    public static GrVisualBivector3D CreateStatic(string name, GrVisualCurveTubeStyle3D style, ILinFloat64Vector3D normal, double radius)
    {
        return new GrVisualBivector3D(
            name,
            style,
            LinFloat64Vector3D.Zero,
            normal,
            radius,
            Float64SamplingSpecs.Static
        );
    }

    public static GrVisualBivector3D CreateStatic(string name, GrVisualCurveTubeStyle3D style, ILinFloat64Vector3D center, ILinFloat64Vector3D normal, double radius)
    {
        return new GrVisualBivector3D(
            name,
            style,
            center,
            normal,
            radius,
            Float64SamplingSpecs.Static
        );
    }

    public static GrVisualBivector3D Create(string name, GrVisualCurveTubeStyle3D style, ILinFloat64Vector3D normal, double radius, Float64SamplingSpecs samplingSpecs)
    {
        return new GrVisualBivector3D(
            name,
            style,
            LinFloat64Vector3D.Zero,
            normal,
            radius,
            samplingSpecs
        );
    }

    public static GrVisualBivector3D Create(string name, GrVisualCurveTubeStyle3D style, ILinFloat64Vector3D center, ILinFloat64Vector3D normal, double radius, Float64SamplingSpecs samplingSpecs)
    {
        return new GrVisualBivector3D(
            name,
            style,
            center,
            normal,
            radius,
            samplingSpecs
        );
    }

    public static GrVisualBivector3D CreateAnimated(string name, GrVisualCurveTubeStyle3D style, GrVisualAnimatedVector3D normal, GrVisualAnimatedScalar radius)
    {
        return new GrVisualBivector3D(
            name,
            style,
            LinFloat64Vector3D.Zero,
            LinFloat64Vector3D.E2,
            1d,
            normal.SamplingSpecs
        ).SetAnimatedNormal(normal)
            .SetAnimatedRadius(radius);
    }

    public static GrVisualBivector3D CreateAnimated(string name, GrVisualCurveTubeStyle3D style, GrVisualAnimatedVector3D center, LinFloat64Vector3D normal, GrVisualAnimatedScalar radius)
    {
        return new GrVisualBivector3D(
                name,
                style,
                LinFloat64Vector3D.Zero,
                normal,
                1d,
                center.SamplingSpecs
            ).SetAnimatedCenter(center)
            .SetAnimatedRadius(radius);
    }

    public static GrVisualBivector3D CreateAnimated(string name, GrVisualCurveTubeStyle3D style, GrVisualAnimatedVector3D center, GrVisualAnimatedVector3D normal, double radius)
    {
        return new GrVisualBivector3D(
                name,
                style,
                LinFloat64Vector3D.Zero,
                LinFloat64Vector3D.E2,
                radius,
                center.SamplingSpecs
            ).SetAnimatedCenter(center)
            .SetAnimatedNormal(normal);
    }

    public static GrVisualBivector3D CreateAnimated(string name, GrVisualCurveTubeStyle3D style, GrVisualAnimatedVector3D center, GrVisualAnimatedVector3D normal, GrVisualAnimatedScalar radius)
    {
        return new GrVisualBivector3D(
            name,
            style,
            LinFloat64Vector3D.Zero,
            LinFloat64Vector3D.E2,
            1d,
            center.SamplingSpecs
        ).SetAnimatedCenter(center)
            .SetAnimatedNormal(normal)
            .SetAnimatedRadius(radius);
    }


    public GrVisualCurveTubeStyle3D VectorStyle { get; }

    public GrVisualSurfaceThickStyle3D CircleStyle { get; }

    public ILinFloat64Vector3D Center { get; }

    public LinFloat64Vector3D Normal { get; }

    public double Radius { get; }

    public bool DrawLineSegments { get; set; } = false;


    public GrVisualAnimatedVector3D? AnimatedCenter { get; set; }

    public GrVisualAnimatedVector3D? AnimatedNormal { get; set; }

    public GrVisualAnimatedScalar? AnimatedRadius { get; set; }

    public int Count
        => DrawLineSegments ? 7 : 4;

    public IGrVisualElement3D this[int index]
    {
        get
        {
            if (DrawLineSegments)
                return index switch
                {
                    0 => GetVisualVector1(),
                    1 => GetVisualVector2(),
                    2 => GetVisualVector3(),
                    3 => GetVisualCircleSurface(),
                    4 => GetVisualLineSegment1(),
                    5 => GetVisualLineSegment2(),
                    6 => GetVisualLineSegment3(),
                    _ => throw new IndexOutOfRangeException()
                };

            return index switch
            {
                0 => GetVisualVector1(),
                1 => GetVisualVector2(),
                2 => GetVisualVector3(),
                3 => GetVisualCircleSurface(),
                _ => throw new IndexOutOfRangeException()
            };
        }
    }


    private GrVisualBivector3D(string name, GrVisualCurveTubeStyle3D style, ILinFloat64Vector3D center, ILinFloat64Vector3D normal, double radius, Float64SamplingSpecs samplingSpecs)
        : base(name, samplingSpecs)
    {
        VectorStyle = style;
        CircleStyle = new GrVisualSurfaceThickStyle3D(style.Material, style.Thickness);
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

    public Triplet<LinFloat64Vector3D> GetPointsTriplet()
    {
        var quaternion = LinBasisVector3D.Pz.VectorToVectorRotationQuaternion(
            Normal.ToUnitLinVector3D()
        );

        const double angle = 2d * Math.PI / 3d;

        var a = Radius * Math.Cos(angle);
        var b = Radius * Math.Sin(angle);

        var point1 = Center + quaternion.RotateVector(Radius, 0, 0);
        var point2 = Center + quaternion.RotateVector(a, b, 0);
        var point3 = Center + quaternion.RotateVector(a, -b, 0);

        return new Triplet<LinFloat64Vector3D>(point1, point2, point3);
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


    public LinFloat64Vector3D GetDirection(LinFloat64DirectedAngle angle1, LinFloat64DirectedAngle angle2)
    {
        var (e1, e2) =
            Normal.GetUnitNormalPair();

        var v1 = Radius * (angle1.Cos() * e1 + angle1.Sin() * e2);
        var v2 = Radius * (angle2.Cos() * e1 + angle2.Sin() * e2);

        return v2 - v1;
    }

    public LinFloat64Vector3D GetDirection(double time, LinFloat64DirectedAngle angle1, LinFloat64DirectedAngle angle2)
    {
        var radius = GetRadius(time);

        var (e1, e2) =
            GetNormal(time).GetUnitNormalPair();

        var v1 = radius * (angle1.Cos() * e1 + angle1.Sin() * e2);
        var v2 = radius * (angle2.Cos() * e1 + angle2.Sin() * e2);

        return v2 - v1;
    }

    public LinFloat64Vector3D GetPosition(LinFloat64DirectedAngle angle)
    {
        var (e1, e2) =
            Normal.GetUnitNormalPair();

        return Center + Radius * (
            angle.Cos() * e1 +
            angle.Sin() * e2
        );
    }

    public LinFloat64Vector3D GetPosition(double time, LinFloat64DirectedAngle angle)
    {
        var center = GetCenter(time);
        var radius = GetRadius(time);
        var (e1, e2) =
            GetNormal(time).GetUnitNormalPair();

        return center + radius * (
            angle.Cos() * e1 +
            angle.Sin() * e2
        );
    }

    public GrVisualVector3D GetVisualVector1()
    {
        var point1 =
            GetPosition(LinFloat64DirectedAngle.Angle240);

        var point2 =
            GetPosition(LinFloat64DirectedAngle.Angle300);

        var vector = GrVisualVector3D.Create(
            $"{Name}Vector1",
            VectorStyle,
            point1,
            point2 - point1,
            SamplingSpecs
        );

        vector.Visibility = Visibility;

        if (SamplingSpecs.IsStatic) return vector;
        
        vector.AnimatedVisibility = AnimatedVisibility;

        vector.AnimatedOrigin =
            SamplingSpecs.CreateAnimatedVector3D(
                t => GetPosition(t, LinFloat64DirectedAngle.Angle240)
            );

        vector.AnimatedDirection =
            SamplingSpecs.CreateAnimatedVector3D(
                t => GetDirection(t, LinFloat64DirectedAngle.Angle240, LinFloat64DirectedAngle.Angle300)
            );
        
        //vector.AddInvalidFrameIndices(
        //    GetInvalidFrameIndices()
        //);

        return vector;
    }

    public GrVisualVector3D GetVisualVector2()
    {
        var point1 =
            GetPosition(LinFloat64DirectedAngle.Angle0);

        var point2 =
            GetPosition(LinFloat64DirectedAngle.Angle60);

        var vector = GrVisualVector3D.Create(
            $"{Name}Vector2",
            VectorStyle,
            point1,
            point2 - point1,
            SamplingSpecs
        );

        vector.Visibility = Visibility;
        
        if (SamplingSpecs.IsStatic) return vector;
        
        vector.AnimatedVisibility = AnimatedVisibility;

        vector.AnimatedOrigin =
            SamplingSpecs.CreateAnimatedVector3D(
                t => GetPosition(t, LinFloat64DirectedAngle.Angle0)
            );

        vector.AnimatedDirection =
            SamplingSpecs.CreateAnimatedVector3D(
                t => GetDirection(t, LinFloat64DirectedAngle.Angle0, LinFloat64DirectedAngle.Angle60)
            );
        
        //vector.AddInvalidFrameIndices(
        //    GetInvalidFrameIndices()
        //);

        return vector;
    }

    public GrVisualVector3D GetVisualVector3()
    {
        var point1 =
            GetPosition(LinFloat64DirectedAngle.Angle120);

        var point2 =
            GetPosition(LinFloat64DirectedAngle.Angle180);

        var vector = GrVisualVector3D.Create(
            $"{Name}Vector3",
            VectorStyle,
            point1,
            point2 - point1,
            SamplingSpecs
        );
        
        vector.Visibility = Visibility;

        if (SamplingSpecs.IsStatic) return vector;
        
        vector.AnimatedVisibility = AnimatedVisibility;

        vector.AnimatedOrigin =
            SamplingSpecs.CreateAnimatedVector3D(
                t => GetPosition(t, LinFloat64DirectedAngle.Angle120)
            );

        vector.AnimatedDirection =
            SamplingSpecs.CreateAnimatedVector3D(
                t => GetDirection(t, LinFloat64DirectedAngle.Angle120, LinFloat64DirectedAngle.Angle180)
            );
        
        //vector.AddInvalidFrameIndices(
        //    GetInvalidFrameIndices()
        //);

        return vector;
    }

    public GrVisualCircleSurface3D GetVisualCircleSurface()
    {
        var circle = GrVisualCircleSurface3D.Create(
            $"{Name}Disc",
            CircleStyle,
            Center,
            Normal,
            Radius,
            false,
            SamplingSpecs
        );
        
        circle.Visibility = Visibility;

        if (SamplingSpecs.IsStatic) return circle;
        
        circle.AnimatedVisibility = AnimatedVisibility;

        circle.AnimatedCenter = AnimatedCenter;
        circle.AnimatedNormal = AnimatedNormal;
        circle.AnimatedRadius = AnimatedRadius;

        //circle.AddInvalidFrameIndices(GetInvalidFrameIndices());

        return circle;
    }

    public GrVisualLineSegment3D GetVisualLineSegment1()
    {
        var point2 =
            GetPosition(LinFloat64DirectedAngle.Angle0);

        var lineSegment = GrVisualLineSegment3D.Create(
            $"{Name}LineSegment1",
            VectorStyle,
            Center,
            point2,
            SamplingSpecs
        );
        
        lineSegment.Visibility = Visibility;

        if (SamplingSpecs.IsStatic) return lineSegment;

        lineSegment.AnimatedVisibility = AnimatedVisibility;

        lineSegment.AnimatedPosition1 = AnimatedCenter;

        lineSegment.AnimatedPosition2 =
            SamplingSpecs.CreateAnimatedVector3D(
                t => GetPosition(t, LinFloat64DirectedAngle.Angle240)
            );
        
        //lineSegment.AddInvalidFrameIndices(
        //    GetInvalidFrameIndices()
        //);

        return lineSegment;
    }

    public GrVisualLineSegment3D GetVisualLineSegment2()
    {
        var point2 =
            GetPosition(LinFloat64DirectedAngle.Angle0);

        var lineSegment = GrVisualLineSegment3D.Create(
            $"{Name}LineSegment2",
            VectorStyle,
            Center,
            point2,
            SamplingSpecs
        );

        
        lineSegment.Visibility = Visibility;

        if (SamplingSpecs.IsStatic) return lineSegment;

        lineSegment.AnimatedVisibility = AnimatedVisibility;

        lineSegment.AnimatedPosition1 = AnimatedCenter;

        lineSegment.AnimatedPosition2 =
            SamplingSpecs.CreateAnimatedVector3D(
                t => GetPosition(t, LinFloat64DirectedAngle.Angle0)
            );
        
        //lineSegment.AddInvalidFrameIndices(
        //    GetInvalidFrameIndices()
        //);

        return lineSegment;
    }

    public GrVisualLineSegment3D GetVisualLineSegment3()
    {
        var point2 =
            GetPosition(LinFloat64DirectedAngle.Angle240);

        var lineSegment = GrVisualLineSegment3D.Create(
            $"{Name}LineSegment3",
            VectorStyle,
            Center,
            point2,
            SamplingSpecs
        );

        
        lineSegment.Visibility = Visibility;

        if (SamplingSpecs.IsStatic) return lineSegment;

        lineSegment.AnimatedVisibility = AnimatedVisibility;

        lineSegment.AnimatedPosition1 = AnimatedCenter;

        lineSegment.AnimatedPosition2 =
            SamplingSpecs.CreateAnimatedVector3D(
                t => GetPosition(t, LinFloat64DirectedAngle.Angle120)
            );
        
        //lineSegment.AddInvalidFrameIndices(
        //    GetInvalidFrameIndices()
        //);

        return lineSegment;
    }

    public GrVisualBivector3D SetAnimatedVisibility(GrVisualAnimatedScalar visibility)
    {
        AnimatedVisibility = visibility;

        return this;
    }

    public GrVisualBivector3D SetAnimatedCenter(GrVisualAnimatedVector3D center)
    {
        AnimatedCenter = center;

        return this;
    }

    public GrVisualBivector3D SetAnimatedNormal(GrVisualAnimatedVector3D normal)
    {
        AnimatedNormal = normal;

        return this;
    }

    public GrVisualBivector3D SetAnimatedRadius(GrVisualAnimatedScalar radius)
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

    public LinFloat64Vector3D GetNormal(double time)
    {
        return SamplingSpecs.IsStatic || AnimatedNormal is null
            ? Normal
            : AnimatedNormal.GetPoint(time).ToUnitLinVector3D();
    }

    public double GetRadius(double time)
    {
        return SamplingSpecs.IsStatic || AnimatedRadius is null
            ? Radius
            : AnimatedRadius.GetValue(time);
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
                GetNormal(time),
                GetRadius(time)
            );
        }
    }

    public IEnumerator<IGrVisualElement3D> GetEnumerator()
    {
        yield return GetVisualVector1();
        yield return GetVisualVector2();
        yield return GetVisualVector3();
        yield return GetVisualCircleSurface();

        if (DrawLineSegments)
        {
            yield return GetVisualLineSegment1();
            yield return GetVisualLineSegment2();
            yield return GetVisualLineSegment3();
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}