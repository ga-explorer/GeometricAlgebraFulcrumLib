using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Primitives.Triangles
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