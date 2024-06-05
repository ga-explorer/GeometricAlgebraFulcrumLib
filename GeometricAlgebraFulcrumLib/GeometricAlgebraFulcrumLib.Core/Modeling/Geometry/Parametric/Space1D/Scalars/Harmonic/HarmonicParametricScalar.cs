using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars.Harmonic;

public class HarmonicParametricScalar :
        IFloat64ParametricC2Scalar
    //IArcLengthCurve1D
{
    //private AdaptiveCurve1D? _adaptiveCurve;
    //private double _adaptiveCurveLength;
    private readonly Dictionary<int, HarmonicParametricScalarTerm> _harmonicTerms
        = new Dictionary<int, HarmonicParametricScalarTerm>();


    public Float64ScalarRange ParameterRange
        => Float64ScalarRange.ZeroToOne;
        

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public HarmonicCurve1D UpdateSampling()
    //{
    //    return UpdateSampling(
    //        new AdaptiveCurveSamplingOptions1D(
    //            5.DegreesToAngle(),
    //            3,
    //            16
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public HarmonicCurve1D UpdateSampling(AdaptiveCurveSamplingOptions1D samplingOptions)
    //{
    //    _adaptiveCurve = this.CreateAdaptiveCurve1D(samplingOptions);
    //    _adaptiveCurveLength = _adaptiveCurve.GetLength();

    //    return this;
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HarmonicParametricScalar Clear()
    {
        //_adaptiveCurve = null;
        _harmonicTerms.Clear();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HarmonicParametricScalar RemoveHarmonic(int harmonicFactor)
    {
        _harmonicTerms.Remove(harmonicFactor);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HarmonicParametricScalar SetHarmonic(int harmonicFactor, double magnitude)
    {
        return SetHarmonic(harmonicFactor, magnitude, 0d);
    }

    public HarmonicParametricScalar SetHarmonic(int harmonicFactor, double magnitude, double parameterShift)
    {
        var term = new HarmonicParametricScalarTerm(
            harmonicFactor,
            magnitude,
            parameterShift
        );

        //_adaptiveCurve = null;

        if (_harmonicTerms.ContainsKey(harmonicFactor))
            _harmonicTerms[harmonicFactor] = term;
        else
            _harmonicTerms.Add(harmonicFactor, term);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _harmonicTerms.Values.All(
            t => 
                t.Magnitude.IsValid() && 
                t.ParameterShift.IsValid()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetValue(double parameterValue)
    {
        parameterValue = parameterValue.ClampPeriodic(1d);

        return _harmonicTerms
            .Values
            .Select(t => t.GetPoint(parameterValue))
            .Aggregate(
                Float64Scalar.Zero, 
                (a, b) => a + b
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetDerivative1Value(double parameterValue)
    {
        parameterValue = parameterValue.ClampPeriodic(1d);

        //return new Tuple1D(
        //    Differentiate.FirstDerivative(t => GetPoint(t).X, parameterValue),
        //    Differentiate.FirstDerivative(t => GetPoint(t).Y, parameterValue),
        //    Differentiate.FirstDerivative(t => GetPoint(t).Z, parameterValue)
        //);

        return _harmonicTerms
            .Values
            .Select(t => t.GetTangent(parameterValue))
            .Aggregate(
                Float64Scalar.Zero, 
                (a, b) => a + b
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetDerivative2Value(double parameterValue)
    {
        parameterValue = parameterValue.ClampPeriodic(1d);

        //return new Tuple1D(
        //    Differentiate.SecondDerivative(t => GetPoint(t).X, parameterValue),
        //    Differentiate.SecondDerivative(t => GetPoint(t).Y, parameterValue),
        //    Differentiate.SecondDerivative(t => GetPoint(t).Z, parameterValue)
        //);

        return _harmonicTerms
            .Values
            .Select(t => t.GetSecondDerivative(parameterValue))
            .Aggregate(
                Float64Scalar.Zero, 
                (a, b) => a + b
            );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public double GetLength()
    //{
    //    if (_adaptiveCurve == null)
    //        UpdateSampling();

    //    return _adaptiveCurveLength;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public double ParameterToLength(double parameterValue)
    //{
    //    if (_adaptiveCurve == null)
    //        UpdateSampling();

    //    return _adaptiveCurve?.ParameterToLength(parameterValue) ?? 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public double LengthToParameter(double length)
    //{
    //    if (_adaptiveCurve == null)
    //        UpdateSampling();

    //    length = length.ClampPeriodic(_adaptiveCurveLength);

    //    return _adaptiveCurve?.LengthToParameter(length) ?? 0;
    //}
}