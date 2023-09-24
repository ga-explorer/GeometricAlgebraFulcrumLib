using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Circles
{
    public class ParametricCircle3D :
        IGraphicsParametricCircle3D
    {
        private readonly IGraphicsParametricCircle3D _baseCircle;
        private readonly SquareMatrix3 _baseCircleRotation;
        
        public int RotationCount { get; }

        public double Radius { get; }

        public Float64Vector3D Center { get; }

        public Float64Vector3D UnitNormal { get; }

        public Float64ScalarRange ParameterRange
            => Float64ScalarRange.ZeroToOne;
        

        public ParametricCircle3D(IFloat64Vector3D center, IFloat64Vector3D unitNormal, double radius, int rotationCount = 1)
        {
            if (radius < 0)
                throw new ArgumentException(nameof(radius));
            
            if (rotationCount == 0 || rotationCount > 100 || rotationCount < -100)
                throw new ArgumentException(nameof(rotationCount));

            RotationCount = Math.Abs(rotationCount);
            Center = center.ToVector3D();
            UnitNormal = unitNormal.ToVector3D();
            Radius = radius;

            var axis = unitNormal.SelectNearestAxis();

            if (axis.IsNegative())
                rotationCount = -rotationCount;

            if (axis.IsXAxis())
                _baseCircle = new ParametricCircleYz3D(radius, rotationCount);

            else if (axis.IsYAxis())
                _baseCircle = new ParametricCircleZx3D(radius, rotationCount);

            else
                _baseCircle = new ParametricCircleXy3D(radius, rotationCount);

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
        public Float64Vector3D GetPoint(double parameterValue)
        {
            return _baseCircleRotation * _baseCircle.GetPoint(parameterValue) + Center;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetDerivative1Point(double parameterValue)
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
                _baseCircleRotation * frame.Tangent,
                _baseCircleRotation * frame.Normal1,
                _baseCircleRotation * frame.Normal2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetDerivative2Point(double parameterValue)
        {
            return _baseCircleRotation * _baseCircle.GetDerivative2Point(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar GetLength()
        {
            return Math.Abs(2d * Math.PI * Radius * RotationCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar ParameterToLength(double parameterValue)
        {
            return parameterValue.ClampPeriodic(1d) * GetLength();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar LengthToParameter(double length)
        {
            var maxLength = GetLength();

            return length.ClampPeriodic(maxLength) / maxLength;
        }
    }
}