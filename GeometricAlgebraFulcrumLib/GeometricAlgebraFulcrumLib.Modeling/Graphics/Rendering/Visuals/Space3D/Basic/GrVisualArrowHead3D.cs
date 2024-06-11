using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Basic;

public sealed class GrVisualArrowHead3D :
    GrVisualElementWithAnimation3D,
    ILinFloat64Vector3D
{
    public sealed record KeyFrameRecord(
        int FrameIndex, 
        double Time,
        double Visibility,
        LinFloat64Vector3D Origin, 
        LinFloat64Vector3D Direction,
        double MaxHeight
    ) : GrVisualAnimatedGeometryKeyFrameRecord(
        FrameIndex,
        Time,
        Visibility
    );


    public static GrVisualArrowHead3D CreateStatic(string name, GrVisualCurveTubeStyle3D style, ILinFloat64Vector3D direction) 
    {
        return new GrVisualArrowHead3D(
            name, 
            style, 
            LinFloat64Vector3D.Zero, 
            direction,
            GrVisualAnimationSpecs.Static
        );
    }

    public static GrVisualArrowHead3D CreateStatic(string name, GrVisualCurveTubeStyle3D style, ILinFloat64Vector3D position, ILinFloat64Vector3D direction) 
    {
        return new GrVisualArrowHead3D(
            name, 
            style, 
            position, 
            direction,
            GrVisualAnimationSpecs.Static
        );
    }
        
    public static GrVisualArrowHead3D Create(string name, GrVisualCurveTubeStyle3D style, ILinFloat64Vector3D position, ILinFloat64Vector3D direction, GrVisualAnimationSpecs animationSpecs) 
    {
        return new GrVisualArrowHead3D(
            name, 
            style, 
            position, 
            direction,
            animationSpecs
        );
    }
        
    public static GrVisualArrowHead3D CreateAnimated(string name, GrVisualCurveTubeStyle3D style, GrVisualAnimatedVector3D direction) 
    {
        return new GrVisualArrowHead3D(
            name, 
            style, 
            LinFloat64Vector3D.Zero, 
            LinFloat64Vector3D.E1,
            direction.AnimationSpecs
        ).SetAnimatedDirection(direction);
    }

    public static GrVisualArrowHead3D CreateAnimated(string name, GrVisualCurveTubeStyle3D style, ILinFloat64Vector3D position, GrVisualAnimatedVector3D direction) 
    {
        return new GrVisualArrowHead3D(
            name, 
            style, 
            position, 
            LinFloat64Vector3D.E1,
            direction.AnimationSpecs
        ).SetAnimatedDirection(direction);
    }

    public static GrVisualArrowHead3D CreateAnimated(string name, GrVisualCurveTubeStyle3D style, GrVisualAnimatedVector3D position, GrVisualAnimatedVector3D direction) 
    {
        return new GrVisualArrowHead3D(
            name, 
            style, 
            LinFloat64Vector3D.Zero, 
            LinFloat64Vector3D.E1,
            position.AnimationSpecs
        ).SetAnimatedOriginDirection(position, direction);
    }


    public GrVisualCurveTubeStyle3D Style { get; } 
        
    public int VSpaceDimensions 
        => 3;

    public ILinFloat64Vector3D Position { get; } 

    public LinFloat64Vector3D Direction { get; }
        
    public double MaxHeight { get; }

    public GrVisualAnimatedVector3D? AnimatedPosition { get; set; }

    public GrVisualAnimatedVector3D? AnimatedDirection { get; set; }
        
    public Float64Scalar Item1 
        => Direction.X;
        
    public Float64Scalar Item2 
        => Direction.Y;
        
    public Float64Scalar Item3 
        => Direction.Z;
        
    public Float64Scalar X 
        => Direction.X;
        
    public Float64Scalar Y 
        => Direction.Y;
        
    public Float64Scalar Z 
        => Direction.Z;
    
        
    private GrVisualArrowHead3D(string name, GrVisualCurveTubeStyle3D style, ILinFloat64Vector3D position, ILinFloat64Vector3D direction, GrVisualAnimationSpecs animationSpecs) 
        : base(name, animationSpecs)
    {
        Position = position;
        MaxHeight = direction.VectorENorm();
        Direction = direction.ToUnitLinVector3D();
        Style = style;

        Debug.Assert(IsValid());
    }

        
    public override bool IsValid()
    {
        return Position.IsValid() &&
               Direction.IsValid() &&
               Direction.IsNearUnitVector() &&
               MaxHeight.IsValid() &&
               MaxHeight >= 0 &&
               GetAnimatedGeometries().All(g => g.IsValid());
    }
        
    public GrVisualArrowHead3D SetAnimatedVisibility(GrVisualAnimatedScalar visibility)
    {
        AnimatedVisibility = visibility;

        return this;
    }

    public GrVisualArrowHead3D SetAnimatedOrigin(GrVisualAnimatedVector3D position)
    {
        AnimatedPosition = position;

        return this;
    }

    public GrVisualArrowHead3D SetAnimatedDirection(GrVisualAnimatedVector3D direction)
    {
        AnimatedDirection = direction;

        return this;
    }
        
    public GrVisualArrowHead3D SetAnimatedOriginDirection(GrVisualAnimatedVector3D position, GrVisualAnimatedVector3D direction)
    {
        AnimatedPosition = position;
        AnimatedDirection = direction;

        return this;
    }

    public override IReadOnlyList<GrVisualAnimatedGeometry> GetAnimatedGeometries()
    {
        var animatedGeometries = new List<GrVisualAnimatedGeometry>();

        if (AnimatedVisibility is not null)
            animatedGeometries.Add(AnimatedVisibility);

        if (AnimatedPosition is not null)
            animatedGeometries.Add(AnimatedPosition);
            
        if (AnimatedDirection is not null)
            animatedGeometries.Add(AnimatedDirection);

        return animatedGeometries;
    }
        
    public LinFloat64Vector3D GetOrigin(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedPosition is null
            ? Position.ToLinVector3D()
            : AnimatedPosition.GetPoint(time);
    }
        
    public LinFloat64Vector3D GetDirection(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedDirection is null
            ? Direction
            : AnimatedDirection.GetPoint(time).ToUnitLinVector3D();
    }
        
    public double GetMaxHeight(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedDirection is null
            ? MaxHeight
            : AnimatedDirection.GetPoint(time).VectorENorm();
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
                GetOrigin(time), 
                GetDirection(time),
                GetMaxHeight(time)
            );
        }
    }
}