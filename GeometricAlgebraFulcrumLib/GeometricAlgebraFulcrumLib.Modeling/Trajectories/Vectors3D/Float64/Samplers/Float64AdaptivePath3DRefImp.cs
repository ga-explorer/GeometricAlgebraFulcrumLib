//using System.Collections;
//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
//using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
//using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;
//using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
//// ReSharper disable CompareOfFloatsByEqualityOperator

//namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Adaptive;

///// <summary>
///// This is a tested reference implementation,
///// not so efficient but correct
///// </summary>
//public sealed class Float64AdaptivePath3D :
//    Float64ArcLengthPath3D,
//    IReadOnlyList<Float64Path3DLocalFrame>
//{
//    public sealed class SamplingOptions :
//        Float64AdaptiveSamplingOptions
//    {
//        public LocalFrameInterpolationMethod FrameInterpolationMethod { get; set; }
//            = LocalFrameInterpolationMethod.TangentInterpolation;

//        public ParametricCurveLocalFrameSamplingMethod FrameSamplingMethod { get; set; }
//            = ParametricCurveLocalFrameSamplingMethod.SimpleRotation;


//        public SamplingOptions(LinFloat64Angle maxAngleError, int minLevelCount, int maxLevelCount)
//        {
//            FrameAngleDifference = maxAngleError;
//            MinDepth = minLevelCount;
//            MaxDepth = maxLevelCount;
//        }
//    }

//    public abstract class Node :
//        IReadOnlyCollection<Node>
//    {
//        public Float64AdaptivePath3D ParentTree { get; }

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

//        public Float64Path3DLocalFrame Frame0
//            => Corner0.Frame;

//        public Float64Path3DLocalFrame Frame1
//            => Corner1.Frame;

//        public double MinParameterValue
//            => Frame0.TimeValue;

//        public double MaxParameterValue
//            => Frame1.TimeValue;

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

//                    stack.Push(branchNode.Child1);
//                    stack.Push(branchNode.Child0);
//                }
//            }
//        }


//        /// <summary>
//        /// Construct root node of tree
//        /// </summary>
//        /// <param name="parentTree"></param>
//        protected Node(Float64AdaptivePath3D parentTree)
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
//        public bool Contains(double parameterValue)
//        {
//            return parameterValue >= MinParameterValue &&
//                   parameterValue <= MaxParameterValue;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public bool ContainsLength(double length)
//        {
//            return length >= Length0 &&
//                   length <= Length1;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public Pair<Float64Path3DLocalFrame> GetEdgeFramePair()
//        {
//            return new Pair<Float64Path3DLocalFrame>(Frame0, Frame1);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public double EdgeFrameDistance()
//        {
//            return Frame0.Point.GetDistanceToPoint(Frame1.Point);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public LinFloat64PolarAngle EdgeFrameMaxAngle()
//        {
//            var maxAngle = LinFloat64PolarAngle.Angle0;

//            var angle = Frame0.Normal1.GetAngle(Frame1.Normal1);
//            if (angle.RadiansValue > maxAngle.RadiansValue) maxAngle = angle;

//            angle = Frame0.Normal2.GetAngle(Frame1.Normal2);
//            if (angle.RadiansValue > maxAngle.RadiansValue) maxAngle = angle;

//            angle = Frame0.Tangent.GetAngle(Frame1.Tangent);
//            if (angle.RadiansValue > maxAngle.RadiansValue) maxAngle = angle;

//            return maxAngle;
//        }

//        public bool HasNearEdgeFrames(SamplingOptions options)
//        {
//            var parameterDistanceMax = options.ParameterSpan;
//            var parameterDistance = Math.Abs(Frame0.TimeValue - Frame1.TimeValue);
//            if (parameterDistance <= parameterDistanceMax)
//                return true;

//            var pointDistanceMax = options.RelativeChordError;
//            var pointDistance = Frame0.Point.GetDistanceToPoint(Frame1.Point);
//            if (pointDistance <= pointDistanceMax)
//                return true;

//            var angleMax = options.FrameAngleDifference.RadiansValue;
//            var angle = Frame0.GetMaxDirectionAngleWithFrame(Frame1).RadiansValue;

//            return angle <= angleMax;

//            //var angle = Frame0.Normal1.GetAngle(Frame1.Normal1).RadiansValue;
//            //if (angle > angleMax) return false;

//            //angle = Frame0.Normal2.GetAngle(Frame1.Normal2).RadiansValue;
//            //if (angle > angleMax) return false;

//            //angle = Frame0.Tangent.GetAngle(Frame1.Tangent).RadiansValue;
//            //if (angle > angleMax) return false;

//            //return true;
//        }

//        public abstract IEnumerator<Node> GetEnumerator();

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }
//    }

//    public sealed class BranchNode : Node
//    {
//        public override int Count => 2;

//        public Node Child0 { get; private set; }

//        public Node Child1 { get; private set; }


//        /// <summary>
//        /// Constructor of the root node of the tree
//        /// </summary>
//        /// <param name="parentTree"></param>
//        internal BranchNode(Float64AdaptivePath3D parentTree)
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
//            // Update normals of second frame based on first frame
//            if (ParentTree.FrameSamplingMethod == ParametricCurveLocalFrameSamplingMethod.MinimizedRotation)
//                Frame1.SetMinimizedRotationNormals(Frame0);
//            else
//                Frame1.SetSimpleRotationNormals(Frame0);


//            var continueSubdivision =
//                // Always subdivide the root node
//                IsRoot ||

//                // Continue subdivision for the required initial number of levels
//                Level < options.MinDepth ||

//                // Continue subdivision if not at max level and frame normals are far from parallel
//                Level < options.MaxDepth && !HasNearEdgeFrames(options);

//            if (continueSubdivision)
//                return CreateBranchChildren(options);

//            // Stop subdivision and replace this branch with a leaf node
//            return ParentTree.AddLeafNode(
//                new LeafNode(ParentBranch, IsRightChild)
//            );
//        }

//        internal Node GetChildContaining(double parameterValue)
//        {
//            if (Child0.Contains(parameterValue))
//                return Child0;

//            if (Child1.Contains(parameterValue))
//                return Child1;

//            throw new InvalidOperationException();
//        }

//        internal Node GetChildContainingLength(double length)
//        {
//            if (Child0.ContainsLength(length))
//                return Child0;

//            if (Child1.ContainsLength(length))
//                return Child1;

//            throw new InvalidOperationException();
//        }

//        internal override double UpdateLengthData(double length0)
//        {
//            Length0 = length0;

//            length0 = Child0.UpdateLengthData(length0);
//            Length1 = Child1.UpdateLengthData(length0);

//            return Length1;
//        }

//        public override IEnumerator<Node> GetEnumerator()
//        {
//            yield return Child0;
//            yield return Child1;
//        }
//    }

//    public sealed class LeafNode : Node
//    {
//        public override int Count
//            => 0;

//        public int LeafListIndex { get; }

//        public LeafNode PrevLeafNode
//            => LeafListIndex >= 0
//                ? ParentTree.LeafNodesList[LeafListIndex - 1]
//                : null;

//        public LeafNode NextLeafNode
//            => LeafListIndex < ParentTree.LeafNodeCount
//                ? ParentTree.LeafNodesList[LeafListIndex + 1]
//                : null;

//        internal LeafNode(BranchNode parentBranch, bool isRightChild)
//            : base(parentBranch, isRightChild)
//        {
//            LeafListIndex = parentBranch.ParentTree.LeafNodeCount;
//        }


//        public IFloat64LineSegment3D GetLineSegment()
//        {
//            return Float64LineSegment3D.Create(
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
//        public Float64AdaptivePath3D ParentTree { get; }

//        public CornerPosition Position { get; }

//        public int Index { get; }

//        public Float64Path3DLocalFrame Frame { get; }

//        public int GridIndex
//            => Position.GetGridIndex(ParentTree.TreeLevelCount);

//        public double InterpolationValue
//            => Position.GetInterpolationValue();


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        internal Corner(Float64AdaptivePath3D parentTree, int index, Float64Path3DLocalFrame frame, CornerPosition position)
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

//    public sealed record Sample
//    {
//        public LeafNode LeafNode { get; }

//        public int LeafNodeIndex
//            => LeafNode.LeafListIndex;

//        public double ParameterValue { get; }

//        public double InterpolationValue { get; }

//        public LocalFrameInterpolationMethod FrameInterpolationMethod
//            => LeafNode.ParentTree.FrameInterpolationMethod;


//        internal Sample(LeafNode leafNode, double parameterValue)
//        {
//            LeafNode = leafNode;

//            ParameterValue = parameterValue;

//            InterpolationValue =
//                (ParameterValue - leafNode.MinParameterValue) /
//                (leafNode.MaxParameterValue - leafNode.MinParameterValue);
//        }


//        public LinFloat64Vector3D GetPoint()
//        {
//            if (ParameterValue == LeafNode.MinParameterValue)
//                return LeafNode.Frame0.Point;

//            if (ParameterValue == LeafNode.MaxParameterValue)
//                return LeafNode.Frame1.Point;

//            return InterpolationValue.Lerp(LeafNode.Frame0.Point, LeafNode.Frame1.Point);
//        }

//        public LinFloat64Vector3D GetTangent()
//        {
//            if (ParameterValue == LeafNode.MinParameterValue)
//                return LeafNode.Frame0.Tangent;

//            if (ParameterValue == LeafNode.MaxParameterValue)
//                return LeafNode.Frame1.Tangent;

//            if (FrameInterpolationMethod == LocalFrameInterpolationMethod.FrameInterpolation)
//                return LeafNode.Frame0.InterpolateTangentToTangent(LeafNode.Frame1, InterpolationValue);

//            return InterpolationValue.InterpolateByRotation(
//                LeafNode.Frame0.Tangent,
//                LeafNode.Frame1.Tangent
//            );
//        }

//        public Float64Path3DLocalFrame GetFrame()
//        {
//            if (ParameterValue == LeafNode.MinParameterValue)
//                return LeafNode.Frame0;

//            if (ParameterValue == LeafNode.MaxParameterValue)
//                return LeafNode.Frame1;

//            if (FrameInterpolationMethod == LocalFrameInterpolationMethod.FrameInterpolation)
//                return LeafNode.Frame0.InterpolateFrameToFrame(LeafNode.Frame1, InterpolationValue);


//            var point =
//                InterpolationValue.Lerp(LeafNode.Frame0.Point, LeafNode.Frame1.Point);

//            // Use rotation interpolation to find the new frame
//            var rotationMatrix =
//                InterpolationValue.GetRotationMatrixInterpolation(
//                    LeafNode.Frame0.Tangent,
//                    LeafNode.Frame1.Tangent
//                );

//            var (tangent, normal1, normal2) =
//                new Triplet<LinFloat64Vector3D>(
//                    LeafNode.Frame0.Tangent,
//                    LeafNode.Frame0.Normal1.ToLinVector3D(),
//                    LeafNode.Frame0.Normal2.ToLinVector3D()
//                ).MapFrameUsing(rotationMatrix);

//            return Float64Path3DLocalFrame.Create(
//                ParameterValue,
//                point,
//                tangent,
//                normal1,
//                normal2
//            );
//        }
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static Float64AdaptivePath3D Finite(Float64Path3D basePath)
//    {
//        return new Float64AdaptivePath3D(
//            basePath.TimeRange,
//            false,
//            basePath
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static Float64AdaptivePath3D Finite(Float64ScalarRange timeRange, Float64Path3D basePath)
//    {
//        return new Float64AdaptivePath3D(timeRange, false, basePath);
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static Float64AdaptivePath3D Periodic(Float64Path3D basePath)
//    {
//        return new Float64AdaptivePath3D(
//            basePath.TimeRange,
//            true,
//            basePath
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static Float64AdaptivePath3D Periodic(Float64ScalarRange timeRange, Float64Path3D basePath)
//    {
//        return new Float64AdaptivePath3D(timeRange, true, basePath);
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static Float64AdaptivePath3D Create(Float64Path3D basePath)
//    {
//        return new Float64AdaptivePath3D(
//            basePath.TimeRange,
//            basePath.IsPeriodic,
//            basePath
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static Float64AdaptivePath3D Create(Float64ScalarRange timeRange, Float64Path3D basePath)
//    {
//        return new Float64AdaptivePath3D(timeRange, basePath.IsPeriodic, basePath);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static Float64AdaptivePath3D Create(bool isPeriodic, Float64Path3D basePath)
//    {
//        return new Float64AdaptivePath3D(basePath.TimeRange, isPeriodic, basePath);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static Float64AdaptivePath3D Create(Float64ScalarRange timeRange, bool isPeriodic, Float64Path3D basePath)
//    {
//        return new Float64AdaptivePath3D(timeRange, isPeriodic, basePath);
//    }


//    private readonly Dictionary<CornerPosition, int> _cornerDictionary
//        = new Dictionary<CornerPosition, int>();

//    private readonly List<Corner> _cornerList
//        = new List<Corner>();

//    private readonly List<LeafNode> _leafNodeList
//        = new List<LeafNode>();


//    public int Count
//        => 1 + _leafNodeList.Count;

//    public Float64Path3DLocalFrame this[int index]
//        => index == _leafNodeList.Count
//            ? _leafNodeList[^1].Frame1
//            : _leafNodeList[index].Frame0;

//    public Float64Path3D Curve { get; }

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
//            var stack = new Stack<Node>();

//            stack.Push(RootNode);

//            while (stack.Count > 0)
//            {
//                var node = stack.Pop();

//                yield return node;

//                if (node is not BranchNode branchNode)
//                    continue;

//                stack.Push(branchNode.Child1);
//                stack.Push(branchNode.Child0);
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

//                stack.Push(branchNode.Child1);
//                stack.Push(branchNode.Child0);
//            }
//        }
//    }

//    public int LeafNodeCount
//        => _leafNodeList.Count;

//    public IReadOnlyList<LeafNode> LeafNodesList
//        => _leafNodeList;

//    public double Length
//        => RootNode.Length1;

//    public override Float64Scalar GetLength()
//    {
//        var arcLength = 0d;

//        Float64Path3DLocalFrame frame1 = null;
//        var firstFrame = true;
//        foreach (var frame2 in this)
//        {
//            if (firstFrame)
//            {
//                frame1 = frame2;
//                firstFrame = false;
//                continue;
//            }

//            arcLength += frame2.Point.GetDistanceToPoint(frame1.Point);

//            frame1 = frame2;
//        }

//        return arcLength;
//    }

//    public LocalFrameInterpolationMethod FrameInterpolationMethod { get; set; }
//        = LocalFrameInterpolationMethod.TangentInterpolation;

//    public ParametricCurveLocalFrameSamplingMethod FrameSamplingMethod { get; set; }
//        = ParametricCurveLocalFrameSamplingMethod.SimpleRotation;

//    public int CornerCount
//        => _cornerList.Count;

//    internal IReadOnlyList<Corner> CornerList
//        => _cornerList;


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private Float64AdaptivePath3D(Float64ScalarRange timeRange, bool isPeriodic, Float64Path3D curve)
//        : base(timeRange, isPeriodic)
//    {
//        Curve = curve;
//        RootNode = null;
//    }

//    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //public Float64AdaptivePath3D(Float64PointPath3D curve, double minParameterValue, double maxParameterValue)
//    //{
//    //    Debug.Assert(
//    //        minParameterValue < maxParameterValue
//    //    );

//    //    Curve = curve;
//    //    TimeRange = Float64ScalarRange.Create(minParameterValue, maxParameterValue);
//    //    RootNode = null;
//    //}

//    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //public Float64AdaptivePath3D(Float64PointPath3D curve)
//    //{
//    //    Curve = curve;
//    //    TimeRange = Float64ScalarRange.Create(0, 1);
//    //    RootNode = null;
//    //}


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Float64AdaptivePath3D Clear()
//    {
//        RootNode = null;
//        _cornerList.Clear();
//        _leafNodeList.Clear();
//        _cornerDictionary.Clear();

//        return this;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public double GetParameterValue(CornerPosition cornerPosition)
//    {
//        return cornerPosition
//            .GetInterpolationValue()
//            .Lerp(TimeRange);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Float64AdaptivePath3D GenerateTree(SamplingOptions options)
//    {
//        Clear();

//        RootNode = new BranchNode(this);

//        RootNode.GenerateTree(options);

//        RootNode.UpdateLengthData(0d);

//        return this;
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public override bool IsValid()
//    {
//        return RootNode is not null;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public override LinFloat64Vector3D GetValue(double parameterValue)
//    {
//        return GetSample(parameterValue).GetPoint();
//    }

//    public override Float64ArcLengthPath3D ToFiniteArcLengthPath()
//    {
//        throw new NotImplementedException();
//    }

//    public override Float64ArcLengthPath3D ToPeriodicArcLengthPath()
//    {
//        throw new NotImplementedException();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public override LinFloat64Vector3D GetDerivative1Value(double parameterValue)
//    {
//        return GetSample(parameterValue).GetTangent();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public override LinFloat64Vector3D GetDerivative2Value(double parameterValue)
//    {
//        throw new NotImplementedException();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public override Float64Path3DLocalFrame GetFrame(double parameterValue)
//    {
//        return GetSample(parameterValue).GetFrame();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public bool ContainsCorner(CornerPosition cornerPosition)
//    {
//        return _cornerDictionary.ContainsKey(cornerPosition);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Corner GetCorner(int cornerIndex)
//    {
//        return _cornerList[cornerIndex];
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Corner GetFrame(CornerPosition cornerPosition)
//    {
//        var cornerIndex = _cornerDictionary[cornerPosition];

//        return _cornerList[cornerIndex];
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public int GetCornerIndex(CornerPosition cornerPosition)
//    {
//        return _cornerDictionary[cornerPosition];
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public bool TryGetCorner(CornerPosition cornerPosition, out Corner corner)
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
//    public bool TryGetCornerIndex(CornerPosition cornerPosition, out int cornerIndex)
//    {
//        return _cornerDictionary.TryGetValue(cornerPosition, out cornerIndex);
//    }

//    internal Corner GetOrAddCorner(CornerPosition cornerPosition)
//    {
//        if (_cornerDictionary.TryGetValue(cornerPosition, out var index))
//            return _cornerList[index];

//        var parameterValue =
//            GetParameterValue(cornerPosition);

//        index = _cornerList.Count;
//        var frame = Curve.GetFrame(parameterValue);
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

//    public Sample GetSample(double parameterValue)
//    {
//        if (!RootNode.Contains(parameterValue))
//            throw new ArgumentOutOfRangeException();

//        var branchNode = RootNode;

//        while (true)
//        {
//            var childNode =
//                branchNode.GetChildContaining(parameterValue);

//            if (childNode is LeafNode leafNode)
//                return new Sample(
//                    leafNode,
//                    parameterValue
//                );

//            branchNode = (BranchNode)childNode;
//        }
//    }

//    public Sample GetSampleByLength(double length)
//    {
//        if (!RootNode.ContainsLength(length))
//            throw new ArgumentOutOfRangeException();

//        var branchNode = RootNode;

//        while (true)
//        {
//            var childNode =
//                branchNode.GetChildContainingLength(length);

//            if (childNode is LeafNode leafNode)
//            {
//                var t =
//                    (length - childNode.Length0) / childNode.Length;

//                var parameterValue =
//                    t.Lerp(childNode.MinParameterValue, childNode.MaxParameterValue);

//                return new Sample(leafNode, parameterValue);
//            }

//            branchNode = (BranchNode)childNode;
//        }
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public IEnumerable<LinFloat64Vector3D> GetPoints(double parameterValue)
//    {
//        return GetPoints(TimeRange.MinValue, parameterValue);
//    }

//    public IEnumerable<LinFloat64Vector3D> GetPoints(double parameterValue1, double parameterValue2)
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
//    public IEnumerable<Float64Path3DLocalFrame> GetFrames(double parameterValue)
//    {
//        return GetFrames(TimeRange.MinValue, parameterValue);
//    }

//    public IEnumerable<Float64Path3DLocalFrame> GetFrames(double parameterValue1, double parameterValue2)
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

//    public override Float64Scalar TimeToLength(double parameterValue)
//    {
//        parameterValue = parameterValue.ClampPeriodic(TimeRange.MinValue, TimeRange.MaxValue);

//        if (!RootNode.Contains(parameterValue))
//            throw new ArgumentOutOfRangeException();

//        var branchNode = RootNode;

//        while (true)
//        {
//            var childNode =
//                branchNode.GetChildContaining(parameterValue);

//            if (childNode is LeafNode)
//            {
//                var t =
//                    (parameterValue - childNode.MinParameterValue) / (childNode.MaxParameterValue - childNode.MinParameterValue);

//                var length =
//                    t.Lerp(childNode.Length0, childNode.Length1);

//                return length;
//            }

//            branchNode = (BranchNode)childNode;
//        }
//    }

//    public override Float64Scalar LengthToTime(double length)
//    {
//        if (!RootNode.ContainsLength(length))
//            throw new ArgumentOutOfRangeException();

//        var branchNode = RootNode;

//        while (true)
//        {
//            var childNode =
//                branchNode.GetChildContainingLength(length);

//            if (childNode is LeafNode)
//            {
//                var t =
//                    (length - childNode.Length0) / childNode.Length;

//                var parameterValue =
//                    t.Lerp(childNode.MinParameterValue, childNode.MaxParameterValue);

//                return parameterValue;
//            }

//            branchNode = (BranchNode)childNode;
//        }
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Float64AdaptivePath3D GetSubCurve(double parameterValue1, double parameterValue2, SamplingOptions options)
//    {
//        if (parameterValue1 > parameterValue2)
//            (parameterValue1, parameterValue2) = (parameterValue2, parameterValue1);

//        var curve = new Float64AdaptivePath3D(
//            Float64ScalarRange.Create(parameterValue1, parameterValue2),
//            false,
//            this
//        );

//        return curve.GenerateTree(options);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Float64AdaptivePath3D GetSubCurveByLength(double length1, double length2, SamplingOptions options)
//    {
//        if (length1 > length2)
//            (length1, length2) = (length2, length1);

//        return GetSubCurve(
//            LengthToTime(length1),
//            LengthToTime(length2),
//            options
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public IEnumerator<Float64Path3DLocalFrame> GetEnumerator()
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