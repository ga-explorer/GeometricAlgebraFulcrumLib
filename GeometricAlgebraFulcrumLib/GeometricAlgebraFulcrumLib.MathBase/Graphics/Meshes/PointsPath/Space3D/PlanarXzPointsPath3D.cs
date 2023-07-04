using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space3D
{
    public sealed class PlanarXzPointsPath3D : 
        PSeqMapped1D<IFloat64Vector2D, IFloat64Vector3D>, 
        IPointsPath3D
    {
        public double ValueY { get; set; }


        public PlanarXzPointsPath3D(IPointsPath2D zxPath)
            : base(zxPath)
        {
            ValueY = 0;
        }

        public PlanarXzPointsPath3D(IPointsPath2D xzPath, double valueY)
            : base(xzPath)
        {
            ValueY = valueY;
        }


        protected override IFloat64Vector3D MappingFunction(IFloat64Vector2D xzPoint)
        {
            return Float64Vector3D.Create(
                xzPoint.X.Value,
                ValueY,
                xzPoint.Y.Value
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