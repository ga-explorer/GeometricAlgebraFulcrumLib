using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Constants;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Circles
{
    public class GrParametricCircle3D :
        IGraphicsParametricCircle3D
    {
        private readonly IGraphicsC1ParametricCurve3D _baseCircle;
        private readonly SquareMatrix3 _baseCircleRotation;

        public double Radius { get; }

        public Tuple3D Center { get; }

        public Tuple3D UnitNormal { get; }


        public GrParametricCircle3D([NotNull] ITuple3D center, [NotNull] ITuple3D unitNormal, double radius)
        {
            if (radius < 0)
                throw new ArgumentException(nameof(radius));

            Center = center.ToTuple3D();
            UnitNormal = unitNormal.ToTuple3D();
            Radius = radius;

            var axis = unitNormal.SelectNearestAxis();
            var isAxisNegative = axis.IsNegative();

            if (axis.IsXAxis())
                _baseCircle = new GrParametricCircleYz3D(radius, isAxisNegative);
            
            else if (axis.IsYAxis())
                _baseCircle = new GrParametricCircleZx3D(radius, isAxisNegative);
            
            else
                _baseCircle = new GrParametricCircleXy3D(radius, isAxisNegative);

            _baseCircleRotation = SquareMatrix3.CreateAxisToVectorRotationMatrix3D(axis, unitNormal);

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Radius.IsValid() &&
                   Center.IsValid() && 
                   UnitNormal.IsValid() && 
                   UnitNormal.IsNearUnitVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetPoint(double parameterValue)
        {
            return _baseCircleRotation * _baseCircle.GetPoint(parameterValue) + Center;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetTangent(double parameterValue)
        {
            return _baseCircleRotation * _baseCircle.GetTangent(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetUnitTangent(double parameterValue)
        {
            return GetTangent(parameterValue).ToUnitVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            var frame = _baseCircle.GetFrame(parameterValue);

            return GrParametricCurveLocalFrame3D.CreateFrame(
                parameterValue,
                _baseCircleRotation * frame.Point + Center,
                _baseCircleRotation * frame.Normal1,
                _baseCircleRotation * frame.Normal2,
                _baseCircleRotation * frame.Tangent
            );
        }
    }
}