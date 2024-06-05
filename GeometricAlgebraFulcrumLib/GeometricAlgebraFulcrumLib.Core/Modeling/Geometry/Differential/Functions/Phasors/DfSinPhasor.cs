using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions.Phasors;

public sealed class DfSinPhasor :
    DifferentialCustomFunction
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfSinPhasor Create(double magnitude, double frequency)
    {
        return new DfSinPhasor(
            magnitude,
            frequency,
            LinFloat64PolarAngle.Angle0
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfSinPhasor Create(double magnitude, double frequency, LinFloat64Angle phase)
    {
        return new DfSinPhasor(
            magnitude,
            frequency,
            phase
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfSinPhasor operator *(double f1, DfSinPhasor f2)
    {
        return new DfSinPhasor(
            f2.Magnitude * f1,
            f2.Frequency,
            f2.Phase
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfSinPhasor operator *(DfSinPhasor f1, double f2)
    {
        return new DfSinPhasor(
            f1.Magnitude * f2,
            f1.Frequency,
            f1.Phase
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfSinPhasor operator /(DfSinPhasor f1, double f2)
    {
        return new DfSinPhasor(
            f1.Magnitude / f2,
            f1.Frequency,
            f1.Phase
        );
    }


    public override bool IsConstant
        => Magnitude == 0d || Frequency == 0d;

    public double Magnitude { get; }

    public double Frequency { get; }

    public LinFloat64Angle Phase { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DfSinPhasor(double magnitude, double frequency, LinFloat64Angle phase)
        : base(false)
    {
        Debug.Assert(
            !magnitude.IsNaNOrInfinite() &&
            !frequency.IsNaNOrInfinite()
        );

        Magnitude = magnitude;
        Frequency = frequency;
        Phase = phase;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Tuple<bool, DifferentialFunction> TrySimplify()
    {
        return new Tuple<bool, DifferentialFunction>(false, this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction Simplify()
    {
        return this;

        //if (Magnitude == 0d)
        //    return DfConstant.Zero;

        //if (Frequency == 0d)
        //    return DfConstant.Create(Magnitude * Phase.Sin()).Simplify();

        //return (Magnitude * DfSin.Create(Frequency * DfVar.DefaultFunction + Phase.Radians)).Simplify();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction GetDerivative1()
    {
        return DfCosPhasor.Create(Magnitude * Frequency, Frequency, Phase);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        return Magnitude * (Frequency * t + Phase.RadiansValue).Sin();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction GetDerivativeN(int order)
    {
        var freqMagnitude = order switch
        {
            0 => 1d,
            1 => Frequency,
            2 => Frequency * Frequency,
            3 => Frequency * Frequency * Frequency,
            _ => Frequency.Power(order)
        };

        var magnitude = Magnitude * freqMagnitude;

        return (order % 4) switch
        {
            0 => Create(magnitude, Frequency, Phase),
            1 => DfCosPhasor.Create(magnitude, Frequency, Phase),
            2 => Create(-magnitude, Frequency, Phase),
            _ => DfCosPhasor.Create(-magnitude, Frequency, Phase)
        };
    }
}