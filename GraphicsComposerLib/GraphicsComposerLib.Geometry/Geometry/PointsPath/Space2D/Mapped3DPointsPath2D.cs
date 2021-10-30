using System;
using DataStructuresLib.Sequences.Periodic1D;
using EuclideanGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Geometry.PointsPath.Space2D
{
    public sealed class Mapped3DPointsPath2D
        : PSeqMapped1D<ITuple3D, ITuple2D>, IPointsPath2D
    {
        public Func<ITuple3D, ITuple2D> Mapping { get; set; }


        public Mapped3DPointsPath2D(IPointsPath3D basePath, Func<ITuple3D, ITuple2D> mapping)
            : base(basePath)
        {
            Mapping = mapping;
        }


        protected override ITuple2D MappingFunction(ITuple3D input)
        {
            return Mapping(input);
        }
    }
}