using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Quaternions;

public class ConstantParametricQuaternion :
    IParametricQuaternion
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricQuaternion Create(LinFloat64Quaternion point)
    {
        return new ConstantParametricQuaternion(
            point,
            LinFloat64Quaternion.Identity
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricQuaternion Create(LinFloat64Quaternion point, LinFloat64Quaternion tangent)
    {
        return new ConstantParametricQuaternion(
            point,
            tangent
        );
    }


    public LinFloat64Quaternion Point { get; }

    public LinFloat64Quaternion Tangent { get; }

    public Float64ScalarRange ParameterRange
        => Float64ScalarRange.Infinite;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ConstantParametricQuaternion(LinFloat64Quaternion point, LinFloat64Quaternion tangent)
    {
        Point = point;
        Tangent = tangent;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Point.IsValid() &&
               Tangent.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Quaternion GetQuaternion(double parameterValue)
    {
        return Point;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Quaternion GetDerivative1Quaternion(double parameterValue)
    {
        return Tangent;
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public ParametricCurveLocalFrame4D GetFrame(double parameterValue)
    //{
    //    return ParametricCurveLocalFrame4D.Create(
    //        parameterValue,
    //        Point,
    //        Tangent
    //    );
    //}
}