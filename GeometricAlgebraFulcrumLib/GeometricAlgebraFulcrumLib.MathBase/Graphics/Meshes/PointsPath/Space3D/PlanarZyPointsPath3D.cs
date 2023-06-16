using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space3D
{
    public sealed class PlanarZyPointsPath3D : 
        PSeqMapped1D<IFloat64Tuple2D, IFloat64Tuple3D>, 
        IPointsPath3D
    {
        public double ValueX { get; set; }


        public PlanarZyPointsPath3D(IPointsPath2D zyPath)
            : base(zyPath)
        {
            ValueX = 0;
        }

        public PlanarZyPointsPath3D(IPointsPath2D zyPath, double valueX)
            : base(zyPath)
        {
            ValueX = valueX;
        }


        protected override IFloat64Tuple3D MappingFunction(IFloat64Tuple2D zyPoint)
        {
            return Float64Vector3D.Create(ValueX,
                zyPoint.Y,
                zyPoint.X);
        }


        public bool IsValid()
        {
            return this.All(p => p.IsValid());
        }

        public IPointsPath3D MapPoints(Func<IFloat64Tuple3D, IFloat64Tuple3D> pointMapping)
        {
            return new ArrayPointsPath3D(
                this.Select(pointMapping).ToArray()
            );
        }
    }
}