using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier;

public sealed class Float64Bezier3Path2D :
    Float64Path2D
{
    public LinFloat64Vector2D Point1 { get; }

    public LinFloat64Vector2D Point2 { get; }

    public LinFloat64Vector2D Point3 { get; }

    public LinFloat64Vector2D Point4 { get; }


    public Float64Bezier3Path2D(bool isPeriodic, ILinFloat64Vector2D point1, ILinFloat64Vector2D point2, ILinFloat64Vector2D point3, ILinFloat64Vector2D point4)
        : base(Float64ScalarRange.ZeroToOne, isPeriodic)
    {
        Point1 = point1.ToLinVector2D();
        Point2 = point2.ToLinVector2D();
        Point3 = point3.ToLinVector2D();
        Point4 = point4.ToLinVector2D();

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return Point1.IsValid() &&
               Point2.IsValid() &&
               Point3.IsValid() &&
               Point4.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Bezier2Path2D GetDerivativeCurve()
    {
        return new Float64Bezier2Path2D(
            IsPeriodic,
            3 * (Point2 - Point1),
            3 * (Point3 - Point2),
            3 * (Point4 - Point3)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetValue(double t)
    {
        var (p1, p2, p3, p4) = t.BernsteinBasis_3();

        return LinFloat64Vector2D.Create(p1 * Point1.X + p2 * Point2.X + p3 * Point3.X + p4 * Point4.X,
            p1 * Point1.Y + p2 * Point2.Y + p3 * Point3.Y + p4 * Point4.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetTangent(double t)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetUnitTangent(double t)
    {
        throw new NotImplementedException();
    }

    public override Float64Path2D ToFinitePath()
    {
        throw new NotImplementedException();
    }

    public override Float64Path2D ToPeriodicPath()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative1Value(double t)
    {
        var s = 1 - t;

        var p1 = 3 * s * s;
        var p2 = 6 * t * s;
        var p3 = 3 * t * t;

        return LinFloat64Vector2D.Create(p1 * (Point2.X - Point1.X) + p2 * (Point3.X - Point2.X) + p3 * (Point4.X - Point3.X),
            p1 * (Point2.Y - Point1.Y) + p2 * (Point3.Y - Point2.Y) + p3 * (Point4.Y - Point3.Y));
    }

    public override LinFloat64Vector2D GetDerivative2Value(double t)
    {
        var derivative2 =
            GetDerivativeCurve().GetDerivativeCurve();

        return derivative2.GetValue(t);
    }

    //public GrParametricCurveLocalFrame2D GetFrenetFrame(double t)
    //{
    //    var derivative2 = GetDerivativeCurve().GetDerivativeCurve();

    //    var firstDerivativeVector = GetTangent(t);
    //    var secondDerivativeVector = derivative2.GetPoint(t);

    //    return GrParametricCurveLocalFrame2D.CreateFrenetFrame(
    //        t,
    //        GetPoint(t),
    //        firstDerivativeVector, 
    //        secondDerivativeVector
    //    );
    //}

    //public GrParametricCurveLocalFrame2D GetFrenetFrameAt0()
    //{
    //    var firstDerivativeVector = 3 * (Point2 - Point1);
    //    var secondDerivativeVector = 6 * (Point3 - 2 * Point2 + Point1);

    //    return GrParametricCurveLocalFrame2D.CreateFrenetFrame(
    //        0,
    //        Point1,
    //        firstDerivativeVector, 
    //        secondDerivativeVector
    //    );
    //}
}