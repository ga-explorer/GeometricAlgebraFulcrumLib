using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves.Adaptive;

public sealed class AdaptiveCurveTreeLeaf3D :
    AdaptiveCurveTreeNode3D
{
    public override int Count
        => 0;

    public int LeafListIndex { get; }

    public AdaptiveCurveTreeLeaf3D PrevLeafNode
        => LeafListIndex >= 0
            ? ParentTree.LeafNodesList[LeafListIndex - 1]
            : null;

    public AdaptiveCurveTreeLeaf3D NextLeafNode
        => LeafListIndex < ParentTree.LeafNodeCount
            ? ParentTree.LeafNodesList[LeafListIndex + 1]
            : null;

    internal AdaptiveCurveTreeLeaf3D(AdaptiveCurveTreeBranch3D parentBranch, bool isRightChild)
        : base(parentBranch, isRightChild)
    {
        LeafListIndex = parentBranch.ParentTree.LeafNodeCount;
    }


    public ILineSegment3D GetLineSegment()
    {
        return LineSegment3D.Create(
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

    public override IEnumerator<AdaptiveCurveTreeNode3D> GetEnumerator()
    {
        return Enumerable.Empty<AdaptiveCurveTreeNode3D>().GetEnumerator();
    }
}