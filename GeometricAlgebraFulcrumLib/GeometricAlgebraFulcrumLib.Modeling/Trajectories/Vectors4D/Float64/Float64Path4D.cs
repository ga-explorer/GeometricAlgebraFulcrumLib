using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors4D.Float64.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors4D.Float64;

/// <summary>
/// A parametric 4D curve with continuous first derivative
/// </summary>
public abstract class Float64Path4D(Float64ScalarRange timeRange, bool isPeriodic) :
    Float64Trajectory<LinFloat64Vector4D>(timeRange, isPeriodic)
{
    public abstract Float64Path4D ToFinitePath();

    public abstract Float64Path4D ToPeriodicPath();
    
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
    

    public virtual Quad<Float64ScalarSignal> GetScalarComponents()
    {
        if (IsFinite)
            return new Quad<Float64ScalarSignal>(
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
                ),

                Float64ScalarSignal.FiniteComputed(
                    TimeRange,
                    t => GetValue(t).Item3,
                    t => GetDerivative1Value(t).Item3,
                    t => GetDerivative2Value(t).Item3
                ),

                Float64ScalarSignal.FiniteComputed(
                    TimeRange,
                    t => GetValue(t).Item4,
                    t => GetDerivative1Value(t).Item4,
                    t => GetDerivative2Value(t).Item4
                )
            );

        return new Quad<Float64ScalarSignal>(
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
            ),

            Float64ScalarSignal.PeriodicComputed(
                TimeRange,
                t => GetValue(t).Item3,
                t => GetDerivative1Value(t).Item3,
                t => GetDerivative2Value(t).Item3
            ),

            Float64ScalarSignal.PeriodicComputed(
                TimeRange,
                t => GetValue(t).Item4,
                t => GetDerivative1Value(t).Item4,
                t => GetDerivative2Value(t).Item4
            )
        );
    }


    private Quad<Float64ScalarRange>? _valueRange;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Quad<Float64ScalarRange> FindValueRange()
    {
        var (s1, s2, s3, s4) = 
            GetScalarComponents();

        return new Quad<Float64ScalarRange>(
            s1.GetValueRange(),
            s2.GetValueRange(),
            s3.GetValueRange(),
            s4.GetValueRange()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Quad<Float64ScalarRange> FindValueRange(double minTime, double maxTime)
    {
        var (s1, s2, s3, s4) = 
            GetScalarComponents();

        return new Quad<Float64ScalarRange>(
            s1.FindValueRange(minTime, maxTime),
            s2.FindValueRange(minTime, maxTime),
            s3.FindValueRange(minTime, maxTime),
            s4.FindValueRange(minTime, maxTime)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Quad<Float64ScalarRange> GetValueRange()
    {
        _valueRange ??= FindValueRange();

        return _valueRange;
    }


    public LinFloat64Vector4D GetDerivative1ValueNumerical(double t)
    {
        if (IsFinite && !TimeRange.Contains(t))
            return LinFloat64Vector4D.Zero;

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

        var z = MathNet.Numerics.Differentiate.Derivative(
            t1 => GetValue(t1).Item3, t, 1
        );
        
        var w = MathNet.Numerics.Differentiate.Derivative(
            t1 => GetValue(t1).Item4, t, 1
        );

        return LinFloat64Vector4D.Create(x, y, z, w);
    }

    public LinFloat64Vector4D GetDerivative2ValueNumerical(double t)
    {
        if (IsFinite && !TimeRange.Contains(t))
            return LinFloat64Vector4D.Zero;

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

        var z = MathNet.Numerics.Differentiate.Derivative(
            t1 => GetDerivative1Value(t1).Item3, t, 1
        );
        
        var w = MathNet.Numerics.Differentiate.Derivative(
            t1 => GetDerivative1Value(t1).Item4, t, 1
        );

        return LinFloat64Vector4D.Create(x, y, z, w);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual LinFloat64Vector4D GetDerivative1Value(double t)
    {
        return GetDerivative1ValueNumerical(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual LinFloat64Vector4D GetDerivative2Value(double t)
    {
        return GetDerivative2ValueNumerical(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Path4D GetDerivative1ErrorSignal()
    {
        return Float64ComputedPath4D.Finite(
            TimeRange,
            t => GetDerivative1Value(t) - GetDerivative1ValueNumerical(t)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Path4D GetDerivative2ErrorSignal()
    {
        return Float64ComputedPath4D.Finite(
            TimeRange,
            t => GetDerivative2Value(t) - GetDerivative2ValueNumerical(t)
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4D GetPoint(double t)
    {
        return GetValue(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4D GetTangentVector(double t)
    {
        return GetDerivative1Value(t);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4D GetUnitTangentVector(double t)
    {
        return GetDerivative1Value(t).DivideByENorm();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual double GetArcSpeed(double t)
    {
        return GetDerivative1Value(t).L2Norm();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4D GetAccelerationVector(double t)
    {
        return GetDerivative2Value(t);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public virtual Float64Path4DLocalFrame GetFrame(double t)
    //{
    //    var tangent = GetDerivative1Value(t);
    //    var scalingFactor = tangent.L2Norm();

    //    return Float64Path4DLocalFrame.Create(
    //        t,
    //        GetValue(t),
    //        scalingFactor,
    //        tangent / scalingFactor
    //    );
    //}
    
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
    public IEnumerable<LinFloat64Vector4D> GetPoints(IEnumerable<double> tList)
    {
        return tList.Select(GetValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4D[] GetPoints(params double[] tList)
    {
        return tList.Select(GetValue).ToArray();
    }

    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64RotatedNormalsPath4D RotateNormals(Func<double, LinFloat64Angle> angleFunction)
    //{
    //    return new Float64RotatedNormalsPath4D(
    //        this,
    //        Float64ScalarSignal.CreateComputed(
    //            TimeRange, 
    //            IsPeriodic,
    //            t => angleFunction(t).RadiansValue
    //        ).RadiansToPolarAngle()
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64RotatedNormalsPath4D RotateNormals(LinFloat64PolarAngleTimeSignal angleFunction)
    //{
    //    return new Float64RotatedNormalsPath4D(
    //        this,
    //        angleFunction
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64RotatedNormalsPath4D RotateNormals(LinFloat64PolarAngle angle)
    //{
    //    return new Float64RotatedNormalsPath4D(
    //        this,
    //        LinFloat64PolarAngleTimeSignal.CreateConstant(
    //            TimeRange,
    //            IsPeriodic,
    //            angle
    //        )
    //    );
    //}
    

    public Quad<Float64Path4D> GetComponents()
    {
        var path1 = Float64ComputedPath4D.Finite(
            TimeRange,
            t => LinFloat64Vector4D.Create(
                GetValue(t).X,
                0,
                0, 
                0
            ),
            t => LinFloat64Vector4D.Create(
                GetDerivative1Value(t).X,
                0,
                0, 
                0
            )
        );

        var path2 = Float64ComputedPath4D.Finite(
            TimeRange,
            t => LinFloat64Vector4D.Create(
                0,
                GetValue(t).Y,
                0, 
                0
            ),
            t => LinFloat64Vector4D.Create(
                0,
                GetDerivative1Value(t).Y,
                0, 
                0
            )
        );

        var path3 = Float64ComputedPath4D.Finite(
            TimeRange,
            t => LinFloat64Vector4D.Create(
                0,
                0,
                GetValue(t).Z,
                0
            ),
            t => LinFloat64Vector4D.Create(
                0,
                0,
                GetDerivative1Value(t).Z, 
                0
            )
        );
        
        var path4 = Float64ComputedPath4D.Finite(
            TimeRange,
            t => LinFloat64Vector4D.Create(
                0,
                0,
                0,
                GetValue(t).W
            ),
            t => LinFloat64Vector4D.Create(
                0,
                0,
                0,
                GetDerivative1Value(t).W
            )
        );

        return new Quad<Float64Path4D>(path1, path2, path3, path4);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public double GetGetDerivative1NormValue(double t)
    //{
    //    return this is Float64DifferentialPath4D dCurve
    //        ? dCurve.GetTangentNormValue(t)
    //        : GetDerivative1Value(t).L2Norm().ScalarValue;
    //}

    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public IEnumerable<Float64Path4DLocalFrame> GetLocalFrames(IEnumerable<double> tList)
    //{
    //    return tList.Select(GetFrame);
    //}
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar TimeToLengthNumerical(double t, Float64ArcLengthNumericalOptions? options = null)
    {
        return GetArcLengthNumerical(MinTime, t, options);
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
    

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public SampledCurve4D GetUniformParameterSampler(Float64ScalarRange parameterRange, int sampleCount, bool isPeriodic)
    //{
    //    return new SampledCurve4D(
    //        parameterRange,
    //        isPeriodic
    //    ).GenerateUniformTime(this, sampleCount);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public SampledCurve4D GetAdaptiveSampler(Float64ScalarRange parameterRange, SampledCurve4D.SamplingOptions samplingOptions, bool isPeriodic)
    //{
    //    return new SampledCurve4D(
    //        parameterRange,
    //        isPeriodic,
    //        samplingOptions
    //    ).GenerateAdaptive(this);
    //}

    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D NegativeTime()
    //{
    //    var timeMap = Float64AffineMap1D.CreateScale((Float64Scalar)(-1));

    //    return MapTimeUsing(timeMap);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D ScaleTimeBy(double timeScaling)
    //{
    //    var timeMap = Float64AffineMap1D.CreateScale((Float64Scalar)timeScaling);

    //    return MapTimeUsing(timeMap);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D ScaleTimeUsing(double inTime, double outTime)
    //{
    //    return ScaleTimeBy(outTime / inTime);
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D OffsetTimeBy(double timeOffset)
    //{
    //    if (timeOffset.IsZero()) 
    //        return this;
        
    //    return MapTimeUsing(
    //        Float64AffineMap1D.Create(
    //            Float64Scalar.One, 
    //            timeOffset
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D OffsetTimeUsing(double inTime, double outTime)
    //{
    //    return OffsetTimeBy(outTime - inTime);
    //}

    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D OffsetTimeMinTo(double outTimeMin)
    //{
    //    return OffsetTimeBy(outTimeMin - MinTime);
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D OffsetTimeMaxTo(double outTimeMax)
    //{
    //    return OffsetTimeBy(outTimeMax - MaxTime);
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D OffsetTimeMidTo(double outTimeMid)
    //{
    //    return OffsetTimeBy(outTimeMid - MidTime);
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D MapTimeUsing(Float64AffineMap1D affineMap)
    //{
    //    if (affineMap.IsIdentity())
    //        return this;

    //    if (this is Float64AffineMappedTimePath4D affineScalar)
    //        return Float64AffineMappedTimePath4D.Create(
    //            affineScalar.BasePath,
    //            affineMap * affineScalar.TimeMap
    //        );

    //    return Float64AffineMappedTimePath4D.Create(
    //        this,
    //        affineMap
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D MapTimeUsing(double inTime1, double inTime2, double outTime1, double outTime2)
    //{
    //    var timeMap = Float64AffineMap1D.CreateFromRanges(inTime1, inTime2, outTime1, outTime2);

    //    return MapTimeUsing(timeMap);
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D MapTimeRangeTo(Float64ScalarRange timeRange)
    //{
    //    if (TimeRange.Equals(timeRange))
    //        return this;

    //    var (outTime1, outTime2) = timeRange;

    //    return MapTimeUsing(
    //        MinTime,
    //        MaxTime,
    //        outTime1,
    //        outTime2
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D MapTimeRangeTo(double outTime1, double outTime2)
    //{
    //    if (TimeRange.Equals(outTime1, outTime2))
    //        return this;

    //    return MapTimeUsing(
    //        MinTime,
    //        MaxTime,
    //        outTime1,
    //        outTime2
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D FlipTimeRange()
    //{
    //    return MapTimeUsing(
    //        MinTime,
    //        MaxTime,
    //        MaxTime,
    //        MinTime
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D FlipTimeRange(double time1, double time2)
    //{
    //    return MapTimeUsing(
    //        time1,
    //        time2,
    //        time2,
    //        time1
    //    );
    //}

    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D Negative()
    //{
    //    var valueMap = Float64InvertibleAffineMap4D.Create().ReflectOrigin();

    //    return MapUsing(valueMap);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D ScaleBy(double valueScaling)
    //{
    //    var valueMap = Float64InvertibleAffineMap4D.Create().Scale(valueScaling);

    //    return MapUsing(valueMap);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D ScaleUsing(double inValue, double outValue)
    //{
    //    return ScaleBy(outValue / inValue);
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D OffsetBy(LinFloat64Vector4D valueOffset)
    //{
    //    var valueMap = Float64InvertibleAffineMap4D.Create().Translate(valueOffset);

    //    return MapUsing(valueMap);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D OffsetUsing(LinFloat64Vector4D inValue, LinFloat64Vector4D outValue)
    //{
    //    return OffsetBy(outValue - inValue);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D OffsetMinTo(LinFloat64Vector4D outValueMin)
    //{
    //    return OffsetBy(outValueMin - GetValueRange().GetMinVector4D());
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D OffsetMaxTo(LinFloat64Vector4D outValueMax)
    //{
    //    return OffsetBy(outValueMax - GetValueRange().GetMaxVector4D());
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D OffsetMidTo(LinFloat64Vector4D outValueMid)
    //{
    //    return OffsetBy(outValueMid - GetValueRange().GetMidVector4D());
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D MapUsing(IFloat64AffineMap4D affineMap)
    //{
    //    if (affineMap.IsIdentity())
    //        return this;

    //    if (this is Float64AffineMappedPath4D affineScalar)
    //        return Float64AffineMappedPath4D.Create(
    //            affineScalar.BasePath,
    //            affineScalar.AffineMap.TransformUsing(affineMap)
    //        );

    //    return Float64AffineMappedPath4D.Create(
    //        this,
    //        affineMap
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D MapUsing(IQuad<Float64ScalarRange> inValueRange, IQuad<Float64ScalarRange> outValueRange)
    //{
    //    var valueMap = Float64ScaleTranslateAffineMap4D.CreateFromRanges(
    //        inValueRange, 
    //        outValueRange
    //    );

    //    return MapUsing(valueMap);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D MapUsing(LinFloat64Vector4D inValue1, LinFloat64Vector4D inValue2, LinFloat64Vector4D outValue1, LinFloat64Vector4D outValue2)
    //{
    //    var valueMap = Float64ScaleTranslateAffineMap4D.CreateFromRanges(
    //        inValue1, 
    //        inValue2, 
    //        outValue1, 
    //        outValue2
    //    );

    //    return MapUsing(valueMap);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D MapRangeTo(Quad<Float64ScalarRange> valueRange)
    //{
    //    return MapUsing(
    //        GetValueRange(),
    //        valueRange
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D MapRangeTo(LinFloat64Vector4D outValue1, LinFloat64Vector4D outValue2)
    //{
    //    var (inValue1, inValue2) =
    //        GetValueRange().GetMinMaxVector4D();

    //    return MapUsing(
    //        inValue1,
    //        inValue2,
    //        outValue1,
    //        outValue2
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D FlipRange()
    //{
    //    var (valueMin, valueMax) =
    //        GetValueRange().GetMinMaxVector4D();

    //    return MapUsing(
    //        valueMin,
    //        valueMax,
    //        valueMax,
    //        valueMin
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Path4D FlipRange(LinFloat64Vector4D value1, LinFloat64Vector4D value2)
    //{
    //    return MapUsing(
    //        value1,
    //        value2,
    //        value2,
    //        value1
    //    );
    //}

}