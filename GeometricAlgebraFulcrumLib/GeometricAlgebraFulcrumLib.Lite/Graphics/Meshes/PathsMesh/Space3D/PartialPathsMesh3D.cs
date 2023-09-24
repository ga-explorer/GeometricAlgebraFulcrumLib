using DataStructuresLib.Permutations;
using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PathsMesh.Space3D
{
    /// <summary>
    /// This class represents a paths mesh composed of a partial selection of paths
    /// from a base 3D paths mesh
    /// </summary>
    public class PartialPathsMesh3D
        : PSeqPartial1D<IPointsPath3D>, IPathsMesh3D
    {
        public IPathsMesh3D BaseMesh { get; }

        public int PathPointsCount 
            => BaseMesh.PathPointsCount;

        public int MeshPointsCount 
            => Count * PathPointsCount;


        public PartialPathsMesh3D(IPathsMesh3D baseMesh, IndexMapRange1D baseIndexRange)
            : base(baseMesh, baseIndexRange)
        {
            BaseMesh = baseMesh;
        }

        public PartialPathsMesh3D(IPathsMesh3D baseMesh, int firstPathIndex)
            : base(baseMesh, firstPathIndex)
        {
            BaseMesh = baseMesh;
        }

        public PartialPathsMesh3D(IPathsMesh3D baseMesh, int firstPathIndex, int pathsCount)
            : base(baseMesh, firstPathIndex, pathsCount)
        {
            BaseMesh = baseMesh;
        }

        public PartialPathsMesh3D(IPathsMesh3D baseMesh, int firstPathIndex, int pathsCount, bool moveForward)
            : base(baseMesh, firstPathIndex, pathsCount, moveForward)
        {
            BaseMesh = baseMesh;
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}