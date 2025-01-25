using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves.Adaptive;

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


    public IFloat64LineSegment3D GetLineSegment()
    {
        return Float64LineSegment3D.Create(
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