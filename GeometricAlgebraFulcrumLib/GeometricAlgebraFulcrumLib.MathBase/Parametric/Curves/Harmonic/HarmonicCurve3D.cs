using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Sampled;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Frames;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Harmonic
{
    public class HarmonicCurve3D :
        IParametricC2Curve3D,
        IArcLengthC1Curve3D
    {
        private SampledParametricCurve3D _sampledCurve;
        private double _sampledCurveLength;
        private readonly Dictionary<int, HarmonicCurveTerm3D> _harmonicTerms
            = new Dictionary<int, HarmonicCurveTerm3D>();


        public double ParameterValueMin
            => 0d;

        public double ParameterValueMax
            => 1d;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public HarmonicCurve3D UpdateSampling()
        {
            return UpdateSampling(
                new SampledParametricCurveTreeOptions3D(
                    5.DegreesToAngle(),
                    3,
                    16
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public HarmonicCurve3D UpdateSampling(SampledParametricCurveTreeOptions3D samplingOptions)
        {
            _sampledCurve = this.CreateSampledCurve3D(samplingOptions);
            _sampledCurveLength = _sampledCurve.GetLength();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public HarmonicCurve3D Clear()
        {
            _sampledCurve = null;
            _harmonicTerms.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public HarmonicCurve3D RemoveHarmonic(int harmonicFactor)
        {
            _harmonicTerms.Remove(harmonicFactor);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public HarmonicCurve3D SetHarmonic(int harmonicFactor, double magnitudeX, double magnitudeY, double magnitudeZ)
        {
            var term = new HarmonicCurveTerm3D(
                harmonicFactor,
                new Float64Tuple3D(magnitudeX, magnitudeY, magnitudeZ)
            );

            _sampledCurve = null;

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
                t => t.MagnitudeVector.IsValid()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetPoint(double parameterValue)
        {
            parameterValue = parameterValue.ClampPeriodic(1d);

            return _harmonicTerms
                .Values
                .Select(t => t.GetPoint(parameterValue))
                .Aggregate(Float64Tuple3D.Zero, (a, b) => a + b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetDerivative1Point(double parameterValue)
        {
            parameterValue = parameterValue.ClampPeriodic(1d);

            //return new Tuple3D(
            //    Differentiate.FirstDerivative(t => GetPoint(t).X, parameterValue),
            //    Differentiate.FirstDerivative(t => GetPoint(t).Y, parameterValue),
            //    Differentiate.FirstDerivative(t => GetPoint(t).Z, parameterValue)
            //);

            return _harmonicTerms
                .Values
                .Select(t => t.GetTangent(parameterValue))
                .Aggregate(Float64Tuple3D.Zero, (a, b) => a + b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            return this.GetFrenetSerretFrame(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetDerivative2Point(double parameterValue)
        {
            parameterValue = parameterValue.ClampPeriodic(1d);

            //return new Tuple3D(
            //    Differentiate.SecondDerivative(t => GetPoint(t).X, parameterValue),
            //    Differentiate.SecondDerivative(t => GetPoint(t).Y, parameterValue),
            //    Differentiate.SecondDerivative(t => GetPoint(t).Z, parameterValue)
            //);

            return _harmonicTerms
                .Values
                .Select(t => t.GetSecondDerivative(parameterValue))
                .Aggregate(Float64Tuple3D.Zero, (a, b) => a + b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetLength()
        {
            if (_sampledCurve == null)
                UpdateSampling();

            return _sampledCurveLength;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ParameterToLength(double parameterValue)
        {
            if (_sampledCurve == null)
                UpdateSampling();

            return _sampledCurve.ParameterToLength(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double LengthToParameter(double length)
        {
            if (_sampledCurve == null)
                UpdateSampling();

            length = length.ClampPeriodic(_sampledCurveLength);

            return _sampledCurve.LengthToParameter(length);
        }
    }
}
