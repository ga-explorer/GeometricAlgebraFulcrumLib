using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Mapped;

public sealed class Float64ScalarTimesSignal :
    Float64ScalarSignal,
    IReadOnlyList<Float64ScalarSignal>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Add(ICollection<Float64ScalarSignal> baseSignals, Float64ScalarSignal scalar)
    {
        if (scalar is not Float64ScalarTimesSignal scalarList)
        {
            baseSignals.Add(scalar);

            return;
        }

        foreach (var s in scalarList)
            Add(baseSignals, s);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarTimesSignal Finite(Float64ScalarSignal scalar1, Float64ScalarSignal scalar2)
    {
        return Finite(new[] { scalar1, scalar2 });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarTimesSignal Finite(Float64ScalarSignal scalar1, Float64ScalarSignal scalar2, params Float64ScalarSignal[] scalarList)
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
    public static Float64ScalarTimesSignal Finite(IEnumerable<Float64ScalarSignal> scalarList)
    {
        var baseSignals = new List<Float64ScalarSignal>();

        foreach (var scalar in scalarList)
            Add(baseSignals, scalar);

        return new Float64ScalarTimesSignal(false, baseSignals);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarTimesSignal Periodic(Float64ScalarSignal scalar1, Float64ScalarSignal scalar2)
    {
        return Periodic(new[] { scalar1, scalar2 });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarTimesSignal Periodic(Float64ScalarSignal scalar1, Float64ScalarSignal scalar2, params Float64ScalarSignal[] scalarList)
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
    public static Float64ScalarTimesSignal Periodic(IEnumerable<Float64ScalarSignal> scalarList)
    {
        var baseSignals = new List<Float64ScalarSignal>();

        foreach (var scalar in scalarList)
            Add(baseSignals, scalar);

        return new Float64ScalarTimesSignal(true, baseSignals);
    }


    public IReadOnlyList<Float64ScalarSignal> BaseSignals { get; }

    public int Count
        => BaseSignals.Count;

    public Float64ScalarSignal this[int index]
        => BaseSignals[index];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarTimesSignal(bool isPeriodic, IReadOnlyList<Float64ScalarSignal> baseSignals)
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
               BaseSignals.All(s => s.IsValid());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToFiniteSignal()
    {
        return IsFinite 
            ? this 
            : new Float64ScalarTimesSignal(
                false,
                BaseSignals
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToPeriodicSignal()
    {
        return IsPeriodic 
            ? this 
            : new Float64ScalarTimesSignal(
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
        ).Aggregate(1d, (product, value) => product * value);
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