using DataStructuresLib.Sequences.Periodic1D;
using GraphicsComposerLib.Geometry.Geometry.PointsPath;

namespace GraphicsComposerLib.Geometry.Geometry.PathsMesh
{
    /// <summary>
    /// This interface represents a 2D mesh composed of two or more point paths.
    /// All paths contain the same number of points
    /// </summary>
    /// <remarks>
    /// Each mesh consists of a collection of paths.
    /// A path is a poly-line constructed from a set of points in 2D.
    /// All paths in the mesh must have the same number of points.
    /// A mesh must have at least two path each with at least two points.
    /// </remarks>
    public interface IPathsMesh2D 
        : IPeriodicSequence1D<IPointsPath2D>
    {
        /// <summary>
        /// Number of points per path in this mesh
        /// </summary>
        int PathPointsCount { get; }

        /// <summary>
        /// Total number of points in this mesh
        /// </summary>
        int MeshPointsCount { get; }
    }
}