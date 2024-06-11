using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves.Lines;

public class ParametricLine3D :
    IParametricC2Curve3D
{
    public Float64ScalarRange ParameterRange
        => Float64ScalarRange.Infinite;

    public LinFloat64Vector3D Point { get; }

    public LinFloat64Vector3D Vector { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricLine3D(ILinFloat64Vector3D point, ILinFloat64Vector3D vector)
    {
        Point = point.ToLinVector3D();
        Vector = vector.ToLinVector3D();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Point.IsValid() &&
               Vector.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetPoint(double parameterValue)
    {
        return LinFloat64Vector3D.Create(Point.X + parameterValue * Vector.X,
            Point.Y + parameterValue * Vector.Y,
            Point.Z + parameterValue * Vector.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetDerivative1Point(double parameterValue)
    {
        return Vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
    {
        return ParametricCurveLocalFrame3D.Create(
            parameterValue,
            GetPoint(parameterValue),
            Vector.ToUnitLinVector3D()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetDerivative2Point(double parameterValue)
    {
        return LinFloat64Vector3D.Zero;
    }
}