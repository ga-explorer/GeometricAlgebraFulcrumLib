using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Lines.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Sampled
{
    public sealed class SampledParametricCurveTreeLeaf3D :
        SampledParametricCurveTreeNode3D
    {
        public override int Count
            => 0;

        public int LeafListIndex { get; }

        public SampledParametricCurveTreeLeaf3D PrevLeafNode
            => LeafListIndex >= 0
                ? ParentTree.LeafNodesList[LeafListIndex - 1]
                : null;

        public SampledParametricCurveTreeLeaf3D NextLeafNode
            => LeafListIndex < ParentTree.LeafNodeCount
                ? ParentTree.LeafNodesList[LeafListIndex + 1]
                : null;

        internal SampledParametricCurveTreeLeaf3D(SampledParametricCurveTreeBranch3D parentBranch, bool isRightChild)
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

        public override IEnumerator<SampledParametricCurveTreeNode3D> GetEnumerator()
        {
            return Enumerable.Empty<SampledParametricCurveTreeNode3D>().GetEnumerator();
        }
    }
}