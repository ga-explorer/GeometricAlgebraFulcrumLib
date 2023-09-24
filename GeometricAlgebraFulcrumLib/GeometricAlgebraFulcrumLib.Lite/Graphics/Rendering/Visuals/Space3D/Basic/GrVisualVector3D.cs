using System.Collections;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Basic;

public sealed class GrVisualVector3D :
    GrVisualElementWithAnimation3D,
    IGrVisualElementList3D
{
    public sealed record KeyFrameRecord(
        int FrameIndex,
        double Time,
        double Visibility,
        Float64Vector3D Origin,
        Float64Vector3D Direction
    ) : GrVisualAnimatedGeometryKeyFrameRecord(
        FrameIndex,
        Time,
        Visibility
    );


    public static GrVisualVector3D CreateStatic(string name, GrVisualCurveTubeStyle3D style, IFloat64Vector3D direction)
    {
        return new GrVisualVector3D(
            name,
            style,
            Float64Vector3D.Zero,
            direction,
            GrVisualAnimationSpecs.Static
        );
    }

    public static GrVisualVector3D CreateStatic(string name, GrVisualCurveTubeStyle3D style, IFloat64Vector3D origin, IFloat64Vector3D direction)
    {
        return new GrVisualVector3D(
            name,
            style,
            origin,
            direction,
            GrVisualAnimationSpecs.Static
        );
    }
    
    public static GrVisualVector3D Create(string name, GrVisualCurveTubeStyle3D style, IFloat64Vector3D origin, IFloat64Vector3D direction, GrVisualAnimationSpecs animationSpecs)
    {
        return new GrVisualVector3D(
            name,
            style,
            origin,
            direction,
            animationSpecs
        );
    }

    public static GrVisualVector3D CreateAnimated(string name, GrVisualCurveTubeStyle3D style, GrVisualAnimatedVector3D direction)
    {
        return new GrVisualVector3D(
            name,
            style,
            Float64Vector3D.Zero,
            Float64Vector3D.E1,
            direction.AnimationSpecs
        ).SetAnimatedDirection(direction);
    }

    public static GrVisualVector3D CreateAnimated(string name, GrVisualCurveTubeStyle3D style, IFloat64Vector3D origin, GrVisualAnimatedVector3D direction)
    {
        return new GrVisualVector3D(
            name,
            style,
            origin,
            Float64Vector3D.E1,
            direction.AnimationSpecs
        ).SetAnimatedDirection(direction);
    }

    public static GrVisualVector3D CreateAnimated(string name, GrVisualCurveTubeStyle3D style, GrVisualAnimatedVector3D origin, GrVisualAnimatedVector3D direction)
    {
        return new GrVisualVector3D(
            name,
            style,
            Float64Vector3D.Zero,
            Float64Vector3D.E1,
            origin.AnimationSpecs
        ).SetAnimatedOriginDirection(origin, direction);
    }
    

    public GrVisualCurveTubeStyle3D Style { get; }

    public IFloat64Vector3D Origin { get; }

    public IFloat64Vector3D Direction { get; }

    public IFloat64Vector3D Position
        => Origin.Add(Direction);

    //public bool FixLength { get; }

    //public double FixedLength { get; }

    public GrVisualAnimatedVector3D? AnimatedOrigin { get; set; }

    public GrVisualAnimatedVector3D? AnimatedDirection { get; set; }

    public GrVisualAnimatedVector3D? AnimatedPosition
    {
        get
        {
            if (AnimatedOrigin is not null && AnimatedDirection is not null)
                return AnimatedOrigin + AnimatedDirection;

            if (AnimatedOrigin is not null && AnimatedDirection is null)
                return AnimatedOrigin + Direction;

            if (AnimatedOrigin is null && AnimatedDirection is not null)
                return Origin + AnimatedDirection;

            return null;
        }
    }

    public int Count
        => 2;

    public IGrVisualElement3D this[int index]
    {
        get
        {
            return index switch
            {
                0 => GetVisualLineSegment(),
                1 => GetVisualArrowHead(),
                _ => throw new IndexOutOfRangeException()
            };
        }
    }


    private GrVisualVector3D(string name, GrVisualCurveTubeStyle3D style, IFloat64Vector3D origin, IFloat64Vector3D direction, GrVisualAnimationSpecs animationSpecs)
        : base(name, animationSpecs)
    {
        Origin = origin;
        Direction = direction;
        Style = style;

        Debug.Assert(IsValid());
    }
    

    public override bool IsValid()
    {
        return Origin.IsValid() &&
               Direction.IsValid() &&
               GetAnimatedGeometries().All(g => g.IsValid());
    }

    public Float64Vector3D GetVisualLineSegmentPosition2()
    {
        var origin = Origin.ToVector3D();
        var direction = Direction.ToVector3D();

        var cylinderHeight = Style.Thickness * 4.5d;
        var maxHeight = direction.ENorm();

        if (cylinderHeight > maxHeight)
            cylinderHeight = maxHeight;

        direction = direction.SetLength(
            Math.Max(1e-7d, maxHeight - cylinderHeight)
        );

        return origin + direction;
    }

    public Float64Vector3D GetVisualLineSegmentPosition2(double time)
    {
        var origin = GetOrigin(time);
        var direction = GetDirection(time);

        var cylinderHeight = Style.Thickness * 4.5d;
        var maxHeight = direction.ENorm();

        if (cylinderHeight > maxHeight)
            cylinderHeight = maxHeight;

        direction = direction.SetLength(
            Math.Max(1e-7d, maxHeight - cylinderHeight)
        );

        return origin + direction;
    }

    public GrVisualLineSegment3D GetVisualLineSegment()
    {
        var lineSegment = GrVisualLineSegment3D.Create(
            $"{Name}LineSegment",
            Style,
            Origin,
            GetVisualLineSegmentPosition2(),
            AnimationSpecs
        );

        lineSegment.Visibility = Visibility;

        if (AnimationSpecs.IsStatic) return lineSegment;
        
        lineSegment.AnimatedVisibility = AnimatedVisibility;
        lineSegment.AnimatedPosition1 = AnimatedOrigin;
        lineSegment.AnimatedPosition2 = AnimationSpecs.CreateAnimatedVector3D(GetVisualLineSegmentPosition2);
        
        //lineSegment.AddInvalidFrameIndices(
        //    GetInvalidFrameIndices()
        //);

        return lineSegment;
    }

    public GrVisualArrowHead3D GetVisualArrowHead()
    {
        var arrowHead = GrVisualArrowHead3D.Create(
            $"{Name}ArrowHead",
            Style,
            Origin.Add(Direction),
            Direction,
            AnimationSpecs
        );

        arrowHead.Visibility = Visibility;

        if (AnimationSpecs.IsStatic) return arrowHead;
        
        arrowHead.AnimatedVisibility = AnimatedVisibility;
        arrowHead.AnimatedPosition = AnimatedPosition;
        arrowHead.AnimatedDirection = AnimatedDirection;
        
        //arrowHead.AddInvalidFrameIndices(
        //    GetInvalidFrameIndices()
        //);

        return arrowHead;
    }

    public double GetLength()
    {
        return Direction.ENorm();
    }

    public Float64Vector3D GetUnitDirection()
    {
        return Direction.ToUnitVector();
    }

    public GrVisualVector3D SetAnimatedVisibility(GrVisualAnimatedScalar visibility)
    {
        AnimatedVisibility = visibility;

        return this;
    }

    public GrVisualVector3D SetAnimatedOrigin(GrVisualAnimatedVector3D origin)
    {
        AnimatedOrigin = origin;

        return this;
    }

    public GrVisualVector3D SetAnimatedDirection(GrVisualAnimatedVector3D direction)
    {
        AnimatedDirection = direction;

        return this;
    }

    public GrVisualVector3D SetAnimatedPosition(GrVisualAnimatedVector3D position)
    {
        AnimatedDirection =
            AnimatedOrigin is null
                ? position - Origin
                : position - AnimatedOrigin;

        return this;
    }

    public GrVisualVector3D SetAnimatedOriginDirection(GrVisualAnimatedVector3D origin, GrVisualAnimatedVector3D direction)
    {
        AnimatedOrigin = origin;
        AnimatedDirection = direction;

        return this;
    }

    public override IReadOnlyList<GrVisualAnimatedGeometry> GetAnimatedGeometries()
    {
        var animatedGeometries = new List<GrVisualAnimatedGeometry>(2);

        if (AnimatedVisibility is not null)
            animatedGeometries.Add(AnimatedVisibility);

        if (AnimatedOrigin is not null)
            animatedGeometries.Add(AnimatedOrigin);

        if (AnimatedDirection is not null)
            animatedGeometries.Add(AnimatedDirection);

        return animatedGeometries;
    }

    public Float64Vector3D GetOrigin(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedOrigin is null
            ? Origin.ToVector3D()
            : AnimatedOrigin.GetPoint(time);
    }

    public Float64Vector3D GetDirection(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedDirection is null
            ? Direction.ToVector3D()
            : AnimatedDirection.GetPoint(time);
    }

    public Float64Vector3D GetPosition(double time)
    {
        return GetOrigin(time) + GetDirection(time);
    }

    public IEnumerable<KeyFrameRecord> GetKeyVectorRecords()
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
                GetDirection(time)
            );
        }
    }

    public IEnumerator<IGrVisualElement3D> GetEnumerator()
    {
        yield return GetVisualLineSegment();
        yield return GetVisualArrowHead();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}