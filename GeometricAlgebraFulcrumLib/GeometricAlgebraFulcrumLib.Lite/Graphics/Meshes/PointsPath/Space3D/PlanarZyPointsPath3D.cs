using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space3D
{
    public sealed class PlanarZyPointsPath3D : 
        PSeqMapped1D<IFloat64Vector2D, IFloat64Vector3D>, 
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


        protected override IFloat64Vector3D MappingFunction(IFloat64Vector2D zyPoint)
        {
            return Float64Vector3D.Create(
                ValueX,
                zyPoint.Y.Value,
                zyPoint.X.Value
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