using DataStructuresLib.BitManipulation;
using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space3D
{
    /// <summary>
    /// A path where points are directly stored in memory as an array
    /// </summary>
    public sealed class ArrayPointsPath3D : 
        PSeqArray1D<IFloat64Vector3D>, 
        IPointsPath3D
    {
        public ArrayPointsPath3D(int pointsCount)
            : base(pointsCount.Repeat(Float64Vector3D.Zero))
        {
        }

        public ArrayPointsPath3D(params IFloat64Vector3D[] pointsArray)
            : base(pointsArray)
        {
        }

        public ArrayPointsPath3D(IEnumerable<IFloat64Vector3D> pointsList)
            : base(pointsList)
        {
        }


        public bool IsValid()
        {
            return this.All(p => p.IsValid());
        }

        public IPointsPath3D MapPoints(Func<IFloat64Vector3D, IFloat64Vector3D> pointMapping)
        {
            return new ArrayPointsPath3D(
                DataArray.Select(pointMapping).ToArray()
            );
        }
    }
}
