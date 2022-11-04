using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Circles
{
    public class GrParametricCircleZx3D :
        IGraphicsParametricCircle3D
    {
        private readonly double _directionFactor;

        public bool ReverseDirection { get; }

        public double Radius { get; }
        
        public Tuple3D Center 
            => Tuple3D.Zero;

        public Tuple3D UnitNormal 
            => ReverseDirection ? Tuple3D.NegativeE2 : Tuple3D.E2;

        public double ParameterValueMin
            => 0d;

        public double ParameterValueMax
            => 1d;


        public GrParametricCircleZx3D(double radius, bool reverseDirection = false)
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
                Radius * Math.Sin(angle),
                0,
                Radius * Math.Cos(angle)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetTangent(double parameterValue)
        {
            var angle = parameterValue * _directionFactor;
            var magnitude = Radius * _directionFactor;

            return new Tuple3D(
                magnitude * Math.Cos(angle),
                0,
                -magnitude * Math.Sin(angle)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetUnitTangent(double parameterValue)
        {
            var angle = parameterValue * _directionFactor;

            return new Tuple3D(
                Math.Cos(angle),
                0,
                -Math.Sin(angle)
            );
        }

        public GrParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            var angle = parameterValue * _directionFactor;
            var cosAngle = Math.Cos(angle);
            var sinAngle = Math.Sin(angle);

            var point = new Tuple3D(Radius * sinAngle, 0d, Radius * cosAngle);
            var normal1 = new Tuple3D(-sinAngle, 0d, -cosAngle);
            var normal2 = Tuple3D.E2;
            var tangent = new Tuple3D(cosAngle, 0d, -sinAngle);

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
                -magnitude * Math.Sin(angle),
                0,
                -magnitude * Math.Cos(angle)
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