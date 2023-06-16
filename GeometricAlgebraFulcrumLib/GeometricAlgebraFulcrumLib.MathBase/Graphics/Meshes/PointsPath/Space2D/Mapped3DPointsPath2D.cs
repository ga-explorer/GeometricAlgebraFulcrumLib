using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space2D
{
    public sealed class Mapped3DPointsPath2D
        : PSeqMapped1D<IFloat64Tuple3D, IFloat64Tuple2D>, IPointsPath2D
    {
        public Func<IFloat64Tuple3D, IFloat64Tuple2D> Mapping { get; set; }


        public Mapped3DPointsPath2D(IPointsPath3D basePath, Func<IFloat64Tuple3D, IFloat64Tuple2D> mapping)
            : base(basePath)
        {
            Mapping = mapping;
        }


        protected override IFloat64Tuple2D MappingFunction(IFloat64Tuple3D input)
        {
            return Mapping(input);
        }

        
        public bool IsValid()
        {
            return this.All(p => p.IsValid());
        }
        
        public IPointsPath2D MapPoints(Func<IFloat64Tuple2D, IFloat64Tuple2D> pointMapping)
        {
            return new ArrayPointsPath2D(
                this.Select(pointMapping).ToArray()
            );
        }
    }
}