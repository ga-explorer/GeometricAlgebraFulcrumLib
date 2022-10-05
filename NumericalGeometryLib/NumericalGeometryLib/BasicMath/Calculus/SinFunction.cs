using System;
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
            0d
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SinFunction Create(double magnitude, double frequency, double phase)
    {
        return new SinFunction(
            magnitude,
            frequency,
            phase
        );
    }


    public double Magnitude { get; }

    public double Frequency { get; }

    public double Phase { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private SinFunction(double magnitude, double frequency, double phase)
    {
        Magnitude = magnitude;
        Frequency = frequency;
        Phase = phase;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValue(double t)
    {
        return Magnitude * 
               Math.Sin(Frequency * t + Phase);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetFirstDerivative(double t)
    {
        return Magnitude * Frequency * 
               Math.Cos(Frequency * t + Phase);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetSecondDerivative(double t)
    {
        return -Magnitude * Frequency * Frequency * 
               Math.Sin(Frequency * t + Phase);
    }

    public double GetThirdDerivative(double t)
    {
        return -Magnitude * Frequency * Frequency * Frequency * 
               Math.Cos(Frequency * t + Phase);
    }
}