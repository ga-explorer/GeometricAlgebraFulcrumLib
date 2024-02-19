using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars.Harmonic;

public class SimpleHarmonicParametricScalar :
    IParametricC2Scalar
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SimpleHarmonicParametricScalar Create(double frequencyHz = 1d, double magnitude = 1, double parameterShift = 0)
    {
        return new SimpleHarmonicParametricScalar(
            Float64ScalarRange.Infinite,
            frequencyHz, 
            magnitude, 
            parameterShift
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SimpleHarmonicParametricScalar Create(Float64ScalarRange parameterRange, double frequencyHz = 1d, double magnitude = 1, double parameterShift = 0)
    {
        return new SimpleHarmonicParametricScalar(
            parameterRange,
            frequencyHz, 
            magnitude, 
            parameterShift
        );
    }


    public Float64Scalar FrequencyHz { get; }

    public Float64Scalar Frequency 
        => 2d * Math.PI * FrequencyHz;

    public Float64Scalar Magnitude { get; }

    public Float64Scalar ParameterShift { get; }
    
    public Float64ScalarRange ParameterRange { get; }
        
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private SimpleHarmonicParametricScalar(Float64ScalarRange parameterRange, Float64Scalar frequencyHz, double magnitude, double parameterShift)
    {
        ParameterRange = parameterRange;
        FrequencyHz = frequencyHz;
        Magnitude = magnitude;
        ParameterShift = parameterShift;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Magnitude.IsValid() &&
               ParameterShift.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetValue(double parameterValue)
    {
        //parameterValue = parameterValue.ClampPeriodic(1d);
        
        return Magnitude * (Frequency * (parameterValue + ParameterShift)).Cos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetDerivative1Value(double parameterValue)
    {
        //parameterValue = parameterValue.ClampPeriodic(1d);

        var w = Frequency;

        return -Magnitude * w * (w * (parameterValue + ParameterShift)).Sin();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetDerivative2Value(double parameterValue)
    {
        //parameterValue = parameterValue.ClampPeriodic(1d);

        var w = Frequency;
        
        return -Magnitude * w * w * (w * (parameterValue + ParameterShift)).Cos();
    }
    
}