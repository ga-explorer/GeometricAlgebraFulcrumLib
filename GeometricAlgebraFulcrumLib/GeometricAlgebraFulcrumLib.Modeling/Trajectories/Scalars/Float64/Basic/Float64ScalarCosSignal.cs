using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Basic;


public sealed class Float64ScalarCosSignal :
    Float64ScalarSignal
{
    internal static Float64ScalarCosSignal FiniteInstance { get; }
        = new Float64ScalarCosSignal(false);

    internal static Float64ScalarCosSignal PeriodicInstance { get; }
        = new Float64ScalarCosSignal(true);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarCosSignal(bool isPeriodic)
        : base(Float64ScalarRange.SymmetricPi, isPeriodic)
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
    public override Float64ScalarRange FindValueRange()
    {
        return Float64ScalarRange.Create(-1, 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        return Math.Cos(this.ClampTime(t));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative1Value(double t)
    {
        return -Math.Sin(this.ClampTime(t));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative2Value(double t)
    {
        return -Math.Cos(this.ClampTime(t));
    }
}