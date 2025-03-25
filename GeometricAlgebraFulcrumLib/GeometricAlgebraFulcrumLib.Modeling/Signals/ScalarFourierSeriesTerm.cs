using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Signals.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Signals;

public sealed class ScalarFourierSeriesTerm
{
    public double CosScalar { get; private set; }

    public double SinScalar { get; private set; }

    public double Frequency { get; }

    public double FrequencyHz
        => Frequency / (Math.Tau);



    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ScalarFourierSeriesTerm(double cosScalar, double sinScalar, double frequency)
    {
        CosScalar = cosScalar;
        SinScalar = Math.Sign(frequency) * sinScalar;
        Frequency = frequency.Abs();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ScalarFourierSeriesTerm AddScalars(double cosScalar, double sinScalar)
    {
        CosScalar += cosScalar;
        SinScalar += sinScalar;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ScalarFourierSeriesTerm Negative()
    {
        return new ScalarFourierSeriesTerm(-CosScalar, -SinScalar, Frequency);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ScalarFourierSeriesTerm Times(double value)
    {
        return new ScalarFourierSeriesTerm(CosScalar * value, SinScalar * value, Frequency);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetScalar(double parameterValue)
    {
        var angle = Frequency * parameterValue;

        return CosScalar * angle.Cos() + SinScalar * angle.Sin();
    }

    public double GetScalarDt(double parameterValue, int degree = 1)
    {
        if (degree < 0)
            throw new ArgumentOutOfRangeException(nameof(degree));

        if (degree == 0)
            return GetScalar(parameterValue);

        var f = degree switch
        {
            1 => Frequency,
            2 => Frequency.Square(),
            3 => Frequency.Cube(),
            _ => Frequency.Power(degree)
        };

        var (cosScalar, sinScalar) = (degree % 4) switch
        {
            0 => new Pair<double>(f * CosScalar, f * SinScalar),
            1 => new Pair<double>(f * SinScalar, -f * CosScalar),
            2 => new Pair<double>(-f * CosScalar, -f * SinScalar),
            _ => new Pair<double>(-f * SinScalar, f * CosScalar)
        };

        var angle = Frequency * parameterValue;

        return cosScalar * angle.Cos() + sinScalar * angle.Sin();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<double> GetScalars(IEnumerable<double> parameterValues)
    {
        return parameterValues.Select(GetScalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal GetSignal(Float64SampledTimeSignal tValuesSignal)
    {
        return tValuesSignal.Select(GetScalar).CreateSignal(tValuesSignal.SamplingRate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<double> GetScalarsDt(IEnumerable<double> parameterValues)
    {
        return parameterValues.Select(GetScalarDt);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal GetSignalDt(Float64SampledTimeSignal tValuesSignal)
    {
        return tValuesSignal.Select(GetScalarDt).CreateSignal(tValuesSignal.SamplingRate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetEnergy(int sampleCount)
    {
        return (CosScalar * CosScalar + SinScalar * SinScalar) * sampleCount / (4 * Math.PI);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetEnergyDc(int sampleCount)
    {
        return Frequency == 0 ? GetEnergy(sampleCount) : 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetEnergyAc(int sampleCount)
    {
        return Frequency == 0 ? 0d : GetEnergy(sampleCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetEnergyAc(Float64SampledTimeSignal tValues)
    {
        return Frequency != 0
            ? tValues.Select(GetScalar).CreateSignal(tValues.SamplingRate).EnergyAc()
            : 0d;
    }

    public ScalarFourierSeriesTerm GetFourierTermDerivativeN(int order = 1)
    {
        if (order < 0)
            throw new ArgumentOutOfRangeException(nameof(order));

        if (order == 0)
            return this;

        var n = order % 4;

        var f = order switch
        {
            1 => Frequency,
            2 => Frequency.Square(),
            3 => Frequency.Cube(),
            _ => Frequency.Power(order)
        };

        return n switch
        {
            0 => new ScalarFourierSeriesTerm(
                f * CosScalar,
                f * SinScalar,
                Frequency
            ),

            1 => new ScalarFourierSeriesTerm(
                f * SinScalar,
                -f * CosScalar,
                Frequency
            ),

            2 => new ScalarFourierSeriesTerm(
                -f * CosScalar,
                -f * SinScalar,
                Frequency
            ),

            _ => new ScalarFourierSeriesTerm(
                -f * SinScalar,
                f * CosScalar,
                Frequency
            )
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetCSharpCode()
    {
        return $"interpolator.SetTerm({Frequency:G}, {CosScalar:G}, {SinScalar:G});";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        if (CosScalar == 0 && SinScalar == 0)
            return "0";

        if (CosScalar != 0 && SinScalar == 0)
            return $"({CosScalar:G}) Cos[2π({FrequencyHz:G}) t]";

        if (CosScalar == 0 && SinScalar != 0)
            return $"({SinScalar:G}) Sin[2π({FrequencyHz:G}) t]";

        return $"({CosScalar:G}) Cos[2π({FrequencyHz:G}) t] + ({SinScalar:G}) Sin[2π({FrequencyHz:G}) t]";
    }
}