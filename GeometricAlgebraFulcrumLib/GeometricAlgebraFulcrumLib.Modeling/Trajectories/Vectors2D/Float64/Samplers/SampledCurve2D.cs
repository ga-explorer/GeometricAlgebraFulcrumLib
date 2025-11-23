using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Samplers;

/// <summary>
/// General-purpose sampler for 2D smooth curves
/// </summary>
public sealed class SampledCurve2D(
    Float64ScalarRange timeRange,
    bool isPeriodic,
    SampledCurve2D.SamplingOptions? options = null) :
    Float64SampledPath2D(timeRange, isPeriodic, options ?? new SamplingOptions())
{
    public sealed class SamplingOptions :
        Float64AdaptiveSamplingOptions
    {
        public Float64IntegrationMode IntegrationMode { get; set; }
            = Float64IntegrationMode.GaussLegendre7;

        public int MaxSimpsonRecursionDepth { get; set; } = 15;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SampledCurve2D Finite(Float64Path2D basePath, SamplingOptions? options = null)
    {
        return new SampledCurve2D(
            basePath.TimeRange,
            false,
            options
        ).GenerateAdaptive(basePath);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SampledCurve2D Finite(Float64ScalarRange timeRange, Float64Path2D basePath, SamplingOptions? options = null)
    {
        return new SampledCurve2D(
            timeRange,
            false,
            options
        ).GenerateAdaptive(basePath);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SampledCurve2D Periodic(Float64Path2D basePath, SamplingOptions? options = null)
    {
        return new SampledCurve2D(
            basePath.TimeRange,
            true,
            options
        ).GenerateAdaptive(basePath);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SampledCurve2D Periodic(Float64ScalarRange timeRange, Float64Path2D basePath, SamplingOptions? options = null)
    {
        return new SampledCurve2D(
            timeRange,
            true,
            options
        ).GenerateAdaptive(basePath);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SampledCurve2D Create(Float64Path2D basePath, SamplingOptions? options = null)
    {
        return new SampledCurve2D(
            basePath.TimeRange,
            basePath.IsPeriodic,
            options
        ).GenerateAdaptive(basePath);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SampledCurve2D Create(Float64ScalarRange timeRange, Float64Path2D basePath, SamplingOptions? options = null)
    {
        return new SampledCurve2D(
            timeRange,
            basePath.IsPeriodic,
            options
        ).GenerateAdaptive(basePath);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SampledCurve2D Create(bool isPeriodic, Float64Path2D basePath, SamplingOptions? options = null)
    {
        return new SampledCurve2D(
            basePath.TimeRange,
            isPeriodic,
            options
        ).GenerateAdaptive(basePath);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SampledCurve2D Create(Float64ScalarRange timeRange, bool isPeriodic, Float64Path2D basePath, SamplingOptions? options = null)
    {
        return new SampledCurve2D(
            timeRange,
            isPeriodic,
            options
        ).GenerateAdaptive(basePath);
    }


    private readonly Dictionary<ulong, double> _arcLengthCache
        = new Dictionary<ulong, double>();

    public new SamplingOptions Options { get; } = options ?? new SamplingOptions();


    private void SubdivideRecursive(Float64Path2D curve, int level, CornerPosition pos0, CornerPosition pos1, double t0, double t1, int c0, int c1, double cumulative)
    {
        var corner0 = CornerList[c0];
        var corner1 = CornerList[c1];

        var paramSpan = Math.Abs(t1 - t0);
        var dx = corner1.Point.X - corner0.Point.X;
        var dy = corner1.Point.Y - corner0.Point.Y;
        var chord = Math.Sqrt(dx * dx + dy * dy);

        var dot = Math.Clamp(corner0.Tangent.X * corner1.Tangent.X + corner0.Tangent.Y * corner1.Tangent.Y, -1.0, 1.0);
        var angle = Math.Acos(dot);

        //var segArcApprox =
        //    (corner1.Tangent - corner0.Tangent).Norm() * (t1 - t0);

        //var segArc = curve.GetArcLength(t0, t1);

        // create a key from corner indices c0,c1 (both int >=0)
        var a = (uint)Math.Min(c0, c1);
        var b = (uint)Math.Max(c0, c1);
        var alcKey = (ulong)a << 32 | b;
        if (!_arcLengthCache.TryGetValue(alcKey, out var segArc))
        {
            segArc = curve.GetArcLength(t0, t1);
            _arcLengthCache[alcKey] = segArc;
        }

        var meetsArcChord = chord < 1e-14 || (segArc - chord) / chord <= base.Options.RelativeChordError;
        var meetsAngle = angle <= Options.FrameAngleDifference.RadiansValue;
        var smallParam = paramSpan <= Options.ParameterSpan;

        var shouldSubdivide = level < Options.MinDepth ||
                              level < Options.MaxDepth && !(meetsArcChord && meetsAngle && smallParam);

        if (shouldSubdivide)
        {
            var childLevel = level + 1;
            var midPos = new CornerPosition(childLevel, (pos0.SegmentCount << 1) + 1);

            var cMid = GetOrAddCorner(curve, midPos, MinTime, MaxTime);
            var pm = CornerList[cMid].Point;

            var leftDx = pm.X - corner0.Point.X;
            var leftDy = pm.Y - corner0.Point.Y;
            var leftChord = Math.Sqrt(leftDx * leftDx + leftDy * leftDy);

            var rightDx = corner1.Point.X - pm.X;
            var rightDy = corner1.Point.Y - pm.Y;
            var rightChord = Math.Sqrt(rightDx * rightDx + rightDy * rightDy);

            var childChordSum = leftChord + rightChord;
            var improvement = childChordSum - chord;
            var relativeImprovement = chord > 1e-14 ? improvement / chord : 0.0;

            if (relativeImprovement < Options.RelativeChordImprovement)
            {
                AddLeaf(t0, t1, c0, c1, cumulative, segArc);
                return;
            }

            var tm = 0.5 * (t0 + t1);
            SubdivideRecursive(curve, childLevel, pos0, midPos, t0, tm, c0, cMid, cumulative);

            var afterLeft = TotalLength;
            SubdivideRecursive(curve, childLevel, midPos, pos1, tm, t1, cMid, c1, afterLeft);
        }
        else
        {
            AddLeaf(t0, t1, c0, c1, cumulative, segArc);
        }
    }

    
    public override Float64ArcLengthPath2D ToFiniteArcLengthPath()
    {
        throw new NotImplementedException();
    }

    public override Float64ArcLengthPath2D ToPeriodicArcLengthPath()
    {
        throw new NotImplementedException();
    }

    public override Float64Scalar TimeToLength(double t)
    {
        return GetSampleByParameter(t).GetLength();
    }

    public override Float64Scalar LengthToTime(double s)
    {
        return GetSampleByLength(s).ParameterValue;
    }

    public override LinFloat64Vector2D GetValue(double t)
    {
        return GetSampleByParameter(t).GetPoint();
    }

    public override LinFloat64Vector2D GetDerivative1Value(double t)
    {
        return GetSampleByParameter(t).GetTangent();
    }

    public override bool IsValid()
    {
        throw new NotImplementedException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SampledCurve2D GetSubCurve(double t1, double t2, SamplingOptions options)
    {
        if (t1 > t2)
            (t1, t2) = (t2, t1);

        return new SampledCurve2D(
            Float64ScalarRange.Create(t1, t2),
            false,
            options
        ).GenerateAdaptive(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SampledCurve2D GetSubCurveByLength(double length1, double length2, SamplingOptions options)
    {
        if (length1 > length2)
            (length1, length2) = (length2, length1);

        return GetSubCurve(
            LengthToTime(length1),
            LengthToTime(length2),
            options
        );
    }
    

    /// <summary>
    /// Construct a trimmed sampled path between two time values.
    /// Keeps existing sampling but discards leaves outside [tMin, tMax].
    /// </summary>
    public SampledCurve2D TrimByTime(double tMin, double tMax)
    {
        if (tMin > tMax)
            (tMin, tMax) = (tMax, tMin);

        if (LeafList.Count == 0)
            throw new InvalidOperationException("Sampler not generated. Call Generate() first.");

        // Clamp to valid range
        tMin = TimeRange.Clamp(tMin, IsPeriodic);
        tMax = TimeRange.Clamp(tMax, IsPeriodic);

        var newSampler = new SampledCurve2D(
            Float64ScalarRange.Create(tMin, tMax),
            false,
            Options
        );

        newSampler.ResetStorage();

        var cumulative = 0.0;
        foreach (var leaf in LeafList)
        {
            if (leaf.T1 <= tMin || leaf.T0 >= tMax)
                continue;

            var t0 = Math.Max(leaf.T0, tMin);
            var t1 = Math.Min(leaf.T1, tMax);

            // Create corners in the new sampler
            var pos0 = new CornerPosition(0, 0); // dummy — ensures unique keys
            var pos1 = new CornerPosition(0, 1);

            var c0 = newSampler.GetOrAddCorner(this, pos0, t0, t1);
            var c1 = newSampler.GetOrAddCorner(this, pos1, t0, t1);

            double segArc = GetArcLength(t0, t1);

            newSampler.AddLeaf(t0, t1, c0, c1, cumulative, segArc);
            cumulative = newSampler.TotalLength;
        }

        return newSampler;
    }

    /// <summary>
    /// Construct a trimmed sampled path between two arc-length values.
    /// Keeps existing sampling but discards leaves outside [sMin, sMax].
    /// </summary>
    public SampledCurve2D TrimByLength(double sMin, double sMax)
    {
        if (sMin > sMax)
            (sMin, sMax) = (sMax, sMin);

        if (LeafList.Count == 0)
            throw new InvalidOperationException("Sampler not generated. Call Generate() first.");

        // Clamp to valid range
        sMin = Math.Clamp(sMin, 0.0, TotalLength);
        sMax = Math.Clamp(sMax, 0.0, TotalLength);

        var tMin = LengthToTime(sMin).ScalarValue;
        var tMax = LengthToTime(sMax).ScalarValue;

        var newSampler = new SampledCurve2D(
            Float64ScalarRange.Create(tMin, tMax),
            false,
            Options
        );

        newSampler.ResetStorage();

        var cumulative = 0.0;
        foreach (var leaf in LeafList)
        {
            if (leaf.Length1 <= sMin || leaf.Length0 >= sMax)
                continue;

            var t0 = Math.Max(leaf.T0, tMin);
            var t1 = Math.Min(leaf.T1, tMax);

            var pos0 = new CornerPosition(0, 0);
            var pos1 = new CornerPosition(0, 1);

            var c0 = newSampler.GetOrAddCorner(this, pos0, t0, t1);
            var c1 = newSampler.GetOrAddCorner(this, pos1, t0, t1);

            double segArc = GetArcLength(t0, t1);

            newSampler.AddLeaf(t0, t1, c0, c1, cumulative, segArc);
            cumulative = newSampler.TotalLength;
        }

        return newSampler;
    }


    /// <summary>
    /// Generate a uniform-time sampled path.
    /// </summary>
    /// <param name="curve">Base parametric curve</param>
    /// <param name="sampleCount">Number of samples (>= 2)</param>
    /// <returns>Uniform-time sampled path</returns>
    public SampledCurve2D GenerateUniformTime(Float64Path2D curve, int sampleCount = 100)
    {
        if (sampleCount < 2)
            throw new ArgumentOutOfRangeException(nameof(sampleCount), "Sample count must be >= 2");

        ResetStorage();
        _arcLengthCache.Clear();

        var t0 = MinTime;
        var t1 = MaxTime;
        var dt = (t1 - t0) / (sampleCount - 1);

        var cumulative = 0.0;
        var prevCorner = GetOrAddCorner(curve, new CornerPosition(0, 0), MinTime, MaxTime);

        for (var i = 1; i < sampleCount; i++)
        {
            var t = t0 + i * dt;
            var cornerPos = new CornerPosition(0, i); // not meaningful dyadic, but ok for uniqueness
            var corner = GetOrAddCorner(curve, cornerPos, MinTime, MaxTime);

            double segArc = 
                curve.GetArcLength(CornerList[prevCorner].ParameterValue, t);

            AddLeaf(CornerList[prevCorner].ParameterValue, t, prevCorner, corner, cumulative, segArc);
            
            cumulative = TotalLength;
            prevCorner = corner;
        }

        return this;
    }

    /// <summary>
    /// Generate a uniform-length sampled path.
    /// </summary>
    /// <param name="curve">Base parametric curve</param>
    /// <param name="sampleCount">Number of samples (>= 2)</param>
    /// <returns>Uniform-length sampled path</returns>
    public SampledCurve2D GenerateUniformLength(Float64Path2D curve, int sampleCount = 100)
    {
        if (sampleCount < 2)
            throw new ArgumentOutOfRangeException(nameof(sampleCount), "Sample count must be >= 2");

        ResetStorage();
        _arcLengthCache.Clear();

        // Compute total length once
        double totalLength = curve.GetArcLength(MinTime, MaxTime);
        var ds = totalLength / (sampleCount - 1);

        var cumulative = 0.0;
        var prevCorner = GetOrAddCorner(curve, new CornerPosition(0, 0), MinTime, MaxTime);
        var tPrev = MinTime;

        for (var i = 1; i < sampleCount; i++)
        {
            var targetLength = i * ds;

            // Invert arc length numerically to find t (binary search)
            var tLow = MinTime;
            var tHigh = MaxTime;
            var tMid = tLow;

            for (var iter = 0; iter < 40; iter++) // ~1e-12 precision
            {
                tMid = 0.5 * (tLow + tHigh);
                double len = curve.GetArcLength(MinTime, tMid);

                if (len < targetLength)
                    tLow = tMid;
                else
                    tHigh = tMid;
            }

            var cornerPos = new CornerPosition(0, i);
            var corner = GetOrAddCorner(curve, cornerPos, MinTime, MaxTime);

            double segArc = curve.GetArcLength(tPrev, tMid);

            AddLeaf(tPrev, tMid, prevCorner, corner, cumulative, segArc);
            cumulative = TotalLength;
            prevCorner = corner;
            tPrev = tMid;
        }

        return this;
    }

    /// <summary>
    /// Generate a path sampled at explicit time values (must be strictly increasing).
    /// </summary>
    /// <param name="curve">Base parametric curve</param>
    /// <param name="timeValues">Sorted list of time values in [MinTime, MaxTime]</param>
    /// <returns>Sampled path</returns>
    public SampledCurve2D GenerateFromTimes(Float64Path2D curve, IEnumerable<double> timeValues)
    {
        var times = timeValues.ToList();

        if (times.Count < 2)
            throw new ArgumentException("At least two time values are required", nameof(timeValues));

        if (!times.SequenceEqual(times.OrderBy(x => x)))
            throw new ArgumentException("Time values must be strictly increasing", nameof(timeValues));

        ResetStorage();
        _arcLengthCache.Clear();

        var cumulative = 0.0;
        var prevCorner = GetOrAddCorner(curve, new CornerPosition(0, 0), MinTime, MaxTime);
        var tPrev = times[0];

        for (var i = 1; i < times.Count; i++)
        {
            var t = times[i];
            var cornerPos = new CornerPosition(0, i);
            var corner = GetOrAddCorner(curve, cornerPos, MinTime, MaxTime);

            double segArc = curve.GetArcLength(tPrev, t);

            AddLeaf(tPrev, t, prevCorner, corner, cumulative, segArc);
            cumulative = TotalLength;
            prevCorner = corner;
            tPrev = t;
        }

        return this;
    }

    /// <summary>
    /// Generation: subdivide (uses base utilities)
    /// </summary>
    public SampledCurve2D GenerateAdaptive(Float64Path2D curve)
    {
        ResetStorage();
        _arcLengthCache.Clear();

        var pos0 = new CornerPosition(0, 0);
        var pos1 = new CornerPosition(0, 1);

        var c0 = GetOrAddCorner(curve, pos0, MinTime, MaxTime);
        var c1 = GetOrAddCorner(curve, pos1, MinTime, MaxTime);

        SubdivideRecursive(curve, 0, pos0, pos1, MinTime, MaxTime, c0, c1, 0.0);

        IsReady = true;

        return this;
    }


}