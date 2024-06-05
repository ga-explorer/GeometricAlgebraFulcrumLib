using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Angles;

public class ComputedParametricPolarAngle :
    IParametricPolarAngle
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricPolarAngle Create(Func<double, LinFloat64PolarAngle> getPointFunc)
    {
        return new ComputedParametricPolarAngle(Float64ScalarRange.Infinite, getPointFunc);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricPolarAngle Create(Float64ScalarRange parameterRange, Func<double, LinFloat64PolarAngle> getPointFunc)
    {
        return new ComputedParametricPolarAngle(parameterRange, getPointFunc);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricPolarAngle Create(Func<double, LinFloat64PolarAngle> getPointFunc, Func<double, LinFloat64PolarAngle> getTangentFunc)
    {
        return new ComputedParametricPolarAngle(Float64ScalarRange.Infinite, getPointFunc, getTangentFunc);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricPolarAngle Create(Float64ScalarRange parameterRange, Func<double, LinFloat64PolarAngle> getPointFunc, Func<double, LinFloat64PolarAngle> getTangentFunc)
    {
        return new ComputedParametricPolarAngle(parameterRange, getPointFunc, getTangentFunc);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricPolarAngle CreateLinearCycles(double cycleTime, int cycleCount = 1)
    {
        return new ComputedParametricPolarAngle(
            Float64ScalarRange.Create(0, cycleTime * cycleCount),
            t => 
                (2 * Math.PI * t / cycleTime).RadiansToPolarAngle()
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricPolarAngle CreateCosWaveCycles(double cycleTime, int cycleCount = 1)
    {
        var maxTime = cycleTime * cycleCount;

        return new ComputedParametricPolarAngle(
            Float64ScalarRange.Create(0, maxTime),
            t => 
                (t / maxTime).CosWave(0, 2 * Math.PI, cycleCount).RadiansToPolarAngle()
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricPolarAngle CreateCosWaveCycles(double cycleTime, LinFloat64PolarAngle angle1, LinFloat64PolarAngle angle2, int cycleCount = 1)
    {
        var maxTime = cycleTime * cycleCount;

        return new ComputedParametricPolarAngle(
            Float64ScalarRange.Create(0, maxTime),
            t => 
                (t / maxTime).CosWave(
                    angle1.RadiansValue, 
                    angle2.RadiansValue, 
                    cycleCount
                ).RadiansToPolarAngle()
        );
    }


    public Func<double, LinFloat64PolarAngle> GetAngleFunc { get; }

    public Func<double, LinFloat64PolarAngle>? GetTangentFunc { get; }

    public Float64ScalarRange ParameterRange { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComputedParametricPolarAngle(Float64ScalarRange parameterRange, Func<double, LinFloat64PolarAngle> getPointFunc)
    {
        ParameterRange = parameterRange;
        GetAngleFunc = getPointFunc;
        GetTangentFunc = t => 
            Differentiate.FirstDerivative(
                x => getPointFunc(x).RadiansValue, 
                t
            ).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComputedParametricPolarAngle(Float64ScalarRange parameterRange, Func<double, LinFloat64PolarAngle> getPointFunc, Func<double, LinFloat64PolarAngle> getTangentFunc)
    {
        ParameterRange = parameterRange;
        GetAngleFunc = getPointFunc;
        GetTangentFunc = getTangentFunc;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle GetAngle(double parameterValue)
    {
        return GetAngleFunc(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle GetDerivative1Angle(double parameterValue)
    {
        if (GetTangentFunc is not null)
            return GetTangentFunc(parameterValue);

        const double epsilon = 1e-7;

        var p1 = GetAngleFunc(parameterValue - epsilon);
        var p2 = GetAngleFunc(parameterValue + epsilon);

        return p2.AngleSubtract(p1.ScalarValue).AngleDivide(2 * epsilon).ToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IFloat64ParametricScalar ToRadianParametricScalar()
    {
        return ComputedParametricScalar.Create(
            ParameterRange,
            t => GetAngle(t).RadiansValue,
            t => GetDerivative1Angle(t).RadiansValue
        );
    }
}