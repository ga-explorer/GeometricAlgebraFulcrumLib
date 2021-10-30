using System;
using DataStructuresLib.Sequences.Periodic1D;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Geometry.PointsPath.Space3D
{
    public sealed class PlanarPointsPath3D
        : PSeqMapped1D<ITuple2D, ITuple3D>, IPointsPath3D
    {
        public ITuple3D Origin { get; set; }

        public ITuple3D Direction1 { get; set; }

        public ITuple3D Direction2 { get; set; }


        public PlanarPointsPath3D(IPointsPath2D basePath) 
            : base(basePath)
        {
            Origin = new Tuple3D(0, 0, 0);
            Direction1 = new Tuple3D(1, 0, 0);
            Direction2 = new Tuple3D(0, 1, 0);
        }


        protected override ITuple3D MappingFunction(ITuple2D pointUv)
        {
            if (ReferenceEquals(pointUv, null))
                throw new ArgumentNullException(nameof(pointUv));

            return new Tuple3D(
                Origin.X + pointUv.X * Direction1.X + pointUv.Y * Direction2.X,
                Origin.Y + pointUv.X * Direction1.Y + pointUv.Y * Direction2.Y,
                Origin.Z + pointUv.X * Direction1.Z + pointUv.Y * Direction2.Z
            );
        }
    }
}