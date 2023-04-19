using System.Collections.Generic;
using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space2D
{
    /// <summary>
    /// A path where points are directly stored in memory as an array
    /// </summary>
    public sealed class ArrayPointsPath2D
        : PSeqArray1D<IFloat64Tuple2D>, IPointsPath2D
    {
        public ArrayPointsPath2D(int pointsCount)
            : base(pointsCount)
        {
        }

        public ArrayPointsPath2D(params IFloat64Tuple2D[] pointsArray)
            : base(pointsArray)
        {
        }

        public ArrayPointsPath2D(IEnumerable<IFloat64Tuple2D> pointsList)
            : base(pointsList)
        {
        }
    }
}