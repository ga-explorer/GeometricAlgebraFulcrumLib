using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Mapped;

public sealed class Float64ScalarDerivativeSignal :
    Float64ScalarSignal
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarDerivativeSignal Create(Float64ScalarSignal baseSignal)
    {
        return new Float64ScalarDerivativeSignal(baseSignal);
    }


    public Float64ScalarSignal BaseSignal { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarDerivativeSignal(Float64ScalarSignal baseSignal)
        : base(baseSignal.TimeRange, baseSignal.IsPeriodic)
    {
        BaseSignal = baseSignal;

        Debug.Assert(IsValid());
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return BaseSignal.IsValid();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToFiniteSignal()
    {
        return IsFinite 
            ? this 
            : new Float64ScalarDerivativeSignal(
                BaseSignal.ToFiniteSignal()
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToPeriodicSignal()
    {
        return IsPeriodic 
            ? this 
            : new Float64ScalarDerivativeSignal(
                BaseSignal.ToPeriodicSignal()
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        var value = BaseSignal.GetDerivative1Value(t);

        Debug.Assert(value.IsFinite());

        return value;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative1Value(double t)
    {
        var value = BaseSignal.GetDerivative2Value(t);

        Debug.Assert(value.IsFinite());

        return value;
    }
    
    public override double GetDerivative2Value(double t)
    {
        if (IsFinite && !TimeRange.Contains(t)) return 0d;

        t = this.ClampTime(t);
            
        if (t < MinTime + 1e-9)
            return (BaseSignal.GetDerivative2Value(t + 1e-12) - BaseSignal.GetDerivative2Value(t)) / 1e-12;

        if (t > MaxTime - 1e-9)
            return (BaseSignal.GetDerivative2Value(t) - BaseSignal.GetDerivative2Value(t - 1e-12)) / 1e-12;
            
        return MathNet.Numerics.Differentiate.Derivative(
            BaseSignal.GetDerivative2Value, 
            t, 
            1
        );
    }

}