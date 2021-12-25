using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.Borders.Space2D.Immutable;

// ReSharper disable InconsistentNaming

namespace GraphicsComposerLib.Geometry.ParametricShapes.Surfaces.Sampled
{
    public abstract class GrParametricSurfaceTreeNode3D:
        IReadOnlyCollection<GrParametricSurfaceTreeNode3D>
    {
        public GrParametricSurfaceTree3D ParentTree { get; }

        public GrParametricSurfaceTreeBranch3D ParentBranch { get; }
        
        public IEnumerable<GrParametricSurfaceTreeBranch3D> ParentBranches 
        {
            get
            {
                for (var node = ParentBranch; node is not null; node = node.ParentBranch)
                    yield return node;
            }
        }

        public bool IsLeaf 
            => this is GrParametricSurfaceTreeLeaf3D;

        public bool IsBranch 
            => this is GrParametricSurfaceTreeBranch3D;

        public abstract int Count { get; }

        public bool IsRoot
            => ParentBranch is null;
        
        public bool IsChild
            => ParentBranch is not null;

        public int Level { get; }

        //public ulong CellCode { get; }

        public int CellIndex1 { get; }

        public int CellIndex2 { get; }

        public bool IsChild00 
            => IsChild && (CellIndex1 & 1) == 0 && (CellIndex2 & 1) == 0;

        public bool IsChild01 
            => IsChild && (CellIndex1 & 1) == 0 && (CellIndex2 & 1) == 1;

        public bool IsChild10 
            => IsChild && (CellIndex1 & 1) == 1 && (CellIndex2 & 1) == 0;

        public bool IsChild11 
            => IsChild && (CellIndex1 & 1) == 1 && (CellIndex2 & 1) == 1;
        
        public bool IsChild0X 
            => IsChild && (CellIndex1 & 1) == 0;
        
        public bool IsChild1X
            => IsChild && (CellIndex1 & 1) == 1;

        public bool IsChildX0 
            => IsChild && (CellIndex2 & 1) == 0;
        
        public bool IsChildX1
            => IsChild && (CellIndex2 & 1) == 1;
        
        public GrParametricSurfaceTreeCorner3D Corner00 { get; }

        public GrParametricSurfaceTreeCorner3D Corner01 { get; }

        public GrParametricSurfaceTreeCorner3D Corner10 { get; }

        public GrParametricSurfaceTreeCorner3D Corner11 { get; }

        public int FrameIndex00 
            => Corner00.Index;

        public int FrameIndex01 
            => Corner01.Index;

        public int FrameIndex10 
            => Corner10.Index;

        public int FrameIndex11 
            => Corner11.Index;

        public Pair<int> GridIndex00
            => Corner00.GridIndex;

        public Pair<int> GridIndex01
            => Corner01.GridIndex;

        public Pair<int> GridIndex10
            => Corner10.GridIndex;

        public Pair<int> GridIndex11
            => Corner11.GridIndex;

        public GrParametricSurfaceLocalFrame3D Frame00
            => Corner00.Frame;

        public GrParametricSurfaceLocalFrame3D Frame01
            => Corner01.Frame;

        public GrParametricSurfaceLocalFrame3D Frame10
            => Corner10.Frame;

        public GrParametricSurfaceLocalFrame3D Frame11
            => Corner11.Frame;

        public double MinParameterValue1
            => Frame00.ParameterValue.Item1;

        public double MinParameterValue2
            => Frame00.ParameterValue.Item2;

        public double MaxParameterValue1
            => Frame11.ParameterValue.Item1;

        public double MaxParameterValue2
            => Frame11.ParameterValue.Item2;

        public BoundingBox2D ParameterValueRange 
            => BoundingBox2D.Create(
                MinParameterValue1,
                MinParameterValue2,
                MaxParameterValue1,
                MaxParameterValue2
            );

        public double ParameterValueRectArea
            => (MaxParameterValue1 - MinParameterValue1) *
               (MaxParameterValue2 - MinParameterValue2);

        public IEnumerable<GrParametricSurfaceTreeLeaf3D> LeafNodes
        {
            get
            {
                var stack = new Stack<GrParametricSurfaceTreeNode3D>();

                stack.Push(this);

                while (stack.Count > 0)
                {
                    var node = stack.Pop();

                    if (node is GrParametricSurfaceTreeLeaf3D leafNode)
                    {
                        yield return leafNode;
                        continue;
                    }

                    var branchNode = (GrParametricSurfaceTreeBranch3D) node;
                    
                    stack.Push(branchNode.Child11);
                    stack.Push(branchNode.Child10);
                    stack.Push(branchNode.Child01);
                    stack.Push(branchNode.Child00);
                }
            }
        }

        public int DeepestLeafNodeLevelX0 
            => LeafNodesX0.Select(n => n.Level).Max();
        
        public int DeepestLeafNodeLevelX1 
            => LeafNodesX1.Select(n => n.Level).Max();
        
        public int DeepestLeafNodeLevel0X 
            => LeafNodes0X.Select(n => n.Level).Max();
        
        public int DeepestLeafNodeLevel1X 
            => LeafNodes1X.Select(n => n.Level).Max();

        public IEnumerable<GrParametricSurfaceTreeLeaf3D> LeafNodesX0
        {
            get
            {
                var stack = new Stack<GrParametricSurfaceTreeNode3D>();

                stack.Push(this);

                while (stack.Count > 0)
                {
                    var node = stack.Pop();

                    if (node is GrParametricSurfaceTreeLeaf3D leafNode)
                    {
                        if (node.IsChildX0)
                            yield return leafNode;

                        continue;
                    }

                    var branchNode = (GrParametricSurfaceTreeBranch3D) node;
                    
                    stack.Push(branchNode.Child10);
                    stack.Push(branchNode.Child00);
                }
            }
        }
        
        public IEnumerable<GrParametricSurfaceTreeLeaf3D> LeafNodesX1
        {
            get
            {
                var stack = new Stack<GrParametricSurfaceTreeNode3D>();

                stack.Push(this);

                while (stack.Count > 0)
                {
                    var node = stack.Pop();

                    if (node is GrParametricSurfaceTreeLeaf3D leafNode)
                    {
                        if (node.IsChildX1)
                            yield return leafNode;

                        continue;
                    }

                    var branchNode = (GrParametricSurfaceTreeBranch3D) node;
                    
                    stack.Push(branchNode.Child11);
                    stack.Push(branchNode.Child01);
                }
            }
        }
        
        public IEnumerable<GrParametricSurfaceTreeLeaf3D> LeafNodes0X
        {
            get
            {
                var stack = new Stack<GrParametricSurfaceTreeNode3D>();

                stack.Push(this);

                while (stack.Count > 0)
                {
                    var node = stack.Pop();

                    if (node is GrParametricSurfaceTreeLeaf3D leafNode)
                    {
                        if (node.IsChild0X)
                            yield return leafNode;

                        continue;
                    }

                    var branchNode = (GrParametricSurfaceTreeBranch3D) node;
                    
                    stack.Push(branchNode.Child01);
                    stack.Push(branchNode.Child00);
                }
            }
        }
        
        public IEnumerable<GrParametricSurfaceTreeLeaf3D> LeafNodes1X
        {
            get
            {
                var stack = new Stack<GrParametricSurfaceTreeNode3D>();

                stack.Push(this);

                while (stack.Count > 0)
                {
                    var node = stack.Pop();

                    if (node is GrParametricSurfaceTreeLeaf3D leafNode)
                    {
                        if (node.IsChild1X)
                            yield return leafNode;

                        continue;
                    }

                    var branchNode = (GrParametricSurfaceTreeBranch3D) node;
                    
                    stack.Push(branchNode.Child11);
                    stack.Push(branchNode.Child10);
                }
            }
        }


        /// <summary>
        /// Construct root node of tree
        /// </summary>
        /// <param name="parentTree"></param>
        protected GrParametricSurfaceTreeNode3D([NotNull] GrParametricSurfaceTree3D parentTree)
        {
            ParentTree = parentTree;
            ParentBranch = null;
            Level = 0;
            CellIndex1 = 0;
            CellIndex2 = 0;

            ParentTree.TreeLevelCount = 0;

            var position0 = ParentTree.GetOrAddCornerPosition(0, 0);
            var position1 = ParentTree.GetOrAddCornerPosition(0, 1);

            Corner00 = ParentTree.GetOrAddCorner(position0, position0);
            Corner01 = ParentTree.GetOrAddCorner(position0, position1);
            Corner10 = ParentTree.GetOrAddCorner(position1, position0);
            Corner11 = ParentTree.GetOrAddCorner(position1, position1);
        }

        /// <summary>
        /// Construct sub-node of tree
        /// </summary>
        /// <param name="parentBranch"></param>
        /// <param name="isChild1X"></param>
        /// <param name="isChildX1"></param>
        protected GrParametricSurfaceTreeNode3D([NotNull] GrParametricSurfaceTreeBranch3D parentBranch, bool isChild1X, bool isChildX1)
        {
            Debug.Assert(parentBranch.Level < 30);

            ParentTree = parentBranch.ParentTree;
            ParentBranch = parentBranch;
            Level = parentBranch.Level + 1;
            CellIndex1 = (parentBranch.CellIndex1 << 1) | (isChild1X ? 1 : 0);
            CellIndex2 = (parentBranch.CellIndex2 << 1) | (isChildX1 ? 1 : 0);

            //CellCode = 0u;
            //for (var i = 0; i < Level; i++)
            //{
            //    CellCode |= (ulong) ((CellIndex2 >> i) & 1) << (2 * i);
            //    CellCode |= (ulong) ((CellIndex1 >> i) & 1) << (2 * i + 1);
            //}

            if (ParentTree.TreeLevelCount < Level)
                ParentTree.TreeLevelCount = Level;

            var position10 = ParentTree.GetOrAddCornerPosition(Level, CellIndex1);
            var position11 = ParentTree.GetOrAddCornerPosition(Level, CellIndex1 + 1);
            var position20 = ParentTree.GetOrAddCornerPosition(Level, CellIndex2);
            var position21 = ParentTree.GetOrAddCornerPosition(Level, CellIndex2 + 1);

            Corner00 = ParentTree.GetOrAddCorner(position10, position20);
            Corner01 = ParentTree.GetOrAddCorner(position10, position21);
            Corner10 = ParentTree.GetOrAddCorner(position11, position20);
            Corner11 = ParentTree.GetOrAddCorner(position11, position21);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(double parameterValue1, double parameterValue2)
        {
            return parameterValue1 >= MinParameterValue1 &&
                   parameterValue1 <= MaxParameterValue1 &&
                   parameterValue2 >= MinParameterValue2 &&
                   parameterValue2 <= MaxParameterValue2;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<Pair<GrParametricSurfaceLocalFrame3D>> GetEdgeFramePairs()
        {
            yield return new Pair<GrParametricSurfaceLocalFrame3D>(Frame00, Frame01);
            yield return new Pair<GrParametricSurfaceLocalFrame3D>(Frame00, Frame10);
            yield return new Pair<GrParametricSurfaceLocalFrame3D>(Frame01, Frame11);
            yield return new Pair<GrParametricSurfaceLocalFrame3D>(Frame10, Frame11);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MaxEdgeFramesDistance()
        {
            return GetEdgeFramePairs().Select(p => 
                p.Item1.Point.GetDistanceToPoint(p.Item2.Point)
            ).Max();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PlanarAngle MaxEdgeFramesAngle()
        {
            return GetEdgeFramePairs().Select(p => 
                p.Item1.Normal.GetVectorsAngle(p.Item2.Normal)
            ).Max().RadiansToAngle();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasNearEdgeFrames(GrParametricSurfaceTreeOptions3D options)
        {
            var maxAngleError = options.MaxEdgeFrameAngle.Radians;
            var maxDistanceError = options.MaxEdgeFrameDistance;

            return GetEdgeFramePairs().All(p =>
                p.Item1.Normal.GetUnitVectorsAngle(p.Item2.Normal) < maxAngleError ||
                p.Item1.Point.GetDistanceToPoint(p.Item2.Point) < maxDistanceError
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsChildOnSide(GrParametricSurfaceTreeNodeSide3D side)
        {
            return side switch
            {
                GrParametricSurfaceTreeNodeSide3D.SideX0 => IsChildX0,
                GrParametricSurfaceTreeNodeSide3D.SideX1 => IsChildX1,
                GrParametricSurfaceTreeNodeSide3D.Side0X => IsChild0X,
                _ => IsChild1X
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private GrParametricSurfaceTreeNode3D GetNeighborChildFromSide(GrParametricSurfaceTreeBranch3D parentNeighbor, GrParametricSurfaceTreeNodeSide3D side)
        {
            return side switch
            {
                GrParametricSurfaceTreeNodeSide3D.SideX0 =>
                    IsChild0X ? parentNeighbor.Child00 : parentNeighbor.Child10,

                GrParametricSurfaceTreeNodeSide3D.SideX1 =>
                    IsChild0X ? parentNeighbor.Child01 : parentNeighbor.Child11,

                GrParametricSurfaceTreeNodeSide3D.Side0X =>
                    IsChildX0 ? parentNeighbor.Child00 : parentNeighbor.Child01,

                _ =>
                    IsChildX0 ? parentNeighbor.Child10 : parentNeighbor.Child11
            };
        }

        /// <summary>
        /// Find the neighbor of this node at the same level from given side
        /// </summary>
        /// <returns></returns>
        public bool TryGetNeighbor(GrParametricSurfaceTreeNodeSide3D side, bool allowUpperLevelLeafNodes, out GrParametricSurfaceTreeNode3D neighborNode)
        {
            neighborNode = null;

            // The root node has no neighbors
            if (IsRoot)
                return false;

            var oppositeSide = side.GetOppositeSide();

            // The neighbor is a sibling with the same parent
            if (IsChildOnSide(oppositeSide))
            {
                neighborNode = side switch
                {
                    // Get left or right bottom sibling
                    GrParametricSurfaceTreeNodeSide3D.SideX0 =>
                        IsChild0X 
                            ? ParentBranch.Child00 
                            : ParentBranch.Child10,

                    // Get left or right top sibling
                    GrParametricSurfaceTreeNodeSide3D.SideX1 =>
                        IsChild0X 
                            ? ParentBranch.Child01 
                            : ParentBranch.Child11,

                    // Get bottom or top left sibling
                    GrParametricSurfaceTreeNodeSide3D.Side0X =>
                        IsChildX0 
                            ? ParentBranch.Child00 
                            : ParentBranch.Child01,

                    // Get bottom or top right sibling
                    _ =>
                        IsChildX0 
                            ? ParentBranch.Child10 
                            : ParentBranch.Child11
                };

                return true;
            }

            // Find neighbor node of parent branch
            if (!ParentBranch.TryGetNeighbor(side, allowUpperLevelLeafNodes, out var parentNodeNeighbor))
                return false;

            // Neighbor node of parent branch is a leaf
            if (parentNodeNeighbor.IsLeaf)
            {
                if (!allowUpperLevelLeafNodes)
                    return false;

                neighborNode = parentNodeNeighbor;
                return true;
            }
            
            // Neighbor node of parent branch is a branch
            // Find opposite-side child of neighbor branch of parent branch
            neighborNode = GetNeighborChildFromSide(
                (GrParametricSurfaceTreeBranch3D) parentNodeNeighbor, 
                oppositeSide
            );

            //// Neighbor node of parent branch is a branch
            //var parentNodeBranchNeighbor = 
            //    (GrParametricSurfaceTreeBranch3D) parentNodeNeighbor;

            //// Find opposite-side child of neighbor branch of parent branch
            //neighborNode = oppositeSide switch
            //{
            //    // Get left or right bottom sibling
            //    GrParametricSurfaceTreeNodeSide3D.SideX0 =>
            //        IsChild0X 
            //            ? parentNodeBranchNeighbor.Child00 
            //            : parentNodeBranchNeighbor.Child10,

            //    // Get left or right top sibling
            //    GrParametricSurfaceTreeNodeSide3D.SideX1 =>
            //        IsChild0X 
            //            ? parentNodeBranchNeighbor.Child01 
            //            : parentNodeBranchNeighbor.Child11,

            //    // Get bottom or top left sibling
            //    GrParametricSurfaceTreeNodeSide3D.Side0X =>
            //        IsChildX0 
            //            ? parentNodeBranchNeighbor.Child00 
            //            : parentNodeBranchNeighbor.Child01,

            //    // Get bottom or top right sibling
            //    _ =>
            //        IsChildX0 
            //            ? parentNodeBranchNeighbor.Child10 
            //            : parentNodeBranchNeighbor.Child11
            //};

            return true;
        }
        
        public abstract IEnumerator<GrParametricSurfaceTreeNode3D> GetEnumerator();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
                .AppendLine($"    Corner 00: {GridIndex00}")
                .AppendLine($"    Corner 01: {GridIndex01}")
                .AppendLine($"    Corner 10: {GridIndex10}")
                .AppendLine($"    Corner 11: {GridIndex11}")
                .ToString();
        }
    }
}