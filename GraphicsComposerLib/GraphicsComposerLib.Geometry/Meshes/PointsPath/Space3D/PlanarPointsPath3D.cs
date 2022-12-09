using System;
using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D
{
    public sealed class PlanarPointsPath3D
        : PSeqMapped1D<IFloat64Tuple2D, IFloat64Tuple3D>, IPointsPath3D
    {
        public IFloat64Tuple3D Origin { get; set; }

        public IFloat64Tuple3D Direction1 { get; set; }

        public IFloat64Tuple3D Direction2 { get; set; }


        public PlanarPointsPath3D(IPointsPath2D basePath) 
            : base(basePath)
        {
            Origin = new Float64Tuple3D(0, 0, 0);
            Direction1 = new Float64Tuple3D(1, 0, 0);
            Direction2 = new Float64Tuple3D(0, 1, 0);
        }


        protected override IFloat64Tuple3D MappingFunction(IFloat64Tuple2D pointUv)
        {
            if (ReferenceEquals(pointUv, null))
                throw new ArgumentNullException(nameof(pointUv));

            return new Float64Tuple3D(
                Origin.X + pointUv.X * Direction1.X + pointUv.Y * Direction2.X,
                Origin.Y + pointUv.X * Direction1.Y + pointUv.Y * Direction2.Y,
                Origin.Z + pointUv.X * Direction1.Z + pointUv.Y * Direction2.Z
            );
        }
    }
}