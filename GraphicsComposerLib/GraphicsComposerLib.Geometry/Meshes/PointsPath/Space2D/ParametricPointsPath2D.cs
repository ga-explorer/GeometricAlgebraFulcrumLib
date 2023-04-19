using System;
using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space2D
{
    public sealed class ParametricPointsPath2D 
        : PSeqMapped1D<double, IFloat64Tuple2D>, IPointsPath2D
    {
        public Func<double, IFloat64Tuple2D> Mapping { get; set; }


        public ParametricPointsPath2D(IPeriodicSequence1D<double> parameterSequence)
            : base(parameterSequence)
        {
        }

        public ParametricPointsPath2D(IPeriodicSequence1D<double> parameterSequence, Func<double, IFloat64Tuple2D> mapping)
            : base(parameterSequence)
        {
            Mapping = mapping;
        }


        protected override IFloat64Tuple2D MappingFunction(double input)
        {
            return Mapping(input);
        }
    }
}