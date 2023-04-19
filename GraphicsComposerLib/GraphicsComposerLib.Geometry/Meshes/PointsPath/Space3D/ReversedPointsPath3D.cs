using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D
{
    public class ReversedPointsPath3D
        : PSeqReverse1D<IFloat64Tuple3D>, IPointsPath3D
    {
        public IPointsPath3D BasePath { get; }


        public ReversedPointsPath3D(IPointsPath3D basePath)
            : base(basePath)
        {
            BasePath = basePath;
        }
    }
}