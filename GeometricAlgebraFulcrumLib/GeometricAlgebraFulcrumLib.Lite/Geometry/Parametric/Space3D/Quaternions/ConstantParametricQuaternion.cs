using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Quaternions;

public class ConstantParametricQuaternion :
    IParametricQuaternion
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricQuaternion Create(Float64Quaternion point)
    {
        return new ConstantParametricQuaternion(
            point,
            Float64Quaternion.Identity
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricQuaternion Create(Float64Quaternion point, Float64Quaternion tangent)
    {
        return new ConstantParametricQuaternion(
            point,
            tangent
        );
    }


    public Float64Quaternion Point { get; }

    public Float64Quaternion Tangent { get; }

    public Float64ScalarRange ParameterRange
        => Float64ScalarRange.Infinite;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ConstantParametricQuaternion(Float64Quaternion point, Float64Quaternion tangent)
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
    public Float64Quaternion GetQuaternion(double parameterValue)
    {
        return Point;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Quaternion GetDerivative1Quaternion(double parameterValue)
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