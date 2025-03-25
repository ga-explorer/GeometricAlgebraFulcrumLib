using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Mapped;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Composers;

public class Float64ScalarSimpleHarmonicSignalComposer
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarSimpleHarmonicSignalComposer Create()
    {
        return new Float64ScalarSimpleHarmonicSignalComposer();
    }


    private readonly Dictionary<int, Float64ScalarSimpleHarmonicSignal> _harmonicTerms
        = new Dictionary<int, Float64ScalarSimpleHarmonicSignal>();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarSimpleHarmonicSignalComposer()
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _harmonicTerms.Values.All(term => term.IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarSimpleHarmonicSignalComposer Clear()
    {
        _harmonicTerms.Clear();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarSimpleHarmonicSignalComposer RemoveHarmonic(int harmonicFactor)
    {
        _harmonicTerms.Remove(harmonicFactor);

        return this;
    }

    public Float64ScalarSimpleHarmonicSignalComposer SetHarmonic(int harmonicFactor, double magnitude, double timeShift = 0d)
    {
        var term = Float64ScalarSimpleHarmonicSignal.Create(
            true,
            harmonicFactor,
            magnitude,
            timeShift
        );

        if (_harmonicTerms.ContainsKey(harmonicFactor))
            _harmonicTerms[harmonicFactor] = term;
        else
            _harmonicTerms.Add(harmonicFactor, term);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarSignal GetSignal(bool isPeriodic)
    {
        return isPeriodic
            ? Float64ScalarPlusSignal.Periodic(_harmonicTerms.Values)
            : Float64ScalarPlusSignal.Finite(_harmonicTerms.Values);
    }
}
