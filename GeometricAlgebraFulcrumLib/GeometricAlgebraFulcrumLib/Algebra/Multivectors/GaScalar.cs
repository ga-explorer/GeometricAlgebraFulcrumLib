using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors
{
    public sealed record GaScalar<T> : 
        IGaAlgebraElement<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator T(GaScalar<T> d)
        {
            return d.Scalar;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator -(GaScalar<T> s1)
        {
            return new GaScalar<T>(s1.Processor, s1.Processor.Negative(s1.Scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator +(GaScalar<T> s1, GaScalar<T> s2)
        {
            return new GaScalar<T>(s1.Processor, s1.Processor.Add(s1.Scalar, s2.Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator +(GaScalar<T> s1, int s2)
        {
            return new GaScalar<T>(
                s1.Processor, 
                s1.Processor.Add(
                    s1.Scalar, 
                    s1.Processor.IntegerToScalar(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator +(int s1, GaScalar<T> s2)
        {
            return new GaScalar<T>(
                s2.Processor, 
                s2.Processor.Add(
                    s2.Processor.IntegerToScalar(s1),
                    s2.Scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator +(GaScalar<T> s1, double s2)
        {
            return new GaScalar<T>(
                s1.Processor, 
                s1.Processor.Add(
                    s1.Scalar, 
                    s1.Processor.Float64ToScalar(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator +(double s1, GaScalar<T> s2)
        {
            return new GaScalar<T>(
                s2.Processor, 
                s2.Processor.Add(
                    s2.Processor.Float64ToScalar(s1),
                    s2.Scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator +(GaScalar<T> s1, T s2)
        {
            return new GaScalar<T>(s1.Processor, s1.Processor.Add(s1.Scalar, s2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator +(T s1, GaScalar<T> s2)
        {
            return new GaScalar<T>(s2.Processor, s2.Processor.Add(s1, s2.Scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator -(GaScalar<T> s1, GaScalar<T> s2)
        {
            return new GaScalar<T>(s1.Processor, s1.Processor.Subtract(s1.Scalar, s2.Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator -(GaScalar<T> s1, int s2)
        {
            return new GaScalar<T>(
                s1.Processor, 
                s1.Processor.Subtract(
                    s1.Scalar, 
                    s1.Processor.IntegerToScalar(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator -(int s1, GaScalar<T> s2)
        {
            return new GaScalar<T>(
                s2.Processor, 
                s2.Processor.Subtract(
                    s2.Processor.IntegerToScalar(s1),
                    s2.Scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator -(GaScalar<T> s1, double s2)
        {
            return new GaScalar<T>(
                s1.Processor, 
                s1.Processor.Subtract(
                    s1.Scalar, 
                    s1.Processor.Float64ToScalar(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator -(double s1, GaScalar<T> s2)
        {
            return new GaScalar<T>(
                s2.Processor, 
                s2.Processor.Subtract(
                    s2.Processor.Float64ToScalar(s1),
                    s2.Scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator -(GaScalar<T> s1, T s2)
        {
            return new GaScalar<T>(s1.Processor, s1.Processor.Subtract(s1.Scalar, s2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator -(T s1, GaScalar<T> s2)
        {
            return new GaScalar<T>(s2.Processor, s2.Processor.Subtract(s1, s2.Scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator *(GaScalar<T> s1, GaScalar<T> s2)
        {
            return new GaScalar<T>(s1.Processor, s1.Processor.Times(s1.Scalar, s2.Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator *(GaScalar<T> s1, int s2)
        {
            return new GaScalar<T>(
                s1.Processor, 
                s1.Processor.Times(
                    s1.Scalar, 
                    s1.Processor.IntegerToScalar(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator *(int s1, GaScalar<T> s2)
        {
            return new GaScalar<T>(
                s2.Processor, 
                s2.Processor.Times(
                    s2.Processor.IntegerToScalar(s1),
                    s2.Scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator *(GaScalar<T> s1, double s2)
        {
            return new GaScalar<T>(
                s1.Processor, 
                s1.Processor.Times(
                    s1.Scalar, 
                    s1.Processor.Float64ToScalar(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator *(double s1, GaScalar<T> s2)
        {
            return new GaScalar<T>(
                s2.Processor, 
                s2.Processor.Times(
                    s2.Processor.Float64ToScalar(s1),
                    s2.Scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator *(GaScalar<T> s1, T s2)
        {
            return new GaScalar<T>(s1.Processor, s1.Processor.Times(s1.Scalar, s2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator *(T s1, GaScalar<T> s2)
        {
            return new GaScalar<T>(s2.Processor, s2.Processor.Times(s1, s2.Scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator /(GaScalar<T> s1, GaScalar<T> s2)
        {
            return new GaScalar<T>(s1.Processor, s1.Processor.Divide(s1.Scalar, s2.Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator /(GaScalar<T> s1, int s2)
        {
            return new GaScalar<T>(
                s1.Processor, 
                s1.Processor.Divide(
                    s1.Scalar, 
                    s1.Processor.IntegerToScalar(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator /(int s1, GaScalar<T> s2)
        {
            return new GaScalar<T>(
                s2.Processor, 
                s2.Processor.Divide(
                    s2.Processor.IntegerToScalar(s1),
                    s2.Scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator /(GaScalar<T> s1, double s2)
        {
            return new GaScalar<T>(
                s1.Processor, 
                s1.Processor.Divide(
                    s1.Scalar, 
                    s1.Processor.Float64ToScalar(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator /(double s1, GaScalar<T> s2)
        {
            return new GaScalar<T>(
                s2.Processor, 
                s2.Processor.Divide(
                    s2.Processor.Float64ToScalar(s1),
                    s2.Scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator /(GaScalar<T> s1, T s2)
        {
            return new GaScalar<T>(s1.Processor, s1.Processor.Divide(s1.Scalar, s2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> operator /(T s1, GaScalar<T> s2)
        {
            return new GaScalar<T>(s2.Processor, s2.Processor.Divide(s1, s2.Scalar));
        }


        public IGaSpace Space 
            => Processor;

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension 
            => Processor.GaSpaceDimension;

        public IGaProcessor<T> Processor { get; }

        public T Scalar { get; }

        public bool IsNumeric 
            => Processor.IsNumeric;

        public bool IsSymbolic 
            => Processor.IsSymbolic;

        public bool IsValid 
            => Processor.IsValid(Scalar);

        public bool IsZero 
            => Processor.IsZero(Scalar);

        public bool IsOne 
            => Processor.IsOne(Scalar);

        public bool IsMinusOne 
            => Processor.IsMinusOne(Scalar);

        public bool IsNearZero 
            => Processor.IsNearZero(Scalar);

        public bool IsPositive 
            => Processor.IsPositive(Scalar);

        public bool IsNegative 
            => Processor.IsNegative(Scalar);

        public bool IsNotNearPositive 
            => Processor.IsNotNearPositive(Scalar);

        public bool IsNotNearNegative 
            => Processor.IsNotNearNegative(Scalar);


        internal GaScalar([NotNull] IGaProcessor<T> processor, [NotNull] T scalar)
        {
            Processor = processor;
            Scalar = scalar;
        }


        public override string ToString()
        {
            return Processor.ToText(Scalar);
        }
    }
}