// File: AdaptiveVolumeSampler3D.cs

using System.Collections.Concurrent;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Volumes.Sampled
{
    public sealed class AdaptiveVolumeSampler3D
    {
        private readonly IVolumeEvaluator _evaluator;
        private readonly (double MinU, double MaxU, double MinV, double MaxV, double MinW, double MaxW) _paramRange;
        private readonly AdaptiveVolumeSamplingOptions _options;

        // canonical set for positions
        private readonly HashSet<SampledVolume3D.CornerPosition3D> _cornerPositionSet = new HashSet<SampledVolume3D.CornerPosition3D>();

        // tuple type alias (8-corner tuple)
        internal struct CornerTupleKey : IEquatable<CornerTupleKey>
        {
            public readonly SampledVolume3D.CornerPosition3D A, B, C, D, E, F, G, H;
            public CornerTupleKey(
                SampledVolume3D.CornerPosition3D a, SampledVolume3D.CornerPosition3D b, SampledVolume3D.CornerPosition3D c, SampledVolume3D.CornerPosition3D d,
                SampledVolume3D.CornerPosition3D e, SampledVolume3D.CornerPosition3D f, SampledVolume3D.CornerPosition3D g, SampledVolume3D.CornerPosition3D h)
            {
                A = a; B = b; C = c; D = d; E = e; F = f; G = g; H = h;
            }

            public bool Equals(CornerTupleKey other) =>
                A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C) && D.Equals(other.D) &&
                E.Equals(other.E) && F.Equals(other.F) && G.Equals(other.G) && H.Equals(other.H);

            public override bool Equals(object obj) => obj is CornerTupleKey k && Equals(k);
            public override int GetHashCode() => HashCode.Combine(A, B, C, D, E, F, G, H);
        }

        internal readonly ConcurrentDictionary<CornerTupleKey, int> CornerIndexMap
            = new ConcurrentDictionary<CornerTupleKey, int>();

        // SoA frame storage
        private readonly List<double> _frameU = new List<double>();
        private readonly List<double> _frameV = new List<double>();
        private readonly List<double> _frameW = new List<double>();
        private readonly List<LinFloat64Vector3D> _framePoints = new List<LinFloat64Vector3D>();
        private readonly List<LinFloat64Vector3D> _frameGradients = new List<LinFloat64Vector3D>();
        private readonly List<double> _frameValues = new List<double>();

        // Map from single canonical corner (represented as 8-tuple where all elements equal) -> frame index
        private readonly ConcurrentDictionary<CornerTupleKey, int> _frameIndexMap = new ConcurrentDictionary<CornerTupleKey, int>();

        private readonly object _frameLock = new object();

        private readonly List<SampledVolume3D.Corner> _cornerList = new List<SampledVolume3D.Corner>();
        private int _cornerListCount = 0;

        private readonly List<CornerTupleKey> _cornerTupleList = new List<CornerTupleKey>();
        private readonly List<SampledVolume3D.LeafDescriptor> _leafDescriptors = new List<SampledVolume3D.LeafDescriptor>();

        private readonly double _cosMaxGradientAngle;

        public int TreeLevelCount { get; private set; } = 0;

        public IReadOnlyList<SampledVolume3D.LeafDescriptor> LeafDescriptors => _leafDescriptors;
        public IReadOnlyList<SampledVolume3D.Corner> CornerList => _cornerList;

        public AdaptiveVolumeSampler3D(
            IVolumeEvaluator evaluator,
            (double MinU, double MaxU, double MinV, double MaxV, double MinW, double MaxW) parameterRange,
            AdaptiveVolumeSamplingOptions? options = null)
        {
            _evaluator = evaluator ?? throw new ArgumentNullException(nameof(evaluator));
            _paramRange = parameterRange;
            _options = options ?? new AdaptiveVolumeSamplingOptions();
            _cosMaxGradientAngle = Math.Cos(_options.MaxGradientAngleRadians);
        }

        public AdaptiveVolumeSampler3D Generate()
        {
            ResetStorage();

            // root corners at level 0; segments are either 0 or 1
            var c000 = GetOrAddCornerPosition(0, 0, 0, 0);
            var c001 = GetOrAddCornerPosition(0, 0, 0, 1);
            var c010 = GetOrAddCornerPosition(0, 0, 1, 0);
            var c011 = GetOrAddCornerPosition(0, 0, 1, 1);
            var c100 = GetOrAddCornerPosition(0, 1, 0, 0);
            var c101 = GetOrAddCornerPosition(0, 1, 0, 1);
            var c110 = GetOrAddCornerPosition(0, 1, 1, 0);
            var c111 = GetOrAddCornerPosition(0, 1, 1, 1);

            SubdivideRecursive(0, 0, 0, 0, c000, c001, c010, c011, c100, c101, c110, c111);

            BuildCornerTupleListFromLeafDescriptors();

            EnsureCornersFromTupleList();

            return this;
        }

        internal SampledVolume3D.CornerPosition3D GetOrAddCornerPosition(int level, int segU, int segV, int segW)
        {
            var pos = new SampledVolume3D.CornerPosition3D(level, segU, segV, segW);
            if (_cornerPositionSet.TryGetValue(pos, out var existing)) return existing;
            _cornerPositionSet.Add(pos);
            return pos;
        }

        private void BuildCornerTupleListFromLeafDescriptors()
        {
            foreach (var leaf in _leafDescriptors)
            {
                AddCornerTupleIfMissing(new CornerTupleKey(leaf.C000, leaf.C001, leaf.C010, leaf.C011, leaf.C100, leaf.C101, leaf.C110, leaf.C111));
            }
        }

        private void AddCornerTupleIfMissing(CornerTupleKey tuple)
        {
            if (_frameIndexMap.ContainsKey(tuple)) return;
            lock (_cornerTupleList) _cornerTupleList.Add(tuple);
        }

        private void EnsureCornersFromTupleList()
        {
            if (_options.ParallelEvaluation)
                Parallel.ForEach(_cornerTupleList, EnsureCornerTupleInMap);
            else
                foreach (var t in _cornerTupleList) EnsureCornerTupleInMap(t);
        }

        private void EnsureCornerTupleInMap(CornerTupleKey tuple)
        {
            if (CornerIndexMap.ContainsKey(tuple)) return;

            // choose tuple.A as representative position
            var repPos = tuple.A;

            var frameIndex = GetFrameIndexForPos(repPos);

            VolumeFrame frame;
            lock (_frameLock)
            {
                var u = _frameU[frameIndex];
                var v = _frameV[frameIndex];
                var w = _frameW[frameIndex];
                var p = _framePoints[frameIndex];
                var g = _frameGradients[frameIndex];
                var val = _frameValues[frameIndex];
                frame = new VolumeFrame(u, v, w, p, g, val);
            }

            var idx = Interlocked.Increment(ref _cornerListCount) - 1;

            lock (_cornerList)
            {
                if (idx == _cornerList.Count)
                    _cornerList.Add(new SampledVolume3D.Corner(this, idx, frame, repPos));
                else if (idx < _cornerList.Count)
                    _cornerList[idx] = new SampledVolume3D.Corner(this, idx, frame, repPos);
                else
                {
                    while (_cornerList.Count < idx) _cornerList.Add(null!);
                    _cornerList.Add(new SampledVolume3D.Corner(this, idx, frame, repPos));
                }

                CornerIndexMap[tuple] = idx;
            }
        }

        private void SubdivideRecursive(
            int level,
            int cellU,
            int cellV,
            int cellW,
            SampledVolume3D.CornerPosition3D c000,
            SampledVolume3D.CornerPosition3D c001,
            SampledVolume3D.CornerPosition3D c010,
            SampledVolume3D.CornerPosition3D c011,
            SampledVolume3D.CornerPosition3D c100,
            SampledVolume3D.CornerPosition3D c101,
            SampledVolume3D.CornerPosition3D c110,
            SampledVolume3D.CornerPosition3D c111)
        {
            var nextLevel = level + 1;
            var doubleU = 2 * cellU;
            var doubleV = 2 * cellV;
            var doubleW = 2 * cellW;

            // a helper to compute midpoint canonical corner at nextLevel by averaging parameters then converting to segment indices
            SampledVolume3D.CornerPosition3D Mid(SampledVolume3D.CornerPosition3D a, SampledVolume3D.CornerPosition3D b)
            {
                var (ua, va, wa) = a.GetParameterValues(_paramRange.MinU, _paramRange.MaxU, _paramRange.MinV, _paramRange.MaxV, _paramRange.MinW, _paramRange.MaxW);
                var (ub, vb, wb) = b.GetParameterValues(_paramRange.MinU, _paramRange.MaxU, _paramRange.MinV, _paramRange.MaxV, _paramRange.MinW, _paramRange.MaxW);
                var um = 0.5 * (ua + ub);
                var vm = 0.5 * (va + vb);
                var wm = 0.5 * (wa + wb);

                // Convert to integer segments at nextLevel
                var segCount = 1 << nextLevel;
                var segU = (int)Math.Round((um - _paramRange.MinU) / (_paramRange.MaxU - _paramRange.MinU) * segCount);
                var segV = (int)Math.Round((vm - _paramRange.MinV) / (_paramRange.MaxV - _paramRange.MinV) * segCount);
                var segW = (int)Math.Round((wm - _paramRange.MinW) / (_paramRange.MaxW - _paramRange.MinW) * segCount);

                return GetOrAddCornerPosition(nextLevel, segU, segV, segW);
            }

            // center and midpoints needed for checks
            var center = Mid(c000, c111);

            // frame indices for corners (requests frames lazily)
            var f000 = GetFrameIndexForPos(c000);
            var f001 = GetFrameIndexForPos(c001);
            var f010 = GetFrameIndexForPos(c010);
            var f011 = GetFrameIndexForPos(c011);
            var f100 = GetFrameIndexForPos(c100);
            var f101 = GetFrameIndexForPos(c101);
            var f110 = GetFrameIndexForPos(c110);
            var f111 = GetFrameIndexForPos(c111);

            // edge midpoints (U direction)
            var mU000 = Mid(c000, c100);
            var mU001 = Mid(c001, c101);
            var mU010 = Mid(c010, c110);
            var mU011 = Mid(c011, c111);

            // V direction
            var mV000 = Mid(c000, c010);
            var mV001 = Mid(c001, c011);
            var mV100 = Mid(c100, c110);
            var mV101 = Mid(c101, c111);

            // W direction
            var mW000 = Mid(c000, c001);
            var mW010 = Mid(c010, c011);
            var mW100 = Mid(c100, c101);
            var mW110 = Mid(c110, c111);

            var f_mU000 = GetFrameIndexForPos(mU000);
            var f_mU001 = GetFrameIndexForPos(mU001);
            var f_mU010 = GetFrameIndexForPos(mU010);
            var f_mU011 = GetFrameIndexForPos(mU011);

            var f_mV000 = GetFrameIndexForPos(mV000);
            var f_mV001 = GetFrameIndexForPos(mV001);
            var f_mV100 = GetFrameIndexForPos(mV100);
            var f_mV101 = GetFrameIndexForPos(mV101);

            var f_mW000 = GetFrameIndexForPos(mW000);
            var f_mW010 = GetFrameIndexForPos(mW010);
            var f_mW100 = GetFrameIndexForPos(mW100);
            var f_mW110 = GetFrameIndexForPos(mW110);

            var fCenter = GetFrameIndexForPos(center);

            var needSplit = false;

            // Quick gradient-angle checks on a representative set of edges
            if (GradientsDiffer(GetFrameGradient(f000), GetFrameGradient(f100)) ||
                GradientsDiffer(GetFrameGradient(f000), GetFrameGradient(f010)) ||
                GradientsDiffer(GetFrameGradient(f000), GetFrameGradient(f001)) ||
                GradientsDiffer(GetFrameGradient(f111), GetFrameGradient(f110)) ||
                GradientsDiffer(GetFrameGradient(f111), GetFrameGradient(f101)) ||
                GradientsDiffer(GetFrameGradient(f111), GetFrameGradient(f011)))
            {
                needSplit = true;
            }

            // chord error checks
            if (!needSplit)
            {
                if (PointTooFar(GetFramePoint(f000), GetFramePoint(f100), GetFramePoint(f_mU000)) ||
                    PointTooFar(GetFramePoint(f001), GetFramePoint(f101), GetFramePoint(f_mU001)) ||
                    PointTooFar(GetFramePoint(f010), GetFramePoint(f110), GetFramePoint(f_mU010)) ||
                    PointTooFar(GetFramePoint(f011), GetFramePoint(f111), GetFramePoint(f_mU011)) ||

                    PointTooFar(GetFramePoint(f000), GetFramePoint(f010), GetFramePoint(f_mV000)) ||
                    PointTooFar(GetFramePoint(f001), GetFramePoint(f011), GetFramePoint(f_mV001)) ||
                    PointTooFar(GetFramePoint(f100), GetFramePoint(f110), GetFramePoint(f_mV100)) ||
                    PointTooFar(GetFramePoint(f101), GetFramePoint(f111), GetFramePoint(f_mV101)) ||

                    PointTooFar(GetFramePoint(f000), GetFramePoint(f001), GetFramePoint(f_mW000)) ||
                    PointTooFar(GetFramePoint(f010), GetFramePoint(f011), GetFramePoint(f_mW010)) ||
                    PointTooFar(GetFramePoint(f100), GetFramePoint(f101), GetFramePoint(f_mW100)) ||
                    PointTooFar(GetFramePoint(f110), GetFramePoint(f111), GetFramePoint(f_mW110)) ||

                    PointTooFarTrilinearCornerSet(
                        GetFramePoint(f000), GetFramePoint(f001), GetFramePoint(f010), GetFramePoint(f011),
                        GetFramePoint(f100), GetFramePoint(f101), GetFramePoint(f110), GetFramePoint(f111),
                        GetFramePoint(fCenter)
                    ))
                {
                    needSplit = true;
                }
            }

            if (_options.MinDepth > level) needSplit = true;
            if (level >= _options.MaxDepth) needSplit = false;

            if (needSplit)
            {
                // create 8 children by computing their corner positions with integer arithmetic (robust)
                for (var cu = 0; cu <= 1; cu++)
                for (var cv = 0; cv <= 1; cv++)
                for (var cw = 0; cw <= 1; cw++)
                {
                    SampledVolume3D.CornerPosition3D ChildCorner(int iu, int iv, int iw)
                    {
                        var segU = (cellU << 1) + cu * (1 << iu); // placeholder; we will compute properly below
                        // simpler: compute child's corner global segment indices directly:
                        var childSegU = (cellU << 1) + iu;
                        var childSegV = (cellV << 1) + iv;
                        var childSegW = (cellW << 1) + iw;
                        return GetOrAddCornerPosition(nextLevel, childSegU, childSegV, childSegW);
                    }

                    var p000 = ChildCorner(0, 0, 0);
                    var p001 = ChildCorner(0, 0, 1);
                    var p010 = ChildCorner(0, 1, 0);
                    var p011 = ChildCorner(0, 1, 1);
                    var p100 = ChildCorner(1, 0, 0);
                    var p101 = ChildCorner(1, 0, 1);
                    var p110 = ChildCorner(1, 1, 0);
                    var p111 = ChildCorner(1, 1, 1);

                    SubdivideRecursive(nextLevel, (cellU << 1) + cu, (cellV << 1) + cv, (cellW << 1) + cw,
                        p000, p001, p010, p011, p100, p101, p110, p111);
                }

                return;
            }

            // create leaf descriptor
            TreeLevelCount = Math.Max(TreeLevelCount, level);
            _leafDescriptors.Add(new SampledVolume3D.LeafDescriptor(level, cellU, cellV, cellW,
                c000, c001, c010, c011, c100, c101, c110, c111));
        }

        private bool GradientsDiffer(LinFloat64Vector3D a, LinFloat64Vector3D b)
        {
            var na = a.L2Norm() == 0 ? a : a.VectorDivideByENorm();
            var nb = b.L2Norm() == 0 ? b : b.VectorDivideByENorm();
            return na.VectorDot(nb) < _cosMaxGradientAngle;
        }

        private bool PointTooFar(LinFloat64Vector3D a, LinFloat64Vector3D b, LinFloat64Vector3D mid)
        {
            var avg = 0.5 * (a + b);
            var diff = mid - avg;
            return diff.L2Norm() > _options.RelativeChordError;
        }

        private bool PointTooFarTrilinearCornerSet(LinFloat64Vector3D p000, LinFloat64Vector3D p001, LinFloat64Vector3D p010, LinFloat64Vector3D p011, LinFloat64Vector3D p100, LinFloat64Vector3D p101, LinFloat64Vector3D p110, LinFloat64Vector3D p111, LinFloat64Vector3D center)
        {
            var triCenter = (p000 + p001 + p010 + p011 + p100 + p101 + p110 + p111) / 8.0;
            var diff = center - triCenter;
            return diff.L2Norm() > _options.RelativeChordError;
        }

        /// <summary>
        /// Get or create frame index for a canonical single corner position.
        /// </summary>
        private int GetFrameIndexForPos(SampledVolume3D.CornerPosition3D pos)
        {
            var key = new CornerTupleKey(pos, pos, pos, pos, pos, pos, pos, pos);

            if (_frameIndexMap.TryGetValue(key, out var existing)) return existing;

            var (u, v, w) = pos.GetParameterValues(_paramRange.MinU, _paramRange.MaxU, _paramRange.MinV, _paramRange.MaxV, _paramRange.MinW, _paramRange.MaxW);

            // Evaluate outside lock.
            var (point, gradient, value) = _evaluator.GetSample(u, v, w);

            var gradUnit = gradient.L2Norm() == 0 ? gradient : gradient.VectorDivideByENorm();

            lock (_frameLock)
            {
                if (_frameIndexMap.TryGetValue(key, out existing)) return existing;
                var index = _framePoints.Count;
                _frameU.Add(u);
                _frameV.Add(v);
                _frameW.Add(w);
                _framePoints.Add(point);
                _frameGradients.Add(gradUnit);
                _frameValues.Add(value);
                _frameIndexMap[key] = index;
                return index;
            }
        }

        private LinFloat64Vector3D GetFramePoint(int index) => _framePoints[index];
        private LinFloat64Vector3D GetFrameGradient(int index) => _frameGradients[index];
        private double GetFrameValue(int index) => _frameValues[index];

        private void ResetStorage()
        {
            _frameU.Clear();
            _frameV.Clear();
            _frameW.Clear();
            _framePoints.Clear();
            _frameGradients.Clear();
            _frameValues.Clear();

            _cornerPositionSet.Clear();
            CornerIndexMap.Clear();
            _cornerTupleList.Clear();
            _leafDescriptors.Clear();
            _frameIndexMap.Clear();
            _cornerList.Clear();
            _cornerListCount = 0;
            TreeLevelCount = 0;
        }

        /// <summary>
        /// Get or create a corner index for a canonical single corner (we create a tuple of 8 same positions).
        /// This aligns with the internal maps' tuple-key shape while letting callers ask for a single corner.
        /// </summary>
        internal int GetOrAddCornerIndex(
            (SampledVolume3D.CornerPosition3D, SampledVolume3D.CornerPosition3D, SampledVolume3D.CornerPosition3D, SampledVolume3D.CornerPosition3D, SampledVolume3D.CornerPosition3D, SampledVolume3D.CornerPosition3D, SampledVolume3D.CornerPosition3D, SampledVolume3D.CornerPosition3D) tuple)
        {
            var key = new CornerTupleKey(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7, tuple.Item8);
            if (CornerIndexMap.TryGetValue(key, out var idx)) return idx;

            // Representative corner:
            var rep = key.A;
            var frameIndex = GetFrameIndexForPos(rep);

            VolumeFrame frame;
            lock (_frameLock)
            {
                frame = new VolumeFrame(
                    _frameU[frameIndex],
                    _frameV[frameIndex],
                    _frameW[frameIndex],
                    _framePoints[frameIndex],
                    _frameGradients[frameIndex],
                    _frameValues[frameIndex]
                );
            }

            var newIdx = Interlocked.Increment(ref _cornerListCount) - 1;
            lock (_cornerList)
            {
                if (newIdx == _cornerList.Count)
                    _cornerList.Add(new SampledVolume3D.Corner(this, newIdx, frame, rep));
                else if (newIdx < _cornerList.Count)
                    _cornerList[newIdx] = new SampledVolume3D.Corner(this, newIdx, frame, rep);
                else
                {
                    while (_cornerList.Count < newIdx) _cornerList.Add(null!);
                    _cornerList.Add(new SampledVolume3D.Corner(this, newIdx, frame, rep));
                }

                CornerIndexMap[key] = newIdx;
            }

            return newIdx;
        }
    }
}
