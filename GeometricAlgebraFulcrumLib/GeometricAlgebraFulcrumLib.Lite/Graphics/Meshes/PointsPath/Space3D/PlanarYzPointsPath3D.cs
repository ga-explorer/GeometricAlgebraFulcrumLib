using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space3D
{
    public sealed class PlanarYzPointsPath3D : 
        PSeqMapped1D<IFloat64Vector2D, IFloat64Vector3D>, 
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


        protected override IFloat64Vector3D MappingFunction(IFloat64Vector2D yzPoint)
        {
            return Float64Vector3D.Create(
                ValueX,
                yzPoint.X.Value,
                yzPoint.Y.Value
            );
        }

        public bool IsValid()
        {
            return this.All(p => p.IsValid());
        }

        public IPointsPath3D MapPoints(Func<IFloat64Vector3D, IFloat64Vector3D> pointMapping)
        {
            return new ArrayPointsPath3D(
                this.Select(pointMapping).ToArray()
            );
        }
    }
}