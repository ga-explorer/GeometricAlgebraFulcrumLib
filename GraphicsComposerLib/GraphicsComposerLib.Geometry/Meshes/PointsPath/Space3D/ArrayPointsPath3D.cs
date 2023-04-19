using System.Collections.Generic;
using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D
{
    /// <summary>
    /// A path where points are directly stored in memory as an array
    /// </summary>
    public sealed class ArrayPointsPath3D
        : PSeqArray1D<IFloat64Tuple3D>, IPointsPath3D
    {
        public ArrayPointsPath3D(int pointsCount)
            : base(pointsCount)
        {
        }

        public ArrayPointsPath3D(params IFloat64Tuple3D[] pointsArray)
            : base(pointsArray)
        {
        }

        public ArrayPointsPath3D(IEnumerable<IFloat64Tuple3D> pointsList)
            : base(pointsList)
        {
        }
    }
}
