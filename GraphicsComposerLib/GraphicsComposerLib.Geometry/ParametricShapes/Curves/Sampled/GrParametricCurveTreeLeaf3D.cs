using System.Collections.Generic;
using System.Linq;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicShapes.Lines;
using NumericalGeometryLib.BasicShapes.Lines.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Sampled
{
    public sealed class GrParametricCurveTreeLeaf3D :
        GrParametricCurveTreeNode3D
    {
        public override int Count 
            => 0;
        

        internal GrParametricCurveTreeLeaf3D(GrParametricCurveTreeBranch3D parentBranch, bool isRightChild)
            : base(parentBranch, isRightChild)
        {
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

        public override IEnumerator<GrParametricCurveTreeNode3D> GetEnumerator()
        {
            return Enumerable.Empty<GrParametricCurveTreeNode3D>().GetEnumerator();
        }
    }
}