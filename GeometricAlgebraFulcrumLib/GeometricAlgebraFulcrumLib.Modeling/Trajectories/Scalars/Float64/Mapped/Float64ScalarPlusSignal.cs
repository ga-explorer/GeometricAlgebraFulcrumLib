using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Mapped;

public sealed class Float64ScalarPlusSignal :
    Float64ScalarSignal,
    IReadOnlyList<Float64ScalarSignal>
{
    private static void Add(ICollection<Float64ScalarSignal> baseSignals, Float64ScalarSignal scalar)
    {
        if (scalar is not Float64ScalarPlusSignal scalarList)
        {
            baseSignals.Add(scalar);

            return;
        }

        foreach (var s in scalarList)
            Add(baseSignals, s);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarPlusSignal Finite(Float64ScalarSignal scalar1, Float64ScalarSignal scalar2)
    {
        return Finite(new[] { scalar1, scalar2 });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarPlusSignal Finite(Float64ScalarSignal scalar1, Float64ScalarSignal scalar2, params Float64ScalarSignal[] scalarList)
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
    public static Float64ScalarPlusSignal Finite(IEnumerable<Float64ScalarSignal> scalarList)
    {
        var baseSignals = new List<Float64ScalarSignal>();

        foreach (var scalar in scalarList)
            Add(baseSignals, scalar);

        return new Float64ScalarPlusSignal(false, baseSignals);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarPlusSignal Periodic(Float64ScalarSignal scalar1, Float64ScalarSignal scalar2)
    {
        return Periodic(new[] { scalar1, scalar2 });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarPlusSignal Periodic(Float64ScalarSignal scalar1, Float64ScalarSignal scalar2, params Float64ScalarSignal[] scalarList)
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
    public static Float64ScalarPlusSignal Periodic(IEnumerable<Float64ScalarSignal> scalarList)
    {
        var baseSignals = new List<Float64ScalarSignal>();

        foreach (var scalar in scalarList)
            Add(baseSignals, scalar);

        return new Float64ScalarPlusSignal(true, baseSignals);
    }


    public IReadOnlyList<Float64ScalarSignal> BaseSignals { get; }

    public int Count
        => BaseSignals.Count;

    public Float64ScalarSignal this[int index]
        => BaseSignals[index];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarPlusSignal(bool isPeriodic, IReadOnlyList<Float64ScalarSignal> baseSignals)
        : base(
            Float64ScalarRange.Create(
                baseSignals.Select(s => s.MinTime).Min(), 
                baseSignals.Select(s => s.MaxTime).Max()
            ),
            isPeriodic
        )
    {
        BaseSignals = baseSignals;

        Debug.Assert(IsValid());
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return BaseSignals.Count >= 2 &&
               BaseSignals.All(s => s.IsValid()) &&
               TimeRange.IsFinite;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToFiniteSignal()
    {
        return IsFinite 
            ? this 
            : new Float64ScalarPlusSignal(
                false,
                BaseSignals
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToPeriodicSignal()
    {
        return IsPeriodic 
            ? this 
            : new Float64ScalarPlusSignal(
                true,
                BaseSignals
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        t = this.ClampTime(t);

        return BaseSignals.Select(
            s => s.GetValue(t)
        ).Sum();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative1Value(double t)
    {
        t = this.ClampTime(t);

        return BaseSignals.Select(
            s => s.GetDerivative1Value(t)
        ).Sum();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative2Value(double t)
    {
        t = this.ClampTime(t);

        return BaseSignals.Select(
            s => s.GetDerivative2Value(t)
        ).Sum();
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