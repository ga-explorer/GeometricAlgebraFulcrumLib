using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space2D.Curves.Adaptive;

public sealed class AdaptiveCurve2D :
    IArcLengthCurve2D,
    IReadOnlyList<ParametricCurveLocalFrame2D>
{
    private readonly Dictionary<AdaptiveCurveTreeCornerPosition2D, int> _cornerDictionary
        = new Dictionary<AdaptiveCurveTreeCornerPosition2D, int>();

    private readonly List<AdaptiveCurveTreeCorner2D> _cornerList
        = new List<AdaptiveCurveTreeCorner2D>();

    private readonly List<AdaptiveCurveTreeLeaf2D> _leafNodeList
        = new List<AdaptiveCurveTreeLeaf2D>();


    public int Count
        => 1 + _leafNodeList.Count;

    public ParametricCurveLocalFrame2D this[int index]
        => index == _leafNodeList.Count
            ? _leafNodeList[^1].Frame1
            : _leafNodeList[index].Frame0;

    public IFloat64ParametricCurve2D Curve { get; }

    public AdaptiveCurveTreeBranch2D RootNode { get; private set; }

    public int TreeLevelCount { get; internal set; }

    /// <summary>
    /// The number of segments per grid side for this tree
    /// </summary>
    public int GridSegmentCount
        => 1 << TreeLevelCount;

    public IEnumerable<AdaptiveCurveTreeNode2D> Nodes
    {
        get
        {
            var stack = new Stack<AdaptiveCurveTreeNode2D>();

            stack.Push(RootNode);

            while (stack.Count > 0)
            {
                var node = stack.Pop();

                yield return node;

                if (node is not AdaptiveCurveTreeBranch2D branchNode)
                    continue;

                stack.Push(branchNode.Child1);
                stack.Push(branchNode.Child0);
            }
        }
    }

    public IEnumerable<AdaptiveCurveTreeBranch2D> BranchNodes
    {
        get
        {
            var stack = new Stack<AdaptiveCurveTreeBranch2D>();

            stack.Push(RootNode);

            while (stack.Count > 0)
            {
                var branchNode = stack.Pop();

                yield return branchNode;

                if (branchNode.Child1 is AdaptiveCurveTreeBranch2D childBranchNode1)
                    stack.Push(childBranchNode1);

                if (branchNode.Child0 is AdaptiveCurveTreeBranch2D childBranchNode0)
                    stack.Push(childBranchNode0);
            }
        }
    }

    public IEnumerable<AdaptiveCurveTreeLeaf2D> LeafNodes
    {
        get
        {
            var stack = new Stack<AdaptiveCurveTreeNode2D>();

            stack.Push(RootNode);

            while (stack.Count > 0)
            {
                var node = stack.Pop();

                if (node is not AdaptiveCurveTreeBranch2D branchNode)
                {
                    yield return (AdaptiveCurveTreeLeaf2D)node;
                    continue;
                }

                stack.Push(branchNode.Child1);
                stack.Push(branchNode.Child0);
            }
        }
    }

    public int LeafNodeCount
        => _leafNodeList.Count;

    public IReadOnlyList<AdaptiveCurveTreeLeaf2D> LeafNodesList
        => _leafNodeList;

    public double Length
        => RootNode.Length1;

    public Float64ScalarRange ParameterRange { get; }

    public Float64Scalar GetLength()
    {
        var arcLength = 0d;

        ParametricCurveLocalFrame2D frame1 = null;
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
        => ParameterRange.MinValue;

    public double ParameterValueMax
        => ParameterRange.MaxValue;

    public ParametricCurveLocalFrameInterpolationMethod FrameInterpolationMethod { get; set; }
        = ParametricCurveLocalFrameInterpolationMethod.TangentLinearInterpolation;

    public ParametricCurveLocalFrameSamplingMethod FrameSamplingMethod { get; set; }
        = ParametricCurveLocalFrameSamplingMethod.SimpleRotation;

    public int CornerCount
        => _cornerList.Count;

    internal IReadOnlyList<AdaptiveCurveTreeCorner2D> CornerList
        => _cornerList;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AdaptiveCurve2D(IFloat64ParametricCurve2D surface, Float64ScalarRange parameterValueRange)
    {
        Curve = surface;
        ParameterRange = parameterValueRange;
        RootNode = null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AdaptiveCurve2D(IFloat64ParametricCurve2D surface, double minParameterValue, double maxParameterValue)
    {
        Debug.Assert(
            minParameterValue < maxParameterValue
        );

        Curve = surface;
        ParameterRange = Float64ScalarRange.Create(minParameterValue, maxParameterValue);
        RootNode = null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AdaptiveCurve2D(IFloat64ParametricCurve2D surface)
    {
        Curve = surface;
        ParameterRange = Float64ScalarRange.Create(0, 1);
        RootNode = null;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AdaptiveCurve2D Clear()
    {
        RootNode = null;
        _cornerList.Clear();
        _leafNodeList.Clear();
        _cornerDictionary.Clear();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetParameterValue(AdaptiveCurveTreeCornerPosition2D cornerPosition)
    {
        return cornerPosition
            .GetInterpolationValue()
            .Lerp(ParameterValueMin, ParameterValueMax);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AdaptiveCurve2D GenerateTree(AdaptiveCurveSamplingOptions2D options)
    {
        Clear();

        RootNode = new AdaptiveCurveTreeBranch2D(this);

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
    public LinFloat64Vector2D GetPoint(double parameterValue)
    {
        return GetSample(parameterValue).GetPoint();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetDerivative1Point(double parameterValue)
    {
        return GetSample(parameterValue).GetTangent();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame2D GetFrame(double parameterValue)
    {
        return GetSample(parameterValue).GetFrame();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsCorner(AdaptiveCurveTreeCornerPosition2D cornerPosition)
    {
        return _cornerDictionary.ContainsKey(cornerPosition);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AdaptiveCurveTreeCorner2D GetCorner(int cornerIndex)
    {
        return _cornerList[cornerIndex];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AdaptiveCurveTreeCorner2D GetFrame(AdaptiveCurveTreeCornerPosition2D cornerPosition)
    {
        var cornerIndex = _cornerDictionary[cornerPosition];

        return _cornerList[cornerIndex];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetCornerIndex(AdaptiveCurveTreeCornerPosition2D cornerPosition)
    {
        return _cornerDictionary[cornerPosition];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetCorner(AdaptiveCurveTreeCornerPosition2D cornerPosition, out AdaptiveCurveTreeCorner2D corner)
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
    public bool TryGetCornerIndex(AdaptiveCurveTreeCornerPosition2D cornerPosition, out int cornerIndex)
    {
        return _cornerDictionary.TryGetValue(cornerPosition, out cornerIndex);
    }

    internal AdaptiveCurveTreeCorner2D GetOrAddCorner(AdaptiveCurveTreeCornerPosition2D cornerPosition)
    {
        if (_cornerDictionary.TryGetValue(cornerPosition, out var index))
            return _cornerList[index];

        var parameterValue =
            GetParameterValue(cornerPosition);

        index = _cornerList.Count;
        var frame = Curve.GetFrame(parameterValue);
        var corner = new AdaptiveCurveTreeCorner2D(this, index, frame, cornerPosition);

        _cornerList.Add(corner);
        _cornerDictionary.Add(cornerPosition, index);

        return corner;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal AdaptiveCurveTreeLeaf2D AddLeafNode(AdaptiveCurveTreeLeaf2D leafNode)
    {
        _leafNodeList.Add(leafNode);

        return leafNode;
    }

    public AdaptiveCurveTreeSample2D GetSample(double parameterValue)
    {
        if (!RootNode.Contains(parameterValue))
            throw new ArgumentOutOfRangeException();

        var branchNode = RootNode;

        while (true)
        {
            var childNode =
                branchNode.GetChildContaining(parameterValue);

            if (childNode is AdaptiveCurveTreeLeaf2D leafNode)
                return new AdaptiveCurveTreeSample2D(
                    leafNode,
                    parameterValue
                );

            branchNode = (AdaptiveCurveTreeBranch2D)childNode;
        }
    }

    public AdaptiveCurveTreeSample2D GetSampleByLength(double length)
    {
        if (!RootNode.ContainsLength(length))
            throw new ArgumentOutOfRangeException();

        var branchNode = RootNode;

        while (true)
        {
            var childNode =
                branchNode.GetChildContainingLength(length);

            if (childNode is AdaptiveCurveTreeLeaf2D leafNode)
            {
                var t =
                    (length - childNode.Length0) / childNode.Length;

                var parameterValue =
                    t.Lerp(childNode.MinParameterValue, childNode.MaxParameterValue);

                return new AdaptiveCurveTreeSample2D(leafNode, parameterValue);
            }

            branchNode = (AdaptiveCurveTreeBranch2D)childNode;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinFloat64Vector2D> GetPoints(double parameterValue)
    {
        return GetPoints(ParameterValueMin, parameterValue);
    }

    public IEnumerable<LinFloat64Vector2D> GetPoints(double parameterValue1, double parameterValue2)
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
    public IEnumerable<ParametricCurveLocalFrame2D> GetFrames(double parameterValue)
    {
        return GetFrames(ParameterValueMin, parameterValue);
    }

    public IEnumerable<ParametricCurveLocalFrame2D> GetFrames(double parameterValue1, double parameterValue2)
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

    public Float64Scalar ParameterToLength(double parameterValue)
    {
        parameterValue = parameterValue.ClampPeriodic(ParameterValueMin, ParameterValueMax);

        if (!RootNode.Contains(parameterValue))
            throw new ArgumentOutOfRangeException();

        var branchNode = RootNode;

        while (true)
        {
            var childNode =
                branchNode.GetChildContaining(parameterValue);

            if (childNode is AdaptiveCurveTreeLeaf2D)
            {
                var t =
                    (parameterValue - childNode.MinParameterValue) / (childNode.MaxParameterValue - childNode.MinParameterValue);

                var length =
                    t.Lerp(childNode.Length0, childNode.Length1);

                return length;
            }

            branchNode = (AdaptiveCurveTreeBranch2D)childNode;
        }
    }

    public Float64Scalar LengthToParameter(double length)
    {
        if (!RootNode.ContainsLength(length))
            throw new ArgumentOutOfRangeException();

        var branchNode = RootNode;

        while (true)
        {
            var childNode =
                branchNode.GetChildContainingLength(length);

            if (childNode is AdaptiveCurveTreeLeaf2D)
            {
                var t =
                    (length - childNode.Length0) / childNode.Length;

                var parameterValue =
                    t.Lerp(childNode.MinParameterValue, childNode.MaxParameterValue);

                return parameterValue;
            }

            branchNode = (AdaptiveCurveTreeBranch2D)childNode;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AdaptiveCurve2D GetSubCurve(double parameterValue1, double parameterValue2, AdaptiveCurveSamplingOptions2D options)
    {
        if (parameterValue1 > parameterValue2)
            (parameterValue1, parameterValue2) = (parameterValue2, parameterValue1);

        var curve = new AdaptiveCurve2D(
            this,
            parameterValue1,
            parameterValue2
        );

        return curve.GenerateTree(options);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AdaptiveCurve2D GetSubCurveByLength(double length1, double length2, AdaptiveCurveSamplingOptions2D options)
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
    public IEnumerator<ParametricCurveLocalFrame2D> GetEnumerator()
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