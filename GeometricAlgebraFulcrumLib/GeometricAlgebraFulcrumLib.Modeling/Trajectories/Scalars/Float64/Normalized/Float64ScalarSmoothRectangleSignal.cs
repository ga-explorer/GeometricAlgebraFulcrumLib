using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Normalized;

public sealed class Float64ScalarSmoothRectangleSignal :
    Float64ScalarNormalizedSignal
{
    internal static Float64ScalarSmoothRectangleSignal FiniteInstance { get; }
        = new Float64ScalarSmoothRectangleSignal(false);

    internal static Float64ScalarSmoothRectangleSignal PeriodicInstance { get; }
        = new Float64ScalarSmoothRectangleSignal(true);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarSmoothRectangleSignal(bool isPeriodic)
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

    public override double GetValue(double t)
    {
        const double epsilon = 1e-9;

        t = this.ClampTime(t);

        if (t is <= -1 + epsilon or >= 1 - epsilon) return -1;
        if (t.IsNearZero(epsilon)) return 1;

        if (t < 0) t = -t;

        return (1 - 2 / (1 + Math.Exp(1 / t - 1 / (1 - t)))).NaNToZero();
    }
    
    public override double GetDerivative1Value(double t)
    {
        const double epsilon = 1e-9;

        t = this.ClampTime(t);

        if (t is <= -1 + epsilon or >= 1 - epsilon) return 0;
        if (t.IsNearZero(epsilon)) return 0;

        var s = -2;
        if (t < 0)
        {
            t = -t;
            s = 2;
        }

        var e1 = Math.Exp(1 / t);
        var e2 = Math.Exp(1 / (1 - t));
        
        var value = 
            s * e1 * e2 * (1 - 2 * t + 2 * t * t) / 
            (t * (1 - t) * (e1 + e2)).Square();
        
        return value.NaNToZero();
    }
    
    public override double GetDerivative2Value(double t)
    {
        const double epsilon = 1e-9;

        t = this.ClampTime(t);

        if (t is <= -1 + epsilon or >= 1 - epsilon) return 0;
        if (t.IsNearZero(epsilon)) return 0;

        if (t < 0) t = -t;

        var e1 = Math.Exp(1 / t);
        var e2 = Math.Exp(1 / (1 - t));
        
        var value = 
            (2 * e1 * e2 * (e2 * (1 - 2 * t + 4 * t.Power(3) - 6 * t.Power(4) + 4 * t.Power(5)) + e1 * (-1 + 2 * (-1 + t) * t * (-3 + 2 * t) * (1 + (-1 + t) * t)))) /
            ((e1 + e2).Power(3) * (-1 + t).Power(4) * t.Power(4));
        
        return value.NaNToZero();
    }
}