using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space2D
{
    public class ConstantPointsPath2D
        : PSeqConstant1D<IFloat64Tuple2D>, IPointsPath2D
    {
        public ConstantPointsPath2D(int count)
            : base(count, new Float64Tuple2D(0, 0))
        {
        }

        public ConstantPointsPath2D(int count, double x, double y)
            : base(count, new Float64Tuple2D(x, y))
        {
        }

        public ConstantPointsPath2D(int count, IFloat64Tuple2D value)
            : base(count, value)
        {
        }
    }
}
