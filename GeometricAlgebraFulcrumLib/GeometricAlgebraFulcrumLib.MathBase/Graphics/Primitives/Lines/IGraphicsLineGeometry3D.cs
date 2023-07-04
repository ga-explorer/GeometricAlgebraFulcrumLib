using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Primitives.Lines
{
    public interface IGraphicsLineGeometry3D 
        : IGraphicsPrimitiveGeometry3D<ILineSegment3D>
    {
        IEnumerable<Pair<IFloat64Vector3D>> LineVertexPoints { get; }

        IEnumerable<Pair<int>> LineVertexIndices { get; }
    }
}