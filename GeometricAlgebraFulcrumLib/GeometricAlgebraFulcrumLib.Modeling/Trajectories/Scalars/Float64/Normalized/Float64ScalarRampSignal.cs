using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Normalized;

public sealed class Float64ScalarRampSignal :
    Float64ScalarNormalizedSignal
{
    internal static Float64ScalarRampSignal FiniteInstance { get; }
        = new Float64ScalarRampSignal(false);

    internal static Float64ScalarRampSignal PeriodicInstance { get; }
        = new Float64ScalarRampSignal(true);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarRampSignal(bool isPeriodic)
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
        return this.ClampTime(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative1Value(double t)
    {
        return IsPeriodic || TimeRange.Contains(t) ? 1 : 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative2Value(double t)
    {
        return 0;
    }

}