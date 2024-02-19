using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Surfaces;

public sealed class GrVisualParallelogramSurface3D :
    GrVisualSurfaceWithAnimation3D
{
    public sealed record KeyFrameRecord(
        int FrameIndex,
        double Time,
        double Visibility,
        Float64Vector3D Position,
        Float64Vector3D Direction1,
        Float64Vector3D Direction2
    ) : GrVisualAnimatedGeometryKeyFrameRecord(
        FrameIndex, 
        Time, 
        Visibility
    );

        
    public static GrVisualParallelogramSurface3D CreateStatic(string name, GrVisualSurfaceStyle3D style, IFloat64Vector3D position)
    {
        return new GrVisualParallelogramSurface3D(
            name,
            style,
            position,
            Float64Vector3D.E1,
            Float64Vector3D.E2,
            GrVisualAnimationSpecs.Static
        );
    }

    public static GrVisualParallelogramSurface3D CreateStatic(string name, GrVisualSurfaceStyle3D style, IFloat64Vector3D position, IFloat64Vector3D direction1, IFloat64Vector3D direction2)
    {
        return new GrVisualParallelogramSurface3D(
            name,
            style,
            position,
            direction1,
            direction2,
            GrVisualAnimationSpecs.Static
        );
    }
        
    public static GrVisualParallelogramSurface3D Create(string name, GrVisualSurfaceStyle3D style, IFloat64Vector3D position, GrVisualAnimationSpecs animationSpecs)
    {
        return new GrVisualParallelogramSurface3D(
            name,
            style,
            position,
            Float64Vector3D.E1,
            Float64Vector3D.E2,
            animationSpecs
        );
    }

    public static GrVisualParallelogramSurface3D Create(string name, GrVisualSurfaceStyle3D style, IFloat64Vector3D position, IFloat64Vector3D direction1, IFloat64Vector3D direction2, GrVisualAnimationSpecs animationSpecs)
    {
        return new GrVisualParallelogramSurface3D(
            name,
            style,
            position,
            direction1,
            direction2,
            animationSpecs
        );
    }
        
    public static GrVisualParallelogramSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, GrVisualAnimatedVector3D position)
    {
        return new GrVisualParallelogramSurface3D(
            name,
            style,
            Float64Vector3D.Zero,
            Float64Vector3D.E1,
            Float64Vector3D.E2,
            position.AnimationSpecs
        ).SetAnimatedPosition(position);
    }

    public static GrVisualParallelogramSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, GrVisualAnimatedVector3D position, GrVisualAnimatedVector3D direction1, GrVisualAnimatedVector3D direction2)
    {
        return new GrVisualParallelogramSurface3D(
            name,
            style,
            Float64Vector3D.Zero,
            Float64Vector3D.E1,
            Float64Vector3D.E2,
            position.AnimationSpecs
        ).SetAnimatedPositionDirections(position, direction1, direction2);
    }

        
    public IFloat64Vector3D Direction1 { get; }

    public IFloat64Vector3D Direction2 { get; }
        
    public IFloat64Vector3D Direction12 
        => Direction1.Add(Direction2);
        
    public Float64Vector3D UnitDirection1 
        => Direction1.ToUnitVector();

    public Float64Vector3D UnitDirection2 
        => Direction2.ToUnitVector();

    public IFloat64Vector3D Position { get; }
        
    public IFloat64Vector3D Position1 
        => Position.Add(Direction1);
            
    public IFloat64Vector3D Position2 
        => Position.Add(Direction2);
        
    public IFloat64Vector3D Position12 
        => Position.Add(Direction1).Add(Direction2);
        
    public double Length1 
        => Direction1.ENorm();

    public double Length2 
        => Direction2.ENorm();

    public Float64DirectedArea3D DirectedArea 
        => new(Direction1, Direction2);

    public Float64Vector3D UnitNormal
        => Direction1.VectorUnitCross(Direction2);
        
    public GrVisualAnimatedVector3D? AnimatedDirection1 { get; set; }
        
    public GrVisualAnimatedVector3D? AnimatedDirection2 { get; set; }
        
    public GrVisualAnimatedVector3D? AnimatedDirection12
    {
        get
        {
            if (AnimatedDirection1 is null)
                return AnimatedDirection2 is null
                    ? null
                    : Direction1 + AnimatedDirection2;

            return AnimatedDirection2 is null
                ? AnimatedDirection1 + Direction2
                : AnimatedDirection1 + AnimatedDirection2;
        }
    }

    public GrVisualAnimatedVector3D? AnimatedPosition { get; set; }

    public GrVisualAnimatedVector3D? AnimatedPosition1
    {
        get
        {
            if (AnimatedPosition is null)
                return AnimatedDirection2 is null
                    ? null
                    : Position + AnimatedDirection2;

            return AnimatedDirection2 is null
                ? AnimatedPosition + Direction2
                : AnimatedPosition + AnimatedDirection2;
        }
    }
        
    public GrVisualAnimatedVector3D? AnimatedPosition2
    {
        get
        {
            if (AnimatedPosition is null)
                return AnimatedDirection1 is null
                    ? null
                    : Position + AnimatedDirection1;

            return AnimatedDirection1 is null
                ? AnimatedPosition + Direction1
                : AnimatedPosition + AnimatedDirection1;
        }
    }
        
    public GrVisualAnimatedVector3D? AnimatedPosition12
    {
        get
        {
            var animatedDirection = AnimatedDirection12;
            if (AnimatedPosition is null)
                return animatedDirection is null
                    ? null
                    : Position + animatedDirection;

            var direction = Direction12;
            return animatedDirection is null
                ? AnimatedPosition + direction
                : AnimatedPosition + animatedDirection;
        }
    }


    private GrVisualParallelogramSurface3D(string name, GrVisualSurfaceStyle3D style, IFloat64Vector3D position, IFloat64Vector3D direction1, IFloat64Vector3D direction2, GrVisualAnimationSpecs animationSpecs)
        : base(name, style, animationSpecs)
    {
        Position = position;
        Direction1 = direction1;
        Direction2 = direction2;

        Debug.Assert(IsValid());
    }


    public override bool IsValid()
    {
        return Position.IsValid() &&
               Direction1.IsValid() &&
               Direction2.IsValid() &&
               GetAnimatedGeometries().All(g => g.IsValid());
    }
        
    public override IReadOnlyList<GrVisualAnimatedGeometry> GetAnimatedGeometries()
    {
        var animatedGeometries = new List<GrVisualAnimatedGeometry>(2);

        if (AnimatedVisibility is not null)
            animatedGeometries.Add(AnimatedVisibility);

        if (AnimatedPosition is not null)
            animatedGeometries.Add(AnimatedPosition);

        if (AnimatedDirection1 is not null)
            animatedGeometries.Add(AnimatedDirection1);

        if (AnimatedDirection2 is not null)
            animatedGeometries.Add(AnimatedDirection2);

        return animatedGeometries;
    }
        
    public GrVisualParallelogramSurface3D SetAnimatedVisibility(GrVisualAnimatedScalar visibility)
    {
        AnimatedVisibility = visibility;

        return this;
    }

    public GrVisualParallelogramSurface3D SetAnimatedDirection1(GrVisualAnimatedVector3D direction1)
    {
        AnimatedDirection1 = direction1;

        return this;
    }
        
    public GrVisualParallelogramSurface3D SetAnimatedDirection2(GrVisualAnimatedVector3D direction2)
    {
        AnimatedDirection2 = direction2;

        return this;
    }
        
    public GrVisualParallelogramSurface3D SetAnimatedDirections(GrVisualAnimatedVector3D direction1, GrVisualAnimatedVector3D direction2)
    {
        AnimatedDirection1 = direction1;
        AnimatedDirection2 = direction2;

        return this;
    }

    public GrVisualParallelogramSurface3D SetAnimatedPosition(GrVisualAnimatedVector3D position)
    {
        AnimatedPosition = position;

        return this;
    }

    public GrVisualParallelogramSurface3D SetAnimatedPosition1(GrVisualAnimatedVector3D position1)
    {
        AnimatedDirection1 = 
            AnimatedPosition is null
                ? position1 - Position
                : position1 - AnimatedPosition;

        return this;
    }
        
    public GrVisualParallelogramSurface3D SetAnimatedPosition2(GrVisualAnimatedVector3D position2)
    {
        AnimatedDirection1 = 
            AnimatedPosition is null
                ? position2 - Position
                : position2 - AnimatedPosition;

        return this;
    }
        
    public GrVisualParallelogramSurface3D SetAnimatedPositions(GrVisualAnimatedVector3D position1, GrVisualAnimatedVector3D position2)
    {
        if (AnimatedPosition is null)
        {
            AnimatedDirection1 = position1 - Position;
            AnimatedDirection2 = position2 - Position;
        }
        else
        {
            AnimatedDirection1 = position1 - AnimatedPosition;
            AnimatedDirection2 = position2 - AnimatedPosition;
        }

        return this;
    }

    public GrVisualParallelogramSurface3D SetAnimatedPositions(GrVisualAnimatedVector3D position, GrVisualAnimatedVector3D position1, GrVisualAnimatedVector3D position2)
    {
        AnimatedPosition = position;
        AnimatedDirection1 = position1 - position;
        AnimatedDirection2 = position2 - position;

        return this;
    }
        
    public GrVisualParallelogramSurface3D SetAnimatedPositionDirections(GrVisualAnimatedVector3D position, GrVisualAnimatedVector3D direction1, GrVisualAnimatedVector3D direction2)
    {
        AnimatedPosition = position;
        AnimatedDirection1 = direction1;
        AnimatedDirection2 = direction2;

        return this;
    }

    public Float64Vector3D GetDirection1(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedDirection1 is null
            ? Direction1.ToVector3D()
            : AnimatedDirection1.GetPoint(time);
    }
        
    public Float64Vector3D GetDirection2(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedDirection2 is null
            ? Direction2.ToVector3D()
            : AnimatedDirection2.GetPoint(time);
    }
        
    public Float64Vector3D GetDirection12(double time)
    {
        return GetDirection1(time) + 
               GetDirection2(time);
    }

    public Float64Vector3D GetPosition(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedPosition is null
            ? Position.ToVector3D()
            : AnimatedPosition.GetPoint(time);
    }

    public Float64Vector3D GetPosition1(double time)
    {
        return GetPosition(time) + GetDirection1(time);
    }
        
    public Float64Vector3D GetPosition2(double time)
    {
        return GetPosition(time) + GetDirection2(time);
    }
        
    public Float64Vector3D GetPosition12(double time)
    {
        return GetPosition(time) + 
               GetDirection1(time) + 
               GetDirection2(time);
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
                GetPosition(time), 
                GetDirection1(time), 
                GetDirection2(time)
            );
        }
    }
}