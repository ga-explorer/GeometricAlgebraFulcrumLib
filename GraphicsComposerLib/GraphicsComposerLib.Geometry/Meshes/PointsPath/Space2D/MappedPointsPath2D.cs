using System;
using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space2D
{
    public sealed class MappedPointsPath2D
        : PSeqMapped1D<IFloat64Tuple2D>, IPointsPath2D
    {
        public Func<IFloat64Tuple2D, IFloat64Tuple2D> Mapping { get; set; }


        public MappedPointsPath2D(IPointsPath2D basePath, Func<IFloat64Tuple2D, IFloat64Tuple2D> mapping)
            : base(basePath)
        {
            Mapping = mapping;
        }


        protected override IFloat64Tuple2D MappingFunction(IFloat64Tuple2D input)
        {
            return Mapping(input);
        }
    }
}