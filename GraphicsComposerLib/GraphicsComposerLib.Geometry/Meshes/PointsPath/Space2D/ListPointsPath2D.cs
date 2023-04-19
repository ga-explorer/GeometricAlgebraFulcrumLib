using System.Collections.Generic;
using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space2D
{
    /// <summary>
    /// A path where points are directly stored in memory as a list
    /// </summary>
    public sealed class ListPointsPath2D
        : PSeqReadOnlyList1D<IFloat64Tuple2D>, IPointsPath2D
    {
        public ListPointsPath2D(params IFloat64Tuple2D[] pointsArray)
            : base(pointsArray)
        {
        }

        public ListPointsPath2D(IReadOnlyList<IFloat64Tuple2D> pointsArray)
            : base(pointsArray)
        {
        }

        public ListPointsPath2D(IEnumerable<IFloat64Tuple2D> pointsList)
            : base(pointsList)
        {
        }
    }
}