using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Adaptive;

public sealed class Float64AdaptivePath3D :
    Float64ArcLengthPath3D,
    IReadOnlyList<Float64Path3DLocalFrame>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath3D Finite(Float64Path3D basePath)
    {
        return new Float64AdaptivePath3D(
            basePath.TimeRange, 
            false, 
            basePath
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath3D Finite(Float64ScalarRange timeRange, Float64Path3D basePath)
    {
        return new Float64AdaptivePath3D(timeRange, false, basePath);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath3D Periodic(Float64Path3D basePath)
    {
        return new Float64AdaptivePath3D(
            basePath.TimeRange, 
            true, 
            basePath
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath3D Periodic(Float64ScalarRange timeRange, Float64Path3D basePath)
    {
        return new Float64AdaptivePath3D(timeRange, true, basePath);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath3D Create(Float64Path3D basePath)
    {
        return new Float64AdaptivePath3D(
            basePath.TimeRange, 
            basePath.IsPeriodic, 
            basePath
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath3D Create(Float64ScalarRange timeRange, Float64Path3D basePath)
    {
        return new Float64AdaptivePath3D(timeRange, basePath.IsPeriodic, basePath);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath3D Create(bool isPeriodic, Float64Path3D basePath)
    {
        return new Float64AdaptivePath3D(basePath.TimeRange, isPeriodic, basePath);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath3D Create(Float64ScalarRange timeRange, bool isPeriodic, Float64Path3D basePath)
    {
        return new Float64AdaptivePath3D(timeRange, isPeriodic, basePath);
    }

    
    private readonly Dictionary<Float64AdaptivePath3DCornerPosition, int> _cornerDictionary
        = new Dictionary<Float64AdaptivePath3DCornerPosition, int>();

    private readonly List<Float64AdaptivePath3DCorner> _cornerList
        = new List<Float64AdaptivePath3DCorner>();

    private readonly List<Float64AdaptivePath3DLeaf> _leafNodeList
        = new List<Float64AdaptivePath3DLeaf>();


    public int Count
        => 1 + _leafNodeList.Count;

    public Float64Path3DLocalFrame this[int index]
        => index == _leafNodeList.Count
            ? _leafNodeList[^1].Frame1
            : _leafNodeList[index].Frame0;

    public Float64Path3D Curve { get; }

    public Float64AdaptivePath3DBranch? RootNode { get; private set; }

    public int TreeLevelCount { get; internal set; }

    /// <summary>
    /// The number of segments per grid side for this tree
    /// </summary>
    public int GridSegmentCount
        => 1 << TreeLevelCount;

    public IEnumerable<Float64AdaptivePath3DNode> Nodes
    {
        get
        {
            var stack = new Stack<Float64AdaptivePath3DNode>();

            stack.Push(RootNode);

            while (stack.Count > 0)
            {
                var node = stack.Pop();

                yield return node;

                if (node is not Float64AdaptivePath3DBranch branchNode)
                    continue;

                stack.Push(branchNode.Child1);
                stack.Push(branchNode.Child0);
            }
        }
    }

    public IEnumerable<Float64AdaptivePath3DBranch> BranchNodes
    {
        get
        {
            var stack = new Stack<Float64AdaptivePath3DBranch>();

            stack.Push(RootNode);

            while (stack.Count > 0)
            {
                var branchNode = stack.Pop();

                yield return branchNode;

                if (branchNode.Child1 is Float64AdaptivePath3DBranch childBranchNode1)
                    stack.Push(childBranchNode1);

                if (branchNode.Child0 is Float64AdaptivePath3DBranch childBranchNode0)
                    stack.Push(childBranchNode0);
            }
        }
    }

    public IEnumerable<Float64AdaptivePath3DLeaf> LeafNodes
    {
        get
        {
            var stack = new Stack<Float64AdaptivePath3DNode>();

            stack.Push(RootNode);

            while (stack.Count > 0)
            {
                var node = stack.Pop();

                if (node is not Float64AdaptivePath3DBranch branchNode)
                {
                    yield return (Float64AdaptivePath3DLeaf)node;
                    continue;
                }

                stack.Push(branchNode.Child1);
                stack.Push(branchNode.Child0);
            }
        }
    }

    public int LeafNodeCount
        => _leafNodeList.Count;

    public IReadOnlyList<Float64AdaptivePath3DLeaf> LeafNodesList
        => _leafNodeList;

    public double Length
        => RootNode.Length1;

    public override Float64Scalar GetLength()
    {
        var arcLength = 0d;

        Float64Path3DLocalFrame frame1 = null;
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

    public ParametricCurveLocalFrameInterpolationMethod FrameInterpolationMethod { get; set; }
        = ParametricCurveLocalFrameInterpolationMethod.TangentLinearInterpolation;

    public ParametricCurveLocalFrameSamplingMethod FrameSamplingMethod { get; set; }
        = ParametricCurveLocalFrameSamplingMethod.SimpleRotation;

    public int CornerCount
        => _cornerList.Count;

    internal IReadOnlyList<Float64AdaptivePath3DCorner> CornerList
        => _cornerList;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64AdaptivePath3D(Float64ScalarRange timeRange, bool isPeriodic, Float64Path3D surface)
        : base(timeRange, isPeriodic)
    {
        Curve = surface;
        RootNode = null;
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64AdaptivePath3D(Float64PointPath3D surface, double minParameterValue, double maxParameterValue)
    //{
    //    Debug.Assert(
    //        minParameterValue < maxParameterValue
    //    );

    //    Curve = surface;
    //    TimeRange = Float64ScalarRange.Create(minParameterValue, maxParameterValue);
    //    RootNode = null;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64AdaptivePath3D(Float64PointPath3D surface)
    //{
    //    Curve = surface;
    //    TimeRange = Float64ScalarRange.Create(0, 1);
    //    RootNode = null;
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AdaptivePath3D Clear()
    {
        RootNode = null;
        _cornerList.Clear();
        _leafNodeList.Clear();
        _cornerDictionary.Clear();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetParameterValue(Float64AdaptivePath3DCornerPosition cornerPosition)
    {
        return cornerPosition
            .GetInterpolationValue()
            .Lerp(TimeRange);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AdaptivePath3D GenerateTree(Float64AdaptivePath3DSamplingOptions options)
    {
        Clear();

        RootNode = new Float64AdaptivePath3DBranch(this);

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
    public override LinFloat64Vector3D GetValue(double parameterValue)
    {
        return GetSample(parameterValue).GetPoint();
    }

    public override Float64ArcLengthPath3D ToFiniteArcLengthPath()
    {
        throw new NotImplementedException();
    }

    public override Float64ArcLengthPath3D ToPeriodicArcLengthPath()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative1Value(double parameterValue)
    {
        return GetSample(parameterValue).GetTangent();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative2Value(double parameterValue)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3DLocalFrame GetFrame(double parameterValue)
    {
        return GetSample(parameterValue).GetFrame();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsCorner(Float64AdaptivePath3DCornerPosition cornerPosition)
    {
        return _cornerDictionary.ContainsKey(cornerPosition);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AdaptivePath3DCorner GetCorner(int cornerIndex)
    {
        return _cornerList[cornerIndex];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AdaptivePath3DCorner GetFrame(Float64AdaptivePath3DCornerPosition cornerPosition)
    {
        var cornerIndex = _cornerDictionary[cornerPosition];

        return _cornerList[cornerIndex];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetCornerIndex(Float64AdaptivePath3DCornerPosition cornerPosition)
    {
        return _cornerDictionary[cornerPosition];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetCorner(Float64AdaptivePath3DCornerPosition cornerPosition, out Float64AdaptivePath3DCorner corner)
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
    public bool TryGetCornerIndex(Float64AdaptivePath3DCornerPosition cornerPosition, out int cornerIndex)
    {
        return _cornerDictionary.TryGetValue(cornerPosition, out cornerIndex);
    }

    internal Float64AdaptivePath3DCorner GetOrAddCorner(Float64AdaptivePath3DCornerPosition cornerPosition)
    {
        if (_cornerDictionary.TryGetValue(cornerPosition, out var index))
            return _cornerList[index];

        var parameterValue =
            GetParameterValue(cornerPosition);

        index = _cornerList.Count;
        var frame = Curve.GetFrame(parameterValue);
        var corner = new Float64AdaptivePath3DCorner(this, index, frame, cornerPosition);

        _cornerList.Add(corner);
        _cornerDictionary.Add(cornerPosition, index);

        return corner;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Float64AdaptivePath3DLeaf AddLeafNode(Float64AdaptivePath3DLeaf leafNode)
    {
        _leafNodeList.Add(leafNode);

        return leafNode;
    }

    public Float64AdaptivePath3DSample GetSample(double parameterValue)
    {
        if (!RootNode.Contains(parameterValue))
            throw new ArgumentOutOfRangeException();

        var branchNode = RootNode;

        while (true)
        {
            var childNode =
                branchNode.GetChildContaining(parameterValue);

            if (childNode is Float64AdaptivePath3DLeaf leafNode)
                return new Float64AdaptivePath3DSample(
                    leafNode,
                    parameterValue
                );

            branchNode = (Float64AdaptivePath3DBranch)childNode;
        }
    }

    public Float64AdaptivePath3DSample GetSampleByLength(double length)
    {
        if (!RootNode.ContainsLength(length))
            throw new ArgumentOutOfRangeException();

        var branchNode = RootNode;

        while (true)
        {
            var childNode =
                branchNode.GetChildContainingLength(length);

            if (childNode is Float64AdaptivePath3DLeaf leafNode)
            {
                var t =
                    (length - childNode.Length0) / childNode.Length;

                var parameterValue =
                    t.Lerp(childNode.MinParameterValue, childNode.MaxParameterValue);

                return new Float64AdaptivePath3DSample(leafNode, parameterValue);
            }

            branchNode = (Float64AdaptivePath3DBranch)childNode;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinFloat64Vector3D> GetPoints(double parameterValue)
    {
        return GetPoints(TimeRange.MinValue, parameterValue);
    }

    public IEnumerable<LinFloat64Vector3D> GetPoints(double parameterValue1, double parameterValue2)
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
    public IEnumerable<Float64Path3DLocalFrame> GetFrames(double parameterValue)
    {
        return GetFrames(TimeRange.MinValue, parameterValue);
    }

    public IEnumerable<Float64Path3DLocalFrame> GetFrames(double parameterValue1, double parameterValue2)
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

    public override Float64Scalar TimeToLength(double parameterValue)
    {
        parameterValue = parameterValue.ClampPeriodic(TimeRange.MinValue, TimeRange.MaxValue);

        if (!RootNode.Contains(parameterValue))
            throw new ArgumentOutOfRangeException();

        var branchNode = RootNode;

        while (true)
        {
            var childNode =
                branchNode.GetChildContaining(parameterValue);

            if (childNode is Float64AdaptivePath3DLeaf)
            {
                var t =
                    (parameterValue - childNode.MinParameterValue) / (childNode.MaxParameterValue - childNode.MinParameterValue);

                var length =
                    t.Lerp(childNode.Length0, childNode.Length1);

                return length;
            }

            branchNode = (Float64AdaptivePath3DBranch)childNode;
        }
    }

    public override Float64Scalar LengthToTime(double length)
    {
        if (!RootNode.ContainsLength(length))
            throw new ArgumentOutOfRangeException();

        var branchNode = RootNode;

        while (true)
        {
            var childNode =
                branchNode.GetChildContainingLength(length);

            if (childNode is Float64AdaptivePath3DLeaf)
            {
                var t =
                    (length - childNode.Length0) / childNode.Length;

                var parameterValue =
                    t.Lerp(childNode.MinParameterValue, childNode.MaxParameterValue);

                return parameterValue;
            }

            branchNode = (Float64AdaptivePath3DBranch)childNode;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AdaptivePath3D GetSubCurve(double parameterValue1, double parameterValue2, Float64AdaptivePath3DSamplingOptions options)
    {
        if (parameterValue1 > parameterValue2)
            (parameterValue1, parameterValue2) = (parameterValue2, parameterValue1);

        var curve = new Float64AdaptivePath3D(
            Float64ScalarRange.Create(parameterValue1, parameterValue2), 
            false,
            this
        );

        return curve.GenerateTree(options);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AdaptivePath3D GetSubCurveByLength(double length1, double length2, Float64AdaptivePath3DSamplingOptions options)
    {
        if (length1 > length2)
            (length1, length2) = (length2, length1);

        return GetSubCurve(
            LengthToTime(length1),
            LengthToTime(length2),
            options
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<Float64Path3DLocalFrame> GetEnumerator()
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