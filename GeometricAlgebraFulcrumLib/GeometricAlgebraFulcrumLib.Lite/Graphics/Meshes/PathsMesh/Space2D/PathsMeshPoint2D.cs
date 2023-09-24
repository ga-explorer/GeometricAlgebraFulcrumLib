using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PathsMesh.Space2D
{
    /// <summary>
    /// This class represents a 2D point inside a paths mesh. The class holds data
    /// on the point coordinates and index values inside the mesh.
    /// </summary>
    public sealed class PathsMeshPoint2D : IFloat64Vector2D
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
        public IFloat64Vector2D Point 
            => BaseMesh[PathIndex][PathPointIndex];
        
        public int VSpaceDimensions 
            => 2;

        public Float64Scalar X 
            => Point.X;

        public Float64Scalar Y 
            => Point.Y;

        public double Item1 
            => X.Value;

        public double Item2
            => Y.Value;

        public bool IsValid() => Point.IsValid();


        internal PathsMeshPoint2D(IPathsMesh2D baseMesh, int pathIndex, int pathPointIndex)
        {
            BaseMesh = baseMesh;
            PathIndex = pathIndex.Mod(baseMesh.Count);
            PathPointIndex = pathPointIndex.Mod(baseMesh.PathPointsCount);
        }

    }
}