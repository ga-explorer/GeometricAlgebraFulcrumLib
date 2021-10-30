using DataStructuresLib.Sequences.Periodic1D;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Geometry.PointsPath.Space2D
{
    public class ConstantPointsPath2D
        : PSeqConstant1D<ITuple2D>, IPointsPath2D
    {
        public ConstantPointsPath2D(int count)
            : base(count, new Tuple2D(0, 0))
        {
        }

        public ConstantPointsPath2D(int count, double x, double y)
            : base(count, new Tuple2D(x, y))
        {
        }

        public ConstantPointsPath2D(int count, ITuple2D value)
            : base(count, value)
        {
        }
    }
}
