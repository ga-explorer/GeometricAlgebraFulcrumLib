using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.SignalAlgebra;

public class Float64SignalValidator
{
    public double ZeroEpsilon { get; set; } = 1e-7;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ValidateEqualZero(Scalar<Float64Signal> scalarSignal1)
    {
        return ValidateEqualZero(scalarSignal1.ScalarValue);
    }

    public bool ValidateEqualZero(Float64Signal scalarSignal1)
    {
        if (scalarSignal1.IsNearZero(ZeroEpsilon))
            return true;

        var scalarSignal1Rms =
            scalarSignal1.Select(s => s.Square()).Average().Sqrt();

        if (scalarSignal1Rms.IsNearZero(ZeroEpsilon))
            return true;

        Console.WriteLine($"RMS value: {scalarSignal1Rms:G}");
        Console.WriteLine();

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ValidateEqual(Scalar<Float64Signal> scalarSignal1, Scalar<Float64Signal> scalarSignal2)
    {
        return ValidateEqual(
            scalarSignal1.ScalarValue,
            scalarSignal2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ValidateEqual(Scalar<Float64Signal> scalarSignal1, Float64Signal scalarSignal2)
    {
        return ValidateEqual(
            scalarSignal1.ScalarValue,
            scalarSignal2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ValidateEqual(Float64Signal scalarSignal1, Scalar<Float64Signal> scalarSignal2)
    {
        return ValidateEqual(
            scalarSignal1,
            scalarSignal2.ScalarValue
        );
    }

    public bool ValidateEqual(Float64Signal scalarSignal1, Float64Signal scalarSignal2)
    {
        var errorSignal =
            scalarSignal1 - scalarSignal2;

        if (errorSignal.IsNearZero(ZeroEpsilon))
            return true;

        var snr = 
            scalarSignal1.PeakSignalToNoiseRatioDb(scalarSignal2).NaNToZero();

        if (snr > 50)
            return true;

        var scalarSignal1Rms = scalarSignal1.Select(s => s.Square()).Average().Sqrt();
        var errorSignalRms = errorSignal.Select(s => s.Square()).Average().Sqrt();

        var errorSignalRmsRatio = (errorSignalRms / scalarSignal1Rms).NaNToZero();

        Console.WriteLine($"SNR: {snr:G}, RMS error ratio: {errorSignalRmsRatio:G}");
        Console.WriteLine();

        return false;
    }
        
}