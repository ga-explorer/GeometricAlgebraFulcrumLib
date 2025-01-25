using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Lines;

public interface IGraphicsLineGeometry3D 
    : IGraphicsPrimitiveGeometry3D<IFloat64LineSegment3D>
{
    IEnumerable<Pair<ILinFloat64Vector3D>> LineVertexPoints { get; }

    IEnumerable<Pair<int>> LineVertexIndices { get; }
}