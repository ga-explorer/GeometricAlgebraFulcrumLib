using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Signals;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Surfaces;

public sealed class GrVisualSphereSurface3D :
    GrVisualSurfaceWithAnimation3D
{
    public sealed record KeyFrameRecord(
        int FrameIndex, 
        double Time,
        double Visibility,
        LinFloat64Vector3D Center, 
        double Radius
    ) : GrVisualAnimatedGeometryKeyFrameRecord(
        FrameIndex,
        Time,
        Visibility
    );


    public static GrVisualSphereSurface3D CreateStatic(string name, GrVisualSurfaceStyle3D style, double radius) 
    {
        return new GrVisualSphereSurface3D(
            name,
            style,
            LinFloat64Vector3D.Zero, 
            radius,
            Float64SamplingSpecs.Static
        );
    }
        
    public static GrVisualSphereSurface3D CreateStatic(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D center, double radius) 
    {
        return new GrVisualSphereSurface3D(
            name,
            style,
            center,
            radius,
            Float64SamplingSpecs.Static
        );
    }
        
    public static GrVisualSphereSurface3D Create(string name, GrVisualSurfaceStyle3D style, double radius, Float64SamplingSpecs samplingSpecs) {
        return new GrVisualSphereSurface3D(
            name,
            style,
            LinFloat64Vector3D.Zero, 
            radius,
            samplingSpecs
        );
    }

    public static GrVisualSphereSurface3D Create(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D center, double radius, Float64SamplingSpecs samplingSpecs) {
        return new GrVisualSphereSurface3D(
            name,
            style,
            center,
            radius,
            samplingSpecs
        );
    }
        
    public static GrVisualSphereSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, GrVisualAnimatedScalar radius) 
    {
        return new GrVisualSphereSurface3D(
            name,
            style,
            LinFloat64Vector3D.Zero, 
            1d,
            radius.SamplingSpecs
        ).SetAnimatedRadius(radius);
    }

    public static GrVisualSphereSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, GrVisualAnimatedVector3D center, GrVisualAnimatedScalar radius) 
    {
        return new GrVisualSphereSurface3D(
                name,
                style,
                LinFloat64Vector3D.Zero, 
                1d,
                center.SamplingSpecs
            ).SetAnimatedCenter(center)
            .SetAnimatedRadius(radius);
    }


    public ILinFloat64Vector3D Center { get; }
        
    public double Radius { get; }

    public GrVisualAnimatedVector3D? AnimatedCenter { get; set; }
        
    public GrVisualAnimatedScalar? AnimatedRadius { get; set; }


    private GrVisualSphereSurface3D(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D center, double radius, Float64SamplingSpecs samplingSpecs) 
        : base(name, style, samplingSpecs)
    {
        Center = center;
        Radius = radius;

        Debug.Assert(IsValid());
    }


    public override bool IsValid()
    {
        return Center.IsValid() &&
               Radius.IsValid() &&
               Radius > 0 &&
               GetAnimatedGeometries().All(g => g.IsValid());
    }
        
    public GrVisualSphereSurface3D SetAnimatedVisibility(GrVisualAnimatedScalar visibility)
    {
        AnimatedVisibility = visibility;
            
        return this;
    }

    public GrVisualSphereSurface3D SetAnimatedCenter(GrVisualAnimatedVector3D center)
    {
        AnimatedCenter = center;
            
        return this;
    }
        
    public GrVisualSphereSurface3D SetAnimatedRadius(GrVisualAnimatedScalar radius)
    {
        AnimatedRadius = radius;
            
        return this;
    }

    public override IReadOnlyList<GrVisualAnimatedGeometry> GetAnimatedGeometries()
    {
        var animatedGeometries = new List<GrVisualAnimatedGeometry>(2);

        if (AnimatedVisibility is not null)
            animatedGeometries.Add(AnimatedVisibility);

        if (AnimatedCenter is not null)
            animatedGeometries.Add(AnimatedCenter);
            
        if (AnimatedRadius is not null)
            animatedGeometries.Add(AnimatedRadius);

        return animatedGeometries;
    }
        
    public LinFloat64Vector3D GetCenter(double time)
    {
        return SamplingSpecs.IsStatic || AnimatedCenter is null
            ? Center.ToLinVector3D()
            : AnimatedCenter.GetValue(time);
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
                GetRadius(time)
            );
        }
    }
}