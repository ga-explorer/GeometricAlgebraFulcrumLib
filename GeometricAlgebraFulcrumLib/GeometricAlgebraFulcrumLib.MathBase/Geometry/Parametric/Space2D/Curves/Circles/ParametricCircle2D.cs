using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Frames;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves.Circles
{
    public class ParametricCircle2D :
        IGraphicsParametricCircle2D
    {
        private readonly double _directionFactor;

        public bool ReverseDirection { get; }

        public double Radius { get; }

        public Float64Vector2D Center { get; }
        
        public Float64Range1D ParameterRange 
            => Float64Range1D.ZeroToOne;
        

        public ParametricCircle2D(IFloat64Tuple2D center, double radius, bool reverseDirection = false)
        {
            if (radius < 0)
                throw new ArgumentException(nameof(radius));

            _directionFactor = 2 * Math.PI;

            if (reverseDirection)
                _directionFactor *= -1;

            ReverseDirection = reverseDirection;
            Radius = radius;
            Center = center.ToLinVector2D();

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Radius.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D GetPoint(double parameterValue)
        {
            var angle = parameterValue * _directionFactor;

            return new Float64Vector2D(
                Radius * Math.Cos(angle),
                Radius * Math.Sin(angle)
            );
        }

        public Float64Vector2D GetTangent(double parameterValue)
        {
            throw new NotImplementedException();
        }

        public Float64Vector2D GetUnitTangent(double parameterValue)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D GetDerivative1Point(double parameterValue)
        {
            var angle = parameterValue * _directionFactor;
            var magnitude = Radius * _directionFactor;

            return new Float64Vector2D(
                -magnitude * Math.Sin(angle),
                magnitude * Math.Cos(angle)
            );
        }

        public ParametricCurveLocalFrame2D GetFrame(double parameterValue)
        {
            var angle = parameterValue * _directionFactor;
            var cosAngle = Math.Cos(angle);
            var sinAngle = Math.Sin(angle);

            var point = new Float64Vector2D(Radius * cosAngle, Radius * sinAngle);
            var tangent = new Float64Vector2D(-sinAngle, cosAngle);

            return ParametricCurveLocalFrame2D.Create(
                parameterValue,
                point,
                tangent
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D GetDerivative2Point(double parameterValue)
        {
            var angle = parameterValue * _directionFactor;
            var magnitude = Radius * _directionFactor * _directionFactor;

            return new Float64Vector2D(
                -magnitude * Math.Cos(angle),
                -magnitude * Math.Sin(angle)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetLength()
        {
            return 2d * Math.PI * Radius;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ParameterToLength(double parameterValue)
        {
            return parameterValue.ClampPeriodic(1d) * GetLength();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double LengthToParameter(double length)
        {
            var maxLength = GetLength();

            return length.ClampPeriodic(maxLength) / maxLength;
        }
    }
}