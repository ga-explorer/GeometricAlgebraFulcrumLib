using DataStructuresLib.Permutations;
using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PathsMesh.Space2D
{
    /// <summary>
    /// This class represents a paths mesh composed of a partial selection of paths
    /// from a base 2D paths mesh
    /// </summary>
    public class PartialPathsMesh2D : 
        PSeqPartial1D<IPointsPath2D>, 
        IPathsMesh2D
    {
        public IPathsMesh2D BaseMesh { get; }

        public int PathPointsCount
            => BaseMesh.PathPointsCount;

        public int MeshPointsCount
            => Count * PathPointsCount;


        public PartialPathsMesh2D(IPathsMesh2D baseMesh, IndexMapRange1D baseIndexRange)
            : base(baseMesh, baseIndexRange)
        {
            BaseMesh = baseMesh;
        }

        public PartialPathsMesh2D(IPathsMesh2D baseMesh, int firstPathIndex)
            : base(baseMesh, firstPathIndex)
        {
            BaseMesh = baseMesh;
        }

        public PartialPathsMesh2D(IPathsMesh2D baseMesh, int firstPathIndex, int pathsCount)
            : base(baseMesh, firstPathIndex, pathsCount)
        {
            BaseMesh = baseMesh;
        }

        public PartialPathsMesh2D(IPathsMesh2D baseMesh, int firstPathIndex, int pathsCount, bool moveForward)
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