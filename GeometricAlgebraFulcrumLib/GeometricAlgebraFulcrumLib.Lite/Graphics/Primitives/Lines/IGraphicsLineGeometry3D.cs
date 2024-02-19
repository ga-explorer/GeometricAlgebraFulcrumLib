using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives.Lines;

public interface IGraphicsLineGeometry3D 
    : IGraphicsPrimitiveGeometry3D<ILineSegment3D>
{
    IEnumerable<Pair<IFloat64Vector3D>> LineVertexPoints { get; }

    IEnumerable<Pair<int>> LineVertexIndices { get; }
}