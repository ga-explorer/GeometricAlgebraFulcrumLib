using System.Diagnostics;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Lines.Mutable
{
    public sealed class MutableLinePair2D : ILinePair2D
    {
        public MutableLinePair2D Create(ILine2D ray1, ILine2D ray2)
        {
            return new MutableLinePair2D(
                ray1.OriginX, ray1.OriginY, ray1.DirectionX, ray1.DirectionY,
                ray2.OriginX, ray2.OriginY, ray2.DirectionX, ray2.DirectionY
            );
        }

        public MutableLinePair2D Create(IFloat64Tuple2D origin1, IFloat64Tuple2D direction1, IFloat64Tuple2D origin2, IFloat64Tuple2D direction2)
        {
            return new MutableLinePair2D(
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


        public double Origin1X { get; set; }

        public double Origin1Y { get; set; }


        public double Direction1X { get; set; }

        public double Direction1Y { get; set; }


        public double Origin2X { get; set; }

        public double Origin2Y { get; set; }


        public double Direction2X { get; set; }

        public double Direction2Y { get; set; }


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


        public MutableLinePair2D()
        {
        }

        internal MutableLinePair2D(double o1X, double o1Y, double d1X, double d1Y, double o2X, double o2Y, double d2X, double d2Y)
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


        public MutableLinePair2D SetOrigin1(double originX, double originY)
        {
            Origin1X = originX;
            Origin1Y = originY;

            return this;
        }

        public MutableLinePair2D SetOrigin1(IFloat64Tuple2D origin)
        {
            Origin1X = origin.X;
            Origin1Y = origin.Y;

            return this;
        }

        public MutableLinePair2D SetDirection1(double directionX, double directionY)
        {
            Direction1X = directionX;
            Direction1Y = directionY;

            return this;
        }

        public MutableLinePair2D SetDirection1(IFloat64Tuple2D direction)
        {
            Direction1X = direction.X;
            Direction1Y = direction.Y;

            return this;
        }

        public MutableLinePair2D SetLine1(ILine2D ray)
        {
            Origin1X = ray.OriginX;
            Origin1Y = ray.OriginY;

            Direction1X = ray.DirectionX;
            Direction1Y = ray.DirectionY;

            return this;
        }

        public MutableLinePair2D SetOrigin2(double originX, double originY)
        {
            Origin2X = originX;
            Origin2Y = originY;

            return this;
        }

        public MutableLinePair2D SetOrigin2(IFloat64Tuple2D origin)
        {
            Origin2X = origin.X;
            Origin2Y = origin.Y;

            return this;
        }

        public MutableLinePair2D SetDirection2(double directionX, double directionY)
        {
            Direction2X = directionX;
            Direction2Y = directionY;

            return this;
        }

        public MutableLinePair2D SetDirection2(IFloat64Tuple2D direction)
        {
            Direction2X = direction.X;
            Direction2Y = direction.Y;

            return this;
        }

        public MutableLinePair2D SetLine2(ILine2D ray)
        {
            Origin2X = ray.OriginX;
            Origin2Y = ray.OriginY;

            Direction2X = ray.DirectionX;
            Direction2Y = ray.DirectionY;

            return this;
        }

        public MutableLinePair2D SetLinePair(ILine2D ray1, ILine2D ray2)
        {
            Origin1X = ray1.OriginX;
            Origin1Y = ray1.OriginY;

            Direction1X = ray1.DirectionX;
            Direction1Y = ray1.DirectionY;

            Origin2X = ray2.OriginX;
            Origin2Y = ray2.OriginY;

            Direction2X = ray2.DirectionX;
            Direction2Y = ray2.DirectionY;

            return this;
        }
    }
}