using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D
{
    public sealed class PlanarXzPointsPath3D : 
        PSeqMapped1D<IFloat64Tuple2D, IFloat64Tuple3D>, 
        IPointsPath3D
    {
        public double ValueY { get; set; }


        public PlanarXzPointsPath3D(IPointsPath2D zxPath)
            : base(zxPath)
        {
            ValueY = 0;
        }

        public PlanarXzPointsPath3D(IPointsPath2D xzPath, double valueY)
            : base(xzPath)
        {
            ValueY = valueY;
        }


        protected override IFloat64Tuple3D MappingFunction(IFloat64Tuple2D xzPoint)
        {
            return new Float64Tuple3D(
                xzPoint.X,
                ValueY,
                xzPoint.Y
            );
        }
    }
}