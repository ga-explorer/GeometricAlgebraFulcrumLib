using System.Collections;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Basic;

public sealed class GrVisualFrame3D :
    GrVisualElementWithAnimation3D,
    IGrVisualElementList3D
{
    public sealed record KeyFrameRecord(
        int FrameIndex,
        double Time,
        double Visibility,
        LinFloat64Vector3D Origin,
        LinFloat64Vector3D Direction1,
        LinFloat64Vector3D Direction2,
        LinFloat64Vector3D Direction3
    ) : GrVisualAnimatedGeometryKeyFrameRecord(
        FrameIndex,
        Time,
        Visibility
    );


    public static GrVisualFrame3D CreateStatic(string name, GrVisualFrameStyle3D style, ILinFloat64Vector3D origin)
    {
        return new GrVisualFrame3D(
            name,
            style,
            origin,
            LinFloat64Vector3D.E1,
            LinFloat64Vector3D.E2,
            LinFloat64Vector3D.E3,
            GrVisualAnimationSpecs.Static
        );
    }

    public static GrVisualFrame3D CreateStatic(string name, GrVisualFrameStyle3D style, ILinFloat64Vector3D origin, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, ILinFloat64Vector3D direction3)
    {
        return new GrVisualFrame3D(
            name,
            style,
            origin,
            direction1,
            direction2,
            direction3,
            GrVisualAnimationSpecs.Static
        );
    }

    public static GrVisualFrame3D Create(string name, GrVisualFrameStyle3D style, ILinFloat64Vector3D origin, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, ILinFloat64Vector3D direction3, GrVisualAnimationSpecs animationSpecs)
    {
        return new GrVisualFrame3D(
            name,
            style,
            origin,
            direction1,
            direction2,
            direction3,
            animationSpecs
        );
    }

    public static GrVisualFrame3D CreateAnimated(string name, GrVisualFrameStyle3D style, GrVisualAnimatedVector3D origin, GrVisualAnimatedVector3D direction1, GrVisualAnimatedVector3D direction2, GrVisualAnimatedVector3D direction3)
    {
        return new GrVisualFrame3D(
                name,
                style,
                LinFloat64Vector3D.Zero,
                LinFloat64Vector3D.E1,
                LinFloat64Vector3D.E2,
                LinFloat64Vector3D.E3,
                origin.AnimationSpecs
            ).SetAnimatedOrigin(origin)
            .SetAnimatedDirection1(direction1)
            .SetAnimatedDirection2(direction2)
            .SetAnimatedDirection3(direction3);
    }


    public GrVisualFrameStyle3D Style { get; }

    public ILinFloat64Vector3D Origin { get; }

    public ILinFloat64Vector3D Direction1 { get; }

    public ILinFloat64Vector3D Direction2 { get; }

    public ILinFloat64Vector3D Direction3 { get; }

    public ILinFloat64Vector3D Position1
        => Origin.VectorAdd(Direction1);

    public ILinFloat64Vector3D Position2
        => Origin.VectorAdd(Direction2);

    public ILinFloat64Vector3D Position3
        => Origin.VectorAdd(Direction3);

    public GrVisualAnimatedVector3D? AnimatedOrigin { get; set; }

    public GrVisualAnimatedVector3D? AnimatedDirection1 { get; set; }

    public GrVisualAnimatedVector3D? AnimatedDirection2 { get; set; }

    public GrVisualAnimatedVector3D? AnimatedDirection3 { get; set; }

    public GrVisualAnimatedVector3D? AnimatedPosition1
    {
        get
        {
            if (AnimatedOrigin is not null && AnimatedDirection1 is not null)
                return AnimatedOrigin + AnimatedDirection1;

            if (AnimatedOrigin is not null && AnimatedDirection1 is null)
                return AnimatedOrigin + Direction1;

            if (AnimatedOrigin is null && AnimatedDirection1 is not null)
                return Origin + AnimatedDirection1;

            return null;
        }
    }

    public GrVisualAnimatedVector3D? AnimatedPosition2
    {
        get
        {
            if (AnimatedOrigin is not null && AnimatedDirection2 is not null)
                return AnimatedOrigin + AnimatedDirection2;

            if (AnimatedOrigin is not null && AnimatedDirection2 is null)
                return AnimatedOrigin + Direction2;

            if (AnimatedOrigin is null && AnimatedDirection2 is not null)
                return Origin + AnimatedDirection2;

            return null;
        }
    }

    public GrVisualAnimatedVector3D? AnimatedPosition3
    {
        get
        {
            if (AnimatedOrigin is not null && AnimatedDirection3 is not null)
                return AnimatedOrigin + AnimatedDirection3;

            if (AnimatedOrigin is not null && AnimatedDirection3 is null)
                return AnimatedOrigin + Direction3;

            if (AnimatedOrigin is null && AnimatedDirection3 is not null)
                return Origin + AnimatedDirection3;

            return null;
        }
    }

    public int Count
        => 7;

    public IGrVisualElement3D this[int index]
    {
        get
        {
            return index switch
            {
                0 => GetVisualOrigin(),
                1 => GetVisualLineSegment1(),
                2 => GetVisualLineSegment2(),
                3 => GetVisualLineSegment3(),
                4 => GetVisualArrowHead1(),
                5 => GetVisualArrowHead2(),
                6 => GetVisualArrowHead3(),
                _ => throw new IndexOutOfRangeException()
            };
        }
    }


    private GrVisualFrame3D(string name, GrVisualFrameStyle3D style, ILinFloat64Vector3D origin, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, ILinFloat64Vector3D direction3, GrVisualAnimationSpecs animationSpecs)
        : base(name, animationSpecs)
    {
        Origin = origin;
        Direction1 = direction1;
        Direction2 = direction2;
        Direction3 = direction3;
        Style = style;
    }


    public override bool IsValid()
    {
        return Origin.IsValid() &&
               Direction1.IsValid() &&
               Direction2.IsValid() &&
               Direction3.IsValid() &&
               GetAnimatedGeometries().All(g => g.IsValid());
    }


    public LinFloat64Vector3D GetVisualLineSegmentEndPosition(LinFloat64Vector3D origin, LinFloat64Vector3D direction)
    {
        var cylinderHeight = Style.Direction1Style.Thickness * 4.5d;
        var maxHeight = direction.VectorENorm();

        if (cylinderHeight > maxHeight)
            cylinderHeight = maxHeight;

        direction = direction.SetLength(
            Math.Max(1e-7d, maxHeight - cylinderHeight)
        );

        return origin + direction;
    }

    public LinFloat64Vector3D GetVisualLineSegment1EndPosition()
    {
        return GetVisualLineSegmentEndPosition(
            Origin.ToLinVector3D(),
            Direction1.ToLinVector3D()
        );
    }

    public LinFloat64Vector3D GetVisualLineSegment2EndPosition()
    {
        return GetVisualLineSegmentEndPosition(
            Origin.ToLinVector3D(),
            Direction2.ToLinVector3D()
        );
    }

    public LinFloat64Vector3D GetVisualLineSegment3EndPosition()
    {
        return GetVisualLineSegmentEndPosition(
            Origin.ToLinVector3D(),
            Direction3.ToLinVector3D()
        );
    }

    public LinFloat64Vector3D GetVisualLineSegment1EndPosition(double time)
    {
        return GetVisualLineSegmentEndPosition(
            GetOrigin(time),
            GetDirection1(time)
        );
    }

    public LinFloat64Vector3D GetVisualLineSegment2EndPosition(double time)
    {
        return GetVisualLineSegmentEndPosition(
            GetOrigin(time),
            GetDirection2(time)
        );
    }

    public LinFloat64Vector3D GetVisualLineSegment3EndPosition(double time)
    {
        return GetVisualLineSegmentEndPosition(
            GetOrigin(time),
            GetDirection3(time)
        );
    }


    public GrVisualPoint3D GetVisualOrigin()
    {
        var point = GrVisualPoint3D.Create(
            $"{Name}Origin",
            Style.OriginStyle,
            Origin,
            AnimationSpecs
        );

        point.Visibility = Visibility;
            
        if (AnimationSpecs.IsStatic) return point;

        point.AnimatedVisibility = AnimatedVisibility;
        point.AnimatedPosition = AnimatedOrigin;

        return point;
    }

    public GrVisualLineSegment3D GetVisualLineSegment1()
    {
        var lineSegment = GrVisualLineSegment3D.Create(
            $"{Name}LineSegment1",
            Style.Direction1Style,
            Origin,
            GetVisualLineSegment1EndPosition(),
            AnimationSpecs
        );
            
        lineSegment.Visibility = Visibility;

        if (AnimationSpecs.IsStatic) return lineSegment;

        lineSegment.AnimatedVisibility = AnimatedVisibility;
        lineSegment.AnimatedPosition1 = AnimatedOrigin;
        lineSegment.AnimatedPosition2 = AnimationSpecs.CreateAnimatedVector3D(GetVisualLineSegment1EndPosition);
            
        //lineSegment.AddInvalidFrameIndices(
        //    GetInvalidFrameIndices()
        //);

        return lineSegment;
    }

    public GrVisualLineSegment3D GetVisualLineSegment2()
    {
        var lineSegment = GrVisualLineSegment3D.Create(
            $"{Name}LineSegment2",
            Style.Direction2Style,
            Origin,
            GetVisualLineSegment2EndPosition(),
            AnimationSpecs
        );
            
        lineSegment.Visibility = Visibility;

        if (AnimationSpecs.IsStatic) return lineSegment;

        lineSegment.AnimatedVisibility = AnimatedVisibility;
        lineSegment.AnimatedPosition1 = AnimatedOrigin;
        lineSegment.AnimatedPosition2 = AnimationSpecs.CreateAnimatedVector3D(GetVisualLineSegment2EndPosition);
            
        //lineSegment.AddInvalidFrameIndices(
        //    GetInvalidFrameIndices()
        //);

        return lineSegment;
    }

    public GrVisualLineSegment3D GetVisualLineSegment3()
    {
        var lineSegment = GrVisualLineSegment3D.Create(
            $"{Name}LineSegment3",
            Style.Direction3Style,
            Origin,
            GetVisualLineSegment3EndPosition(),
            AnimationSpecs
        );

        lineSegment.Visibility = Visibility;

        if (AnimationSpecs.IsStatic) return lineSegment;

        lineSegment.AnimatedVisibility = AnimatedVisibility;
        lineSegment.AnimatedPosition1 = AnimatedOrigin;
        lineSegment.AnimatedPosition2 = AnimationSpecs.CreateAnimatedVector3D(GetVisualLineSegment3EndPosition);
            
        //lineSegment.AddInvalidFrameIndices(
        //    GetInvalidFrameIndices()
        //);

        return lineSegment;
    }

    public GrVisualArrowHead3D GetVisualArrowHead1()
    {
        var arrowHead = GrVisualArrowHead3D.Create(
            $"{Name}ArrowHead1",
            Style.Direction1Style,
            Origin.VectorAdd(Direction1),
            Direction1,
            AnimationSpecs
        );

        arrowHead.Visibility = Visibility;

        if (AnimationSpecs.IsStatic) return arrowHead;

        arrowHead.AnimatedVisibility = AnimatedVisibility;
        arrowHead.AnimatedPosition = AnimatedPosition1;
        arrowHead.AnimatedDirection = AnimatedDirection1;
            
        //arrowHead.AddInvalidFrameIndices(
        //    GetInvalidFrameIndices()
        //);

        return arrowHead;
    }

    public GrVisualArrowHead3D GetVisualArrowHead2()
    {
        var arrowHead = GrVisualArrowHead3D.Create(
            $"{Name}ArrowHead2",
            Style.Direction2Style,
            Origin.VectorAdd(Direction2),
            Direction2,
            AnimationSpecs
        );

        arrowHead.Visibility = Visibility;

        if (AnimationSpecs.IsStatic) return arrowHead;

        arrowHead.AnimatedVisibility = AnimatedVisibility;
        arrowHead.AnimatedPosition = AnimatedPosition2;
        arrowHead.AnimatedDirection = AnimatedDirection2;
            
        //arrowHead.AddInvalidFrameIndices(
        //    GetInvalidFrameIndices()
        //);

        return arrowHead;
    }

    public GrVisualArrowHead3D GetVisualArrowHead3()
    {
        var arrowHead = GrVisualArrowHead3D.Create(
            $"{Name}ArrowHead3",
            Style.Direction3Style,
            Origin.VectorAdd(Direction3),
            Direction3,
            AnimationSpecs
        );

        arrowHead.Visibility = Visibility;

        if (AnimationSpecs.IsStatic) return arrowHead;

        arrowHead.AnimatedVisibility = AnimatedVisibility;
        arrowHead.AnimatedPosition = AnimatedPosition3;
        arrowHead.AnimatedDirection = AnimatedDirection3;
            
        //arrowHead.AddInvalidFrameIndices(
        //    GetInvalidFrameIndices()
        //);

        return arrowHead;
    }


    public double GetLength1()
    {
        return Direction1.VectorENorm();
    }

    public double GetLength2()
    {
        return Direction2.VectorENorm();
    }

    public double GetLength3()
    {
        return Direction3.VectorENorm();
    }

    public LinFloat64Vector3D GetUnitDirection1()
    {
        return Direction1.ToUnitLinVector3D();
    }

    public LinFloat64Vector3D GetUnitDirection2()
    {
        return Direction2.ToUnitLinVector3D();
    }

    public LinFloat64Vector3D GetUnitDirection3()
    {
        return Direction3.ToUnitLinVector3D();
    }


    public GrVisualFrame3D SetAnimatedVisibility(GrVisualAnimatedScalar visibility)
    {
        AnimatedVisibility = visibility;

        return this;
    }

    public GrVisualFrame3D SetAnimatedOrigin(GrVisualAnimatedVector3D origin)
    {
        AnimatedOrigin = origin;

        return this;
    }

    public GrVisualFrame3D SetAnimatedDirection1(GrVisualAnimatedVector3D direction1)
    {
        AnimatedDirection1 = direction1;

        return this;
    }

    public GrVisualFrame3D SetAnimatedDirection2(GrVisualAnimatedVector3D direction2)
    {
        AnimatedDirection2 = direction2;

        return this;
    }

    public GrVisualFrame3D SetAnimatedDirection3(GrVisualAnimatedVector3D direction3)
    {
        AnimatedDirection3 = direction3;

        return this;
    }

    public GrVisualFrame3D SetAnimatedPosition1(GrVisualAnimatedVector3D position1)
    {
        AnimatedDirection1 =
            AnimatedOrigin is null
                ? position1 - Origin
                : position1 - AnimatedOrigin;

        return this;
    }

    public GrVisualFrame3D SetAnimatedPosition2(GrVisualAnimatedVector3D position2)
    {
        AnimatedDirection1 =
            AnimatedOrigin is null
                ? position2 - Origin
                : position2 - AnimatedOrigin;

        return this;
    }

    public GrVisualFrame3D SetAnimatedPosition3(GrVisualAnimatedVector3D position3)
    {
        AnimatedDirection1 =
            AnimatedOrigin is null
                ? position3 - Origin
                : position3 - AnimatedOrigin;

        return this;
    }


    public override IReadOnlyList<GrVisualAnimatedGeometry> GetAnimatedGeometries()
    {
        var animatedGeometries = new List<GrVisualAnimatedGeometry>(2);

        if (AnimatedVisibility is not null)
            animatedGeometries.Add(AnimatedVisibility);

        if (AnimatedOrigin is not null)
            animatedGeometries.Add(AnimatedOrigin);

        if (AnimatedDirection1 is not null)
            animatedGeometries.Add(AnimatedDirection1);

        if (AnimatedDirection2 is not null)
            animatedGeometries.Add(AnimatedDirection2);

        if (AnimatedDirection3 is not null)
            animatedGeometries.Add(AnimatedDirection3);

        return animatedGeometries;
    }

    public LinFloat64Vector3D GetOrigin(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedOrigin is null
            ? Origin.ToLinVector3D()
            : AnimatedOrigin.GetPoint(time);
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

    public LinFloat64Vector3D GetPosition1(double time)
    {
        return GetOrigin(time) + GetDirection1(time);
    }

    public LinFloat64Vector3D GetPosition2(double time)
    {
        return GetOrigin(time) + GetDirection2(time);
    }

    public LinFloat64Vector3D GetPosition3(double time)
    {
        return GetOrigin(time) + GetDirection3(time);
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
                GetDirection1(time),
                GetDirection2(time),
                GetDirection3(time)
            );
        }
    }

    public IEnumerator<IGrVisualElement3D> GetEnumerator()
    {
        yield return GetVisualOrigin();
        yield return GetVisualLineSegment1();
        yield return GetVisualLineSegment2();
        yield return GetVisualLineSegment3();
        yield return GetVisualArrowHead1();
        yield return GetVisualArrowHead2();
        yield return GetVisualArrowHead3();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}