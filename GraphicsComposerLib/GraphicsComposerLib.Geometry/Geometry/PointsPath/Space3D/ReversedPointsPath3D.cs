using DataStructuresLib.Sequences.Periodic1D;
using EuclideanGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Geometry.PointsPath.Space3D
{
    public class ReversedPointsPath3D
        : PSeqReverse1D<ITuple3D>, IPointsPath3D
    {
        public IPointsPath3D BasePath { get; }


        public ReversedPointsPath3D(IPointsPath3D basePath)
            : base(basePath)
        {
            BasePath = basePath;
        }
    }
}