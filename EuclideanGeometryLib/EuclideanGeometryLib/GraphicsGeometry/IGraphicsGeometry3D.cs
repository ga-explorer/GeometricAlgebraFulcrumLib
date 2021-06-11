using System.Collections.Generic;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.GraphicsGeometry.Vertices;

namespace EuclideanGeometryLib.GraphicsGeometry
{
    /// <summary>
    /// This interface represents a primitives-based graphics 3D geometry like a
    /// set of 3D points, line-strips, triangle-fans, etc.
    /// </summary>
    public interface IGraphicsGeometry3D
    {
        /// <summary>
        /// The primitive type of this graphics geometry:
        /// points, line-loops, line-strips, triangles, triangle-fans, etc.
        /// </summary>
        GraphicsPrimitiveType3D PrimitiveType { get; }

        /// <summary>
        /// The list of vertex information of this graphics geometry
        /// </summary>
        IEnumerable<IGraphicsVertex3D> Vertices { get; }

        /// <summary>
        /// The list of vertex positions of this graphics geometry
        /// </summary>
        IReadOnlyList<ITuple3D> VertexPoints { get; }

        /// <summary>
        /// The list of indices connecting the vertices of this graphics geometry
        /// </summary>
        IReadOnlyList<int> VertexIndices { get; }
    }

    /// <summary>
    /// This interface represents a primitives-based graphics 3D geometry like a
    /// set of 3D points, line-strips, triangle-fans, etc.
    /// </summary>
    public interface IGraphicsGeometry3D<out T>
        : IGraphicsGeometry3D, IReadOnlyList<T>
    {
    }
}