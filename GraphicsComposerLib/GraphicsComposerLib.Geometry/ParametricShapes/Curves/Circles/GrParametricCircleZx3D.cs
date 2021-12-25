using System;
using System.Diagnostics;
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


        public GrParametricCircleZx3D(double radius, bool reverseDirection = false)
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
                Radius * Math.Sin(angle),
                0,
                Radius * Math.Cos(angle)
            );
        }

        public Tuple3D GetTangent(double parameterValue)
        {
            var angle = parameterValue * _directionFactor;

            return new Tuple3D(
                Radius * Math.Cos(angle),
                0,
                -Radius * Math.Sin(angle)
            );
        }

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
            var normal1 = new Tuple3D(sinAngle, 0d, cosAngle);
            var normal2 = Tuple3D.E2;
            var tangent = new Tuple3D(cosAngle, 0d, -sinAngle);

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