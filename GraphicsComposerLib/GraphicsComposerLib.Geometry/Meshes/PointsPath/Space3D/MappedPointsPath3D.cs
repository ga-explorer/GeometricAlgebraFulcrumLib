using System;
using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D
{
    public sealed class MappedPointsPath3D
        : PSeqMapped1D<IFloat64Tuple3D>, IPointsPath3D
    {
        public Func<IFloat64Tuple3D, IFloat64Tuple3D> Mapping { get; set; }


        public MappedPointsPath3D(IPointsPath3D basePath)
            : base(basePath)
        {
            Mapping = (point => point);
        }

        public MappedPointsPath3D(IPointsPath3D basePath, Func<IFloat64Tuple3D, IFloat64Tuple3D> mapping)
            : base(basePath)
        {
            Mapping = mapping;
        }


        protected override IFloat64Tuple3D MappingFunction(IFloat64Tuple3D input)
        {
            return Mapping(input);
        }
    }
}