using System.Diagnostics;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicShapes.Lines;
using EuclideanGeometryLib.BasicShapes.Lines.Immutable;
using EuclideanGeometryLib.Borders.Space1D;
using EuclideanGeometryLib.Borders.Space1D.Immutable;

namespace EuclideanGeometryLib.Computers
{
    public sealed class LineTraversalData2D
    {
        public double[] Origin { get; } = new double[2];

        public double[] Direction { get; } = new double[2];

        public double[] DirectionInv { get; } = new double[2];

        public int[] DirectionSign { get; } = new int[2];

        public double ParameterMinValue { get; private set; }

        public double ParameterMaxValue { get; private set; }

        public bool IsLine
            => double.IsNegativeInfinity(ParameterMinValue) &&
               double.IsPositiveInfinity(ParameterMaxValue);

        public bool IsRay
            => !double.IsInfinity(ParameterMinValue) && 
               double.IsPositiveInfinity(ParameterMaxValue);

        public bool IsLineSegment
            => !double.IsInfinity(ParameterMinValue) && 
               !double.IsInfinity(ParameterMaxValue);


        internal LineTraversalData2D(ILine2D line)
        {
            Origin[0] = line.OriginX;
            Origin[1] = line.OriginY;

            Direction[0] = line.DirectionX;
            Direction[1] = line.DirectionY;

            DirectionInv[0] = 1 / line.DirectionX;
            DirectionInv[1] = 1 / line.DirectionY;

            DirectionSign[0] = line.DirectionX < 0 ? 1 : 0;
            DirectionSign[1] = line.DirectionY < 0 ? 1 : 0;

            ParameterMinValue = double.NegativeInfinity;
            ParameterMaxValue = double.PositiveInfinity;
        }

        internal LineTraversalData2D(ILine2D line, IBoundingBox1D paramRange)
        {
            Origin[0] = line.OriginX;
            Origin[1] = line.OriginY;

            Direction[0] = line.DirectionX;
            Direction[1] = line.DirectionY;

            DirectionInv[0] = 1 / line.DirectionX;
            DirectionInv[1] = 1 / line.DirectionY;

            DirectionSign[0] = line.DirectionX < 0 ? 1 : 0;
            DirectionSign[1] = line.DirectionY < 0 ? 1 : 0;

            ParameterMinValue = paramRange.MinValue;
            ParameterMaxValue = paramRange.MaxValue;
        }

        internal LineTraversalData2D(ILine2D line, double paramMinValue, double paramMaxValue)
        {
            Origin[0] = line.OriginX;
            Origin[1] = line.OriginY;

            Direction[0] = line.DirectionX;
            Direction[1] = line.DirectionY;

            DirectionInv[0] = 1 / line.DirectionX;
            DirectionInv[1] = 1 / line.DirectionY;

            DirectionSign[0] = line.DirectionX < 0 ? 1 : 0;
            DirectionSign[1] = line.DirectionY < 0 ? 1 : 0;

            ParameterMinValue = paramMinValue;
            ParameterMaxValue = paramMaxValue;
        }


        internal LineTraversalData2D RestrictParameterMaxValue(double value)
        {
            Debug.Assert(value >= ParameterMinValue);

            if (ParameterMaxValue > value)
                ParameterMaxValue = value;

            return this;
        }


        public BoundingBox1D GetParameterRange()
            => new BoundingBox1D(ParameterMinValue, ParameterMaxValue);

        public LineSegment2D GetLineSegment() 
            => new LineSegment2D(
                Origin[0] + ParameterMinValue * Direction[0],
                Origin[1] + ParameterMinValue * Direction[1],
                Origin[0] + ParameterMaxValue * Direction[0],
                Origin[1] + ParameterMaxValue * Direction[1]
            );

        public Tuple2D GetMinPoint()
            => new Tuple2D(
                Origin[0] + ParameterMinValue * Direction[0],
                Origin[1] + ParameterMinValue * Direction[1]
            );

        public Tuple2D GetMaxPoint()
            => new Tuple2D(
                Origin[0] + ParameterMaxValue * Direction[0],
                Origin[1] + ParameterMaxValue * Direction[1]
            );
    }
}