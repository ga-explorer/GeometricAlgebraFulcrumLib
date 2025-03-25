using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Basic;

public sealed class Float64ScalarConstantZeroSignal :
    Float64ScalarSignal
{
    internal static Float64ScalarConstantZeroSignal FiniteInstance { get; } 
        = new Float64ScalarConstantZeroSignal(false);

    internal static Float64ScalarConstantZeroSignal PeriodicInstance { get; } 
        = new Float64ScalarConstantZeroSignal(true);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarConstantZeroSignal(bool isPeriodic)
        : base(Float64ScalarRange.SymmetricOne, isPeriodic)
    {
        Debug.Assert(IsValid());
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
        return Float64ScalarRange.Create(0, 0);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarRange FindValueRange(double minTime, double maxTime)
    {
        return Float64ScalarRange.Create(0, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        return 0;
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