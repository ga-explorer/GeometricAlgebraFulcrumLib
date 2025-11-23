using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors4D.Float64.Basic;

public sealed class Float64ComputedPath4D :
    Float64Path4D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath4D Finite(Func<double, LinFloat64Vector4D> getPointFunc)
    {
        return new Float64ComputedPath4D(Float64ScalarRange.SymmetricOne, false, getPointFunc);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath4D Finite(Float64ScalarRange timeRange, DifferentialFunction xFunc, DifferentialFunction yFunc, DifferentialFunction zFunc, DifferentialFunction wFunc)
    {
        var xDtFunc = xFunc.GetDerivative1();
        var yDtFunc = yFunc.GetDerivative1();
        var zDtFunc = zFunc.GetDerivative1();
        var wDtFunc = wFunc.GetDerivative1();

        return new Float64ComputedPath4D(
            timeRange,
            false,
            t =>
                LinFloat64Vector4D.Create(
                    xFunc.GetValue(t),
                    yFunc.GetValue(t),
                    zFunc.GetValue(t),
                    wFunc.GetValue(t)
                ),
            t =>
                LinFloat64Vector4D.Create(
                    xDtFunc.GetValue(t),
                    yDtFunc.GetValue(t),
                    zDtFunc.GetValue(t),
                    wDtFunc.GetValue(t)
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath4D Finite(Float64ScalarRange timeRange, Func<double, double> xFunc, Func<double, double> yFunc, Func<double, double> zFunc, Func<double, double> wFunc)
    {
        return new Float64ComputedPath4D(
            timeRange,
            false,
            t =>
                LinFloat64Vector4D.Create(
                    xFunc(t),
                    yFunc(t),
                    zFunc(t),
                    wFunc(t)
                ),
            t =>
                LinFloat64Vector4D.Create(
                    Differentiate.FirstDerivative(xFunc, t),
                    Differentiate.FirstDerivative(yFunc, t),
                    Differentiate.FirstDerivative(zFunc, t),
                    Differentiate.FirstDerivative(wFunc, t)
                )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath4D Finite(Float64ScalarRange timeRange, Func<double, LinFloat64Vector4D> getValueFunc)
    {
        return new Float64ComputedPath4D(
            timeRange, 
            false,
            getValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath4D Finite(Float64ScalarRange timeRange, Func<double, LinFloat64Vector4D> getValueFunc, Func<double, LinFloat64Vector4D> getDerivative1ValueFunc)
    {
        return new Float64ComputedPath4D(
            timeRange, 
            false, 
            getValueFunc, 
            getDerivative1ValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath4D Finite(Float64ScalarRange timeRange, Func<double, LinFloat64Vector4D> getValueFunc, Func<double, LinFloat64Vector4D> getDerivative1ValueFunc, Func<double, LinFloat64Vector4D> getDerivative2ValueFunc)
    {
        return new Float64ComputedPath4D(
            timeRange, 
            false, 
            getValueFunc, 
            getDerivative1ValueFunc, 
            getDerivative2ValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath4D Finite(double timeMin, double timeMax, Func<double, LinFloat64Vector4D> getValueFunc)
    {
        var timeRange = Float64ScalarRange.Create(timeMin, timeMax);

        return new Float64ComputedPath4D(
            timeRange, 
            false, 
            getValueFunc
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath4D Periodic(Func<double, LinFloat64Vector4D> getPointFunc)
    {
        return new Float64ComputedPath4D(Float64ScalarRange.SymmetricOne, true, getPointFunc);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath4D Periodic(Float64ScalarRange timeRange, DifferentialFunction xFunc, DifferentialFunction yFunc, DifferentialFunction zFunc, DifferentialFunction wFunc)
    {
        var xDtFunc = xFunc.GetDerivative1();
        var yDtFunc = yFunc.GetDerivative1();
        var zDtFunc = zFunc.GetDerivative1();
        var wDtFunc = wFunc.GetDerivative1();

        return new Float64ComputedPath4D(
            timeRange,
            true,
            t =>
                LinFloat64Vector4D.Create(
                    xFunc.GetValue(t),
                    yFunc.GetValue(t),
                    zFunc.GetValue(t),
                    wFunc.GetValue(t)
                ),
            t =>
                LinFloat64Vector4D.Create(
                    xDtFunc.GetValue(t),
                    yDtFunc.GetValue(t),
                    zDtFunc.GetValue(t),
                    wDtFunc.GetValue(t)
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath4D Periodic(Float64ScalarRange timeRange, Func<double, double> xFunc, Func<double, double> yFunc, Func<double, double> zFunc, Func<double, double> wFunc)
    {
        return new Float64ComputedPath4D(
            timeRange,
            true,
            t =>
                LinFloat64Vector4D.Create(
                    xFunc(t),
                    yFunc(t),
                    zFunc(t),
                    wFunc(t)
                ),
            t =>
                LinFloat64Vector4D.Create(
                    Differentiate.FirstDerivative(xFunc, t),
                    Differentiate.FirstDerivative(yFunc, t),
                    Differentiate.FirstDerivative(zFunc, t),
                    Differentiate.FirstDerivative(wFunc, t)
                )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath4D Periodic(Float64ScalarRange timeRange, Func<double, LinFloat64Vector4D> getValueFunc)
    {
        return new Float64ComputedPath4D(
            timeRange, 
            true,
            getValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath4D Periodic(Float64ScalarRange timeRange, Func<double, LinFloat64Vector4D> getValueFunc, Func<double, LinFloat64Vector4D> getDerivative1ValueFunc)
    {
        return new Float64ComputedPath4D(
            timeRange, 
            true, 
            getValueFunc, 
            getDerivative1ValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath4D Periodic(Float64ScalarRange timeRange, Func<double, LinFloat64Vector4D> getValueFunc, Func<double, LinFloat64Vector4D> getDerivative1ValueFunc, Func<double, LinFloat64Vector4D> getDerivative2ValueFunc)
    {
        return new Float64ComputedPath4D(
            timeRange, 
            true, 
            getValueFunc, 
            getDerivative1ValueFunc, 
            getDerivative2ValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath4D Periodic(double timeMin, double timeMax, Func<double, LinFloat64Vector4D> getValueFunc)
    {
        var timeRange = Float64ScalarRange.Create(timeMin, timeMax);

        return new Float64ComputedPath4D(
            timeRange, 
            true, 
            getValueFunc
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath4D Create(bool isPeriodic, Func<double, LinFloat64Vector4D> getPointFunc)
    {
        return new Float64ComputedPath4D(Float64ScalarRange.SymmetricOne, isPeriodic, getPointFunc);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath4D Create(bool isPeriodic, Float64ScalarRange timeRange, DifferentialFunction xFunc, DifferentialFunction yFunc, DifferentialFunction zFunc, DifferentialFunction wFunc)
    {
        var xDtFunc = xFunc.GetDerivative1();
        var yDtFunc = yFunc.GetDerivative1();
        var zDtFunc = zFunc.GetDerivative1();
        var wDtFunc = zFunc.GetDerivative1();

        return new Float64ComputedPath4D(
            timeRange,
            isPeriodic,
            t =>
                LinFloat64Vector4D.Create(
                    xFunc.GetValue(t),
                    yFunc.GetValue(t),
                    zFunc.GetValue(t),
                    wFunc.GetValue(t)
                ),
            t =>
                LinFloat64Vector4D.Create(
                    xDtFunc.GetValue(t),
                    yDtFunc.GetValue(t),
                    zDtFunc.GetValue(t),
                    wDtFunc.GetValue(t)
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath4D Create(Float64ScalarRange timeRange, bool isPeriodic, Func<double, double> xFunc, Func<double, double> yFunc, Func<double, double> zFunc, Func<double, double> wFunc)
    {
        return new Float64ComputedPath4D(
            timeRange,
            isPeriodic,
            t =>
                LinFloat64Vector4D.Create(
                    xFunc(t),
                    yFunc(t),
                    zFunc(t),
                    wFunc(t)
                ),
            t =>
                LinFloat64Vector4D.Create(
                    Differentiate.FirstDerivative(xFunc, t),
                    Differentiate.FirstDerivative(yFunc, t),
                    Differentiate.FirstDerivative(zFunc, t),
                    Differentiate.FirstDerivative(wFunc, t)
                )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath4D Create(Float64ScalarRange timeRange, bool isPeriodic, Func<double, LinFloat64Vector4D> getValueFunc)
    {
        return new Float64ComputedPath4D(
            timeRange, 
            isPeriodic,
            getValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath4D Create(Float64ScalarRange timeRange, bool isPeriodic, Func<double, LinFloat64Vector4D> getValueFunc, Func<double, LinFloat64Vector4D> getDerivative1ValueFunc)
    {
        return new Float64ComputedPath4D(
            timeRange, 
            isPeriodic, 
            getValueFunc, 
            getDerivative1ValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath4D Create(Float64ScalarRange timeRange, bool isPeriodic, Func<double, LinFloat64Vector4D> getValueFunc, Func<double, LinFloat64Vector4D> getDerivative1ValueFunc, Func<double, LinFloat64Vector4D> getDerivative2ValueFunc)
    {
        return new Float64ComputedPath4D(
            timeRange, 
            isPeriodic, 
            getValueFunc, 
            getDerivative1ValueFunc, 
            getDerivative2ValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath4D Create(double timeMin, double timeMax, bool isPeriodic, Func<double, LinFloat64Vector4D> getValueFunc)
    {
        var timeRange = Float64ScalarRange.Create(timeMin, timeMax);

        return new Float64ComputedPath4D(
            timeRange, 
            isPeriodic, 
            getValueFunc
        );
    }


    private Func<double, LinFloat64Vector4D> GetValueFunc { get; }

    private Func<double, LinFloat64Vector4D>? GetDerivative1ValueFunc { get; }

    private Func<double, LinFloat64Vector4D>? GetDerivative2ValueFunc { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ComputedPath4D(Float64ScalarRange timeRange, bool isPeriodic, Func<double, LinFloat64Vector4D> getValueFunc, Func<double, LinFloat64Vector4D>? getDerivative1ValueFunc = null, Func<double, LinFloat64Vector4D>? getDerivative2ValueFunc = null)
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
    public override Float64Path4D ToFinitePath()
    {
        return IsFinite 
            ? this 
            : new Float64ComputedPath4D(
                TimeRange, 
                false, 
                GetValueFunc, 
                GetDerivative1ValueFunc, 
                GetDerivative2ValueFunc
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path4D ToPeriodicPath()
    {
        return IsPeriodic 
            ? this 
            : new Float64ComputedPath4D(
                TimeRange, 
                true, 
                GetValueFunc, 
                GetDerivative1ValueFunc, 
                GetDerivative2ValueFunc
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector4D GetValue(double t)
    {
        return GetValueFunc(
            this.ClampTime(t)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector4D GetDerivative1Value(double t)
    {
        return GetDerivative1ValueFunc?.Invoke(this.ClampTime(t)) 
               ?? GetDerivative1ValueNumerical(t);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector4D GetDerivative2Value(double t)
    {
        return GetDerivative2ValueFunc?.Invoke(this.ClampTime(t)) 
               ?? GetDerivative2ValueNumerical(t);
    }
    
}