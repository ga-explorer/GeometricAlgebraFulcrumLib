using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Angles;

public class ComputedParametricAngle :
    IParametricAngle
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricAngle Create(Func<double, Float64PlanarAngle> getPointFunc)
    {
        return new ComputedParametricAngle(Float64ScalarRange.Infinite, getPointFunc);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricAngle Create(Float64ScalarRange parameterRange, Func<double, Float64PlanarAngle> getPointFunc)
    {
        return new ComputedParametricAngle(parameterRange, getPointFunc);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricAngle Create(Func<double, Float64PlanarAngle> getPointFunc, Func<double, Float64PlanarAngle> getTangentFunc)
    {
        return new ComputedParametricAngle(Float64ScalarRange.Infinite, getPointFunc, getTangentFunc);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricAngle Create(Float64ScalarRange parameterRange, Func<double, Float64PlanarAngle> getPointFunc, Func<double, Float64PlanarAngle> getTangentFunc)
    {
        return new ComputedParametricAngle(parameterRange, getPointFunc, getTangentFunc);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricAngle CreateLinearCycles(double cycleTime, int cycleCount = 1)
    {
        return new ComputedParametricAngle(
            Float64ScalarRange.Create(0, cycleTime * cycleCount),
            t => 
                (2 * Math.PI * t / cycleTime)
                .RadiansToAngle()
                .GetAngleInPositiveRange()
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricAngle CreateCosWaveCycles(double cycleTime, int cycleCount = 1)
    {
        var maxTime = cycleTime * cycleCount;

        return new ComputedParametricAngle(
            Float64ScalarRange.Create(0, maxTime),
            t => 
                (t / maxTime).CosWave(0, 2 * Math.PI, cycleCount)
                .RadiansToAngle()
                .GetAngleInPositiveRange()
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricAngle CreateCosWaveCycles(double cycleTime, Float64PlanarAngle angle1, Float64PlanarAngle angle2, int cycleCount = 1)
    {
        var maxTime = cycleTime * cycleCount;

        return new ComputedParametricAngle(
            Float64ScalarRange.Create(0, maxTime),
            t => 
                (t / maxTime).CosWave(
                    angle1.GetAngleInPositiveRange(), 
                    angle2.GetAngleInPositiveRange(), 
                    cycleCount
                )
                .RadiansToAngle()
                .GetAngleInPositiveRange()
        );
    }


    public Func<double, Float64PlanarAngle> GetAngleFunc { get; }

    public Func<double, Float64PlanarAngle>? GetTangentFunc { get; }

    public Float64ScalarRange ParameterRange { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComputedParametricAngle(Float64ScalarRange parameterRange, Func<double, Float64PlanarAngle> getPointFunc)
    {
        ParameterRange = parameterRange;
        GetAngleFunc = getPointFunc;
        GetTangentFunc = t => 
            Differentiate.FirstDerivative(
                x => getPointFunc(x).Radians, 
                t
            ).RadiansToAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComputedParametricAngle(Float64ScalarRange parameterRange, Func<double, Float64PlanarAngle> getPointFunc, Func<double, Float64PlanarAngle> getTangentFunc)
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
    public Float64PlanarAngle GetAngle(double parameterValue)
    {
        return GetAngleFunc(parameterValue).GetAngleInPositiveRange();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64PlanarAngle GetDerivative1Angle(double parameterValue)
    {
        if (GetTangentFunc is not null)
            return GetTangentFunc(parameterValue).GetAngleInPositiveRange();

        const double epsilon = 1e-7;

        var p1 = GetAngleFunc(parameterValue - epsilon);
        var p2 = GetAngleFunc(parameterValue + epsilon);

        return ((p2 - p1) / (2 * epsilon)).GetAngleInPositiveRange();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IParametricScalar ToRadianParametricScalar()
    {
        return ComputedParametricScalar.Create(
            ParameterRange,
            t => GetAngle(t).Radians.Value,
            t => GetDerivative1Angle(t).Radians.Value
        );
    }
}