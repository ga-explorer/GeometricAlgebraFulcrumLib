using System.Collections.Generic;
using DataStructuresLib.Sequences.Periodic1D;
using EuclideanGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Geometry.PointsPath.Space3D
{
    /// <summary>
    /// A path where points are directly stored in memory as a list
    /// </summary>
    public sealed class ListPointsPath3D
        : PSeqReadOnlyList1D<ITuple3D>, IPointsPath3D
    {
        public ListPointsPath3D(IReadOnlyList<ITuple3D> pointsList)
            : base(pointsList)
        {
        }

        public ListPointsPath3D(params ITuple3D[] pointsArray)
            : base(pointsArray)
        {
        }

        public ListPointsPath3D(IEnumerable<ITuple3D> pointsList)
            : base(pointsList)
        {
        }
    }
}