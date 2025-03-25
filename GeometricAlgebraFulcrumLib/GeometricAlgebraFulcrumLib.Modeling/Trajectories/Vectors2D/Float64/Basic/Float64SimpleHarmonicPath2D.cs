using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Basic;

public sealed class Float64SimpleHarmonicPath2D :
    Float64Path2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SimpleHarmonicPath2D Finite(int harmonicFactor, LinFloat64Vector2D magnitude, LinFloat64Vector2D timeOffset)
    {
        return new Float64SimpleHarmonicPath2D(
            false, 
            harmonicFactor, 
            magnitude, 
            timeOffset
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SimpleHarmonicPath2D Periodic(int harmonicFactor, LinFloat64Vector2D magnitude, LinFloat64Vector2D timeOffset)
    {
        return new Float64SimpleHarmonicPath2D(
            true, 
            harmonicFactor, 
            magnitude, 
            timeOffset
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SimpleHarmonicPath2D Create(bool isPeriodic, int harmonicFactor, LinFloat64Vector2D magnitude, LinFloat64Vector2D timeOffset)
    {
        return new Float64SimpleHarmonicPath2D(
            isPeriodic, 
            harmonicFactor, 
            magnitude, 
            timeOffset
        );
    }


    public int HarmonicFactor { get; }

    public LinFloat64Vector2D Magnitude { get; }

    public LinFloat64Vector2D TimeOffset { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64SimpleHarmonicPath2D(bool isPeriodic, int harmonicFactor, LinFloat64Vector2D magnitude, LinFloat64Vector2D timeOffset)
        : base(Float64ScalarRange.SymmetricPi, isPeriodic)
    {
        //if (harmonicFactor < 1)
        //    throw new ArgumentOutOfRangeException(nameof(harmonicFactor));

        if (!magnitude.IsFinite())
            throw new ArgumentException(nameof(magnitude));

        if (!timeOffset.IsFinite())
            throw new ArgumentException(nameof(timeOffset));

        HarmonicFactor = harmonicFactor;
        Magnitude = magnitude;
        TimeOffset = timeOffset;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return Magnitude.IsFinite() &&
               TimeOffset.IsFinite();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path2D ToFinitePath()
    {
        return IsFinite
            ? this
            : new Float64SimpleHarmonicPath2D(
                false,
                HarmonicFactor,
                Magnitude,
                TimeOffset
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path2D ToPeriodicPath()
    {
        return IsPeriodic
            ? this
            : new Float64SimpleHarmonicPath2D(
                true,
                HarmonicFactor,
                Magnitude,
                TimeOffset
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetValue(double t)
    {
        t = this.ClampTime(t);

        var w = Math.Tau * HarmonicFactor;

        return LinFloat64Vector2D.Create(
            Magnitude.X * (w * (t + TimeOffset.X)).Cos(),
            Magnitude.Y * (w * (t + TimeOffset.Y)).Cos()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative1Value(double t)
    {
        t = this.ClampTime(t);

        var w = Math.Tau * HarmonicFactor;

        return LinFloat64Vector2D.Create(
            -Magnitude.X * w * (w * (t + TimeOffset.X)).Sin(),
            -Magnitude.Y * w * (w * (t + TimeOffset.Y)).Sin()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative2Value(double t)
    {
        t = this.ClampTime(t);

        var w = Math.Tau * HarmonicFactor;
        var w2 = w * w;

        return LinFloat64Vector2D.Create(
            -Magnitude.X * w2 * (w * (t + TimeOffset.X)).Cos(),
            -Magnitude.Y * w2 * (w * (t + TimeOffset.Y)).Cos()
        );
    }

    
}