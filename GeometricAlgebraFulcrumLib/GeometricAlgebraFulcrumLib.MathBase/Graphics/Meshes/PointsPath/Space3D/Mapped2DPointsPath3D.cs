using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space3D
{
    public sealed class Mapped2DPointsPath3D : 
        PSeqMapped1D<IFloat64Tuple2D, IFloat64Tuple3D>, 
        IPointsPath3D
    {
        public Func<IFloat64Tuple2D, IFloat64Tuple3D> Mapping { get; set; }


        public Mapped2DPointsPath3D(IPointsPath2D basePath, Func<IFloat64Tuple2D, IFloat64Tuple3D> mapping)
            : base(basePath)
        {
            Mapping = mapping;
        }


        protected override IFloat64Tuple3D MappingFunction(IFloat64Tuple2D input)
        {
            return Mapping(input);
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