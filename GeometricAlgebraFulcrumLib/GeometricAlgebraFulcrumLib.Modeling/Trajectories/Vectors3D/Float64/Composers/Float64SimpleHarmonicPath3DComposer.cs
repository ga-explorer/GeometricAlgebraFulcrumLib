using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Mapped;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Composers;

public sealed class Float64SimpleHarmonicPath3DComposer
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SimpleHarmonicPath3DComposer Create()
    {
        return new Float64SimpleHarmonicPath3DComposer();
    }


    private readonly Dictionary<int, Float64SimpleHarmonicPath3D> _harmonicTerms
        = new Dictionary<int, Float64SimpleHarmonicPath3D>();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64SimpleHarmonicPath3DComposer()
    {
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SimpleHarmonicPath3DComposer Clear()
    {
        _harmonicTerms.Clear();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SimpleHarmonicPath3DComposer RemoveHarmonic(int harmonicFactor)
    {
        _harmonicTerms.Remove(harmonicFactor);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SimpleHarmonicPath3DComposer SetHarmonic(int harmonicFactor, double magnitudeX, double magnitudeY, double magnitudeZ)
    {
        return SetHarmonic(
            harmonicFactor,
            LinFloat64Vector3D.Create(magnitudeX, magnitudeY, magnitudeZ)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SimpleHarmonicPath3DComposer SetHarmonic(int harmonicFactor, LinFloat64Vector3D magnitude)
    {
        return SetHarmonic(
            harmonicFactor,
            magnitude,
            LinFloat64Vector3D.Create(0, 1 / 3d, -1 / 3d)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SimpleHarmonicPath3DComposer SetHarmonic(int harmonicFactor, LinFloat64Vector3D magnitude, LinFloat64Vector3D timeOffset)
    {
        var term = 
            Float64SimpleHarmonicPath3D.Periodic(
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
    public Float64Path3D GetSignal(bool isPeriodic)
    {
        return isPeriodic
            ? Float64PlusPath3D.Periodic(_harmonicTerms.Values)
            : Float64PlusPath3D.Finite(_harmonicTerms.Values);
    }
    
}