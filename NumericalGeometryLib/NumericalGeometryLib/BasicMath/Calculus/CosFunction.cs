using System;
using System.Runtime.CompilerServices;

namespace NumericalGeometryLib.BasicMath.Calculus;

public sealed class CosFunction :
    IScalarD3Function
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CosFunction Create(double magnitude, double frequency)
    {
        return new CosFunction(
            magnitude,
            frequency,
            0d
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CosFunction Create(double magnitude, double frequency, double phase)
    {
        return new CosFunction(
            magnitude,
            frequency,
            phase
        );
    }


    public double Magnitude { get; }

    public double Frequency { get; }

    public double Phase { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private CosFunction(double magnitude, double frequency, double phase)
    {
        Magnitude = magnitude;
        Frequency = frequency;
        Phase = phase;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValue(double t)
    {
        return Magnitude * 
               Math.Cos(Frequency * t + Phase);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetFirstDerivative(double t)
    {
        return -Magnitude * Frequency * 
               Math.Sin(Frequency * t + Phase);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetSecondDerivative(double t)
    {
        return -Magnitude * Frequency * Frequency * 
               Math.Cos(Frequency * t + Phase);
    }

    public double GetThirdDerivative(double t)
    {
        return Magnitude * Frequency * Frequency * Frequency * 
               Math.Sin(Frequency * t + Phase);
    }
}