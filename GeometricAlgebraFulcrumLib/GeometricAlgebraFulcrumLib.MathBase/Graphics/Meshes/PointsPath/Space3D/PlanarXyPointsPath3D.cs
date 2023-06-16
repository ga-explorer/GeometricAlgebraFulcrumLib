using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space3D
{
    public sealed class PlanarXyPointsPath3D : 
        PSeqMapped1D<IFloat64Tuple2D, IFloat64Tuple3D>, 
        IPointsPath3D
    {
        public double ValueZ { get; set; }


        public PlanarXyPointsPath3D(IPointsPath2D xyPath)
            : base(xyPath)
        {
            ValueZ = 0;
        }

        public PlanarXyPointsPath3D(IPointsPath2D xyPath, double valueZ)
            : base(xyPath)
        {
            ValueZ = valueZ;
        }


        protected override IFloat64Tuple3D MappingFunction(IFloat64Tuple2D xyPoint)
        {
            return Float64Vector3D.Create(xyPoint.X,
                xyPoint.Y,
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