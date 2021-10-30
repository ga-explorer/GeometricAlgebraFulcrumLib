using DataStructuresLib.Sequences.Periodic1D;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Geometry.PointsPath.Space3D
{
    public sealed class PlanarZxPointsPath3D
        : PSeqMapped1D<ITuple2D, ITuple3D>, IPointsPath3D
    {
        public double ValueY { get; set; }


        public PlanarZxPointsPath3D(IPointsPath2D zxPath)
            : base(zxPath)
        {
            ValueY = 0;
        }

        public PlanarZxPointsPath3D(IPointsPath2D zxPath, double valueX)
            : base(zxPath)
        {
            ValueY = valueX;
        }


        protected override ITuple3D MappingFunction(ITuple2D zxPoint)
        {
            return new Tuple3D(
                zxPoint.Y,
                ValueY,
                zxPoint.X
            );
        }
    }
}