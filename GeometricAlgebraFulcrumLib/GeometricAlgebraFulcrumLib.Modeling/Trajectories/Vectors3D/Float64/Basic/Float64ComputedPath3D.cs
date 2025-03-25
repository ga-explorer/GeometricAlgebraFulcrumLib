using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;

public sealed class Float64ComputedPath3D :
    Float64Path3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D Finite(Func<double, LinFloat64Vector3D> getPointFunc)
    {
        return new Float64ComputedPath3D(Float64ScalarRange.SymmetricOne, false, getPointFunc);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D Finite(Float64ScalarRange timeRange, DifferentialFunction xFunc, DifferentialFunction yFunc, DifferentialFunction zFunc)
    {
        var xDtFunc = xFunc.GetDerivative1();
        var yDtFunc = yFunc.GetDerivative1();
        var zDtFunc = zFunc.GetDerivative1();

        return new Float64ComputedPath3D(
            timeRange,
            false,
            t =>
                LinFloat64Vector3D.Create(
                    xFunc.GetValue(t),
                    yFunc.GetValue(t),
                    zFunc.GetValue(t)
                ),
            t =>
                LinFloat64Vector3D.Create(
                    xDtFunc.GetValue(t),
                    yDtFunc.GetValue(t),
                    zDtFunc.GetValue(t)
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D Finite(Float64ScalarRange timeRange, Func<double, double> xFunc, Func<double, double> yFunc, Func<double, double> zFunc)
    {
        return new Float64ComputedPath3D(
            timeRange,
            false,
            t =>
                LinFloat64Vector3D.Create(
                    xFunc(t),
                    yFunc(t),
                    zFunc(t)
                ),
            t =>
                LinFloat64Vector3D.Create(
                    Differentiate.FirstDerivative(xFunc, t),
                    Differentiate.FirstDerivative(yFunc, t),
                    Differentiate.FirstDerivative(zFunc, t)
                )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D Finite(Float64ScalarRange timeRange, Func<double, LinFloat64Vector3D> getValueFunc)
    {
        return new Float64ComputedPath3D(
            timeRange, 
            false,
            getValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D Finite(Float64ScalarRange timeRange, Func<double, LinFloat64Vector3D> getValueFunc, Func<double, LinFloat64Vector3D> getDerivative1ValueFunc)
    {
        return new Float64ComputedPath3D(
            timeRange, 
            false, 
            getValueFunc, 
            getDerivative1ValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D Finite(Float64ScalarRange timeRange, Func<double, LinFloat64Vector3D> getValueFunc, Func<double, LinFloat64Vector3D> getDerivative1ValueFunc, Func<double, LinFloat64Vector3D> getDerivative2ValueFunc)
    {
        return new Float64ComputedPath3D(
            timeRange, 
            false, 
            getValueFunc, 
            getDerivative1ValueFunc, 
            getDerivative2ValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D Finite(double timeMin, double timeMax, Func<double, LinFloat64Vector3D> getValueFunc)
    {
        var timeRange = Float64ScalarRange.Create(timeMin, timeMax);

        return new Float64ComputedPath3D(
            timeRange, 
            false, 
            getValueFunc
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D Periodic(Func<double, LinFloat64Vector3D> getPointFunc)
    {
        return new Float64ComputedPath3D(Float64ScalarRange.SymmetricOne, true, getPointFunc);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D Periodic(Float64ScalarRange timeRange, DifferentialFunction xFunc, DifferentialFunction yFunc, DifferentialFunction zFunc)
    {
        var xDtFunc = xFunc.GetDerivative1();
        var yDtFunc = yFunc.GetDerivative1();
        var zDtFunc = zFunc.GetDerivative1();

        return new Float64ComputedPath3D(
            timeRange,
            true,
            t =>
                LinFloat64Vector3D.Create(
                    xFunc.GetValue(t),
                    yFunc.GetValue(t),
                    zFunc.GetValue(t)
                ),
            t =>
                LinFloat64Vector3D.Create(
                    xDtFunc.GetValue(t),
                    yDtFunc.GetValue(t),
                    zDtFunc.GetValue(t)
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D Periodic(Float64ScalarRange timeRange, Func<double, double> xFunc, Func<double, double> yFunc, Func<double, double> zFunc)
    {
        return new Float64ComputedPath3D(
            timeRange,
            true,
            t =>
                LinFloat64Vector3D.Create(
                    xFunc(t),
                    yFunc(t),
                    zFunc(t)
                ),
            t =>
                LinFloat64Vector3D.Create(
                    Differentiate.FirstDerivative(xFunc, t),
                    Differentiate.FirstDerivative(yFunc, t),
                    Differentiate.FirstDerivative(zFunc, t)
                )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D Periodic(Float64ScalarRange timeRange, Func<double, LinFloat64Vector3D> getValueFunc)
    {
        return new Float64ComputedPath3D(
            timeRange, 
            true,
            getValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D Periodic(Float64ScalarRange timeRange, Func<double, LinFloat64Vector3D> getValueFunc, Func<double, LinFloat64Vector3D> getDerivative1ValueFunc)
    {
        return new Float64ComputedPath3D(
            timeRange, 
            true, 
            getValueFunc, 
            getDerivative1ValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D Periodic(Float64ScalarRange timeRange, Func<double, LinFloat64Vector3D> getValueFunc, Func<double, LinFloat64Vector3D> getDerivative1ValueFunc, Func<double, LinFloat64Vector3D> getDerivative2ValueFunc)
    {
        return new Float64ComputedPath3D(
            timeRange, 
            true, 
            getValueFunc, 
            getDerivative1ValueFunc, 
            getDerivative2ValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D Periodic(double timeMin, double timeMax, Func<double, LinFloat64Vector3D> getValueFunc)
    {
        var timeRange = Float64ScalarRange.Create(timeMin, timeMax);

        return new Float64ComputedPath3D(
            timeRange, 
            true, 
            getValueFunc
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D Create(bool isPeriodic, Func<double, LinFloat64Vector3D> getPointFunc)
    {
        return new Float64ComputedPath3D(Float64ScalarRange.SymmetricOne, isPeriodic, getPointFunc);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D Create(bool isPeriodic, Float64ScalarRange timeRange, DifferentialFunction xFunc, DifferentialFunction yFunc, DifferentialFunction zFunc)
    {
        var xDtFunc = xFunc.GetDerivative1();
        var yDtFunc = yFunc.GetDerivative1();
        var zDtFunc = zFunc.GetDerivative1();

        return new Float64ComputedPath3D(
            timeRange,
            isPeriodic,
            t =>
                LinFloat64Vector3D.Create(
                    xFunc.GetValue(t),
                    yFunc.GetValue(t),
                    zFunc.GetValue(t)
                ),
            t =>
                LinFloat64Vector3D.Create(
                    xDtFunc.GetValue(t),
                    yDtFunc.GetValue(t),
                    zDtFunc.GetValue(t)
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D Create(Float64ScalarRange timeRange, bool isPeriodic, Func<double, double> xFunc, Func<double, double> yFunc, Func<double, double> zFunc)
    {
        return new Float64ComputedPath3D(
            timeRange,
            isPeriodic,
            t =>
                LinFloat64Vector3D.Create(
                    xFunc(t),
                    yFunc(t),
                    zFunc(t)
                ),
            t =>
                LinFloat64Vector3D.Create(
                    Differentiate.FirstDerivative(xFunc, t),
                    Differentiate.FirstDerivative(yFunc, t),
                    Differentiate.FirstDerivative(zFunc, t)
                )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D Create(Float64ScalarRange timeRange, bool isPeriodic, Func<double, LinFloat64Vector3D> getValueFunc)
    {
        return new Float64ComputedPath3D(
            timeRange, 
            isPeriodic,
            getValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D Create(Float64ScalarRange timeRange, bool isPeriodic, Func<double, LinFloat64Vector3D> getValueFunc, Func<double, LinFloat64Vector3D> getDerivative1ValueFunc)
    {
        return new Float64ComputedPath3D(
            timeRange, 
            isPeriodic, 
            getValueFunc, 
            getDerivative1ValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D Create(Float64ScalarRange timeRange, bool isPeriodic, Func<double, LinFloat64Vector3D> getValueFunc, Func<double, LinFloat64Vector3D> getDerivative1ValueFunc, Func<double, LinFloat64Vector3D> getDerivative2ValueFunc)
    {
        return new Float64ComputedPath3D(
            timeRange, 
            isPeriodic, 
            getValueFunc, 
            getDerivative1ValueFunc, 
            getDerivative2ValueFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D Create(double timeMin, double timeMax, bool isPeriodic, Func<double, LinFloat64Vector3D> getValueFunc)
    {
        var timeRange = Float64ScalarRange.Create(timeMin, timeMax);

        return new Float64ComputedPath3D(
            timeRange, 
            isPeriodic, 
            getValueFunc
        );
    }


    private Func<double, LinFloat64Vector3D> GetValueFunc { get; }

    private Func<double, LinFloat64Vector3D>? GetDerivative1ValueFunc { get; }

    private Func<double, LinFloat64Vector3D>? GetDerivative2ValueFunc { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ComputedPath3D(Float64ScalarRange timeRange, bool isPeriodic, Func<double, LinFloat64Vector3D> getValueFunc, Func<double, LinFloat64Vector3D>? getDerivative1ValueFunc = null, Func<double, LinFloat64Vector3D>? getDerivative2ValueFunc = null)
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
    public override Float64Path3D ToFinitePath()
    {
        return IsFinite 
            ? this 
            : new Float64ComputedPath3D(
                TimeRange, 
                false, 
                GetValueFunc, 
                GetDerivative1ValueFunc, 
                GetDerivative2ValueFunc
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3D ToPeriodicPath()
    {
        return IsPeriodic 
            ? this 
            : new Float64ComputedPath3D(
                TimeRange, 
                true, 
                GetValueFunc, 
                GetDerivative1ValueFunc, 
                GetDerivative2ValueFunc
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetValue(double t)
    {
        return GetValueFunc(
            this.ClampTime(t)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative1Value(double t)
    {
        return GetDerivative1ValueFunc?.Invoke(this.ClampTime(t)) 
               ?? GetDerivative1ValueNumerical(t);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative2Value(double t)
    {
        return GetDerivative2ValueFunc?.Invoke(this.ClampTime(t)) 
               ?? GetDerivative2ValueNumerical(t);
    }
    
}