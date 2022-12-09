using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D
{
    public sealed class LinearPointsPath3D
        : PSeqMapped1D<double, IFloat64Tuple3D>, IPointsPath3D
    {
        public IFloat64Tuple3D Point1 { get; set; }

        public IFloat64Tuple3D Point2 { get; set; }


        public LinearPointsPath3D(IFloat64Tuple3D point1, IFloat64Tuple3D point2, int count)
            : base(new PSeqLinearDouble1D(0, 1, count, 0, 0))
        {
            Point1 = point1;
            Point2 = point2;
        }

        public LinearPointsPath3D(IFloat64Tuple3D point1, IFloat64Tuple3D point2, IPeriodicSequence1D<double> parameterSequence)
            : base(parameterSequence)
        {
            Point1 = point1;
            Point2 = point2;
        }


        protected override IFloat64Tuple3D MappingFunction(double t)
        {
            var s = 1 - t;

            return new Float64Tuple3D(
                s * Point1.X + t * Point2.X,
                s * Point1.Y + t * Point2.Y,
                s * Point1.Z + t * Point2.Z
            );
        }
    }
}
