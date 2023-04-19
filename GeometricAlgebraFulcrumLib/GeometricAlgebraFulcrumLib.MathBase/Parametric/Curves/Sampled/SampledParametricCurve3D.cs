using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Borders.Space1D;
using GeometricAlgebraFulcrumLib.MathBase.Borders.Space1D.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Frames;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Sampled
{
    public sealed class SampledParametricCurve3D :
        IArcLengthC1Curve3D,
        IReadOnlyList<ParametricCurveLocalFrame3D>
    {
        private readonly Dictionary<ParametricTreeCornerPosition3D, int> _cornerDictionary
            = new Dictionary<ParametricTreeCornerPosition3D, int>();

        private readonly List<SampledParametricCurveTreeCorner3D> _cornerList
            = new List<SampledParametricCurveTreeCorner3D>();

        private readonly List<SampledParametricCurveTreeLeaf3D> _leafNodeList
            = new List<SampledParametricCurveTreeLeaf3D>();


        public int Count
            => 1 + _leafNodeList.Count;

        public ParametricCurveLocalFrame3D this[int index]
            => index == _leafNodeList.Count
                ? _leafNodeList[^1].Frame1
                : _leafNodeList[index].Frame0;

        public IParametricCurve3D Curve { get; }

        public SampledParametricCurveTreeBranch3D RootNode { get; private set; }

        public int TreeLevelCount { get; internal set; }

        /// <summary>
        /// The number of segments per grid side for this tree
        /// </summary>
        public int GridSegmentCount
            => 1 << TreeLevelCount;

        public IEnumerable<SampledParametricCurveTreeNode3D> Nodes
        {
            get
            {
                var stack = new Stack<SampledParametricCurveTreeNode3D>();

                stack.Push(RootNode);

                while (stack.Count > 0)
                {
                    var node = stack.Pop();

                    yield return node;

                    if (node is not SampledParametricCurveTreeBranch3D branchNode)
                        continue;

                    stack.Push(branchNode.Child1);
                    stack.Push(branchNode.Child0);
                }
            }
        }

        public IEnumerable<SampledParametricCurveTreeBranch3D> BranchNodes
        {
            get
            {
                var stack = new Stack<SampledParametricCurveTreeBranch3D>();

                stack.Push(RootNode);

                while (stack.Count > 0)
                {
                    var branchNode = stack.Pop();

                    yield return branchNode;

                    if (branchNode.Child1 is SampledParametricCurveTreeBranch3D childBranchNode1)
                        stack.Push(childBranchNode1);

                    if (branchNode.Child0 is SampledParametricCurveTreeBranch3D childBranchNode0)
                        stack.Push(childBranchNode0);
                }
            }
        }

        public IEnumerable<SampledParametricCurveTreeLeaf3D> LeafNodes
        {
            get
            {
                var stack = new Stack<SampledParametricCurveTreeNode3D>();

                stack.Push(RootNode);

                while (stack.Count > 0)
                {
                    var node = stack.Pop();

                    if (node is not SampledParametricCurveTreeBranch3D branchNode)
                    {
                        yield return (SampledParametricCurveTreeLeaf3D)node;
                        continue;
                    }

                    stack.Push(branchNode.Child1);
                    stack.Push(branchNode.Child0);
                }
            }
        }

        public int LeafNodeCount
            => _leafNodeList.Count;

        public IReadOnlyList<SampledParametricCurveTreeLeaf3D> LeafNodesList
            => _leafNodeList;

        public double Length
            => RootNode.Length1;

        public IBoundingBox1D ParameterValueRange { get; }

        public double GetLength()
        {
            var arcLength = 0d;

            ParametricCurveLocalFrame3D frame1 = null;
            var firstFrame = true;
            foreach (var frame2 in this)
            {
                if (firstFrame)
                {
                    frame1 = frame2;
                    firstFrame = false;
                    continue;
                }

                arcLength += frame2.Point.GetDistanceToPoint(frame1.Point);

                frame1 = frame2;
            }

            return arcLength;
        }

        public double ParameterValueMin
            => ParameterValueRange.MinValue;

        public double ParameterValueMax
            => ParameterValueRange.MaxValue;

        public ParametricCurveLocalFrameInterpolationMethod FrameInterpolationMethod { get; set; }
            = ParametricCurveLocalFrameInterpolationMethod.TangentLinearInterpolation;

        public ParametricCurveLocalFrameSamplingMethod FrameSamplingMethod { get; set; }
            = ParametricCurveLocalFrameSamplingMethod.SimpleRotation;

        public int CornerCount
            => _cornerList.Count;

        internal IReadOnlyList<SampledParametricCurveTreeCorner3D> CornerList
            => _cornerList;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SampledParametricCurve3D(IParametricCurve3D surface, IBoundingBox1D parameterValueRange)
        {
            Curve = surface;
            ParameterValueRange = parameterValueRange;
            RootNode = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SampledParametricCurve3D(IParametricCurve3D surface, double minParameterValue, double maxParameterValue)
        {
            Debug.Assert(
                minParameterValue < maxParameterValue
            );

            Curve = surface;
            ParameterValueRange = BoundingBox1D.Create(minParameterValue, maxParameterValue);
            RootNode = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SampledParametricCurve3D(IParametricCurve3D surface)
        {
            Curve = surface;
            ParameterValueRange = BoundingBox1D.Create(0, 1);
            RootNode = null;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SampledParametricCurve3D Clear()
        {
            RootNode = null;
            _cornerList.Clear();
            _leafNodeList.Clear();
            _cornerDictionary.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetParameterValue(ParametricTreeCornerPosition3D cornerPosition)
        {
            return cornerPosition
                .GetInterpolationValue()
                .Lerp(ParameterValueMin, ParameterValueMax);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SampledParametricCurve3D GenerateTree(SampledParametricCurveTreeOptions3D options)
        {
            Clear();

            RootNode = new SampledParametricCurveTreeBranch3D(this);

            RootNode.GenerateTree(options);

            RootNode.UpdateLengthData(0d);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return RootNode is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetPoint(double parameterValue)
        {
            return GetSample(parameterValue).GetPoint();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetDerivative1Point(double parameterValue)
        {
            return GetSample(parameterValue).GetTangent();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            return GetSample(parameterValue).GetFrame();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsCorner(ParametricTreeCornerPosition3D cornerPosition)
        {
            return _cornerDictionary.ContainsKey(cornerPosition);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SampledParametricCurveTreeCorner3D GetCorner(int cornerIndex)
        {
            return _cornerList[cornerIndex];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SampledParametricCurveTreeCorner3D GetFrame(ParametricTreeCornerPosition3D cornerPosition)
        {
            var cornerIndex = _cornerDictionary[cornerPosition];

            return _cornerList[cornerIndex];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetCornerIndex(ParametricTreeCornerPosition3D cornerPosition)
        {
            return _cornerDictionary[cornerPosition];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCorner(ParametricTreeCornerPosition3D cornerPosition, out SampledParametricCurveTreeCorner3D corner)
        {
            if (_cornerDictionary.TryGetValue(cornerPosition, out var cornerIndex))
            {
                corner = _cornerList[cornerIndex];
                return true;
            }

            corner = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCornerIndex(ParametricTreeCornerPosition3D cornerPosition, out int cornerIndex)
        {
            return _cornerDictionary.TryGetValue(cornerPosition, out cornerIndex);
        }

        internal SampledParametricCurveTreeCorner3D GetOrAddCorner(ParametricTreeCornerPosition3D cornerPosition)
        {
            if (_cornerDictionary.TryGetValue(cornerPosition, out var index))
                return _cornerList[index];

            var parameterValue =
                GetParameterValue(cornerPosition);

            index = _cornerList.Count;
            var frame = Curve.GetFrame(parameterValue);
            var corner = new SampledParametricCurveTreeCorner3D(this, index, frame, cornerPosition);

            _cornerList.Add(corner);
            _cornerDictionary.Add(cornerPosition, index);

            return corner;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal SampledParametricCurveTreeLeaf3D AddLeafNode(SampledParametricCurveTreeLeaf3D leafNode)
        {
            _leafNodeList.Add(leafNode);

            return leafNode;
        }

        public SampledParametricCurveTreeSample3D GetSample(double parameterValue)
        {
            if (!RootNode.Contains(parameterValue))
                throw new ArgumentOutOfRangeException();

            var branchNode = RootNode;

            while (true)
            {
                var childNode =
                    branchNode.GetChildContaining(parameterValue);

                if (childNode is SampledParametricCurveTreeLeaf3D leafNode)
                    return new SampledParametricCurveTreeSample3D(
                        leafNode,
                        parameterValue
                    );

                branchNode = (SampledParametricCurveTreeBranch3D)childNode;
            }
        }

        public SampledParametricCurveTreeSample3D GetSampleByLength(double length)
        {
            if (!RootNode.ContainsLength(length))
                throw new ArgumentOutOfRangeException();

            var branchNode = RootNode;

            while (true)
            {
                var childNode =
                    branchNode.GetChildContainingLength(length);

                if (childNode is SampledParametricCurveTreeLeaf3D leafNode)
                {
                    var t =
                        (length - childNode.Length0) / childNode.Length;

                    var parameterValue =
                        t.Lerp(childNode.MinParameterValue, childNode.MaxParameterValue);

                    return new SampledParametricCurveTreeSample3D(leafNode, parameterValue);
                }

                branchNode = (SampledParametricCurveTreeBranch3D)childNode;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<Float64Tuple3D> GetPoints(double parameterValue)
        {
            return GetPoints(ParameterValueMin, parameterValue);
        }

        public IEnumerable<Float64Tuple3D> GetPoints(double parameterValue1, double parameterValue2)
        {
            if (parameterValue1 > parameterValue2)
                (parameterValue1, parameterValue2) = (parameterValue2, parameterValue1);

            var sample1 = GetSample(parameterValue1);
            var sample2 = GetSample(parameterValue2);

            yield return sample1.GetPoint();

            var index1 = sample1.LeafNodeIndex + 1;
            var index2 = sample2.LeafNodeIndex;
            for (var index = index1; index <= index2; index++)
            {
                var leafNode = _leafNodeList[index];

                yield return leafNode.Frame0.Point;
            }

            if (parameterValue2 > sample2.LeafNode.MinParameterValue)
                yield return sample2.GetPoint();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ParametricCurveLocalFrame3D> GetFrames(double parameterValue)
        {
            return GetFrames(ParameterValueMin, parameterValue);
        }

        public IEnumerable<ParametricCurveLocalFrame3D> GetFrames(double parameterValue1, double parameterValue2)
        {
            if (parameterValue1 > parameterValue2)
                (parameterValue1, parameterValue2) = (parameterValue2, parameterValue1);

            var sample1 = GetSample(parameterValue1);
            var sample2 = GetSample(parameterValue2);

            yield return sample1.GetFrame();

            var index1 = sample1.LeafNodeIndex + 1;
            var index2 = sample2.LeafNodeIndex;
            for (var index = index1; index <= index2; index++)
            {
                var leafNode = _leafNodeList[index];

                yield return leafNode.Frame0;
            }

            if (parameterValue2 > sample2.LeafNode.MinParameterValue)
                yield return sample2.GetFrame();
        }

        public double ParameterToLength(double parameterValue)
        {
            parameterValue = parameterValue.ClampPeriodic(ParameterValueMin, ParameterValueMax);

            if (!RootNode.Contains(parameterValue))
                throw new ArgumentOutOfRangeException();

            var branchNode = RootNode;

            while (true)
            {
                var childNode =
                    branchNode.GetChildContaining(parameterValue);

                if (childNode is SampledParametricCurveTreeLeaf3D)
                {
                    var t =
                        (parameterValue - childNode.MinParameterValue) / (childNode.MaxParameterValue - childNode.MinParameterValue);

                    var length =
                        t.Lerp(childNode.Length0, childNode.Length1);

                    return length;
                }

                branchNode = (SampledParametricCurveTreeBranch3D)childNode;
            }
        }

        public double LengthToParameter(double length)
        {
            if (!RootNode.ContainsLength(length))
                throw new ArgumentOutOfRangeException();

            var branchNode = RootNode;

            while (true)
            {
                var childNode =
                    branchNode.GetChildContainingLength(length);

                if (childNode is SampledParametricCurveTreeLeaf3D)
                {
                    var t =
                        (length - childNode.Length0) / childNode.Length;

                    var parameterValue =
                        t.Lerp(childNode.MinParameterValue, childNode.MaxParameterValue);

                    return parameterValue;
                }

                branchNode = (SampledParametricCurveTreeBranch3D)childNode;
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SampledParametricCurve3D GetSubCurve(double parameterValue1, double parameterValue2, SampledParametricCurveTreeOptions3D options)
        {
            if (parameterValue1 > parameterValue2)
                (parameterValue1, parameterValue2) = (parameterValue2, parameterValue1);

            var curve = new SampledParametricCurve3D(
                this,
                parameterValue1,
                parameterValue2
            );

            return curve.GenerateTree(options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SampledParametricCurve3D GetSubCurveByLength(double length1, double length2, SampledParametricCurveTreeOptions3D options)
        {
            if (length1 > length2)
                (length1, length2) = (length2, length1);

            return GetSubCurve(
                LengthToParameter(length1),
                LengthToParameter(length2),
                options
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<ParametricCurveLocalFrame3D> GetEnumerator()
        {
            if (_leafNodeList.Count == 0)
                yield break;

            foreach (var leafNode in _leafNodeList)
                yield return leafNode.Frame0;

            yield return _leafNodeList[^1].Frame1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}