// File: GrParametricSurfaceTree3D.cs

using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Triangles;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Surfaces.Sampled
{
    /// <summary>
    /// Tree wrapper: builds the sampler, holds corners and leaf nodes,
    /// and exposes public API + mesh generation.
    /// Uses linear search for (u,v) lookup (no grid for memory safety).
    /// </summary>
    public sealed class SampledSurface3D
    {
        #region Nested Types
         public readonly struct CornerPosition : IEquatable<CornerPosition>
        {
            public int Level { get; }

            public int SegmentCount { get; }

            internal CornerPosition(int level, int segmentCount)
            {
                if (level < 0) throw new ArgumentOutOfRangeException(nameof(level));
                if (segmentCount < 0) throw new ArgumentOutOfRangeException(nameof(segmentCount));

                if (segmentCount == 0)
                {
                    Level = 0;
                    SegmentCount = 0;
                    return;
                }

                var tz = BitOperations.TrailingZeroCount((uint)segmentCount);
                if (tz > level) tz = level;

                Level = level - tz;
                SegmentCount = segmentCount >> tz;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public double GetInterpolationValue()
            {
                return SegmentCount / (double)(1L << Level);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public double GetValue(double minValue, double maxValue)
            {
                var alpha = GetInterpolationValue();
                return minValue + alpha * (maxValue - minValue);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int GetGridIndex(int maxTreeLevel)
            {
                var shift = maxTreeLevel - Level;
                return SegmentCount << shift;
            }

            public bool Equals(CornerPosition other) =>
                Level == other.Level && SegmentCount == other.SegmentCount;

            public override bool Equals(object? obj) =>
                obj is CornerPosition c && Equals(c);

            public override int GetHashCode() =>
                HashCode.Combine(Level, SegmentCount);

            public override string ToString() =>
                $"(Level={Level}, Seg={SegmentCount}, α={GetInterpolationValue():G4})";
        }

        public readonly struct LeafDescriptor
        {
            public int Level { get; }

            public int CellIndexU { get; }

            public int CellIndexV { get; }

            public (CornerPosition, CornerPosition) Corner00 { get; }

            public (CornerPosition, CornerPosition) Corner01 { get; }

            public (CornerPosition, CornerPosition) Corner10 { get; }

            public (CornerPosition, CornerPosition) Corner11 { get; }


            public LeafDescriptor(
                int level,
                int cellIndexU,
                int cellIndexV,
                (CornerPosition, CornerPosition) c00,
                (CornerPosition, CornerPosition) c01,
                (CornerPosition, CornerPosition) c10,
                (CornerPosition, CornerPosition) c11
            )
            {
                Level = level;
                CellIndexU = cellIndexU;
                CellIndexV = cellIndexV;
                Corner00 = c00;
                Corner01 = c01;
                Corner10 = c10;
                Corner11 = c11;
            }
        }

        public sealed class Corner
        {
            public AdaptiveSurfaceSampler3D Sampler { get; }

            public int Index { get; }

            public GrParametricSurfaceLocalFrame3D Frame { get; }

            public (CornerPosition, CornerPosition) Position { get; }

            internal Corner(
                AdaptiveSurfaceSampler3D sampler,
                int index,
                GrParametricSurfaceLocalFrame3D frame,
                (CornerPosition, CornerPosition) position
            )
            {
                Sampler = sampler ?? throw new ArgumentNullException(nameof(sampler));
                Index = index;
                Frame = frame ?? throw new ArgumentNullException(nameof(frame));
                Position = position;
            }

            private (int, int)? _gridIndexCache = null;
            /// <summary>
            /// Precomputed grid index for this corner, valid once sampler.TreeLevelCount is known.
            /// </summary>
            public (int, int) GridIndex
            {
                get
                {
                    if (_gridIndexCache.HasValue) 
                        return _gridIndexCache.Value;

                    Debug.Assert(
                        Sampler.TreeLevelCount > 0, 
                        "Sampler must be generated first."
                    );
                    
                    var maxLevel = Sampler.TreeLevelCount;
                    var uIndex = Position.Item1.GetGridIndex(maxLevel);
                    var vIndex = Position.Item2.GetGridIndex(maxLevel);

                    Debug.Assert(uIndex < (1 << maxLevel));
                    Debug.Assert(vIndex < (1 << maxLevel));

                    _gridIndexCache = (uIndex, vIndex);

                    return _gridIndexCache.Value;
                }
            }

            internal void InvalidateGridIndexCache()
            {
                _gridIndexCache = null;
            }

            public override string ToString()
            {
                return $"Corner[{Index}] (u={Frame.ParameterValueU:G3}, v={Frame.ParameterValueV:G3})";
            }
        }

        /// <summary>
        /// Represents an interpolated point/frame sample inside a leaf node.
        /// </summary>
        public sealed class Sample
        {
            public LeafNode LeafNode { get; }

            public double ParameterValue1 { get; }

            public double ParameterValue2 { get; }

            private readonly double _s00, _s01, _s10, _s11;

            internal Sample(LeafNode leafNode, double parameterValue1, double parameterValue2)
            {
                LeafNode = leafNode;

                ParameterValue1 = parameterValue1;
                ParameterValue2 = parameterValue2;

                var u0 = (ParameterValue1 - leafNode.MinParameterValue1) /
                         (leafNode.MaxParameterValue1 - leafNode.MinParameterValue1);
                var v0 = (ParameterValue2 - leafNode.MinParameterValue2) /
                         (leafNode.MaxParameterValue2 - leafNode.MinParameterValue2);

                Debug.Assert(u0 is >= 0d and <= 1d);
                Debug.Assert(v0 is >= 0d and <= 1d);

                _s00 = (1 - u0) * (1 - v0);
                _s01 = (1 - u0) * v0;
                _s10 = u0 * (1 - v0);
                _s11 = u0 * v0;
            }

            public LinFloat64Vector3D GetPoint()
            {
                return _s00 * LeafNode.Frame00.Point +
                       _s01 * LeafNode.Frame01.Point +
                       _s10 * LeafNode.Frame10.Point +
                       _s11 * LeafNode.Frame11.Point;
            }

            public LinFloat64Vector3D GetNormal()
            {
                var normal =
                    _s00 * LeafNode.Frame00.Normal.ToLinVector3D() +
                    _s01 * LeafNode.Frame01.Normal.ToLinVector3D() +
                    _s10 * LeafNode.Frame10.Normal.ToLinVector3D() +
                    _s11 * LeafNode.Frame11.Normal.ToLinVector3D();

                return normal.ToUnitLinVector3D();
            }

            public GrParametricSurfaceLocalFrame3D GetFrame()
            {
                var point =
                    _s00 * LeafNode.Frame00.Point +
                    _s01 * LeafNode.Frame01.Point +
                    _s10 * LeafNode.Frame10.Point +
                    _s11 * LeafNode.Frame11.Point;

                var normal =
                    _s00 * LeafNode.Frame00.Normal.ToLinVector3D() +
                    _s01 * LeafNode.Frame01.Normal.ToLinVector3D() +
                    _s10 * LeafNode.Frame10.Normal.ToLinVector3D() +
                    _s11 * LeafNode.Frame11.Normal.ToLinVector3D();

                return new GrParametricSurfaceLocalFrame3D(
                    ParameterValue1,
                    ParameterValue2,
                    point,
                    normal.ToUnitLinVector3D()
                );
            }
        }

        public sealed class LeafNode
        {
            private const double ContainsTolerance = 1e-12;

            public AdaptiveSurfaceSampler3D Sampler { get; }

            public int LeafDescriptorIndex { get; }

            public int CornerIndex00 { get; }

            public int CornerIndex01 { get; }

            public int CornerIndex10 { get; }

            public int CornerIndex11 { get; }

            public GrParametricSurfaceLocalFrame3D Frame00 =>
                Sampler.CornerList[CornerIndex00].Frame;

            public GrParametricSurfaceLocalFrame3D Frame01 =>
                Sampler.CornerList[CornerIndex01].Frame;

            public GrParametricSurfaceLocalFrame3D Frame10 =>
                Sampler.CornerList[CornerIndex10].Frame;

            public GrParametricSurfaceLocalFrame3D Frame11 =>
                Sampler.CornerList[CornerIndex11].Frame;

            internal LeafNode(
                AdaptiveSurfaceSampler3D sampler,
                int leafDescriptorIndex,
                int corner00Idx,
                int corner01Idx,
                int corner10Idx,
                int corner11Idx
            )
            {
                Sampler = sampler ?? throw new ArgumentNullException(nameof(sampler));
                LeafDescriptorIndex = leafDescriptorIndex;
                CornerIndex00 = corner00Idx;
                CornerIndex01 = corner01Idx;
                CornerIndex10 = corner10Idx;
                CornerIndex11 = corner11Idx;
            }

            // Avoid allocations and LINQ by using Math.Min/Max chain
            public double MinParameterValue1 =>
                Math.Min(Math.Min(Frame00.ParameterValueU, Frame01.ParameterValueU),
                         Math.Min(Frame10.ParameterValueU, Frame11.ParameterValueU));

            public double MaxParameterValue1 =>
                Math.Max(Math.Max(Frame00.ParameterValueU, Frame01.ParameterValueU),
                         Math.Max(Frame10.ParameterValueU, Frame11.ParameterValueU));

            public double MinParameterValue2 =>
                Math.Min(Math.Min(Frame00.ParameterValueV, Frame01.ParameterValueV),
                         Math.Min(Frame10.ParameterValueV, Frame11.ParameterValueV));

            public double MaxParameterValue2 =>
                Math.Max(Math.Max(Frame00.ParameterValueV, Frame01.ParameterValueV),
                         Math.Max(Frame10.ParameterValueV, Frame11.ParameterValueV));

            public bool Contains(double u, double v)
            {
                return u >= MinParameterValue1 - ContainsTolerance && u <= MaxParameterValue1 + ContainsTolerance &&
                       v >= MinParameterValue2 - ContainsTolerance && v <= MaxParameterValue2 + ContainsTolerance;
            }

            public IEnumerable<Triplet<int>> GetTriangleIndexTriplets()
            {
                yield return new Triplet<int>(CornerIndex00, CornerIndex10, CornerIndex11);
                yield return new Triplet<int>(CornerIndex00, CornerIndex11, CornerIndex01);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public IEnumerable<Pair<GrParametricSurfaceLocalFrame3D>> GetEdgeFramePairs()
            {
                yield return new Pair<GrParametricSurfaceLocalFrame3D>(Frame00, Frame01);
                yield return new Pair<GrParametricSurfaceLocalFrame3D>(Frame00, Frame10);
                yield return new Pair<GrParametricSurfaceLocalFrame3D>(Frame01, Frame11);
                yield return new Pair<GrParametricSurfaceLocalFrame3D>(Frame10, Frame11);
            }

            // Inline compute without LINQ allocations
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public double MaxEdgeFramesDistance()
            {
                var d1 = Frame00.Point.GetDistanceToPoint(Frame01.Point);
                var d2 = Frame00.Point.GetDistanceToPoint(Frame10.Point);
                var d3 = Frame01.Point.GetDistanceToPoint(Frame11.Point);
                var d4 = Frame10.Point.GetDistanceToPoint(Frame11.Point);

                return Math.Max(Math.Max(d1, d2), Math.Max(d3, d4));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public LinFloat64PolarAngle MaxEdgeFramesAngle()
            {
                var a1 = Frame00.Normal.GetAngle(Frame01.Normal).RadiansValue;
                var a2 = Frame00.Normal.GetAngle(Frame10.Normal).RadiansValue;
                var a3 = Frame01.Normal.GetAngle(Frame11.Normal).RadiansValue;
                var a4 = Frame10.Normal.GetAngle(Frame11.Normal).RadiansValue;

                var max = Math.Max(Math.Max(a1, a2), Math.Max(a3, a4));
                return max.RadiansToPolarAngle();
            }
        }

        #endregion
        
        private readonly AdaptiveSurfaceSampler3D _sampler;
        private readonly List<LeafNode> _leafNodes = [];

        /// <summary>
        /// The number of segments per grid side for this tree
        /// </summary>
        public int GridSegmentCount
            => 1 << TreeLevelCount;

        public SampledSurface3D(
            IGraphicsParametricSurface3D surface,
            Float64BoundingBox2D parameterRange,
            AdaptiveSurfaceSampler3D.SamplingOptions? options = null
        )
        {
            _sampler = new AdaptiveSurfaceSampler3D(surface, parameterRange, options);
        }

        public int TreeLevelCount
            => _sampler.TreeLevelCount;

        public int CornerCount
            => _sampler.CornerList.Count;

        public IReadOnlyList<Corner> CornerList
            => _sampler.CornerList;

        public IReadOnlyList<LeafDescriptor> LeafDescriptors
            => _sampler.LeafDescriptors;

        public Float64BoundingBox2D ParameterValueRange
            => _sampler.ParameterRange;

        public int LeafNodeCount
            => _leafNodes.Count;

        public IReadOnlyList<LeafNode> LeafNodes
            => _leafNodes.AsReadOnly();

        public SampledSurface3D Generate()
        {
            _sampler.Generate();
            BuildLeafNodes();
            return this;
        }

        private void BuildLeafNodes()
        {
            _leafNodes.Clear();
            _leafNodes.Capacity = _sampler.LeafDescriptors.Count;

            for (var i = 0; i < _sampler.LeafDescriptors.Count; i++)
            {
                var leaf = _sampler.LeafDescriptors[i];

                var c00 = _sampler.GetOrAddCornerIndex(leaf.Corner00);
                var c01 = _sampler.GetOrAddCornerIndex(leaf.Corner01);
                var c10 = _sampler.GetOrAddCornerIndex(leaf.Corner10);
                var c11 = _sampler.GetOrAddCornerIndex(leaf.Corner11);

                var node = new LeafNode(_sampler, i, c00, c01, c10, c11);
                _leafNodes.Add(node);
            }
        }

        public GrVertexListTriangleGeometry3D GenerateGeometry()
        {
            var frames = new GrParametricSurfaceLocalFrame3D[_sampler.CornerList.Count];
            for (var i = 0; i < frames.Length; i++)
                frames[i] = _sampler.CornerList[i].Frame;

            var geometry = new GrVertexListTriangleGeometry3D(frames);

            foreach (var leafNode in _leafNodes)
            {
                geometry.AddTriangle(leafNode.CornerIndex00, leafNode.CornerIndex10, leafNode.CornerIndex11);
                geometry.AddTriangle(leafNode.CornerIndex00, leafNode.CornerIndex11, leafNode.CornerIndex01);
            }

            return geometry;
        }

        public Corner GetCorner(int index)
        {
            return CornerList[index];
        }

        private LeafNode? FindLeafNodeForParameters(double u, double v)
        {
            foreach (var leafNode in _leafNodes)
                if (leafNode.Contains(u, v)) return leafNode;

            return null;
        }

        public GrParametricSurfaceLocalFrame3D GetFrame(double u, double v)
        {
            var leafNode = FindLeafNodeForParameters(u, v);
            if (leafNode != null)
                return new Sample(leafNode, u, v).GetFrame();

            throw new ArgumentOutOfRangeException(nameof(u),
                $"Parameters (u={u}, v={v}) are outside the sampled domain.");
        }

        public bool TryGetFrame(double u, double v, out GrParametricSurfaceLocalFrame3D frame)
        {
            var leafNode = FindLeafNodeForParameters(u, v);
            if (leafNode != null)
            {
                frame = new Sample(leafNode, u, v).GetFrame();
                return true;
            }

            frame = null!;
            return false;
        }

        public Sample GetSample(double u, double v)
        {
            var leafNode = FindLeafNodeForParameters(u, v);
            if (leafNode != null)
                return new Sample(leafNode, u, v);

            throw new ArgumentOutOfRangeException(nameof(u),
                $"Parameters (u={u}, v={v}) are outside the tree's parameter domain.");
        }
    }
}
