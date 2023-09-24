using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space2D
{
    /// <summary>
    /// A path where points are directly stored in memory as a list
    /// </summary>
    public sealed class ListPointsPath2D
        : PSeqReadOnlyList1D<IFloat64Vector2D>, IPointsPath2D
    {
        public ListPointsPath2D(params IFloat64Vector2D[] pointsArray)
            : base(pointsArray)
        {
        }

        public ListPointsPath2D(IReadOnlyList<IFloat64Vector2D> pointsArray)
            : base(pointsArray)
        {
        }

        public ListPointsPath2D(IEnumerable<IFloat64Vector2D> pointsList)
            : base(pointsList)
        {
        }
        

        public bool IsValid()
        {
            return DataList.Count >= 2 &&
                   DataList.All(p => p.IsValid());
        }
        
        public IPointsPath2D MapPoints(Func<IFloat64Vector2D, IFloat64Vector2D> pointMapping)
        {
            return new ListPointsPath2D(this.Select(pointMapping));
        }
    }
}