using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space2D
{
    public sealed class LinearPointsPath2D
        : PSeqMapped1D<double, ITuple2D>, IPointsPath2D
    {
        public ITuple2D Point1 { get; }

        public ITuple2D Point2 { get; }


        public LinearPointsPath2D(ITuple2D point1, ITuple2D point2, int count)
            : base(new PSeqLinearDouble1D(0, 1, count, 0, 0))
        {
            Point1 = point1;
            Point2 = point2;
        }

        public LinearPointsPath2D(ITuple2D point1, ITuple2D point2, IPeriodicSequence1D<double> parameterSequence)
            : base(parameterSequence)
        {
            Point1 = point1;
            Point2 = point2;
        }


        protected override ITuple2D MappingFunction(double t)
        {
            var s = 1 - t;

            return new Tuple2D(
                s * Point1.X + t * Point2.X,
                s * Point1.Y + t * Point2.Y
            );
        }
    }
}