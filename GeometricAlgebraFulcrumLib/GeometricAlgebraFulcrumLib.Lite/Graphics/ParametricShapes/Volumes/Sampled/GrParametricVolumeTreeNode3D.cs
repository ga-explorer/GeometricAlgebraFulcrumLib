using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space3D;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space3D.Immutable;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using Float64Vector3DAffineUtils = GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D.Float64Vector3DAffineUtils;

// ReSharper disable InconsistentNaming

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.ParametricShapes.Volumes.Sampled
{
    public abstract class GrParametricVolumeTreeNode3D:
        IReadOnlyCollection<GrParametricVolumeTreeNode3D>
    {
        public GrParametricVolumeTree3D ParentTree { get; }

        public GrParametricVolumeTreeBranch3D ParentBranch { get; }
        
        public IEnumerable<GrParametricVolumeTreeBranch3D> ParentBranches 
        {
            get
            {
                for (var node = ParentBranch; node is not null; node = node.ParentBranch)
                    yield return node;
            }
        }

        public bool IsLeaf 
            => this is GrParametricVolumeTreeLeaf3D;

        public bool IsBranch 
            => this is GrParametricVolumeTreeBranch3D;

        public abstract int Count { get; }

        public bool IsRoot
            => ParentBranch is null;
        
        public bool IsChild
            => ParentBranch is not null;

        public int Level { get; }

        //public ulong CellCode { get; }

        public int CellIndex1 { get; }

        public int CellIndex2 { get; }

        public int CellIndex3 { get; }

        public bool IsChild000 
            => IsChild && 
               (CellIndex1 & 1) == 0 && 
               (CellIndex2 & 1) == 0 && 
               (CellIndex3 & 1) == 0;

        public bool IsChild001 
            => IsChild && 
               (CellIndex1 & 1) == 0 && 
               (CellIndex2 & 1) == 0 && 
               (CellIndex3 & 1) == 1;

        public bool IsChild010 
            => IsChild && 
               (CellIndex1 & 1) == 0 && 
               (CellIndex2 & 1) == 1 && 
               (CellIndex3 & 1) == 0;

        public bool IsChild011 
            => IsChild && 
               (CellIndex1 & 1) == 0 && 
               (CellIndex2 & 1) == 1 && 
               (CellIndex3 & 1) == 1;
        
        public bool IsChild100 
            => IsChild && 
               (CellIndex1 & 1) == 1 && 
               (CellIndex2 & 1) == 0 && 
               (CellIndex3 & 1) == 0;

        public bool IsChild101 
            => IsChild && 
               (CellIndex1 & 1) == 1 && 
               (CellIndex2 & 1) == 0 && 
               (CellIndex3 & 1) == 1;

        public bool IsChild110 
            => IsChild && 
               (CellIndex1 & 1) == 1 && 
               (CellIndex2 & 1) == 1 && 
               (CellIndex3 & 1) == 0;

        public bool IsChild111 
            => IsChild && 
               (CellIndex1 & 1) == 1 && 
               (CellIndex2 & 1) == 1 && 
               (CellIndex3 & 1) == 1;
        
        public bool IsChild00X 
            => IsChild && 
               (CellIndex1 & 1) == 0 && 
               (CellIndex2 & 1) == 0;
        
        public bool IsChild01X 
            => IsChild && 
               (CellIndex1 & 1) == 0 && 
               (CellIndex2 & 1) == 1;
        
        public bool IsChild10X 
            => IsChild && 
               (CellIndex1 & 1) == 1 && 
               (CellIndex2 & 1) == 0;
        
        public bool IsChild11X 
            => IsChild && 
               (CellIndex1 & 1) == 1 && 
               (CellIndex2 & 1) == 1;
        
        public bool IsChild0X0 
            => IsChild && 
               (CellIndex1 & 1) == 0 && 
               (CellIndex3 & 1) == 0;
        
        public bool IsChild0X1 
            => IsChild && 
               (CellIndex1 & 1) == 0 && 
               (CellIndex3 & 1) == 1;
        
        public bool IsChild1X0 
            => IsChild && 
               (CellIndex1 & 1) == 1 && 
               (CellIndex3 & 1) == 0;
        
        public bool IsChild1X1 
            => IsChild && 
               (CellIndex1 & 1) == 1 && 
               (CellIndex3 & 1) == 1;
        
        public bool IsChildX00 
            => IsChild && 
               (CellIndex2 & 1) == 0 && 
               (CellIndex3 & 1) == 0;
        
        public bool IsChildX01 
            => IsChild && 
               (CellIndex2 & 1) == 0 && 
               (CellIndex3 & 1) == 1;
        
        public bool IsChildX10 
            => IsChild && 
               (CellIndex2 & 1) == 1 && 
               (CellIndex3 & 1) == 0;
        
        public bool IsChildX11 
            => IsChild && 
               (CellIndex2 & 1) == 1 && 
               (CellIndex3 & 1) == 1;

        public bool IsChild0XX 
            => IsChild && 
               (CellIndex1 & 1) == 0;
        
        public bool IsChild1XX
            => IsChild && 
               (CellIndex1 & 1) == 1;

        public bool IsChildX0X 
            => IsChild && 
               (CellIndex2 & 1) == 0;
        
        public bool IsChildX1X
            => IsChild && 
               (CellIndex2 & 1) == 1;
        
        public bool IsChildXX0 
            => IsChild && 
               (CellIndex3 & 1) == 0;
        
        public bool IsChildXX1
            => IsChild && 
               (CellIndex3 & 1) == 1;

        public int FrameIndex000 
            => Corner000.Index;

        public int FrameIndex001 
            => Corner001.Index;

        public int FrameIndex010 
            => Corner010.Index;

        public int FrameIndex011 
            => Corner011.Index;
        
        public int FrameIndex100 
            => Corner100.Index;

        public int FrameIndex101 
            => Corner101.Index;

        public int FrameIndex110 
            => Corner110.Index;

        public int FrameIndex111 
            => Corner111.Index;

        public GrParametricVolumeTreeCorner3D Corner000 { get; }

        public GrParametricVolumeTreeCorner3D Corner001 { get; }

        public GrParametricVolumeTreeCorner3D Corner010 { get; }

        public GrParametricVolumeTreeCorner3D Corner011 { get; }
        
        public GrParametricVolumeTreeCorner3D Corner100 { get; }

        public GrParametricVolumeTreeCorner3D Corner101 { get; }

        public GrParametricVolumeTreeCorner3D Corner110 { get; }

        public GrParametricVolumeTreeCorner3D Corner111 { get; }

        public Triplet<int> GridIndex000
            => Corner000.GridIndex;

        public Triplet<int> GridIndex001
            => Corner001.GridIndex;

        public Triplet<int> GridIndex010
            => Corner010.GridIndex;

        public Triplet<int> GridIndex011
            => Corner011.GridIndex;
        
        public Triplet<int> GridIndex100
            => Corner100.GridIndex;

        public Triplet<int> GridIndex101
            => Corner101.GridIndex;

        public Triplet<int> GridIndex110
            => Corner110.GridIndex;

        public Triplet<int> GridIndex111
            => Corner111.GridIndex;

        public GrParametricVolumeLocalFrame3D Frame000
            => Corner000.Frame;

        public GrParametricVolumeLocalFrame3D Frame001
            => Corner001.Frame;

        public GrParametricVolumeLocalFrame3D Frame010
            => Corner010.Frame;

        public GrParametricVolumeLocalFrame3D Frame011
            => Corner011.Frame;
        
        public GrParametricVolumeLocalFrame3D Frame100
            => Corner100.Frame;

        public GrParametricVolumeLocalFrame3D Frame101
            => Corner101.Frame;

        public GrParametricVolumeLocalFrame3D Frame110
            => Corner110.Frame;

        public GrParametricVolumeLocalFrame3D Frame111
            => Corner111.Frame;

        public double MinParameterValue1
            => Frame000.ParameterValue.Item1;

        public double MinParameterValue2
            => Frame000.ParameterValue.Item2;
        
        public double MinParameterValue3
            => Frame000.ParameterValue.Item3;

        public double MaxParameterValue1
            => Frame111.ParameterValue.Item1;

        public double MaxParameterValue2
            => Frame111.ParameterValue.Item2;
        
        public double MaxParameterValue3
            => Frame111.ParameterValue.Item3;

        public IBoundingBox3D ParameterValueRange 
            => BoundingBox3D.Create(
                MinParameterValue1,
                MinParameterValue2,
                MinParameterValue3,
                MaxParameterValue1,
                MaxParameterValue2,
                MaxParameterValue3
            );

        public IEnumerable<GrParametricVolumeTreeLeaf3D> LeafNodes
        {
            get
            {
                var stack = new Stack<GrParametricVolumeTreeNode3D>();

                stack.Push(this);

                while (stack.Count > 0)
                {
                    var node = stack.Pop();

                    if (node is GrParametricVolumeTreeLeaf3D leafNode)
                    {
                        yield return leafNode;
                        continue;
                    }

                    var branchNode = (GrParametricVolumeTreeBranch3D) node;
                    
                    stack.Push(branchNode.Child111);
                    stack.Push(branchNode.Child110);
                    stack.Push(branchNode.Child101);
                    stack.Push(branchNode.Child100);
                    stack.Push(branchNode.Child011);
                    stack.Push(branchNode.Child010);
                    stack.Push(branchNode.Child001);
                    stack.Push(branchNode.Child000);
                }
            }
        }
        
        public IEnumerable<GrParametricVolumeTreeLeaf3D> LeafNodesXX0
        {
            get
            {
                var stack = new Stack<GrParametricVolumeTreeNode3D>();

                stack.Push(this);

                while (stack.Count > 0)
                {
                    var node = stack.Pop();

                    if (node is GrParametricVolumeTreeLeaf3D leafNode)
                    {
                        if (node.IsChildXX0)
                            yield return leafNode;

                        continue;
                    }

                    var branchNode = (GrParametricVolumeTreeBranch3D) node;

                    stack.Push(branchNode.Child110);
                    stack.Push(branchNode.Child100);
                    stack.Push(branchNode.Child010);
                    stack.Push(branchNode.Child000);
                }
            }
        }
        
        public IEnumerable<GrParametricVolumeTreeLeaf3D> LeafNodesXX1
        {
            get
            {
                var stack = new Stack<GrParametricVolumeTreeNode3D>();

                stack.Push(this);

                while (stack.Count > 0)
                {
                    var node = stack.Pop();

                    if (node is GrParametricVolumeTreeLeaf3D leafNode)
                    {
                        if (node.IsChildXX1)
                            yield return leafNode;

                        continue;
                    }

                    var branchNode = (GrParametricVolumeTreeBranch3D) node;
                    
                    stack.Push(branchNode.Child111);
                    stack.Push(branchNode.Child101);
                    stack.Push(branchNode.Child011);
                    stack.Push(branchNode.Child001);
                }
            }
        }
        
        public IEnumerable<GrParametricVolumeTreeLeaf3D> LeafNodesX0X
        {
            get
            {
                var stack = new Stack<GrParametricVolumeTreeNode3D>();

                stack.Push(this);

                while (stack.Count > 0)
                {
                    var node = stack.Pop();

                    if (node is GrParametricVolumeTreeLeaf3D leafNode)
                    {
                        if (node.IsChildX0X)
                            yield return leafNode;

                        continue;
                    }

                    var branchNode = (GrParametricVolumeTreeBranch3D) node;
                    
                    stack.Push(branchNode.Child101);
                    stack.Push(branchNode.Child100);
                    stack.Push(branchNode.Child001);
                    stack.Push(branchNode.Child000);
                }
            }
        }
        
        public IEnumerable<GrParametricVolumeTreeLeaf3D> LeafNodesX1X
        {
            get
            {
                var stack = new Stack<GrParametricVolumeTreeNode3D>();

                stack.Push(this);

                while (stack.Count > 0)
                {
                    var node = stack.Pop();

                    if (node is GrParametricVolumeTreeLeaf3D leafNode)
                    {
                        if (node.IsChildX1X)
                            yield return leafNode;

                        continue;
                    }

                    var branchNode = (GrParametricVolumeTreeBranch3D) node;
                    
                    stack.Push(branchNode.Child111);
                    stack.Push(branchNode.Child110);
                    stack.Push(branchNode.Child011);
                    stack.Push(branchNode.Child010);
                }
            }
        }

        public IEnumerable<GrParametricVolumeTreeLeaf3D> LeafNodes0XX
        {
            get
            {
                var stack = new Stack<GrParametricVolumeTreeNode3D>();

                stack.Push(this);

                while (stack.Count > 0)
                {
                    var node = stack.Pop();

                    if (node is GrParametricVolumeTreeLeaf3D leafNode)
                    {
                        if (node.IsChild0XX)
                            yield return leafNode;

                        continue;
                    }

                    var branchNode = (GrParametricVolumeTreeBranch3D) node;

                    stack.Push(branchNode.Child011);
                    stack.Push(branchNode.Child010);
                    stack.Push(branchNode.Child001);
                    stack.Push(branchNode.Child000);
                }
            }
        }
        
        public IEnumerable<GrParametricVolumeTreeLeaf3D> LeafNodes1XX
        {
            get
            {
                var stack = new Stack<GrParametricVolumeTreeNode3D>();

                stack.Push(this);

                while (stack.Count > 0)
                {
                    var node = stack.Pop();

                    if (node is GrParametricVolumeTreeLeaf3D leafNode)
                    {
                        if (node.IsChild1XX)
                            yield return leafNode;

                        continue;
                    }

                    var branchNode = (GrParametricVolumeTreeBranch3D) node;

                    stack.Push(branchNode.Child111);
                    stack.Push(branchNode.Child110);
                    stack.Push(branchNode.Child101);
                    stack.Push(branchNode.Child100);
                }
            }
        }


        /// <summary>
        /// Construct root node of tree
        /// </summary>
        /// <param name="parentTree"></param>
        protected GrParametricVolumeTreeNode3D(GrParametricVolumeTree3D parentTree)
        {
            ParentTree = parentTree;
            ParentBranch = null;
            Level = 0;
            CellIndex1 = 0;
            CellIndex2 = 0;
            CellIndex3 = 0;
            //CellCode = 0u;
            ParentTree.TreeLevelCount = 0;

            var position0 = ParentTree.GetOrAddCornerPosition(0, 0);
            var position1 = ParentTree.GetOrAddCornerPosition(0, 1);

            Corner000 = ParentTree.GetOrAddCorner(position0, position0, position0);
            Corner001 = ParentTree.GetOrAddCorner(position0, position0, position1);
            Corner010 = ParentTree.GetOrAddCorner(position0, position1, position0);
            Corner011 = ParentTree.GetOrAddCorner(position0, position1, position1);
            Corner100 = ParentTree.GetOrAddCorner(position1, position0, position0);
            Corner101 = ParentTree.GetOrAddCorner(position1, position0, position1);
            Corner110 = ParentTree.GetOrAddCorner(position1, position1, position0);
            Corner111 = ParentTree.GetOrAddCorner(position1, position1, position1);
        }

        /// <summary>
        /// Construct sub-node of tree
        /// </summary>
        /// <param name="parentBranch"></param>
        /// <param name="isChild1XX"></param>
        /// <param name="isChildX1X"></param>
        /// <param name="isChildXX1"></param>
        protected GrParametricVolumeTreeNode3D(GrParametricVolumeTreeBranch3D parentBranch, bool isChild1XX, bool isChildX1X, bool isChildXX1)
        {
            Debug.Assert(parentBranch.Level < 30);

            ParentTree = parentBranch.ParentTree;
            ParentBranch = parentBranch;
            Level = parentBranch.Level + 1;
            CellIndex1 = (parentBranch.CellIndex1 << 1) | (isChild1XX ? 1 : 0);
            CellIndex2 = (parentBranch.CellIndex2 << 1) | (isChildX1X ? 1 : 0);
            CellIndex3 = (parentBranch.CellIndex3 << 1) | (isChildXX1 ? 1 : 0);

            //CellCode = 0u;
            //for (var i = 0; i < Level; i++)
            //{
            //    CellCode |= (ulong) ((CellIndex3 >> i) & 1) << (3 * i);
            //    CellCode |= (ulong) ((CellIndex2 >> i) & 1) << (3 * i + 1);
            //    CellCode |= (ulong) ((CellIndex1 >> i) & 1) << (3 * i + 2);
            //}

            if (ParentTree.TreeLevelCount < Level)
                ParentTree.TreeLevelCount = Level;

            var position10 = ParentTree.GetOrAddCornerPosition(Level, CellIndex1);
            var position11 = ParentTree.GetOrAddCornerPosition(Level, CellIndex1 + 1);
            var position20 = ParentTree.GetOrAddCornerPosition(Level, CellIndex2);
            var position21 = ParentTree.GetOrAddCornerPosition(Level, CellIndex2 + 1);
            var position30 = ParentTree.GetOrAddCornerPosition(Level, CellIndex3);
            var position31 = ParentTree.GetOrAddCornerPosition(Level, CellIndex3 + 1);

            Corner000 = ParentTree.GetOrAddCorner(position10, position20, position30);
            Corner001 = ParentTree.GetOrAddCorner(position10, position20, position31);
            Corner010 = ParentTree.GetOrAddCorner(position10, position21, position30);
            Corner011 = ParentTree.GetOrAddCorner(position10, position21, position31);
            Corner100 = ParentTree.GetOrAddCorner(position11, position20, position30);
            Corner101 = ParentTree.GetOrAddCorner(position11, position20, position31);
            Corner110 = ParentTree.GetOrAddCorner(position11, position21, position30);
            Corner111 = ParentTree.GetOrAddCorner(position11, position21, position31);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(double parameterValue1, double parameterValue2, double parameterValue3)
        {
            return parameterValue1 >= MinParameterValue1 &&
                   parameterValue1 <= MaxParameterValue1 &&
                   parameterValue2 >= MinParameterValue2 &&
                   parameterValue2 <= MaxParameterValue2 &&
                   parameterValue3 >= MinParameterValue3 &&
                   parameterValue3 <= MaxParameterValue3;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MaxEdgeFramesDistance()
        {
            return GetEdgeFramePairs().Select(p => 
                Float64Vector3DAffineUtils.GetDistanceToPoint(p.Item1.Point, p.Item2.Point)
            ).Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasNearEdgeFrames(GrParametricVolumeTreeOptions3D options)
        {
            var maxDistance = options.MaxEdgeFrameDistance;

            return GetEdgeFramePairs().All(p =>
                p.Item1.ScalarDistance.HasSameSignAs(p.Item2.ScalarDistance) ||
                p.Item1.Point.GetDistanceToPoint(p.Item2.Point) < maxDistance
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsChildOnSide(GrParametricVolumeTreeNodeSide3D side)
        {
            return side switch
            {
                GrParametricVolumeTreeNodeSide3D.Side0XX => IsChild0XX,
                GrParametricVolumeTreeNodeSide3D.Side1XX => IsChild1XX,
                GrParametricVolumeTreeNodeSide3D.SideX0X => IsChildX0X,
                GrParametricVolumeTreeNodeSide3D.SideX1X => IsChildX1X,
                GrParametricVolumeTreeNodeSide3D.SideXX0 => IsChildXX0,
                _ => IsChildXX1
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private GrParametricVolumeTreeNode3D GetSiblingFromSide(GrParametricVolumeTreeNodeSide3D side)
        {
            if (side == GrParametricVolumeTreeNodeSide3D.SideXX0)
            {
                if (IsChild000) return ParentBranch.Child001;
                if (IsChild010) return ParentBranch.Child011;
                if (IsChild100) return ParentBranch.Child101;
                return ParentBranch.Child111;
            }

            if (side == GrParametricVolumeTreeNodeSide3D.SideXX1)
            {
                if (IsChild001) return ParentBranch.Child000;
                if (IsChild011) return ParentBranch.Child010;
                if (IsChild101) return ParentBranch.Child100;
                return ParentBranch.Child110;
            }


            if (side == GrParametricVolumeTreeNodeSide3D.SideX0X)
            {
                if (IsChild000) return ParentBranch.Child010;
                if (IsChild001) return ParentBranch.Child011;
                if (IsChild100) return ParentBranch.Child110;
                return ParentBranch.Child111;
            }

            if (side == GrParametricVolumeTreeNodeSide3D.SideX1X)
            {
                if (IsChild010) return ParentBranch.Child000;
                if (IsChild011) return ParentBranch.Child001;
                if (IsChild110) return ParentBranch.Child100;
                return ParentBranch.Child101;
            }


            if (side == GrParametricVolumeTreeNodeSide3D.Side0XX)
            {
                if (IsChild000) return ParentBranch.Child100;
                if (IsChild001) return ParentBranch.Child101;
                if (IsChild010) return ParentBranch.Child110;
                return ParentBranch.Child111;
            }

            if (IsChild100) return ParentBranch.Child000;
            if (IsChild101) return ParentBranch.Child001;
            if (IsChild110) return ParentBranch.Child010;
            return ParentBranch.Child011;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private GrParametricVolumeTreeNode3D GetNeighborChildFromSide(GrParametricVolumeTreeBranch3D parentNeighbor, GrParametricVolumeTreeNodeSide3D side)
        {
            if (side == GrParametricVolumeTreeNodeSide3D.SideXX0)
            {
                if (IsChild000) return parentNeighbor.Child000;
                if (IsChild010) return parentNeighbor.Child010;
                if (IsChild100) return parentNeighbor.Child100;
                return parentNeighbor.Child110;
            }

            if (side == GrParametricVolumeTreeNodeSide3D.SideXX1)
            {
                if (IsChild001) return parentNeighbor.Child001;
                if (IsChild011) return parentNeighbor.Child011;
                if (IsChild101) return parentNeighbor.Child101;
                return parentNeighbor.Child111;
            }


            if (side == GrParametricVolumeTreeNodeSide3D.SideX0X)
            {
                if (IsChild000) return parentNeighbor.Child000;
                if (IsChild001) return parentNeighbor.Child001;
                if (IsChild100) return parentNeighbor.Child100;
                return parentNeighbor.Child101;
            }

            if (side == GrParametricVolumeTreeNodeSide3D.SideX1X)
            {
                if (IsChild010) return parentNeighbor.Child010;
                if (IsChild011) return parentNeighbor.Child011;
                if (IsChild110) return parentNeighbor.Child110;
                return parentNeighbor.Child111;
            }


            if (side == GrParametricVolumeTreeNodeSide3D.Side0XX)
            {
                if (IsChild000) return parentNeighbor.Child000;
                if (IsChild001) return parentNeighbor.Child001;
                if (IsChild010) return parentNeighbor.Child010;
                return parentNeighbor.Child011;
            }

            if (IsChild100) return parentNeighbor.Child100;
            if (IsChild101) return parentNeighbor.Child101;
            if (IsChild110) return parentNeighbor.Child110;
            return parentNeighbor.Child111;
        }

        /// <summary>
        /// Find the neighbor of this node at the same level from bottom side
        /// </summary>
        /// <returns></returns>
        public bool TryGetNeighbor(GrParametricVolumeTreeNodeSide3D side, bool allowUpperLevelLeafNodes, out GrParametricVolumeTreeNode3D neighborNode)
        {
            neighborNode = null;

            // The root node has no neighbors
            if (IsRoot)
                return false;

            var oppositeSide = side.GetOppositeSide();

            // The neighbor is a sibling with the same parent
            if (IsChildOnSide(oppositeSide))
            {
                neighborNode = GetSiblingFromSide(side);

                return true;
            }

            // Find neighbor branch of parent branch
            if (!ParentBranch.TryGetNeighbor(side, allowUpperLevelLeafNodes, out var parentNodeNeighbor) || parentNodeNeighbor.IsLeaf)
                return false;

            if (parentNodeNeighbor.IsLeaf)
            {
                if (!allowUpperLevelLeafNodes)
                    return false;

                neighborNode = parentNodeNeighbor;
                return true;
            }

            // Find opposite-side child of neighbor branch of parent branch
            neighborNode = GetNeighborChildFromSide(
                (GrParametricVolumeTreeBranch3D) parentNodeNeighbor, 
                oppositeSide
            );

            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GrParametricVolumeTreeCorner3D> GetCornersOnSide(GrParametricVolumeTreeNodeSide3D side)
        {
            switch (side)
            {
                case GrParametricVolumeTreeNodeSide3D.Side0XX:
                    yield return Corner000;
                    yield return Corner001;
                    yield return Corner010;
                    yield return Corner011;
                    break;

                case GrParametricVolumeTreeNodeSide3D.Side1XX:
                    yield return Corner100;
                    yield return Corner101;
                    yield return Corner110;
                    yield return Corner111;
                    break;

                case GrParametricVolumeTreeNodeSide3D.SideX0X:
                    yield return Corner000;
                    yield return Corner001;
                    yield return Corner100;
                    yield return Corner101;
                    break;

                case GrParametricVolumeTreeNodeSide3D.SideX1X:
                    yield return Corner010;
                    yield return Corner011;
                    yield return Corner110;
                    yield return Corner111;
                    break;

                case GrParametricVolumeTreeNodeSide3D.SideXX0:
                    yield return Corner000;
                    yield return Corner010;
                    yield return Corner100;
                    yield return Corner110;
                    break;

                default:
                    yield return Corner001;
                    yield return Corner011;
                    yield return Corner101;
                    yield return Corner111;
                    break;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<Pair<GrParametricVolumeLocalFrame3D>> GetEdgeFramePairs()
        {
            yield return new Pair<GrParametricVolumeLocalFrame3D>(Frame000, Frame001);
            yield return new Pair<GrParametricVolumeLocalFrame3D>(Frame000, Frame010);
            yield return new Pair<GrParametricVolumeLocalFrame3D>(Frame000, Frame100);

            yield return new Pair<GrParametricVolumeLocalFrame3D>(Frame001, Frame011);
            yield return new Pair<GrParametricVolumeLocalFrame3D>(Frame001, Frame101);

            yield return new Pair<GrParametricVolumeLocalFrame3D>(Frame010, Frame011);
            yield return new Pair<GrParametricVolumeLocalFrame3D>(Frame010, Frame110);

            yield return new Pair<GrParametricVolumeLocalFrame3D>(Frame100, Frame101);
            yield return new Pair<GrParametricVolumeLocalFrame3D>(Frame100, Frame110);

            yield return new Pair<GrParametricVolumeLocalFrame3D>(Frame011, Frame111);
            yield return new Pair<GrParametricVolumeLocalFrame3D>(Frame101, Frame111);
            yield return new Pair<GrParametricVolumeLocalFrame3D>(Frame110, Frame111);
        }

        //public abstract IEnumerable<Triplet<Pair<int>>> GetTriangleGridIndexTriplets();

        //public abstract IEnumerable<Triplet<int>> GetTriangleIndexTriplets();


        public abstract IEnumerator<GrParametricVolumeTreeNode3D> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine($"Cell <{CellIndex1}, {CellIndex2}>:")
                .AppendLine($"        Level: {Level}")
                //.AppendLine($"         Code: {CellCode}")
                .AppendLine($"    Corner 000: {GridIndex000}")
                .AppendLine($"    Corner 001: {GridIndex001}")
                .AppendLine($"    Corner 010: {GridIndex010}")
                .AppendLine($"    Corner 011: {GridIndex011}")
                .AppendLine($"    Corner 100: {GridIndex100}")
                .AppendLine($"    Corner 101: {GridIndex101}")
                .AppendLine($"    Corner 110: {GridIndex110}")
                .AppendLine($"    Corner 111: {GridIndex111}")
                .ToString();
        }
    }
}