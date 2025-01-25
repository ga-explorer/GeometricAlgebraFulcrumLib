using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Surfaces;

public sealed class GrVisualCircleRingSurface3D :
    GrVisualSurfaceWithAnimation3D
{
    public sealed record KeyFrameRecord(
        int FrameIndex, 
        double Time,
        double Visibility,
        LinFloat64Vector3D Center, 
        LinFloat64Vector3D Normal,
        double MinRadius,
        double MaxRadius
    ) : GrVisualAnimatedGeometryKeyFrameRecord(
        FrameIndex,
        Time,
        Visibility
    );


    public static GrVisualCircleRingSurface3D CreateStatic(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D normal, double minRadius, double maxRadius)
    {
        return new GrVisualCircleRingSurface3D(
            name, 
            style, 
            LinFloat64Vector3D.Zero, 
            normal, 
            minRadius, 
            maxRadius,
            Float64SamplingSpecs.Static
        );
    }
        
    public static GrVisualCircleRingSurface3D CreateStatic(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D center, ILinFloat64Vector3D normal, double minRadius, double maxRadius)
    {
        return new GrVisualCircleRingSurface3D(
            name, 
            style, 
            center, 
            normal, 
            minRadius, 
            maxRadius,
            Float64SamplingSpecs.Static
        );
    }

    public static GrVisualCircleRingSurface3D Create(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D normal, double minRadius, double maxRadius, Float64SamplingSpecs samplingSpecs)
    {
        return new GrVisualCircleRingSurface3D(
            name, 
            style, 
            LinFloat64Vector3D.Zero, 
            normal, 
            minRadius, 
            maxRadius,
            samplingSpecs
        );
    }
        
    public static GrVisualCircleRingSurface3D Create(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D center, ILinFloat64Vector3D normal, double minRadius, double maxRadius, Float64SamplingSpecs samplingSpecs)
    {
        return new GrVisualCircleRingSurface3D(
            name, 
            style, 
            center, 
            normal, 
            minRadius, 
            maxRadius,
            samplingSpecs
        );
    }
        
    public static GrVisualCircleRingSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, GrVisualAnimatedVector3D normal, GrVisualAnimatedScalar minRadius, GrVisualAnimatedScalar maxRadius)
    {
        return new GrVisualCircleRingSurface3D(
                name, 
                style, 
                LinFloat64Vector3D.Zero, 
                LinFloat64Vector3D.E2, 
                0.5d, 
                1d,
                normal.SamplingSpecs
            ).SetAnimatedNormal(normal)
            .SetAnimatedMinRadius(minRadius)
            .SetAnimatedMaxRadius(maxRadius);
    }
        
    public static GrVisualCircleRingSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, GrVisualAnimatedVector3D center, GrVisualAnimatedVector3D normal, GrVisualAnimatedScalar minRadius, GrVisualAnimatedScalar maxRadius)
    {
        return new GrVisualCircleRingSurface3D(
                name, 
                style, 
                LinFloat64Vector3D.Zero, 
                LinFloat64Vector3D.E2, 
                0.5d, 
                1d,
                center.SamplingSpecs
            ).SetAnimatedCenter(center)
            .SetAnimatedNormal(normal)
            .SetAnimatedMinRadius(minRadius)
            .SetAnimatedMaxRadius(maxRadius);
    }


    public ILinFloat64Vector3D Center { get; }

    public LinFloat64Vector3D Normal { get; }

    public double MinRadius { get; }

    public double MaxRadius { get; }
        
    public GrVisualAnimatedVector3D? AnimatedCenter { get; set; }

    public GrVisualAnimatedVector3D? AnimatedNormal { get; set; }

    public GrVisualAnimatedScalar? AnimatedMinRadius { get; set; }
        
    public GrVisualAnimatedScalar? AnimatedMaxRadius { get; set; }


    private GrVisualCircleRingSurface3D(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D center, ILinFloat64Vector3D normal, double minRadius, double maxRadius, Float64SamplingSpecs samplingSpecs) 
        : base(name, style, samplingSpecs)
    {
        Center = center;
        Normal = normal.ToUnitLinVector3D();
        MinRadius = minRadius;
        MaxRadius = maxRadius;

        Debug.Assert(IsValid());
    }
        
        
    public override bool IsValid()
    {
        return Center.IsValid() &&
               Normal.IsValid() &&
               Normal.IsNearUnitVector() &&
               MinRadius.IsValid() &&
               MaxRadius.IsValid() &&
               MinRadius > 0 && 
               MinRadius < MaxRadius &&
               GetAnimatedGeometries().All(g => g.IsValid());
    }
        
    public double GetInnerEdgeLength()
    {
        return 2d * Math.PI * MinRadius;
    }
        
    public double GetOuterEdgeLength()
    {
        return 2d * Math.PI * MaxRadius;
    }

    public Triplet<LinFloat64Vector3D> GetInnerEdgePointsTriplet()
    {
        var quaternion = LinBasisVector3D.Pz.VectorToVectorRotationQuaternion(
            Normal.ToUnitLinVector3D()
        );

        const double angle = 2d * Math.PI / 3d;

        var a = MinRadius * Math.Cos(angle);
        var b = MinRadius * Math.Sin(angle);

        var point1 = Center + quaternion.RotateVector(MinRadius, 0, 0);
        var point2 = Center + quaternion.RotateVector(a, b, 0);
        var point3 = Center + quaternion.RotateVector(a, -b, 0);

        return new Triplet<LinFloat64Vector3D>(point1, point2, point3);
    }
        
    public Triplet<LinFloat64Vector3D> GetOuterEdgePointsTriplet()
    {
        var quaternion = LinBasisVector3D.Pz.VectorToVectorRotationQuaternion(
            Normal.ToUnitLinVector3D()
        );

        const double angle = 2d * Math.PI / 3d;

        var a = MaxRadius * Math.Cos(angle);
        var b = MaxRadius * Math.Sin(angle);

        var point1 = Center + quaternion.RotateVector(MaxRadius, 0, 0);
        var point2 = Center + quaternion.RotateVector(a, b, 0);
        var point3 = Center + quaternion.RotateVector(a, -b, 0);

        return new Triplet<LinFloat64Vector3D>(point1, point2, point3);
    }

    public GrVisualCircleRingSurface3D SetAnimatedVisibility(GrVisualAnimatedScalar visibility)
    {
        AnimatedVisibility = visibility;
            
        return this;
    }

    public GrVisualCircleRingSurface3D SetAnimatedCenter(GrVisualAnimatedVector3D center)
    {
        AnimatedCenter = center;
            
        return this;
    }
        
    public GrVisualCircleRingSurface3D SetAnimatedNormal(GrVisualAnimatedVector3D normal)
    {
        AnimatedNormal = normal;
            
        return this;
    }
        
    public GrVisualCircleRingSurface3D SetAnimatedMinRadius(GrVisualAnimatedScalar radius)
    {
        AnimatedMinRadius = radius;
            
        return this;
    }
        
    public GrVisualCircleRingSurface3D SetAnimatedMaxRadius(GrVisualAnimatedScalar radius)
    {
        AnimatedMaxRadius = radius;
            
        return this;
    }

    public GrVisualCircleRingSurface3D SetAnimatedCenterNormalRadius(GrVisualAnimatedVector3D center, GrVisualAnimatedVector3D normal, GrVisualAnimatedScalar minRadius, GrVisualAnimatedScalar maxRadius)
    {
        AnimatedCenter = center;
        AnimatedNormal = normal;
        AnimatedMinRadius = minRadius;
        AnimatedMaxRadius = maxRadius;
            
        return this;
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
            
        if (AnimatedMinRadius is not null)
            animatedGeometries.Add(AnimatedMinRadius);

        if (AnimatedMaxRadius is not null)
            animatedGeometries.Add(AnimatedMaxRadius);

        return animatedGeometries;
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

    public double GetMinRadius(double time)
    {
        return SamplingSpecs.IsStatic || AnimatedMinRadius is null
            ? MinRadius
            : AnimatedMinRadius.GetValue(time);
    }
        
    public double GetMaxRadius(double time)
    {
        return SamplingSpecs.IsStatic || AnimatedMaxRadius is null
            ? MaxRadius
            : AnimatedMaxRadius.GetValue(time);
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
                GetMinRadius(time),
                GetMaxRadius(time)
            );
        }
    }
}