using System;
using DataStructuresLib.Sequences.Periodic1D;
using EuclideanGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Geometry.PointsPath.Space3D
{
    public sealed class MappedPointsPath3D
        : PSeqMapped1D<ITuple3D>, IPointsPath3D
    {
        public Func<ITuple3D, ITuple3D> Mapping { get; set; }


        public MappedPointsPath3D(IPointsPath3D basePath)
            : base(basePath)
        {
            Mapping = (point => point);
        }

        public MappedPointsPath3D(IPointsPath3D basePath, Func<ITuple3D, ITuple3D> mapping)
            : base(basePath)
        {
            Mapping = mapping;
        }


        protected override ITuple3D MappingFunction(ITuple3D input)
        {
            return Mapping(input);
        }
    }
}