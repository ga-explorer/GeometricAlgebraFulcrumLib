using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space3D
{
    public sealed class PlanarPointsPath3D : 
        PSeqMapped1D<IFloat64Vector2D, IFloat64Vector3D>, 
        IPointsPath3D
    {
        public IFloat64Vector3D Origin { get; set; }

        public IFloat64Vector3D Direction1 { get; set; }

        public IFloat64Vector3D Direction2 { get; set; }


        public PlanarPointsPath3D(IPointsPath2D basePath) 
            : base(basePath)
        {
            Origin = Float64Vector3D.Create(0, 0, 0);
            Direction1 = Float64Vector3D.Create(1, 0, 0);
            Direction2 = Float64Vector3D.Create(0, 1, 0);
        }


        protected override IFloat64Vector3D MappingFunction(IFloat64Vector2D pointUv)
        {
            if (ReferenceEquals(pointUv, null))
                throw new ArgumentNullException(nameof(pointUv));

            return Float64Vector3D.Create(Origin.X + pointUv.X * Direction1.X + pointUv.Y * Direction2.X,
                Origin.Y + pointUv.X * Direction1.Y + pointUv.Y * Direction2.Y,
                Origin.Z + pointUv.X * Direction1.Z + pointUv.Y * Direction2.Z);
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