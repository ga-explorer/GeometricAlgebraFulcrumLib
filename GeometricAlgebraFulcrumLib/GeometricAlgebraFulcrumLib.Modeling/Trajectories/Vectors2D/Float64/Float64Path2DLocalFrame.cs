using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64;

public sealed record Float64Path2DLocalFrame :
    ILinFloat64Vector2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2DLocalFrame Create(double t, ILinFloat64Vector2D point, ILinFloat64Vector2D tangent)
    {
        return new Float64Path2DLocalFrame(
            t,
            point.ToLinVector2D(),
            tangent.ToLinVector2D()
        );
    }


    /// <summary>
    /// The curve parameter value at the given curve point
    /// </summary>
    public Float64Scalar Time { get; }

    public int Index { get; internal set; } = -1;

    /// <summary>
    /// A point on the curve
    /// </summary>
    public LinFloat64Vector2D Point { get; }

    public int VSpaceDimensions
        => 2;

    public Float64Scalar Item1
        => Point.X;

    public Float64Scalar Item2
        => Point.Y;

    public Float64Scalar X
        => Point.X;

    public Float64Scalar Y
        => Point.Y;

    public Color Color { get; set; }

    /// <summary>
    /// The tangent unit vector to the curve at the given curve point
    /// </summary>
    public LinFloat64Vector2D Tangent { get; }

    public LinFloat64Vector2D Normal { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64Path2DLocalFrame(double t, ILinFloat64Vector2D point, ILinFloat64Vector2D tangent)
    {
        Time = t;
        Point = point.ToLinVector2D();
        Tangent = tangent.ToLinVector2D();
        Normal = Tangent.GetNormal();

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        var isValid =
            !double.IsNaN(Time) &&
            Point.IsValid() &&
            Tangent.IsValid() &&
            Tangent.VectorENormSquared().IsNearEqual(1);

        return isValid;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle GetMaxDirectionAngleWithFrame(Float64Path2DLocalFrame frame2)
    {
        //TODO: Is this correct?
        var maxAngle = LinFloat64PolarAngle.Angle0;

        var angle = Tangent.GetAngle(frame2.Tangent);
        if (maxAngle.RadiansValue < angle.RadiansValue) maxAngle = angle;

        return maxAngle;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Path2DLocalFrame TranslateBy(ILinFloat64Vector2D translationVector)
    {
        Debug.Assert(translationVector.IsValid());

        return new Float64Path2DLocalFrame(
            Time,
            LinFloat64Vector2D.Create(Point.X + translationVector.X,
                Point.Y + translationVector.Y),
            Tangent
        );
    }
}