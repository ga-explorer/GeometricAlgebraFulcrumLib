using System;
using DataStructuresLib.Sequences.Periodic1D;
using EuclideanGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Geometry.PointsPath.Space3D
{
    public sealed class ParametricPointsPath3D 
        : PSeqMapped1D<double, ITuple3D>, IPointsPath3D
    {
        public Func<double, ITuple3D> Mapping { get; set; }


        public ParametricPointsPath3D(IPeriodicSequence1D<double> parameterSequence)
            : base(parameterSequence)
        {
        }

        public ParametricPointsPath3D(IPeriodicSequence1D<double> parameterSequence, Func<double, ITuple3D> mapping)
            : base(parameterSequence)
        {
            Mapping = mapping;
        }


        protected override ITuple3D MappingFunction(double input)
        {
            return Mapping(input);
        }
    }
}