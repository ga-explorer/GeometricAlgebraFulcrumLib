using System.Collections;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Signals;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Basic;

public sealed class GrVisualVector3D :
    GrVisualElementWithAnimation3D,
    IGrVisualElementList3D
{
    public sealed record KeyFrameRecord(
        int FrameIndex,
        double Time,
        double Visibility,
        LinFloat64Vector3D Origin,
        LinFloat64Vector3D Direction
    ) : GrVisualAnimatedGeometryKeyFrameRecord(
        FrameIndex,
        Time,
        Visibility
    );


    public static GrVisualVector3D CreateStatic(string name, GrVisualCurveTubeStyle3D style, ILinFloat64Vector3D direction)
    {
        return new GrVisualVector3D(
            name,
            style,
            LinFloat64Vector3D.Zero,
            direction,
            Float64SamplingSpecs.Static
        );
    }

    public static GrVisualVector3D CreateStatic(string name, GrVisualCurveTubeStyle3D style, ILinFloat64Vector3D origin, ILinFloat64Vector3D direction)
    {
        return new GrVisualVector3D(
            name,
            style,
            origin,
            direction,
            Float64SamplingSpecs.Static
        );
    }
    
    public static GrVisualVector3D Create(string name, GrVisualCurveTubeStyle3D style, ILinFloat64Vector3D origin, ILinFloat64Vector3D direction, Float64SamplingSpecs samplingSpecs)
    {
        return new GrVisualVector3D(
            name,
            style,
            origin,
            direction,
            samplingSpecs
        );
    }

    public static GrVisualVector3D CreateAnimated(string name, GrVisualCurveTubeStyle3D style, GrVisualAnimatedVector3D direction)
    {
        return new GrVisualVector3D(
            name,
            style,
            LinFloat64Vector3D.Zero,
            LinFloat64Vector3D.E1,
            direction.SamplingSpecs
        ).SetAnimatedDirection(direction);
    }

    public static GrVisualVector3D CreateAnimated(string name, GrVisualCurveTubeStyle3D style, ILinFloat64Vector3D origin, GrVisualAnimatedVector3D direction)
    {
        return new GrVisualVector3D(
            name,
            style,
            origin,
            LinFloat64Vector3D.E1,
            direction.SamplingSpecs
        ).SetAnimatedDirection(direction);
    }

    public static GrVisualVector3D CreateAnimated(string name, GrVisualCurveTubeStyle3D style, GrVisualAnimatedVector3D origin, GrVisualAnimatedVector3D direction)
    {
        return new GrVisualVector3D(
            name,
            style,
            LinFloat64Vector3D.Zero,
            LinFloat64Vector3D.E1,
            origin.SamplingSpecs
        ).SetAnimatedOriginDirection(origin, direction);
    }
    

    public GrVisualCurveTubeStyle3D Style { get; }

    public ILinFloat64Vector3D Origin { get; }

    public ILinFloat64Vector3D Direction { get; }

    public ILinFloat64Vector3D Position
        => Origin.VectorAdd(Direction);

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


    private GrVisualVector3D(string name, GrVisualCurveTubeStyle3D style, ILinFloat64Vector3D origin, ILinFloat64Vector3D direction, Float64SamplingSpecs samplingSpecs)
        : base(name, samplingSpecs)
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

    public LinFloat64Vector3D GetVisualLineSegmentPosition2()
    {
        var origin = Origin.ToLinVector3D();
        var direction = Direction.ToLinVector3D();

        var cylinderHeight = Style.Thickness * 4.5d;
        var maxHeight = direction.VectorENorm();

        if (cylinderHeight > maxHeight)
            cylinderHeight = maxHeight;

        direction = direction.SetLength(
            Math.Max(1e-7d, maxHeight - cylinderHeight)
        );

        return origin + direction;
    }

    public LinFloat64Vector3D GetVisualLineSegmentPosition2(double time)
    {
        var origin = GetOrigin(time);
        var direction = GetDirection(time);

        var cylinderHeight = Style.Thickness * 4.5d;
        var maxHeight = direction.VectorENorm();

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
            SamplingSpecs
        );

        lineSegment.Visibility = Visibility;

        if (SamplingSpecs.IsStatic) return lineSegment;
        
        lineSegment.AnimatedVisibility = AnimatedVisibility;
        lineSegment.AnimatedPosition1 = AnimatedOrigin;
        lineSegment.AnimatedPosition2 = SamplingSpecs.CreateAnimatedVector3D(GetVisualLineSegmentPosition2);
        
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
            Origin.VectorAdd(Direction),
            Direction,
            SamplingSpecs
        );

        arrowHead.Visibility = Visibility;

        if (SamplingSpecs.IsStatic) return arrowHead;
        
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
        return Direction.VectorENorm();
    }

    public LinFloat64Vector3D GetUnitDirection()
    {
        return Direction.ToUnitLinVector3D();
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

    public LinFloat64Vector3D GetOrigin(double time)
    {
        return SamplingSpecs.IsStatic || AnimatedOrigin is null
            ? Origin.ToLinVector3D()
            : AnimatedOrigin.GetValue(time);
    }

    public LinFloat64Vector3D GetDirection(double time)
    {
        return SamplingSpecs.IsStatic || AnimatedDirection is null
            ? Direction.ToLinVector3D()
            : AnimatedDirection.GetValue(time);
    }

    public LinFloat64Vector3D GetPosition(double time)
    {
        return GetOrigin(time) + GetDirection(time);
    }

    public IEnumerable<KeyFrameRecord> GetKeyVectorRecords()
    {
        Debug.Assert(IsValid());

        foreach (var frameIndex in GetValidFrameIndexSet())
        {
            var time = (double)frameIndex / SamplingSpecs.SamplingRate;

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