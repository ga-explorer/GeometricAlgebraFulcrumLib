using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Constants;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Frames;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Circles
{
    public class ParametricCircle3D :
        IGraphicsParametricCircle3D
    {
        private readonly IGraphicsParametricCircle3D _baseCircle;
        private readonly SquareMatrix3 _baseCircleRotation;

        public double Radius { get; }

        public Float64Tuple3D Center { get; }

        public Float64Tuple3D UnitNormal { get; }

        public double ParameterValueMin
            => 0d;

        public double ParameterValueMax
            => 1d;


        public ParametricCircle3D(IFloat64Tuple3D center, IFloat64Tuple3D unitNormal, double radius)
        {
            if (radius < 0)
                throw new ArgumentException(nameof(radius));

            Center = center.ToTuple3D();
            UnitNormal = unitNormal.ToTuple3D();
            Radius = radius;

            var axis = unitNormal.SelectNearestAxis();
            var isAxisNegative = axis.IsNegative();

            if (axis.IsXAxis())
                _baseCircle = new ParametricCircleYz3D(radius, isAxisNegative);

            else if (axis.IsYAxis())
                _baseCircle = new ParametricCircleZx3D(radius, isAxisNegative);

            else
                _baseCircle = new ParametricCircleXy3D(radius, isAxisNegative);

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
        public Float64Tuple3D GetPoint(double parameterValue)
        {
            return _baseCircleRotation * _baseCircle.GetPoint(parameterValue) + Center;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetDerivative1Point(double parameterValue)
        {
            return _baseCircleRotation * _baseCircle.GetDerivative1Point(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            var frame = _baseCircle.GetFrame(parameterValue);

            return ParametricCurveLocalFrame3D.Create(
                parameterValue,
                _baseCircleRotation * frame.Point + Center,
                _baseCircleRotation * frame.Normal1,
                _baseCircleRotation * frame.Normal2,
                _baseCircleRotation * frame.Tangent
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetDerivative2Point(double parameterValue)
        {
            return _baseCircleRotation * _baseCircle.GetDerivative2Point(parameterValue);
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