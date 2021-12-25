using System;
using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space2D
{
    public sealed class MappedPointsPath2D
        : PSeqMapped1D<ITuple2D>, IPointsPath2D
    {
        public Func<ITuple2D, ITuple2D> Mapping { get; set; }


        public MappedPointsPath2D(IPointsPath2D basePath, Func<ITuple2D, ITuple2D> mapping)
            : base(basePath)
        {
            Mapping = mapping;
        }


        protected override ITuple2D MappingFunction(ITuple2D input)
        {
            return Mapping(input);
        }
    }
}