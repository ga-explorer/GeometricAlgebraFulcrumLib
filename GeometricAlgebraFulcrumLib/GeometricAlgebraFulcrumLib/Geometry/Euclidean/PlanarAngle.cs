using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean
{
    public sealed record PlanarAngle<T>
    {
        //public static PlanarAngle Angle0 { get; }
        //    = new PlanarAngle(0d);

        //public static PlanarAngle Angle30 { get; }
        //    = new PlanarAngle(30d);

        //public static PlanarAngle Angle60 { get; }
        //    = new PlanarAngle(60d);

        //public static PlanarAngle Angle90 { get; }
        //    = new PlanarAngle(90d);

        //public static PlanarAngle Angle120 { get; }
        //    = new PlanarAngle(120d);

        //public static PlanarAngle Angle150 { get; }
        //    = new PlanarAngle(150d);

        //public static PlanarAngle Angle180 { get; }
        //    = new PlanarAngle(180d);

        //public static PlanarAngle Angle210 { get; }
        //    = new PlanarAngle(210d);
        
        //public static PlanarAngle Angle240 { get; }
        //    = new PlanarAngle(240d);

        //public static PlanarAngle Angle270 { get; }
        //    = new PlanarAngle(270d);

        //public static PlanarAngle Angle300 { get; }
        //    = new PlanarAngle(300d);

        //public static PlanarAngle Angle330 { get; }
        //    = new PlanarAngle(330d);

        //public static PlanarAngle Angle360 { get; }
        //    = new PlanarAngle(360d);

        
        


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator T(PlanarAngle<T> angle)
        {
            return angle.Radians;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle<T> operator -(PlanarAngle<T> angle)
        {
            var scalarProcessor = angle.ScalarProcessor;
            var angleInRadians = scalarProcessor.Negative(angle.Radians);

            return new PlanarAngle<T>(
                scalarProcessor, 
                angleInRadians
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle<T> operator +(PlanarAngle<T> angle1, PlanarAngle<T> angle2)
        {
            var scalarProcessor = angle1.ScalarProcessor;
            var angleInRadians = scalarProcessor.Add(angle1.Radians, angle2.Radians);

            return new PlanarAngle<T>(
                scalarProcessor, 
                angleInRadians
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle<T> operator +(PlanarAngle<T> angle1, T angleInRadians2)
        {
            var scalarProcessor = angle1.ScalarProcessor;
            var angleInRadians = scalarProcessor.Add(angle1.Radians, angleInRadians2);

            return new PlanarAngle<T>(
                scalarProcessor, 
                angleInRadians
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle<T> operator +(T angleInRadians1, PlanarAngle<T> angle2)
        {
            var scalarProcessor = angle2.ScalarProcessor;
            var angleInRadians = scalarProcessor.Add(angleInRadians1, angle2.Radians);

            return new PlanarAngle<T>(
                scalarProcessor, 
                angleInRadians
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle<T> operator -(PlanarAngle<T> angle1, PlanarAngle<T> angle2)
        {
            var scalarProcessor = angle1.ScalarProcessor;
            var angleInRadians = scalarProcessor.Subtract(angle1.Radians, angle2.Radians);

            return new PlanarAngle<T>(
                scalarProcessor, 
                angleInRadians
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle<T> operator -(PlanarAngle<T> angle1, T angleInRadians2)
        {
            var scalarProcessor = angle1.ScalarProcessor;
            var angleInRadians = scalarProcessor.Subtract(angle1.Radians, angleInRadians2);

            return new PlanarAngle<T>(
                scalarProcessor, 
                angleInRadians
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle<T> operator -(T angleInRadians1, PlanarAngle<T> angle2)
        {
            var scalarProcessor = angle2.ScalarProcessor;
            var angleInRadians = scalarProcessor.Subtract(angleInRadians1, angle2.Radians);

            return new PlanarAngle<T>(
                scalarProcessor, 
                angleInRadians
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle<T> operator *(PlanarAngle<T> angle, T number)
        {
            var scalarProcessor = angle.ScalarProcessor;
            var angleInRadians = scalarProcessor.Times(angle.Radians, number);

            return new PlanarAngle<T>(
                scalarProcessor, 
                angleInRadians
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle<T> operator *(T number, PlanarAngle<T> angle)
        {
            var scalarProcessor = angle.ScalarProcessor;
            var angleInRadians = scalarProcessor.Times(number, angle.Radians);

            return new PlanarAngle<T>(
                scalarProcessor, 
                angleInRadians
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle<T> operator /(PlanarAngle<T> angle, T number)
        {
            var scalarProcessor = angle.ScalarProcessor;
            var angleInRadians = scalarProcessor.Divide(angle.Radians, number);

            return new PlanarAngle<T>(
                scalarProcessor, 
                angleInRadians
            );
        }


        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        public T Radians { get; }

        public T Degrees 
            => ScalarProcessor.RadiansToDegrees(Radians);

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal PlanarAngle([NotNull] IScalarAlgebraProcessor<T> scalarProcessor, T angleInRadians)
        {
            ScalarProcessor = scalarProcessor;
            Radians = angleInRadians;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> Cos()
        {
            return ScalarProcessor.Cos(Radians).CreateScalar(ScalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> Sin()
        {
            return ScalarProcessor.Sin(Radians).CreateScalar(ScalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> Tan()
        {
            return ScalarProcessor.Tan(Radians).CreateScalar(ScalarProcessor);
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public PlanarAngle ClampPositive()
        //{
        //    return (Degrees < 0d)
        //        ? new PlanarAngle(Degrees + 360d)
        //        : this;
        //}
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public PlanarAngle ClampNegative()
        //{
        //    const T maxValue = 360d;

        //    var value = Degrees + 180d;

        //    return value switch
        //    {
        //        //value < -maxValue
        //        < -maxValue => new PlanarAngle(value + Math.Ceiling(-value / maxValue) * maxValue),

        //        //-maxValue <= value < 0
        //        < 0 => new PlanarAngle(value + maxValue),

        //        //value > maxValue
        //        > maxValue => new PlanarAngle(value - Math.Truncate(value / maxValue) * maxValue),

        //        //0 <= value <= maxValue
        //        _ => new PlanarAngle(value)
        //    };
        //}
        
        //public PlanarAngle ClampPeriodic(T maxAngleInDegrees)
        //{
        //    //Make sure maxValue > 0
        //    Debug.Assert(maxAngleInDegrees is > 0d and <= 360d);

        //    //value < -maxValue
        //    if (Degrees < -maxAngleInDegrees)
        //        return new PlanarAngle(Degrees + Math.Ceiling(-Degrees / maxAngleInDegrees) * maxAngleInDegrees);

        //    //-maxValue <= value < 0
        //    if (Degrees < 0)
        //        return new PlanarAngle(Degrees + maxAngleInDegrees);

        //    //value > maxValue
        //    if (Degrees > maxAngleInDegrees)
        //        return new PlanarAngle(Degrees - Math.Truncate(Degrees / maxAngleInDegrees) * maxAngleInDegrees);

        //    //0 <= value <= maxValue
        //    return new PlanarAngle(Degrees);
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public bool IsValid()
        //{
        //    return Degrees.IsValid() &&
        //           Degrees is >= -360d and <= 360d;
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"{Degrees:G5} degrees";
        }
    }
}