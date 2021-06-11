using System.Collections.Generic;
using DataStructuresLib.Basic;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicShapes.Lines;

namespace EuclideanGeometryLib.GraphicsGeometry.Lines
{
    public interface IGraphicsLinesGeometry3D 
        : IGraphicsGeometry3D<ILineSegment3D>
    {
        IReadOnlyList<Pair<ITuple3D>> LineVerticesPoints { get; }

        IReadOnlyList<Pair<int>> LineVerticesIndices { get; }
    }
}