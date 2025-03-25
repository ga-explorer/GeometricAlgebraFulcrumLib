using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Mapped;

public sealed class Float64ScalarListSignal :
    Float64ScalarSignal,
    IReadOnlyList<Float64ScalarSignal>
{
    private static void Add(List<Float64ScalarSignal> baseSignals, Float64ScalarSignal scalar)
    {
        if (scalar is not Float64ScalarListSignal scalarList)
        {
            if (baseSignals.Count == 0)
            {
                baseSignals.Add(scalar);
                return;
            }

            var timeMax = baseSignals[^1].MaxTime;

            baseSignals.Add(
                scalar.OffsetTimeMinTo(timeMax)
            );

            return;
        }

        foreach (var s in scalarList)
            Add(baseSignals, s);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarListSignal Finite(Float64ScalarSignal scalar1, Float64ScalarSignal scalar2)
    {
        return Finite(new[] { scalar1, scalar2 });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarListSignal Finite(Float64ScalarSignal scalar1, Float64ScalarSignal scalar2, params Float64ScalarSignal[] scalarList)
    {
        var scalars = new List<Float64ScalarSignal>(scalarList.Length + 2)
        {
            scalar1,
            scalar2
        };

        scalars.AddRange(scalarList);

        return Finite(scalars);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarListSignal Finite(IEnumerable<Float64ScalarSignal> scalarList)
    {
        var baseSignals = new List<Float64ScalarSignal>();

        foreach (var scalar in scalarList)
            Add(baseSignals, scalar);

        return new Float64ScalarListSignal(false, baseSignals);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarListSignal Periodic(Float64ScalarSignal scalar1, Float64ScalarSignal scalar2)
    {
        return Periodic(new[] { scalar1, scalar2 });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarListSignal Periodic(Float64ScalarSignal scalar1, Float64ScalarSignal scalar2, params Float64ScalarSignal[] scalarList)
    {
        var scalars = new List<Float64ScalarSignal>(scalarList.Length + 2)
        {
            scalar1,
            scalar2
        };

        scalars.AddRange(scalarList);

        return Periodic(scalars);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarListSignal Periodic(IEnumerable<Float64ScalarSignal> scalarList)
    {
        var baseSignals = new List<Float64ScalarSignal>();

        foreach (var scalar in scalarList)
            Add(baseSignals, scalar);

        return new Float64ScalarListSignal(true, baseSignals);
    }


    public IReadOnlyList<Float64ScalarSignal> BaseSignals { get; }

    public int Count
        => BaseSignals.Count;

    public Float64ScalarSignal this[int index]
        => BaseSignals[index];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarListSignal(bool isPeriodic, IReadOnlyList<Float64ScalarSignal> scalarList) 
        : base(
            Float64ScalarRange.Create(
                scalarList[0].MinTime, 
                scalarList[^1].MaxTime
            ),
            isPeriodic
        )
    {
        BaseSignals = scalarList;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return BaseSignals.Count >= 2 &&
               BaseSignals.All(s => s.IsValid());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToFiniteSignal()
    {
        return IsFinite 
            ? this 
            : new Float64ScalarListSignal(
                false,
                BaseSignals
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToPeriodicSignal()
    {
        return IsPeriodic 
            ? this 
            : new Float64ScalarListSignal(
                true,
                BaseSignals
            );
    }

    public override Float64ScalarRange FindValueRange()
    {
        var minValue = double.PositiveInfinity;
        var maxValue = double.NegativeInfinity;

        foreach (var scalar in BaseSignals)
        {
            var (rangeMin, rangeMax) = scalar.GetValueRange();

            if (minValue > rangeMin) minValue = rangeMin;
            if (maxValue < rangeMax) maxValue = rangeMax;
        }

        return Float64ScalarRange.Create(minValue, maxValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarRange FindValueRange(double minTime, double maxTime)
    {
        var minValue = double.PositiveInfinity;
        var maxValue = double.NegativeInfinity;

        foreach (var scalar in BaseSignals)
        {
            var (rangeMin, rangeMax) = scalar.FindValueRange(minTime, maxTime);
            
            if (minValue > rangeMin) minValue = rangeMin;
            if (maxValue < rangeMax) maxValue = rangeMax;
        }

        return Float64ScalarRange.Create(minValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        t = this.ClampTime(t);

        return BaseSignals.First(
            scalar => scalar.ContainsTime(t)
        ).GetValue(t);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative1Value(double t)
    {
        t = this.ClampTime(t);

        return BaseSignals.First(
            scalar => scalar.ContainsTime(t)
        ).GetDerivative1Value(t);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative2Value(double t)
    {
        t = this.ClampTime(t);

        return BaseSignals.First(
            scalar => scalar.ContainsTime(t)
        ).GetDerivative2Value(t);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<Float64ScalarSignal> GetEnumerator()
    {
        return BaseSignals.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}