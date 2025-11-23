using System.Collections;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Samplers;

/// <summary>
/// Base abstract class for adaptive 3D samplers (flattened leaves + corner cache).
/// Subclasses must provide point/derivative/arc-length access.
/// </summary>
public abstract class Float64SampledPath3D(Float64ScalarRange timeRange, bool isPeriodic) :
    Float64ArcLengthPath3D(timeRange, isPeriodic),
    IReadOnlyList<Float64Path3DLocalFrame>
{
    /// <summary>
    /// Compact dyadic corner position with normalization (removes trailing factors of two).
    /// </summary>
    public readonly struct CornerPosition : 
        IEquatable<CornerPosition>
    {
        public int Level { get; }

        public int SegmentCount { get; }


        internal CornerPosition(int level, int segmentCount)
        {
            Debug.Assert(level >= 0 && segmentCount >= 0);

            // normalize dyadic representation by removing trailing zeros (factors of 2)
            if (segmentCount == 0)
            {
                Level = 0;
                SegmentCount = 0;
                return;
            }

            var tz = 
                BitOperations.TrailingZeroCount((uint)segmentCount);
            
            if (tz > level) 
                tz = level;

            Level = level - tz;
            SegmentCount = segmentCount >> tz;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetInterpolationValue()
        {
            return SegmentCount / (double)(1UL << Level);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetGridIndex(int maxTreeLevel)
        {
            Debug.Assert(maxTreeLevel >= Level);

            return SegmentCount << (maxTreeLevel - Level);
        }

        public bool Equals(CornerPosition other)
        {
            return Level == other.Level && 
                   SegmentCount == other.SegmentCount;
        }

        public override bool Equals(object? obj)
        {
            return obj is CornerPosition c && Equals(c);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Level, SegmentCount);
        }
    }

    public readonly struct Corner(int index, CornerPosition cornerPosition, Float64Path3DLocalFrame frame)
    {
        public int Index { get; } = index;

        public CornerPosition Position { get; } = cornerPosition;

        public Float64Path3DLocalFrame Frame { get; } = frame;
        
        public double ParameterValue 
            => Frame.TimeValue;

        public LinFloat64Vector3D Point 
            => Frame.Point;

        public LinFloat64Vector3D Tangent 
            => Frame.Tangent;


        public LinFloat64PolarAngle GetMaxAngle(Corner corner2)
        {
            return Frame.GetMaxAngleWithFrame(corner2.Frame);
        }

    }

    /// <summary>
    /// Leaf stores parameter interval [T0,T1], indices of corner endpoints, and cumulative lengths.
    /// Cumulative lengths are s0 (start) and s1 (end).
    /// </summary>
    public readonly struct Leaf
    {
        /// <summary>
        /// Leaf stores parameter interval [T0,T1], indices of corner endpoints, and cumulative lengths.
        /// Cumulative lengths are s0 (start) and s1 (end).
        /// </summary>
        public Leaf(double t0, double t1, IReadOnlyList<Corner> cornerList, int c0, int c1, double len0, double len1)
        {
            Debug.Assert(t1 > t0, "Leaf interval must have positive length");
            Debug.Assert(c0 >= 0 && c1 >= 0 && c0 < cornerList.Count && c1 < cornerList.Count);

            T0 = t0;
            T1 = t1;
            CornerList = cornerList;
            CornerIndex0 = c0;
            CornerIndex1 = c1;
            Length0 = len0;
            Length1 = len1;
        }

        public double T0 { get; }

        public double T1 { get; }

        public IReadOnlyList<Corner> CornerList { get; }

        internal int CornerIndex0 { get; }

        internal int CornerIndex1 { get; }

        public Corner Corner0 
            => CornerList[CornerIndex0];
        
        public Corner Corner1 
            => CornerList[CornerIndex1];
        
        public Float64Path3DLocalFrame Frame0 
            => CornerList[CornerIndex0].Frame;
        
        public Float64Path3DLocalFrame Frame1 
            => CornerList[CornerIndex1].Frame;

        /// <summary>
        /// Cumulative length at T0
        /// </summary>
        public double Length0 { get; }

        /// <summary>
        /// Cumulative length at T1
        /// </summary>
        public double Length1 { get; }

        public double Length 
            => Length1 - Length0;


        public Float64LineSegment3D GetLineSegment()
        {
            return Float64LineSegment3D.Create(
                Frame0.Point,
                Frame1.Point
            );
        }
        
        public LinFloat64PolarAngle EdgeFrameMaxAngle()
        {
            return Frame0.GetMaxAngleWithFrame(Frame1);
        }
    }

    public readonly struct Sample
    {
        public Leaf LeafNode { get; }
        
        public double ParameterValue { get; }

        public double InterpolationValue { get; }

        public LocalFrameInterpolationMethod FrameInterpolationMethod { get; }


        internal Sample(Leaf leafNode, double parameterValue, LocalFrameInterpolationMethod frameInterpolationMethod)
        {
            LeafNode = leafNode;
            FrameInterpolationMethod = frameInterpolationMethod;
            ParameterValue = parameterValue;
            InterpolationValue = (ParameterValue - leafNode.T0) / (leafNode.T1 - leafNode.T0);
        }

        
        public double GetLength()
        {
            if (ParameterValue <= LeafNode.T0)
                return LeafNode.Length0;

            if (ParameterValue >= LeafNode.T1)
                return LeafNode.Length1;

            return InterpolationValue.Lerp(
                LeafNode.Length0, 
                LeafNode.Length1
            );
        }

        public LinFloat64Vector3D GetPoint()
        {
            if (ParameterValue <= LeafNode.T0)
                return LeafNode.Frame0.Point;

            if (ParameterValue >= LeafNode.T1)
                return LeafNode.Frame1.Point;

            return InterpolationValue.Lerp(
                LeafNode.Frame0.Point, 
                LeafNode.Frame1.Point
            );
        }

        public LinFloat64Vector3D GetTangent()
        {
            if (ParameterValue <= LeafNode.T0)
                return LeafNode.Frame0.Tangent;

            if (ParameterValue >= LeafNode.T1)
                return LeafNode.Frame1.Tangent;
            
            if (FrameInterpolationMethod == LocalFrameInterpolationMethod.FrameInterpolation)
                return LeafNode.Frame0.InterpolateTangentToTangent(
                    LeafNode.Frame1, 
                    InterpolationValue
                );
            
            return InterpolationValue.InterpolateByRotation(
                LeafNode.Frame0.Tangent,
                LeafNode.Frame1.Tangent
            );
        }

        public Float64Path3DLocalFrame GetFrame()
        {
            if (ParameterValue <= LeafNode.T0)
                return LeafNode.Frame0;

            if (ParameterValue >= LeafNode.T1)
                return LeafNode.Frame1;

            if (FrameInterpolationMethod == LocalFrameInterpolationMethod.FrameInterpolation)
                return LeafNode.Frame0.InterpolateFrameToFrame(
                    LeafNode.Frame1, 
                    InterpolationValue
                );

            
            var point =
                InterpolationValue.Lerp(
                    LeafNode.Frame0.Point, 
                    LeafNode.Frame1.Point
                );
            
            var scalingFactor = double.Lerp(
                LeafNode.Frame0.ScalingFactor,
                LeafNode.Frame1.ScalingFactor, 
                InterpolationValue
            );

            // Use rotation interpolation to find the new frame
            var rotationMatrix = 
                InterpolationValue.GetRotationMatrixInterpolation(
                    LeafNode.Frame0.Tangent, 
                    LeafNode.Frame1.Tangent
                );

            var (tangent, normal1, normal2) = 
                new Triplet<LinFloat64Vector3D>(
                    LeafNode.Frame0.Tangent,
                    LeafNode.Frame0.Normal1.ToLinVector3D(),
                    LeafNode.Frame0.Normal2.ToLinVector3D()
                ).MapFrameUsing(rotationMatrix);

            return Float64Path3DLocalFrame.Create(
                ParameterValue,
                point,
                scalingFactor,
                tangent,
                normal1,
                normal2
            );
        }
    }

    
    /// <summary>
    /// Corner key encoding 
    /// </summary>
    /// <param name="level"></param>
    /// <param name="segmentCount"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static ulong EncodeCornerKey(int level, int segmentCount)
    {
        // Normalize fraction segmentCount / 2^level by removing factors of 2
        if (segmentCount == 0) return 0UL;

        var tz = BitOperations.TrailingZeroCount((uint)segmentCount);
        if (tz > level) tz = level;
        
        var segNorm = (ulong)(segmentCount >> tz);
        var levelNorm = (ulong)(level - tz);
        
        return (levelNorm << 32) | segNorm;
    }


    private IReadOnlyList<int>? _sortedCornerIndicesCache;
    
    protected readonly Dictionary<ulong, int> CornerIndex = new();
    
    protected readonly List<Corner> CornerList = new();
    
    protected readonly List<Leaf> LeafList = new();
    

    public double TotalLength { get; protected set; } = 0.0;

    public IReadOnlyList<Leaf> Leaves 
        => LeafList;

    public IEnumerable<Float64LineSegment3D> Segments 
        => LeafList.Select(l => l.GetLineSegment());

    protected IReadOnlyList<int> SortedCornerIndices
    {
        get
        {
            if (CornerList.Count == 0) 
                throw new InvalidOperationException("Sampler not generated. Call Generate() first.");

            _sortedCornerIndicesCache ??= 
                CornerList
                    .Select((c, i) => (corner: c, index: i))
                    .OrderBy(x => x.corner.ParameterValue)
                    .Select(x => x.index)
                    .ToList()
                    .AsReadOnly();

            return _sortedCornerIndicesCache;
        }
    }

    public IEnumerable<Corner> Corners 
        => SortedCornerIndices.Select(i => CornerList[i]);
    
    public IEnumerable<double> ParameterValues 
        => SortedCornerIndices.Select(i => CornerList[i].ParameterValue);

    public IEnumerable<LinFloat64Vector3D> Points 
        => SortedCornerIndices.Select(i => CornerList[i].Point);
    
    public IEnumerable<LinFloat64Vector3D> Tangents 
        => SortedCornerIndices.Select(i => CornerList[i].Frame.Tangent);

    public bool IsReady { get; protected set; }

    public int Count 
        => CornerList.Count;

    public Float64Path3DLocalFrame this[int index]
    {
        get
        {
            var cornerIndex = SortedCornerIndices[index];
            
            return CornerList[cornerIndex].Frame;
        }
    }


    protected int GetOrAddCorner(Float64Path3D path, CornerPosition pos, double tClampMin, double tClampMax)
    {
        var key = EncodeCornerKey(pos.Level, pos.SegmentCount);
        if (CornerIndex.TryGetValue(key, out var idx)) 
            return idx;

        var alpha = pos.GetInterpolationValue();
        
        // linear mapping; subclasses may use different domain transform if needed
        var t = (1.0 - alpha) * tClampMin + alpha * tClampMax;

        var frame = path.GetFrame(t);
        
        idx = CornerList.Count;
        
        CornerList.Add(
            new Corner(idx, pos, frame)
        );
        CornerIndex[key] = idx;
        return idx;
    }

    /// <summary>
    /// Subclasses must call Generate() to build the sampling structure.
    /// This base class provides SubdivideRecursive helper that subclasses should drive.
    /// </summary>
    protected void ResetStorage()
    {
        CornerIndex.Clear();
        CornerList.Clear();
        LeafList.Clear();
        _sortedCornerIndicesCache = null;
        TotalLength = 0.0;
    }

    /// <summary>
    /// Add a leaf interval [t0,t1] with endpoint corner indices and cumulative lengths.
    /// </summary>
    protected void AddLeaf(double t0, double t1, int c0, int c1, double cumulative, double segArc)
    {
        var s1 = cumulative + segArc;
        
        LeafList.Add(
            new Leaf(t0, t1, CornerList, c0, c1, cumulative, s1)
        );
        
        TotalLength = s1;
    }

    /// <summary>
    /// Binary search leaf by parameter t (common).
    /// </summary>
    protected int FindLeafIndexByParameter(double t)
    {
        var lo = 0;
        var hi = LeafList.Count - 1;
        while (lo <= hi)
        {
            var mid = (lo + hi) >> 1;
            var midLeaf = LeafList[mid];

            if (t < midLeaf.T0)
                hi = mid - 1;
            else if (t > midLeaf.T1)
                lo = mid + 1;
            else
                return mid;
        }

        return Math.Clamp(lo, 0, LeafList.Count - 1);
    }

    /// <summary>
    /// Find leaf by cumulative length s (binary search)
    /// </summary>
    protected int FindLeafIndexByLength(double s)
    {
        int lo = 0, hi = LeafList.Count - 1;
        while (lo <= hi)
        {
            var mid = (lo + hi) >> 1;
            var leaf = LeafList[mid];

            if (s < leaf.Length0)
                hi = mid - 1;
            else if (s > leaf.Length1)
                lo = mid + 1;
            else
                return mid;
        }

        return Math.Clamp(lo, 0, LeafList.Count - 1);
    }

    
    public override Float64Scalar GetLength()
    {
        return TotalLength;
    }

    /// <summary>
    /// Parameter sampling helper
    /// </summary>
    /// <param name="t"></param>
    /// <param name="interpolationMethod"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public Sample GetSampleByParameter(double t, LocalFrameInterpolationMethod interpolationMethod)
    {
        if (LeafList.Count == 0) 
            throw new InvalidOperationException("Sampler not generated. Call Generate() first.");

        t = TimeRange.Clamp(t, IsPeriodic);
        var leafIndex = FindLeafIndexByParameter(t);
        var leaf = LeafList[leafIndex];

        return new Sample(leaf, t, interpolationMethod);
    }

    /// <summary>
    /// Length sampling helper
    /// </summary>
    /// <param name="s"></param>
    /// <param name="interpolationMethod"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public Sample GetSampleByLength(double s, LocalFrameInterpolationMethod interpolationMethod)
    {
        if (LeafList.Count == 0) 
            throw new InvalidOperationException("Sampler not generated. Call Generate() first.");

        // clamp outside interval - subclasses may have different domain; by convention we use [TMin,TMax] in subclass
        // here we assume subclass will clamp in GetDerivative/GetPoint if needed; use t directly
        var leafIndex = FindLeafIndexByLength(s);
        var leaf = LeafList[leafIndex];

        var ts = (s - leaf.Length0) / leaf.Length;
        var t = (1 - ts) * leaf.T0 + ts * leaf.T1;

        return new Sample(leaf, t, interpolationMethod);
    }

    public IEnumerator<Float64Path3DLocalFrame> GetEnumerator()
    {
        return Corners.Select(c => c.Frame).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}