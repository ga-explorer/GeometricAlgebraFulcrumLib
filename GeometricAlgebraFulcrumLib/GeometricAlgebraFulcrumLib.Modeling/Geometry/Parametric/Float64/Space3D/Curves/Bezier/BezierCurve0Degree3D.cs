using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves.Bezier;

public class BezierCurve0Degree3D :
    IParametricCurve3D
{
    public LinFloat64Vector3D Point1 { get; }

    public Float64ScalarRange ParameterRange
        => Float64ScalarRange.Infinite;


    public BezierCurve0Degree3D(ILinFloat64Vector3D point1)
    {
        Point1 = point1.ToLinVector3D();

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Point1.IsValid();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetPoint(double t)
    {
        return Point1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetDerivative1Point(double t)
    {
        return LinFloat64Vector3D.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
    {
        return ParametricCurveLocalFrame3D.Create(
            parameterValue,
            Point1,
            LinFloat64Vector3D.UnitSymmetric
        );
    }
}