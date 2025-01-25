using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Signals;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Surfaces;

public sealed class GrVisualParallelogramSurface3D :
    GrVisualSurfaceWithAnimation3D
{
    public sealed record KeyFrameRecord(
        int FrameIndex,
        double Time,
        double Visibility,
        LinFloat64Vector3D Position,
        LinFloat64Vector3D Direction1,
        LinFloat64Vector3D Direction2
    ) : GrVisualAnimatedGeometryKeyFrameRecord(
        FrameIndex, 
        Time, 
        Visibility
    );

        
    public static GrVisualParallelogramSurface3D CreateStatic(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D position)
    {
        return new GrVisualParallelogramSurface3D(
            name,
            style,
            position,
            LinFloat64Vector3D.E1,
            LinFloat64Vector3D.E2,
            Float64SamplingSpecs.Static
        );
    }

    public static GrVisualParallelogramSurface3D CreateStatic(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D position, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2)
    {
        return new GrVisualParallelogramSurface3D(
            name,
            style,
            position,
            direction1,
            direction2,
            Float64SamplingSpecs.Static
        );
    }
        
    public static GrVisualParallelogramSurface3D Create(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D position, Float64SamplingSpecs samplingSpecs)
    {
        return new GrVisualParallelogramSurface3D(
            name,
            style,
            position,
            LinFloat64Vector3D.E1,
            LinFloat64Vector3D.E2,
            samplingSpecs
        );
    }

    public static GrVisualParallelogramSurface3D Create(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D position, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, Float64SamplingSpecs samplingSpecs)
    {
        return new GrVisualParallelogramSurface3D(
            name,
            style,
            position,
            direction1,
            direction2,
            samplingSpecs
        );
    }
        
    public static GrVisualParallelogramSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, GrVisualAnimatedVector3D position)
    {
        return new GrVisualParallelogramSurface3D(
            name,
            style,
            LinFloat64Vector3D.Zero,
            LinFloat64Vector3D.E1,
            LinFloat64Vector3D.E2,
            position.SamplingSpecs
        ).SetAnimatedPosition(position);
    }

    public static GrVisualParallelogramSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, GrVisualAnimatedVector3D position, GrVisualAnimatedVector3D direction1, GrVisualAnimatedVector3D direction2)
    {
        return new GrVisualParallelogramSurface3D(
            name,
            style,
            LinFloat64Vector3D.Zero,
            LinFloat64Vector3D.E1,
            LinFloat64Vector3D.E2,
            position.SamplingSpecs
        ).SetAnimatedPositionDirections(position, direction1, direction2);
    }

        
    public ILinFloat64Vector3D Direction1 { get; }

    public ILinFloat64Vector3D Direction2 { get; }
        
    public ILinFloat64Vector3D Direction12 
        => Direction1.VectorAdd(Direction2);
        
    public LinFloat64Vector3D UnitDirection1 
        => Direction1.ToUnitLinVector3D();

    public LinFloat64Vector3D UnitDirection2 
        => Direction2.ToUnitLinVector3D();

    public ILinFloat64Vector3D Position { get; }
        
    public ILinFloat64Vector3D Position1 
        => Position.VectorAdd(Direction1);
            
    public ILinFloat64Vector3D Position2 
        => Position.VectorAdd(Direction2);
        
    public ILinFloat64Vector3D Position12 
        => Position.VectorAdd(Direction1).VectorAdd(Direction2);
        
    public double Length1 
        => Direction1.VectorENorm();

    public double Length2 
        => Direction2.VectorENorm();

    public LinFloat64DirectedArea3D DirectedArea 
        => new(Direction1, Direction2);

    public LinFloat64Vector3D UnitNormal
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


    private GrVisualParallelogramSurface3D(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D position, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, Float64SamplingSpecs samplingSpecs)
        : base(name, style, samplingSpecs)
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

    public LinFloat64Vector3D GetDirection1(double time)
    {
        return SamplingSpecs.IsStatic || AnimatedDirection1 is null
            ? Direction1.ToLinVector3D()
            : AnimatedDirection1.GetPoint(time);
    }
        
    public LinFloat64Vector3D GetDirection2(double time)
    {
        return SamplingSpecs.IsStatic || AnimatedDirection2 is null
            ? Direction2.ToLinVector3D()
            : AnimatedDirection2.GetPoint(time);
    }
        
    public LinFloat64Vector3D GetDirection12(double time)
    {
        return GetDirection1(time) + 
               GetDirection2(time);
    }

    public LinFloat64Vector3D GetPosition(double time)
    {
        return SamplingSpecs.IsStatic || AnimatedPosition is null
            ? Position.ToLinVector3D()
            : AnimatedPosition.GetPoint(time);
    }

    public LinFloat64Vector3D GetPosition1(double time)
    {
        return GetPosition(time) + GetDirection1(time);
    }
        
    public LinFloat64Vector3D GetPosition2(double time)
    {
        return GetPosition(time) + GetDirection2(time);
    }
        
    public LinFloat64Vector3D GetPosition12(double time)
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
            var time = (double)frameIndex / SamplingSpecs.SamplingRate;
                
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