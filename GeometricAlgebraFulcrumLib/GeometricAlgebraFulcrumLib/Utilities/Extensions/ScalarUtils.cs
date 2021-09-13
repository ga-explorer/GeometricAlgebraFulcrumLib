using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class ScalarUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this Scalar<T> scalar1, int scalar2)
        {
            var processor = scalar1.ScalarProcessor;

            return processor.IsZero(
                processor.Subtract(
                    scalar1.ScalarValue, 
                    processor.GetScalarFromNumber(scalar2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this int scalar1, Scalar<T> scalar2)
        {
            var processor = scalar2.ScalarProcessor;

            return processor.IsZero(
                processor.Subtract(
                    processor.GetScalarFromNumber(scalar1),
                    scalar2.ScalarValue
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this Scalar<T> scalar1, uint scalar2)
        {
            var processor = scalar1.ScalarProcessor;

            return processor.IsZero(
                processor.Subtract(
                    scalar1.ScalarValue, 
                    processor.GetScalarFromNumber(scalar2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this uint scalar1, Scalar<T> scalar2)
        {
            var processor = scalar2.ScalarProcessor;

            return processor.IsZero(
                processor.Subtract(
                    processor.GetScalarFromNumber(scalar1),
                    scalar2.ScalarValue
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this Scalar<T> scalar1, long scalar2)
        {
            var processor = scalar1.ScalarProcessor;

            return processor.IsZero(
                processor.Subtract(
                    scalar1.ScalarValue, 
                    processor.GetScalarFromNumber(scalar2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this long scalar1, Scalar<T> scalar2)
        {
            var processor = scalar2.ScalarProcessor;

            return processor.IsZero(
                processor.Subtract(
                    processor.GetScalarFromNumber(scalar1),
                    scalar2.ScalarValue
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this Scalar<T> scalar1, ulong scalar2)
        {
            var processor = scalar1.ScalarProcessor;

            return processor.IsZero(
                processor.Subtract(
                    scalar1.ScalarValue, 
                    processor.GetScalarFromNumber(scalar2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this ulong scalar1, Scalar<T> scalar2)
        {
            var processor = scalar2.ScalarProcessor;

            return processor.IsZero(
                processor.Subtract(
                    processor.GetScalarFromNumber(scalar1),
                    scalar2.ScalarValue
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this Scalar<T> scalar1, float scalar2)
        {
            var processor = scalar1.ScalarProcessor;

            return processor.IsZero(
                processor.Subtract(
                    scalar1.ScalarValue, 
                    processor.GetScalarFromNumber(scalar2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this float scalar1, Scalar<T> scalar2)
        {
            var processor = scalar2.ScalarProcessor;

            return processor.IsZero(
                processor.Subtract(
                    processor.GetScalarFromNumber(scalar1),
                    scalar2.ScalarValue
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this Scalar<T> scalar1, double scalar2)
        {
            var processor = scalar1.ScalarProcessor;

            return processor.IsZero(
                processor.Subtract(
                    scalar1.ScalarValue, 
                    processor.GetScalarFromNumber(scalar2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this double scalar1, Scalar<T> scalar2)
        {
            var processor = scalar2.ScalarProcessor;

            return processor.IsZero(
                processor.Subtract(
                    processor.GetScalarFromNumber(scalar1),
                    scalar2.ScalarValue
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this Scalar<T> scalar1, T scalar2)
        {
            var processor = scalar1.ScalarProcessor;

            return processor.IsZero(
                processor.Subtract(
                    scalar1.ScalarValue, 
                    scalar2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this T scalar1, Scalar<T> scalar2)
        {
            var processor = scalar2.ScalarProcessor;

            return processor.IsZero(
                processor.Subtract(
                    scalar1, 
                    scalar2.ScalarValue
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this Scalar<T> scalar1, string scalar2)
        {
            var processor = scalar1.ScalarProcessor;

            return processor.IsZero(
                processor.Subtract(
                    scalar1.ScalarValue, 
                    processor.GetScalarFromText(scalar2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this string scalar1, Scalar<T> scalar2)
        {
            var processor = scalar2.ScalarProcessor;

            return processor.IsZero(
                processor.Subtract(
                    processor.GetScalarFromText(scalar1),
                    scalar2.ScalarValue
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this Scalar<T> scalar1, object scalar2)
        {
            var processor = scalar1.ScalarProcessor;

            return processor.IsZero(
                processor.Subtract(
                    scalar1.ScalarValue, 
                    processor.GetScalarFromObject(scalar2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this object scalar1, Scalar<T> scalar2)
        {
            var processor = scalar2.ScalarProcessor;

            return processor.IsZero(
                processor.Subtract(
                    processor.GetScalarFromObject(scalar1),
                    scalar2.ScalarValue
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this Scalar<T> scalar1, Scalar<T> scalar2)
        {
            var processor = scalar1.ScalarProcessor;

            return processor.IsZero(
                processor.Subtract(
                    scalar1.ScalarValue, 
                    scalar2.ScalarValue
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Inverse<T>(this Scalar<T> scalar)
        {
            var processor = scalar.ScalarProcessor;

            return new Scalar<T>(
                processor, 
                processor.Inverse(scalar.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Abs<T>(this Scalar<T> scalar)
        {
            var processor = scalar.ScalarProcessor;

            return new Scalar<T>(
                processor, 
                processor.Abs(scalar.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sqrt<T>(this Scalar<T> scalar)
        {
            var processor = scalar.ScalarProcessor;

            return new Scalar<T>(
                processor, 
                processor.Sqrt(scalar.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> SqrtOfAbs<T>(this Scalar<T> scalar)
        {
            var processor = scalar.ScalarProcessor;

            return new Scalar<T>(
                processor, 
                processor.SqrtOfAbs(scalar.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Exp<T>(this Scalar<T> scalar)
        {
            var processor = scalar.ScalarProcessor;

            return new Scalar<T>(
                processor, 
                processor.Exp(scalar.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Log<T>(this Scalar<T> scalar)
        {
            var processor = scalar.ScalarProcessor;

            return new Scalar<T>(
                processor, 
                processor.LogE(scalar.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Log2<T>(this Scalar<T> scalar)
        {
            var processor = scalar.ScalarProcessor;

            return new Scalar<T>(
                processor, 
                processor.Log2(scalar.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Log10<T>(this Scalar<T> scalar)
        {
            var processor = scalar.ScalarProcessor;

            return new Scalar<T>(
                processor, 
                processor.Log10(scalar.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Log<T>(this Scalar<T> scalar, T baseScalar)
        {
            var processor = scalar.ScalarProcessor;

            return new Scalar<T>(
                processor, 
                processor.Log(baseScalar, scalar.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Log<T>(this Scalar<T> scalar, Scalar<T> baseScalar)
        {
            var processor = scalar.ScalarProcessor;

            return new Scalar<T>(
                processor, 
                processor.Log(baseScalar.ScalarValue, scalar.ScalarValue)
            );
        }
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Cos<T>(this Scalar<T> scalar)
        {
            var processor = scalar.ScalarProcessor;

            return new Scalar<T>(
                processor, 
                processor.Cos(scalar.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sin<T>(this Scalar<T> scalar)
        {
            var processor = scalar.ScalarProcessor;

            return new Scalar<T>(
                processor, 
                processor.Sin(scalar.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Tan<T>(this Scalar<T> scalar)
        {
            var processor = scalar.ScalarProcessor;

            return new Scalar<T>(
                processor, 
                processor.Tan(scalar.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ArcCos<T>(this Scalar<T> scalar)
        {
            var processor = scalar.ScalarProcessor;

            return new Scalar<T>(
                processor, 
                processor.ArcCos(scalar.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ArcSin<T>(this Scalar<T> scalar)
        {
            var processor = scalar.ScalarProcessor;

            return new Scalar<T>(
                processor, 
                processor.ArcSin(scalar.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ArcTan<T>(this Scalar<T> scalar)
        {
            var processor = scalar.ScalarProcessor;

            return new Scalar<T>(
                processor, 
                processor.ArcTan(scalar.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ArcTan2<T>(this Scalar<T> scalarX, T scalarY)
        {
            var processor = scalarX.ScalarProcessor;

            return new Scalar<T>(
                processor, 
                processor.ArcTan2(scalarX.ScalarValue, scalarY)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ArcTan2<T>(this Scalar<T> scalarX, Scalar<T> scalarY)
        {
            var processor = scalarX.ScalarProcessor;

            return new Scalar<T>(
                processor, 
                processor.ArcTan2(scalarX.ScalarValue, scalarY.ScalarValue)
            );
        }
  
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Cosh<T>(this Scalar<T> scalar)
        {
            var processor = scalar.ScalarProcessor;

            return new Scalar<T>(
                processor, 
                processor.Cosh(scalar.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Sinh<T>(this Scalar<T> scalar)
        {
            var processor = scalar.ScalarProcessor;

            return new Scalar<T>(
                processor, 
                processor.Sinh(scalar.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Tanh<T>(this Scalar<T> scalar)
        {
            var processor = scalar.ScalarProcessor;

            return new Scalar<T>(
                processor, 
                processor.Tanh(scalar.ScalarValue)
            );
        }
    }
}