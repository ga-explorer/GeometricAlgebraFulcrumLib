using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.Borders.Space1D;
using NumericalGeometryLib.Borders.Space1D.Immutable;
using GraphicsComposerLib.Geometry.Primitives.Lines;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Sampled
{
    public sealed class GrParametricCurveTree3D :
        IGraphicsC1ArcLengthCurve3D,
        IReadOnlyList<GrParametricCurveLocalFrame3D>
    {
        private readonly Dictionary<GrParametricTreeCornerPosition3D, int> _cornerDictionary
            = new Dictionary<GrParametricTreeCornerPosition3D, int>();

        private readonly List<GrParametricCurveTreeCorner3D> _cornerList
            = new List<GrParametricCurveTreeCorner3D>();

        private readonly List<GrParametricCurveTreeLeaf3D> _leafNodeList
            = new List<GrParametricCurveTreeLeaf3D>();


        public int Count 
            => 1 + _leafNodeList.Count;

        public GrParametricCurveLocalFrame3D this[int index] 
            => index == _leafNodeList.Count
                ? _leafNodeList[^1].Frame1
                : _leafNodeList[index].Frame0;

        public IGraphicsC1ParametricCurve3D Curve { get; }

        public GrParametricCurveTreeBranch3D RootNode { get; private set; }
        
        public int TreeLevelCount { get; internal set; }
        
        /// <summary>
        /// The number of segments per grid side for this tree
        /// </summary>
        public int GridSegmentCount 
            => 1 << TreeLevelCount;

        public IEnumerable<GrParametricCurveTreeNode3D> Nodes
        {
            get
            {
                var stack = new Stack<GrParametricCurveTreeNode3D>();

                stack.Push(RootNode);

                while (stack.Count > 0)
                {
                    var node = stack.Pop();

                    yield return node;

                    if (node is not GrParametricCurveTreeBranch3D branchNode)
                        continue;

                    stack.Push(branchNode.Child1);
                    stack.Push(branchNode.Child0);
                }
            }
        }

        public IEnumerable<GrParametricCurveTreeBranch3D> BranchNodes
        {
            get
            {
                var stack = new Stack<GrParametricCurveTreeBranch3D>();

                stack.Push(RootNode);

                while (stack.Count > 0)
                {
                    var branchNode = stack.Pop();

                    yield return branchNode;

                    if (branchNode.Child1 is GrParametricCurveTreeBranch3D childBranchNode1)
                        stack.Push(childBranchNode1);

                    if (branchNode.Child0 is GrParametricCurveTreeBranch3D childBranchNode0)
                        stack.Push(childBranchNode0);
                }
            }
        }

        public IEnumerable<GrParametricCurveTreeLeaf3D> LeafNodes
        {
            get
            {
                var stack = new Stack<GrParametricCurveTreeNode3D>();

                stack.Push(RootNode);

                while (stack.Count > 0)
                {
                    var node = stack.Pop();

                    if (node is not GrParametricCurveTreeBranch3D branchNode)
                    {
                        yield return (GrParametricCurveTreeLeaf3D) node;
                        continue;
                    }

                    stack.Push(branchNode.Child1);
                    stack.Push(branchNode.Child0);
                }
            }
        }

        public int LeafNodeCount 
            => _leafNodeList.Count;
        
        public IReadOnlyList<GrParametricCurveTreeLeaf3D> LeafNodesList 
            => _leafNodeList;

        public double Length 
            => RootNode.Length1;

        public IBoundingBox1D ParameterValueRange { get; }
        
        public double GetLength()
        {
            var arcLength = 0d;
            
            GrParametricCurveLocalFrame3D frame1 = null;
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
        
        public GrCurveFrameInterpolationMethod FrameInterpolationMethod { get; set; }
            = GrCurveFrameInterpolationMethod.TangentLinearInterpolation;
        
        public GrCurveFrameSamplingMethod FrameSamplingMethod { get; set; }
            = GrCurveFrameSamplingMethod.SimpleRotation;
        
        public int CornerCount 
            => _cornerList.Count;

        internal IReadOnlyList<GrParametricCurveTreeCorner3D> CornerList 
            => _cornerList;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricCurveTree3D([NotNull] IGraphicsC1ParametricCurve3D surface, [NotNull] IBoundingBox1D parameterValueRange)
        {
            Curve = surface;
            ParameterValueRange = parameterValueRange;
            RootNode = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricCurveTree3D([NotNull] IGraphicsC1ParametricCurve3D surface, double minParameterValue, double maxParameterValue)
        {
            Debug.Assert(
                minParameterValue < maxParameterValue
            );

            Curve = surface;
            ParameterValueRange = BoundingBox1D.Create(minParameterValue, maxParameterValue);
            RootNode = null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricCurveTree3D([NotNull] IGraphicsC1ParametricCurve3D surface)
        {
            Curve = surface;
            ParameterValueRange = BoundingBox1D.Create(0, 1);
            RootNode = null;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricCurveTree3D Clear()
        {
            RootNode = null;
            _cornerList.Clear();
            _leafNodeList.Clear();
            _cornerDictionary.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetParameterValue(GrParametricTreeCornerPosition3D cornerPosition)
        {
            return cornerPosition
                .GetInterpolationValue()
                .Lerp(ParameterValueMin, ParameterValueMax);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricCurveTree3D GenerateTree(GrParametricCurveTreeOptions3D options)
        {
            Clear();

            RootNode = new GrParametricCurveTreeBranch3D(this);

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
        public Float64Tuple3D GetTangent(double parameterValue)
        {
            return GetSample(parameterValue).GetTangent();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetUnitTangent(double parameterValue)
        {
            return GetSample(parameterValue).GetTangent().ToUnitVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            return GetSample(parameterValue).GetFrame();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsCorner(GrParametricTreeCornerPosition3D cornerPosition)
        {
            return _cornerDictionary.ContainsKey(cornerPosition);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricCurveTreeCorner3D GetCorner(int cornerIndex)
        {
            return _cornerList[cornerIndex];
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricCurveTreeCorner3D GetFrame(GrParametricTreeCornerPosition3D cornerPosition)
        {
            var cornerIndex = _cornerDictionary[cornerPosition];

            return _cornerList[cornerIndex];
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetCornerIndex(GrParametricTreeCornerPosition3D cornerPosition)
        {
            return _cornerDictionary[cornerPosition];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCorner(GrParametricTreeCornerPosition3D cornerPosition, out GrParametricCurveTreeCorner3D corner)
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
        public bool TryGetCornerIndex(GrParametricTreeCornerPosition3D cornerPosition, out int cornerIndex)
        {
            return _cornerDictionary.TryGetValue(cornerPosition, out cornerIndex);
        }
        
        internal GrParametricCurveTreeCorner3D GetOrAddCorner(GrParametricTreeCornerPosition3D cornerPosition)
        {
            if (_cornerDictionary.TryGetValue(cornerPosition, out var index))
                return _cornerList[index];

            var parameterValue = 
                GetParameterValue(cornerPosition);

            index = _cornerList.Count;
            var frame = Curve.GetFrame(parameterValue);
            var corner = new GrParametricCurveTreeCorner3D(this, index, frame, cornerPosition);

            _cornerList.Add(corner);
            _cornerDictionary.Add(cornerPosition, index);

            return corner;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GrParametricCurveTreeLeaf3D AddLeafNode([NotNull] GrParametricCurveTreeLeaf3D leafNode)
        {
            _leafNodeList.Add(leafNode);

            return leafNode;
        }

        public GrParametricCurveTreeSample3D GetSample(double parameterValue)
        {
            if (!RootNode.Contains(parameterValue))
                throw new ArgumentOutOfRangeException();

            var branchNode = RootNode;

            while (true)
            {
                var childNode = 
                    branchNode.GetChildContaining(parameterValue);

                if (childNode is GrParametricCurveTreeLeaf3D leafNode)
                    return new GrParametricCurveTreeSample3D(
                        leafNode, 
                        parameterValue
                    );

                branchNode = (GrParametricCurveTreeBranch3D) childNode;
            }
        }
        
        public GrParametricCurveTreeSample3D GetSampleByLength(double length)
        {
            if (!RootNode.ContainsLength(length))
                throw new ArgumentOutOfRangeException();

            var branchNode = RootNode;

            while (true)
            {
                var childNode = 
                    branchNode.GetChildContainingLength(length);

                if (childNode is GrParametricCurveTreeLeaf3D leafNode)
                {
                    var t = 
                        (length - childNode.Length0) / childNode.Length;

                    var parameterValue = 
                        t.Lerp(childNode.MinParameterValue, childNode.MaxParameterValue);

                    return new GrParametricCurveTreeSample3D(leafNode, parameterValue);
                }

                branchNode = (GrParametricCurveTreeBranch3D) childNode;
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
        public IEnumerable<GrParametricCurveLocalFrame3D> GetFrames(double parameterValue)
        {
            return GetFrames(ParameterValueMin, parameterValue);
        }

        public IEnumerable<GrParametricCurveLocalFrame3D> GetFrames(double parameterValue1, double parameterValue2)
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

                if (childNode is GrParametricCurveTreeLeaf3D)
                {
                    var t = 
                        (parameterValue - childNode.MinParameterValue) / (childNode.MaxParameterValue - childNode.MinParameterValue);

                    var length = 
                        t.Lerp(childNode.Length0, childNode.Length1);

                    return length;
                }

                branchNode = (GrParametricCurveTreeBranch3D) childNode;
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

                if (childNode is GrParametricCurveTreeLeaf3D)
                {
                    var t = 
                        (length - childNode.Length0) / childNode.Length;

                    var parameterValue = 
                        t.Lerp(childNode.MinParameterValue, childNode.MaxParameterValue);

                    return parameterValue;
                }

                branchNode = (GrParametricCurveTreeBranch3D) childNode;
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricCurveTree3D GetSubCurve(double parameterValue1, double parameterValue2, GrParametricCurveTreeOptions3D options)
        {
            if (parameterValue1 > parameterValue2)
                (parameterValue1, parameterValue2) = (parameterValue2, parameterValue1);

            var curve = new GrParametricCurveTree3D(
                this, 
                parameterValue1, 
                parameterValue2
            );

            return curve.GenerateTree(options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricCurveTree3D GetSubCurveByLength(double length1, double length2, GrParametricCurveTreeOptions3D options)
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
        public GrLineStripGeometry3D GenerateGeometry()
        {
            return GrLineStripGeometry3D.Create(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<GrParametricCurveLocalFrame3D> GetEnumerator()
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