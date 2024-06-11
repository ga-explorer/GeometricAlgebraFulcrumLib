using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Immutable;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space2D.Curves.Adaptive;

public sealed class AdaptiveCurveTreeLeaf2D :
    AdaptiveCurveTreeNode2D
{
    public override int Count
        => 0;

    public int LeafListIndex { get; }

    public AdaptiveCurveTreeLeaf2D PrevLeafNode
        => LeafListIndex >= 0
            ? ParentTree.LeafNodesList[LeafListIndex - 1]
            : null;

    public AdaptiveCurveTreeLeaf2D NextLeafNode
        => LeafListIndex < ParentTree.LeafNodeCount
            ? ParentTree.LeafNodesList[LeafListIndex + 1]
            : null;

    internal AdaptiveCurveTreeLeaf2D(AdaptiveCurveTreeBranch2D parentBranch, bool isRightChild)
        : base(parentBranch, isRightChild)
    {
        LeafListIndex = parentBranch.ParentTree.LeafNodeCount;
    }


    public ILineSegment2D GetLineSegment()
    {
        return LineSegment2D.Create(
            Frame0.Point,
            Frame1.Point
        );
    }

    internal override double UpdateLengthData(double length0)
    {
        Length0 = length0;
        Length1 = length0 + Frame1.Point.GetDistanceToPoint(Frame0.Point);

        return Length1;
    }

    public override IEnumerator<AdaptiveCurveTreeNode2D> GetEnumerator()
    {
        return Enumerable.Empty<AdaptiveCurveTreeNode2D>().GetEnumerator();
    }
}