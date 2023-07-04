using System.Diagnostics;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Lines.Immutable
{
    public sealed class LinePair2D : ILinePair2D
    {
        public LinePair2D Create(ILine2D ray1, ILine2D ray2)
        {
            return new LinePair2D(
                ray1.OriginX, ray1.OriginY, ray1.DirectionX, ray1.DirectionY,
                ray2.OriginX, ray2.OriginY, ray2.DirectionX, ray2.DirectionY
            );
        }

        public LinePair2D Create(IFloat64Vector2D origin1, IFloat64Vector2D direction1, IFloat64Vector2D origin2, IFloat64Vector2D direction2)
        {
            return new LinePair2D(
                origin1.X, 
                origin1.Y, 
                direction1.X, 
                direction1.Y,
                origin2.X, 
                origin2.Y,
                direction2.X, 
                direction2.Y
            );
        }


        public double Origin1X { get; }

        public double Origin1Y { get; }


        public double Direction1X { get; }

        public double Direction1Y { get; }


        public double Origin2X { get; }

        public double Origin2Y { get; }


        public double Direction2X { get; }

        public double Direction2Y { get; }


        public bool IsValid()
        {
            return !double.IsNaN(Origin1X) &&
                   !double.IsNaN(Origin1Y) &&
                   !double.IsNaN(Origin2X) &&
                   !double.IsNaN(Origin2Y) &&
                   !double.IsNaN(Direction1X) &&
                   !double.IsNaN(Direction1Y) &&
                   !double.IsNaN(Direction2X) &&
                   !double.IsNaN(Direction2Y);
        }


        internal LinePair2D(double o1X, double o1Y, double d1X, double d1Y, double o2X, double o2Y, double d2X, double d2Y)
        {
            Origin1X = o1X;
            Origin1Y = o1Y;

            Direction1X = d1X;
            Direction1Y = d1Y;

            Origin2X = o2X;
            Origin2Y = o2Y;

            Direction2X = d2X;
            Direction2Y = d2Y;

            Debug.Assert(IsValid());
        }
    }
}
