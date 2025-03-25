using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Adaptive;

public sealed class Float64AdaptivePath2DLeaf :
    Float64AdaptivePath2DNode
{
    public override int Count
        => 0;

    public int LeafListIndex { get; }

    public Float64AdaptivePath2DLeaf PrevLeafNode
        => LeafListIndex >= 0
            ? ParentTree.LeafNodesList[LeafListIndex - 1]
            : null;

    public Float64AdaptivePath2DLeaf NextLeafNode
        => LeafListIndex < ParentTree.LeafNodeCount
            ? ParentTree.LeafNodesList[LeafListIndex + 1]
            : null;

    internal Float64AdaptivePath2DLeaf(Float64AdaptivePath2DBranch parentBranch, bool isRightChild)
        : base(parentBranch, isRightChild)
    {
        LeafListIndex = parentBranch.ParentTree.LeafNodeCount;
    }


    public IFloat64LineSegment2D GetLineSegment()
    {
        return Float64LineSegment2D.Create(
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

    public override IEnumerator<Float64AdaptivePath2DNode> GetEnumerator()
    {
        return Enumerable.Empty<Float64AdaptivePath2DNode>().GetEnumerator();
    }
}