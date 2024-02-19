using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves.Adaptive;

public abstract class AdaptiveCurveTreeNode2D :
    IReadOnlyCollection<AdaptiveCurveTreeNode2D>
{
    public AdaptiveCurve2D ParentTree { get; }

    public AdaptiveCurveTreeBranch2D ParentBranch { get; }

    public IEnumerable<AdaptiveCurveTreeBranch2D> ParentBranches
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
        => this is AdaptiveCurveTreeLeaf2D;

    public bool IsBranch
        => this is AdaptiveCurveTreeBranch2D;

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

    public AdaptiveCurveTreeCorner2D Corner0 { get; }

    public AdaptiveCurveTreeCorner2D Corner1 { get; }

    public int GridIndex0
        => Corner0.GridIndex;

    public int GridIndex1
        => Corner1.GridIndex;

    public ParametricCurveLocalFrame2D Frame0
        => Corner0.Frame;

    public ParametricCurveLocalFrame2D Frame1
        => Corner1.Frame;

    public double MinParameterValue
        => Frame0.ParameterValue;

    public double MaxParameterValue
        => Frame1.ParameterValue;

    public double Length0 { get; internal set; }

    public double Length1 { get; internal set; }

    public double Length
        => Length1 - Length0;

    public IEnumerable<AdaptiveCurveTreeLeaf2D> LeafNodes
    {
        get
        {
            var stack = new Stack<AdaptiveCurveTreeNode2D>();

            stack.Push(this);

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


    /// <summary>
    /// Construct root node of tree
    /// </summary>
    /// <param name="parentTree"></param>
    protected AdaptiveCurveTreeNode2D(AdaptiveCurve2D parentTree)
    {
        ParentTree = parentTree;
        ParentBranch = null;
        Level = 0;
        CellIndex = 0;

        ParentTree.TreeLevelCount = 0;

        var position0 = new AdaptiveCurveTreeCornerPosition2D(0, 0);
        var position1 = new AdaptiveCurveTreeCornerPosition2D(0, 1);

        Corner0 = ParentTree.GetOrAddCorner(position0);
        Corner1 = ParentTree.GetOrAddCorner(position1);
    }

    /// <summary>
    /// Construct sub-node of tree
    /// </summary>
    /// <param name="parentBranch"></param>
    /// <param name="isRightChild"></param>
    protected AdaptiveCurveTreeNode2D(AdaptiveCurveTreeBranch2D parentBranch, bool isRightChild)
    {
        Debug.Assert(parentBranch.Level < 30);

        ParentTree = parentBranch.ParentTree;
        ParentBranch = parentBranch;
        Level = parentBranch.Level + 1;
        CellIndex = parentBranch.CellIndex << 1 | (isRightChild ? 1 : 0);

        if (ParentTree.TreeLevelCount < Level)
            ParentTree.TreeLevelCount = Level;

        var position0 = new AdaptiveCurveTreeCornerPosition2D(Level, CellIndex);
        var position1 = new AdaptiveCurveTreeCornerPosition2D(Level, CellIndex + 1);

        Corner0 = ParentTree.GetOrAddCorner(position0);
        Corner1 = ParentTree.GetOrAddCorner(position1);
    }


    internal abstract double UpdateLengthData(double length0);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(double parameterValue)
    {
        return parameterValue >= MinParameterValue &&
               parameterValue <= MaxParameterValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsLength(double length)
    {
        return length >= Length0 &&
               length <= Length1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<ParametricCurveLocalFrame2D> GetEdgeFramePair()
    {
        return new Pair<ParametricCurveLocalFrame2D>(Frame0, Frame1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double EdgeFrameDistance()
    {
        return Frame0.Point.GetDistanceToPoint(Frame1.Point);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64PlanarAngle EdgeFrameMaxAngle()
    {
        return Frame0.Tangent.GetVectorsAngle(Frame1.Tangent);
    }

    public bool HasNearEdgeFrames(AdaptiveCurveSamplingOptions2D options)
    {
        var parameterDistanceMax = options.MaxEdgeFramesParameterDistance;
        var parameterDistance = Math.Abs(Frame0.ParameterValue - Frame1.ParameterValue);
        if (parameterDistance <= parameterDistanceMax)
            return true;

        var pointDistanceMax = options.MaxEdgeFramesDistance;
        var pointDistance = Frame0.Point.GetDistanceToPoint(Frame1.Point);
        if (pointDistance <= pointDistanceMax)
            return true;
            
        var angleMax = options.MaxEdgeFramesAngle.Radians;
        var angle = Frame0.Tangent.GetVectorsAngle(Frame1.Tangent).Radians;
        if (angle <= angleMax)
            return true;

        return false;
    }

    public abstract IEnumerator<AdaptiveCurveTreeNode2D> GetEnumerator();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}