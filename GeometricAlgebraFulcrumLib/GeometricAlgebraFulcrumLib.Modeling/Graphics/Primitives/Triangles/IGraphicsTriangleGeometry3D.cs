using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Triangles;

public interface IGraphicsTriangleGeometry3D 
    : IGraphicsPrimitiveGeometry3D<IFloat64Triangle3D>
{
    IEnumerable<Triplet<ILinFloat64Vector3D>> TriangleVertexPoints { get; }

    IEnumerable<Triplet<int>> TriangleVertexIndices { get; }

    IEnumerable<ILinFloat64Vector3D> VertexNormals { get; }

    IEnumerable<ILinFloat64Vector2D> VertexTextureUVs { get; }

    IEnumerable<Color> VertexColors { get; }

    bool VertexNormalsEnabled { get; }

    bool VertexTextureUVsEnabled { get; }

    bool VertexColorsEnabled { get; }

    LinFloat64Normal3D GetVertexNormal(int index);

    GrVertexNormalComputationMethod NormalComputationMethod { get; }

    void ComputeVertexNormals(bool inverseNormals);
}