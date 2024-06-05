//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Scalars;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Tuples.Immutable;
//using GeometricAlgebraFulcrumLib.Algebra.Parametric.Space2D.Curves.Sampled;
//using GeometricAlgebraFulcrumLib.Algebra.Parametric.Space2D.Frames;

//namespace GeometricAlgebraFulcrumLib.Algebra.Parametric.Space2D.Curves.Harmonic
//{
//    public class HarmonicCurve2D :
//        IParametricC2Curve2D,
//        IArcLengthC1Curve2D
//    {
//        private SampledParametricCurve2D _sampledCurve;
//        private double _sampledCurveLength;
//        private readonly Dictionary<int, HarmonicCurveTerm2D> _harmonicTerms
//            = new Dictionary<int, HarmonicCurveTerm2D>();


//        public double ParameterValueMin
//            => 0d;

//        public double ParameterValueMax
//            => 1d;


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public HarmonicCurve2D UpdateSampling()
//        {
//            return UpdateSampling(
//                new AdaptiveCurveSamplingOptions2D(
//                    5.DegreesToAngle(),
//                    3,
//                    16
//                )
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public HarmonicCurve2D UpdateSampling(AdaptiveCurveSamplingOptions2D samplingOptions)
//        {
//            _sampledCurve = this.CreateAdaptiveCurve2D(samplingOptions);
//            _sampledCurveLength = _sampledCurve.GetLength();

//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public HarmonicCurve2D Clear()
//        {
//            _sampledCurve = null;
//            _harmonicTerms.Clear();

//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public HarmonicCurve2D RemoveHarmonic(int harmonicFactor)
//        {
//            _harmonicTerms.Remove(harmonicFactor);

//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public HarmonicCurve2D SetHarmonic(int harmonicFactor, double magnitudeX, double magnitudeY, double magnitudeZ)
//        {
//            var term = new HarmonicCurveTerm2D(
//                harmonicFactor,
//                new Float64Tuple2D(magnitudeX, magnitudeY)
//            );

//            _sampledCurve = null;

//            if (_harmonicTerms.ContainsKey(harmonicFactor))
//                _harmonicTerms[harmonicFactor] = term;
//            else
//                _harmonicTerms.Add(harmonicFactor, term);

//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public bool IsValid()
//        {
//            return _harmonicTerms.Values.All(
//                t => t.MagnitudeVector.IsValid()
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public Float64Tuple2D GetPoint(double parameterValue)
//        {
//            parameterValue = parameterValue.ClampPeriodic(1d);

//            return _harmonicTerms
//                .Values
//                .Select(t => t.GetPoint(parameterValue))
//                .Aggregate(Float64Tuple2D.Zero, (a, b) => a + b);
//        }

//        public Float64Tuple2D GetTangent(double parameterValue)
//        {
//            throw new NotImplementedException();
//        }

//        public Float64Tuple2D GetUnitTangent(double parameterValue)
//        {
//            throw new NotImplementedException();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public Float64Tuple2D GetDerivative1Point(double parameterValue)
//        {
//            parameterValue = parameterValue.ClampPeriodic(1d);

//            //return new Tuple2D(
//            //    Differentiate.FirstDerivative(t => GetPoint(t).X, parameterValue),
//            //    Differentiate.FirstDerivative(t => GetPoint(t).Y, parameterValue),
//            //    Differentiate.FirstDerivative(t => GetPoint(t).Z, parameterValue)
//            //);

//            return _harmonicTerms
//                .Values
//                .Select(t => t.GetTangent(parameterValue))
//                .Aggregate(Float64Tuple2D.Zero, (a, b) => a + b);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public ParametricCurveLocalFrame2D GetFrame(double parameterValue)
//        {
//            return this.GetFrenetSerretFrame(parameterValue);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public Float64Tuple2D GetDerivative2Point(double parameterValue)
//        {
//            parameterValue = parameterValue.ClampPeriodic(1d);

//            //return new Tuple2D(
//            //    Differentiate.SecondDerivative(t => GetPoint(t).X, parameterValue),
//            //    Differentiate.SecondDerivative(t => GetPoint(t).Y, parameterValue),
//            //    Differentiate.SecondDerivative(t => GetPoint(t).Z, parameterValue)
//            //);

//            return _harmonicTerms
//                .Values
//                .Select(t => t.GetSecondDerivative(parameterValue))
//                .Aggregate(Float64Tuple2D.Zero, (a, b) => a + b);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public double GetLength()
//        {
//            if (_sampledCurve == null)
//                UpdateSampling();

//            return _sampledCurveLength;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public double ParameterToLength(double parameterValue)
//        {
//            if (_sampledCurve == null)
//                UpdateSampling();

//            return _sampledCurve.ParameterToLength(parameterValue);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public double LengthToParameter(double length)
//        {
//            if (_sampledCurve == null)
//                UpdateSampling();

//            length = length.ClampPeriodic(_sampledCurveLength);

//            return _sampledCurve.LengthToParameter(length);
//        }
//    }
//}
