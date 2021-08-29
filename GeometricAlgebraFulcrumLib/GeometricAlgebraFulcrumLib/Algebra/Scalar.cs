using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Algebra
{
    public sealed record Scalar<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator T(Scalar<T> d)
        {
            return d.ScalarValue;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator -(Scalar<T> s1)
        {
            return new Scalar<T>(s1.ScalarProcessor, s1.ScalarProcessor.Negative(s1.ScalarValue));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator +(Scalar<T> s1, Scalar<T> s2)
        {
            return new Scalar<T>(s1.ScalarProcessor, s1.ScalarProcessor.Add(s1.ScalarValue, s2.ScalarValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator +(Scalar<T> s1, int s2)
        {
            return new Scalar<T>(
                s1.ScalarProcessor, 
                s1.ScalarProcessor.Add(
                    s1.ScalarValue, 
                    s1.ScalarProcessor.GetScalarFromInteger(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator +(int s1, Scalar<T> s2)
        {
            return new Scalar<T>(
                s2.ScalarProcessor, 
                s2.ScalarProcessor.Add(
                    s2.ScalarProcessor.GetScalarFromInteger(s1),
                    s2.ScalarValue
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator +(Scalar<T> s1, double s2)
        {
            return new Scalar<T>(
                s1.ScalarProcessor, 
                s1.ScalarProcessor.Add(
                    s1.ScalarValue, 
                    s1.ScalarProcessor.GetScalarFromFloat64(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator +(double s1, Scalar<T> s2)
        {
            return new Scalar<T>(
                s2.ScalarProcessor, 
                s2.ScalarProcessor.Add(
                    s2.ScalarProcessor.GetScalarFromFloat64(s1),
                    s2.ScalarValue
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator +(Scalar<T> s1, T s2)
        {
            return new Scalar<T>(s1.ScalarProcessor, s1.ScalarProcessor.Add(s1.ScalarValue, s2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator +(T s1, Scalar<T> s2)
        {
            return new Scalar<T>(s2.ScalarProcessor, s2.ScalarProcessor.Add(s1, s2.ScalarValue));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator -(Scalar<T> s1, Scalar<T> s2)
        {
            return new Scalar<T>(s1.ScalarProcessor, s1.ScalarProcessor.Subtract(s1.ScalarValue, s2.ScalarValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator -(Scalar<T> s1, int s2)
        {
            return new Scalar<T>(
                s1.ScalarProcessor, 
                s1.ScalarProcessor.Subtract(
                    s1.ScalarValue, 
                    s1.ScalarProcessor.GetScalarFromInteger(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator -(int s1, Scalar<T> s2)
        {
            return new Scalar<T>(
                s2.ScalarProcessor, 
                s2.ScalarProcessor.Subtract(
                    s2.ScalarProcessor.GetScalarFromInteger(s1),
                    s2.ScalarValue
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator -(Scalar<T> s1, double s2)
        {
            return new Scalar<T>(
                s1.ScalarProcessor, 
                s1.ScalarProcessor.Subtract(
                    s1.ScalarValue, 
                    s1.ScalarProcessor.GetScalarFromFloat64(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator -(double s1, Scalar<T> s2)
        {
            return new Scalar<T>(
                s2.ScalarProcessor, 
                s2.ScalarProcessor.Subtract(
                    s2.ScalarProcessor.GetScalarFromFloat64(s1),
                    s2.ScalarValue
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator -(Scalar<T> s1, T s2)
        {
            return new Scalar<T>(s1.ScalarProcessor, s1.ScalarProcessor.Subtract(s1.ScalarValue, s2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator -(T s1, Scalar<T> s2)
        {
            return new Scalar<T>(s2.ScalarProcessor, s2.ScalarProcessor.Subtract(s1, s2.ScalarValue));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator *(Scalar<T> s1, Scalar<T> s2)
        {
            return new Scalar<T>(s1.ScalarProcessor, s1.ScalarProcessor.Times(s1.ScalarValue, s2.ScalarValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator *(Scalar<T> s1, int s2)
        {
            return new Scalar<T>(
                s1.ScalarProcessor, 
                s1.ScalarProcessor.Times(
                    s1.ScalarValue, 
                    s1.ScalarProcessor.GetScalarFromInteger(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator *(int s1, Scalar<T> s2)
        {
            return new Scalar<T>(
                s2.ScalarProcessor, 
                s2.ScalarProcessor.Times(
                    s2.ScalarProcessor.GetScalarFromInteger(s1),
                    s2.ScalarValue
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator *(Scalar<T> s1, double s2)
        {
            return new Scalar<T>(
                s1.ScalarProcessor, 
                s1.ScalarProcessor.Times(
                    s1.ScalarValue, 
                    s1.ScalarProcessor.GetScalarFromFloat64(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator *(double s1, Scalar<T> s2)
        {
            return new Scalar<T>(
                s2.ScalarProcessor, 
                s2.ScalarProcessor.Times(
                    s2.ScalarProcessor.GetScalarFromFloat64(s1),
                    s2.ScalarValue
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator *(Scalar<T> s1, T s2)
        {
            return new Scalar<T>(s1.ScalarProcessor, s1.ScalarProcessor.Times(s1.ScalarValue, s2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator *(T s1, Scalar<T> s2)
        {
            return new Scalar<T>(s2.ScalarProcessor, s2.ScalarProcessor.Times(s1, s2.ScalarValue));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator /(Scalar<T> s1, Scalar<T> s2)
        {
            return new Scalar<T>(s1.ScalarProcessor, s1.ScalarProcessor.Divide(s1.ScalarValue, s2.ScalarValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator /(Scalar<T> s1, int s2)
        {
            return new Scalar<T>(
                s1.ScalarProcessor, 
                s1.ScalarProcessor.Divide(
                    s1.ScalarValue, 
                    s1.ScalarProcessor.GetScalarFromInteger(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator /(int s1, Scalar<T> s2)
        {
            return new Scalar<T>(
                s2.ScalarProcessor, 
                s2.ScalarProcessor.Divide(
                    s2.ScalarProcessor.GetScalarFromInteger(s1),
                    s2.ScalarValue
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator /(Scalar<T> s1, double s2)
        {
            return new Scalar<T>(
                s1.ScalarProcessor, 
                s1.ScalarProcessor.Divide(
                    s1.ScalarValue, 
                    s1.ScalarProcessor.GetScalarFromFloat64(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator /(double s1, Scalar<T> s2)
        {
            return new Scalar<T>(
                s2.ScalarProcessor, 
                s2.ScalarProcessor.Divide(
                    s2.ScalarProcessor.GetScalarFromFloat64(s1),
                    s2.ScalarValue
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator /(Scalar<T> s1, T s2)
        {
            return new Scalar<T>(s1.ScalarProcessor, s1.ScalarProcessor.Divide(s1.ScalarValue, s2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> operator /(T s1, Scalar<T> s2)
        {
            return new Scalar<T>(s2.ScalarProcessor, s2.ScalarProcessor.Divide(s1, s2.ScalarValue));
        }

        
        public IScalarProcessor<T> ScalarProcessor { get; }

        public T ScalarValue { get; }

        public bool IsNumeric 
            => ScalarProcessor.IsNumeric;

        public bool IsSymbolic 
            => ScalarProcessor.IsSymbolic;

        public bool IsValid 
            => ScalarProcessor.IsValid(ScalarValue);

        public bool IsZero 
            => ScalarProcessor.IsZero(ScalarValue);

        public bool IsOne 
            => ScalarProcessor.IsOne(ScalarValue);

        public bool IsMinusOne 
            => ScalarProcessor.IsMinusOne(ScalarValue);

        public bool IsNearZero 
            => ScalarProcessor.IsNearZero(ScalarValue);

        public bool IsPositive 
            => ScalarProcessor.IsPositive(ScalarValue);

        public bool IsNegative 
            => ScalarProcessor.IsNegative(ScalarValue);

        public bool IsNotNearPositive 
            => ScalarProcessor.IsNotNearPositive(ScalarValue);

        public bool IsNotNearNegative 
            => ScalarProcessor.IsNotNearNegative(ScalarValue);


        internal Scalar([NotNull] IScalarProcessor<T> processor, [NotNull] T scalar)
        {
            ScalarProcessor = processor;
            ScalarValue = scalar;
        }


        public override string ToString()
        {
            return ScalarProcessor.ToText(ScalarValue);
        }
    }
}