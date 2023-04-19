using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Coordinates;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath
{
    public sealed record Float64PlanarAngle :
        IGeometricElement
    {
        private const double DegreeToRadianFactor = Math.PI / 180d;
        private const double RadianToDegreeFactor = 180d / Math.PI;


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
            return angleInDegrees switch
            {
                < -360d => new Float64PlanarAngle(angleInDegrees % 720d + 360d),
                > 360 => new Float64PlanarAngle(angleInDegrees % 360d),
                _ => new Float64PlanarAngle(angleInDegrees)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle CreateFromRadians(double angleInRadians)
        {
            return CreateFromDegrees(
                angleInRadians * RadianToDegreeFactor
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle CreateFromUnitVectors(IFloat64Tuple2D v1, IFloat64Tuple2D v2)
        {
            return CreateFromDegrees(
                Math.Acos(v1.VectorDot(v2)) * RadianToDegreeFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle CreateFromUnitVectors(IFloat64Tuple3D v1, IFloat64Tuple3D v2)
        {
            return CreateFromDegrees(
                Math.Acos(v1.VectorDot(v2)) * RadianToDegreeFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle CreateFromUnitVectors(IFloat64Tuple4D v1, IFloat64Tuple4D v2)
        {
            return CreateFromDegrees(
                Math.Acos(v1.VectorDot(v2)) * RadianToDegreeFactor
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator double(Float64PlanarAngle angle)
        {
            return angle.Radians;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Float64PlanarAngle(double angleInRadians)
        {
            return CreateFromDegrees(angleInRadians * RadianToDegreeFactor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle operator -(Float64PlanarAngle angle)
        {
            return CreateFromDegrees(-angle.Degrees);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle operator +(Float64PlanarAngle angle1, Float64PlanarAngle angle2)
        {
            return CreateFromDegrees(angle1.Degrees + angle2.Degrees);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle operator +(Float64PlanarAngle angle1, double angleInRadians2)
        {
            return CreateFromDegrees(angle1.Degrees + angleInRadians2 * RadianToDegreeFactor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle operator +(double angleInRadians1, Float64PlanarAngle angle2)
        {
            return CreateFromDegrees(angleInRadians1 * RadianToDegreeFactor + angle2.Degrees);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle operator -(Float64PlanarAngle angle1, Float64PlanarAngle angle2)
        {
            return CreateFromDegrees(angle1.Degrees - angle2.Degrees);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle operator -(Float64PlanarAngle angle1, double angleInRadians2)
        {
            return CreateFromDegrees(angle1.Degrees - angleInRadians2 * RadianToDegreeFactor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle operator -(double angleInRadians1, Float64PlanarAngle angle2)
        {
            return CreateFromDegrees(angleInRadians1 * RadianToDegreeFactor - angle2.Degrees);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle operator *(Float64PlanarAngle angle, double number)
        {
            return CreateFromDegrees(angle.Degrees * number);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle operator *(double number, Float64PlanarAngle angle)
        {
            return CreateFromDegrees(number * angle.Degrees);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle operator /(Float64PlanarAngle angle, double number)
        {
            return CreateFromDegrees(angle.Degrees / number);
        }

        private double? _cosAngle;
        private double? _sinAngle;

        public double Degrees { get; }

        public double Radians 
            => Degrees * DegreeToRadianFactor;

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Float64PlanarAngle(double degrees)
        {
            Degrees = degrees;

            Debug.Assert(IsValid());
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Cos()
        {
            _cosAngle ??= Math.Cos(Radians);

            return _cosAngle.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Sin()
        {
            _sinAngle ??= Math.Sin(Radians);

            return _sinAngle.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Tan()
        {
            return Sin() / Cos();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64PlanarAngle ClampPositive()
        {
            return Degrees < 0d
                ? new Float64PlanarAngle(Degrees + 360d)
                : this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64PlanarAngle ClampNegative()
        {
            const double maxValue = 360d;

            var value = Degrees + 180d;

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
        
        public Float64PlanarAngle ClampPeriodic(double maxAngleInDegrees)
        {
            //Make sure maxValue > 0
            Debug.Assert(maxAngleInDegrees is > 0d and <= 360d);

            //value < -maxValue
            if (Degrees < -maxAngleInDegrees)
                return new Float64PlanarAngle(Degrees + Math.Ceiling(-Degrees / maxAngleInDegrees) * maxAngleInDegrees);

            //-maxValue <= value < 0
            if (Degrees < 0)
                return new Float64PlanarAngle(Degrees + maxAngleInDegrees);

            //value > maxValue
            if (Degrees > maxAngleInDegrees)
                return new Float64PlanarAngle(Degrees - Math.Truncate(Degrees / maxAngleInDegrees) * maxAngleInDegrees);

            //0 <= value <= maxValue
            return new Float64PlanarAngle(Degrees);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolarPosition2D ToPolarPosition()
        {
            return new PolarPosition2D(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolarPosition2D ToPolarPosition(double r)
        {
            return new PolarPosition2D(r, this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return !Degrees.IsNaNOrInfinite() &&
                   Degrees is >= -360d and <= 360d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"{Degrees:G5} degrees";
        }
    }
}