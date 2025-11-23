using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space1D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Mapped;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64
{
    public abstract class Float64Path2D(Float64ScalarRange timeRange, bool isPeriodic) :
        Float64Trajectory<LinFloat64Vector2D>(timeRange, isPeriodic)
    {
        public sealed class SamplingOptions :
            Float64AdaptiveSamplingOptions
        {
            public Float64IntegrationMode IntegrationMode { get; set; }
                = Float64IntegrationMode.GaussLegendre7;

            public int MaxSimpsonRecursionDepth { get; set; } = 15;
        }


        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IFloat64Trajectory ToFinite()
        {
            return ToFinitePath();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IFloat64Trajectory ToPeriodic()
        {
            return ToPeriodicPath();
        }

        public abstract Float64Path2D ToFinitePath();

        public abstract Float64Path2D ToPeriodicPath();


        public virtual Pair<Float64ScalarSignal> GetScalarComponents()
        {
            if (IsFinite)
                return new Pair<Float64ScalarSignal>(
                    Float64ScalarSignal.FiniteComputed(
                        TimeRange,
                        t => GetValue(t).Item1,
                        t => GetDerivative1Value(t).Item1,
                        t => GetDerivative2Value(t).Item1
                    ),

                    Float64ScalarSignal.FiniteComputed(
                        TimeRange,
                        t => GetValue(t).Item2,
                        t => GetDerivative1Value(t).Item2,
                        t => GetDerivative2Value(t).Item2
                    )
                );

            return new Pair<Float64ScalarSignal>(
                Float64ScalarSignal.PeriodicComputed(
                    TimeRange,
                    t => GetValue(t).Item1,
                    t => GetDerivative1Value(t).Item1,
                    t => GetDerivative2Value(t).Item1
                ),

                Float64ScalarSignal.PeriodicComputed(
                    TimeRange,
                    t => GetValue(t).Item2,
                    t => GetDerivative1Value(t).Item2,
                    t => GetDerivative2Value(t).Item2
                )
            );
        }


        private Pair<Float64ScalarRange>? _valueRange;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual Pair<Float64ScalarRange> FindValueRange()
        {
            var (s1, s2) =
                GetScalarComponents();

            return new Pair<Float64ScalarRange>(
                s1.GetValueRange(),
                s2.GetValueRange()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual Pair<Float64ScalarRange> FindValueRange(double minTime, double maxTime)
        {
            var (s1, s2) =
                GetScalarComponents();

            return new Pair<Float64ScalarRange>(
                s1.FindValueRange(minTime, maxTime),
                s2.FindValueRange(minTime, maxTime)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pair<Float64ScalarRange> GetValueRange()
        {
            _valueRange ??= FindValueRange();

            return _valueRange;
        }


        public LinFloat64Vector2D GetDerivative1ValueNumerical(double t)
        {
            if (IsFinite && !TimeRange.Contains(t))
                return LinFloat64Vector2D.Zero;

            var epsilon = Math.Pow(2, -39); // Near 1.82e-12

            t = this.ClampTime(t);

            if (t < MinTime + 16 * epsilon)
                return (GetValue(t + epsilon) - GetValue(t)) / epsilon;

            if (t > MaxTime - 16 * epsilon)
                return (GetValue(t) - GetValue(t - epsilon)) / epsilon;

            var x = MathNet.Numerics.Differentiate.Derivative(
                t1 => GetValue(t1).Item1, t, 1
            );

            var y = MathNet.Numerics.Differentiate.Derivative(
                t1 => GetValue(t1).Item2, t, 1
            );

            return LinFloat64Vector2D.Create(x, y);
        }

        public LinFloat64Vector2D GetDerivative2ValueNumerical(double t)
        {
            if (IsFinite && !TimeRange.Contains(t))
                return LinFloat64Vector2D.Zero;

            var epsilon = Math.Pow(2, -39); // Near 1.82e-12

            t = this.ClampTime(t);

            if (t < MinTime + 16 * epsilon)
                return (GetDerivative1Value(t + epsilon) - GetDerivative1Value(t)) / epsilon;

            if (t > MaxTime - 16 * epsilon)
                return (GetDerivative1Value(t) - GetDerivative1Value(t - epsilon)) / epsilon;

            var x = MathNet.Numerics.Differentiate.Derivative(
                t1 => GetDerivative1Value(t1).Item1, t, 1
            );

            var y = MathNet.Numerics.Differentiate.Derivative(
                t1 => GetDerivative1Value(t1).Item2, t, 1
            );

            return LinFloat64Vector2D.Create(x, y);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual LinFloat64Vector2D GetDerivative1Value(double t)
        {
            return GetDerivative1ValueNumerical(t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual LinFloat64Vector2D GetDerivative2Value(double t)
        {
            return GetDerivative2ValueNumerical(t);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D GetDerivative1ErrorSignal()
        {
            return Float64ComputedPath2D.Finite(
                TimeRange,
                t => GetDerivative1Value(t) - GetDerivative1ValueNumerical(t)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D GetDerivative2ErrorSignal()
        {
            return Float64ComputedPath2D.Finite(
                TimeRange,
                t => GetDerivative2Value(t) - GetDerivative2ValueNumerical(t)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64AffineMappedTimePath2D GetMappedParameterCurve(Float64AffineMap1D timeMap)
        {
            return Float64AffineMappedTimePath2D.Create(this, timeMap);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector2D GetPoint(double t)
        {
            return GetValue(t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector2D GetTangentVector(double t)
        {
            return GetDerivative1Value(t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector2D GetUnitTangentVector(double t)
        {
            return GetDerivative1Value(t).ToUnitLinVector2D();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector2D GetAccelerationVector(double t)
        {
            return GetDerivative2Value(t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual double GetArcSpeed(double t)
        {
            return GetDerivative1Value(t).L2Norm();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual Float64Path2DLocalFrame GetFrame(double t)
        {
            return Float64Path2DLocalFrame.Create(
                t,
                GetValue(t),
                GetDerivative1Value(t)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual double GetArcLength(double t)
        {
            return GetArcLength(MinTime, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual Float64Scalar GetArcLength(double a, double b)
        {
            return GetArcLengthNumerical(a, b);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<Float64Path2DLocalFrame> GetFrames(IEnumerable<double> parameterValueList)
        {
            return parameterValueList.Select(
                t =>
                    Float64Path2DLocalFrame.Create(
                        t,
                        GetValue(t),
                        GetDerivative1Value(t)
                    )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<LinFloat64Vector2D> GetPoints(IEnumerable<double> tList)
        {
            return tList.Select(GetValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector2D[] GetPoints(params double[] tList)
        {
            return tList.Select(GetValue).ToArray();
        }


        public Float64Scalar TimeToLengthNumerical(double t)
        {
            return GetArcLengthNumerical(MinTime, t);
        }

        /// <summary>
        /// Length to time conversion (Newton with partial-arc cache) 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Float64Scalar LengthToTimeNumerical(double s)
        {
            // Compute total arc length
            var totalLength =
                TimeToLengthNumerical(MaxTime).ScalarValue;

            if (s <= 0)
                return MinTime;

            if (s >= totalLength)
                return MaxTime;

            // Initial guess (linear mapping)
            var t = MinTime + (s / totalLength) * (MaxTime - MinTime);

            const int maxIterations = 50;
            const double tolerance = 1e-9;

            // --- Try Newton-Raphson ---
            for (var i = 0; i < maxIterations; i++)
            {
                var lengthAtT = TimeToLengthNumerical(t).ScalarValue;
                var f = lengthAtT - s;

                if (Math.Abs(f) < tolerance)
                    return t;

                var speed = GetArcSpeed(t);
                if (speed <= 1e-14)
                    break; // Can't divide by near-zero slope

                var delta = f / speed;
                t -= delta;

                // Clamp inside valid domain
                if (t < MinTime) t = MinTime;
                if (t > MaxTime) t = MaxTime;

                if (Math.Abs(delta) < tolerance)
                    return t;
            }

            // --- Fallback: Bisection method ---
            double low = MinTime, high = MaxTime;
            while (high - low > tolerance)
            {
                var mid = 0.5 * (low + high);
                var lengthAtMid = TimeToLengthNumerical(mid).ScalarValue;

                if (lengthAtMid < s)
                    low = mid;
                else
                    high = mid;
            }

            return 0.5 * (low + high);
        }

        public Float64Scalar GetArcLengthNumerical(double a, double b, Float64ArcLengthNumericalOptions? options = null)
        {
            if (b <= a) return 0.0;

            options ??= new Float64ArcLengthNumericalOptions();

            return options.IntegrationMode switch
            {
                Float64IntegrationMode.GaussLegendre5 => GaussLegendreIntegrate(a, b, 5),
                Float64IntegrationMode.GaussLegendre7 => GaussLegendreIntegrate(a, b, 7),
                _ => ComputeArcLengthAdaptiveSimpson(a, b, options.RelativeChordError, options.MaxSimpsonRecursionDepth)
            };
        }

        /// <summary>
        /// Integration (adaptive Simpson + Gauss-Legendre) 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="fa"></param>
        /// <param name="fm"></param>
        /// <param name="fb"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double SimpsonEstimate(double a, double b, double fa, double fm, double fb)
        {
            return (b - a) * (fa + 4.0 * fm + fb) / 6.0;
        }

        private double ComputeArcLengthAdaptiveSimpson(double a, double b, double tol, int maxDepth)
        {
            var fa = GetArcSpeed(a);
            var fb = GetArcSpeed(b);
            var m = 0.5 * (a + b);
            var fm = GetArcSpeed(m);
            var s = SimpsonEstimate(a, b, fa, fm, fb);

            return AdaptiveSimpsonRecursive(a, b, fa, fm, fb, s, tol, maxDepth);
        }

        private double AdaptiveSimpsonRecursive(double a, double b, double fa, double fm, double fb, double s, double tol, int depth)
        {
            var m = 0.5 * (a + b);
            var lm = 0.5 * (a + m);
            var rm = 0.5 * (m + b);

            var flm = GetArcSpeed(lm);
            var frm = GetArcSpeed(rm);

            var sLeft = SimpsonEstimate(a, m, fa, flm, fm);
            var sRight = SimpsonEstimate(m, b, fm, frm, fb);
            var s2 = sLeft + sRight;

            var err = Math.Abs(s2 - s);
            if (err <= 15.0 * tol || depth <= 0)
                return s2 + (s2 - s) / 15.0;

            var left = AdaptiveSimpsonRecursive(a, m, fa, flm, fm, sLeft, tol * 0.5, depth - 1);
            var right = AdaptiveSimpsonRecursive(m, b, fm, frm, fb, sRight, tol * 0.5, depth - 1);

            return left + right;
        }

        private double GaussLegendreIntegrate(double a, double b, int order)
        {
            var c1 = 0.5 * (b - a);
            var c2 = 0.5 * (b + a);

            var sum = 0.0;
            if (order == 5)
            {
                double[] x = [0.0, -0.5384693101056831, 0.5384693101056831, -0.9061798459386640, 0.9061798459386640];
                double[] w = [0.5688888888888889, 0.4786286704993665, 0.4786286704993665, 0.2369268850561891, 0.2369268850561891];

                for (var i = 0; i < 5; i++)
                {
                    var t = c1 * x[i] + c2;
                    sum += w[i] * GetArcSpeed(t);
                }
            }
            else // 7
            {
                double[] x = [0.0, -0.4058451513773972, 0.4058451513773972, -0.7415311855993945, 0.7415311855993945, -0.9491079123427585, 0.9491079123427585];
                double[] w = [0.4179591836734694, 0.3818300505051189, 0.3818300505051189, 0.2797053914892766, 0.2797053914892766, 0.1294849661688697, 0.1294849661688697];

                for (var i = 0; i < 7; i++)
                {
                    var t = c1 * x[i] + c2;
                    sum += w[i] * GetArcSpeed(t);
                }
            }

            return c1 * sum;
        }


        /// <summary>
        /// https://en.wikipedia.org/wiki/Frenet%E2%80%93Serret_formulas
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Float64Path2DLocalFrame GetFrenetSerretFrame(double t)
        {
            var origin = GetValue(t);

            var vDt1 = GetDerivative1Value(t);
            var sDt1 = LinFloat64Vector2DUtils.L2Norm(vDt1);
            var vDs1 = vDt1 / sDt1;

            return Float64Path2DLocalFrame.Create(
                t,
                origin,
                vDs1
            );
        }

        /// <summary>
        /// https://en.wikipedia.org/wiki/Frenet%E2%80%93Serret_formulas
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public LinFloat64AffineFrame2D GetFrenetSerretAffineFrame(double t)
        {
            var origin = GetValue(t);

            var vDt1 = GetDerivative1Value(t);
            var vDt2 = GetDerivative2Value(t);

            var sDt1 = LinFloat64Vector2DUtils.L2Norm(vDt1);
            var sDt2 = vDt1.VectorDot(vDt2) / sDt1;

            var vDs1 = vDt1 / sDt1;
            var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();

            var e1 = vDs1;
            var e2 = (vDs2 - vDs2.ProjectOnUnitVector(vDs1)).ToUnitLinVector2D();

            return LinFloat64AffineFrame2D.Create(origin, e1, e2);
        }


        /// <summary>
        /// Adaptive sampling
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public IReadOnlyList<double> GetAdaptiveTimeSamples(SamplingOptions? options = null)
        {
            var parameters = new SortedSet<double>
            {
                TimeRange.MinValue,
                TimeRange.MaxValue
            };

            options ??= new SamplingOptions();

            SubdivideRecursive(options, 0, TimeRange.MinValue, TimeRange.MaxValue, parameters);

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
        
        public IReadOnlyList<double> GetUniformTimeSamples(int sampleCount)
        {
            return MinTime.GetLinearRange(
                MaxTime, 
                sampleCount, 
                IsPeriodic
            ).ToArray();
        }

        // -------------------------------
        // Uniform arc-length sampling
        // -------------------------------
        public virtual IReadOnlyList<double> GetUniformArcLengthTimeSamples(int sampleCount)
        {
            if (sampleCount < 2)
                throw new ArgumentOutOfRangeException(nameof(sampleCount), "Sample count must be >= 2");

            var parameters = new List<double>(sampleCount);

            double totalLength = GetArcLength(TimeRange.MinValue, TimeRange.MaxValue);
            var ds = totalLength / (sampleCount - 1);

            parameters.Add(TimeRange.MinValue);

            for (var i = 1; i < sampleCount - 1; i++)
            {
                var targetLength = i * ds;

                // Binary search inversion of arc length
                var tLow = TimeRange.MinValue;
                var tHigh = TimeRange.MaxValue;
                var tMid = tLow;

                for (var iter = 0; iter < 40; iter++) // ~1e-12 precision
                {
                    tMid = 0.5 * (tLow + tHigh);
                    double len = GetArcLength(TimeRange.MinValue, tMid);

                    if (len < targetLength)
                        tLow = tMid;
                    else
                        tHigh = tMid;
                }

                parameters.Add(tMid);
            }

            parameters.Add(TimeRange.MaxValue);

            return parameters;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D NegativeTime()
        {
            var timeMap = Float64AffineMap1D.CreateScale((Float64Scalar)(-1));

            return MapTimeUsing(timeMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D ScaleTimeBy(double timeScaling)
        {
            var timeMap = Float64AffineMap1D.CreateScale((Float64Scalar)timeScaling);

            return MapTimeUsing(timeMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D ScaleTimeUsing(double inTime, double outTime)
        {
            return ScaleTimeBy(outTime / inTime);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D OffsetTimeBy(double timeOffset)
        {
            if (timeOffset.IsZero())
                return this;

            return MapTimeUsing(
                Float64AffineMap1D.Create(
                    Float64Scalar.One,
                    timeOffset
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D OffsetTimeUsing(double inTime, double outTime)
        {
            return OffsetTimeBy(outTime - inTime);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D OffsetTimeMinTo(double outTimeMin)
        {
            return OffsetTimeBy(outTimeMin - MinTime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D OffsetTimeMaxTo(double outTimeMax)
        {
            return OffsetTimeBy(outTimeMax - MaxTime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D OffsetTimeMidTo(double outTimeMid)
        {
            return OffsetTimeBy(outTimeMid - MidTime);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D MapTimeUsing(Float64AffineMap1D affineMap)
        {
            if (affineMap.IsIdentity())
                return this;

            if (this is Float64AffineMappedTimePath2D affineScalar)
                return Float64AffineMappedTimePath2D.Create(
                    affineScalar.BasePath,
                    affineMap * affineScalar.TimeMap
                );

            return Float64AffineMappedTimePath2D.Create(
                this,
                affineMap
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D MapTimeUsing(double inTime1, double inTime2, double outTime1, double outTime2)
        {
            var timeMap = Float64AffineMap1D.CreateFromRanges(inTime1, inTime2, outTime1, outTime2);

            return MapTimeUsing(timeMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D MapTimeRangeTo(Float64ScalarRange timeRange)
        {
            if (TimeRange.Equals(timeRange))
                return this;

            var (outTime1, outTime2) = timeRange;

            return MapTimeUsing(
                MinTime,
                MaxTime,
                outTime1,
                outTime2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D MapTimeRangeTo(double outTime1, double outTime2)
        {
            if (TimeRange.Equals(outTime1, outTime2))
                return this;

            return MapTimeUsing(
                MinTime,
                MaxTime,
                outTime1,
                outTime2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D FlipTimeRange()
        {
            return MapTimeUsing(
                MinTime,
                MaxTime,
                MaxTime,
                MinTime
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D FlipTimeRange(double time1, double time2)
        {
            return MapTimeUsing(
                time1,
                time2,
                time2,
                time1
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D Negative()
        {
            var valueMap = Float64InvertibleAffineMap2D.Create().ReflectOrigin();

            return MapUsing(valueMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D ScaleBy(LinFloat64Vector2D valueScaling)
        {
            var valueMap = Float64InvertibleAffineMap2D.Create().Scale(valueScaling);

            return MapUsing(valueMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D ScaleUsing(LinFloat64Vector2D inValue, LinFloat64Vector2D outValue)
        {
            return ScaleBy(
                LinFloat64Vector2D.Create(
                    outValue.Item1 / inValue.Item1,
                    outValue.Item2 / inValue.Item2
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D OffsetBy(LinFloat64Vector2D valueOffset)
        {
            var valueMap = Float64InvertibleAffineMap2D.Create().Translate(valueOffset);

            return MapUsing(valueMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D OffsetUsing(LinFloat64Vector2D inValue, LinFloat64Vector2D outValue)
        {
            return OffsetBy(outValue - inValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D OffsetMinTo(LinFloat64Vector2D outValueMin)
        {
            return OffsetBy(outValueMin - GetValueRange().GetMinVector2D());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D OffsetMaxTo(LinFloat64Vector2D outValueMax)
        {
            return OffsetBy(outValueMax - GetValueRange().GetMaxVector2D());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D OffsetMidTo(LinFloat64Vector2D outValueMid)
        {
            return OffsetBy(outValueMid - GetValueRange().GetMidVector2D());
        }


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public Float64Path2D MapValueUsing(Func<double, double> valueMap)
        //{
        //    if (this is Float64MappedScalarSignal mappedScalar)
        //        return Float64MappedScalarSignal.Create(mappedScalar.BaseSignal,
        //            t => valueMap(mappedScalar.ValueMap(t)));

        //    return Float64MappedScalarSignal.Create(this, valueMap);
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D MapUsing(IFloat64AffineMap2D affineMap)
        {
            if (affineMap.IsIdentity())
                return this;

            if (this is Float64AffineMappedPath2D affineScalar)
                return Float64AffineMappedPath2D.Create(
                    affineScalar.BasePath,
                    affineScalar.AffineMap.TransformUsing(affineMap)
                );

            return Float64AffineMappedPath2D.Create(
                this,
                affineMap
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D MapUsing(IPair<Float64ScalarRange> inValueRange, IPair<Float64ScalarRange> outValueRange)
        {
            var valueMap = Float64ScaleTranslateAffineMap2D.CreateFromRanges(
                inValueRange,
                outValueRange
            );

            return MapUsing(valueMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D MapUsing(LinFloat64Vector2D inValue1, LinFloat64Vector2D inValue2, LinFloat64Vector2D outValue1, LinFloat64Vector2D outValue2)
        {
            var valueMap = Float64ScaleTranslateAffineMap2D.CreateFromRanges(
                inValue1,
                inValue2,
                outValue1,
                outValue2
            );

            return MapUsing(valueMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D MapRangeTo(Pair<Float64ScalarRange> valueRange)
        {
            return MapUsing(
                GetValueRange(),
                valueRange
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D MapRangeTo(LinFloat64Vector2D outValue1, LinFloat64Vector2D outValue2)
        {
            var (inValue1, inValue2) =
                GetValueRange().GetMinMaxVector2D();

            return MapUsing(
                inValue1,
                inValue2,
                outValue1,
                outValue2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D FlipRange()
        {
            var (valueMin, valueMax) =
                GetValueRange().GetMinMaxVector2D();

            return MapUsing(
                valueMin,
                valueMax,
                valueMax,
                valueMin
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Path2D FlipRange(LinFloat64Vector2D value1, LinFloat64Vector2D value2)
        {
            return MapUsing(
                value1,
                value2,
                value2,
                value1
            );
        }

    }
}
