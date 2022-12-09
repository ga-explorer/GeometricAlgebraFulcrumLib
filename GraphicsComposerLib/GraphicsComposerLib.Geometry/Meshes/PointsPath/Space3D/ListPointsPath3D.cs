using System.Collections.Generic;
using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D
{
    /// <summary>
    /// A path where points are directly stored in memory as a list
    /// </summary>
    public sealed class ListPointsPath3D
        : PSeqReadOnlyList1D<IFloat64Tuple3D>, IPointsPath3D
    {
        public ListPointsPath3D(IReadOnlyList<IFloat64Tuple3D> pointsList)
            : base(pointsList)
        {
        }

        public ListPointsPath3D(params IFloat64Tuple3D[] pointsArray)
            : base(pointsArray)
        {
        }

        public ListPointsPath3D(IEnumerable<IFloat64Tuple3D> pointsList)
            : base(pointsList)
        {
        }
    }
}