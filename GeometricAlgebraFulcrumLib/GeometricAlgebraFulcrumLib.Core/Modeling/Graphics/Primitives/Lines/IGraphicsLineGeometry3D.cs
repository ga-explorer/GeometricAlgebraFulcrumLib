using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Primitives.Lines;

public interface IGraphicsLineGeometry3D 
    : IGraphicsPrimitiveGeometry3D<ILineSegment3D>
{
    IEnumerable<Pair<ILinFloat64Vector3D>> LineVertexPoints { get; }

    IEnumerable<Pair<int>> LineVertexIndices { get; }
}