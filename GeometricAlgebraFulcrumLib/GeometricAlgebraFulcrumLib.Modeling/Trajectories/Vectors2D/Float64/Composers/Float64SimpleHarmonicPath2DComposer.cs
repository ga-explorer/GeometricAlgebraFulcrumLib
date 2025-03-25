using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Mapped;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Composers;

public sealed class Float64SimpleHarmonicPath2DComposer
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SimpleHarmonicPath2DComposer Create()
    {
        return new Float64SimpleHarmonicPath2DComposer();
    }


    private readonly Dictionary<int, Float64SimpleHarmonicPath2D> _harmonicTerms
        = new Dictionary<int, Float64SimpleHarmonicPath2D>();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64SimpleHarmonicPath2DComposer()
    {
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SimpleHarmonicPath2DComposer Clear()
    {
        _harmonicTerms.Clear();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SimpleHarmonicPath2DComposer RemoveHarmonic(int harmonicFactor)
    {
        _harmonicTerms.Remove(harmonicFactor);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SimpleHarmonicPath2DComposer SetHarmonic(int harmonicFactor, LinFloat64Vector2D magnitude, LinFloat64Vector2D timeOffset)
    {
        var term = 
            Float64SimpleHarmonicPath2D.Periodic(
                harmonicFactor,
                magnitude,
                timeOffset
            );

        if (_harmonicTerms.ContainsKey(harmonicFactor))
            _harmonicTerms[harmonicFactor] = term;
        else
            _harmonicTerms.Add(harmonicFactor, term);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Path2D GetSignal(bool isPeriodic)
    {
        return isPeriodic
            ? Float64PlusPath2D.Periodic(_harmonicTerms.Values)
            : Float64PlusPath2D.Finite(_harmonicTerms.Values);
    }
    
}