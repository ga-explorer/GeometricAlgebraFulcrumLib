using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives.Triangles
{
    public interface IGraphicsTriangleGeometry3D 
        : IGraphicsPrimitiveGeometry3D<ITriangle3D>
    {
        IEnumerable<Triplet<IFloat64Vector3D>> TriangleVertexPoints { get; }

        IEnumerable<Triplet<int>> TriangleVertexIndices { get; }

        IEnumerable<IFloat64Vector3D> VertexNormals { get; }

        IEnumerable<IFloat64Vector2D> VertexTextureUVs { get; }

        IEnumerable<Color> VertexColors { get; }

        bool VertexNormalsEnabled { get; }

        bool VertexTextureUVsEnabled { get; }

        bool VertexColorsEnabled { get; }

        Normal3D GetVertexNormal(int index);

        GrVertexNormalComputationMethod NormalComputationMethod { get; }

        void ComputeVertexNormals(bool inverseNormals);
    }
}