using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars.Harmonic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Angles;

public class SimpleHarmonicParametricAngle :
    IParametricC2Angle
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SimpleHarmonicParametricAngle Create(int frequencyHz = 1, double magnitude = 1, double parameterShift = 0)
    {
        return new SimpleHarmonicParametricAngle(frequencyHz, magnitude, parameterShift);
    }


    public int FrequencyHz { get; }

    public Float64Scalar Frequency
        => 2d * Math.PI * FrequencyHz;

    public Float64Scalar Magnitude { get; }

    public Float64Scalar ParameterShift { get; }

    public Float64ScalarRange ParameterRange
        => Float64ScalarRange.ZeroToOne;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private SimpleHarmonicParametricAngle(int frequencyHz, double magnitude, double parameterShift)
    {
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
    public Float64PlanarAngle GetAngle(double parameterValue)
    {
        parameterValue = parameterValue.ClampPeriodic(1d);

        var angle =
            Magnitude * (Frequency * (parameterValue + ParameterShift)).Cos();

        return Float64PlanarAngle.CreateFromRadians(
            angle,
            Float64PlanarAngleRange.Positive
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64PlanarAngle GetDerivative1Angle(double parameterValue)
    {
        parameterValue = parameterValue.ClampPeriodic(1d);

        var w = Frequency;

        var angle =
            -Magnitude * w * (w * (parameterValue + ParameterShift)).Sin();

        return Float64PlanarAngle.CreateFromRadians(
            angle,
            Float64PlanarAngleRange.Positive
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64PlanarAngle GetDerivative2Point(double parameterValue)
    {
        parameterValue = parameterValue.ClampPeriodic(1d);

        var w = Frequency;

        var angle =
            -Magnitude * w * w * (w * (parameterValue + ParameterShift)).Cos();

        return Float64PlanarAngle.CreateFromRadians(
            angle,
            Float64PlanarAngleRange.Positive
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IParametricScalar ToRadianParametricScalar()
    {
        return SimpleHarmonicParametricScalar.Create(
            ParameterRange,
            Frequency,
            Magnitude,
            ParameterShift
        );
    }

}