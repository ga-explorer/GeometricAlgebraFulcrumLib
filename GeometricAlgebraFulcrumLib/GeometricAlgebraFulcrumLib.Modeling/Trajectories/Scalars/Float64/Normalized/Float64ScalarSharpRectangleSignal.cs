using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Normalized;

public sealed class Float64ScalarSharpRectangleSignal :
    Float64ScalarNormalizedSignal
{
    internal static Float64ScalarSharpRectangleSignal FiniteInstance { get; }
        = new Float64ScalarSharpRectangleSignal(false);

    internal static Float64ScalarSharpRectangleSignal PeriodicInstance { get; }
        = new Float64ScalarSharpRectangleSignal(true);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarSharpRectangleSignal(bool isPeriodic)
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
    public override Float64ScalarRange FindValueRange()
    {
        return Float64ScalarRange.SymmetricOne;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        return this.ClampTime(t) switch
        {
            < -0.5 or > 0.5 => -1,
            > -0.5 and < 0.5 => 1,
            _ => 0
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative1Value(double t)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative2Value(double t)
    {
        return 0;
    }
}