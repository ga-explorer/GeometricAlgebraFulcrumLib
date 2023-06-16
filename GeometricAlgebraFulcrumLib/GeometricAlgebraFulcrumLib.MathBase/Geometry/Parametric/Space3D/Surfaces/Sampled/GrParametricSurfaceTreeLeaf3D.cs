using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Primitives;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Surfaces.Sampled
{
    public sealed class GrParametricSurfaceTreeLeaf3D :
        GrParametricSurfaceTreeNode3D
    {
        public override int Count 
            => 0;

        public GrParametricSurfaceTreeLeafEdge3D EdgeX0 { get; }

        public GrParametricSurfaceTreeLeafEdge3D EdgeX1 { get; }

        public GrParametricSurfaceTreeLeafEdge3D Edge0X { get; }

        public GrParametricSurfaceTreeLeafEdge3D Edge1X { get; }

        public bool EdgeDataReady
            => EdgeX0.Count >= 2 &&
               EdgeX1.Count >= 2 &&
               Edge0X.Count >= 2 &&
               Edge1X.Count >= 2;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GrParametricSurfaceTreeLeaf3D(GrParametricSurfaceTreeBranch3D parentBranch, bool isChild1X, bool isChildX1)
            : base(parentBranch, isChild1X, isChildX1)
        {
            EdgeX0 = new GrParametricSurfaceTreeLeafEdge3D(
                this, 
                GrParametricSurfaceTreeNodeSide3D.SideX0
            );
            
            EdgeX1 = new GrParametricSurfaceTreeLeafEdge3D(
                this, 
                GrParametricSurfaceTreeNodeSide3D.SideX1
            );
            
            Edge0X = new GrParametricSurfaceTreeLeafEdge3D(
                this, 
                GrParametricSurfaceTreeNodeSide3D.Side0X
            );

            Edge1X = new GrParametricSurfaceTreeLeafEdge3D(
                this, 
                GrParametricSurfaceTreeNodeSide3D.Side1X
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GrParametricSurfaceTreeBranch3D Split()
        {
            var branchNode = new GrParametricSurfaceTreeBranch3D(
                ParentBranch, 
                IsChild1X, 
                IsChildX1, 
                true
            );

            ParentBranch.SetChild(this);

            return branchNode;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GrParametricSurfaceTreeCorner3D> GetTreeCornersOnSide(GrParametricSurfaceTreeNodeSide3D side)
        {
            switch (side)
            {
                case GrParametricSurfaceTreeNodeSide3D.Side0X:
                    yield return Corner00;
                    yield return Corner01;
                    break;

                case GrParametricSurfaceTreeNodeSide3D.Side1X:
                    yield return Corner10;
                    yield return Corner11;
                    break;

                case GrParametricSurfaceTreeNodeSide3D.SideX0:
                    yield return Corner00;
                    yield return Corner10;
                    break;

                default:
                    yield return Corner01;
                    yield return Corner11;
                    break;
            }
        }
        
        /// <summary>
        /// Find the lower-level leaf node neighbors of this node from the given side
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GrParametricSurfaceTreeLeaf3D> GetDeeperLeafNeighbors(GrParametricSurfaceTreeNodeSide3D side)
        {
            if (!TryGetNeighbor(side, false, out var neighborNode) || neighborNode.IsLeaf)
                return Enumerable.Empty<GrParametricSurfaceTreeLeaf3D>();

            var neighborBranchNode = 
                (GrParametricSurfaceTreeBranch3D) neighborNode;

            return side switch
            {
                GrParametricSurfaceTreeNodeSide3D.SideX0 => 
                    neighborBranchNode.LeafNodesX1,

                GrParametricSurfaceTreeNodeSide3D.SideX1 => 
                    neighborBranchNode.LeafNodesX0,

                GrParametricSurfaceTreeNodeSide3D.Side0X => 
                    neighborBranchNode.LeafNodes1X,

                _ => 
                    neighborBranchNode.LeafNodes0X
            };
        }

        /// <summary>
        /// Find all the leaf node neighbors of this node from the given side
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GrParametricSurfaceTreeLeaf3D> GetLeafNeighbors(GrParametricSurfaceTreeNodeSide3D side)
        {
            if (!TryGetNeighbor(side, true, out var neighborNode))
                return Enumerable.Empty<GrParametricSurfaceTreeLeaf3D>();

            if (neighborNode is GrParametricSurfaceTreeLeaf3D neighborLeafNode)
                return new[] { neighborLeafNode };

            var neighborBranchNode = 
                (GrParametricSurfaceTreeBranch3D) neighborNode;

            return side switch
            {
                GrParametricSurfaceTreeNodeSide3D.SideX0 => 
                    neighborBranchNode.LeafNodesX1,

                GrParametricSurfaceTreeNodeSide3D.SideX1 => 
                    neighborBranchNode.LeafNodesX0,

                GrParametricSurfaceTreeNodeSide3D.Side0X => 
                    neighborBranchNode.LeafNodes1X,

                _ => 
                    neighborBranchNode.LeafNodes0X
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<GrParametricSurfaceTreeLeaf3D> GetLeafNeighbors()
        {
            var leafNeighborsList = new List<GrParametricSurfaceTreeLeaf3D>();

            leafNeighborsList.AddRange(
                GetLeafNeighbors(GrParametricSurfaceTreeNodeSide3D.SideX0)
            );

            leafNeighborsList.AddRange(
                GetLeafNeighbors(GrParametricSurfaceTreeNodeSide3D.SideX1)
            );

            leafNeighborsList.AddRange(
                GetLeafNeighbors(GrParametricSurfaceTreeNodeSide3D.Side0X)
            );

            leafNeighborsList.AddRange(
                GetLeafNeighbors(GrParametricSurfaceTreeNodeSide3D.Side1X)
            );

            return leafNeighborsList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricSurfaceTreeLeaf3D ClearEdgeData()
        {
            EdgeX0.Clear();
            EdgeX1.Clear();
            Edge0X.Clear();
            Edge1X.Clear();

            return this;
        }

        public GrParametricSurfaceTreeLeaf3D GenerateEdgeData()
        {
            EdgeX0.AddCornersFromSide(GrParametricSurfaceTreeNodeSide3D.SideX0, this);
            EdgeX1.AddCornersFromSide(GrParametricSurfaceTreeNodeSide3D.SideX1, this);
            Edge0X.AddCornersFromSide(GrParametricSurfaceTreeNodeSide3D.Side0X, this);
            Edge1X.AddCornersFromSide(GrParametricSurfaceTreeNodeSide3D.Side1X, this);
            
            if (Level == ParentTree.TreeLevelCount)
                return this;

            var leafNeighbors =
                GetDeeperLeafNeighbors(GrParametricSurfaceTreeNodeSide3D.SideX0);

            foreach (var neighborsLeafNode in leafNeighbors)
                EdgeX0.AddCornersFromSide(
                    GrParametricSurfaceTreeNodeSide3D.SideX1, 
                    neighborsLeafNode
                );
            
            leafNeighbors =
                GetDeeperLeafNeighbors(GrParametricSurfaceTreeNodeSide3D.SideX1);

            foreach (var neighborsLeafNode in leafNeighbors)
                EdgeX1.AddCornersFromSide(
                    GrParametricSurfaceTreeNodeSide3D.SideX0, 
                    neighborsLeafNode
                );

            leafNeighbors =
                GetDeeperLeafNeighbors(GrParametricSurfaceTreeNodeSide3D.Side0X);

            foreach (var neighborsLeafNode in leafNeighbors)
                Edge0X.AddCornersFromSide(
                    GrParametricSurfaceTreeNodeSide3D.Side1X, 
                    neighborsLeafNode
                );

            leafNeighbors =
                GetDeeperLeafNeighbors(GrParametricSurfaceTreeNodeSide3D.Side1X);

            foreach (var neighborsLeafNode in leafNeighbors)
                Edge1X.AddCornersFromSide(
                    GrParametricSurfaceTreeNodeSide3D.Side0X, 
                    neighborsLeafNode
                );
            
            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<Triplet<Pair<int>>> GetTriangleGridIndexTriplets(bool clearEdgeDataWhenFinished = false)
        {
            return GetTriangleIndexTriplets(clearEdgeDataWhenFinished)
                .Select(t =>
                    {
                        var (index1, index2, index3) = t;

                        return new Triplet<Pair<int>>(
                            ParentTree.CornerList[index1].GridIndex,
                            ParentTree.CornerList[index2].GridIndex,
                            ParentTree.CornerList[index3].GridIndex
                        );
                    }
            );
        }

        public IEnumerable<Triplet<int>> GetTriangleIndexTriplets(bool clearEdgeDataWhenFinished = false)
        {
            if (!EdgeDataReady)
                GenerateEdgeData();

            Triplet<int> indexTriplet;

            // Generate Lower-right Triangles of leaf cell
            if (EdgeX0.Count == 2 && Edge1X.Count == 2)
            {
                indexTriplet = new Triplet<int>(
                    FrameIndex00,
                    FrameIndex10,
                    FrameIndex11
                );

                Debug.Assert(
                    indexTriplet.IsValidTriangleIndexTriplet(false)
                );

                yield return indexTriplet;
            }
            else if (EdgeX0.Count == 2 && Edge1X.Count > 2)
            {
                var frameIndexList =
                    Edge1X.Skip(1).Select(f => f.Index);

                var index1 = FrameIndex00;
                var index2 = FrameIndex10;

                foreach (var index3 in frameIndexList)
                {
                    indexTriplet = new Triplet<int>(index1, index2, index3);

                    Debug.Assert(
                        indexTriplet.IsValidTriangleIndexTriplet(false)
                    );

                    yield return indexTriplet;

                    index2 = index3;
                }
            }
            else if (EdgeX0.Count > 2 && Edge1X.Count == 2)
            {
                var frameIndexList =
                    EdgeX0.Skip(1).Select(f => f.Index);

                var index1 = FrameIndex00;
                var index3 = FrameIndex11;

                foreach (var index2 in frameIndexList)
                {
                    indexTriplet = new Triplet<int>(index1, index2, index3);

                    Debug.Assert(
                        indexTriplet.IsValidTriangleIndexTriplet(false)
                    );

                    yield return indexTriplet;

                    index1 = index2;
                }
            }
            else
            {
                var frameIndexList =
                    Edge1X.Skip(1).Select(f => f.Index);

                var index1 = FrameIndex00;
                var index2 = FrameIndex10;

                foreach (var index3 in frameIndexList)
                {
                    indexTriplet = new Triplet<int>(index1, index2, index3);

                    Debug.Assert(
                        indexTriplet.IsValidTriangleIndexTriplet(false)
                    );

                    yield return indexTriplet;

                    index2 = index3;
                }

                frameIndexList =
                    EdgeX0.Skip(1).Select(f => f.Index);

                var i1 = FrameIndex00;
                var i3 = Edge1X.Skip(1).First().Index;

                foreach (var i2 in frameIndexList)
                {
                    indexTriplet = new Triplet<int>(i1, i2, i3);

                    Debug.Assert(
                        indexTriplet.IsValidTriangleIndexTriplet(false)
                    );

                    yield return indexTriplet;

                    i1 = i2;
                }
            }

            // Generate Upper-left Triangles of leaf cell
            if (EdgeX1.Count == 2 && Edge0X.Count == 2)
            {
                indexTriplet = new Triplet<int>(
                    FrameIndex00,
                    FrameIndex11,
                    FrameIndex01
                );

                Debug.Assert(
                    indexTriplet.IsValidTriangleIndexTriplet(false)
                );

                yield return indexTriplet;
            }
            else if (EdgeX1.Count == 2 && Edge0X.Count > 2)
            {
                var frameIndexList =
                    Edge0X.Skip(1).Select(f => f.Index);

                var index1 = FrameIndex00;
                var index2 = FrameIndex11;

                foreach (var index3 in frameIndexList)
                {
                    indexTriplet = new Triplet<int>(index1, index2, index3);

                    Debug.Assert(
                        indexTriplet.IsValidTriangleIndexTriplet(false)
                    );

                    yield return indexTriplet;

                    index1 = index3;
                }
            }
            else if (EdgeX1.Count > 2 && Edge0X.Count == 2)
            {
                var frameIndexList =
                    EdgeX1.Skip(1).Select(f => f.Index);

                var index1 = FrameIndex00;
                var index3 = FrameIndex01;

                foreach (var index2 in frameIndexList)
                {
                    indexTriplet = new Triplet<int>(index1, index2, index3);

                    Debug.Assert(
                        indexTriplet.IsValidTriangleIndexTriplet(false)
                    );

                    yield return indexTriplet;

                    index3 = index2;
                }
            }
            else
            {
                var frameIndexList =
                    Edge0X.Skip(1).Select(f => f.Index);

                var index1 = FrameIndex00;
                var index2 = FrameIndex11;

                foreach (var index3 in frameIndexList)
                {
                    indexTriplet = new Triplet<int>(index1, index2, index3);

                    Debug.Assert(
                        indexTriplet.IsValidTriangleIndexTriplet(false)
                    );

                    yield return indexTriplet;

                    index1 = index3;
                }

                frameIndexList =
                    EdgeX1.Skip(1).Select(f => f.Index);

                var i1 = Edge0X.Skip(Edge0X.Count - 2).First().Index;
                var i3 = FrameIndex01;

                foreach (var i2 in frameIndexList)
                {
                    indexTriplet = new Triplet<int>(i1, i2, i3);

                    Debug.Assert(
                        indexTriplet.IsValidTriangleIndexTriplet(false)
                    );

                    yield return indexTriplet;

                    i3 = i2;
                }
            }

            if (clearEdgeDataWhenFinished)
                ClearEdgeData();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerator<GrParametricSurfaceTreeNode3D> GetEnumerator()
        {
            return Enumerable.Empty<GrParametricSurfaceTreeNode3D>().GetEnumerator();
        }
    }
}
