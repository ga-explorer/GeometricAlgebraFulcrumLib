using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space2D
{
    public sealed class MappedPointsPath2D
        : PSeqMapped1D<IFloat64Tuple2D>, IPointsPath2D
    {
        public IPointsPath2D BasePath { get; }

        public Func<IFloat64Tuple2D, IFloat64Tuple2D> Mapping { get; }


        public MappedPointsPath2D(IPointsPath2D basePath, Func<IFloat64Tuple2D, IFloat64Tuple2D> mapping)
            : base(basePath)
        {
            BasePath = basePath;
            Mapping = mapping;
        }


        protected override IFloat64Tuple2D MappingFunction(IFloat64Tuple2D input)
        {
            return Mapping(input);
        }

        
        public bool IsValid()
        {
            return this.All(p => p.IsValid());
        }
        
        public IPointsPath2D MapPoints(Func<IFloat64Tuple2D, IFloat64Tuple2D> pointMapping)
        {
            return new MappedPointsPath2D(
                BasePath,
                p => pointMapping(Mapping(p))
            );
        }
    }
}