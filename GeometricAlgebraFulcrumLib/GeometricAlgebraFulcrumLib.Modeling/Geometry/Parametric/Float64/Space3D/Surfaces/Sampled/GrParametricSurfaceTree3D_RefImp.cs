//using System.Collections;
//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using System.Text;
//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
//using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
//using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;
//using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives;
//using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Triangles;
//using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Adaptive;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

//namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Surfaces.Sampled;

//public sealed class GrParametricSurfaceTree3D :
//    IGraphicsParametricSurface3D
//{
//    public enum TreeNodeSide
//    {
//        Side0X = 0,
//        Side1X = 1,
//        SideX0 = 2,
//        SideX1 = 3
//    }

//    public sealed class SamplingOptions
//    {
//        private double _maxEdgeFrameDistance = 1e-5d;
//        public double MaxEdgeFrameDistance
//        {
//            get => _maxEdgeFrameDistance;
//            set
//            {
//                if (value <= 0)
//                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be > 0");

//                _maxEdgeFrameDistance = value;
//            }
//        }

//        private LinFloat64DirectedAngle _maxEdgeFrameAngle = 45;
//        public LinFloat64DirectedAngle MaxEdgeFrameAngle
//        {
//            get => _maxEdgeFrameAngle;
//            set
//            {
//                if (value.DegreesValue is <= 0 or > 180)
//                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be > 0 and <= 180 degrees");

//                _maxEdgeFrameAngle = value;
//            }
//        }

//        private int _maxLevelCount = 10;
//        public int MaxLevelCount
//        {
//            get => _maxLevelCount;
//            set
//            {
//                if (value is < 2 or > 30)
//                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be >= 2 and <= 30");

//                _maxLevelCount = value;
//            }
//        }

//        private int _minLevelCount = 3;
//        public int MinLevelCount
//        {
//            get => _minLevelCount;
//            set
//            {
//                if (value < 0)
//                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be >= 0");

//                _minLevelCount = value;
//            }
//        }

//        public bool ForceBalancedTree { get; set; } = true;


//        public SamplingOptions(LinFloat64DirectedAngle maxAngleError, int minLevelCount, int maxLevelCount)
//        {
//            MaxEdgeFrameAngle = maxAngleError;
//            MinLevelCount = minLevelCount;
//            MaxLevelCount = maxLevelCount;
//        }
//    }

//    public abstract class Node :
//        IReadOnlyCollection<Node>
//    {
//        public GrParametricSurfaceTree3D ParentTree { get; }

//        public BranchNode ParentBranch { get; }

//        public IEnumerable<BranchNode> ParentBranches
//        {
//            get
//            {
//                for (var node = ParentBranch; node is not null; node = node.ParentBranch)
//                    yield return node;
//            }
//        }

//        public bool IsLeaf
//            => this is LeafNode;

//        public bool IsBranch
//            => this is BranchNode;

//        public abstract int Count { get; }

//        public bool IsRoot
//            => ParentBranch is null;

//        public bool IsChild
//            => ParentBranch is not null;

//        public int Level { get; }

//        //public ulong CellCode { get; }

//        public int CellIndex1 { get; }

//        public int CellIndex2 { get; }

//        public bool IsChild00
//            => IsChild && (CellIndex1 & 1) == 0 && (CellIndex2 & 1) == 0;

//        public bool IsChild01
//            => IsChild && (CellIndex1 & 1) == 0 && (CellIndex2 & 1) == 1;

//        public bool IsChild10
//            => IsChild && (CellIndex1 & 1) == 1 && (CellIndex2 & 1) == 0;

//        public bool IsChild11
//            => IsChild && (CellIndex1 & 1) == 1 && (CellIndex2 & 1) == 1;

//        public bool IsChild0X
//            => IsChild && (CellIndex1 & 1) == 0;

//        public bool IsChild1X
//            => IsChild && (CellIndex1 & 1) == 1;

//        public bool IsChildX0
//            => IsChild && (CellIndex2 & 1) == 0;

//        public bool IsChildX1
//            => IsChild && (CellIndex2 & 1) == 1;

//        public Corner Corner00 { get; }

//        public Corner Corner01 { get; }

//        public Corner Corner10 { get; }

//        public Corner Corner11 { get; }

//        public int FrameIndex00
//            => Corner00.Index;

//        public int FrameIndex01
//            => Corner01.Index;

//        public int FrameIndex10
//            => Corner10.Index;

//        public int FrameIndex11
//            => Corner11.Index;

//        public Pair<int> GridIndex00
//            => Corner00.GridIndex;

//        public Pair<int> GridIndex01
//            => Corner01.GridIndex;

//        public Pair<int> GridIndex10
//            => Corner10.GridIndex;

//        public Pair<int> GridIndex11
//            => Corner11.GridIndex;

//        public GrParametricSurfaceLocalFrame3D Frame00
//            => Corner00.Frame;

//        public GrParametricSurfaceLocalFrame3D Frame01
//            => Corner01.Frame;

//        public GrParametricSurfaceLocalFrame3D Frame10
//            => Corner10.Frame;

//        public GrParametricSurfaceLocalFrame3D Frame11
//            => Corner11.Frame;

//        public double MinParameterValue1
//            => Frame00.ParameterValue.Item1;

//        public double MinParameterValue2
//            => Frame00.ParameterValue.Item2;

//        public double MaxParameterValue1
//            => Frame11.ParameterValue.Item1;

//        public double MaxParameterValue2
//            => Frame11.ParameterValue.Item2;

//        public Float64BoundingBox2D ParameterValueRange
//            => Float64BoundingBox2D.Containing(
//                MinParameterValue1,
//                MinParameterValue2,
//                MaxParameterValue1,
//                MaxParameterValue2
//            );

//        public double ParameterValueRectArea
//            => (MaxParameterValue1 - MinParameterValue1) *
//               (MaxParameterValue2 - MinParameterValue2);

//        public IEnumerable<LeafNode> LeafNodes
//        {
//            get
//            {
//                var stack = new Stack<Node>();

//                stack.Push(this);

//                while (stack.Count > 0)
//                {
//                    var node = stack.Pop();

//                    if (node is LeafNode leafNode)
//                    {
//                        yield return leafNode;
//                        continue;
//                    }

//                    var branchNode = (BranchNode)node;

//                    stack.Push(branchNode.Child11);
//                    stack.Push(branchNode.Child10);
//                    stack.Push(branchNode.Child01);
//                    stack.Push(branchNode.Child00);
//                }
//            }
//        }

//        public int DeepestLeafNodeLevelX0
//            => LeafNodesX0.Select(n => n.Level).Max();

//        public int DeepestLeafNodeLevelX1
//            => LeafNodesX1.Select(n => n.Level).Max();

//        public int DeepestLeafNodeLevel0X
//            => LeafNodes0X.Select(n => n.Level).Max();

//        public int DeepestLeafNodeLevel1X
//            => LeafNodes1X.Select(n => n.Level).Max();

//        public IEnumerable<LeafNode> LeafNodesX0
//        {
//            get
//            {
//                var stack = new Stack<Node>();

//                stack.Push(this);

//                while (stack.Count > 0)
//                {
//                    var node = stack.Pop();

//                    if (node is LeafNode leafNode)
//                    {
//                        if (node.IsChildX0)
//                            yield return leafNode;

//                        continue;
//                    }

//                    var branchNode = (BranchNode)node;

//                    stack.Push(branchNode.Child10);
//                    stack.Push(branchNode.Child00);
//                }
//            }
//        }

//        public IEnumerable<LeafNode> LeafNodesX1
//        {
//            get
//            {
//                var stack = new Stack<Node>();

//                stack.Push(this);

//                while (stack.Count > 0)
//                {
//                    var node = stack.Pop();

//                    if (node is LeafNode leafNode)
//                    {
//                        if (node.IsChildX1)
//                            yield return leafNode;

//                        continue;
//                    }

//                    var branchNode = (BranchNode)node;

//                    stack.Push(branchNode.Child11);
//                    stack.Push(branchNode.Child01);
//                }
//            }
//        }

//        public IEnumerable<LeafNode> LeafNodes0X
//        {
//            get
//            {
//                var stack = new Stack<Node>();

//                stack.Push(this);

//                while (stack.Count > 0)
//                {
//                    var node = stack.Pop();

//                    if (node is LeafNode leafNode)
//                    {
//                        if (node.IsChild0X)
//                            yield return leafNode;

//                        continue;
//                    }

//                    var branchNode = (BranchNode)node;

//                    stack.Push(branchNode.Child01);
//                    stack.Push(branchNode.Child00);
//                }
//            }
//        }

//        public IEnumerable<LeafNode> LeafNodes1X
//        {
//            get
//            {
//                var stack = new Stack<Node>();

//                stack.Push(this);

//                while (stack.Count > 0)
//                {
//                    var node = stack.Pop();

//                    if (node is LeafNode leafNode)
//                    {
//                        if (node.IsChild1X)
//                            yield return leafNode;

//                        continue;
//                    }

//                    var branchNode = (BranchNode)node;

//                    stack.Push(branchNode.Child11);
//                    stack.Push(branchNode.Child10);
//                }
//            }
//        }


//        /// <summary>
//        /// Construct root node of tree
//        /// </summary>
//        /// <param name="parentTree"></param>
//        protected Node(GrParametricSurfaceTree3D parentTree)
//        {
//            ParentTree = parentTree;
//            ParentBranch = null;
//            Level = 0;
//            CellIndex1 = 0;
//            CellIndex2 = 0;

//            ParentTree.TreeLevelCount = 0;

//            var position0 = ParentTree.GetOrAddCornerPosition(0, 0);
//            var position1 = ParentTree.GetOrAddCornerPosition(0, 1);

//            Corner00 = ParentTree.GetOrAddCorner(position0, position0);
//            Corner01 = ParentTree.GetOrAddCorner(position0, position1);
//            Corner10 = ParentTree.GetOrAddCorner(position1, position0);
//            Corner11 = ParentTree.GetOrAddCorner(position1, position1);
//        }

//        /// <summary>
//        /// Construct sub-node of tree
//        /// </summary>
//        /// <param name="parentBranch"></param>
//        /// <param name="isChild1X"></param>
//        /// <param name="isChildX1"></param>
//        protected Node(BranchNode parentBranch, bool isChild1X, bool isChildX1)
//        {
//            Debug.Assert(parentBranch.Level < 30);

//            ParentTree = parentBranch.ParentTree;
//            ParentBranch = parentBranch;
//            Level = parentBranch.Level + 1;
//            CellIndex1 = parentBranch.CellIndex1 << 1 | (isChild1X ? 1 : 0);
//            CellIndex2 = parentBranch.CellIndex2 << 1 | (isChildX1 ? 1 : 0);

//            //CellCode = 0u;
//            //for (var i = 0; i < Level; i++)
//            //{
//            //    CellCode |= (ulong) ((CellIndex2 >> i) & 1) << (2 * i);
//            //    CellCode |= (ulong) ((CellIndex1 >> i) & 1) << (2 * i + 1);
//            //}

//            if (ParentTree.TreeLevelCount < Level)
//                ParentTree.TreeLevelCount = Level;

//            var position10 = ParentTree.GetOrAddCornerPosition(Level, CellIndex1);
//            var position11 = ParentTree.GetOrAddCornerPosition(Level, CellIndex1 + 1);
//            var position20 = ParentTree.GetOrAddCornerPosition(Level, CellIndex2);
//            var position21 = ParentTree.GetOrAddCornerPosition(Level, CellIndex2 + 1);

//            Corner00 = ParentTree.GetOrAddCorner(position10, position20);
//            Corner01 = ParentTree.GetOrAddCorner(position10, position21);
//            Corner10 = ParentTree.GetOrAddCorner(position11, position20);
//            Corner11 = ParentTree.GetOrAddCorner(position11, position21);
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public bool Contains(double parameterValue1, double parameterValue2)
//        {
//            return parameterValue1 >= MinParameterValue1 &&
//                   parameterValue1 <= MaxParameterValue1 &&
//                   parameterValue2 >= MinParameterValue2 &&
//                   parameterValue2 <= MaxParameterValue2;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public IEnumerable<Pair<GrParametricSurfaceLocalFrame3D>> GetEdgeFramePairs()
//        {
//            yield return new Pair<GrParametricSurfaceLocalFrame3D>(Frame00, Frame01);
//            yield return new Pair<GrParametricSurfaceLocalFrame3D>(Frame00, Frame10);
//            yield return new Pair<GrParametricSurfaceLocalFrame3D>(Frame01, Frame11);
//            yield return new Pair<GrParametricSurfaceLocalFrame3D>(Frame10, Frame11);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public double MaxEdgeFramesDistance()
//        {
//            return GetEdgeFramePairs().Select(p =>
//                p.Item1.Point.GetDistanceToPoint(p.Item2.Point)
//            ).Max();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public LinFloat64DirectedAngle MaxEdgeFramesAngle()
//        {
//            return GetEdgeFramePairs().Select(p =>
//                p.Item1.Normal.GetAngle(p.Item2.Normal).RadiansValue
//            ).Max().RadiansToDirectedAngle();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public bool HasNearEdgeFrames(SamplingOptions options)
//        {
//            var maxAngleError = options.MaxEdgeFrameAngle.RadiansValue;
//            var maxDistanceError = options.MaxEdgeFrameDistance;

//            return GetEdgeFramePairs().All(p =>
//                p.Item1.Normal.GetUnitVectorsAngle(p.Item2.Normal).RadiansValue < maxAngleError ||
//                p.Item1.Point.GetDistanceToPoint(p.Item2.Point) < maxDistanceError
//            );
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public bool IsChildOnSide(TreeNodeSide side)
//        {
//            return side switch
//            {
//                TreeNodeSide.SideX0 => IsChildX0,
//                TreeNodeSide.SideX1 => IsChildX1,
//                TreeNodeSide.Side0X => IsChild0X,
//                _ => IsChild1X
//            };
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private Node GetNeighborChildFromSide(BranchNode parentNeighbor, TreeNodeSide side)
//        {
//            return side switch
//            {
//                TreeNodeSide.SideX0 =>
//                    IsChild0X ? parentNeighbor.Child00 : parentNeighbor.Child10,

//                TreeNodeSide.SideX1 =>
//                    IsChild0X ? parentNeighbor.Child01 : parentNeighbor.Child11,

//                TreeNodeSide.Side0X =>
//                    IsChildX0 ? parentNeighbor.Child00 : parentNeighbor.Child01,

//                _ =>
//                    IsChildX0 ? parentNeighbor.Child10 : parentNeighbor.Child11
//            };
//        }

//        /// <summary>
//        /// Find the neighbor of this node at the same level from given side
//        /// </summary>
//        /// <returns></returns>
//        public bool TryGetNeighbor(TreeNodeSide side, bool allowUpperLevelLeafNodes, out Node neighborNode)
//        {
//            neighborNode = null;

//            // The root node has no neighbors
//            if (IsRoot)
//                return false;

//            var oppositeSide = side.GetOppositeSide();

//            // The neighbor is a sibling with the same parent
//            if (IsChildOnSide(oppositeSide))
//            {
//                neighborNode = side switch
//                {
//                    // Get left or right bottom sibling
//                    TreeNodeSide.SideX0 =>
//                        IsChild0X
//                            ? ParentBranch.Child00
//                            : ParentBranch.Child10,

//                    // Get left or right top sibling
//                    TreeNodeSide.SideX1 =>
//                        IsChild0X
//                            ? ParentBranch.Child01
//                            : ParentBranch.Child11,

//                    // Get bottom or top left sibling
//                    TreeNodeSide.Side0X =>
//                        IsChildX0
//                            ? ParentBranch.Child00
//                            : ParentBranch.Child01,

//                    // Get bottom or top right sibling
//                    _ =>
//                        IsChildX0
//                            ? ParentBranch.Child10
//                            : ParentBranch.Child11
//                };

//                return true;
//            }

//            // Find neighbor node of parent branch
//            if (!ParentBranch.TryGetNeighbor(side, allowUpperLevelLeafNodes, out var parentNodeNeighbor))
//                return false;

//            // Neighbor node of parent branch is a leaf
//            if (parentNodeNeighbor.IsLeaf)
//            {
//                if (!allowUpperLevelLeafNodes)
//                    return false;

//                neighborNode = parentNodeNeighbor;
//                return true;
//            }

//            // Neighbor node of parent branch is a branch
//            // Find opposite-side child of neighbor branch of parent branch
//            neighborNode = GetNeighborChildFromSide(
//                (BranchNode)parentNodeNeighbor,
//                oppositeSide
//            );

//            //// Neighbor node of parent branch is a branch
//            //var parentNodeBranchNeighbor = 
//            //    (GrParametricSurfaceTreeBranch3D) parentNodeNeighbor;

//            //// Find opposite-side child of neighbor branch of parent branch
//            //neighborNode = oppositeSide switch
//            //{
//            //    // Get left or right bottom sibling
//            //    GrParametricSurfaceTreeNodeSide3D.SideX0 =>
//            //        IsChild0X 
//            //            ? parentNodeBranchNeighbor.Child00 
//            //            : parentNodeBranchNeighbor.Child10,

//            //    // Get left or right top sibling
//            //    GrParametricSurfaceTreeNodeSide3D.SideX1 =>
//            //        IsChild0X 
//            //            ? parentNodeBranchNeighbor.Child01 
//            //            : parentNodeBranchNeighbor.Child11,

//            //    // Get bottom or top left sibling
//            //    GrParametricSurfaceTreeNodeSide3D.Side0X =>
//            //        IsChildX0 
//            //            ? parentNodeBranchNeighbor.Child00 
//            //            : parentNodeBranchNeighbor.Child01,

//            //    // Get bottom or top right sibling
//            //    _ =>
//            //        IsChildX0 
//            //            ? parentNodeBranchNeighbor.Child10 
//            //            : parentNodeBranchNeighbor.Child11
//            //};

//            return true;
//        }

//        public abstract IEnumerator<Node> GetEnumerator();

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override string ToString()
//        {
//            return new StringBuilder()
//                .AppendLine($"Cell <{CellIndex1}, {CellIndex2}>:")
//                .AppendLine($"        Level: {Level}")
//                //.AppendLine($"         Code: {CellCode}")
//                .AppendLine($"    Corner 00: {GridIndex00}")
//                .AppendLine($"    Corner 01: {GridIndex01}")
//                .AppendLine($"    Corner 10: {GridIndex10}")
//                .AppendLine($"    Corner 11: {GridIndex11}")
//                .ToString();
//        }
//    }

//    public sealed class BranchNode : Node
//    {
//        public override int Count
//            => 4;

//        public Node Child00 { get; private set; }

//        public Node Child01 { get; private set; }

//        public Node Child10 { get; private set; }

//        public Node Child11 { get; private set; }


//        public int MidFrameIndexX0
//            => Child00.FrameIndex10;

//        public int MidFrameIndexX1
//            => Child01.FrameIndex11;

//        public int MidFrameIndex0X
//            => Child00.FrameIndex01;

//        public int MidFrameIndex1X
//            => Child10.FrameIndex11;

//        public int MidFrameIndex
//            => Child00.FrameIndex11;


//        public Corner MidCornerIndexX0
//            => Child00.Corner10;

//        public Corner MidCornerIndexX1
//            => Child01.Corner11;

//        public Corner MidCornerIndex0X
//            => Child00.Corner01;

//        public Corner MidCornerIndex1X
//            => Child10.Corner11;

//        public Corner MidCornerIndex
//            => Child00.Corner11;


//        public Pair<int> MidGridIndexX0
//            => Child00.GridIndex10;

//        public Pair<int> MidGridIndexX1
//            => Child01.GridIndex11;

//        public Pair<int> MidGridIndex0X
//            => Child00.GridIndex01;

//        public Pair<int> MidGridIndex1X
//            => Child10.GridIndex11;

//        public Pair<int> MidGridIndex
//            => Child00.GridIndex11;


//        public GrParametricSurfaceLocalFrame3D MidFrameX0
//            => Child00.Frame10;

//        public GrParametricSurfaceLocalFrame3D MidFrameX1
//            => Child01.Frame11;

//        public GrParametricSurfaceLocalFrame3D MidFrame0X
//            => Child00.Frame01;

//        public GrParametricSurfaceLocalFrame3D MidFrame1X
//            => Child10.Frame11;

//        public GrParametricSurfaceLocalFrame3D MidFrame
//            => Child00.Frame11;


//        /// <summary>
//        /// Constructor of the root node of the tree
//        /// </summary>
//        /// <param name="parentTree"></param>
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        internal BranchNode(GrParametricSurfaceTree3D parentTree)
//            : base(parentTree)
//        {
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        internal BranchNode(BranchNode parentBranch, bool isChild1X, bool isChildX1, bool createLeafChildren = false)
//            : base(parentBranch, isChild1X, isChildX1)
//        {
//            if (createLeafChildren)
//            {
//                Child00 = ParentTree.AddLeafNode(
//                    new LeafNode(this, false, false)
//                );

//                Child01 = ParentTree.AddLeafNode(
//                    new LeafNode(this, false, true)
//                );

//                Child10 = ParentTree.AddLeafNode(
//                    new LeafNode(this, true, false)
//                );

//                Child11 = ParentTree.AddLeafNode(
//                    new LeafNode(this, true, true)
//                );
//            }
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        internal void SetChild(Node node)
//        {
//            Debug.Assert(ReferenceEquals(node.ParentBranch, this) && node.Level == Level + 1);

//            if (node.IsChild00)
//                Child00 = node;

//            else if (node.IsChild01)
//                Child01 = node;

//            else if (node.IsChild10)
//                Child10 = node;

//            else
//                Child11 = node;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private BranchNode CreateBranchChildren(SamplingOptions options)
//        {
//            Child00 = new BranchNode(
//                this,
//                false,
//                false
//            ).GenerateTree(options);

//            Child01 = new BranchNode(
//                this,
//                false,
//                true
//            ).GenerateTree(options);

//            Child10 = new BranchNode(
//                this,
//                true,
//                false
//            ).GenerateTree(options);

//            Child11 = new BranchNode(
//                this,
//                true,
//                true
//            ).GenerateTree(options);

//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        internal Node GenerateTree(SamplingOptions options)
//        {
//            var continueSubdivision =
//                // Always subdivide the root node
//                IsRoot ||

//                // Continue subdivision for the required initial number of levels
//                Level < options.MinLevelCount ||

//                // Continue subdivision if not at at max level and frame normals are far from parallel
//                Level < options.MaxLevelCount && !HasNearEdgeFrames(options);

//            if (continueSubdivision)
//                return CreateBranchChildren(options);

//            // Stop subdivision and replace this branch with a leaf node
//            return ParentTree.AddLeafNode(
//                new LeafNode(ParentBranch, IsChild1X, IsChildX1)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        internal Node GetChildContaining(double parameterValue1, double parameterValue2)
//        {
//            if (Child00.Contains(parameterValue1, parameterValue2))
//                return Child00;

//            if (Child01.Contains(parameterValue1, parameterValue2))
//                return Child01;

//            if (Child10.Contains(parameterValue1, parameterValue2))
//                return Child10;

//            if (Child11.Contains(parameterValue1, parameterValue2))
//                return Child11;

//            throw new InvalidOperationException();
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override IEnumerator<Node> GetEnumerator()
//        {
//            yield return Child00;
//            yield return Child01;
//            yield return Child10;
//            yield return Child11;
//        }
//    }

//    public sealed class LeafNode : Node
//    {
//        public override int Count
//            => 0;

//        public LeafEdge EdgeX0 { get; }

//        public LeafEdge EdgeX1 { get; }

//        public LeafEdge Edge0X { get; }

//        public LeafEdge Edge1X { get; }

//        public bool EdgeDataReady
//            => EdgeX0.Count >= 2 &&
//               EdgeX1.Count >= 2 &&
//               Edge0X.Count >= 2 &&
//               Edge1X.Count >= 2;


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        internal LeafNode(BranchNode parentBranch, bool isChild1X, bool isChildX1)
//            : base(parentBranch, isChild1X, isChildX1)
//        {
//            EdgeX0 = new LeafEdge(
//                this,
//                TreeNodeSide.SideX0
//            );

//            EdgeX1 = new LeafEdge(
//                this,
//                TreeNodeSide.SideX1
//            );

//            Edge0X = new LeafEdge(
//                this,
//                TreeNodeSide.Side0X
//            );

//            Edge1X = new LeafEdge(
//                this,
//                TreeNodeSide.Side1X
//            );
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        internal BranchNode Split()
//        {
//            var branchNode = new BranchNode(
//                ParentBranch,
//                IsChild1X,
//                IsChildX1,
//                true
//            );

//            ParentBranch.SetChild(this);

//            return branchNode;
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public IEnumerable<Corner> GetTreeCornersOnSide(TreeNodeSide side)
//        {
//            switch (side)
//            {
//                case TreeNodeSide.Side0X:
//                    yield return Corner00;
//                    yield return Corner01;
//                    break;

//                case TreeNodeSide.Side1X:
//                    yield return Corner10;
//                    yield return Corner11;
//                    break;

//                case TreeNodeSide.SideX0:
//                    yield return Corner00;
//                    yield return Corner10;
//                    break;

//                default:
//                    yield return Corner01;
//                    yield return Corner11;
//                    break;
//            }
//        }

//        /// <summary>
//        /// Find the lower-level leaf node neighbors of this node from the given side
//        /// </summary>
//        /// <returns></returns>
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public IEnumerable<LeafNode> GetDeeperLeafNeighbors(TreeNodeSide side)
//        {
//            if (!TryGetNeighbor(side, false, out var neighborNode) || neighborNode.IsLeaf)
//                return Enumerable.Empty<LeafNode>();

//            var neighborBranchNode =
//                (BranchNode)neighborNode;

//            return side switch
//            {
//                TreeNodeSide.SideX0 =>
//                    neighborBranchNode.LeafNodesX1,

//                TreeNodeSide.SideX1 =>
//                    neighborBranchNode.LeafNodesX0,

//                TreeNodeSide.Side0X =>
//                    neighborBranchNode.LeafNodes1X,

//                _ =>
//                    neighborBranchNode.LeafNodes0X
//            };
//        }

//        /// <summary>
//        /// Find all the leaf node neighbors of this node from the given side
//        /// </summary>
//        /// <returns></returns>
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public IEnumerable<LeafNode> GetLeafNeighbors(TreeNodeSide side)
//        {
//            if (!TryGetNeighbor(side, true, out var neighborNode))
//                return Enumerable.Empty<LeafNode>();

//            if (neighborNode is LeafNode neighborLeafNode)
//                return new[] { neighborLeafNode };

//            var neighborBranchNode =
//                (BranchNode)neighborNode;

//            return side switch
//            {
//                TreeNodeSide.SideX0 =>
//                    neighborBranchNode.LeafNodesX1,

//                TreeNodeSide.SideX1 =>
//                    neighborBranchNode.LeafNodesX0,

//                TreeNodeSide.Side0X =>
//                    neighborBranchNode.LeafNodes1X,

//                _ =>
//                    neighborBranchNode.LeafNodes0X
//            };
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public IReadOnlyList<LeafNode> GetLeafNeighbors()
//        {
//            var leafNeighborsList = new List<LeafNode>();

//            leafNeighborsList.AddRange(
//                GetLeafNeighbors(TreeNodeSide.SideX0)
//            );

//            leafNeighborsList.AddRange(
//                GetLeafNeighbors(TreeNodeSide.SideX1)
//            );

//            leafNeighborsList.AddRange(
//                GetLeafNeighbors(TreeNodeSide.Side0X)
//            );

//            leafNeighborsList.AddRange(
//                GetLeafNeighbors(TreeNodeSide.Side1X)
//            );

//            return leafNeighborsList;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public LeafNode ClearEdgeData()
//        {
//            EdgeX0.Clear();
//            EdgeX1.Clear();
//            Edge0X.Clear();
//            Edge1X.Clear();

//            return this;
//        }

//        public LeafNode GenerateEdgeData()
//        {
//            EdgeX0.AddCornersFromSide(TreeNodeSide.SideX0, this);
//            EdgeX1.AddCornersFromSide(TreeNodeSide.SideX1, this);
//            Edge0X.AddCornersFromSide(TreeNodeSide.Side0X, this);
//            Edge1X.AddCornersFromSide(TreeNodeSide.Side1X, this);

//            if (Level == ParentTree.TreeLevelCount)
//                return this;

//            var leafNeighbors =
//                GetDeeperLeafNeighbors(TreeNodeSide.SideX0);

//            foreach (var neighborsLeafNode in leafNeighbors)
//                EdgeX0.AddCornersFromSide(
//                    TreeNodeSide.SideX1,
//                    neighborsLeafNode
//                );

//            leafNeighbors =
//                GetDeeperLeafNeighbors(TreeNodeSide.SideX1);

//            foreach (var neighborsLeafNode in leafNeighbors)
//                EdgeX1.AddCornersFromSide(
//                    TreeNodeSide.SideX0,
//                    neighborsLeafNode
//                );

//            leafNeighbors =
//                GetDeeperLeafNeighbors(TreeNodeSide.Side0X);

//            foreach (var neighborsLeafNode in leafNeighbors)
//                Edge0X.AddCornersFromSide(
//                    TreeNodeSide.Side1X,
//                    neighborsLeafNode
//                );

//            leafNeighbors =
//                GetDeeperLeafNeighbors(TreeNodeSide.Side1X);

//            foreach (var neighborsLeafNode in leafNeighbors)
//                Edge1X.AddCornersFromSide(
//                    TreeNodeSide.Side0X,
//                    neighborsLeafNode
//                );

//            return this;
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public IEnumerable<Triplet<Pair<int>>> GetTriangleGridIndexTriplets(bool clearEdgeDataWhenFinished = false)
//        {
//            return GetTriangleIndexTriplets(clearEdgeDataWhenFinished)
//                .Select(t =>
//                    {
//                        var (index1, index2, index3) = t;

//                        return new Triplet<Pair<int>>(
//                            ParentTree.CornerList[index1].GridIndex,
//                            ParentTree.CornerList[index2].GridIndex,
//                            ParentTree.CornerList[index3].GridIndex
//                        );
//                    }
//                );
//        }

//        public IEnumerable<Triplet<int>> GetTriangleIndexTriplets(bool clearEdgeDataWhenFinished = false)
//        {
//            if (!EdgeDataReady)
//                GenerateEdgeData();

//            Triplet<int> indexTriplet;

//            // Generate Lower-right Triangles of leaf cell
//            if (EdgeX0.Count == 2 && Edge1X.Count == 2)
//            {
//                indexTriplet = new Triplet<int>(
//                    FrameIndex00,
//                    FrameIndex10,
//                    FrameIndex11
//                );

//                Debug.Assert(
//                    indexTriplet.IsValidTriangleIndexTriplet(false)
//                );

//                yield return indexTriplet;
//            }
//            else if (EdgeX0.Count == 2 && Edge1X.Count > 2)
//            {
//                var frameIndexList =
//                    Edge1X.Skip(1).Select(f => f.Index);

//                var index1 = FrameIndex00;
//                var index2 = FrameIndex10;

//                foreach (var index3 in frameIndexList)
//                {
//                    indexTriplet = new Triplet<int>(index1, index2, index3);

//                    Debug.Assert(
//                        indexTriplet.IsValidTriangleIndexTriplet(false)
//                    );

//                    yield return indexTriplet;

//                    index2 = index3;
//                }
//            }
//            else if (EdgeX0.Count > 2 && Edge1X.Count == 2)
//            {
//                var frameIndexList =
//                    EdgeX0.Skip(1).Select(f => f.Index);

//                var index1 = FrameIndex00;
//                var index3 = FrameIndex11;

//                foreach (var index2 in frameIndexList)
//                {
//                    indexTriplet = new Triplet<int>(index1, index2, index3);

//                    Debug.Assert(
//                        indexTriplet.IsValidTriangleIndexTriplet(false)
//                    );

//                    yield return indexTriplet;

//                    index1 = index2;
//                }
//            }
//            else
//            {
//                var frameIndexList =
//                    Edge1X.Skip(1).Select(f => f.Index);

//                var index1 = FrameIndex00;
//                var index2 = FrameIndex10;

//                foreach (var index3 in frameIndexList)
//                {
//                    indexTriplet = new Triplet<int>(index1, index2, index3);

//                    Debug.Assert(
//                        indexTriplet.IsValidTriangleIndexTriplet(false)
//                    );

//                    yield return indexTriplet;

//                    index2 = index3;
//                }

//                frameIndexList =
//                    EdgeX0.Skip(1).Select(f => f.Index);

//                var i1 = FrameIndex00;
//                var i3 = Edge1X.Skip(1).First().Index;

//                foreach (var i2 in frameIndexList)
//                {
//                    indexTriplet = new Triplet<int>(i1, i2, i3);

//                    Debug.Assert(
//                        indexTriplet.IsValidTriangleIndexTriplet(false)
//                    );

//                    yield return indexTriplet;

//                    i1 = i2;
//                }
//            }

//            // Generate Upper-left Triangles of leaf cell
//            if (EdgeX1.Count == 2 && Edge0X.Count == 2)
//            {
//                indexTriplet = new Triplet<int>(
//                    FrameIndex00,
//                    FrameIndex11,
//                    FrameIndex01
//                );

//                Debug.Assert(
//                    indexTriplet.IsValidTriangleIndexTriplet(false)
//                );

//                yield return indexTriplet;
//            }
//            else if (EdgeX1.Count == 2 && Edge0X.Count > 2)
//            {
//                var frameIndexList =
//                    Edge0X.Skip(1).Select(f => f.Index);

//                var index1 = FrameIndex00;
//                var index2 = FrameIndex11;

//                foreach (var index3 in frameIndexList)
//                {
//                    indexTriplet = new Triplet<int>(index1, index2, index3);

//                    Debug.Assert(
//                        indexTriplet.IsValidTriangleIndexTriplet(false)
//                    );

//                    yield return indexTriplet;

//                    index1 = index3;
//                }
//            }
//            else if (EdgeX1.Count > 2 && Edge0X.Count == 2)
//            {
//                var frameIndexList =
//                    EdgeX1.Skip(1).Select(f => f.Index);

//                var index1 = FrameIndex00;
//                var index3 = FrameIndex01;

//                foreach (var index2 in frameIndexList)
//                {
//                    indexTriplet = new Triplet<int>(index1, index2, index3);

//                    Debug.Assert(
//                        indexTriplet.IsValidTriangleIndexTriplet(false)
//                    );

//                    yield return indexTriplet;

//                    index3 = index2;
//                }
//            }
//            else
//            {
//                var frameIndexList =
//                    Edge0X.Skip(1).Select(f => f.Index);

//                var index1 = FrameIndex00;
//                var index2 = FrameIndex11;

//                foreach (var index3 in frameIndexList)
//                {
//                    indexTriplet = new Triplet<int>(index1, index2, index3);

//                    Debug.Assert(
//                        indexTriplet.IsValidTriangleIndexTriplet(false)
//                    );

//                    yield return indexTriplet;

//                    index1 = index3;
//                }

//                frameIndexList =
//                    EdgeX1.Skip(1).Select(f => f.Index);

//                var i1 = Edge0X.Skip(Edge0X.Count - 2).First().Index;
//                var i3 = FrameIndex01;

//                foreach (var i2 in frameIndexList)
//                {
//                    indexTriplet = new Triplet<int>(i1, i2, i3);

//                    Debug.Assert(
//                        indexTriplet.IsValidTriangleIndexTriplet(false)
//                    );

//                    yield return indexTriplet;

//                    i3 = i2;
//                }
//            }

//            if (clearEdgeDataWhenFinished)
//                ClearEdgeData();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override IEnumerator<Node> GetEnumerator()
//        {
//            return Enumerable.Empty<Node>().GetEnumerator();
//        }
//    }

//    public sealed class LeafEdge :
//        IReadOnlyCollection<Corner>
//    {
//        private readonly Dictionary<Pair<Float64SampledPath3D.CornerPosition>, Corner> _cornerDictionary
//            = new Dictionary<Pair<Float64SampledPath3D.CornerPosition>, Corner>();


//        public int Count
//            => _cornerDictionary.Count;

//        public LeafNode LeafNode { get; }

//        public TreeNodeSide Side { get; }

//        public bool IsSideX0OrX1
//            => Side is
//                TreeNodeSide.SideX0 or
//                TreeNodeSide.SideX1;

//        public bool IsSide0XOr1X
//            => Side is
//                TreeNodeSide.Side0X or
//                TreeNodeSide.Side1X;


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        internal LeafEdge(LeafNode leafNode, TreeNodeSide edgePosition)
//        {
//            LeafNode = leafNode;
//            Side = edgePosition;
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public LeafEdge Clear()
//        {
//            _cornerDictionary.Clear();
//            return this;
//        }

//        internal LeafEdge AddCornersFromSide(TreeNodeSide side, LeafNode node)
//        {
//            Debug.Assert(
//                side switch
//                {
//                    TreeNodeSide.Side0X => IsSide0XOr1X,
//                    TreeNodeSide.Side1X => IsSide0XOr1X,
//                    TreeNodeSide.SideX0 => IsSideX0OrX1,
//                    _ => IsSideX0OrX1,
//                }
//            );

//            var cornerList =
//                node.GetTreeCornersOnSide(side);

//            foreach (var corner in cornerList)
//            {
//                var position =
//                    corner.Position;

//                _cornerDictionary.TryAdd(position, corner);
//            }

//            return this;
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public IEnumerator<Corner> GetEnumerator()
//        {
//            var corners =
//                IsSideX0OrX1
//                    ? _cornerDictionary.Values.OrderBy(c => c.GridIndex.Item1)
//                    : _cornerDictionary.Values.OrderBy(c => c.GridIndex.Item2);

//            return corners.GetEnumerator();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }
//    }

//    public sealed record Corner
//    {
//        public GrParametricSurfaceTree3D ParentTree { get; }

//        public Pair<Float64SampledPath3D.CornerPosition> Position { get; }

//        public int Index { get; }

//        public GrParametricSurfaceLocalFrame3D Frame { get; }

//        public Pair<int> GridIndex
//        {
//            get
//            {
//                var maxTreeLevel = ParentTree.TreeLevelCount;

//                return new Pair<int>(
//                    Position.Item1.GetGridIndex(maxTreeLevel),
//                    Position.Item2.GetGridIndex(maxTreeLevel)
//                );
//            }
//        }

//        public Pair<double> InterpolationValue
//            => new Pair<double>(
//                Position.Item1.GetInterpolationValue(),
//                Position.Item2.GetInterpolationValue()
//            );


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        internal Corner(GrParametricSurfaceTree3D parentTree, int index, GrParametricSurfaceLocalFrame3D frame, Pair<Float64SampledPath3D.CornerPosition> position)
//        {
//            ParentTree = parentTree;
//            Index = index;
//            Frame = frame;
//            Position = position;
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override string ToString()
//        {
//            var (i1, i2) = GridIndex;

//            return $"({i1}, {i2})";
//        }
//    }
    
//    public sealed record Sample
//    {
//        public LeafNode LeafNode { get; }

//        public double ParameterValue1 { get; }

//        public double ParameterValue2 { get; }

//        public double InterpolationValue1 { get; }

//        public double InterpolationValue2 { get; }


//        internal Sample(LeafNode leafNode, double parameterValue1, double parameterValue2)
//        {
//            LeafNode = leafNode;

//            ParameterValue1 = parameterValue1;
//            ParameterValue2 = parameterValue2;

//            InterpolationValue1 =
//                (ParameterValue1 - leafNode.MinParameterValue1) /
//                (leafNode.MaxParameterValue1 - leafNode.MinParameterValue1);

//            InterpolationValue2 =
//                (ParameterValue2 - leafNode.MinParameterValue2) /
//                (leafNode.MaxParameterValue2 - leafNode.MinParameterValue2);

//            Debug.Assert(InterpolationValue1 is >= 0d and <= 1d);
//            Debug.Assert(InterpolationValue2 is >= 0d and <= 1d);
//        }


//        public LinFloat64Vector3D GetPoint()
//        {
//            var t10 = 1d - InterpolationValue1;
//            var t11 = InterpolationValue1;

//            var t20 = 1 - InterpolationValue2;
//            var t21 = InterpolationValue2;

//            var s00 = t10 * t20;
//            var s01 = t10 * t21;
//            var s10 = t11 * t20;
//            var s11 = t11 * t21;

//            return s00 * LeafNode.Frame00.Point +
//                   s01 * LeafNode.Frame01.Point +
//                   s10 * LeafNode.Frame10.Point +
//                   s11 * LeafNode.Frame11.Point;
//        }

//        public LinFloat64Vector3D GetNormal()
//        {
//            var t10 = 1d - InterpolationValue1;
//            var t11 = InterpolationValue1;

//            var t20 = 1 - InterpolationValue2;
//            var t21 = InterpolationValue2;

//            var s00 = t10 * t20;
//            var s01 = t10 * t21;
//            var s10 = t11 * t20;
//            var s11 = t11 * t21;

//            return s00 * LeafNode.Frame00.Normal.ToLinVector3D() +
//                   s01 * LeafNode.Frame01.Normal.ToLinVector3D() +
//                   s10 * LeafNode.Frame10.Normal.ToLinVector3D() +
//                   s11 * LeafNode.Frame11.Normal.ToLinVector3D();
//        }

//        public GrParametricSurfaceLocalFrame3D GetFrame()
//        {
//            var t10 = 1d - InterpolationValue1;
//            var t11 = InterpolationValue1;

//            var t20 = 1 - InterpolationValue2;
//            var t21 = InterpolationValue2;

//            var s00 = t10 * t20;
//            var s01 = t10 * t21;
//            var s10 = t11 * t20;
//            var s11 = t11 * t21;

//            var point =
//                s00 * LeafNode.Frame00.Point +
//                s01 * LeafNode.Frame01.Point +
//                s10 * LeafNode.Frame10.Point +
//                s11 * LeafNode.Frame11.Point;

//            var normal =
//                s00 * LeafNode.Frame00.Normal.ToLinVector3D() +
//                s01 * LeafNode.Frame01.Normal.ToLinVector3D() +
//                s10 * LeafNode.Frame10.Normal.ToLinVector3D() +
//                s11 * LeafNode.Frame11.Normal.ToLinVector3D();

//            return new GrParametricSurfaceLocalFrame3D(
//                ParameterValue1,
//                ParameterValue2,
//                point,
//                normal
//            );
//        }
//    }


//    private readonly HashSet<Float64SampledPath3D.CornerPosition> _cornerPositionSet
//        = new HashSet<Float64SampledPath3D.CornerPosition>();

//    private readonly Dictionary<Pair<Float64SampledPath3D.CornerPosition>, int> _cornerDictionary
//        = new Dictionary<Pair<Float64SampledPath3D.CornerPosition>, int>();

//    private readonly List<Corner> _cornerList
//        = new List<Corner>();

//    private readonly List<LeafNode> _leafNodeList
//        = new List<LeafNode>();


//    public IGraphicsParametricSurface3D Surface { get; }

//    public BranchNode? RootNode { get; private set; }

//    public int TreeLevelCount { get; internal set; }

//    /// <summary>
//    /// The number of segments per grid side for this tree
//    /// </summary>
//    public int GridSegmentCount
//        => 1 << TreeLevelCount;

//    public int LeafNodeCount
//        => _leafNodeList.Count;

//    public IEnumerable<Node> Nodes
//    {
//        get
//        {
//            var stack = new Stack<Node>();

//            stack.Push(RootNode);

//            while (stack.Count > 0)
//            {
//                var node = stack.Pop();

//                yield return node;

//                if (node is not BranchNode branchNode)
//                    continue;

//                stack.Push(branchNode.Child11);
//                stack.Push(branchNode.Child10);
//                stack.Push(branchNode.Child01);
//                stack.Push(branchNode.Child00);
//            }
//        }
//    }

//    public IEnumerable<BranchNode> BranchNodes
//    {
//        get
//        {
//            var stack = new Stack<BranchNode>();

//            stack.Push(RootNode);

//            while (stack.Count > 0)
//            {
//                var branchNode = stack.Pop();

//                yield return branchNode;

//                if (branchNode.Child11 is BranchNode childBranchNode11)
//                    stack.Push(childBranchNode11);

//                if (branchNode.Child10 is BranchNode childBranchNode10)
//                    stack.Push(childBranchNode10);

//                if (branchNode.Child01 is BranchNode childBranchNode01)
//                    stack.Push(childBranchNode01);

//                if (branchNode.Child00 is BranchNode childBranchNode00)
//                    stack.Push(childBranchNode00);
//            }
//        }
//    }

//    public IEnumerable<LeafNode> LeafNodes
//    {
//        get
//        {
//            var stack = new Stack<Node>();

//            stack.Push(RootNode);

//            while (stack.Count > 0)
//            {
//                var node = stack.Pop();

//                if (node is LeafNode leafNode)
//                {
//                    yield return leafNode;
//                    continue;
//                }

//                var branchNode = (BranchNode)node;

//                stack.Push(branchNode.Child11);
//                stack.Push(branchNode.Child10);
//                stack.Push(branchNode.Child01);
//                stack.Push(branchNode.Child00);
//            }
//        }
//    }

//    public IReadOnlyList<LeafNode> LeafNodesList
//        => _leafNodeList;

//    public Float64BoundingBox2D ParameterValueRange { get; }

//    public double MinParameterValue1
//        => ParameterValueRange.MinX;

//    public double MinParameterValue2
//        => ParameterValueRange.MinY;

//    public double MaxParameterValue1
//        => ParameterValueRange.MaxX;

//    public double MaxParameterValue2
//        => ParameterValueRange.MaxY;


//    public int CornerCount
//        => _cornerList.Count;

//    public IReadOnlyList<Corner> CornerList
//        => _cornerList;


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public GrParametricSurfaceTree3D(IGraphicsParametricSurface3D surface, Float64BoundingBox2D parameterValueRange)
//    {
//        Surface = surface;
//        ParameterValueRange = parameterValueRange;
//        RootNode = null;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public GrParametricSurfaceTree3D(IGraphicsParametricSurface3D surface)
//    {
//        Surface = surface;
//        ParameterValueRange = Float64BoundingBox2D.Containing(0, 0, 1, 1);
//        RootNode = null;
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public GrParametricSurfaceTree3D Clear()
//    {
//        RootNode = null;
//        _cornerList.Clear();
//        _leafNodeList.Clear();
//        _cornerPositionSet.Clear();
//        _cornerDictionary.Clear();

//        return this;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Pair<double> GetParameterValue(Pair<Float64SampledPath3D.CornerPosition> cornerPosition)
//    {
//        var t1 = cornerPosition.Item1.GetInterpolationValue();
//        var t2 = cornerPosition.Item2.GetInterpolationValue();

//        var parameterValue1 = t1.Lerp(MinParameterValue1, MaxParameterValue1);
//        var parameterValue2 = t2.Lerp(MinParameterValue2, MaxParameterValue2);

//        return new Pair<double>(parameterValue1, parameterValue2);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public GrParametricSurfaceTree3D ClearLeafEdgeData()
//    {
//        foreach (var leafNode in LeafNodesList)
//            leafNode.ClearEdgeData();

//        return this;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public GrParametricSurfaceTree3D GenerateLeafEdgeData()
//    {
//        foreach (var leafNode in LeafNodesList)
//            leafNode.GenerateEdgeData();

//        return this;
//    }

//    public GrParametricSurfaceTree3D GenerateTree(SamplingOptions options)
//    {
//        Clear();

//        RootNode = new BranchNode(this);

//        RootNode.GenerateTree(options);

//        if (options.ForceBalancedTree)
//            BalanceTree();

//        foreach (var corner in _cornerList)
//            corner.Frame.Index = corner.Index;

//        _cornerPositionSet.Clear();
//        _cornerDictionary.Clear();

//        return this;
//    }

//    private void BalanceTree()
//    {
//        var leafNodeSet = new HashSet<LeafNode>();

//        foreach (var leafNode in _leafNodeList.Where(n => n.Level < TreeLevelCount - 1))
//            leafNodeSet.Add(leafNode);

//        while (leafNodeSet.Count > 0)
//        {
//            var leafNode = leafNodeSet.First();
//            leafNodeSet.Remove(leafNode);

//            var neighborLeafNodesList = leafNode.GetLeafNeighbors();
//            var deepestLevel = neighborLeafNodesList.Max(n => n.Level);

//            // The leaf node needs to be split
//            if (deepestLevel > leafNode.Level + 1)
//            {
//                // Replace the leaf node by a new branch
//                var branchNode = leafNode.Split();

//                // Add neighbor leaf nodes that require splitting due to new level difference
//                foreach (var neighborLeafNode in neighborLeafNodesList.Where(n => n.Level < leafNode.Level))
//                    leafNodeSet.Add(neighborLeafNode);
//            }
//        }
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public bool ContainsCorner(Pair<Float64SampledPath3D.CornerPosition> cornerPosition)
//    {
//        return _cornerDictionary.ContainsKey(cornerPosition);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Corner GetCorner(int cornerIndex)
//    {
//        return _cornerList[cornerIndex];
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public GrParametricSurfaceLocalFrame3D GetFrame(int cornerIndex)
//    {
//        return _cornerList[cornerIndex].Frame;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Corner GetCorner(Pair<Float64SampledPath3D.CornerPosition> cornerPosition)
//    {
//        var cornerIndex = _cornerDictionary[cornerPosition];

//        return _cornerList[cornerIndex];
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public int GetCornerIndex(Pair<Float64SampledPath3D.CornerPosition> cornerPosition)
//    {
//        return _cornerDictionary[cornerPosition];
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public bool TryGetCorner(Pair<Float64SampledPath3D.CornerPosition> cornerPosition, out Corner corner)
//    {
//        if (_cornerDictionary.TryGetValue(cornerPosition, out var cornerIndex))
//        {
//            corner = _cornerList[cornerIndex];
//            return true;
//        }

//        corner = default;
//        return false;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public bool TryGetCornerIndex(Pair<Float64SampledPath3D.CornerPosition> cornerPosition, out int cornerIndex)
//    {
//        return _cornerDictionary.TryGetValue(cornerPosition, out cornerIndex);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    internal Float64SampledPath3D.CornerPosition GetOrAddCornerPosition(int level, int segmentCount)
//    {
//        var newPosition = new Float64SampledPath3D.CornerPosition(level, segmentCount);

//        if (_cornerPositionSet.TryGetValue(newPosition, out var position))
//            return position;

//        _cornerPositionSet.Add(newPosition);

//        return newPosition;
//    }

//    internal Corner GetOrAddCorner(Float64SampledPath3D.CornerPosition position1, Float64SampledPath3D.CornerPosition position2)
//    {
//        var cornerPosition =
//            new Pair<Float64SampledPath3D.CornerPosition>(position1, position2);

//        if (_cornerDictionary.TryGetValue(cornerPosition, out var index))
//            return _cornerList[index];

//        var (parameterValue1, parameterValue2) =
//            GetParameterValue(cornerPosition);

//        index = _cornerList.Count;
//        var frame = Surface.GetFrame(parameterValue1, parameterValue2);
//        var corner = new Corner(this, index, frame, cornerPosition);

//        _cornerList.Add(corner);
//        _cornerDictionary.Add(cornerPosition, index);

//        return corner;
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    internal LeafNode AddLeafNode(LeafNode leafNode)
//    {
//        _leafNodeList.Add(leafNode);

//        return leafNode;
//    }

//    public Sample GetSample(double parameterValue1, double parameterValue2)
//    {
//        if (!RootNode.Contains(parameterValue1, parameterValue2))
//            throw new ArgumentOutOfRangeException();

//        var branchNode = RootNode;

//        while (true)
//        {
//            var childNode =
//                branchNode.GetChildContaining(parameterValue1, parameterValue2);

//            if (childNode is LeafNode leafNode)
//                return new Sample(
//                    leafNode,
//                    parameterValue1,
//                    parameterValue2
//                );

//            branchNode = (BranchNode)childNode;
//        }
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Pair<int> GetGridIndex(Corner cornerPosition)
//    {
//        return cornerPosition.GridIndex;
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public GrVertexListTriangleGeometry3D GenerateGeometry()
//    {
//        var geometry =
//            new GrVertexListTriangleGeometry3D(
//                CornerList.Select(c => c.Frame).ToArray()
//            );

//        var triangleIndexTriplets =
//            LeafNodesList.SelectMany(n => n.GetTriangleIndexTriplets());

//        geometry.AddTriangles(triangleIndexTriplets);

//        //var triangleGridIndexTriplets = 
//        //    surfaceTree.LeafNodes.SelectMany(n => n.GetTriangleGridIndexTriplets());

//        //foreach (var (i1, i2, i3) in triangleGridIndexTriplets)
//        //{
//        //    var s1 = $"({i1.Item1}, {i1.Item2})";
//        //    var s2 = $"({i2.Item1}, {i2.Item2})";
//        //    var s3 = $"({i3.Item1}, {i3.Item2})";

//        //    Console.WriteLine($"Triangle: <{s1}, {s2}, {s3}>");
//        //}

//        return geometry;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public bool IsValid()
//    {
//        return RootNode is not null;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public LinFloat64Vector3D GetPoint(double parameterValue1, double parameterValue2)
//    {
//        return GetSample(parameterValue1, parameterValue2).GetPoint();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public LinFloat64Vector3D GetNormal(double parameterValue1, double parameterValue2)
//    {
//        return GetSample(parameterValue1, parameterValue2).GetNormal();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public LinFloat64Vector3D GetUnitNormal(double parameterValue1, double parameterValue2)
//    {
//        return GetSample(parameterValue1, parameterValue2).GetNormal().GetUnitNormal();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public GrParametricSurfaceLocalFrame3D GetFrame(double parameterValue1, double parameterValue2)
//    {
//        return GetSample(parameterValue1, parameterValue2).GetFrame();
//    }
//}

//public static class GrParametricSurfaceTreeUtils3D
//{
//    public static GrParametricSurfaceTree3D.TreeNodeSide GetOppositeSide(this GrParametricSurfaceTree3D.TreeNodeSide side)
//    {
//        return side switch
//        {
//            GrParametricSurfaceTree3D.TreeNodeSide.SideX0 => GrParametricSurfaceTree3D.TreeNodeSide.SideX1,
//            GrParametricSurfaceTree3D.TreeNodeSide.SideX1 => GrParametricSurfaceTree3D.TreeNodeSide.SideX0,
//            GrParametricSurfaceTree3D.TreeNodeSide.Side0X => GrParametricSurfaceTree3D.TreeNodeSide.Side1X,
//            _ => GrParametricSurfaceTree3D.TreeNodeSide.Side0X
//        };
//    }

//}