using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Surfaces;

public sealed class GrVisualParallelepipedSurface3D :
    GrVisualSurfaceWithAnimation3D
{
    public sealed record KeyFrameRecord(
        int FrameIndex,
        double Time,
        double Visibility,
        LinFloat64Vector3D Position,
        LinFloat64Vector3D Direction1,
        LinFloat64Vector3D Direction2,
        LinFloat64Vector3D Direction3
    ) : GrVisualAnimatedGeometryKeyFrameRecord(
        FrameIndex, 
        Time, 
        Visibility
    );

        
    public static GrVisualParallelepipedSurface3D CreateStatic(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D position)
    {
        return new GrVisualParallelepipedSurface3D(
            name,
            style,
            position,
            LinFloat64Vector3D.E1,
            LinFloat64Vector3D.E2,
            LinFloat64Vector3D.E3,
            GrVisualAnimationSpecs.Static
        );
    }

    public static GrVisualParallelepipedSurface3D CreateStatic(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D position, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, ILinFloat64Vector3D direction3)
    {
        return new GrVisualParallelepipedSurface3D(
            name,
            style,
            position,
            direction1,
            direction2,
            direction3,
            GrVisualAnimationSpecs.Static
        );
    }
        
    public static GrVisualParallelepipedSurface3D Create(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D position, GrVisualAnimationSpecs animationSpecs)
    {
        return new GrVisualParallelepipedSurface3D(
            name,
            style,
            position,
            LinFloat64Vector3D.E1,
            LinFloat64Vector3D.E2,
            LinFloat64Vector3D.E3,
            animationSpecs
        );
    }

    public static GrVisualParallelepipedSurface3D Create(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D position, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, ILinFloat64Vector3D direction3, GrVisualAnimationSpecs animationSpecs)
    {
        return new GrVisualParallelepipedSurface3D(
            name,
            style,
            position,
            direction1,
            direction2,
            direction3,
            animationSpecs
        );
    }
        
    public static GrVisualParallelepipedSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, GrVisualAnimatedVector3D position)
    {
        return new GrVisualParallelepipedSurface3D(
            name,
            style,
            LinFloat64Vector3D.Zero,
            LinFloat64Vector3D.E1,
            LinFloat64Vector3D.E2,
            LinFloat64Vector3D.E3,
            position.AnimationSpecs
        ).SetAnimatedPosition(position);
    }
    
    public static GrVisualParallelepipedSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D position, GrVisualAnimatedVector3D direction1, GrVisualAnimatedVector3D direction2, GrVisualAnimatedVector3D direction3)
    {
        return new GrVisualParallelepipedSurface3D(
            name,
            style,
            position,
            LinFloat64Vector3D.E1,
            LinFloat64Vector3D.E2,
            LinFloat64Vector3D.E3,
            direction1.AnimationSpecs
        ).SetAnimatedDirections(direction1, direction2, direction3);
    }

    public static GrVisualParallelepipedSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, GrVisualAnimatedVector3D position, GrVisualAnimatedVector3D direction1, GrVisualAnimatedVector3D direction2, GrVisualAnimatedVector3D direction3)
    {
        return new GrVisualParallelepipedSurface3D(
            name,
            style,
            LinFloat64Vector3D.Zero,
            LinFloat64Vector3D.E1,
            LinFloat64Vector3D.E2,
            LinFloat64Vector3D.E3,
            position.AnimationSpecs
        ).SetAnimatedPositionDirections(position, direction1, direction2, direction3);
    }

    
    public GrVisualCurveStyle3D? EdgeStyle { get; set; }

    public bool DrawEdge 
        => EdgeStyle != null;

    public ILinFloat64Vector3D Direction1 { get; }

    public ILinFloat64Vector3D Direction2 { get; }

    public ILinFloat64Vector3D Direction3 { get; }
    
    public ILinFloat64Vector3D Direction12 
        => Direction1.VectorAdd(Direction2);
    
    public ILinFloat64Vector3D Direction13 
        => Direction1.VectorAdd(Direction3);
    
    public ILinFloat64Vector3D Direction23 
        => Direction2.VectorAdd(Direction3);

    public ILinFloat64Vector3D Direction123 
        => Direction1.VectorAdd(Direction2).VectorAdd(Direction3);
     
    public ILinFloat64Vector3D Position { get; }

    public ILinFloat64Vector3D Position1 
        => Position.VectorAdd(Direction1);
    
    public ILinFloat64Vector3D Position2 
        => Position.VectorAdd(Direction2);
    
    public ILinFloat64Vector3D Position3 
        => Position.VectorAdd(Direction3);
    
    public ILinFloat64Vector3D Position12 
        => Position.VectorAdd(Direction1).VectorAdd(Direction2);

    public ILinFloat64Vector3D Position13 
        => Position.VectorAdd(Direction1).VectorAdd(Direction3);
    
    public ILinFloat64Vector3D Position23 
        => Position.VectorAdd(Direction2).VectorAdd(Direction3);
    
    public ILinFloat64Vector3D Position123 
        => Position.VectorAdd(Direction1).VectorAdd(Direction2).VectorAdd(Direction3);
    
    public double Length1 
        => Direction1.VectorENorm();

    public double Length2 
        => Direction2.VectorENorm();

    public double Length3 
        => Direction3.VectorENorm();

    public LinFloat64Vector3D UnitDirection1 
        => Direction1.ToUnitLinVector3D();

    public LinFloat64Vector3D UnitDirection2 
        => Direction2.ToUnitLinVector3D();

    public LinFloat64Vector3D UnitDirection3 
        => Direction3.ToUnitLinVector3D();

    public LinFloat64DirectedArea3D DirectedArea12 
        => new LinFloat64DirectedArea3D(Direction1, Direction2);
    
    public LinFloat64DirectedArea3D DirectedArea23 
        => new LinFloat64DirectedArea3D(Direction2, Direction3);
    
    public LinFloat64DirectedArea3D DirectedArea31 
        => new LinFloat64DirectedArea3D(Direction3, Direction1);

    public double DirectedVolume 
        => Direction1.Determinant(Direction2, Direction3);


    public GrVisualAnimatedVector3D? AnimatedDirection1 { get; set; }
        
    public GrVisualAnimatedVector3D? AnimatedDirection2 { get; set; }
        
    public GrVisualAnimatedVector3D? AnimatedDirection3 { get; set; }
    
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
    
    public GrVisualAnimatedVector3D? AnimatedDirection13
    {
        get
        {
            if (AnimatedDirection1 is null)
                return AnimatedDirection3 is null
                    ? null
                    : Direction1 + AnimatedDirection3;

            return AnimatedDirection3 is null
                ? AnimatedDirection1 + Direction3
                : AnimatedDirection1 + AnimatedDirection3;
        }
    }
    
    public GrVisualAnimatedVector3D? AnimatedDirection23
    {
        get
        {
            if (AnimatedDirection2 is null)
                return AnimatedDirection3 is null
                    ? null
                    : Direction2 + AnimatedDirection3;

            return AnimatedDirection3 is null
                ? AnimatedDirection2 + Direction3
                : AnimatedDirection2 + AnimatedDirection3;
        }
    }
    
    public GrVisualAnimatedVector3D? AnimatedDirection123
    {
        get
        {
            if (AnimatedDirection1 is null)
            {
                if (AnimatedDirection2 is null)
                    return AnimatedDirection3 is null
                        ? null
                        : Direction1 + (Direction2 + AnimatedDirection3);

                return AnimatedDirection3 is null
                    ? Direction1 + AnimatedDirection2 + Direction3
                    : Direction1 + AnimatedDirection2 + AnimatedDirection3;
            }

            if (AnimatedDirection2 is null)
                return AnimatedDirection3 is null
                    ? AnimatedDirection1 + Direction2 + Direction3
                    : AnimatedDirection1 + Direction2 + AnimatedDirection3;

            return AnimatedDirection3 is null
                ? AnimatedDirection1 + AnimatedDirection2 + Direction3
                : AnimatedDirection1 + AnimatedDirection2 + AnimatedDirection3;
        }
    }

    public GrVisualAnimatedVector3D? AnimatedPosition { get; set; }

    public GrVisualAnimatedVector3D? AnimatedPosition1
    {
        get
        {
            var animatedDirection = AnimatedDirection1;
            if (AnimatedPosition is null)
                return animatedDirection is null
                    ? null
                    : Position + animatedDirection;

            var direction = Direction1;
            return animatedDirection is null
                ? AnimatedPosition + direction
                : AnimatedPosition + animatedDirection;
        }
    }
        
    public GrVisualAnimatedVector3D? AnimatedPosition2
    {
        get
        {
            var animatedDirection = AnimatedDirection2;
            if (AnimatedPosition is null)
                return animatedDirection is null
                    ? null
                    : Position + animatedDirection;

            var direction = Direction2;
            return animatedDirection is null
                ? AnimatedPosition + direction
                : AnimatedPosition + animatedDirection;
        }
    }
        
    public GrVisualAnimatedVector3D? AnimatedPosition3
    {
        get
        {
            var animatedDirection = AnimatedDirection3;
            if (AnimatedPosition is null)
                return animatedDirection is null
                    ? null
                    : Position + animatedDirection;

            var direction = Direction3;
            return animatedDirection is null
                ? AnimatedPosition + direction
                : AnimatedPosition + animatedDirection;
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
    
    public GrVisualAnimatedVector3D? AnimatedPosition13
    {
        get
        {
            var animatedDirection = AnimatedDirection13;
            if (AnimatedPosition is null)
                return animatedDirection is null
                    ? null
                    : Position + animatedDirection;

            var direction = Direction13;
            return animatedDirection is null
                ? AnimatedPosition + direction
                : AnimatedPosition + animatedDirection;
        }
    }
    
    public GrVisualAnimatedVector3D? AnimatedPosition23
    {
        get
        {
            var animatedDirection = AnimatedDirection23;
            if (AnimatedPosition is null)
                return animatedDirection is null
                    ? null
                    : Position + animatedDirection;

            var direction = Direction23;
            return animatedDirection is null
                ? AnimatedPosition + direction
                : AnimatedPosition + animatedDirection;
        }
    }
    
    public GrVisualAnimatedVector3D? AnimatedPosition123
    {
        get
        {
            var animatedDirection = AnimatedDirection123;
            if (AnimatedPosition is null)
                return animatedDirection is null
                    ? null
                    : Position + animatedDirection;

            var direction = Direction123;
            return animatedDirection is null
                ? AnimatedPosition + direction
                : AnimatedPosition + animatedDirection;
        }
    }
    
    private GrVisualParallelepipedSurface3D(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D position, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, ILinFloat64Vector3D direction3, GrVisualAnimationSpecs animationSpecs)
        : base(name, style, animationSpecs)
    {
        Position = position;
        Direction1 = direction1;
        Direction2 = direction2;
        Direction3 = direction3;

        Debug.Assert(IsValid());
    }


    public override bool IsValid()
    {
        return Position.IsValid() &&
               Direction1.IsValid() &&
               Direction2.IsValid() &&
               Direction3.IsValid() &&
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
        
        if (AnimatedDirection3 is not null)
            animatedGeometries.Add(AnimatedDirection3);

        return animatedGeometries;
    }

    public GrVisualParallelogramSurface3D GetParallelogramSurface0()
    {
        var parallelogram = GrVisualParallelogramSurface3D.Create(
            Name + "Face0",
            Style,
            Position,
            Direction1,
            Direction2,
            AnimationSpecs
        );
        
        parallelogram.Visibility = Visibility;
        parallelogram.AnimatedPosition = AnimatedPosition;
        parallelogram.AnimatedDirection1 = AnimatedDirection1;
        parallelogram.AnimatedDirection2 = AnimatedDirection2;

        return parallelogram;
    }
    
    public GrVisualParallelogramSurface3D GetParallelogramSurface1()
    {
        var parallelogram = GrVisualParallelogramSurface3D.Create(
            Name + "Face1",
            Style,
            Position,
            Direction1,
            Direction3,
            AnimationSpecs
        );
        
        parallelogram.Visibility = Visibility;
        parallelogram.AnimatedPosition = AnimatedPosition;
        parallelogram.AnimatedDirection1 = AnimatedDirection1;
        parallelogram.AnimatedDirection2 = AnimatedDirection3;

        return parallelogram;
    }
    
    public GrVisualParallelogramSurface3D GetParallelogramSurface2()
    {
        var parallelogram = GrVisualParallelogramSurface3D.Create(
            Name + "Face2",
            Style,
            Position,
            Direction2,
            Direction3,
            AnimationSpecs
        );
        
        parallelogram.Visibility = Visibility;
        parallelogram.AnimatedPosition = AnimatedPosition;
        parallelogram.AnimatedDirection1 = AnimatedDirection2;
        parallelogram.AnimatedDirection2 = AnimatedDirection3;

        return parallelogram;
    }
    
    public GrVisualParallelogramSurface3D GetParallelogramSurface3()
    {
        var parallelogram = GrVisualParallelogramSurface3D.Create(
            Name + "Face3",
            Style,
            Position123,
            Direction1.VectorNegative(),
            Direction2.VectorNegative(),
            AnimationSpecs
        );
        
        parallelogram.Visibility = Visibility;
        parallelogram.AnimatedPosition = AnimatedPosition123;
        parallelogram.AnimatedDirection1 = AnimatedDirection1.Negative();
        parallelogram.AnimatedDirection2 = AnimatedDirection2.Negative();

        return parallelogram;
    }
    
    public GrVisualParallelogramSurface3D GetParallelogramSurface4()
    {
        var parallelogram = GrVisualParallelogramSurface3D.Create(
            Name + "Face4",
            Style,
            Position123,
            Direction1.VectorNegative(),
            Direction3.VectorNegative(),
            AnimationSpecs
        );
        
        parallelogram.Visibility = Visibility;
        parallelogram.AnimatedPosition = AnimatedPosition123;
        parallelogram.AnimatedDirection1 = AnimatedDirection1.Negative();
        parallelogram.AnimatedDirection2 = AnimatedDirection3.Negative();

        return parallelogram;
    }
    
    public GrVisualParallelogramSurface3D GetParallelogramSurface5()
    {
        var parallelogram = GrVisualParallelogramSurface3D.Create(
            Name + "Face5",
            Style,
            Position123,
            Direction2.VectorNegative(),
            Direction3.VectorNegative(),
            AnimationSpecs
        );
        
        parallelogram.Visibility = Visibility;
        parallelogram.AnimatedPosition = AnimatedPosition123;
        parallelogram.AnimatedDirection1 = AnimatedDirection2.Negative();
        parallelogram.AnimatedDirection2 = AnimatedDirection3.Negative();

        return parallelogram;
    }

    public IEnumerable<GrVisualParallelogramSurface3D> GetParallelogramSurfaces()
    {
        return new[]
        {
            GetParallelogramSurface0(),
            GetParallelogramSurface1(),
            GetParallelogramSurface2(),
            GetParallelogramSurface3(),
            GetParallelogramSurface4(),
            GetParallelogramSurface5()
        };
    }

    public GrVisualParallelepipedSurface3D SetAnimatedVisibility(GrVisualAnimatedScalar visibility)
    {
        AnimatedVisibility = visibility;

        return this;
    }

    public GrVisualParallelepipedSurface3D SetAnimatedDirection1(GrVisualAnimatedVector3D direction1)
    {
        AnimatedDirection1 = direction1;

        return this;
    }
        
    public GrVisualParallelepipedSurface3D SetAnimatedDirection2(GrVisualAnimatedVector3D direction2)
    {
        AnimatedDirection2 = direction2;

        return this;
    }
        
    public GrVisualParallelepipedSurface3D SetAnimatedDirection3(GrVisualAnimatedVector3D direction3)
    {
        AnimatedDirection3 = direction3;

        return this;
    }

    public GrVisualParallelepipedSurface3D SetAnimatedPosition(GrVisualAnimatedVector3D position)
    {
        AnimatedPosition = position;

        return this;
    }

    public GrVisualParallelepipedSurface3D SetAnimatedPosition1(GrVisualAnimatedVector3D position1)
    {
        AnimatedDirection1 = 
            AnimatedPosition is null
                ? position1 - Position
                : position1 - AnimatedPosition;

        return this;
    }
        
    public GrVisualParallelepipedSurface3D SetAnimatedPosition2(GrVisualAnimatedVector3D position2)
    {
        AnimatedDirection2 = 
            AnimatedPosition is null
                ? position2 - Position
                : position2 - AnimatedPosition;

        return this;
    }
        
    public GrVisualParallelepipedSurface3D SetAnimatedPosition3(GrVisualAnimatedVector3D position3)
    {
        AnimatedDirection3 = 
            AnimatedPosition is null
                ? position3 - Position
                : position3 - AnimatedPosition;

        return this;
    }
    
    public GrVisualParallelepipedSurface3D SetAnimatedPositions(GrVisualAnimatedVector3D position1, GrVisualAnimatedVector3D position2, GrVisualAnimatedVector3D position3)
    {
        if (AnimatedPosition is null)
        {
            AnimatedDirection1 = position1 - Position;
            AnimatedDirection2 = position2 - Position;
            AnimatedDirection3 = position3 - Position;
        }
        else
        {
            AnimatedDirection1 = position1 - AnimatedPosition;
            AnimatedDirection2 = position2 - AnimatedPosition;
            AnimatedDirection3 = position3 - AnimatedPosition;
        }

        return this;
    }

    public GrVisualParallelepipedSurface3D SetAnimatedPositions(GrVisualAnimatedVector3D position, GrVisualAnimatedVector3D position1, GrVisualAnimatedVector3D position2, GrVisualAnimatedVector3D position3)
    {
        AnimatedPosition = position;
        AnimatedDirection1 = position1 - position;
        AnimatedDirection2 = position2 - position;
        AnimatedDirection3 = position3 - position;

        return this;
    }
    
    public GrVisualParallelepipedSurface3D SetAnimatedDirections(GrVisualAnimatedVector3D direction1, GrVisualAnimatedVector3D direction2, GrVisualAnimatedVector3D direction3)
    {
        AnimatedDirection1 = direction1;
        AnimatedDirection2 = direction2;
        AnimatedDirection3 = direction3;

        return this;
    }

    public GrVisualParallelepipedSurface3D SetAnimatedPositionDirections(GrVisualAnimatedVector3D position, GrVisualAnimatedVector3D direction1, GrVisualAnimatedVector3D direction2, GrVisualAnimatedVector3D direction3)
    {
        AnimatedPosition = position;
        AnimatedDirection1 = direction1;
        AnimatedDirection2 = direction2;
        AnimatedDirection3 = direction3;

        return this;
    }

    public LinFloat64Vector3D GetDirection1(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedDirection1 is null
            ? Direction1.ToLinVector3D()
            : AnimatedDirection1.GetPoint(time);
    }
        
    public LinFloat64Vector3D GetDirection2(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedDirection2 is null
            ? Direction2.ToLinVector3D()
            : AnimatedDirection2.GetPoint(time);
    }
        
    public LinFloat64Vector3D GetDirection3(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedDirection3 is null
            ? Direction3.ToLinVector3D()
            : AnimatedDirection3.GetPoint(time);
    }

    public LinFloat64Vector3D GetPosition(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedPosition is null
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
        
    public LinFloat64Vector3D GetPosition3(double time)
    {
        return GetPosition(time) + GetDirection3(time);
    }

    public LinFloat64Vector3D GetPosition12(double time)
    {
        return GetPosition(time) + 
               GetDirection1(time) + 
               GetDirection2(time);
    }
    
    public LinFloat64Vector3D GetPosition13(double time)
    {
        return GetPosition(time) + 
               GetDirection1(time) + 
               GetDirection3(time);
    }
    
    public LinFloat64Vector3D GetPosition23(double time)
    {
        return GetPosition(time) + 
               GetDirection2(time) + 
               GetDirection3(time);
    }
    
    public LinFloat64Vector3D GetPosition123(double time)
    {
        return GetPosition(time) + 
               GetDirection1(time) + 
               GetDirection2(time) + 
               GetDirection3(time);
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
                GetDirection2(time), 
                GetDirection3(time)
            );
        }
    }
}