using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Borders.Space1D.Immutable;

namespace NumericalGeometryLib.Computers
{
    public sealed class LineTraversalData3D
    {
        private readonly Stack<BoundingBox1D> _parameterRangeStack
            = new Stack<BoundingBox1D>();


        public Float64Tuple3D Origin { get; }

        public Float64Tuple3D Direction { get; }

        public Float64Tuple3D DirectionInv { get; }

        public int[] DirectionSign { get; }

        public double ParameterMinValue { get; private set; }

        public double ParameterMaxValue { get; private set; }

        public bool IsLine
        {
            get
            {
                return double.IsNegativeInfinity(ParameterMinValue) &&
                       double.IsPositiveInfinity(ParameterMaxValue);
            }
        }

        public bool IsRay
        {
            get
            {
                return !double.IsInfinity(ParameterMinValue) &&
                       double.IsPositiveInfinity(ParameterMaxValue);
            }
        }

        public bool IsLineSegment
        {
            get
            {
                return !double.IsInfinity(ParameterMinValue) &&
                       !double.IsInfinity(ParameterMaxValue);
            }
        }

        internal LineTraversalData3D(ILine3D line)
        {
            Origin = line.GetOrigin();
            Direction = line.GetDirection();
            DirectionInv = line.GetDirectionInv();
            DirectionSign = line.GetDirectionSign();
            ParameterMinValue = double.NegativeInfinity;
            ParameterMaxValue = double.PositiveInfinity;
        }

        internal LineTraversalData3D(ILine3D line, double paramMinValue, double paramMaxValue)
        {
            Origin = line.GetOrigin();
            Direction = line.GetDirection();
            DirectionInv = line.GetDirectionInv();
            DirectionSign = line.GetDirectionSign();
            ParameterMinValue = paramMinValue;
            ParameterMaxValue = paramMaxValue;
        }


        internal LineTraversalData3D StoreParameterRange(double newMinValue, double newMaxValue)
        {
            _parameterRangeStack.Push(
                new BoundingBox1D(ParameterMinValue, ParameterMaxValue)
            );

            ParameterMinValue = newMinValue;
            ParameterMaxValue = newMaxValue;

            return this;
        }

        internal LineTraversalData3D RestoreParameterRange()
        {
            var range = _parameterRangeStack.Pop();

            ParameterMinValue = range.MinValue;
            ParameterMaxValue = range.MaxValue;

            return this;
        }

        internal LineTraversalData3D UpdateParameterMaxValue(double value)
        {
            if (ParameterMaxValue > value)
                ParameterMaxValue = value;

            return this;
        }

        public BoundingBox1D GetParameterRange()
        {
            return new BoundingBox1D(ParameterMinValue, ParameterMaxValue);
        }

        public LineSegment3D GetLineSegment()
        {
            return new LineSegment3D(
                Origin.X + ParameterMinValue * Direction.X,
                Origin.Y + ParameterMinValue * Direction.Y,
                Origin.Z + ParameterMinValue * Direction.Z,
                Origin.X + ParameterMaxValue * Direction.X,
                Origin.Y + ParameterMaxValue * Direction.Y,
                Origin.Z + ParameterMaxValue * Direction.Z
            );
        }

        public Float64Tuple3D GetMinPoint()
        {
            return new Float64Tuple3D(
                Origin.X + ParameterMinValue * Direction.X,
                Origin.Y + ParameterMinValue * Direction.Y,
                Origin.Z + ParameterMinValue * Direction.Z
            );
        }

        public Float64Tuple3D GetMaxPoint()
        {
            return new Float64Tuple3D(
                Origin.X + ParameterMaxValue * Direction.X,
                Origin.Y + ParameterMaxValue * Direction.Y,
                Origin.Z + ParameterMaxValue * Direction.Z
            );
        }
    }
}