using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D
{
    public sealed class PlanarZyPointsPath3D
        : PSeqMapped1D<IFloat64Tuple2D, IFloat64Tuple3D>, IPointsPath3D
    {
        public double ValueX { get; set; }


        public PlanarZyPointsPath3D(IPointsPath2D zyPath)
            : base(zyPath)
        {
            ValueX = 0;
        }

        public PlanarZyPointsPath3D(IPointsPath2D zyPath, double valueX)
            : base(zyPath)
        {
            ValueX = valueX;
        }


        protected override IFloat64Tuple3D MappingFunction(IFloat64Tuple2D zyPoint)
        {
            return new Float64Tuple3D(
                ValueX,
                zyPoint.Y,
                zyPoint.X
            );
        }
    }
}