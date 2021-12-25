using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D
{
    public class ConstantPointsPath3D
        : PSeqConstant1D<ITuple3D>, IPointsPath3D
    {
        public ConstantPointsPath3D(int count)
            : base(count, new Tuple3D(0, 0, 0))
        {
        }

        public ConstantPointsPath3D(int count, double x, double y, double z)
            : base(count, new Tuple3D(x, y, z))
        {
        }

        public ConstantPointsPath3D(int count, ITuple3D value)
            : base(count, value)
        {
        }
    }
}