using System;
using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D
{
    public sealed class ParametricPointsPath3D 
        : PSeqMapped1D<double, IFloat64Tuple3D>, IPointsPath3D
    {
        public Func<double, IFloat64Tuple3D> Mapping { get; set; }


        public ParametricPointsPath3D(IPeriodicSequence1D<double> parameterSequence)
            : base(parameterSequence)
        {
        }

        public ParametricPointsPath3D(IPeriodicSequence1D<double> parameterSequence, Func<double, IFloat64Tuple3D> mapping)
            : base(parameterSequence)
        {
            Mapping = mapping;
        }


        protected override IFloat64Tuple3D MappingFunction(double input)
        {
            return Mapping(input);
        }
    }
}