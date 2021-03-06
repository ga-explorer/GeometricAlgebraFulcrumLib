using System;
using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space2D
{
    public sealed class CircularPointsPath2D : 
        PSeqMapped1D<double, ITuple2D>, 
        IPointsPath2D
    {
        public ITuple2D Center { get; }

        public double Radius { get; }


        public CircularPointsPath2D(ITuple2D center, double radius, int count)
            : base(PeriodicSequenceUtils.CreateLinearDoubleSequence(count))
        {
            if (Count < 2)
                throw new ArgumentOutOfRangeException(nameof(count));

            Center = center;
            Radius = radius;
        }

        public CircularPointsPath2D(ITuple2D center, double radius, IPeriodicSequence1D<double> parameterSequence)
            : base(parameterSequence)
        {
            Center = center;
            Radius = radius;
        }


        protected override ITuple2D MappingFunction(double t)
        {
            var angle = 2 * Math.PI * t;
            var cosAngle = Math.Cos(angle);
            var sinAngle = Math.Sin(angle);

            return new Tuple2D(
                Center.X + Radius * cosAngle,
                Center.Y + Radius * sinAngle
            );
        }
    }
}