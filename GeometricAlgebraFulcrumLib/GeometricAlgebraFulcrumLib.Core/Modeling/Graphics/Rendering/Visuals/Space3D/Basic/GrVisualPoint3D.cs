using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Basic;

public sealed class GrVisualPoint3D :
    GrVisualElementWithAnimation3D,
    ILinFloat64Vector3D
{
    public sealed record KeyFrameRecord(
        int FrameIndex,
        double Time,
        double Visibility,
        LinFloat64Vector3D Position
    ) : GrVisualAnimatedGeometryKeyFrameRecord(
        FrameIndex, 
        Time, 
        Visibility
    );
        

    public static GrVisualPoint3D CreateStatic(string name, GrVisualSurfaceThickStyle3D style, ILinFloat64Vector3D position)
    {
        return new GrVisualPoint3D(
            name, 
            style, 
            position, 
            GrVisualAnimationSpecs.Static
        );
    }
        
    public static GrVisualPoint3D Create(string name, GrVisualSurfaceThickStyle3D style, ILinFloat64Vector3D position, GrVisualAnimationSpecs animationSpecs)
    {
        return new GrVisualPoint3D(
            name, 
            style, 
            position,
            animationSpecs
        );
    }
        
    public static GrVisualPoint3D CreateAnimated(string name, GrVisualSurfaceThickStyle3D style, GrVisualAnimatedVector3D position)
    {
        return new GrVisualPoint3D(
            name, 
            style, 
            LinFloat64Vector3D.Zero,
            position.AnimationSpecs
        ).SetAnimatedPosition(position);
    }


    public GrVisualSurfaceThickStyle3D Style { get; }

    public ILinFloat64Vector3D Position { get; } 
        
    public GrVisualAnimatedVector3D? AnimatedPosition { get; set; }
        
    public int VSpaceDimensions 
        => 3;

    public Float64Scalar Item1 
        => Position.X;
        
    public Float64Scalar Item2 
        => Position.Y;
        
    public Float64Scalar Item3 
        => Position.Z;
        
    public Float64Scalar X 
        => Position.X;
        
    public Float64Scalar Y 
        => Position.Y;
        
    public Float64Scalar Z 
        => Position.Z;


    private GrVisualPoint3D(string name, GrVisualSurfaceThickStyle3D style, ILinFloat64Vector3D position, GrVisualAnimationSpecs animationSpecs) 
        : base(name, animationSpecs)
    {
        Position = position;
        Style = style;

        Debug.Assert(IsValid());
    }
        

    public override bool IsValid()
    {
        return Position.IsValid() &&
               AnimatedPosition.IsNullOrValid(AnimationSpecs.FrameTimeRange);
    }

    public override IReadOnlyList<GrVisualAnimatedGeometry> GetAnimatedGeometries()
    {
        var animatedGeometries = new List<GrVisualAnimatedGeometry>(2);

        if (AnimatedVisibility is not null)
            animatedGeometries.Add(AnimatedVisibility);

        if (AnimatedPosition is not null)
            animatedGeometries.Add(AnimatedPosition);

        return animatedGeometries;
    }
        
    public GrVisualPoint3D SetAnimatedVisibility(GrVisualAnimatedScalar visibility)
    {
        AnimatedVisibility = visibility;
            
        return this;
    }

    public GrVisualPoint3D SetAnimatedPosition(GrVisualAnimatedVector3D position)
    {
        AnimatedPosition = position;

        return this;
    }
        
    public LinFloat64Vector3D GetPosition(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedPosition is null
            ? Position.ToLinVector3D()
            : AnimatedPosition.GetPoint(time);
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
                GetPosition(time)
            );
        }
    }
}