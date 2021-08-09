using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils
{
    public static class GaScalarUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this GaScalar<T> scalar1, int scalar2)
        {
            var processor = scalar1.Processor;

            return processor.IsZero(
                processor.Subtract(
                    scalar1.Scalar, 
                    processor.IntegerToScalar(scalar2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this int scalar1, GaScalar<T> scalar2)
        {
            var processor = scalar2.Processor;

            return processor.IsZero(
                processor.Subtract(
                    processor.IntegerToScalar(scalar1),
                    scalar2.Scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this GaScalar<T> scalar1, double scalar2)
        {
            var processor = scalar1.Processor;

            return processor.IsZero(
                processor.Subtract(
                    scalar1.Scalar, 
                    processor.Float64ToScalar(scalar2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this double scalar1, GaScalar<T> scalar2)
        {
            var processor = scalar2.Processor;

            return processor.IsZero(
                processor.Subtract(
                    processor.Float64ToScalar(scalar1),
                    scalar2.Scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this GaScalar<T> scalar1, T scalar2)
        {
            var processor = scalar1.Processor;

            return processor.IsZero(
                processor.Subtract(
                    scalar1.Scalar, 
                    scalar2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this T scalar1, GaScalar<T> scalar2)
        {
            var processor = scalar2.Processor;

            return processor.IsZero(
                processor.Subtract(
                    scalar1, 
                    scalar2.Scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this GaScalar<T> scalar1, GaScalar<T> scalar2)
        {
            var processor = scalar1.Processor;

            return processor.IsZero(
                processor.Subtract(
                    scalar1.Scalar, 
                    scalar2.Scalar
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> Inverse<T>(this GaScalar<T> scalar)
        {
            var processor = scalar.Processor;

            return new GaScalar<T>(
                processor, 
                processor.Inverse(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> Abs<T>(this GaScalar<T> scalar)
        {
            var processor = scalar.Processor;

            return new GaScalar<T>(
                processor, 
                processor.Abs(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> Sqrt<T>(this GaScalar<T> scalar)
        {
            var processor = scalar.Processor;

            return new GaScalar<T>(
                processor, 
                processor.Sqrt(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> SqrtOfAbs<T>(this GaScalar<T> scalar)
        {
            var processor = scalar.Processor;

            return new GaScalar<T>(
                processor, 
                processor.SqrtOfAbs(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> Exp<T>(this GaScalar<T> scalar)
        {
            var processor = scalar.Processor;

            return new GaScalar<T>(
                processor, 
                processor.Exp(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> Log<T>(this GaScalar<T> scalar)
        {
            var processor = scalar.Processor;

            return new GaScalar<T>(
                processor, 
                processor.Log(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> Log2<T>(this GaScalar<T> scalar)
        {
            var processor = scalar.Processor;

            return new GaScalar<T>(
                processor, 
                processor.Log2(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> Log10<T>(this GaScalar<T> scalar)
        {
            var processor = scalar.Processor;

            return new GaScalar<T>(
                processor, 
                processor.Log10(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> Log<T>(this GaScalar<T> scalar, T baseScalar)
        {
            var processor = scalar.Processor;

            return new GaScalar<T>(
                processor, 
                processor.Log(scalar.Scalar, baseScalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> Log<T>(this GaScalar<T> scalar, GaScalar<T> baseScalar)
        {
            var processor = scalar.Processor;

            return new GaScalar<T>(
                processor, 
                processor.Log(scalar.Scalar, baseScalar.Scalar)
            );
        }
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> Cos<T>(this GaScalar<T> scalar)
        {
            var processor = scalar.Processor;

            return new GaScalar<T>(
                processor, 
                processor.Cos(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> Sin<T>(this GaScalar<T> scalar)
        {
            var processor = scalar.Processor;

            return new GaScalar<T>(
                processor, 
                processor.Sin(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> Tan<T>(this GaScalar<T> scalar)
        {
            var processor = scalar.Processor;

            return new GaScalar<T>(
                processor, 
                processor.Tan(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> ArcCos<T>(this GaScalar<T> scalar)
        {
            var processor = scalar.Processor;

            return new GaScalar<T>(
                processor, 
                processor.ArcCos(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> ArcSin<T>(this GaScalar<T> scalar)
        {
            var processor = scalar.Processor;

            return new GaScalar<T>(
                processor, 
                processor.ArcSin(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> ArcTan<T>(this GaScalar<T> scalar)
        {
            var processor = scalar.Processor;

            return new GaScalar<T>(
                processor, 
                processor.ArcTan(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> ArcTan2<T>(this GaScalar<T> scalarX, T scalarY)
        {
            var processor = scalarX.Processor;

            return new GaScalar<T>(
                processor, 
                processor.ArcTan2(scalarX.Scalar, scalarY)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> ArcTan2<T>(this GaScalar<T> scalarX, GaScalar<T> scalarY)
        {
            var processor = scalarX.Processor;

            return new GaScalar<T>(
                processor, 
                processor.ArcTan2(scalarX.Scalar, scalarY.Scalar)
            );
        }
  
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> Cosh<T>(this GaScalar<T> scalar)
        {
            var processor = scalar.Processor;

            return new GaScalar<T>(
                processor, 
                processor.Cosh(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> Sinh<T>(this GaScalar<T> scalar)
        {
            var processor = scalar.Processor;

            return new GaScalar<T>(
                processor, 
                processor.Sinh(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> Tanh<T>(this GaScalar<T> scalar)
        {
            var processor = scalar.Processor;

            return new GaScalar<T>(
                processor, 
                processor.Tanh(scalar.Scalar)
            );
        }
    }
}