using System;
using DataStructuresLib.Sequences.Periodic1D;
using EuclideanGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Geometry.PointsPath.Space3D
{
    public sealed class Mapped2DPointsPath3D
        : PSeqMapped1D<ITuple2D, ITuple3D>, IPointsPath3D
    {
        public Func<ITuple2D, ITuple3D> Mapping { get; set; }


        public Mapped2DPointsPath3D(IPointsPath2D basePath, Func<ITuple2D, ITuple3D> mapping)
            : base(basePath)
        {
            Mapping = mapping;
        }


        protected override ITuple3D MappingFunction(ITuple2D input)
        {
            return Mapping(input);
        }
    }
}