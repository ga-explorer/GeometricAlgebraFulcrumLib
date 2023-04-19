using DataStructuresLib.Permutations;
using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space2D
{
    public class PartialPointsPath2D
        : PSeqPartial1D<IFloat64Tuple2D>, IPointsPath2D
    {
        public IPointsPath2D BasePath { get; }


        public PartialPointsPath2D(IPointsPath2D basePath, IndexMapRange1D baseIndexRange) 
            : base(basePath, baseIndexRange)
        {
            BasePath = basePath;
        }

        public PartialPointsPath2D(IPointsPath2D basePath, int firstIndex) 
            : base(basePath, firstIndex)
        {
            BasePath = basePath;
        }

        public PartialPointsPath2D(IPointsPath2D basePath, int firstIndex, int count) 
            : base(basePath, firstIndex, count)
        {
            BasePath = basePath;
        }

        public PartialPointsPath2D(IPointsPath2D basePath, int firstIndex, int count, bool moveForward) 
            : base(basePath, firstIndex, count, moveForward)
        {
            BasePath = basePath;
        }
    }
}