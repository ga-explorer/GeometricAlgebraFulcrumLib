using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Lines;

public interface IGraphicsLineGeometry3D 
    : IGraphicsPrimitiveGeometry3D<ILineSegment3D>
{
    IEnumerable<Pair<ILinFloat64Vector3D>> LineVertexPoints { get; }

    IEnumerable<Pair<int>> LineVertexIndices { get; }
}