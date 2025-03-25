using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;

public sealed class Float64SimpleHarmonicPath3D :
    Float64Path3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SimpleHarmonicPath3D FiniteSymmetric(int harmonicFactor, LinFloat64Vector3D magnitudeVector)
    {
        return new Float64SimpleHarmonicPath3D(
            false,
            harmonicFactor,
            magnitudeVector,
            LinFloat64Vector3D.Create(0, 1 / 3d, -1 / 3d)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SimpleHarmonicPath3D PeriodicSymmetric(int harmonicFactor, LinFloat64Vector3D magnitudeVector)
    {
        return new Float64SimpleHarmonicPath3D(
            true,
            harmonicFactor,
            magnitudeVector,
            LinFloat64Vector3D.Create(0, 1 / 3d, -1 / 3d)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SimpleHarmonicPath3D Finite(int harmonicFactor, LinFloat64Vector3D magnitude, LinFloat64Vector3D timeOffset)
    {
        return new Float64SimpleHarmonicPath3D(
            false, 
            harmonicFactor, 
            magnitude, 
            timeOffset
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SimpleHarmonicPath3D Periodic(int harmonicFactor, LinFloat64Vector3D magnitude, LinFloat64Vector3D timeOffset)
    {
        return new Float64SimpleHarmonicPath3D(
            true, 
            harmonicFactor, 
            magnitude, 
            timeOffset
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SimpleHarmonicPath3D Create(bool isPeriodic, int harmonicFactor, LinFloat64Vector3D magnitude, LinFloat64Vector3D timeOffset)
    {
        return new Float64SimpleHarmonicPath3D(
            isPeriodic, 
            harmonicFactor, 
            magnitude, 
            timeOffset
        );
    }


    public int HarmonicFactor { get; }

    public LinFloat64Vector3D Magnitude { get; }

    public LinFloat64Vector3D TimeOffset { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64SimpleHarmonicPath3D(bool isPeriodic, int harmonicFactor, LinFloat64Vector3D magnitude, LinFloat64Vector3D timeOffset)
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
    public override Float64Path3D ToFinitePath()
    {
        return IsFinite
            ? this
            : new Float64SimpleHarmonicPath3D(
                false,
                HarmonicFactor,
                Magnitude,
                TimeOffset
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3D ToPeriodicPath()
    {
        return IsPeriodic
            ? this
            : new Float64SimpleHarmonicPath3D(
                true,
                HarmonicFactor,
                Magnitude,
                TimeOffset
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetValue(double t)
    {
        t = this.ClampTime(t);

        var w = Math.Tau * HarmonicFactor;

        return LinFloat64Vector3D.Create(
            Magnitude.X * (w * (t + TimeOffset.X)).Cos(),
            Magnitude.Y * (w * (t + TimeOffset.Y)).Cos(),
            Magnitude.Z * (w * (t + TimeOffset.Z)).Cos()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative1Value(double t)
    {
        t = this.ClampTime(t);

        var w = Math.Tau * HarmonicFactor;

        return LinFloat64Vector3D.Create(
            -Magnitude.X * w * (w * (t + TimeOffset.X)).Sin(),
            -Magnitude.Y * w * (w * (t + TimeOffset.Y)).Sin(),
            -Magnitude.Z * w * (w * (t + TimeOffset.Z)).Sin()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative2Value(double t)
    {
        t = this.ClampTime(t);

        var w = Math.Tau * HarmonicFactor;
        var w2 = w * w;

        return LinFloat64Vector3D.Create(
            -Magnitude.X * w2 * (w * (t + TimeOffset.X)).Cos(),
            -Magnitude.Y * w2 * (w * (t + TimeOffset.Y)).Cos(),
            -Magnitude.Z * w2 * (w * (t + TimeOffset.Z)).Cos()
        );
    }

    
}