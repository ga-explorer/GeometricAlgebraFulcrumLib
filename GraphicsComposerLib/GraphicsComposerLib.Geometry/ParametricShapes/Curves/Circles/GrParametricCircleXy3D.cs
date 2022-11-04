using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Circles
{
    public class GrParametricCircleXy3D :
        IGraphicsParametricCircle3D
    {
        private readonly double _directionFactor;

        public bool ReverseDirection { get; }

        public double Radius { get; }
        
        public Tuple3D Center 
            => Tuple3D.Zero;

        public Tuple3D UnitNormal 
            => ReverseDirection ? Tuple3D.NegativeE3 : Tuple3D.E3;

        public double ParameterValueMin
            => 0d;

        public double ParameterValueMax 
            => 1d;


        public GrParametricCircleXy3D(double radius, bool reverseDirection = false)
        {
            if (radius < 0)
                throw new ArgumentException(nameof(radius));

            _directionFactor = 2 * Math.PI;

            if (reverseDirection) 
                _directionFactor *= -1;

            ReverseDirection = reverseDirection;
            Radius = radius;

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Radius.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetPoint(double parameterValue)
        {
            var angle = parameterValue * _directionFactor;

            return new Tuple3D(
                Radius * Math.Cos(angle),
                Radius * Math.Sin(angle),
                0
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetTangent(double parameterValue)
        {
            var angle = parameterValue * _directionFactor;
            var magnitude = Radius * _directionFactor;

            return new Tuple3D(
                -magnitude * Math.Sin(angle),
                magnitude * Math.Cos(angle),
                0
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetUnitTangent(double parameterValue)
        {
            var angle = parameterValue * _directionFactor;

            return new Tuple3D(
                -Math.Sin(angle),
                Math.Cos(angle),
                0
            );
        }

        public GrParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            var angle = parameterValue * _directionFactor;
            var cosAngle = Math.Cos(angle);
            var sinAngle = Math.Sin(angle);

            var point = new Tuple3D(Radius * cosAngle, Radius * sinAngle, 0d);
            var normal1 = new Tuple3D(-cosAngle, -sinAngle, 0d);
            var normal2 = Tuple3D.E3;
            var tangent = new Tuple3D(-sinAngle, cosAngle, 0d);

            return GrParametricCurveLocalFrame3D.Create(
                parameterValue,
                point,
                normal1,
                normal2,
                tangent
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetSecondDerivative(double parameterValue)
        {
            var angle = parameterValue * _directionFactor;
            var magnitude = Radius * _directionFactor * _directionFactor;

            return new Tuple3D(
                -magnitude * Math.Cos(angle),
                -magnitude * Math.Sin(angle),
                0
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