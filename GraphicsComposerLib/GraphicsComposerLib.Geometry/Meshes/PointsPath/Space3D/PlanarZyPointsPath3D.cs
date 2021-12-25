using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D
{
    public sealed class PlanarZyPointsPath3D
        : PSeqMapped1D<ITuple2D, ITuple3D>, IPointsPath3D
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


        protected override ITuple3D MappingFunction(ITuple2D zyPoint)
        {
            return new Tuple3D(
                ValueX,
                zyPoint.Y,
                zyPoint.X
            );
        }
    }
}