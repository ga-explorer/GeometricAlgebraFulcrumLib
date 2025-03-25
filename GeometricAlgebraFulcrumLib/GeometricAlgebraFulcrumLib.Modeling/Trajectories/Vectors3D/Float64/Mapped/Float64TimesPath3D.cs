using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Mapped;

public sealed class Float64TimesPath3D :
    Float64Path3D,
    IReadOnlyList<Float64Path3D>
{
    private static void Add(ICollection<Float64Path3D> baseSignals, Float64Path3D scalar)
    {
        if (scalar is not Float64TimesPath3D scalarList)
        {
            baseSignals.Add(scalar);

            return;
        }

        foreach (var s in scalarList)
            Add(baseSignals, s);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64TimesPath3D Finite(Float64Path3D scalar1, Float64Path3D scalar2)
    {
        return Finite(new[] { scalar1, scalar2 });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64TimesPath3D Finite(Float64Path3D scalar1, Float64Path3D scalar2, params Float64Path3D[] scalarList)
    {
        var scalars = new List<Float64Path3D>(scalarList.Length + 2)
        {
            scalar1,
            scalar2
        };

        scalars.AddRange(scalarList);

        return Finite(scalars);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64TimesPath3D Finite(IEnumerable<Float64Path3D> scalarList)
    {
        var baseSignals = new List<Float64Path3D>();

        foreach (var scalar in scalarList)
            Add(baseSignals, scalar);

        return new Float64TimesPath3D(false, baseSignals);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64TimesPath3D Periodic(Float64Path3D scalar1, Float64Path3D scalar2)
    {
        return Periodic(new[] { scalar1, scalar2 });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64TimesPath3D Periodic(Float64Path3D scalar1, Float64Path3D scalar2, params Float64Path3D[] scalarList)
    {
        var scalars = new List<Float64Path3D>(scalarList.Length + 2)
        {
            scalar1,
            scalar2
        };

        scalars.AddRange(scalarList);

        return Periodic(scalars);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64TimesPath3D Periodic(IEnumerable<Float64Path3D> scalarList)
    {
        var baseSignals = new List<Float64Path3D>();

        foreach (var scalar in scalarList)
            Add(baseSignals, scalar);

        return new Float64TimesPath3D(true, baseSignals);
    }


    public IReadOnlyList<Float64Path3D> BaseSignals { get; }

    public int Count
        => BaseSignals.Count;

    public Float64Path3D this[int index]
        => BaseSignals[index];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64TimesPath3D(bool isPeriodic, IReadOnlyList<Float64Path3D> baseSignals)
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
    public override Float64Path3D ToFinitePath()
    {
        return IsFinite 
            ? this 
            : new Float64TimesPath3D(
                false,
                BaseSignals
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3D ToPeriodicPath()
    {
        return IsPeriodic 
            ? this 
            : new Float64TimesPath3D(
                true,
                BaseSignals
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetValue(double t)
    {
        t = this.ClampTime(t);

        return BaseSignals.Aggregate(
            LinFloat64Vector3D.Symmetric,
            (a, b) => a.VectorComponentTimes(b.GetValue(t))
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<Float64Path3D> GetEnumerator()
    {
        return BaseSignals.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}