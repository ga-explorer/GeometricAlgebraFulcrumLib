using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Bezier;

public sealed class Float64Bezier3Path3D :
    Float64Path3D
{
    public LinFloat64Vector3D Point1 { get; }

    public LinFloat64Vector3D Point2 { get; }

    public LinFloat64Vector3D Point3 { get; }

    public LinFloat64Vector3D Point4 { get; }


    public Float64Bezier3Path3D(bool isPeriodic, ILinFloat64Vector3D point1, ILinFloat64Vector3D point2, ILinFloat64Vector3D point3, ILinFloat64Vector3D point4)
        : base(Float64ScalarRange.ZeroToOne, isPeriodic)
    {
        Point1 = point1.ToLinVector3D();
        Point2 = point2.ToLinVector3D();
        Point3 = point3.ToLinVector3D();
        Point4 = point4.ToLinVector3D();

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
    public Float64Bezier2Path3D GetDerivativeCurve()
    {
        return new Float64Bezier2Path3D(
            IsPeriodic,
            3 * (Point2 - Point1),
            3 * (Point3 - Point2),
            3 * (Point4 - Point3)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetValue(double parameterValue)
    {
        var (p1, p2, p3, p4) = parameterValue.BernsteinBasis_3();

        return LinFloat64Vector3D.Create(p1 * Point1.X + p2 * Point2.X + p3 * Point3.X + p4 * Point4.X,
            p1 * Point1.Y + p2 * Point2.Y + p3 * Point3.Y + p4 * Point4.Y,
            p1 * Point1.Z + p2 * Point2.Z + p3 * Point3.Z + p4 * Point4.Z);
    }

    public override Float64Path3D ToFinitePath()
    {
        throw new NotImplementedException();
    }

    public override Float64Path3D ToPeriodicPath()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative1Value(double parameterValue)
    {
        var s = 1 - parameterValue;

        var p1 = 3 * s * s;
        var p2 = 6 * parameterValue * s;
        var p3 = 3 * parameterValue * parameterValue;

        return LinFloat64Vector3D.Create(p1 * (Point2.X - Point1.X) + p2 * (Point3.X - Point2.X) + p3 * (Point4.X - Point3.X),
            p1 * (Point2.Y - Point1.Y) + p2 * (Point3.Y - Point2.Y) + p3 * (Point4.Y - Point3.Y),
            p1 * (Point2.Z - Point1.Z) + p2 * (Point3.Z - Point2.Z) + p3 * (Point4.Z - Point3.Z));
    }


    public override LinFloat64Vector3D GetDerivative2Value(double parameterValue)
    {
        var derivative2 =
            GetDerivativeCurve().GetDerivativeCurve();

        return derivative2.GetValue(parameterValue);
    }

    //public GrParametricCurveLocalFrame3D GetFrenetFrame(double parameterValue)
    //{
    //    var derivative2 = GetDerivativeCurve().GetDerivativeCurve();

    //    var firstDerivativeVector = GetTangent(parameterValue);
    //    var secondDerivativeVector = derivative2.GetPoint(parameterValue);

    //    return GrParametricCurveLocalFrame3D.CreateFrenetFrame(
    //        parameterValue,
    //        GetPoint(parameterValue),
    //        firstDerivativeVector, 
    //        secondDerivativeVector
    //    );
    //}

    //public GrParametricCurveLocalFrame3D GetFrenetFrameAt0()
    //{
    //    var firstDerivativeVector = 3 * (Point2 - Point1);
    //    var secondDerivativeVector = 6 * (Point3 - 2 * Point2 + Point1);

    //    return GrParametricCurveLocalFrame3D.CreateFrenetFrame(
    //        0,
    //        Point1,
    //        firstDerivativeVector, 
    //        secondDerivativeVector
    //    );
    //}
}