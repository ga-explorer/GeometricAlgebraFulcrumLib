using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Mapped;

public sealed class Float64ScalarRepeatedSignal :
    Float64ScalarSignal
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarRepeatedSignal Create(Float64ScalarSignal baseSignal, int count)
    {
        return new Float64ScalarRepeatedSignal(
            baseSignal, 
            count
        );
    }


    public Float64ScalarSignal BaseSignal { get; }

    public int Count { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarRepeatedSignal(Float64ScalarSignal baseSignal, int count) 
        : base(
            Float64ScalarRange.Create(
                baseSignal.MinTime,
                baseSignal.MinTime + baseSignal.TimeRangeLength * count
            ),
            false
        )
    {
        if (count < 1)
            throw new InvalidOperationException();

        BaseSignal = baseSignal;
        Count = count;
        
        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return BaseSignal.IsValid() &&
               Count >= 1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToFiniteSignal()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToPeriodicSignal()
    {
        return BaseSignal.ToPeriodicSignal();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarRange FindValueRange()
    {
        return BaseSignal.GetValueRange();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarRange FindValueRange(double minTime, double maxTime)
    {
        return BaseSignal.FindValueRange(minTime, maxTime);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        t = BaseSignal.ClampTime(
            this.ClampTime(t), 
            true
        );

        return BaseSignal.GetValue(t);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative1Value(double t)
    {
        t = BaseSignal.ClampTime(
            this.ClampTime(t), 
            true
        );

        return BaseSignal.GetDerivative1Value(t);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative2Value(double t)
    {
        t = BaseSignal.ClampTime(
            this.ClampTime(t), 
            true
        );

        return BaseSignal.GetDerivative2Value(t);
    }
}