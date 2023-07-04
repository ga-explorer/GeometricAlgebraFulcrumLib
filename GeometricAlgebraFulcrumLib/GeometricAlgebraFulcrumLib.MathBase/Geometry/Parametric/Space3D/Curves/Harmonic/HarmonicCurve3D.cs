using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.Adaptive;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Frames;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.Harmonic
{
    public class HarmonicCurve3D :
        IParametricC2Curve3D,
        IArcLengthCurve3D
    {
        private AdaptiveCurve3D _adaptiveCurve;
        private double _adaptiveCurveLength;
        private readonly Dictionary<int, HarmonicCurveTerm3D> _harmonicTerms
            = new Dictionary<int, HarmonicCurveTerm3D>();


        public Float64Range1D ParameterRange
            => Float64Range1D.ZeroToOne;
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public HarmonicCurve3D UpdateSampling()
        {
            return UpdateSampling(
                new AdaptiveCurveSamplingOptions3D(
                    5.DegreesToAngle(),
                    3,
                    16
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public HarmonicCurve3D UpdateSampling(AdaptiveCurveSamplingOptions3D samplingOptions)
        {
            _adaptiveCurve = this.CreateAdaptiveCurve3D(samplingOptions);
            _adaptiveCurveLength = _adaptiveCurve.GetLength();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public HarmonicCurve3D Clear()
        {
            _adaptiveCurve = null;
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
                Float64Vector3D.Create(magnitudeX, magnitudeY, magnitudeZ)
            );

            _adaptiveCurve = null;

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
        public Float64Vector3D GetPoint(double parameterValue)
        {
            parameterValue = parameterValue.ClampPeriodic(1d);

            return _harmonicTerms
                .Values
                .Select(t => t.GetPoint(parameterValue))
                .Aggregate(Float64Vector3D.Zero, (a, b) => a + b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetDerivative1Point(double parameterValue)
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
                .Aggregate(Float64Vector3D.Zero, (a, b) => a + b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            return this.GetFrenetSerretFrame(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetDerivative2Point(double parameterValue)
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
                .Aggregate(Float64Vector3D.Zero, (a, b) => a + b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetLength()
        {
            if (_adaptiveCurve == null)
                UpdateSampling();

            return _adaptiveCurveLength;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ParameterToLength(double parameterValue)
        {
            if (_adaptiveCurve == null)
                UpdateSampling();

            return _adaptiveCurve.ParameterToLength(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double LengthToParameter(double length)
        {
            if (_adaptiveCurve == null)
                UpdateSampling();

            length = length.ClampPeriodic(_adaptiveCurveLength);

            return _adaptiveCurve.LengthToParameter(length);
        }
    }
}
