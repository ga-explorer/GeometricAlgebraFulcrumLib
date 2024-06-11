using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves.Adaptive;

public abstract class AdaptiveCurveTreeNode3D :
    IReadOnlyCollection<AdaptiveCurveTreeNode3D>
{
    public AdaptiveCurve3D ParentTree { get; }

    public AdaptiveCurveTreeBranch3D? ParentBranch { get; }

    public IEnumerable<AdaptiveCurveTreeBranch3D> ParentBranches
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
        => this is AdaptiveCurveTreeLeaf3D;

    public bool IsBranch
        => this is AdaptiveCurveTreeBranch3D;

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

    public AdaptiveCurveTreeCorner3D Corner0 { get; }

    public AdaptiveCurveTreeCorner3D Corner1 { get; }

    public int GridIndex0
        => Corner0.GridIndex;

    public int GridIndex1
        => Corner1.GridIndex;

    public ParametricCurveLocalFrame3D Frame0
        => Corner0.Frame;

    public ParametricCurveLocalFrame3D Frame1
        => Corner1.Frame;

    public double MinParameterValue
        => Frame0.ParameterValue;

    public double MaxParameterValue
        => Frame1.ParameterValue;

    public double Length0 { get; internal set; }

    public double Length1 { get; internal set; }

    public double Length
        => Length1 - Length0;

    public IEnumerable<AdaptiveCurveTreeLeaf3D> LeafNodes
    {
        get
        {
            var stack = new Stack<AdaptiveCurveTreeNode3D>();

            stack.Push(this);

            while (stack.Count > 0)
            {
                var node = stack.Pop();

                if (node is not AdaptiveCurveTreeBranch3D branchNode)
                {
                    yield return (AdaptiveCurveTreeLeaf3D)node;
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
    protected AdaptiveCurveTreeNode3D(AdaptiveCurve3D parentTree)
    {
        ParentTree = parentTree;
        ParentBranch = null;
        Level = 0;
        CellIndex = 0;

        ParentTree.TreeLevelCount = 0;

        var position0 = new AdaptiveCurveTreeCornerPosition3D(0, 0);
        var position1 = new AdaptiveCurveTreeCornerPosition3D(0, 1);

        Corner0 = ParentTree.GetOrAddCorner(position0);
        Corner1 = ParentTree.GetOrAddCorner(position1);
    }

    /// <summary>
    /// Construct sub-node of tree
    /// </summary>
    /// <param name="parentBranch"></param>
    /// <param name="isRightChild"></param>
    protected AdaptiveCurveTreeNode3D(AdaptiveCurveTreeBranch3D parentBranch, bool isRightChild)
    {
        Debug.Assert(parentBranch.Level < 30);

        ParentTree = parentBranch.ParentTree;
        ParentBranch = parentBranch;
        Level = parentBranch.Level + 1;
        CellIndex = parentBranch.CellIndex << 1 | (isRightChild ? 1 : 0);

        if (ParentTree.TreeLevelCount < Level)
            ParentTree.TreeLevelCount = Level;

        var position0 = new AdaptiveCurveTreeCornerPosition3D(Level, CellIndex);
        var position1 = new AdaptiveCurveTreeCornerPosition3D(Level, CellIndex + 1);

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
    public Pair<ParametricCurveLocalFrame3D> GetEdgeFramePair()
    {
        return new Pair<ParametricCurveLocalFrame3D>(Frame0, Frame1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double EdgeFrameDistance()
    {
        return Frame0.Point.GetDistanceToPoint(Frame1.Point);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle EdgeFrameMaxAngle()
    {
        var maxAngle = LinFloat64PolarAngle.Angle0;

        var angle = Frame0.Normal1.GetAngle(Frame1.Normal1);
        if (angle.RadiansValue > maxAngle.RadiansValue) maxAngle = angle;

        angle = Frame0.Normal2.GetAngle(Frame1.Normal2);
        if (angle.RadiansValue > maxAngle.RadiansValue) maxAngle = angle;

        angle = Frame0.Tangent.GetAngle(Frame1.Tangent);
        if (angle.RadiansValue > maxAngle.RadiansValue) maxAngle = angle;

        return maxAngle;
    }

    public bool HasNearEdgeFrames(AdaptiveCurveSamplingOptions3D options)
    {
        var parameterDistanceMax = options.MaxEdgeFramesParameterDistance;
        var parameterDistance = Math.Abs(Frame0.ParameterValue - Frame1.ParameterValue);
        if (parameterDistance <= parameterDistanceMax)
            return true;

        var pointDistanceMax = options.MaxEdgeFramesDistance;
        var pointDistance = Frame0.Point.GetDistanceToPoint(Frame1.Point);
        if (pointDistance <= pointDistanceMax)
            return true;

        var angleMax = options.MaxEdgeFramesAngle.RadiansValue;
        var angle = Frame0.Normal1.GetAngle(Frame1.Normal1).RadiansValue;
        if (angle > angleMax) return false;

        angle = Frame0.Normal2.GetAngle(Frame1.Normal2).RadiansValue;
        if (angle > angleMax) return false;

        angle = Frame0.Tangent.GetAngle(Frame1.Tangent).RadiansValue;
        if (angle > angleMax) return false;

        return true;
    }

    public abstract IEnumerator<AdaptiveCurveTreeNode3D> GetEnumerator();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}