using System.Collections.Generic;
using DataStructuresLib.Sequences.Periodic1D;
using EuclideanGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Geometry.PointsPath.Space3D
{
    /// <summary>
    /// A path where points are directly stored in memory as an array
    /// </summary>
    public sealed class ArrayPointsPath3D
        : PSeqArray1D<ITuple3D>, IPointsPath3D
    {
        public ArrayPointsPath3D(int pointsCount)
            : base(pointsCount)
        {
        }

        public ArrayPointsPath3D(params ITuple3D[] pointsArray)
            : base(pointsArray)
        {
        }

        public ArrayPointsPath3D(IEnumerable<ITuple3D> pointsList)
            : base(pointsList)
        {
        }
    }
}
