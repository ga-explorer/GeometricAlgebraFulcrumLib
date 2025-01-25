using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Quaternions;

public class ComputedParametricQuaternion :
    IParametricQuaternion
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricQuaternion Create(Float64ScalarRange parameterRange, Func<double, LinFloat64Quaternion> getPointFunc)
    {
        return new ComputedParametricQuaternion(
            parameterRange,
            getPointFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricQuaternion Create(Func<double, LinFloat64Quaternion> getPointFunc)
    {
        return new ComputedParametricQuaternion(
            Float64ScalarRange.Infinite,
            getPointFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricQuaternion Create(Float64ScalarRange parameterRange, Func<double, LinFloat64Quaternion> getPointFunc, Func<double, LinFloat64Quaternion> getTangentFunc)
    {
        return new ComputedParametricQuaternion(
            parameterRange,
            getPointFunc,
            getTangentFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricQuaternion Create(Func<double, LinFloat64Quaternion> getPointFunc, Func<double, LinFloat64Quaternion> getTangentFunc)
    {
        return new ComputedParametricQuaternion(
            Float64ScalarRange.Infinite,
            getPointFunc,
            getTangentFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricQuaternion Create(DifferentialFunction xFunc, DifferentialFunction yFunc, DifferentialFunction zFunc, DifferentialFunction wFunc)
    {
        var xDtFunc = xFunc.GetDerivative1();
        var yDtFunc = yFunc.GetDerivative1();
        var zDtFunc = zFunc.GetDerivative1();
        var wDtFunc = wFunc.GetDerivative1();

        return new ComputedParametricQuaternion(
            Float64ScalarRange.Infinite,
            t => LinFloat64Quaternion.Create(wFunc.GetValue(t), xFunc.GetValue(t), yFunc.GetValue(t), zFunc.GetValue(t)),
            t => LinFloat64Quaternion.Create(wDtFunc.GetValue(t), xDtFunc.GetValue(t), yDtFunc.GetValue(t), zDtFunc.GetValue(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricQuaternion Create(Func<double, double> xFunc, Func<double, double> yFunc, Func<double, double> zFunc, Func<double, double> wFunc)
    {
        return new ComputedParametricQuaternion(
            Float64ScalarRange.Infinite,
            t => LinFloat64Quaternion.Create(wFunc(t), xFunc(t), yFunc(t), zFunc(t)),
            t => LinFloat64Quaternion.Create(Differentiate.FirstDerivative(wFunc, t), Differentiate.FirstDerivative(xFunc, t), Differentiate.FirstDerivative(yFunc, t), Differentiate.FirstDerivative(zFunc, t))
        );
    }


    public Func<double, LinFloat64Quaternion> GetPointFunc { get; }

    public Func<double, LinFloat64Quaternion>? GetTangentFunc { get; }

    public Float64ScalarRange ParameterRange { get; }
    //=> Float64Range1D.Infinite;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComputedParametricQuaternion(Float64ScalarRange parameterRange, Func<double, LinFloat64Quaternion> getPointFunc)
    {
        ParameterRange = parameterRange;
        GetPointFunc = getPointFunc;
        GetTangentFunc = null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComputedParametricQuaternion(Float64ScalarRange parameterRange, Func<double, LinFloat64Quaternion> getPointFunc, Func<double, LinFloat64Quaternion> getTangentFunc)
    {
        ParameterRange = parameterRange;
        GetPointFunc = getPointFunc;
        GetTangentFunc = getTangentFunc;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Quaternion GetQuaternion(double parameterValue)
    {
        return GetPointFunc(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Quaternion GetDerivative1Quaternion(double parameterValue)
    {
        if (GetTangentFunc is not null)
            return GetTangentFunc(parameterValue);

        const double zeroEpsilon = 1e-7;

        var p1 = GetPointFunc(parameterValue - zeroEpsilon);
        var p2 = GetPointFunc(parameterValue + zeroEpsilon);

        return (p2 - p1) / (2 * zeroEpsilon);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public ParametricCurveLocalFrame4D GetFrame(double parameterValue)
    //{
    //    return ParametricCurveLocalFrame4D.Create(
    //        parameterValue,
    //        GetPoint(parameterValue),
    //        GetDerivative1Point(parameterValue)
    //    );
    //}
}