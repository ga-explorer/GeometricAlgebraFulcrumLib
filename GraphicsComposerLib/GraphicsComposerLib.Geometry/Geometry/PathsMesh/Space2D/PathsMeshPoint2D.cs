using DataStructuresLib.Basic;
using EuclideanGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Geometry.PathsMesh.Space2D
{
    /// <summary>
    /// This class represents a 2D point inside a paths mesh. The class holds data
    /// on the point coordinates and index values inside the mesh.
    /// </summary>
    public sealed class PathsMeshPoint2D : ITuple2D
    {
        /// <summary>
        /// The base mesh of this point
        /// </summary>
        public IPathsMesh2D BaseMesh { get; }

        /// <summary>
        /// The path index of this point inside its base mesh
        /// </summary>
        public int PathIndex { get; }

        /// <summary>
        /// The point index of this point inside its base path
        /// </summary>
        public int PathPointIndex { get; }

        /// <summary>
        /// The point index of this point inside its base mesh
        /// </summary>
        public int MeshPointIndex 
            => PathPointIndex + PathIndex * BaseMesh.PathPointsCount;

        /// <summary>
        /// The path index and path point index of this point
        /// </summary>
        public Pair<int> PointIndexPair 
            => new Pair<int>(PathIndex, PathPointIndex);

        /// <summary>
        /// The point coordinates of this point
        /// </summary>
        public ITuple2D Point 
            => BaseMesh[PathIndex][PathPointIndex];

        public double X 
            => Point.X;

        public double Y 
            => Point.Y;

        public double Item1 
            => X;

        public double Item2
            => Y;

        public bool IsValid 
            => Point.IsValid;

        public bool IsInvalid 
            => Point.IsInvalid;


        internal PathsMeshPoint2D(IPathsMesh2D baseMesh, int pathIndex, int pathPointIndex)
        {
            BaseMesh = baseMesh;
            PathIndex = pathIndex.Mod(baseMesh.Count);
            PathPointIndex = pathPointIndex.Mod(baseMesh.PathPointsCount);
        }

    }
}