using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Mapped;

public sealed class Float64TimesPath2D :
    Float64Path2D,
    IReadOnlyList<Float64Path2D>
{
    private static void Add(ICollection<Float64Path2D> baseSignals, Float64Path2D scalar)
    {
        if (scalar is not Float64TimesPath2D scalarList)
        {
            baseSignals.Add(scalar);

            return;
        }

        foreach (var s in scalarList)
            Add(baseSignals, s);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64TimesPath2D Finite(Float64Path2D scalar1, Float64Path2D scalar2)
    {
        return Finite(new[] { scalar1, scalar2 });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64TimesPath2D Finite(Float64Path2D scalar1, Float64Path2D scalar2, params Float64Path2D[] scalarList)
    {
        var scalars = new List<Float64Path2D>(scalarList.Length + 2)
        {
            scalar1,
            scalar2
        };

        scalars.AddRange(scalarList);

        return Finite(scalars);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64TimesPath2D Finite(IEnumerable<Float64Path2D> scalarList)
    {
        var baseSignals = new List<Float64Path2D>();

        foreach (var scalar in scalarList)
            Add(baseSignals, scalar);

        return new Float64TimesPath2D(false, baseSignals);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64TimesPath2D Periodic(Float64Path2D scalar1, Float64Path2D scalar2)
    {
        return Periodic(new[] { scalar1, scalar2 });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64TimesPath2D Periodic(Float64Path2D scalar1, Float64Path2D scalar2, params Float64Path2D[] scalarList)
    {
        var scalars = new List<Float64Path2D>(scalarList.Length + 2)
        {
            scalar1,
            scalar2
        };

        scalars.AddRange(scalarList);

        return Periodic(scalars);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64TimesPath2D Periodic(IEnumerable<Float64Path2D> scalarList)
    {
        var baseSignals = new List<Float64Path2D>();

        foreach (var scalar in scalarList)
            Add(baseSignals, scalar);

        return new Float64TimesPath2D(true, baseSignals);
    }


    public IReadOnlyList<Float64Path2D> BaseSignals { get; }

    public int Count
        => BaseSignals.Count;

    public Float64Path2D this[int index]
        => BaseSignals[index];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64TimesPath2D(bool isPeriodic, IReadOnlyList<Float64Path2D> baseSignals)
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
    public override Float64Path2D ToFinitePath()
    {
        return IsFinite 
            ? this 
            : new Float64TimesPath2D(
                false,
                BaseSignals
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path2D ToPeriodicPath()
    {
        return IsPeriodic 
            ? this 
            : new Float64TimesPath2D(
                true,
                BaseSignals
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetValue(double t)
    {
        t = this.ClampTime(t);

        return BaseSignals.Aggregate(
            LinFloat64Vector2D.Symmetric,
            (a, b) => a.VectorComponentTimes(b.GetValue(t))
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<Float64Path2D> GetEnumerator()
    {
        return BaseSignals.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}