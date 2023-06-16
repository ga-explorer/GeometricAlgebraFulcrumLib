using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space3D
{
    public sealed class PlanarYxPointsPath3D : 
        PSeqMapped1D<IFloat64Tuple2D, IFloat64Tuple3D>, 
        IPointsPath3D
    {
        public double ValueZ { get; set; }


        public PlanarYxPointsPath3D(IPointsPath2D yxPath)
            : base(yxPath)
        {
            ValueZ = 0;
        }

        public PlanarYxPointsPath3D(IPointsPath2D yxPath, double valueZ)
            : base(yxPath)
        {
            ValueZ = valueZ;
        }


        protected override IFloat64Tuple3D MappingFunction(IFloat64Tuple2D yxPoint)
        {
            if (ReferenceEquals(yxPoint, null))
                throw new ArgumentNullException(nameof(yxPoint));

            return Float64Vector3D.Create(yxPoint.Y,
                yxPoint.X,
                ValueZ);
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