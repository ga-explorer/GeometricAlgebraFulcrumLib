using DataStructuresLib.Permutations;
using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D
{
    public class PartialPointsPath3D
        : PSeqPartial1D<IFloat64Tuple3D>, IPointsPath3D
    {
        public IPointsPath3D BasePath { get; }


        public PartialPointsPath3D(IPointsPath3D basePath, IndexMapRange1D baseIndexRange) 
            : base(basePath, baseIndexRange)
        {
            BasePath = basePath;
        }

        public PartialPointsPath3D(IPointsPath3D basePath, int firstIndex) 
            : base(basePath, firstIndex)
        {
            BasePath = basePath;
        }

        public PartialPointsPath3D(IPointsPath3D basePath, int firstIndex, int count) 
            : base(basePath, firstIndex, count)
        {
            BasePath = basePath;
        }

        public PartialPointsPath3D(IPointsPath3D basePath, int firstIndex, int count, bool moveForward) 
            : base(basePath, firstIndex, count, moveForward)
        {
            BasePath = basePath;
        }
    }
}