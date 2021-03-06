using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D
{
    public sealed class PlanarYzPointsPath3D
        : PSeqMapped1D<ITuple2D, ITuple3D>, IPointsPath3D
    {
        public double ValueX { get; set; }


        public PlanarYzPointsPath3D(IPointsPath2D xyPath)
            : base(xyPath)
        {
            ValueX = 0;
        }

        public PlanarYzPointsPath3D(IPointsPath2D xyPath, double valueX)
            : base(xyPath)
        {
            ValueX = valueX;
        }


        protected override ITuple3D MappingFunction(ITuple2D yzPoint)
        {
            return new Tuple3D(
                ValueX,
                yzPoint.X,
                yzPoint.Y
            );
        }
    }
}