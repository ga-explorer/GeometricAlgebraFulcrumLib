using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Adaptive;

public sealed class Float64AdaptivePath2D :
    Float64ArcLengthPath2D,
    IReadOnlyList<Float64Path2DLocalFrame>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath2D Finite(Float64Path2D basePath)
    {
        return new Float64AdaptivePath2D(
            basePath.TimeRange, 
            false, 
            basePath
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath2D Finite(Float64ScalarRange timeRange, Float64Path2D basePath)
    {
        return new Float64AdaptivePath2D(timeRange, false, basePath);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath2D Periodic(Float64Path2D basePath)
    {
        return new Float64AdaptivePath2D(
            basePath.TimeRange, 
            true, 
            basePath
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath2D Periodic(Float64ScalarRange timeRange, Float64Path2D basePath)
    {
        return new Float64AdaptivePath2D(timeRange, true, basePath);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath2D Create(Float64Path2D basePath)
    {
        return new Float64AdaptivePath2D(
            basePath.TimeRange, 
            basePath.IsPeriodic, 
            basePath
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath2D Create(Float64ScalarRange timeRange, Float64Path2D basePath)
    {
        return new Float64AdaptivePath2D(timeRange, basePath.IsPeriodic, basePath);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath2D Create(bool isPeriodic, Float64Path2D basePath)
    {
        return new Float64AdaptivePath2D(basePath.TimeRange, isPeriodic, basePath);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath2D Create(Float64ScalarRange timeRange, bool isPeriodic, Float64Path2D basePath)
    {
        return new Float64AdaptivePath2D(timeRange, isPeriodic, basePath);
    }

    
    private readonly Dictionary<Float64AdaptivePath2DCornerPosition, int> _cornerDictionary
        = new Dictionary<Float64AdaptivePath2DCornerPosition, int>();

    private readonly List<Float64AdaptivePath2DCorner> _cornerList
        = new List<Float64AdaptivePath2DCorner>();

    private readonly List<Float64AdaptivePath2DLeaf> _leafNodeList
        = new List<Float64AdaptivePath2DLeaf>();


    public int Count
        => 1 + _leafNodeList.Count;

    public Float64Path2DLocalFrame this[int index]
        => index == _leafNodeList.Count
            ? _leafNodeList[^1].Frame1
            : _leafNodeList[index].Frame0;

    public Float64Path2D Curve { get; }

    public Float64AdaptivePath2DBranch? RootNode { get; private set; }

    public int TreeLevelCount { get; internal set; }

    /// <summary>
    /// The number of segments per grid side for this tree
    /// </summary>
    public int GridSegmentCount
        => 1 << TreeLevelCount;

    public IEnumerable<Float64AdaptivePath2DNode> Nodes
    {
        get
        {
            if (RootNode is null)
                yield break;

            var stack = new Stack<Float64AdaptivePath2DNode>();

            stack.Push(RootNode);

            while (stack.Count > 0)
            {
                var node = stack.Pop();

                yield return node;

                if (node is not Float64AdaptivePath2DBranch branchNode)
                    continue;

                stack.Push(branchNode.Child1);
                stack.Push(branchNode.Child0);
            }
        }
    }

    public IEnumerable<Float64AdaptivePath2DBranch> BranchNodes
    {
        get
        {
            if (RootNode is null)
                yield break;

            var stack = new Stack<Float64AdaptivePath2DBranch>();

            stack.Push(RootNode);

            while (stack.Count > 0)
            {
                var branchNode = stack.Pop();

                yield return branchNode;

                if (branchNode.Child1 is Float64AdaptivePath2DBranch childBranchNode1)
                    stack.Push(childBranchNode1);

                if (branchNode.Child0 is Float64AdaptivePath2DBranch childBranchNode0)
                    stack.Push(childBranchNode0);
            }
        }
    }

    public IEnumerable<Float64AdaptivePath2DLeaf> LeafNodes
    {
        get
        {
            if (RootNode is null)
                yield break;

            var stack = new Stack<Float64AdaptivePath2DNode>();

            stack.Push(RootNode);

            while (stack.Count > 0)
            {
                var node = stack.Pop();

                if (node is not Float64AdaptivePath2DBranch branchNode)
                {
                    yield return (Float64AdaptivePath2DLeaf)node;
                    continue;
                }

                stack.Push(branchNode.Child1);
                stack.Push(branchNode.Child0);
            }
        }
    }

    public int LeafNodeCount
        => _leafNodeList.Count;

    public IReadOnlyList<Float64AdaptivePath2DLeaf> LeafNodesList
        => _leafNodeList;

    public double Length
        => RootNode?.Length1 ?? 0;

    public override Float64Scalar GetLength()
    {
        var arcLength = 0d;

        Float64Path2DLocalFrame? frame1 = null;
        var firstFrame = true;
        foreach (var frame2 in this)
        {
            if (firstFrame)
            {
                frame1 = frame2;
                firstFrame = false;
                continue;
            }

            arcLength += frame2.Point.GetDistanceToPoint(frame1?.Point ?? frame2.Point);

            frame1 = frame2;
        }

        return arcLength;
    }

    public double ParameterValueMin
        => TimeRange.MinValue;

    public double ParameterValueMax
        => TimeRange.MaxValue;

    public ParametricCurveLocalFrameInterpolationMethod FrameInterpolationMethod { get; set; }
        = ParametricCurveLocalFrameInterpolationMethod.TangentLinearInterpolation;

    public ParametricCurveLocalFrameSamplingMethod FrameSamplingMethod { get; set; }
        = ParametricCurveLocalFrameSamplingMethod.SimpleRotation;

    public int CornerCount
        => _cornerList.Count;

    internal IReadOnlyList<Float64AdaptivePath2DCorner> CornerList
        => _cornerList;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64AdaptivePath2D(Float64ScalarRange timeRange, bool isPeriodic, Float64Path2D basePath)
        : base(timeRange, isPeriodic)
    {
        Curve = basePath;
        RootNode = null;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AdaptivePath2D Clear()
    {
        RootNode = null;
        _cornerList.Clear();
        _leafNodeList.Clear();
        _cornerDictionary.Clear();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetParameterValue(Float64AdaptivePath2DCornerPosition cornerPosition)
    {
        return cornerPosition
            .GetInterpolationValue()
            .Lerp(ParameterValueMin, ParameterValueMax);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AdaptivePath2D GenerateTree(Float64AdaptivePath2DSamplingOptions options)
    {
        Clear();

        RootNode = new Float64AdaptivePath2DBranch(this);

        RootNode.GenerateTree(options);

        RootNode.UpdateLengthData(0d);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return RootNode is not null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetValue(double t)
    {
        return GetSample(t).GetPoint();
    }

    public override Float64ArcLengthPath2D ToFiniteArcLengthPath()
    {
        throw new NotImplementedException();
    }

    public override Float64ArcLengthPath2D ToPeriodicArcLengthPath()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative1Value(double t)
    {
        return GetSample(t).GetTangent();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative2Value(double t)
    {
        throw new NotImplementedException();
        //return GetSample(t).GetTangent();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path2DLocalFrame GetFrame(double t)
    {
        return GetSample(t).GetFrame();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsCorner(Float64AdaptivePath2DCornerPosition cornerPosition)
    {
        return _cornerDictionary.ContainsKey(cornerPosition);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AdaptivePath2DCorner GetCorner(int cornerIndex)
    {
        return _cornerList[cornerIndex];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AdaptivePath2DCorner GetFrame(Float64AdaptivePath2DCornerPosition cornerPosition)
    {
        var cornerIndex = _cornerDictionary[cornerPosition];

        return _cornerList[cornerIndex];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetCornerIndex(Float64AdaptivePath2DCornerPosition cornerPosition)
    {
        return _cornerDictionary[cornerPosition];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetCorner(Float64AdaptivePath2DCornerPosition cornerPosition, out Float64AdaptivePath2DCorner corner)
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
    public bool TryGetCornerIndex(Float64AdaptivePath2DCornerPosition cornerPosition, out int cornerIndex)
    {
        return _cornerDictionary.TryGetValue(cornerPosition, out cornerIndex);
    }

    internal Float64AdaptivePath2DCorner GetOrAddCorner(Float64AdaptivePath2DCornerPosition cornerPosition)
    {
        if (_cornerDictionary.TryGetValue(cornerPosition, out var index))
            return _cornerList[index];

        var t =
            GetParameterValue(cornerPosition);

        index = _cornerList.Count;
        var frame = Curve.GetFrame(t);
        var corner = new Float64AdaptivePath2DCorner(this, index, frame, cornerPosition);

        _cornerList.Add(corner);
        _cornerDictionary.Add(cornerPosition, index);

        return corner;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Float64AdaptivePath2DLeaf AddLeafNode(Float64AdaptivePath2DLeaf leafNode)
    {
        _leafNodeList.Add(leafNode);

        return leafNode;
    }

    public Float64AdaptivePath2DSample GetSample(double t)
    {
        if (RootNode is null || !RootNode.Contains(t))
            throw new ArgumentOutOfRangeException();

        var branchNode = RootNode;

        while (true)
        {
            var childNode =
                branchNode.GetChildContaining(t);

            if (childNode is Float64AdaptivePath2DLeaf leafNode)
                return new Float64AdaptivePath2DSample(
                    leafNode,
                    t
                );

            branchNode = (Float64AdaptivePath2DBranch)childNode;
        }
    }

    public Float64AdaptivePath2DSample GetSampleByLength(double length)
    {
        if (RootNode is null || !RootNode.ContainsLength(length))
            throw new ArgumentOutOfRangeException();

        var branchNode = RootNode;

        while (true)
        {
            var childNode =
                branchNode.GetChildContainingLength(length);

            if (childNode is Float64AdaptivePath2DLeaf leafNode)
            {
                var l =
                    (length - childNode.Length0) / childNode.Length;

                var t =
                    l.Lerp(childNode.MinParameterValue, childNode.MaxParameterValue);

                return new Float64AdaptivePath2DSample(leafNode, t);
            }

            branchNode = (Float64AdaptivePath2DBranch)childNode;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinFloat64Vector2D> GetPoints(double t)
    {
        return GetPoints(ParameterValueMin, t);
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
    public IEnumerable<Float64Path2DLocalFrame> GetFrames(double t)
    {
        return GetFrames(ParameterValueMin, t);
    }

    public IEnumerable<Float64Path2DLocalFrame> GetFrames(double parameterValue1, double parameterValue2)
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

    public override Float64Scalar TimeToLength(double t)
    {
        t = t.ClampPeriodic(ParameterValueMin, ParameterValueMax);

        if (RootNode is null || !RootNode.Contains(t))
            throw new ArgumentOutOfRangeException();

        var branchNode = RootNode;

        while (true)
        {
            var childNode =
                branchNode.GetChildContaining(t);

            if (childNode is Float64AdaptivePath2DLeaf)
            {
                var t1 =
                    (t - childNode.MinParameterValue) / (childNode.MaxParameterValue - childNode.MinParameterValue);

                var length =
                    t1.Lerp(childNode.Length0, childNode.Length1);

                return length;
            }

            branchNode = (Float64AdaptivePath2DBranch)childNode;
        }
    }

    public override Float64Scalar LengthToTime(double length)
    {
        if (RootNode is null || !RootNode.ContainsLength(length))
            throw new ArgumentOutOfRangeException();

        var branchNode = RootNode;

        while (true)
        {
            var childNode =
                branchNode.GetChildContainingLength(length);

            if (childNode is Float64AdaptivePath2DLeaf)
            {
                var l =
                    (length - childNode.Length0) / childNode.Length;

                var t =
                    l.Lerp(childNode.MinParameterValue, childNode.MaxParameterValue);

                return t;
            }

            branchNode = (Float64AdaptivePath2DBranch)childNode;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AdaptivePath2D GetFiniteSubCurve(double parameterValue1, double parameterValue2, Float64AdaptivePath2DSamplingOptions options)
    {
        if (parameterValue1 > parameterValue2)
            (parameterValue1, parameterValue2) = (parameterValue2, parameterValue1);

        var curve = new Float64AdaptivePath2D(
            Float64ScalarRange.Create(parameterValue1, parameterValue2),
            false,
            this
        );

        return curve.GenerateTree(options);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AdaptivePath2D GetSubCurveByLength(double length1, double length2, Float64AdaptivePath2DSamplingOptions options)
    {
        if (length1 > length2)
            (length1, length2) = (length2, length1);

        return GetFiniteSubCurve(
            LengthToTime(length1),
            LengthToTime(length2),
            options
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<Float64Path2DLocalFrame> GetEnumerator()
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