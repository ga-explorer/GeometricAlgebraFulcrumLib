using System;
using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D
{
    public sealed class PlanarYxPointsPath3D
        : PSeqMapped1D<IFloat64Tuple2D, IFloat64Tuple3D>, IPointsPath3D
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


        protected override IFloat64Tuple3D MappingFunction(IFloat64Tuple2D yxPoint)
        {
            if (ReferenceEquals(yxPoint, null))
                throw new ArgumentNullException(nameof(yxPoint));

            return new Float64Tuple3D(
                yxPoint.Y,
                yxPoint.X,
                ValueZ
            );
        }
    }
}