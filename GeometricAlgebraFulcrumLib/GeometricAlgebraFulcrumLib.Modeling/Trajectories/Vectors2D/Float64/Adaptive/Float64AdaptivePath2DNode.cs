using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Adaptive;

public abstract class Float64AdaptivePath2DNode :
    IReadOnlyCollection<Float64AdaptivePath2DNode>
{
    public Float64AdaptivePath2D ParentTree { get; }

    public Float64AdaptivePath2DBranch ParentBranch { get; }

    public IEnumerable<Float64AdaptivePath2DBranch> ParentBranches
    {
        get
        {
            for (var node = ParentBranch; node is not null; node = node.ParentBranch)
                yield return node;
        }
    }

    public bool IsRoot
        => ParentBranch is null;

    public bool IsLeaf
        => this is Float64AdaptivePath2DLeaf;

    public bool IsBranch
        => this is Float64AdaptivePath2DBranch;

    public bool IsChild
        => ParentBranch is not null;

    public bool IsLeftChild
        => (CellIndex & 1) == 0;

    public bool IsRightChild
        => (CellIndex & 1) == 1;

    public abstract int Count { get; }

    public int Level { get; }

    public int CellIndex { get; }

    public int FrameIndex0
        => Corner0.Index;

    public int FrameIndex1
        => Corner1.Index;

    public Float64AdaptivePath2DCorner Corner0 { get; }

    public Float64AdaptivePath2DCorner Corner1 { get; }

    public int GridIndex0
        => Corner0.GridIndex;

    public int GridIndex1
        => Corner1.GridIndex;

    public Float64Path2DLocalFrame Frame0
        => Corner0.Frame;

    public Float64Path2DLocalFrame Frame1
        => Corner1.Frame;

    public double MinParameterValue
        => Frame0.Time;

    public double MaxParameterValue
        => Frame1.Time;

    public double Length0 { get; internal set; }

    public double Length1 { get; internal set; }

    public double Length
        => Length1 - Length0;

    public IEnumerable<Float64AdaptivePath2DLeaf> LeafNodes
    {
        get
        {
            var stack = new Stack<Float64AdaptivePath2DNode>();

            stack.Push(this);

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


    /// <summary>
    /// Construct root node of tree
    /// </summary>
    /// <param name="parentTree"></param>
    protected Float64AdaptivePath2DNode(Float64AdaptivePath2D parentTree)
    {
        ParentTree = parentTree;
        ParentBranch = null;
        Level = 0;
        CellIndex = 0;

        ParentTree.TreeLevelCount = 0;

        var position0 = new Float64AdaptivePath2DCornerPosition(0, 0);
        var position1 = new Float64AdaptivePath2DCornerPosition(0, 1);

        Corner0 = ParentTree.GetOrAddCorner(position0);
        Corner1 = ParentTree.GetOrAddCorner(position1);
    }

    /// <summary>
    /// Construct sub-node of tree
    /// </summary>
    /// <param name="parentBranch"></param>
    /// <param name="isRightChild"></param>
    protected Float64AdaptivePath2DNode(Float64AdaptivePath2DBranch parentBranch, bool isRightChild)
    {
        Debug.Assert(parentBranch.Level < 30);

        ParentTree = parentBranch.ParentTree;
        ParentBranch = parentBranch;
        Level = parentBranch.Level + 1;
        CellIndex = parentBranch.CellIndex << 1 | (isRightChild ? 1 : 0);

        if (ParentTree.TreeLevelCount < Level)
            ParentTree.TreeLevelCount = Level;

        var position0 = new Float64AdaptivePath2DCornerPosition(Level, CellIndex);
        var position1 = new Float64AdaptivePath2DCornerPosition(Level, CellIndex + 1);

        Corner0 = ParentTree.GetOrAddCorner(position0);
        Corner1 = ParentTree.GetOrAddCorner(position1);
    }


    internal abstract double UpdateLengthData(double length0);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(double t)
    {
        return t >= MinParameterValue &&
               t <= MaxParameterValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsLength(double length)
    {
        return length >= Length0 &&
               length <= Length1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<Float64Path2DLocalFrame> GetEdgeFramePair()
    {
        return new Pair<Float64Path2DLocalFrame>(Frame0, Frame1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double EdgeFrameDistance()
    {
        return Frame0.Point.GetDistanceToPoint(Frame1.Point);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle EdgeFrameMaxAngle()
    {
        return Frame0.Tangent.GetAngle(Frame1.Tangent);
    }

    public bool HasNearEdgeFrames(Float64AdaptivePath2DSamplingOptions options)
    {
        var parameterDistanceMax = options.MaxEdgeFramesParameterDistance;
        var parameterDistance = Math.Abs(Frame0.Time - Frame1.Time);
        if (parameterDistance <= parameterDistanceMax)
            return true;

        var pointDistanceMax = options.MaxEdgeFramesDistance;
        var pointDistance = Frame0.Point.GetDistanceToPoint(Frame1.Point);
        if (pointDistance <= pointDistanceMax)
            return true;

        var angleMax = options.MaxEdgeFramesAngle.RadiansValue;
        var angle = Frame0.Tangent.GetAngle(Frame1.Tangent).RadiansValue;
        if (angle <= angleMax)
            return true;

        return false;
    }

    public abstract IEnumerator<Float64AdaptivePath2DNode> GetEnumerator();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}