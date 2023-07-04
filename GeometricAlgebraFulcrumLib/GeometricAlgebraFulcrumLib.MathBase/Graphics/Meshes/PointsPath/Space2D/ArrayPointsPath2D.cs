using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space2D
{
    /// <summary>
    /// A path where points are directly stored in memory as an array
    /// </summary>
    public sealed class ArrayPointsPath2D
        : PSeqArray1D<IFloat64Vector2D>, IPointsPath2D
    {
        public ArrayPointsPath2D(int pointsCount)
            : base(pointsCount)
        {
        }

        public ArrayPointsPath2D(params IFloat64Vector2D[] pointsArray)
            : base(pointsArray)
        {
        }

        public ArrayPointsPath2D(IEnumerable<IFloat64Vector2D> pointsList)
            : base(pointsList)
        {
        }


        public bool IsValid()
        {
            return this.All(p => p.IsValid());
        }
        
        public IPointsPath2D MapPoints(Func<IFloat64Vector2D, IFloat64Vector2D> pointMapping)
        {
            return new ArrayPointsPath2D(
                this.Select(pointMapping).ToArray()
            );
        }
    }
}