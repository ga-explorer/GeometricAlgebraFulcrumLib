using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space2D
{
    public sealed class LinearPointsPath2D
        : PSeqMapped1D<double, IFloat64Tuple2D>, IPointsPath2D
    {
        public IFloat64Tuple2D Point1 { get; }

        public IFloat64Tuple2D Point2 { get; }


        public LinearPointsPath2D(IFloat64Tuple2D point1, IFloat64Tuple2D point2, int count)
            : base(new PSeqLinearDouble1D(0, 1, count, 0, 0))
        {
            Point1 = point1;
            Point2 = point2;
        }

        public LinearPointsPath2D(IFloat64Tuple2D point1, IFloat64Tuple2D point2, IPeriodicSequence1D<double> parameterSequence)
            : base(parameterSequence)
        {
            Point1 = point1;
            Point2 = point2;
        }


        protected override IFloat64Tuple2D MappingFunction(double t)
        {
            var s = 1 - t;

            return new Float64Tuple2D(
                s * Point1.X + t * Point2.X,
                s * Point1.Y + t * Point2.Y
            );
        }
    }
}