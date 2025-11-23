using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Samplers;

/// <summary>
/// General-purpose adaptive sampler for 3D smooth curves
/// </summary>
public sealed class SampledCurve3D(
    Float64ScalarRange timeRange,
    bool isPeriodic,
    SampledCurve3D.SamplingOptions? options = null) :
    Float64SampledPath3D(timeRange, isPeriodic)
{
    public sealed class SamplingOptions :
        Float64AdaptiveSamplingOptions
    {
        public LocalFrameInterpolationMethod FrameInterpolationMethod { get; set; }
            = LocalFrameInterpolationMethod.TangentInterpolation;

        public ParametricCurveLocalFrameSamplingMethod FrameSamplingMethod { get; set; }
            = ParametricCurveLocalFrameSamplingMethod.SimpleRotation;

        public Float64IntegrationMode IntegrationMode { get; set; }
            = Float64IntegrationMode.GaussLegendre7;

        public int MaxSimpsonRecursionDepth { get; set; } = 15;
    }

    internal sealed class CurveData
    {
        public Float64ScalarRange TimeRange { get; }

        public Func<double, LinFloat64Vector3D> GetPointFunc { get; }

        public Func<double, LinFloat64Vector3D> GetTangentFunc { get; }

        public Func<double, Triplet<LinFloat64Vector3D>> GetFrameFunc { get; }


        public CurveData(Float64ScalarRange timeRange, Func<double, LinFloat64Vector3D> getPointFunc, Func<double, LinFloat64Vector3D>? getTangentFunc = null, Func<double, Triplet<LinFloat64Vector3D>>? getFrameFunc = null)
        {
            TimeRange = timeRange;
            GetPointFunc = getPointFunc;
            GetTangentFunc = getTangentFunc ?? ComputeTangent;
            GetFrameFunc = getFrameFunc ?? ComputeFrame;
        }


        private LinFloat64Vector3D ComputeTangent(double t)
        {
            t = TimeRange.Clamp(t);

            const double machineEpsilon = 2.2204460492503131e-16;
            var eps = Math.Sqrt(machineEpsilon);
            var h = Math.Max(1e-12, eps * Math.Max(1.0, Math.Abs(t)));

            var min = TimeRange.MinValue;
            var max = TimeRange.MaxValue;

            // Candidate points
            var tM2 = t - 2 * h;
            var tM1 = t - h;
            var tP1 = t + h;
            var tP2 = t + 2 * h;

            var hasM2 = tM2 >= min;
            var hasM1 = tM1 >= min;
            var hasP1 = tP1 <= max;
            var hasP2 = tP2 <= max;

            // 5-point central if possible (high accuracy)
            if (hasM2 && hasP2)
            {
                var p1 = GetPointFunc(tM2);
                var p2 = GetPointFunc(tM1);
                var p3 = GetPointFunc(tP1);
                var p4 = GetPointFunc(tP2);

                var denom = 12.0 * h;
                var dx = (-p4.X + 8.0 * p3.X - 8.0 * p2.X + p1.X) / denom;
                var dy = (-p4.Y + 8.0 * p3.Y - 8.0 * p2.Y + p1.Y) / denom;
                var dz = (-p4.Z + 8.0 * p3.Z - 8.0 * p2.Z + p1.Z) / denom;

                return LinFloat64Vector3D.Create(dx, dy, dz);
            }

            // Fallback to 3-point central if possible
            if (hasM1 && hasP1)
            {
                var p2 = GetPointFunc(tM1);
                var p3 = GetPointFunc(tP1);

                var denom = 2.0 * h;
                var dx = (p3.X - p2.X) / denom;
                var dy = (p3.Y - p2.Y) / denom;
                var dz = (p3.Z - p2.Z) / denom;

                return LinFloat64Vector3D.Create(dx, dy, dz);
            }

            // One-sided forward or backward 3-point (lower order) near domain boundaries
            if (hasP1 && hasP2)
            {
                var p0 = GetPointFunc(t);
                var p1 = GetPointFunc(tP1);
                var p2 = GetPointFunc(tP2);

                // forward difference (order h^2)
                var denom = 2.0 * h;
                var dx = (-3.0 * p0.X + 4.0 * p1.X - p2.X) / denom;
                var dy = (-3.0 * p0.Y + 4.0 * p1.Y - p2.Y) / denom;
                var dz = (-3.0 * p0.Z + 4.0 * p1.Z - p2.Z) / denom;

                return LinFloat64Vector3D.Create(dx, dy, dz);
            }

            // backward difference
            {
                var p0 = GetPointFunc(t);
                var p1 = GetPointFunc(tM1);
                var p2 = hasM2 ? GetPointFunc(tM2) : p1; // if m2 not available, reuse p1

                var denom = 2.0 * h;
                var dx = (3.0 * p0.X - 4.0 * p1.X + p2.X) / denom;
                var dy = (3.0 * p0.Y - 4.0 * p1.Y + p2.Y) / denom;
                var dz = (3.0 * p0.Z - 4.0 * p1.Z + p2.Z) / denom;

                return LinFloat64Vector3D.Create(dx, dy, dz);
            }
        }

        private Triplet<LinFloat64Vector3D> ComputeFrame(double t)
        {
            var tangentRaw = GetTangentFunc(t);

            // compute norm explicitly to avoid relying on library helper naming
            var nx = tangentRaw.X;
            var ny = tangentRaw.Y;
            var nz = tangentRaw.Z;
            var norm = Math.Sqrt(nx * nx + ny * ny + nz * nz);

            const double tol = 1e-14;
            LinFloat64Vector3D tangent;
            if (norm <= tol)
            {
                // fallback: try small forward difference to get a nonzero tangent
                var h = 1e-8 * Math.Max(1.0, Math.Abs(t));
                var tp = TimeRange.Clamp(t + h);
                var tm = TimeRange.Clamp(t - h);
                var pPlus = GetPointFunc(tp);
                var pMinus = GetPointFunc(tm);

                var dx = (pPlus.X - pMinus.X) / (tp - tm);
                var dy = (pPlus.Y - pMinus.Y) / (tp - tm);
                var dz = (pPlus.Z - pMinus.Z) / (tp - tm);

                tangent = LinFloat64Vector3D.Create(dx, dy, dz);

                // if still nearly zero, choose an arbitrary unit vector
                var tn = Math.Sqrt(dx * dx + dy * dy + dz * dz);
                tangent = tn <= tol
                    ? LinFloat64Vector3D.Create(1.0, 0.0, 0.0)
                    : tangent.VectorDivideByENorm();
            }
            else
            {
                tangent = tangentRaw.VectorDivideByENorm();
            }

            var (normal1, normal2) =
                tangent.GetUnitNormalPair();

            return new Triplet<LinFloat64Vector3D>(tangent, normal1, normal2);
        }

    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SampledCurve3D Finite(Float64Path3D basePath, SamplingOptions? options = null)
    {
        return new SampledCurve3D(
            basePath.TimeRange,
            false,
            options
        ).GenerateAdaptive(basePath);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SampledCurve3D Finite(Float64ScalarRange timeRange, Float64Path3D basePath, SamplingOptions? options = null)
    {
        return new SampledCurve3D(
            timeRange,
            false,
            options
        ).GenerateAdaptive(basePath);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SampledCurve3D Periodic(Float64Path3D basePath, SamplingOptions? options = null)
    {
        return new SampledCurve3D(
            basePath.TimeRange,
            true,
            options
        ).GenerateAdaptive(basePath);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SampledCurve3D Periodic(Float64ScalarRange timeRange, Float64Path3D basePath, SamplingOptions? options = null)
    {
        return new SampledCurve3D(
            timeRange,
            true,
            options
        ).GenerateAdaptive(basePath);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SampledCurve3D Create(Float64Path3D basePath, SamplingOptions? options = null)
    {
        return new SampledCurve3D(
            basePath.TimeRange,
            basePath.IsPeriodic,
            options
        ).GenerateAdaptive(basePath);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SampledCurve3D Create(Float64ScalarRange timeRange, Float64Path3D basePath, SamplingOptions? options = null)
    {
        return new SampledCurve3D(
            timeRange,
            basePath.IsPeriodic,
            options
        ).GenerateAdaptive(basePath);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SampledCurve3D Create(bool isPeriodic, Float64Path3D basePath, SamplingOptions? options = null)
    {
        return new SampledCurve3D(
            basePath.TimeRange,
            isPeriodic,
            options
        ).GenerateAdaptive(basePath);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SampledCurve3D Create(Float64ScalarRange timeRange, bool isPeriodic, Float64Path3D basePath, SamplingOptions? options = null)
    {
        return new SampledCurve3D(
            timeRange,
            isPeriodic,
            options
        ).GenerateAdaptive(basePath);
    }


    private readonly Dictionary<ulong, double> _arcLengthCache
        = new Dictionary<ulong, double>();

    public SamplingOptions Options { get; } = options ?? new SamplingOptions();


    private void SubdivideRecursive(Float64Path3D curve, int level, CornerPosition pos0, CornerPosition pos1, double t0, double t1, int c0, int c1, double cumulative)
    {
        var corner0 = CornerList[c0];
        var corner1 = CornerList[c1];

        var paramSpan = Math.Abs(t1 - t0);
        var dx = corner1.Point.X - corner0.Point.X;
        var dy = corner1.Point.Y - corner0.Point.Y;
        var dz = corner1.Point.Z - corner0.Point.Z;
        var chord = Math.Sqrt(dx * dx + dy * dy + dz * dz);

        var angle = corner0.GetMaxAngle(corner1).RadiansValue;

        //var segArcApprox =
        //    (corner1.Tangent - corner0.Tangent).Norm() * (t1 - t0);

        //var segArc = curve.GetArcLength(t0, t1);

        // create a key from corner indices c0,c1 (both int >=0)
        var a = (uint)Math.Min(c0, c1);
        var b = (uint)Math.Max(c0, c1);
        var alcKey = ((ulong)a << 32) | b;
        if (!_arcLengthCache.TryGetValue(alcKey, out var segArc))
        {
            segArc = curve.GetArcLength(t0, t1);
            _arcLengthCache[alcKey] = segArc;
        }

        var meetsArcChord = chord < 1e-14 || (segArc - chord) / chord <= Options.RelativeChordError;
        var meetsAngle = angle <= Options.FrameAngleDifference.RadiansValue;
        var smallParam = paramSpan <= Options.ParameterSpan;

        var shouldSubdivide =
            level < Options.MinDepth ||
            (level < Options.MaxDepth && !(meetsArcChord && meetsAngle && smallParam));

        if (shouldSubdivide)
        {
            var childLevel = level + 1;
            var midPos = new CornerPosition(childLevel, (pos0.SegmentCount << 1) + 1);

            var cMid = GetOrAddCorner(curve, midPos, MinTime, MaxTime);
            var pm = CornerList[cMid].Point;

            var leftDx = pm.X - corner0.Point.X;
            var leftDy = pm.Y - corner0.Point.Y;
            var leftDz = pm.Z - corner0.Point.Z;
            var leftChord = Math.Sqrt(leftDx * leftDx + leftDy * leftDy + leftDz * leftDz);

            var rightDx = corner1.Point.X - pm.X;
            var rightDy = corner1.Point.Y - pm.Y;
            var rightDz = corner1.Point.Z - pm.Z;
            var rightChord = Math.Sqrt(rightDx * rightDx + rightDy * rightDy + rightDz * rightDz);

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


    public override bool IsValid()
    {
        throw new NotImplementedException();
    }

    public override Float64ArcLengthPath3D ToFiniteArcLengthPath()
    {
        throw new NotImplementedException();
    }

    public override Float64ArcLengthPath3D ToPeriodicArcLengthPath()
    {
        throw new NotImplementedException();
    }

    public override Float64Scalar TimeToLength(double t)
    {
        var sample = GetSampleByParameter(t, Options.FrameInterpolationMethod);

        return sample.GetLength();
    }

    public override Float64Scalar LengthToTime(double s)
    {
        var sample = GetSampleByLength(s, Options.FrameInterpolationMethod);

        return sample.ParameterValue;
    }

    public override LinFloat64Vector3D GetValue(double t)
    {
        return GetSampleByParameter(t, Options.FrameInterpolationMethod).GetPoint();
    }

    public override LinFloat64Vector3D GetDerivative1Value(double t)
    {
        return GetSampleByParameter(t, Options.FrameInterpolationMethod).GetTangent();
    }

    public override Float64Path3DLocalFrame GetFrame(double t)
    {
        return GetSampleByParameter(t, Options.FrameInterpolationMethod).GetFrame();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SampledCurve3D GetSubCurve(double t1, double t2, SamplingOptions options)
    {
        if (t1 > t2)
            (t1, t2) = (t2, t1);

        return new SampledCurve3D(
            Float64ScalarRange.Create(t1, t2),
            false,
            options
        ).GenerateAdaptive(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SampledCurve3D GetSubCurveByLength(double length1, double length2, SamplingOptions options)
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
    public SampledCurve3D TrimByTime(double tMin, double tMax)
    {
        if (tMin > tMax)
            (tMin, tMax) = (tMax, tMin);

        if (LeafList.Count == 0)
            throw new InvalidOperationException("Sampler not generated. Call Generate() first.");

        // Clamp to valid range
        tMin = TimeRange.Clamp(tMin, IsPeriodic);
        tMax = TimeRange.Clamp(tMax, IsPeriodic);

        var newSampler = new SampledCurve3D(
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
    public SampledCurve3D TrimByLength(double sMin, double sMax)
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

        var newSampler = new SampledCurve3D(
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
    public SampledCurve3D GenerateUniformTime(Float64Path3D curve, int sampleCount = 100)
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
            var cornerPos = new CornerPosition(0, i);
            var corner = GetOrAddCorner(curve, cornerPos, MinTime, MaxTime);

            double segArc = curve.GetArcLength(CornerList[prevCorner].ParameterValue, t);

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
    public SampledCurve3D GenerateUniformLength(Float64Path3D curve, int sampleCount = 100)
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

            // Invert arc length numerically via binary search
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
    public SampledCurve3D GenerateFromTimes(Float64Path3D curve, IEnumerable<double> timeValues)
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
    public SampledCurve3D GenerateAdaptive(Float64Path3D curve)
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