using System.Runtime.CompilerServices;

namespace NumericalGeometryLib.BasicMath.Calculus;

public sealed class SinFunction :
    IScalarD3Function
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SinFunction Create(double magnitude, double frequency)
    {
        return new SinFunction(
            magnitude,
            frequency,
            PlanarAngle.Angle0
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SinFunction Create(double magnitude, double frequency, PlanarAngle phase)
    {
        return new SinFunction(
            magnitude,
            frequency,
            phase
        );
    }


    public double Magnitude { get; }

    public double Frequency { get; }

    public PlanarAngle Phase { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private SinFunction(double magnitude, double frequency, PlanarAngle phase)
    {
        Magnitude = magnitude;
        Frequency = frequency;
        Phase = phase;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValue(double t)
    {
        return Magnitude * 
               (Frequency * t + Phase).Sin();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetFirstDerivativeValue(double t)
    {
        return Magnitude * Frequency * 
               (Frequency * t + Phase).Cos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetSecondDerivativeValue(double t)
    {
        return -Magnitude * Frequency * Frequency * 
               (Frequency * t + Phase).Sin();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetThirdDerivativeValue(double t)
    {
        return -Magnitude * Frequency * Frequency * Frequency * 
               (Frequency * t + Phase).Cos();
    }
}