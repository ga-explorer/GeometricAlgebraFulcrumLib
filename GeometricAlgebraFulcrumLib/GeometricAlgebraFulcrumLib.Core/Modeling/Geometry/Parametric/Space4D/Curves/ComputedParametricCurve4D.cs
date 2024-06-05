using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space4D.Curves;

public class ComputedParametricCurve4D :
    IParametricCurve4D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve4D Create(Func<double, LinFloat64Vector4D> getPointFunc)
    {
        return new ComputedParametricCurve4D(getPointFunc);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve4D Create(Func<double, LinFloat64Vector4D> getPointFunc, Func<double, LinFloat64Vector4D> getTangentFunc)
    {
        return new ComputedParametricCurve4D(getPointFunc, getTangentFunc);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve4D Create(DifferentialFunction xFunc, DifferentialFunction yFunc, DifferentialFunction zFunc, DifferentialFunction wFunc)
    {
        var xDtFunc = xFunc.GetDerivative1();
        var yDtFunc = yFunc.GetDerivative1();
        var zDtFunc = zFunc.GetDerivative1();
        var wDtFunc = wFunc.GetDerivative1();

        return Create(t => LinFloat64Vector4D.Create(xFunc.GetValue(t),
                yFunc.GetValue(t),
                zFunc.GetValue(t),
                wFunc.GetValue(t)),
            t => LinFloat64Vector4D.Create(xDtFunc.GetValue(t),
                yDtFunc.GetValue(t),
                zDtFunc.GetValue(t),
                wDtFunc.GetValue(t)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve4D Create(Func<double, double> xFunc, Func<double, double> yFunc, Func<double, double> zFunc, Func<double, double> wFunc)
    {
        return Create(t => LinFloat64Vector4D.Create(xFunc(t),
                yFunc(t),
                zFunc(t),
                wFunc(t)),
            t => LinFloat64Vector4D.Create(Differentiate.FirstDerivative(xFunc, t),
                Differentiate.FirstDerivative(yFunc, t),
                Differentiate.FirstDerivative(zFunc, t),
                Differentiate.FirstDerivative(wFunc, t)));
    }


    public Func<double, LinFloat64Vector4D> GetPointFunc { get; }

    public Func<double, LinFloat64Vector4D>? GetTangentFunc { get; }
        
    public Float64ScalarRange ParameterRange 
        => Float64ScalarRange.Infinite;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComputedParametricCurve4D(Func<double, LinFloat64Vector4D> getPointFunc)
    {
        GetPointFunc = getPointFunc;
        GetTangentFunc = null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComputedParametricCurve4D(Func<double, LinFloat64Vector4D> getPointFunc, Func<double, LinFloat64Vector4D> getTangentFunc)
    {
        GetPointFunc = getPointFunc;
        GetTangentFunc = getTangentFunc;
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4D GetPoint(double parameterValue)
    {
        return GetPointFunc(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4D GetDerivative1Point(double parameterValue)
    {
        if (GetTangentFunc is not null)
            return GetTangentFunc(parameterValue);

        const double epsilon = 1e-7;

        var p1 = GetPointFunc(parameterValue - epsilon);
        var p2 = GetPointFunc(parameterValue + epsilon);

        return (p2 - p1) / (2 * epsilon);
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