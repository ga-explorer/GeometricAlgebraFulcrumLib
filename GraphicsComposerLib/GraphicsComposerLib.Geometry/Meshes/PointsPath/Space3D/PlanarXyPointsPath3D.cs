using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D
{
    public sealed class PlanarXyPointsPath3D
        : PSeqMapped1D<IFloat64Tuple2D, IFloat64Tuple3D>, IPointsPath3D
    {
        public double ValueZ { get; set; }


        public PlanarXyPointsPath3D(IPointsPath2D xyPath)
            : base(xyPath)
        {
            ValueZ = 0;
        }

        public PlanarXyPointsPath3D(IPointsPath2D xyPath, double valueZ)
            : base(xyPath)
        {
            ValueZ = valueZ;
        }


        protected override IFloat64Tuple3D MappingFunction(IFloat64Tuple2D xyPoint)
        {
            return new Float64Tuple3D(
                xyPoint.X,
                xyPoint.Y,
                ValueZ
            );
        }
    }
}