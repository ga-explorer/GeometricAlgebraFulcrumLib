using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space3D
{
    public sealed class PlanarZxPointsPath3D : 
        PSeqMapped1D<IFloat64Vector2D, IFloat64Vector3D>, 
        IPointsPath3D
    {
        public double ValueY { get; set; }


        public PlanarZxPointsPath3D(IPointsPath2D zxPath)
            : base(zxPath)
        {
            ValueY = 0;
        }

        public PlanarZxPointsPath3D(IPointsPath2D zxPath, double valueX)
            : base(zxPath)
        {
            ValueY = valueX;
        }


        protected override IFloat64Vector3D MappingFunction(IFloat64Vector2D zxPoint)
        {
            return Float64Vector3D.Create(
                zxPoint.Y.Value,
                ValueY,
                zxPoint.X.Value
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