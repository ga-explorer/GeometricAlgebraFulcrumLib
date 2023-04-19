using System.Collections.Generic;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Lines;

namespace GraphicsComposerLib.Geometry.Primitives.Lines
{
    public interface IGraphicsLineGeometry3D 
        : IGraphicsPrimitiveGeometry3D<ILineSegment3D>
    {
        IEnumerable<Pair<IFloat64Tuple3D>> LineVertexPoints { get; }

        IEnumerable<Pair<int>> LineVertexIndices { get; }
    }
}