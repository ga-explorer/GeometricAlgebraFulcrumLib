using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions.Phasors;

public sealed class DfCosPhasor :
    DifferentialCustomFunction
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfCosPhasor Create(double magnitude, double frequency)
    {
        return new DfCosPhasor(
            magnitude,
            frequency,
            LinFloat64PolarAngle.Angle0
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfCosPhasor Create(double magnitude, double frequency, LinFloat64Angle phase)
    {
        return new DfCosPhasor(
            magnitude,
            frequency,
            phase
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfCosPhasor operator *(double f1, DfCosPhasor f2)
    {
        return new DfCosPhasor(
            f2.Magnitude * f1,
            f2.Frequency,
            f2.Phase
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfCosPhasor operator *(DfCosPhasor f1, double f2)
    {
        return new DfCosPhasor(
            f1.Magnitude * f2,
            f1.Frequency,
            f1.Phase
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfCosPhasor operator /(DfCosPhasor f1, double f2)
    {
        return new DfCosPhasor(
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
    private DfCosPhasor(double magnitude, double frequency, LinFloat64Angle phase)
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
        //    return DfConstant.Create(Magnitude * Phase.Cos()).Simplify();

        //return (Magnitude * DfCos.Create(Frequency * DfVar.DefaultFunction + Phase.Radians)).Simplify();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction GetDerivative1()
    {
        return DfSinPhasor.Create(-Magnitude * Frequency, Frequency, Phase);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        return Magnitude * (Frequency * t + Phase.RadiansValue).Cos();
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
            1 => DfSinPhasor.Create(-magnitude, Frequency, Phase),
            2 => Create(-magnitude, Frequency, Phase),
            _ => DfSinPhasor.Create(magnitude, Frequency, Phase)
        };
    }
}