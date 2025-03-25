using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Basic;

public sealed class Float64ScalarComputedSignal :
    Float64ScalarSignal
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarComputedSignal Finite(Float64ScalarRange timeRange, Func<double, double> getValueFunc)
    {
        return new Float64ScalarComputedSignal(
            timeRange, 
            false,
            getValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarComputedSignal Finite(Float64ScalarRange timeRange, Func<double, double> getValueFunc, Func<double, double> getDerivative1ValueFunc)
    {
        return new Float64ScalarComputedSignal(
            timeRange, 
            false, 
            getValueFunc, 
            getDerivative1ValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarComputedSignal Finite(Float64ScalarRange timeRange, Func<double, double> getValueFunc, Func<double, double> getDerivative1ValueFunc, Func<double, double> getDerivative2ValueFunc)
    {
        return new Float64ScalarComputedSignal(
            timeRange, 
            false, 
            getValueFunc, 
            getDerivative1ValueFunc, 
            getDerivative2ValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarComputedSignal Finite(double timeMin, double timeMax, Func<double, double> getValueFunc)
    {
        var timeRange = Float64ScalarRange.Create(timeMin, timeMax);

        return new Float64ScalarComputedSignal(
            timeRange, 
            false, 
            getValueFunc
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarComputedSignal Periodic(Float64ScalarRange timeRange, Func<double, double> getValueFunc)
    {
        return new Float64ScalarComputedSignal(
            timeRange, 
            true,
            getValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarComputedSignal Periodic(Float64ScalarRange timeRange, Func<double, double> getValueFunc, Func<double, double> getDerivative1ValueFunc)
    {
        return new Float64ScalarComputedSignal(
            timeRange, 
            true, 
            getValueFunc, 
            getDerivative1ValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarComputedSignal Periodic(Float64ScalarRange timeRange, Func<double, double> getValueFunc, Func<double, double> getDerivative1ValueFunc, Func<double, double> getDerivative2ValueFunc)
    {
        return new Float64ScalarComputedSignal(
            timeRange, 
            true, 
            getValueFunc, 
            getDerivative1ValueFunc, 
            getDerivative2ValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarComputedSignal Periodic(double timeMin, double timeMax, Func<double, double> getValueFunc)
    {
        var timeRange = Float64ScalarRange.Create(timeMin, timeMax);

        return new Float64ScalarComputedSignal(
            timeRange, 
            true, 
            getValueFunc
        );
    }


    private Func<double, double> GetValueFunc { get; }

    private Func<double, double>? GetDerivative1ValueFunc { get; }

    private Func<double, double>? GetDerivative2ValueFunc { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarComputedSignal(Float64ScalarRange timeRange, bool isPeriodic, Func<double, double> getValueFunc, Func<double, double>? getDerivative1ValueFunc = null, Func<double, double>? getDerivative2ValueFunc = null)
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
    public override Float64ScalarSignal ToFiniteSignal()
    {
        return IsFinite 
            ? this 
            : new Float64ScalarComputedSignal(
                TimeRange, 
                false, 
                GetValueFunc, 
                GetDerivative1ValueFunc, 
                GetDerivative2ValueFunc
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToPeriodicSignal()
    {
        return IsPeriodic 
            ? this 
            : new Float64ScalarComputedSignal(
                TimeRange, 
                true, 
                GetValueFunc, 
                GetDerivative1ValueFunc, 
                GetDerivative2ValueFunc
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        return GetValueFunc(
            this.ClampTime(t)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative1Value(double t)
    {
        return GetDerivative1ValueFunc?.Invoke(this.ClampTime(t)) 
               ?? GetDerivative1ValueNumerical(t);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative2Value(double t)
    {
        return GetDerivative2ValueFunc?.Invoke(this.ClampTime(t)) 
               ?? GetDerivative2ValueNumerical(t);
    }
}