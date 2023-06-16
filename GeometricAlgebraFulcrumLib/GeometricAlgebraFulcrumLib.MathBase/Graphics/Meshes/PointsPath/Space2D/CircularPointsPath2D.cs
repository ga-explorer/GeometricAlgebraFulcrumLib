using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space2D
{
    public sealed class CircularPointsPath2D : 
        PSeqMapped1D<double, IFloat64Tuple2D>, 
        IPointsPath2D
    {
        public IFloat64Tuple2D Center { get; }

        public double Radius { get; }


        public CircularPointsPath2D(IFloat64Tuple2D center, double radius, int count)
            : base(PeriodicSequenceUtils.CreateLinearDoubleSequence(count))
        {
            if (Count < 2)
                throw new ArgumentOutOfRangeException(nameof(count));

            Center = center;
            Radius = radius;
        }

        public CircularPointsPath2D(IFloat64Tuple2D center, double radius, IPeriodicSequence1D<double> parameterSequence)
            : base(parameterSequence)
        {
            Center = center;
            Radius = radius;
        }


        protected override IFloat64Tuple2D MappingFunction(double t)
        {
            var angle = 2 * Math.PI * t;
            var cosAngle = Math.Cos(angle);
            var sinAngle = Math.Sin(angle);

            return new Float64Vector2D(
                Center.X + Radius * cosAngle,
                Center.Y + Radius * sinAngle
            );
        }

        
        public bool IsValid()
        {
            return this.All(p => p.IsValid());
        }
        
        public IPointsPath2D MapPoints(Func<IFloat64Tuple2D, IFloat64Tuple2D> pointMapping)
        {
            return new ArrayPointsPath2D(
                this.Select(pointMapping).ToArray()
            );
        }
    }
}