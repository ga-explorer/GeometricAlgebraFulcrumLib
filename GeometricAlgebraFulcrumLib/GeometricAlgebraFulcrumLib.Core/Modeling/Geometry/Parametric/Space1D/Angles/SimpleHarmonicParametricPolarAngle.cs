using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars.Harmonic;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Angles;

public class SimpleHarmonicParametricPolarAngle :
    IParametricC2PolarAngle
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SimpleHarmonicParametricPolarAngle Create(int frequencyHz = 1, double magnitude = 1, double parameterShift = 0)
    {
        return new SimpleHarmonicParametricPolarAngle(frequencyHz, magnitude, parameterShift);
    }


    public int FrequencyHz { get; }

    public Float64Scalar Frequency
        => 2d * Math.PI * FrequencyHz;

    public Float64Scalar Magnitude { get; }

    public Float64Scalar ParameterShift { get; }

    public Float64ScalarRange ParameterRange
        => Float64ScalarRange.ZeroToOne;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private SimpleHarmonicParametricPolarAngle(int frequencyHz, double magnitude, double parameterShift)
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
    public LinFloat64PolarAngle GetAngle(double parameterValue)
    {
        parameterValue = parameterValue.ClampPeriodic(1d);

        var angle =
            Magnitude * (Frequency * (parameterValue + ParameterShift)).Cos();

        return angle.RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle GetDerivative1Angle(double parameterValue)
    {
        parameterValue = parameterValue.ClampPeriodic(1d);

        var w = Frequency;

        var angle =
            -Magnitude * w * (w * (parameterValue + ParameterShift)).Sin();

        return angle.RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle GetDerivative2Point(double parameterValue)
    {
        parameterValue = parameterValue.ClampPeriodic(1d);

        var w = Frequency;

        var angle =
            -Magnitude * w * w * (w * (parameterValue + ParameterShift)).Cos();

        return angle.RadiansToPolarAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IFloat64ParametricScalar ToRadianParametricScalar()
    {
        return SimpleHarmonicParametricScalar.Create(
            ParameterRange,
            Frequency,
            Magnitude,
            ParameterShift
        );
    }

}