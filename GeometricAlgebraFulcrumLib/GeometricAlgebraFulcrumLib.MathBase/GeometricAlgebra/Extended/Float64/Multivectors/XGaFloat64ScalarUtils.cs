using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors
{
    public static class XGaFloat64ScalarUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo(this XGaFloat64Scalar scalar1, int scalar2)
        {
            return (scalar1.ScalarValue - scalar2).IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo(this int scalar1, XGaFloat64Scalar scalar2)
        {
            return (scalar1 - scalar2.ScalarValue).IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo(this XGaFloat64Scalar scalar1, uint scalar2)
        {
            return (scalar1.ScalarValue - scalar2).IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo(this uint scalar1, XGaFloat64Scalar scalar2)
        {
            return (scalar1 - scalar2.ScalarValue).IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo(this XGaFloat64Scalar scalar1, long scalar2)
        {
            return (scalar1.ScalarValue - scalar2).IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo(this long scalar1, XGaFloat64Scalar scalar2)
        {
            return (scalar1 - scalar2.ScalarValue).IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo(this XGaFloat64Scalar scalar1, ulong scalar2)
        {
            return (scalar1.ScalarValue - scalar2).IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo(this ulong scalar1, XGaFloat64Scalar scalar2)
        {
            return (scalar1 - scalar2.ScalarValue).IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo(this XGaFloat64Scalar scalar1, float scalar2)
        {
            return (scalar1.ScalarValue - scalar2).IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo(this float scalar1, XGaFloat64Scalar scalar2)
        {
            return (scalar1 - scalar2.ScalarValue).IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo(this XGaFloat64Scalar scalar1, double scalar2)
        {
            return (scalar1.ScalarValue - scalar2).IsZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo(this double scalar1, XGaFloat64Scalar scalar2)
        {
            return (scalar1 - scalar2.ScalarValue).IsZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo(this XGaFloat64Scalar scalar1, XGaFloat64Scalar scalar2)
        {
            return (scalar1 - scalar2).IsZero;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar Inverse(this XGaFloat64Scalar scalar)
        {
            var metric = scalar.Processor;

            return metric.CreateScalar(1 / scalar.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar Abs(this XGaFloat64Scalar scalar)
        {
            var metric = scalar.Processor;
            
            return metric.CreateScalar(
                scalar.ScalarValue.Abs()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar Square(this XGaFloat64Scalar scalar)
        {
            var metric = scalar.Processor;
            
            return metric.CreateScalar(
                scalar.ScalarValue.Square()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar Cube(this XGaFloat64Scalar scalar)
        {
            var metric = scalar.Processor;
            
            return metric.CreateScalar(
                scalar.ScalarValue.Cube()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar Sign(this XGaFloat64Scalar scalar)
        {
            var metric = scalar.Processor;

            return metric.CreateScalar(
                scalar.ScalarValue.Sign().ToFloat64()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar Sqrt(this XGaFloat64Scalar scalar)
        {
            var metric = scalar.Processor;
            
            return metric.CreateScalar(
                scalar.ScalarValue.Sqrt()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar SqrtOfAbs(this XGaFloat64Scalar scalar)
        {
            var metric = scalar.Processor;
            
            return metric.CreateScalar(
                scalar.ScalarValue.SqrtOfAbs()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar Power(this XGaFloat64Scalar scalar, int exponentScalar)
        {
            var metric = scalar.Processor;
            
            return metric.CreateScalar(
                scalar.ScalarValue.Power(exponentScalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar Power(this XGaFloat64Scalar scalar, double exponentScalar)
        {
            var metric = scalar.Processor;
            
            return metric.CreateScalar(
                scalar.ScalarValue.Power(exponentScalar)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar Exp(this XGaFloat64Scalar scalar)
        {
            var metric = scalar.Processor;
            
            return metric.CreateScalar(
                scalar.ScalarValue.Exp()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar Log(this XGaFloat64Scalar scalar)
        {
            var metric = scalar.Processor;
            
            return metric.CreateScalar(
                scalar.ScalarValue.LogE()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar Log2(this XGaFloat64Scalar scalar)
        {
            var metric = scalar.Processor;
            
            return metric.CreateScalar(
                scalar.ScalarValue.Log2()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar Log10(this XGaFloat64Scalar scalar)
        {
            var metric = scalar.Processor;
            
            return metric.CreateScalar(
                scalar.ScalarValue.Log10()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar Log(this XGaFloat64Scalar scalar, double baseScalar)
        {
            var metric = scalar.Processor;
            
            return metric.CreateScalar(
                baseScalar.Log(scalar.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar Log(this XGaFloat64Scalar scalar, XGaFloat64Scalar baseScalar)
        {
            var metric = scalar.Processor;
            
            return metric.CreateScalar(
                baseScalar.ScalarValue.Log(scalar.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar Cos(this XGaFloat64Scalar scalar)
        {
            var metric = scalar.Processor;
            
            return metric.CreateScalar(
                scalar.ScalarValue.Cos()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar Sin(this XGaFloat64Scalar scalar)
        {
            var metric = scalar.Processor;
            
            return metric.CreateScalar(
                scalar.ScalarValue.Sin()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar Tan(this XGaFloat64Scalar scalar)
        {
            var metric = scalar.Processor;
            
            return metric.CreateScalar(
                scalar.ScalarValue.Tan()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar ArcCos(this XGaFloat64Scalar scalar)
        {
            var metric = scalar.Processor;
            
            return metric.CreateScalar(
                scalar.ScalarValue.ArcCos()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar ArcSin(this XGaFloat64Scalar scalar)
        {
            var metric = scalar.Processor;
            
            return metric.CreateScalar(
                scalar.ScalarValue.ArcSin()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar ArcTan(this XGaFloat64Scalar scalar)
        {
            var metric = scalar.Processor;
            
            return metric.CreateScalar(
                scalar.ScalarValue.ArcTan()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar ArcTan2(this XGaFloat64Scalar scalarX, double scalarY)
        {
            var metric = scalarX.Processor;

            return metric.CreateScalar(
                Math.Atan2(scalarY, scalarX.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar ArcTan2(this XGaFloat64Scalar scalarX, XGaFloat64Scalar scalarY)
        {
            var metric = scalarX.Processor;

            return metric.CreateScalar(
                Math.Atan2(scalarY.ScalarValue, scalarX.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar Cosh(this XGaFloat64Scalar scalar)
        {
            var metric = scalar.Processor;
            
            return metric.CreateScalar(
                scalar.ScalarValue.Cosh()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar Sinh(this XGaFloat64Scalar scalar)
        {
            var metric = scalar.Processor;
            
            return metric.CreateScalar(
                scalar.ScalarValue.Sinh()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar Tanh(this XGaFloat64Scalar scalar)
        {
            var metric = scalar.Processor;
            
            return metric.CreateScalar(
                scalar.ScalarValue.Tanh()
            );
        }
    }
}