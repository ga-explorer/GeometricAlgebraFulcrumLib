using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves.Bezier;

public class BezierCurve3Degree3D :
    IParametricC2Curve3D
{
    public LinFloat64Vector3D Point1 { get; }

    public LinFloat64Vector3D Point2 { get; }

    public LinFloat64Vector3D Point3 { get; }

    public LinFloat64Vector3D Point4 { get; }
        
    public Float64ScalarRange ParameterRange 
        => Float64ScalarRange.Infinite;


    public BezierCurve3Degree3D(ILinFloat64Vector3D point1, ILinFloat64Vector3D point2, ILinFloat64Vector3D point3, ILinFloat64Vector3D point4)
    {
        Point1 = point1.ToLinVector3D();
        Point2 = point2.ToLinVector3D();
        Point3 = point3.ToLinVector3D();
        Point4 = point4.ToLinVector3D();

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Point1.IsValid() &&
               Point2.IsValid() &&
               Point3.IsValid() &&
               Point4.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BezierCurve2Degree3D GetDerivativeCurve()
    {
        return new BezierCurve2Degree3D(
            3 * (Point2 - Point1),
            3 * (Point3 - Point2),
            3 * (Point4 - Point3)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetPoint(double parameterValue)
    {
        var (p1, p2, p3, p4) = parameterValue.BernsteinBasis_3();

        return LinFloat64Vector3D.Create(p1 * Point1.X + p2 * Point2.X + p3 * Point3.X + p4 * Point4.X,
            p1 * Point1.Y + p2 * Point2.Y + p3 * Point3.Y + p4 * Point4.Y,
            p1 * Point1.Z + p2 * Point2.Z + p3 * Point3.Z + p4 * Point4.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetDerivative1Point(double parameterValue)
    {
        var s = 1 - parameterValue;

        var p1 = 3 * s * s;
        var p2 = 6 * parameterValue * s;
        var p3 = 3 * parameterValue * parameterValue;

        return LinFloat64Vector3D.Create(p1 * (Point2.X - Point1.X) + p2 * (Point3.X - Point2.X) + p3 * (Point4.X - Point3.X),
            p1 * (Point2.Y - Point1.Y) + p2 * (Point3.Y - Point2.Y) + p3 * (Point4.Y - Point3.Y),
            p1 * (Point2.Z - Point1.Z) + p2 * (Point3.Z - Point2.Z) + p3 * (Point4.Z - Point3.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
    {
        return ParametricCurveLocalFrame3D.Create(
            parameterValue,
            GetPoint(parameterValue),
            GetDerivative1Point(parameterValue)
        );
    }

    public LinFloat64Vector3D GetDerivative2Point(double parameterValue)
    {
        var derivative2 =
            GetDerivativeCurve().GetDerivativeCurve();

        return derivative2.GetPoint(parameterValue);
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