using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.BSpline
{
    /// <summary>
    /// 2D B-spline curve with optimized span-local evaluation and factories for interpolation and least-squares fitting.
    /// </summary>
    public sealed class BSplineCurve2D : BSplineCurveBase<LinFloat64Vector2D>
    {
        public sealed class SamplingOptions :
            Float64AdaptiveSamplingOptions
        {
            public Float64IntegrationMode IntegrationMode { get; set; }
                = Float64IntegrationMode.GaussLegendre7;

            public int MaxSimpsonRecursionDepth { get; set; } = 15;
        }


        public static BSplineCurve2D Interpolation(IReadOnlyList<LinFloat64Vector2D> dataPoints, int degree = 3, bool useCentripetal = true)
        {
            if (dataPoints == null) throw new ArgumentNullException(nameof(dataPoints));
            if (dataPoints.Count < 2) throw new ArgumentException("At least two points required.");
            if (degree < 1) throw new ArgumentOutOfRangeException(nameof(degree));

            // Parameterization
            var parameters = useCentripetal 
                ? CreateCentripetalParameters(dataPoints, (p1, p2) => 
                {
                    var dx = p2.X - p1.X;
                    var dy = p2.Y - p1.Y;
                    return dx * dx + dy * dy;
                })
                : CreateUniformParameters(dataPoints.Count);

            // Solve interpolation system
            var (controlPoints, basisSet) = SolveInterpolationSystem(
                dataPoints,
                parameters,
                degree,
                p => new[] { p.X, p.Y },
                comp => LinFloat64Vector2D.Create(comp[0], comp[1]));

            return new BSplineCurve2D(basisSet, controlPoints);
        }

        public static BSplineCurve2D Regression(IReadOnlyList<LinFloat64Vector2D> dataPoints, int degree = 3, int controlPointCount = -1, bool clampEndpoints = true, bool useCentripetal = true)
        {
            if (dataPoints == null) throw new ArgumentNullException(nameof(dataPoints));
            if (dataPoints.Count < 2) throw new ArgumentException("At least two points required.");
            if (degree < 1) throw new ArgumentOutOfRangeException(nameof(degree));

            var actualControlPointCount = controlPointCount > 0 ? controlPointCount : 
                Math.Max(degree + 1, dataPoints.Count / 2 + 1);
            
            if (actualControlPointCount <= degree)
                throw new ArgumentException("Control point count must be greater than degree.");

            // Parameterization
            var parameters = useCentripetal
                ? CreateCentripetalParameters(dataPoints, (p1, p2) =>
                {
                    var dx = p2.X - p1.X;
                    var dy = p2.Y - p1.Y;
                    return dx * dx + dy * dy;
                })
                : CreateUniformParameters(dataPoints.Count);

            // Solve least squares system
            var (controlPoints, basisSet) = SolveLeastSquaresSystem(
                dataPoints,
                parameters,
                degree,
                actualControlPointCount,
                p => new[] { p.X, p.Y },
                comp => LinFloat64Vector2D.Create(comp[0], comp[1]),
                clampEndpoints);

            return new BSplineCurve2D(basisSet, controlPoints);
        }

        private static double[] CreateUniformParameters(int pointCount)
        {
            var parameters = new double[pointCount];
            for (var i = 0; i < pointCount; i++)
            {
                parameters[i] = (double)i / (pointCount - 1);
            }
            return parameters;
        }



        internal BSplineCurve2D(BSplineBasisSet basisSet, IReadOnlyList<LinFloat64Vector2D> controlPoints)
            : base(basisSet, controlPoints)
        {
        }

        #region Vector Operations Implementation

        protected override LinFloat64Vector2D CreateZero() => LinFloat64Vector2D.Zero;

        protected override LinFloat64Vector2D CreateFromComponents(double[] components)
        {
            if (components.Length != 2)
                throw new ArgumentException("2D vector requires exactly 2 components");
            return LinFloat64Vector2D.Create(components[0], components[1]);
        }

        protected override LinFloat64Vector2D AddVectors(LinFloat64Vector2D a, LinFloat64Vector2D b)
            => LinFloat64Vector2D.Create(a.X + b.X, a.Y + b.Y);

        protected override LinFloat64Vector2D ScaleVector(LinFloat64Vector2D vector, double scalar)
            => LinFloat64Vector2D.Create(vector.X * scalar, vector.Y * scalar);

        protected override double[] GetVectorComponents(LinFloat64Vector2D vector)
            => new[] { vector.X, vector.Y };

        #endregion

        #region Curve Evaluation

        public LinFloat64Vector2D GetPoint(double t) 
            => Evaluate(t, 0);

        public LinFloat64Vector2D GetDerivative1Value(double t) 
            => Evaluate(t, 1);

        public LinFloat64Vector2D GetDerivative2Value(double t) 
            => Evaluate(t, 2);

        public LinFloat64Vector2D GetUnitTangentVector(double t)
        {
            var derivative = GetDerivative1Value(t);
            var norm = derivative.L2Norm();
            return norm <= Epsilon 
                ? LinFloat64Vector2D.Zero 
                : LinFloat64Vector2D.Create(derivative.X / norm, derivative.Y / norm);
        }

        public double GetCurvature(double t)
        {
            var d1 = GetDerivative1Value(t);
            var d2 = GetDerivative2Value(t);
            var numerator = d1.X * d2.Y - d1.Y * d2.X;
            var denominator = Math.Pow(d1.L2Norm(), 3.0);
            return denominator <= Epsilon ? 0.0 : numerator / denominator;
        }

        #endregion

        #region Advanced Operations

        public double GetArcLength(double t0, double t1, int samplingPoints = 1000)
        {
            if (t1 <= t0) return 0.0;
            if (samplingPoints < 2) throw new ArgumentOutOfRangeException(nameof(samplingPoints));

            var length = 0.0;
            var previousPoint = GetPoint(t0);
            
            for (var i = 1; i <= samplingPoints; i++)
            {
                var t = t0 + (t1 - t0) * i / samplingPoints;
                var currentPoint = GetPoint(t);
                length += (currentPoint - previousPoint).L2Norm();
                previousPoint = currentPoint;
            }

            return length;
        }

        public LinFloat64Vector2D GetPointAtArcLength(double arcLength, double tolerance = 1e-6)
        {
            if (arcLength < 0) throw new ArgumentOutOfRangeException(nameof(arcLength));
            
            var totalLength = GetArcLength(0, 1);
            if (arcLength >= totalLength) return GetPoint(1.0);

            // Use bisection to find parameter corresponding to arc length
            var tLow = 0.0;
            var tHigh = 1.0;
            
            for (var iter = 0; iter < 100; iter++)
            {
                var tMid = (tLow + tHigh) / 2;
                var midLength = GetArcLength(0, tMid);
                
                if (Math.Abs(midLength - arcLength) < tolerance)
                    return GetPoint(tMid);
                
                if (midLength < arcLength)
                    tLow = tMid;
                else
                    tHigh = tMid;
            }

            return GetPoint((tLow + tHigh) / 2);
        }

        #endregion


        
        /// <summary>
        /// Adaptive sampling
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public IReadOnlyList<double> GetAdaptiveTimeSamples(SamplingOptions? options = null)
        {
            var timeRange = BasisSet.KnotVector.GetParameterRange(BasisSet.Degree);

            var parameters = new SortedSet<double>
            {
                timeRange.MinValue,
                timeRange.MaxValue
            };

            options ??= new SamplingOptions();

            SubdivideRecursive(options, 0, timeRange.MinValue, timeRange.MaxValue, parameters);

            return parameters.ToArray();
        }

        private void SubdivideRecursive(SamplingOptions options, int level, double t0, double t1, SortedSet<double> parameters)
        {
            var p0 = GetPoint(t0);
            var p1 = GetPoint(t1);

            var chord = (p1 - p0).L2Norm();
            var segLength = GetArcLength(t0, t1);

            var u0 = GetUnitTangentVector(t0);
            var u1 = GetUnitTangentVector(t1);

            var dot = Math.Clamp(u0.X * u1.X + u0.Y * u1.Y, -1.0, 1.0);
            var angle = Math.Acos(dot);

            var tooSmallParam = Math.Abs(t1 - t0) <= options.ParameterSpan;
            var levelReached = level >= options.MaxDepth;
            var minLevelNeeded = level < options.MinDepth;
            var arcChordGood = chord < 1e-14 || (segLength - chord) / chord <= options.RelativeChordError;
            var angleGood = angle <= options.FrameAngleDifference.RadiansValue;

            if (minLevelNeeded || !levelReached && !(arcChordGood && angleGood) && !tooSmallParam)
            {
                var tm = 0.5 * (t0 + t1);
                var pm = GetPoint(tm);

                var leftChord = (pm - p0).L2Norm();
                var rightChord = (p1 - pm).L2Norm();
                var totalChildChord = leftChord + rightChord;
                var improvement = totalChildChord - chord;
                var relativeImprovement = chord > 1e-14 ? improvement / chord : 0.0;

                if (relativeImprovement < options.RelativeChordImprovement)
                {
                    parameters.Add(t0);
                    parameters.Add(t1);
                    return;
                }

                SubdivideRecursive(options, level + 1, t0, tm, parameters);
                SubdivideRecursive(options, level + 1, tm, t1, parameters);
                return;
            }

            parameters.Add(t0);
            parameters.Add(t1);
        }
        
    }
}