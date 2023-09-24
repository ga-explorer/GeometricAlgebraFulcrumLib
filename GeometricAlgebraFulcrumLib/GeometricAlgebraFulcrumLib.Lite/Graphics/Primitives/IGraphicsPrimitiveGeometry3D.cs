using GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives.Vertices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives
{
    /// <summary>
    /// This interface represents a primitives-based graphics 3D geometry like a
    /// set of 3D points, line-strips, triangle-fans, etc.
    /// </summary>
    public interface IGraphicsPrimitiveGeometry3D
    {
        /// <summary>
        /// The primitive type of this graphics geometry:
        /// points, line-loops, line-strips, triangles, triangle-fans, etc.
        /// </summary>
        GraphicsPrimitiveType3D PrimitiveType { get; }

        /// <summary>
        /// The number of vertices in this geometry
        /// </summary>
        int VertexCount { get; }

        /// <summary>
        /// The list of indices connecting the vertices of this graphics geometry
        /// </summary>
        IEnumerable<int> GeometryIndices { get; }

        /// <summary>
        /// The list of vertex information of this graphics geometry
        /// </summary>
        IEnumerable<IGraphicsVertex3D> GeometryVertices { get; }

        /// <summary>
        /// The list of vertex positions of this graphics geometry
        /// </summary>
        IEnumerable<IFloat64Vector3D> GeometryPoints { get; }

        /// <summary>
        /// Get a vertex point by vertex index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        IFloat64Vector3D GetGeometryPoint(int index);
    }

    /// <summary>
    /// This interface represents a primitives-based graphics 3D geometry like a
    /// set of 3D points, line-strips, triangle-fans, etc.
    /// </summary>
    public interface IGraphicsPrimitiveGeometry3D<out T> : 
        IGraphicsPrimitiveGeometry3D, 
        IReadOnlyList<T>
    {
    }
}