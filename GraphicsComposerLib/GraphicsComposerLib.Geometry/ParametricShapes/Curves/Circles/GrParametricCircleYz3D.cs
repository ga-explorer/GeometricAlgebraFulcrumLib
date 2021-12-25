using System;
using System.Diagnostics;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Circles
{
    public class GrParametricCircleYz3D :
        IGraphicsParametricCircle3D
    {
        private readonly double _directionFactor;

        public bool ReverseDirection { get; }

        public double Radius { get; }
        
        public Tuple3D Center 
            => Tuple3D.Zero;

        public Tuple3D UnitNormal 
            => ReverseDirection ? Tuple3D.NegativeE1 : Tuple3D.E1;


        public GrParametricCircleYz3D(double radius, bool reverseDirection = false)
        {
            if (radius < 0)
                throw new ArgumentException(nameof(radius));

            _directionFactor =
                reverseDirection ? (-2 * Math.PI) : (2 * Math.PI);

            ReverseDirection = reverseDirection;
            Radius = radius;

            Debug.Assert(IsValid());
        }


        public bool IsValid()
        {
            return Radius.IsValid();
        }

        public Tuple3D GetPoint(double parameterValue)
        {
            var angle = parameterValue * _directionFactor;

            return new Tuple3D(
                0,
                Radius * Math.Cos(angle),
                Radius * Math.Sin(angle)
            );
        }

        public Tuple3D GetTangent(double parameterValue)
        {
            var angle = parameterValue * _directionFactor;

            return new Tuple3D(
                0d,
                -Radius * Math.Sin(angle),
                Radius * Math.Cos(angle)
            );
        }

        public Tuple3D GetUnitTangent(double parameterValue)
        {
            var angle = parameterValue * _directionFactor;

            return new Tuple3D(
                0d,
                -Math.Sin(angle),
                Math.Cos(angle)
            );
        }

        public GrParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            var angle = parameterValue * _directionFactor;
            var cosAngle = Math.Cos(angle);
            var sinAngle = Math.Sin(angle);

            var point = new Tuple3D(0d, Radius * cosAngle, Radius * sinAngle);
            var normal1 = new Tuple3D(0d, cosAngle, sinAngle);
            var normal2 = Tuple3D.E1;
            var tangent = new Tuple3D(0d, -sinAngle, cosAngle);

            return GrParametricCurveLocalFrame3D.CreateFrame(
                parameterValue,
                point,
                normal1,
                normal2,
                tangent
            );
        }
    }
}