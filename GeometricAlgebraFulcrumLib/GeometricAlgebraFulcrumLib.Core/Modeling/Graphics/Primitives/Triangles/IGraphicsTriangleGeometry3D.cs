using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Primitives.Triangles;

public interface IGraphicsTriangleGeometry3D 
    : IGraphicsPrimitiveGeometry3D<ITriangle3D>
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