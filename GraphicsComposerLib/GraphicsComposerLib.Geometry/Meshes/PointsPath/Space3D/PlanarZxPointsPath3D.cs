using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D
{
    public sealed class PlanarZxPointsPath3D
        : PSeqMapped1D<IFloat64Tuple2D, IFloat64Tuple3D>, IPointsPath3D
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


        protected override IFloat64Tuple3D MappingFunction(IFloat64Tuple2D zxPoint)
        {
            return new Float64Tuple3D(
                zxPoint.Y,
                ValueY,
                zxPoint.X
            );
        }
    }
}