using System.Collections.Generic;
using DataStructuresLib.Sequences.Periodic1D;
using GraphicsComposerLib.Geometry.Meshes.PointsPath;

namespace GraphicsComposerLib.Geometry.Meshes.PathsMesh.Space2D
{
    /// <summary>
    /// This class represents an array of 2D point paths
    /// </summary>
    public sealed class ArrayPathsMesh2D
        : PSeqArray1D<IPointsPath2D>, IPathsMesh2D
    {
        public int PathPointsCount { get; }

        public int MeshPointsCount
            => Count * PathPointsCount;


        public ArrayPathsMesh2D(int verticesPerPath, int pathsCount)
            : base(pathsCount)
        {
            PathPointsCount = verticesPerPath;
        }

        public ArrayPathsMesh2D(int verticesPerPath, params IPointsPath2D[] pathsArray)
            : base(pathsArray)
        {
            PathPointsCount = verticesPerPath;
        }

        public ArrayPathsMesh2D(int verticesPerPath, IEnumerable<IPointsPath2D> pathsList)
            : base(pathsList)
        {
            PathPointsCount = verticesPerPath;
        }
    }
}