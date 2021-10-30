using System;
using DataStructuresLib.Sequences.Periodic1D;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Geometry.PointsPath.Space3D
{
    public sealed class PlanarYxPointsPath3D
        : PSeqMapped1D<ITuple2D, ITuple3D>, IPointsPath3D
    {
        public double ValueZ { get; set; }


        public PlanarYxPointsPath3D(IPointsPath2D yxPath)
            : base(yxPath)
        {
            ValueZ = 0;
        }

        public PlanarYxPointsPath3D(IPointsPath2D yxPath, double valueZ)
            : base(yxPath)
        {
            ValueZ = valueZ;
        }


        protected override ITuple3D MappingFunction(ITuple2D yxPoint)
        {
            if (ReferenceEquals(yxPoint, null))
                throw new ArgumentNullException(nameof(yxPoint));

            return new Tuple3D(
                yxPoint.Y,
                yxPoint.X,
                ValueZ
            );
        }
    }
}