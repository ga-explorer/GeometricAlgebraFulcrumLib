using System.Collections.Generic;
using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space2D
{
    /// <summary>
    /// A path where points are directly stored in memory as a list
    /// </summary>
    public sealed class ListPointsPath2D
        : PSeqReadOnlyList1D<ITuple2D>, IPointsPath2D
    {
        public ListPointsPath2D(params ITuple2D[] pointsArray)
            : base(pointsArray)
        {
        }

        public ListPointsPath2D(IReadOnlyList<ITuple2D> pointsArray)
            : base(pointsArray)
        {
        }

        public ListPointsPath2D(IEnumerable<ITuple2D> pointsList)
            : base(pointsList)
        {
        }
    }
}