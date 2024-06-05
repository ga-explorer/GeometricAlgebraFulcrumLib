using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Surfaces;

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
            GrVisualAnimationSpecs.Static
        );
    }
        
    public static GrVisualSphereSurface3D CreateStatic(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D center, double radius) 
    {
        return new GrVisualSphereSurface3D(
            name,
            style,
            center,
            radius,
            GrVisualAnimationSpecs.Static
        );
    }
        
    public static GrVisualSphereSurface3D Create(string name, GrVisualSurfaceStyle3D style, double radius, GrVisualAnimationSpecs animationSpecs) {
        return new GrVisualSphereSurface3D(
            name,
            style,
            LinFloat64Vector3D.Zero, 
            radius,
            animationSpecs
        );
    }

    public static GrVisualSphereSurface3D Create(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D center, double radius, GrVisualAnimationSpecs animationSpecs) {
        return new GrVisualSphereSurface3D(
            name,
            style,
            center,
            radius,
            animationSpecs
        );
    }
        
    public static GrVisualSphereSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, GrVisualAnimatedScalar radius) 
    {
        return new GrVisualSphereSurface3D(
            name,
            style,
            LinFloat64Vector3D.Zero, 
            1d,
            radius.AnimationSpecs
        ).SetAnimatedRadius(radius);
    }

    public static GrVisualSphereSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, GrVisualAnimatedVector3D center, GrVisualAnimatedScalar radius) 
    {
        return new GrVisualSphereSurface3D(
                name,
                style,
                LinFloat64Vector3D.Zero, 
                1d,
                center.AnimationSpecs
            ).SetAnimatedCenter(center)
            .SetAnimatedRadius(radius);
    }


    public ILinFloat64Vector3D Center { get; }
        
    public double Radius { get; }

    public GrVisualAnimatedVector3D? AnimatedCenter { get; set; }
        
    public GrVisualAnimatedScalar? AnimatedRadius { get; set; }


    private GrVisualSphereSurface3D(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D center, double radius, GrVisualAnimationSpecs animationSpecs) 
        : base(name, style, animationSpecs)
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
        return AnimationSpecs.IsStatic || AnimatedCenter is null
            ? Center.ToLinVector3D()
            : AnimatedCenter.GetPoint(time);
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
                GetRadius(time)
            );
        }
    }
}