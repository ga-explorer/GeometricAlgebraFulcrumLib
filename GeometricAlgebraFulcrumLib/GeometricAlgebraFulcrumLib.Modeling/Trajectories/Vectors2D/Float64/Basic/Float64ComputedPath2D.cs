using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Basic;

public sealed class Float64ComputedPath2D :
    Float64Path2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D Finite(Func<double, LinFloat64Vector2D> getPointFunc)
    {
        return new Float64ComputedPath2D(Float64ScalarRange.SymmetricOne, false, getPointFunc);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D Finite(Float64ScalarRange timeRange, DifferentialFunction xFunc, DifferentialFunction yFunc)
    {
        var xDtFunc = xFunc.GetDerivative1();
        var yDtFunc = yFunc.GetDerivative1();

        return new Float64ComputedPath2D(
            timeRange,
            false,
            t =>
                LinFloat64Vector2D.Create(
                    xFunc.GetValue(t),
                    yFunc.GetValue(t)
                ),
            t =>
                LinFloat64Vector2D.Create(
                    xDtFunc.GetValue(t),
                    yDtFunc.GetValue(t)
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D Finite(Float64ScalarRange timeRange, Func<double, double> xFunc, Func<double, double> yFunc)
    {
        return new Float64ComputedPath2D(
            timeRange,
            false,
            t =>
                LinFloat64Vector2D.Create(
                    xFunc(t),
                    yFunc(t)
                ),
            t =>
                LinFloat64Vector2D.Create(
                    Differentiate.FirstDerivative(xFunc, t),
                    Differentiate.FirstDerivative(yFunc, t)
                )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D Finite(Float64ScalarRange timeRange, Func<double, LinFloat64Vector2D> getValueFunc)
    {
        return new Float64ComputedPath2D(
            timeRange, 
            false,
            getValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D Finite(Float64ScalarRange timeRange, Func<double, LinFloat64Vector2D> getValueFunc, Func<double, LinFloat64Vector2D> getDerivative1ValueFunc)
    {
        return new Float64ComputedPath2D(
            timeRange, 
            false, 
            getValueFunc, 
            getDerivative1ValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D Finite(Float64ScalarRange timeRange, Func<double, LinFloat64Vector2D> getValueFunc, Func<double, LinFloat64Vector2D> getDerivative1ValueFunc, Func<double, LinFloat64Vector2D> getDerivative2ValueFunc)
    {
        return new Float64ComputedPath2D(
            timeRange, 
            false, 
            getValueFunc, 
            getDerivative1ValueFunc, 
            getDerivative2ValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D Finite(double timeMin, double timeMax, Func<double, LinFloat64Vector2D> getValueFunc)
    {
        var timeRange = Float64ScalarRange.Create(timeMin, timeMax);

        return new Float64ComputedPath2D(
            timeRange, 
            false, 
            getValueFunc
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D Periodic(Float64ScalarRange timeRange, Func<double, LinFloat64Vector2D> getValueFunc)
    {
        return new Float64ComputedPath2D(
            timeRange, 
            true,
            getValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D Periodic(Float64ScalarRange timeRange, Func<double, LinFloat64Vector2D> getValueFunc, Func<double, LinFloat64Vector2D> getDerivative1ValueFunc)
    {
        return new Float64ComputedPath2D(
            timeRange, 
            true, 
            getValueFunc, 
            getDerivative1ValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D Periodic(Float64ScalarRange timeRange, Func<double, LinFloat64Vector2D> getValueFunc, Func<double, LinFloat64Vector2D> getDerivative1ValueFunc, Func<double, LinFloat64Vector2D> getDerivative2ValueFunc)
    {
        return new Float64ComputedPath2D(
            timeRange, 
            true, 
            getValueFunc, 
            getDerivative1ValueFunc, 
            getDerivative2ValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D Periodic(double timeMin, double timeMax, Func<double, LinFloat64Vector2D> getValueFunc)
    {
        var timeRange = Float64ScalarRange.Create(timeMin, timeMax);

        return new Float64ComputedPath2D(
            timeRange, 
            true, 
            getValueFunc
        );
    }


    private Func<double, LinFloat64Vector2D> GetValueFunc { get; }

    private Func<double, LinFloat64Vector2D>? GetDerivative1ValueFunc { get; }

    private Func<double, LinFloat64Vector2D>? GetDerivative2ValueFunc { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ComputedPath2D(Float64ScalarRange timeRange, bool isPeriodic, Func<double, LinFloat64Vector2D> getValueFunc, Func<double, LinFloat64Vector2D>? getDerivative1ValueFunc = null, Func<double, LinFloat64Vector2D>? getDerivative2ValueFunc = null)
        : base(timeRange, isPeriodic)
    {
        GetValueFunc = getValueFunc;
        GetDerivative1ValueFunc = getDerivative1ValueFunc;
        GetDerivative2ValueFunc = getDerivative2ValueFunc;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path2D ToFinitePath()
    {
        return IsFinite 
            ? this 
            : new Float64ComputedPath2D(
                TimeRange, 
                false, 
                GetValueFunc, 
                GetDerivative1ValueFunc, 
                GetDerivative2ValueFunc
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path2D ToPeriodicPath()
    {
        return IsPeriodic 
            ? this 
            : new Float64ComputedPath2D(
                TimeRange, 
                true, 
                GetValueFunc, 
                GetDerivative1ValueFunc, 
                GetDerivative2ValueFunc
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetValue(double t)
    {
        return GetValueFunc(
            this.ClampTime(t)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative1Value(double t)
    {
        return GetDerivative1ValueFunc?.Invoke(this.ClampTime(t)) 
               ?? GetDerivative1ValueNumerical(t);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative2Value(double t)
    {
        return GetDerivative2ValueFunc?.Invoke(this.ClampTime(t)) 
               ?? GetDerivative2ValueNumerical(t);
    }
    
}