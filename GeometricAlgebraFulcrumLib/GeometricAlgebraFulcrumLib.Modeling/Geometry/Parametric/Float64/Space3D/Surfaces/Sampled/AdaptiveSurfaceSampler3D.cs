// File: ParametricSurfaceSampler3D.cs

using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories;
using System.Collections.Concurrent;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Surfaces.Sampled
{
    public sealed class AdaptiveSurfaceSampler3D
    {
        public sealed class SamplingOptions : 
            Float64AdaptiveSamplingOptions
        {
            public bool ForceBalancedTree { get; set; } = true;

            public bool ParallelFrameEvaluation { get; set; } = true;
        }
        

        private readonly IGraphicsParametricSurface3D _surface;
        private readonly SamplingOptions _options;

        private readonly HashSet<SampledSurface3D.CornerPosition> _cornerPositionSet = [];
        internal readonly ConcurrentDictionary<(SampledSurface3D.CornerPosition, SampledSurface3D.CornerPosition), int> CornerIndexMap = new();
        private readonly List<SampledSurface3D.Corner> _cornerList = [];
        private readonly List<(SampledSurface3D.CornerPosition, SampledSurface3D.CornerPosition)> _cornerPositionList = [];
        private readonly List<SampledSurface3D.LeafDescriptor> _leafDescriptors = [];

        private readonly double _cosMaxEdgeAngle;

        // Frame data stored in SoA format (append-only, protected by _frameLock)
        private readonly List<double> _frameU = [];
        private readonly List<double> _frameV = [];
        private readonly List<LinFloat64Vector3D> _framePoints = [];
        
        // store unit normals to avoid re-normalization
        private readonly List<LinFloat64Vector3D> _frameNormals = [];

        // Map (corner pair) -> index in SoA arrays (thread-safe)
        private readonly ConcurrentDictionary<(SampledSurface3D.CornerPosition, SampledSurface3D.CornerPosition), int> _frameIndexMap = new();

        // lock for appending to SoA lists
        private readonly object _frameLock = new();

        public AdaptiveSurfaceSampler3D(IGraphicsParametricSurface3D surface, Float64BoundingBox2D parameterRange, SamplingOptions? options = null)
        {
            _surface = surface ?? throw new ArgumentNullException(nameof(surface));
            ParameterRange = parameterRange;
            _options = options ?? new SamplingOptions();

            _cosMaxEdgeAngle = Math.Cos(_options.FrameAngleDifference.Radians);
        }

        public IReadOnlyList<SampledSurface3D.Corner> CornerList
            => _cornerList;

        public IReadOnlyList<SampledSurface3D.LeafDescriptor> LeafDescriptors
            => _leafDescriptors;

        public int TreeLevelCount { get; private set; } = 0;

        public Float64BoundingBox2D ParameterRange { get; }

        public AdaptiveSurfaceSampler3D Generate()
        {
            ResetStorage();

            var p00 = (GetOrAddCornerPosition(0, 0), GetOrAddCornerPosition(0, 0));
            var p01 = (GetOrAddCornerPosition(0, 0), GetOrAddCornerPosition(0, 1));
            var p10 = (GetOrAddCornerPosition(0, 1), GetOrAddCornerPosition(0, 0));
            var p11 = (GetOrAddCornerPosition(0, 1), GetOrAddCornerPosition(0, 1));

            SubdivideRecursive(0, 0, 0, p00, p01, p10, p11);

            BuildCornerPositionListFromLeafDescriptors();

            // At this point CornerIndexMap & _cornerList are populated (possibly in parallel)
            return this;
        }

        internal SampledSurface3D.CornerPosition GetOrAddCornerPosition(int level, int segmentCount)
        {
            var pos = new SampledSurface3D.CornerPosition(level, segmentCount);
            if (_cornerPositionSet.TryGetValue(pos, out var existing))
                return existing;

            _cornerPositionSet.Add(pos);
            return pos;
        }

        private void BuildCornerPositionListFromLeafDescriptors()
        {
            // Collect unique corner positions from leaf descriptors
            foreach (var leaf in _leafDescriptors)
            {
                AddCornerPositionIfMissing(leaf.Corner00);
                AddCornerPositionIfMissing(leaf.Corner01);
                AddCornerPositionIfMissing(leaf.Corner10);
                AddCornerPositionIfMissing(leaf.Corner11);
            }

            // Ensure CornerIndexMap and _cornerList are populated.
            // We can evaluate frames in parallel safely because GetFrameIndex is concurrency-safe.
            if (_options.ParallelFrameEvaluation)
            {
                // iterate over unique corner positions
                Parallel.ForEach(_cornerPositionList, EnsureCornerInMap);
            }
            else
            {
                foreach (var pos in _cornerPositionList)
                    EnsureCornerInMap(pos);
            }
        }

        private void AddCornerPositionIfMissing((SampledSurface3D.CornerPosition, SampledSurface3D.CornerPosition) pos)
        {
            // track the list of unique positions for later parallel evaluation
            // avoid duplicates by checking the frame map first
            if (_frameIndexMap.ContainsKey(pos)) return;

            // Also avoid adding duplicates to _cornerPositionList (simple check)
            // It's fine if rare duplicates slip through; EnsureCornerInMap does idempotent add.
            lock (_cornerPositionList)
            {
                _cornerPositionList.Add(pos);
            }
        }

        private void EnsureCornerInMap((SampledSurface3D.CornerPosition, SampledSurface3D.CornerPosition) pos)
        {
            // If another thread already added it, we're done
            if (CornerIndexMap.ContainsKey(pos)) return;

            // Obtain a frame index (thread-safe); this will append to SoA arrays if missing
            var frameIndex = GetFrameIndex(pos);

            // Build GrParametricSurfaceLocalFrame3D from SoA (note: stored normals are already unit)
            GrParametricSurfaceLocalFrame3D frame;
            lock (_frameLock)
            {
                // capture atomically
                var u = _frameU[frameIndex];
                var v = _frameV[frameIndex];
                var point = _framePoints[frameIndex];
                var normal = _frameNormals[frameIndex];

                frame = new GrParametricSurfaceLocalFrame3D(u, v, point, normal);
            }

            // Add corner index entry in a thread-safe manner
            var idx = Interlocked.Increment(ref _cornerListCount) - 1;

            // We must ensure list capacity and thread-safe insertion:
            lock (_cornerList)
            {
                // if idx equals current count, append; otherwise set
                if (idx == _cornerList.Count)
                    _cornerList.Add(new SampledSurface3D.Corner(this, idx, frame, pos));
                else if (idx < _cornerList.Count)
                    _cornerList[idx] = new SampledSurface3D.Corner(this, idx, frame, pos);
                else
                {
                    // Should not happen, but to be robust expand list
                    while (_cornerList.Count < idx) _cornerList.Add(null!);
                    _cornerList.Add(new SampledSurface3D.Corner(this, idx, frame, pos));
                }

                CornerIndexMap[pos] = idx;
            }
        }

        // internal counter used for lock-free index allocation for corner list
        private int _cornerListCount = 0;

        private void SubdivideRecursive(
            int level,
            int cellU,
            int cellV,
            (SampledSurface3D.CornerPosition, SampledSurface3D.CornerPosition) p00,
            (SampledSurface3D.CornerPosition, SampledSurface3D.CornerPosition) p01,
            (SampledSurface3D.CornerPosition, SampledSurface3D.CornerPosition) p10,
            (SampledSurface3D.CornerPosition, SampledSurface3D.CornerPosition) p11
        )
        {
            var nextLevel = level + 1;
            var doubleU = 2 * cellU;
            var doubleV = 2 * cellV;

            // Midpoint corner positions
            var uMid = GetOrAddCornerPosition(nextLevel, doubleU + 1);
            var vMid = GetOrAddCornerPosition(nextLevel, doubleV + 1);

            var pc0 = (uMid, p00.Item2); // bottom edge midpoint
            var pc1 = (uMid, p01.Item2); // top edge midpoint
            var pc2 = (p00.Item1, vMid); // left edge midpoint
            var pc3 = (p10.Item1, vMid); // right edge midpoint
            var pc6 = (uMid, vMid);      // center

            // Frame indices in SoA storage (GetFrameIndex is concurrency-safe)
            var f00 = GetFrameIndex(p00);
            var f01 = GetFrameIndex(p01);
            var f10 = GetFrameIndex(p10);
            var f11 = GetFrameIndex(p11);

            var f0c = GetFrameIndex(pc0);
            var f1c = GetFrameIndex(pc1);
            var fc0 = GetFrameIndex(pc2);
            var fc1 = GetFrameIndex(pc3);
            var fcc = GetFrameIndex(pc6);

            // === Refinement Criteria ===

            var needSplit =
                NormalsDiffer(GetFrameNormal(f00), GetFrameNormal(f0c)) ||
                NormalsDiffer(GetFrameNormal(f0c), GetFrameNormal(f10)) ||
                NormalsDiffer(GetFrameNormal(f01), GetFrameNormal(f1c)) ||
                NormalsDiffer(GetFrameNormal(f1c), GetFrameNormal(f11)) ||
                NormalsDiffer(GetFrameNormal(f00), GetFrameNormal(fc0)) ||
                NormalsDiffer(GetFrameNormal(fc0), GetFrameNormal(f01)) ||
                NormalsDiffer(GetFrameNormal(f10), GetFrameNormal(fc1)) ||
                NormalsDiffer(GetFrameNormal(fc1), GetFrameNormal(f11)) ||
                // Use center frame to detect interior distortion
                NormalsDiffer(GetFrameNormal(fcc), GetFrameNormal(f00)) ||
                NormalsDiffer(GetFrameNormal(fcc), GetFrameNormal(f11));

            // Distance checks
            if (!needSplit &&
                (PointsTooFar(GetFramePoint(f00), GetFramePoint(f0c), GetFramePoint(f10)) ||
                 PointsTooFar(GetFramePoint(f01), GetFramePoint(f1c), GetFramePoint(f11)) ||
                 PointsTooFar(GetFramePoint(f00), GetFramePoint(fc0), GetFramePoint(f01)) ||
                 PointsTooFar(GetFramePoint(f10), GetFramePoint(fc1), GetFramePoint(f11)) ||
                 // Use center point for interior bulge check
                 PointsTooFar(GetFramePoint(f00), GetFramePoint(fcc), GetFramePoint(f11))))
            {
                needSplit = true;
            }

            // Force refinement until MinLevelCount
            if (_options.ForceBalancedTree && level < _options.MinDepth)
                needSplit = true;

            // Stop at MaxLevelCount
            if (level >= _options.MaxDepth)
                needSplit = false;

            // === Recursive Subdivision ===
            if (needSplit)
            {
                SubdivideRecursive(nextLevel, doubleU, doubleV, p00, pc2, pc0, pc6);
                SubdivideRecursive(nextLevel, doubleU + 1, doubleV, pc0, pc6, p10, pc3);
                SubdivideRecursive(nextLevel, doubleU, doubleV + 1, pc2, p01, pc6, pc1);
                SubdivideRecursive(nextLevel, doubleU + 1, doubleV + 1, pc6, pc1, pc3, p11);
                return;
            }

            // Store as leaf
            TreeLevelCount = Math.Max(TreeLevelCount, level);
            _leafDescriptors.Add(new SampledSurface3D.LeafDescriptor(level, cellU, cellV, p00, p01, p10, p11));
        }

        private bool NormalsDiffer(LinFloat64Vector3D a, LinFloat64Vector3D b)
        {
            return a.VectorDot(b) < _cosMaxEdgeAngle;
        }

        private bool PointsTooFar(LinFloat64Vector3D a, LinFloat64Vector3D b, LinFloat64Vector3D c)
        {
            return (b - 0.5 * (a + c)).L2Norm() > _options.RelativeChordError;
        }

        /// <summary>
        /// Thread-safe: returns an index into the SoA frame arrays for the given corner position.
        /// If not present, evaluates the frame using the surface and appends it atomically.
        /// </summary>
        private int GetFrameIndex((SampledSurface3D.CornerPosition, SampledSurface3D.CornerPosition) pos)
        {
            if (_frameIndexMap.TryGetValue(pos, out var existing))
                return existing;

            // We must compute the actual parameter values
            var u = pos.Item1.GetValue(ParameterRange.MinX, ParameterRange.MaxX);
            var v = pos.Item2.GetValue(ParameterRange.MinY, ParameterRange.MaxY);

            // Evaluate frame from surface (potentially expensive)
            var frame = _surface.GetFrame(u, v);

            // Prepare data to append; ensure normal is unit length
            var point = frame.Point;
            var normal = frame.Normal.ToLinVector3D().ToUnitLinVector3D();

            // Lock while appending to the SoA lists to keep indexes consistent
            lock (_frameLock)
            {
                // Double-check another thread didn't already add this pos while we were computing
                if (_frameIndexMap.TryGetValue(pos, out existing))
                    return existing;

                var index = _framePoints.Count;

                _frameU.Add(u);
                _frameV.Add(v);
                _framePoints.Add(point);
                _frameNormals.Add(normal);

                _frameIndexMap[pos] = index;
                return index;
            }
        }

        private LinFloat64Vector3D GetFramePoint(int index)
            => _framePoints[index];

        private LinFloat64Vector3D GetFrameNormal(int index)
            => _frameNormals[index];

        private double GetFrameU(int index)
            => _frameU[index];

        private double GetFrameV(int index)
            => _frameV[index];

        private GrParametricSurfaceLocalFrame3D GetFrame((SampledSurface3D.CornerPosition, SampledSurface3D.CornerPosition) pos)
        {
            var index = GetFrameIndex(pos);

            // No lock required for read since lists are append-only and accesses are within bounds
            return new GrParametricSurfaceLocalFrame3D(
                GetFrameU(index),
                GetFrameV(index),
                GetFramePoint(index),
                GetFrameNormal(index)
            );
        }

        private void ResetStorage()
        {
            _frameU.Clear();
            _frameV.Clear();
            _framePoints.Clear();
            _frameNormals.Clear();

            _cornerPositionSet.Clear();
            CornerIndexMap.Clear();
            _cornerList.Clear();
            _cornerPositionList.Clear();
            _leafDescriptors.Clear();
            _frameIndexMap.Clear();
            _cornerListCount = 0;
            TreeLevelCount = 0;
        }

        internal int GetOrAddCornerIndex((SampledSurface3D.CornerPosition, SampledSurface3D.CornerPosition) position)
        {
            if (CornerIndexMap.TryGetValue(position, out var idx)) return idx;

            // Ensure the frame exists (GetFrameIndex is concurrency-safe)
            var frameIndex = GetFrameIndex(position);

            // Build frame object from SoA
            GrParametricSurfaceLocalFrame3D frame;
            lock (_frameLock)
            {
                frame = new GrParametricSurfaceLocalFrame3D(
                    _frameU[frameIndex],
                    _frameV[frameIndex],
                    _framePoints[frameIndex],
                    _frameNormals[frameIndex]
                );
            }

            // create corner and add it to the cornerList and map in a thread-safe way
            var newIdx = Interlocked.Increment(ref _cornerListCount) - 1;
            lock (_cornerList)
            {
                if (newIdx == _cornerList.Count)
                    _cornerList.Add(new SampledSurface3D.Corner(this, newIdx, frame, position));
                else if (newIdx < _cornerList.Count)
                    _cornerList[newIdx] = new SampledSurface3D.Corner(this, newIdx, frame, position);
                else
                {
                    while (_cornerList.Count < newIdx) _cornerList.Add(null!);
                    _cornerList.Add(new SampledSurface3D.Corner(this, newIdx, frame, position));
                }

                CornerIndexMap[position] = newIdx;
            }

            return newIdx;
        }
    }
}
