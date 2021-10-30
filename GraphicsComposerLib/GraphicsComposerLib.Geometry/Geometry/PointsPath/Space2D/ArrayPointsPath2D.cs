using System.Collections.Generic;
using DataStructuresLib.Sequences.Periodic1D;
using EuclideanGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Geometry.PointsPath.Space2D
{
    /// <summary>
    /// A path where points are directly stored in memory as an array
    /// </summary>
    public sealed class ArrayPointsPath2D
        : PSeqArray1D<ITuple2D>, IPointsPath2D
    {
        public ArrayPointsPath2D(int pointsCount)
            : base(pointsCount)
        {
        }

        public ArrayPointsPath2D(params ITuple2D[] pointsArray)
            : base(pointsArray)
        {
        }

        public ArrayPointsPath2D(IEnumerable<ITuple2D> pointsList)
            : base(pointsList)
        {
        }
    }
}