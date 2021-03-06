using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space2D
{
    public class ReversedPointsPath2D
        : PSeqReverse1D<ITuple2D>, IPointsPath2D
    {
        public IPointsPath2D BasePath { get; }


        public ReversedPointsPath2D(IPointsPath2D basePath)
            : base(basePath)
        {
            BasePath = basePath;
        }
    }
}