using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Normalized;

public sealed class Float64ScalarHalfSinStepSignal :
    Float64ScalarNormalizedSignal
{
    internal static Float64ScalarHalfSinStepSignal FiniteInstance { get; }
        = new Float64ScalarHalfSinStepSignal(false);

    internal static Float64ScalarHalfSinStepSignal PeriodicInstance { get; }
        = new Float64ScalarHalfSinStepSignal(true);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarHalfSinStepSignal(bool isPeriodic)
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
        const double s = Math.PI / 2;

        return Math.Sin(s * this.ClampTime(t));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative1Value(double t)
    {
        const double s = Math.PI / 2;

        return s * Math.Cos(s * this.ClampTime(t));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative2Value(double t)
    {
        const double s = Math.PI / 2;
        const double s2 = -s * s;

        return s2 * Math.Sin(s * this.ClampTime(t));
    }

}