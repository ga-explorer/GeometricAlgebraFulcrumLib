using System.Collections.Generic;
using DataStructuresLib.Sequences.Periodic1D;
using GraphicsComposerLib.Geometry.Geometry.PointsPath;

namespace GraphicsComposerLib.Geometry.Geometry.PathsMesh.Space2D
{
    /// <summary>
    /// This class represents a mesh constructed from a sequence of paths in 2D.
    /// All paths are assumed to have the exact same number of points on them.
    /// </summary>
    public sealed class ListPathsMesh2D
        : PSeqReadOnlyList1D<IPointsPath2D>, IPathsMesh2D
    {
        public int PathPointsCount { get; }

        public int MeshPointsCount
            => Count * PathPointsCount;


        public ListPathsMesh2D(int pathPointsCount, IReadOnlyList<IPointsPath2D> pathsList)
            : base(pathsList)
        {
            PathPointsCount = pathPointsCount;
        }

        public ListPathsMesh2D(int verticesPerPath, params IPointsPath2D[] pathsArray)
            : base(pathsArray)
        {
            PathPointsCount = verticesPerPath;
        }
    }
}