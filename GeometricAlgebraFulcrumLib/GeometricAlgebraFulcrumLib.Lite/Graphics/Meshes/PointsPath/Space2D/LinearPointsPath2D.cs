using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space2D
{
    public sealed class LinearPointsPath2D
        : PSeqMapped1D<double, IFloat64Vector2D>, IPointsPath2D
    {
        public IFloat64Vector2D Point1 { get; }

        public IFloat64Vector2D Point2 { get; }


        public LinearPointsPath2D(IFloat64Vector2D point1, IFloat64Vector2D point2, int count)
            : base(new PSeqLinearDouble1D(0, 1, count, 0, 0))
        {
            Point1 = point1;
            Point2 = point2;
        }

        public LinearPointsPath2D(IFloat64Vector2D point1, IFloat64Vector2D point2, IPeriodicSequence1D<double> parameterSequence)
            : base(parameterSequence)
        {
            Point1 = point1;
            Point2 = point2;
        }


        protected override IFloat64Vector2D MappingFunction(double t)
        {
            var s = 1 - t;

            return Float64Vector2D.Create(s * Point1.X + t * Point2.X,
                s * Point1.Y + t * Point2.Y);
        }
        
        public bool IsValid()
        {
            return Point1.IsValid() &&
                   Point2.IsValid();
        }
        
        public IPointsPath2D MapPoints(Func<IFloat64Vector2D, IFloat64Vector2D> pointMapping)
        {
            return new ArrayPointsPath2D(this.Select(pointMapping));
        }
    }
}