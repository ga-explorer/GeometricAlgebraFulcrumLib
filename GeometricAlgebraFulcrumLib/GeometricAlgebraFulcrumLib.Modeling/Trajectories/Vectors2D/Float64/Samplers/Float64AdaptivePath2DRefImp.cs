//using System.Collections;
//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Matrices;
//using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
//using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
//using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
//// ReSharper disable CompareOfFloatsByEqualityOperator

//namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Adaptive;

///// <summary>
///// This is a tested reference implementation,
///// not so efficient but correct
///// </summary>
//public sealed class Float64AdaptivePath2D :
//    Float64ArcLengthPath2D,
//    IReadOnlyList<Float64Path2DLocalFrame>
//{
//    public sealed class SamplingOptions
//    {
//        private double _maxEdgeFramesParameterDistance;
//        public double MaxEdgeFramesParameterDistance
//        {
//            get => _maxEdgeFramesParameterDistance;
//            set
//            {
//                if (double.IsNaN(value) || value < 0)
//                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be > 0");

//                _maxEdgeFramesParameterDistance = value;
//            }
//        }

//        private double _maxEdgeFramesDistance = 1e-5d;
//        public double MaxEdgeFramesDistance
//        {
//            get => _maxEdgeFramesDistance;
//            set
//            {
//                if (double.IsNaN(value) || value <= 0)
//                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be > 0");

//                _maxEdgeFramesDistance = value;
//            }
//        }

//        private LinFloat64Angle _maxEdgeFramesAngle = LinFloat64PolarAngle.Angle45;
//        public LinFloat64Angle MaxEdgeFramesAngle
//        {
//            get => _maxEdgeFramesAngle;
//            set
//            {
//                if (value.DegreesValue is <= 0 or > 180)
//                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be > 0 and <= 180 degrees");

//                _maxEdgeFramesAngle = value;
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


//        public SamplingOptions(LinFloat64Angle maxAngleError, int minLevelCount, int maxLevelCount)
//        {
//            MaxEdgeFramesAngle = maxAngleError;
//            MinLevelCount = minLevelCount;
//            MaxLevelCount = maxLevelCount;
//        }
//    }

//    public abstract class Node :
//        IReadOnlyCollection<Node>
//    {
//        public Float64AdaptivePath2D ParentTree { get; }

//        public BranchNode? ParentBranch { get; }

//        public IEnumerable<BranchNode> ParentBranches
//        {
//            get
//            {
//                for (var node = ParentBranch; node is not null; node = node.ParentBranch)
//                    yield return node;
//            }
//        }

//        public bool IsRoot
//            => ParentBranch is null;

//        public bool IsLeaf
//            => this is LeafNode;

//        public bool IsBranch
//            => this is BranchNode;

//        public bool IsChild
//            => ParentBranch is not null;

//        public bool IsLeftChild
//            => (CellIndex & 1) == 0;

//        public bool IsRightChild
//            => (CellIndex & 1) == 1;

//        public abstract int Count { get; }

//        public int Level { get; }

//        public int CellIndex { get; }

//        public int FrameIndex0
//            => Corner0.Index;

//        public int FrameIndex1
//            => Corner1.Index;

//        public Corner Corner0 { get; }

//        public Corner Corner1 { get; }

//        public int GridIndex0
//            => Corner0.GridIndex;

//        public int GridIndex1
//            => Corner1.GridIndex;

//        public Float64Path2DLocalFrame Frame0
//            => Corner0.Frame;

//        public Float64Path2DLocalFrame Frame1
//            => Corner1.Frame;

//        public double MinParameterValue
//            => Frame0.Time;

//        public double MaxParameterValue
//            => Frame1.Time;

//        public double Length0 { get; internal set; }

//        public double Length1 { get; internal set; }

//        public double Length
//            => Length1 - Length0;

//        public IEnumerable<LeafNode> LeafNodes
//        {
//            get
//            {
//                var stack = new Stack<Node>();

//                stack.Push(this);

//                while (stack.Count > 0)
//                {
//                    var node = stack.Pop();

//                    if (node is not BranchNode branchNode)
//                    {
//                        yield return (LeafNode)node;
//                        continue;
//                    }

//                    if (branchNode.Child1 is not null)
//                        stack.Push(branchNode.Child1);
                    
//                    if (branchNode.Child0 is not null)
//                        stack.Push(branchNode.Child0);
//                }
//            }
//        }


//        /// <summary>
//        /// Construct root node of tree
//        /// </summary>
//        /// <param name="parentTree"></param>
//        protected Node(Float64AdaptivePath2D parentTree)
//        {
//            ParentTree = parentTree;
//            ParentBranch = null;
//            Level = 0;
//            CellIndex = 0;

//            ParentTree.TreeLevelCount = 0;

//            var position0 = new CornerPosition(0, 0);
//            var position1 = new CornerPosition(0, 1);

//            Corner0 = ParentTree.GetOrAddCorner(position0);
//            Corner1 = ParentTree.GetOrAddCorner(position1);
//        }

//        /// <summary>
//        /// Construct sub-node of tree
//        /// </summary>
//        /// <param name="parentBranch"></param>
//        /// <param name="isRightChild"></param>
//        protected Node(BranchNode parentBranch, bool isRightChild)
//        {
//            Debug.Assert(parentBranch.Level < 30);

//            ParentTree = parentBranch.ParentTree;
//            ParentBranch = parentBranch;
//            Level = parentBranch.Level + 1;
//            CellIndex = parentBranch.CellIndex << 1 | (isRightChild ? 1 : 0);

//            if (ParentTree.TreeLevelCount < Level)
//                ParentTree.TreeLevelCount = Level;

//            var position0 = new CornerPosition(Level, CellIndex);
//            var position1 = new CornerPosition(Level, CellIndex + 1);

//            Corner0 = ParentTree.GetOrAddCorner(position0);
//            Corner1 = ParentTree.GetOrAddCorner(position1);
//        }


//        internal abstract double UpdateLengthData(double length0);

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public bool Contains(double t)
//        {
//            return t >= MinParameterValue &&
//                   t <= MaxParameterValue;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public bool ContainsLength(double length)
//        {
//            return length >= Length0 &&
//                   length <= Length1;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public Pair<Float64Path2DLocalFrame> GetEdgeFramePair()
//        {
//            return new Pair<Float64Path2DLocalFrame>(Frame0, Frame1);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public double EdgeFrameDistance()
//        {
//            return Frame0.Point.GetDistanceToPoint(Frame1.Point);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public LinFloat64PolarAngle EdgeFrameMaxAngle()
//        {
//            return Frame0.Tangent.GetAngle(Frame1.Tangent);
//        }

//        public bool HasNearEdgeFrames(SamplingOptions options)
//        {
//            var parameterDistanceMax = options.MaxEdgeFramesParameterDistance;
//            var parameterDistance = Math.Abs(Frame0.Time - Frame1.Time);
//            if (parameterDistance <= parameterDistanceMax)
//                return true;

//            var pointDistanceMax = options.MaxEdgeFramesDistance;
//            var pointDistance = Frame0.Point.GetDistanceToPoint(Frame1.Point);
//            if (pointDistance <= pointDistanceMax)
//                return true;

//            var angleMax = options.MaxEdgeFramesAngle.RadiansValue;
//            var angle = Frame0.Tangent.GetAngle(Frame1.Tangent).RadiansValue;
//            if (angle <= angleMax)
//                return true;

//            return false;
//        }

//        public abstract IEnumerator<Node> GetEnumerator();

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }
//    }

//    public sealed class LeafNode :
//        Node
//    {
//        public override int Count
//            => 0;

//        public int LeafListIndex { get; }

//        public LeafNode? PrevLeafNode
//            => LeafListIndex >= 0
//                ? ParentTree.LeafNodesList[LeafListIndex - 1]
//                : null;

//        public LeafNode? NextLeafNode
//            => LeafListIndex < ParentTree.LeafNodeCount
//                ? ParentTree.LeafNodesList[LeafListIndex + 1]
//                : null;

//        internal LeafNode(BranchNode parentBranch, bool isRightChild)
//            : base(parentBranch, isRightChild)
//        {
//            LeafListIndex = parentBranch.ParentTree.LeafNodeCount;
//        }


//        public IFloat64LineSegment2D GetLineSegment()
//        {
//            return Float64LineSegment2D.Create(
//                Frame0.Point,
//                Frame1.Point
//            );
//        }

//        internal override double UpdateLengthData(double length0)
//        {
//            Length0 = length0;
//            Length1 = length0 + Frame1.Point.GetDistanceToPoint(Frame0.Point);

//            return Length1;
//        }

//        public override IEnumerator<Node> GetEnumerator()
//        {
//            return Enumerable.Empty<Node>().GetEnumerator();
//        }
//    }

//    public sealed class BranchNode :
//        Node
//    {
//        public override int Count => 2;

//        public Node? Child0 { get; private set; }

//        public Node? Child1 { get; private set; }


//        /// <summary>
//        /// Constructor of the root node of the tree
//        /// </summary>
//        /// <param name="parentTree"></param>
//        internal BranchNode(Float64AdaptivePath2D parentTree)
//            : base(parentTree)
//        {
//        }

//        internal BranchNode(BranchNode parentBranch, bool isRightChild)
//            : base(parentBranch, isRightChild)
//        {
//        }


//        private BranchNode CreateBranchChildren(SamplingOptions options)
//        {
//            Child0 = new BranchNode(this, false).GenerateTree(options);
//            Child1 = new BranchNode(this, true).GenerateTree(options);

//            return this;
//        }

//        internal Node GenerateTree(SamplingOptions options)
//        {
//            //// Update normals of second frame based on first frame
//            //if (ParentTree.FrameSamplingMethod == ParametricCurveLocalFrameSamplingMethod.MinimizedRotation)
//            //    Frame1.SetMinimizedRotationNormals(Frame0);
//            //else
//            //    Frame1.SetSimpleRotationNormals(Frame0);


//            var continueSubdivision =
//                // Always subdivide the root node
//                IsRoot ||

//                // Continue subdivision for the required initial number of levels
//                Level < options.MinLevelCount ||

//                // Continue subdivision if not at max level and frame normals are far from parallel
//                Level < options.MaxLevelCount && !HasNearEdgeFrames(options);

//            if (continueSubdivision)
//                return CreateBranchChildren(options);
            
//            // Stop subdivision and replace this branch with a leaf node
//            return ParentTree.AddLeafNode(
//                new LeafNode(ParentBranch!, IsRightChild)
//            );
//        }

//        internal Node GetChildContaining(double t)
//        {
//            if (Child0?.Contains(t) ?? false)
//                return Child0;

//            if (Child1?.Contains(t) ?? false)
//                return Child1;

//            throw new InvalidOperationException();
//        }

//        internal Node GetChildContainingLength(double length)
//        {
//            if (Child0?.ContainsLength(length) ?? false)
//                return Child0;

//            if (Child1?.ContainsLength(length) ?? false)
//                return Child1;

//            throw new InvalidOperationException();
//        }

//        internal override double UpdateLengthData(double length0)
//        {
//            Length0 = length0;

//            length0 = Child0?.UpdateLengthData(length0) ?? length0;
//            Length1 = Child1?.UpdateLengthData(length0) ?? length0;

//            return Length1;
//        }

//        public override IEnumerator<Node> GetEnumerator()
//        {
//            if (Child0 is not null) yield return Child0;
//            if (Child1 is not null) yield return Child1;
//        }
//    }

//    public sealed record CornerPosition :
//        IComparable<CornerPosition>
//    {
//        public int Level { get; }

//        public int SegmentCount { get; }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public CornerPosition(int level, int segmentCount)
//        {
//            Debug.Assert(level is >= 0 and <= 31);
//            Debug.Assert(segmentCount >= 0 && segmentCount <= 1 << level);

//            if (segmentCount == 0)
//                level = 0;
//            else
//                while ((segmentCount & 1) == 0)
//                {
//                    segmentCount >>= 1;
//                    level--;
//                }

//            Level = level;
//            SegmentCount = segmentCount;
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public int GetGridIndex(int maxTreeLevel)
//        {
//            Debug.Assert(maxTreeLevel >= Level);

//            return SegmentCount << maxTreeLevel - Level;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public double GetInterpolationValue()
//        {
//            return SegmentCount / (double)(1 << Level);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public int CompareTo(CornerPosition? other)
//        {
//            if (other is null)
//                throw new ArgumentNullException(nameof(other));

//            if (other.Level == Level)
//                return SegmentCount - other.SegmentCount;

//            if (other.Level > Level)
//                return (SegmentCount << other.Level - Level) - other.SegmentCount;

//            return SegmentCount - (other.SegmentCount << Level - other.Level);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override string ToString()
//        {
//            return $"(L{Level}, S{SegmentCount})";
//        }
//    }

//    public sealed record Corner
//    {
//        public Float64AdaptivePath2D ParentTree { get; }

//        public CornerPosition Position { get; }

//        public int Index { get; }

//        public Float64Path2DLocalFrame Frame { get; }

//        public int GridIndex
//            => Position.GetGridIndex(ParentTree.TreeLevelCount);

//        public double InterpolationValue
//            => Position.GetInterpolationValue();


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        internal Corner(Float64AdaptivePath2D parentTree, int index, Float64Path2DLocalFrame frame, CornerPosition position)
//        {
//            ParentTree = parentTree;
//            Index = index;
//            Frame = frame;
//            Position = position;
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override string ToString()
//        {
//            return $"({GridIndex})";
//        }
//    }

//    public sealed record PathSample
//    {
//        public LeafNode LeafNode { get; }

//        public int LeafNodeIndex
//            => LeafNode.LeafListIndex;

//        public double ParameterValue { get; }

//        public double InterpolationValue { get; }

//        public ParametricCurveLocalFrameInterpolationMethod FrameInterpolationMethod
//            => LeafNode.ParentTree.FrameInterpolationMethod;


//        internal PathSample(LeafNode leafNode, double t)
//        {
//            LeafNode = leafNode;

//            ParameterValue = t;

//            InterpolationValue =
//                (ParameterValue - leafNode.MinParameterValue) /
//                (leafNode.MaxParameterValue - leafNode.MinParameterValue);
//        }


//        public LinFloat64Vector2D GetPoint()
//        {
//            if (ParameterValue == LeafNode.MinParameterValue)
//                return LeafNode.Frame0.Point;

//            if (ParameterValue == LeafNode.MaxParameterValue)
//                return LeafNode.Frame1.Point;

//            return InterpolationValue.Lerp(LeafNode.Frame0.Point, LeafNode.Frame1.Point);
//        }

//        public LinFloat64Vector2D GetTangent()
//        {
//            if (ParameterValue == LeafNode.MinParameterValue)
//                return LeafNode.Frame0.Tangent;

//            if (ParameterValue == LeafNode.MaxParameterValue)
//                return LeafNode.Frame1.Tangent;

//            if (FrameInterpolationMethod == ParametricCurveLocalFrameInterpolationMethod.TangentLinearInterpolation)
//                return InterpolationValue.Lerp(
//                    LeafNode.Frame0.Tangent,
//                    LeafNode.Frame1.Tangent
//                ).ToUnitLinVector2D();

//            var angle =
//                LeafNode.Frame0.Tangent.GetAngle(
//                    LeafNode.Frame1.Tangent
//                );

//            return SquareMatrix3
//                .CreateRotationMatrix2D(angle.AngleTimes(InterpolationValue))
//                .MapAffineVector(LeafNode.Frame0.Tangent);
//        }

//        public Float64Path2DLocalFrame GetFrame()
//        {
//            if (ParameterValue == LeafNode.MinParameterValue)
//                return LeafNode.Frame0;

//            if (ParameterValue == LeafNode.MaxParameterValue)
//                return LeafNode.Frame1;

//            var point =
//                InterpolationValue.Lerp(LeafNode.Frame0.Point, LeafNode.Frame1.Point);

//            LinFloat64Vector2D tangent;

//            if (FrameInterpolationMethod == ParametricCurveLocalFrameInterpolationMethod.TangentLinearInterpolation)
//            {
//                // Use linear interpolation to find the new tangent
//                tangent = InterpolationValue.Lerp(LeafNode.Frame0.Tangent, LeafNode.Frame1.Tangent).ToUnitLinVector2D();
//            }
//            else
//            {
//                // Use spherical linear interpolation on the whole frame
//                var angle =
//                    LeafNode.Frame0.Tangent.GetAngle(
//                        LeafNode.Frame1.Tangent
//                    );

//                tangent =
//                    SquareMatrix3
//                        .CreateRotationMatrix2D(angle.AngleTimes(InterpolationValue))
//                        .MapAffineVector(
//                            LeafNode.Frame0.Tangent
//                        );
//            }

//            return Float64Path2DLocalFrame.Create(
//                ParameterValue,
//                point,
//                tangent
//            );
//        }
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static Float64AdaptivePath2D Finite(Float64Path2D basePath)
//    {
//        return new Float64AdaptivePath2D(
//            basePath.TimeRange,
//            false,
//            basePath
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static Float64AdaptivePath2D Finite(Float64ScalarRange timeRange, Float64Path2D basePath)
//    {
//        return new Float64AdaptivePath2D(timeRange, false, basePath);
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static Float64AdaptivePath2D Periodic(Float64Path2D basePath)
//    {
//        return new Float64AdaptivePath2D(
//            basePath.TimeRange,
//            true,
//            basePath
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static Float64AdaptivePath2D Periodic(Float64ScalarRange timeRange, Float64Path2D basePath)
//    {
//        return new Float64AdaptivePath2D(timeRange, true, basePath);
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static Float64AdaptivePath2D Create(Float64Path2D basePath)
//    {
//        return new Float64AdaptivePath2D(
//            basePath.TimeRange,
//            basePath.IsPeriodic,
//            basePath
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static Float64AdaptivePath2D Create(Float64ScalarRange timeRange, Float64Path2D basePath)
//    {
//        return new Float64AdaptivePath2D(timeRange, basePath.IsPeriodic, basePath);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static Float64AdaptivePath2D Create(bool isPeriodic, Float64Path2D basePath)
//    {
//        return new Float64AdaptivePath2D(basePath.TimeRange, isPeriodic, basePath);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static Float64AdaptivePath2D Create(Float64ScalarRange timeRange, bool isPeriodic, Float64Path2D basePath)
//    {
//        return new Float64AdaptivePath2D(timeRange, isPeriodic, basePath);
//    }


//    private readonly Dictionary<CornerPosition, int> _cornerDictionary
//        = new Dictionary<CornerPosition, int>();

//    private readonly List<Corner> _cornerList
//        = new List<Corner>();

//    private readonly List<LeafNode> _leafNodeList
//        = new List<LeafNode>();


//    public int Count
//        => 1 + _leafNodeList.Count;

//    public Float64Path2DLocalFrame this[int index]
//        => index == _leafNodeList.Count
//            ? _leafNodeList[^1].Frame1
//            : _leafNodeList[index].Frame0;

//    public Float64Path2D Curve { get; }

//    public BranchNode? RootNode { get; private set; }

//    public int TreeLevelCount { get; internal set; }

//    /// <summary>
//    /// The number of segments per grid side for this tree
//    /// </summary>
//    public int GridSegmentCount
//        => 1 << TreeLevelCount;

//    public IEnumerable<Node> Nodes
//    {
//        get
//        {
//            if (RootNode is null)
//                yield break;

//            var stack = new Stack<Node>();

//            stack.Push(RootNode);

//            while (stack.Count > 0)
//            {
//                var node = stack.Pop();

//                yield return node;

//                if (node is not BranchNode branchNode)
//                    continue;

//                if (branchNode.Child1 is not null)
//                    stack.Push(branchNode.Child1);

//                if (branchNode.Child0 is not null)
//                    stack.Push(branchNode.Child0);
//            }
//        }
//    }

//    public IEnumerable<BranchNode> BranchNodes
//    {
//        get
//        {
//            if (RootNode is null)
//                yield break;

//            var stack = new Stack<BranchNode>();

//            stack.Push(RootNode);

//            while (stack.Count > 0)
//            {
//                var branchNode = stack.Pop();

//                yield return branchNode;

//                if (branchNode.Child1 is BranchNode childBranchNode1)
//                    stack.Push(childBranchNode1);

//                if (branchNode.Child0 is BranchNode childBranchNode0)
//                    stack.Push(childBranchNode0);
//            }
//        }
//    }

//    public IEnumerable<LeafNode> LeafNodes
//    {
//        get
//        {
//            if (RootNode is null)
//                yield break;

//            var stack = new Stack<Node>();

//            stack.Push(RootNode);

//            while (stack.Count > 0)
//            {
//                var node = stack.Pop();

//                if (node is not BranchNode branchNode)
//                {
//                    yield return (LeafNode)node;
//                    continue;
//                }

//                if (branchNode.Child1 is not null)
//                    stack.Push(branchNode.Child1);

//                if (branchNode.Child0 is not null)
//                    stack.Push(branchNode.Child0);
//            }
//        }
//    }

//    public int LeafNodeCount
//        => _leafNodeList.Count;

//    public IReadOnlyList<LeafNode> LeafNodesList
//        => _leafNodeList;

//    public double Length
//        => RootNode?.Length1 ?? 0;

//    public override Float64Scalar GetLength()
//    {
//        var arcLength = 0d;

//        Float64Path2DLocalFrame? frame1 = null;
//        var firstFrame = true;
//        foreach (var frame2 in this)
//        {
//            if (firstFrame)
//            {
//                frame1 = frame2;
//                firstFrame = false;
//                continue;
//            }

//            arcLength += frame2.Point.GetDistanceToPoint(frame1?.Point ?? frame2.Point);

//            frame1 = frame2;
//        }

//        return arcLength;
//    }

//    public double ParameterValueMin
//        => TimeRange.MinValue;

//    public double ParameterValueMax
//        => TimeRange.MaxValue;

//    public ParametricCurveLocalFrameInterpolationMethod FrameInterpolationMethod { get; set; }
//        = ParametricCurveLocalFrameInterpolationMethod.TangentLinearInterpolation;

//    public ParametricCurveLocalFrameSamplingMethod FrameSamplingMethod { get; set; }
//        = ParametricCurveLocalFrameSamplingMethod.SimpleRotation;

//    public int CornerCount
//        => _cornerList.Count;

//    internal IReadOnlyList<Corner> CornerList
//        => _cornerList;


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private Float64AdaptivePath2D(Float64ScalarRange timeRange, bool isPeriodic, Float64Path2D basePath)
//        : base(timeRange, isPeriodic)
//    {
//        Curve = basePath;
//        RootNode = null;
//    }
    

//    private Corner GetOrAddCorner(CornerPosition cornerPosition)
//    {
//        if (_cornerDictionary.TryGetValue(cornerPosition, out var index))
//            return _cornerList[index];

//        var t =
//            GetParameterValue(cornerPosition);

//        index = _cornerList.Count;
//        var frame = Curve.GetFrame(t);
//        var corner = new Corner(this, index, frame, cornerPosition);

//        _cornerList.Add(corner);
//        _cornerDictionary.Add(cornerPosition, index);

//        return corner;
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private LeafNode AddLeafNode(LeafNode leafNode)
//    {
//        _leafNodeList.Add(leafNode);

//        return leafNode;
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private bool ContainsCorner(CornerPosition cornerPosition)
//    {
//        return _cornerDictionary.ContainsKey(cornerPosition);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private Corner GetCorner(int cornerIndex)
//    {
//        return _cornerList[cornerIndex];
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private Corner GetFrame(CornerPosition cornerPosition)
//    {
//        var cornerIndex = _cornerDictionary[cornerPosition];

//        return _cornerList[cornerIndex];
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private int GetCornerIndex(CornerPosition cornerPosition)
//    {
//        return _cornerDictionary[cornerPosition];
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private bool TryGetCorner(CornerPosition cornerPosition, out Corner? corner)
//    {
//        if (_cornerDictionary.TryGetValue(cornerPosition, out var cornerIndex))
//        {
//            corner = _cornerList[cornerIndex];
//            return true;
//        }

//        corner = null;
//        return false;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private bool TryGetCornerIndex(CornerPosition cornerPosition, out int cornerIndex)
//    {
//        return _cornerDictionary.TryGetValue(cornerPosition, out cornerIndex);
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private double GetParameterValue(CornerPosition cornerPosition)
//    {
//        return cornerPosition
//            .GetInterpolationValue()
//            .Lerp(ParameterValueMin, ParameterValueMax);
//    }

    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public override bool IsValid()
//    {
//        return RootNode is not null;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Float64AdaptivePath2D Clear()
//    {
//        RootNode = null;
//        _cornerList.Clear();
//        _leafNodeList.Clear();
//        _cornerDictionary.Clear();

//        return this;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Float64AdaptivePath2D Generate(SamplingOptions options)
//    {
//        Clear();

//        RootNode = new BranchNode(this);

//        RootNode.GenerateTree(options);

//        RootNode.UpdateLengthData(0d);

//        return this;
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public override LinFloat64Vector2D GetValue(double t)
//    {
//        return GetSample(t).GetPoint();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public override LinFloat64Vector2D GetDerivative1Value(double t)
//    {
//        return GetSample(t).GetTangent();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public override LinFloat64Vector2D GetDerivative2Value(double t)
//    {
//        throw new NotImplementedException();
//        //return GetSample(t).GetTangent();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public override Float64Path2DLocalFrame GetFrame(double t)
//    {
//        return GetSample(t).GetFrame();
//    }
    
//    public PathSample GetSample(double t)
//    {
//        if (RootNode is null || !RootNode.Contains(t))
//            throw new ArgumentOutOfRangeException();

//        var branchNode = RootNode;

//        while (true)
//        {
//            var childNode =
//                branchNode.GetChildContaining(t);

//            if (childNode is LeafNode leafNode)
//                return new PathSample(
//                    leafNode,
//                    t
//                );

//            branchNode = (BranchNode)childNode;
//        }
//    }

//    public PathSample GetSampleByLength(double length)
//    {
//        if (RootNode is null || !RootNode.ContainsLength(length))
//            throw new ArgumentOutOfRangeException();

//        var branchNode = RootNode;

//        while (true)
//        {
//            var childNode =
//                branchNode.GetChildContainingLength(length);

//            if (childNode is LeafNode leafNode)
//            {
//                var l =
//                    (length - childNode.Length0) / childNode.Length;

//                var t =
//                    l.Lerp(childNode.MinParameterValue, childNode.MaxParameterValue);

//                return new PathSample(leafNode, t);
//            }

//            branchNode = (BranchNode)childNode;
//        }
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public IEnumerable<LinFloat64Vector2D> GetPoints(double t)
//    {
//        return GetPoints(ParameterValueMin, t);
//    }

//    public IEnumerable<LinFloat64Vector2D> GetPoints(double parameterValue1, double parameterValue2)
//    {
//        if (parameterValue1 > parameterValue2)
//            (parameterValue1, parameterValue2) = (parameterValue2, parameterValue1);

//        var sample1 = GetSample(parameterValue1);
//        var sample2 = GetSample(parameterValue2);

//        yield return sample1.GetPoint();

//        var index1 = sample1.LeafNodeIndex + 1;
//        var index2 = sample2.LeafNodeIndex;
//        for (var index = index1; index <= index2; index++)
//        {
//            var leafNode = _leafNodeList[index];

//            yield return leafNode.Frame0.Point;
//        }

//        if (parameterValue2 > sample2.LeafNode.MinParameterValue)
//            yield return sample2.GetPoint();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public IEnumerable<Float64Path2DLocalFrame> GetFrames(double t)
//    {
//        return GetFrames(ParameterValueMin, t);
//    }

//    public IEnumerable<Float64Path2DLocalFrame> GetFrames(double parameterValue1, double parameterValue2)
//    {
//        if (parameterValue1 > parameterValue2)
//            (parameterValue1, parameterValue2) = (parameterValue2, parameterValue1);

//        var sample1 = GetSample(parameterValue1);
//        var sample2 = GetSample(parameterValue2);

//        yield return sample1.GetFrame();

//        var index1 = sample1.LeafNodeIndex + 1;
//        var index2 = sample2.LeafNodeIndex;
//        for (var index = index1; index <= index2; index++)
//        {
//            var leafNode = _leafNodeList[index];

//            yield return leafNode.Frame0;
//        }

//        if (parameterValue2 > sample2.LeafNode.MinParameterValue)
//            yield return sample2.GetFrame();
//    }

//    public override Float64Scalar TimeToLength(double t)
//    {
//        t = t.ClampPeriodic(ParameterValueMin, ParameterValueMax);

//        if (RootNode is null || !RootNode.Contains(t))
//            throw new ArgumentOutOfRangeException();

//        var branchNode = RootNode;

//        while (true)
//        {
//            var childNode =
//                branchNode.GetChildContaining(t);

//            if (childNode is LeafNode)
//            {
//                var t1 =
//                    (t - childNode.MinParameterValue) / (childNode.MaxParameterValue - childNode.MinParameterValue);

//                var length =
//                    t1.Lerp(childNode.Length0, childNode.Length1);

//                return length;
//            }

//            branchNode = (BranchNode)childNode;
//        }
//    }

//    public override Float64Scalar LengthToTime(double length)
//    {
//        if (RootNode is null || !RootNode.ContainsLength(length))
//            throw new ArgumentOutOfRangeException();

//        var branchNode = RootNode;

//        while (true)
//        {
//            var childNode =
//                branchNode.GetChildContainingLength(length);

//            if (childNode is LeafNode)
//            {
//                var l =
//                    (length - childNode.Length0) / childNode.Length;

//                var t =
//                    l.Lerp(childNode.MinParameterValue, childNode.MaxParameterValue);

//                return t;
//            }

//            branchNode = (BranchNode)childNode;
//        }
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Float64AdaptivePath2D GetFiniteSubCurve(double parameterValue1, double parameterValue2, SamplingOptions options)
//    {
//        if (parameterValue1 > parameterValue2)
//            (parameterValue1, parameterValue2) = (parameterValue2, parameterValue1);

//        var curve = new Float64AdaptivePath2D(
//            Float64ScalarRange.Create(parameterValue1, parameterValue2),
//            false,
//            this
//        );

//        return curve.Generate(options);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Float64AdaptivePath2D GetSubCurveByLength(double length1, double length2, SamplingOptions options)
//    {
//        if (length1 > length2)
//            (length1, length2) = (length2, length1);

//        return GetFiniteSubCurve(
//            LengthToTime(length1),
//            LengthToTime(length2),
//            options
//        );
//    }

    
//    public override Float64ArcLengthPath2D ToFiniteArcLengthPath()
//    {
//        throw new NotImplementedException();
//    }

//    public override Float64ArcLengthPath2D ToPeriodicArcLengthPath()
//    {
//        throw new NotImplementedException();
//    }
    

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public IEnumerator<Float64Path2DLocalFrame> GetEnumerator()
//    {
//        if (_leafNodeList.Count == 0)
//            yield break;

//        foreach (var leafNode in _leafNodeList)
//            yield return leafNode.Frame0;

//        yield return _leafNodeList[^1].Frame1;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    IEnumerator IEnumerable.GetEnumerator()
//    {
//        return GetEnumerator();
//    }
//}