using System.Collections.Generic;
using DataStructuresLib.Sequences.Periodic1D;
using GraphicsComposerLib.Geometry.Meshes.PointsPath;

namespace GraphicsComposerLib.Geometry.Meshes.PathsMesh.Space3D
{
    /// <summary>
    /// This class represents a mesh constructed from a sequence of paths in 3D.
    /// All paths are assumed to have the exact same number of vertices on them.
    /// </summary>
    public sealed class ListPathsMesh3D : 
        PSeqReadOnlyList1D<IPointsPath3D>, 
        IPathsMesh3D
    {
        public int PathPointsCount { get; }

        public int MeshPointsCount 
            => Count * PathPointsCount;


        public ListPathsMesh3D(int verticesPerPath, IReadOnlyList<IPointsPath3D> pathsList)
            : base(pathsList)
        {
            PathPointsCount = verticesPerPath;
        }

        public ListPathsMesh3D(int verticesPerPath, params IPointsPath3D[] pathsArray)
            : base(pathsArray)
        {
            PathPointsCount = verticesPerPath;
        }
    }
}
