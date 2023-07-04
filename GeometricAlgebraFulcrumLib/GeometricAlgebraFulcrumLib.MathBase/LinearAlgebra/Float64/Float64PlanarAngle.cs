using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64
{
    public sealed record Float64PlanarAngle :
        IGeometricElement
    {
        public const double DegreeToRadianFactor = Math.PI / 180d;

        public const double RadianToDegreeFactor = 180d / Math.PI;


        public static Float64PlanarAngle Angle0 { get; }
            = new Float64PlanarAngle(0d);

        public static Float64PlanarAngle Angle30 { get; }
            = new Float64PlanarAngle(30d);

        public static Float64PlanarAngle Angle45 { get; }
            = new Float64PlanarAngle(45d);

        public static Float64PlanarAngle Angle60 { get; }
            = new Float64PlanarAngle(60d);

        public static Float64PlanarAngle Angle90 { get; }
            = new Float64PlanarAngle(90d);

        public static Float64PlanarAngle Angle120 { get; }
            = new Float64PlanarAngle(120d);

        public static Float64PlanarAngle Angle135 { get; }
            = new Float64PlanarAngle(135d);

        public static Float64PlanarAngle Angle150 { get; }
            = new Float64PlanarAngle(150d);

        public static Float64PlanarAngle Angle180 { get; }
            = new Float64PlanarAngle(180d);

        public static Float64PlanarAngle Angle225 { get; }
            = new Float64PlanarAngle(225d);

        public static Float64PlanarAngle Angle210 { get; }
            = new Float64PlanarAngle(210d);

        public static Float64PlanarAngle Angle240 { get; }
            = new Float64PlanarAngle(240d);

        public static Float64PlanarAngle Angle270 { get; }
            = new Float64PlanarAngle(270d);

        public static Float64PlanarAngle Angle300 { get; }
            = new Float64PlanarAngle(300d);

        public static Float64PlanarAngle Angle315 { get; }
            = new Float64PlanarAngle(315d);

        public static Float64PlanarAngle Angle330 { get; }
            = new Float64PlanarAngle(330d);

        public static Float64PlanarAngle Angle360 { get; }
            = new Float64PlanarAngle(360d);

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle CreateFromDegrees(double angleInDegrees)
        {
            // A full-range angle is in the range [-360, 360] degrees
            var fullRangeAngleValue = 
                angleInDegrees switch
                {
                    < -360d => angleInDegrees % 720d + 360d,
                    > 360 => angleInDegrees % 360d,
                    _ => angleInDegrees
                };

            return new Float64PlanarAngle(fullRangeAngleValue);

            //return angleInDegrees switch
            //{
            //    < -360d => new Float64PlanarAngle(angleInDegrees % 720d + 360d),
            //    > 360 => new Float64PlanarAngle(angleInDegrees % 360d),
            //    _ => new Float64PlanarAngle(angleInDegrees)
            //};
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle CreateFromDegrees(double angleInDegrees, Float64PlanarAngleRange range)
        {
            // A full-range angle is in the range [-360, 360] degrees
            var fullRangeAngleValue = 
                angleInDegrees switch
                {
                    < -360d => angleInDegrees % 720d + 360d,
                    > 360 => angleInDegrees % 360d,
                    _ => angleInDegrees
                };

            if (range == Float64PlanarAngleRange.Positive)
            {
                // A positive-range angle is in the range [0, 360] degrees
                if (fullRangeAngleValue < 0)
                    fullRangeAngleValue += 360;
            }
            
            else if (range == Float64PlanarAngleRange.Negative)
            {
                // A negative-range angle is in the range [-360, 0] degrees
                if (fullRangeAngleValue > 0)
                    fullRangeAngleValue -= 360;
            }

            else if (range == Float64PlanarAngleRange.Symmetric)
            {
                // A symmetric-range angle is in the range [-180, 180] degrees
                if (fullRangeAngleValue < -180)
                    fullRangeAngleValue += 360;

                else if (fullRangeAngleValue > 180)
                    fullRangeAngleValue -= 360;
            }

            return new Float64PlanarAngle(fullRangeAngleValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle CreateFromRadians(double angleInRadians)
        {
            return CreateFromDegrees(
                angleInRadians * RadianToDegreeFactor
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle CreateFromRadians(double angleInRadians, Float64PlanarAngleRange range)
        {
            return CreateFromDegrees(
                angleInRadians * RadianToDegreeFactor,
                range
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle CreateFromXy(double x, double y)
        {
            return CreateFromRadians(
                Math.Atan2(y, x)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle CreateFromXy(double x, double y, Float64PlanarAngleRange range)
        {
            return CreateFromRadians(
                Math.Atan2(y, x),
                range
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle CreateFromXy(IPair<double> xyPair)
        {
            return CreateFromRadians(
                Math.Atan2(xyPair.Item2, xyPair.Item1)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle CreateFromXy(IPair<double> xyPair, Float64PlanarAngleRange range)
        {
            return CreateFromRadians(
                Math.Atan2(xyPair.Item2, xyPair.Item1),
                range
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle CreateFromYx(double y, double x)
        {
            return CreateFromRadians(
                Math.Atan2(y, x)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle CreateFromYx(double y, double x, Float64PlanarAngleRange range)
        {
            return CreateFromRadians(
                Math.Atan2(y, x),
                range
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle CreateFromYx(IPair<double> yxPair)
        {
            return CreateFromRadians(
                Math.Atan2(yxPair.Item1, yxPair.Item2)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle CreateFromYx(IPair<double> yxPair, Float64PlanarAngleRange range)
        {
            return CreateFromRadians(
                Math.Atan2(yxPair.Item1, yxPair.Item2),
                range
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle CreateFromUnitVectors(IFloat64Vector2D v1, IFloat64Vector2D v2)
        {
            return v1.ESp(v2).Clamp(-1, 1).ArcCos();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle CreateFromUnitVectors(IFloat64Vector3D v1, IFloat64Vector3D v2)
        {
            return v1.ESp(v2).Clamp(-1, 1).ArcCos();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle CreateFromUnitVectors(IFloat64Vector4D v1, IFloat64Vector4D v2)
        {
            return v1.ESp(v2).Clamp(-1, 1).ArcCos();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator double(Float64PlanarAngle angle)
        {
            return angle.Radians.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Float64PlanarAngle(double angleInRadians)
        {
            return CreateFromDegrees(
                angleInRadians * RadianToDegreeFactor
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle operator -(Float64PlanarAngle angle)
        {
            return CreateFromDegrees(-angle.Degrees.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle operator +(Float64PlanarAngle angle1, Float64PlanarAngle angle2)
        {
            return CreateFromDegrees(angle1.Degrees.Value + angle2.Degrees.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle operator +(Float64PlanarAngle angle1, double angleInRadians2)
        {
            return CreateFromDegrees(angle1.Degrees.Value + angleInRadians2 * RadianToDegreeFactor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle operator +(double angleInRadians1, Float64PlanarAngle angle2)
        {
            return CreateFromDegrees(angleInRadians1 * RadianToDegreeFactor + angle2.Degrees.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle operator -(Float64PlanarAngle angle1, Float64PlanarAngle angle2)
        {
            return CreateFromDegrees(angle1.Degrees.Value - angle2.Degrees.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle operator -(Float64PlanarAngle angle1, double angleInRadians2)
        {
            return CreateFromDegrees(angle1.Degrees.Value - angleInRadians2 * RadianToDegreeFactor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle operator -(double angleInRadians1, Float64PlanarAngle angle2)
        {
            return CreateFromDegrees(angleInRadians1 * RadianToDegreeFactor - angle2.Degrees.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle operator *(Float64PlanarAngle angle, double number)
        {
            return CreateFromDegrees(angle.Degrees.Value * number);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle operator *(double number, Float64PlanarAngle angle)
        {
            return CreateFromDegrees(number * angle.Degrees.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle operator /(Float64PlanarAngle angle, double number)
        {
            return CreateFromDegrees(angle.Degrees.Value / number);
        }


        private Float64Scalar? _cosAngle;
        private Float64Scalar? _sinAngle;

        public Float64Scalar Degrees { get; }

        public Float64Scalar Radians
            => Degrees * DegreeToRadianFactor;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Float64PlanarAngle(double degrees)
        {
            Degrees = new Float64Scalar(degrees);

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar Cos()
        {
            _cosAngle ??= Radians.Cos();

            return _cosAngle.Value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar Sec()
        {
            return Cos().Inverse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar Sin()
        {
            _sinAngle ??= Radians.Sin();

            return _sinAngle.Value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar Csc()
        {
            return Sin().Inverse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar Tan()
        {
            return Sin() / Cos();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar Cot()
        {
            return Cos() / Sin();
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public Float64PlanarAngle ClampPositive()
        //{
        //    return Degrees < 0d
        //        ? new Float64PlanarAngle(Degrees.Value + 360d)
        //        : this;
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64PlanarAngle ClampNegative()
        {
            const double maxValue = 360d;

            var value = Degrees.Value + 180d;

            return value switch
            {
                //value < -maxValue
                < -maxValue => new Float64PlanarAngle(value + Math.Ceiling(-value / maxValue) * maxValue),

                //-maxValue <= value < 0
                < 0 => new Float64PlanarAngle(value + maxValue),

                //value > maxValue
                > maxValue => new Float64PlanarAngle(value - Math.Truncate(value / maxValue) * maxValue),

                //0 <= value <= maxValue
                _ => new Float64PlanarAngle(value)
            };
        }

        public Float64PlanarAngle GetAngleInPeriodicRange(double maxAngleInDegrees)
        {
            //Make sure maxValue > 0
            Debug.Assert(maxAngleInDegrees is > 0d and <= 360d);

            //value < -maxValue
            if (Degrees < -maxAngleInDegrees)
                return new Float64PlanarAngle(Degrees + Math.Ceiling(-Degrees / maxAngleInDegrees) * maxAngleInDegrees);

            //-maxValue <= value < 0
            if (Degrees.IsNegative())
                return new Float64PlanarAngle(Degrees + maxAngleInDegrees);

            //value > maxValue
            if (Degrees > maxAngleInDegrees)
                return new Float64PlanarAngle(Degrees - Math.Truncate(Degrees / maxAngleInDegrees) * maxAngleInDegrees);

            //0 <= value <= maxValue
            return new Float64PlanarAngle(Degrees);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64PolarVector2D ToPolarPosition()
        {
            return new Float64PolarVector2D(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64PolarVector2D ToPolarPosition(double r)
        {
            return new Float64PolarVector2D(r, this);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return !Degrees.Value.IsNaNOrInfinite() &&
                   Degrees.Value is >= -360d and <= 360d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsRight()
        {
            return Degrees.Value is 90d or -90d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsStraight()
        {
            return Degrees.Value is 180d or -180d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsAcute()
        {
            return Degrees.Value is > 0d and < 90d or < -270d and > -360d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsObtuse()
        {
            return Degrees.Value is > 0d and < 180d or < -180d and > -360d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsReflex()
        {
            return Degrees.Value is > 180d and < 360d or < 0d and > -180d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZeroOrFullRotation()
        {
            return Degrees.Value is 0d or -360d or 360d;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(double epsilon = 1e-9)
        {
            return Float64Utils.IsNearZero(Degrees, epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearFullRotation(double epsilon = 1e-9)
        {
            return Float64Utils.IsNearZero((Degrees - 360d), epsilon) ||
                   Float64Utils.IsNearZero((Degrees + 360d), epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZeroOrFullRotation(double epsilon = 1e-9)
        {
            return Float64Utils.IsNearZero(Degrees, epsilon) ||
                   Float64Utils.IsNearZero((Degrees - 360d), epsilon) ||
                   Float64Utils.IsNearZero((Degrees + 360d), epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearRight(double epsilon = 1e-9)
        {
            return Float64Utils.IsNearZero((Degrees - 90d), epsilon) ||
                   Float64Utils.IsNearZero((Degrees + 90d), epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearStraight(double epsilon = 1e-9)
        {
            return Float64Utils.IsNearZero((Degrees - 180d), epsilon) ||
                   Float64Utils.IsNearZero((Degrees + 180d), epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearEqual(Float64PlanarAngle angle, double epsilon = 1e-9)
        {
            return Float64PlanarAngleUtils.DegreesToAngle((Degrees - angle.Degrees)).IsNearZeroOrFullRotation(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearFullRotation(Float64PlanarAngle angle, double epsilon = 1e-9)
        {
            return Float64PlanarAngleUtils.DegreesToAngle((Degrees + angle.Degrees)).IsNearZeroOrFullRotation(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearEqualOrFullRotation(Float64PlanarAngle angle, double epsilon = 1e-9)
        {
            return Float64PlanarAngleUtils.DegreesToAngle((Degrees - angle.Degrees)).IsNearZeroOrFullRotation(epsilon) ||
                   Float64PlanarAngleUtils.DegreesToAngle((Degrees + angle.Degrees)).IsNearZeroOrFullRotation(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearComplementary(Float64PlanarAngle angle, double epsilon = 1e-9)
        {
            return Float64PlanarAngleUtils.DegreesToAngle((Degrees + angle.Degrees - 90)).IsNearZeroOrFullRotation(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearSupplementary(Float64PlanarAngle angle, double epsilon = 1e-9)
        {
            return Float64PlanarAngleUtils.DegreesToAngle((Degrees + angle.Degrees - 180)).IsNearZeroOrFullRotation(epsilon) ||
                   Float64PlanarAngleUtils.DegreesToAngle((Degrees + angle.Degrees + 180)).IsNearZeroOrFullRotation(epsilon);
        }


        /// <summary>
        /// Get the half angle by direct division of its value by 2
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64PlanarAngle GetHalfAngle()
        {
            return new Float64PlanarAngle(Degrees.Value / 2);
        }
        
        /// <summary>
        /// Convert this angle to the range [0, 360], then get the half angle
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64PlanarAngle GetHalfAngleInPositiveRange()
        {
            return Degrees.IsNegative()
                ? new Float64PlanarAngle((Degrees.Value + 360d) / 2)
                : new Float64PlanarAngle(Degrees.Value / 2);
        }
        
        /// <summary>
        /// Convert this angle to the range [0, 360], then get the half angle
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64PlanarAngle GetHalfAngleInNegativeRange()
        {
            return Degrees.IsPositive()
                ? new Float64PlanarAngle((Degrees.Value - 360d) / 2)
                : new Float64PlanarAngle(Degrees.Value / 2);
        }
        
        /// <summary>
        /// Convert this angle to the range [-180, 180], then get the half angle
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64PlanarAngle GetHalfAngleInSymmetricRange()
        {
            return Degrees.Value switch
            {
                < -180 => new Float64PlanarAngle((Degrees.Value + 360d) / 2),
                > 180 => new Float64PlanarAngle((Degrees.Value - 360d) / 2),
                _ => new Float64PlanarAngle(Degrees.Value / 2)
            };
        }


        /// <summary>
        /// Convert this angle to the range [0, 360] degrees
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64PlanarAngle GetAngleInPositiveRange()
        {
            return Degrees.IsNegative()
                ? new Float64PlanarAngle(Degrees.Value + 360d)
                : this;
        }

        /// <summary>
        /// Convert this angle to the range [-360, 0] degrees
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64PlanarAngle GetAngleInNegativeRange()
        {
            return Degrees.IsPositive()
                ? new Float64PlanarAngle(Degrees.Value - 360d)
                : this;
        }

        /// <summary>
        /// Convert this angle to the range [-180, 180] degrees
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64PlanarAngle GetAngleInSymmetricRange()
        {
            return Degrees.Value switch
            {
                < -180 => new Float64PlanarAngle(Degrees.Value + 360d),
                > 180 => new Float64PlanarAngle(Degrees.Value - 360d),
                _ => this
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pair<Float64Vector2D> RotateBasisFrame2D()
        {
            return new Pair<Float64Vector2D>(
                Rotate(Float64Vector2D.E1),
                Rotate(Float64Vector2D.E2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D Rotate(double x, double y)
        {
            var x1 = x * Cos() - y * Sin();
            var y1 = x * Sin() + y * Cos();

            return Float64Vector2D.Create(x1, y1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D Rotate(LinUnitBasisVector2D axis)
        {
            var (x, y) = axis.ToVector2D();

            var x1 = x * Cos() - y * Sin();
            var y1 = x * Sin() + y * Cos();

            return Float64Vector2D.Create(x1, y1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D Rotate(IFloat64Vector2D vector)
        {
            var x = vector.X;
            var y = vector.Y;

            var x1 = x * Cos() - y * Sin();
            var y1 = x * Sin() + y * Cos();

            return Float64Vector2D.Create(x1, y1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pair<Float64Vector2D> Rotate(IFloat64Vector2D vector1, IFloat64Vector2D vector2)
        {
            return new Pair<Float64Vector2D>(
                Rotate(vector1),
                Rotate(vector2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triplet<Float64Vector2D> Rotate(IFloat64Vector2D vector1, IFloat64Vector2D vector2, IFloat64Vector2D vector3)
        {
            return new Triplet<Float64Vector2D>(
                Rotate(vector1),
                Rotate(vector2),
                Rotate(vector3)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<Float64Vector2D> Rotate(params IFloat64Vector2D[] vectorArray)
        {
            return vectorArray
                .Select(Rotate)
                .ToImmutableArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<Float64Vector2D> Rotate(IEnumerable<IFloat64Vector2D> vectorList)
        {
            return vectorList.Select(Rotate);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"{Degrees:G5} degrees";
        }
    }
}