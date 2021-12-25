using System.Collections.Generic;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicShapes.Lines;

namespace GraphicsComposerLib.Geometry.Primitives.Lines
{
    public interface IGraphicsLineGeometry3D 
        : IGraphicsPrimitiveGeometry3D<ILineSegment3D>
    {
        IEnumerable<Pair<ITuple3D>> LineVertexPoints { get; }

        IEnumerable<Pair<int>> LineVertexIndices { get; }
    }
}