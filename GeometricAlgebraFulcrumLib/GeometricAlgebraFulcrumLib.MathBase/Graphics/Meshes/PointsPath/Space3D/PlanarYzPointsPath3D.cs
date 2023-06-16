using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space3D
{
    public sealed class PlanarYzPointsPath3D : 
        PSeqMapped1D<IFloat64Tuple2D, IFloat64Tuple3D>, 
        IPointsPath3D
    {
        public double ValueX { get; set; }


        public PlanarYzPointsPath3D(IPointsPath2D xyPath)
            : base(xyPath)
        {
            ValueX = 0;
        }

        public PlanarYzPointsPath3D(IPointsPath2D xyPath, double valueX)
            : base(xyPath)
        {
            ValueX = valueX;
        }


        protected override IFloat64Tuple3D MappingFunction(IFloat64Tuple2D yzPoint)
        {
            return Float64Vector3D.Create(ValueX,
                yzPoint.X,
                yzPoint.Y);
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