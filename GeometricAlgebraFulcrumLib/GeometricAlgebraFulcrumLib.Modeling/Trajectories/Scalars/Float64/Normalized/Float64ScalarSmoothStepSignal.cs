using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Normalized;

public sealed class Float64ScalarSmoothStepSignal :
    Float64ScalarNormalizedSignal
{
    internal static Float64ScalarSmoothStepSignal FiniteInstance { get; }
        = new Float64ScalarSmoothStepSignal(false);

    internal static Float64ScalarSmoothStepSignal PeriodicInstance { get; }
        = new Float64ScalarSmoothStepSignal(true);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarSmoothStepSignal(bool isPeriodic)
        : base(isPeriodic)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToFiniteSignal()
    {
        return FiniteInstance;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToPeriodicSignal()
    {
        return PeriodicInstance;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        t = this.ClampTime(t);

        return t switch
        {
            <= -1 => -1,
            >= 1 => 1,
            _ => 2 / (1 + Math.Exp(4 * t / (t * t - 1))) - 1
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative1Value(double t)
    {
        t = this.ClampTime(t);

        if (t is <= -1 or >= 1)
            return 0;

        var a = t * t + 1;
        var b = 1 / (t * t - 1);
        var c = 1 / Math.Cosh(2 * t * b);

        return (2 * a * b * b * c * c).NaNToZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative2Value(double t)
    {
        t = this.ClampTime(t);

        if (t is <= -1 or >= 1)
            return 0;

        var a = t * t + 1;
        var b = 1 / (t * t - 1);
        var c = 2 * t * b;
        
        var t1 = 3 * t - 2 * t.Power(3) - t.Power(5) + 2 * a * a * Math.Tanh(c);

        return (4 * t1 * b.Power(4) / Math.Cosh(c).Square()).NaNToZero();
    }
}