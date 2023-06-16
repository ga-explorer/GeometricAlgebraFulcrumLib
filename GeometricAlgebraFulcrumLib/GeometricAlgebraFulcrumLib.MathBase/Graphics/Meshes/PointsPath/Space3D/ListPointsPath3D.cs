using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space3D
{
    /// <summary>
    /// A path where points are directly stored in memory as a list
    /// </summary>
    public sealed class ListPointsPath3D : 
        PSeqReadOnlyList1D<IFloat64Tuple3D>, 
        IPointsPath3D
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


        public bool IsValid()
        {
            return DataList.Count >= 2 &&
                   DataList.All(p => p.IsValid());
        }
        
        public IPointsPath3D MapPoints(Func<IFloat64Tuple3D, IFloat64Tuple3D> pointMapping)
        {
            return new ListPointsPath3D(this.Select(pointMapping));
        }
    }
}