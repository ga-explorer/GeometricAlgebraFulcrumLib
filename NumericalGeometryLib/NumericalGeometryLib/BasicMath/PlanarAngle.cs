using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Tuples;

namespace NumericalGeometryLib.BasicMath
{
    public sealed record PlanarAngle :
        IGeometricElement
    {
        private const double DegreeToRadianFactor = Math.PI / 180d;
        private const double RadianToDegreeFactor = 180d / Math.PI;


        public static PlanarAngle Angle0 { get; }
            = new PlanarAngle(0d);

        public static PlanarAngle Angle30 { get; }
            = new PlanarAngle(30d);

        public static PlanarAngle Angle60 { get; }
            = new PlanarAngle(60d);

        public static PlanarAngle Angle90 { get; }
            = new PlanarAngle(90d);

        public static PlanarAngle Angle120 { get; }
            = new PlanarAngle(120d);

        public static PlanarAngle Angle150 { get; }
            = new PlanarAngle(150d);

        public static PlanarAngle Angle180 { get; }
            = new PlanarAngle(180d);

        public static PlanarAngle Angle210 { get; }
            = new PlanarAngle(210d);
        
        public static PlanarAngle Angle240 { get; }
            = new PlanarAngle(240d);

        public static PlanarAngle Angle270 { get; }
            = new PlanarAngle(270d);

        public static PlanarAngle Angle300 { get; }
            = new PlanarAngle(300d);

        public static PlanarAngle Angle330 { get; }
            = new PlanarAngle(330d);

        public static PlanarAngle Angle360 { get; }
            = new PlanarAngle(360d);

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle CreateFromDegrees(double angleInDegrees)
        {
            return angleInDegrees switch
            {
                < -360d => new PlanarAngle((angleInDegrees % 720d) + 360d),
                > 360 => new PlanarAngle(angleInDegrees % 360d),
                _ => new PlanarAngle(angleInDegrees)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle CreateFromRadians(double angleInRadians)
        {
            return CreateFromDegrees(
                angleInRadians * RadianToDegreeFactor
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle CreateFromUnitVectors(ITuple2D v1, ITuple2D v2)
        {
            return CreateFromDegrees(
                Math.Acos(v1.VectorDot(v2)) * RadianToDegreeFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle CreateFromUnitVectors(ITuple3D v1, ITuple3D v2)
        {
            return CreateFromDegrees(
                Math.Acos(v1.VectorDot(v2)) * RadianToDegreeFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle CreateFromUnitVectors(ITuple4D v1, ITuple4D v2)
        {
            return CreateFromDegrees(
                Math.Acos(v1.VectorDot(v2)) * RadianToDegreeFactor
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator double(PlanarAngle angle)
        {
            return angle.Radians;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator PlanarAngle(double angleInRadians)
        {
            return CreateFromDegrees(angleInRadians * RadianToDegreeFactor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle operator -(PlanarAngle angle)
        {
            return CreateFromDegrees(-angle.Degrees);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle operator +(PlanarAngle angle1, PlanarAngle angle2)
        {
            return CreateFromDegrees(angle1.Degrees + angle2.Degrees);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle operator +(PlanarAngle angle1, double angleInRadians2)
        {
            return CreateFromDegrees(angle1.Degrees + angleInRadians2 * RadianToDegreeFactor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle operator +(double angleInRadians1, PlanarAngle angle2)
        {
            return CreateFromDegrees(angleInRadians1 * RadianToDegreeFactor + angle2.Degrees);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle operator -(PlanarAngle angle1, PlanarAngle angle2)
        {
            return CreateFromDegrees(angle1.Degrees - angle2.Degrees);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle operator -(PlanarAngle angle1, double angleInRadians2)
        {
            return CreateFromDegrees(angle1.Degrees - angleInRadians2 * RadianToDegreeFactor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle operator -(double angleInRadians1, PlanarAngle angle2)
        {
            return CreateFromDegrees(angleInRadians1 * RadianToDegreeFactor - angle2.Degrees);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle operator *(PlanarAngle angle, double number)
        {
            return CreateFromDegrees(angle.Degrees * number);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle operator *(double number, PlanarAngle angle)
        {
            return CreateFromDegrees(number * angle.Degrees);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle operator /(PlanarAngle angle, double number)
        {
            return CreateFromDegrees(angle.Degrees / number);
        }

        private double? _cosAngle;
        private double? _sinAngle;

        public double Degrees { get; }

        public double Radians 
            => Degrees * DegreeToRadianFactor;

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private PlanarAngle(double degrees)
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
        public PlanarAngle ClampPositive()
        {
            return (Degrees < 0d)
                ? new PlanarAngle(Degrees + 360d)
                : this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PlanarAngle ClampNegative()
        {
            const double maxValue = 360d;

            var value = Degrees + 180d;

            return value switch
            {
                //value < -maxValue
                < -maxValue => new PlanarAngle(value + Math.Ceiling(-value / maxValue) * maxValue),

                //-maxValue <= value < 0
                < 0 => new PlanarAngle(value + maxValue),

                //value > maxValue
                > maxValue => new PlanarAngle(value - Math.Truncate(value / maxValue) * maxValue),

                //0 <= value <= maxValue
                _ => new PlanarAngle(value)
            };
        }
        
        public PlanarAngle ClampPeriodic(double maxAngleInDegrees)
        {
            //Make sure maxValue > 0
            Debug.Assert(maxAngleInDegrees is > 0d and <= 360d);

            //value < -maxValue
            if (Degrees < -maxAngleInDegrees)
                return new PlanarAngle(Degrees + Math.Ceiling(-Degrees / maxAngleInDegrees) * maxAngleInDegrees);

            //-maxValue <= value < 0
            if (Degrees < 0)
                return new PlanarAngle(Degrees + maxAngleInDegrees);

            //value > maxValue
            if (Degrees > maxAngleInDegrees)
                return new PlanarAngle(Degrees - Math.Truncate(Degrees / maxAngleInDegrees) * maxAngleInDegrees);

            //0 <= value <= maxValue
            return new PlanarAngle(Degrees);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Degrees.IsValid() &&
                   Degrees is >= -360d and <= 360d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"{Degrees:G5} degrees";
        }
    }
}