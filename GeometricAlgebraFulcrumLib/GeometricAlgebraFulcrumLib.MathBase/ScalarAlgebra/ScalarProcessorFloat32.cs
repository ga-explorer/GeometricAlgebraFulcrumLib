using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra
{
    public sealed class ScalarProcessorFloat32
        : INumericScalarProcessor<float>
    {
        public static ScalarProcessorFloat32 DefaultProcessor { get; }
            = new ScalarProcessorFloat32();


        public float ZeroEpsilon { get; set; }
            = 1e-7f;


        public bool IsNumeric
            => true;

        public bool IsSymbolic
            => false;

        public float ScalarZero
            => 0f;

        public float ScalarOne
            => 1f;

        public float ScalarMinusOne
            => -1f;

        public float ScalarTwo
            => 2f;

        public float ScalarMinusTwo
            => -2f;

        public float ScalarTen
            => 10f;

        public float ScalarMinusTen
            => -10f;

        public float ScalarPi
            => MathF.PI;

        public float ScalarTwoPi
            => 2f * MathF.PI;

        public float ScalarPiOver2
            => 0.5f * MathF.PI;

        public float ScalarE
            => MathF.E;

        public float ScalarDegreeToRadian
            => MathF.PI / 180f;

        public float ScalarRadianToDegree
            => 180f / MathF.PI;


        private ScalarProcessorFloat32()
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Add(float scalar1, float scalar2)
        {
            return scalar1 + scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Subtract(float scalar1, float scalar2)
        {
            return scalar1 - scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Times(float scalar1, float scalar2)
        {
            return scalar1 * scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Times(IntegerSign sign, float scalar)
        {
            if (sign.IsZero) return 0f;

            return sign.IsPositive
                ? scalar
                : -scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float NegativeTimes(float scalar1, float scalar2)
        {
            return -(scalar1 * scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Divide(float scalar1, float scalar2)
        {
            return scalar1 / scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float NegativeDivide(float scalar1, float scalar2)
        {
            return -(scalar1 / scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Positive(float scalar)
        {
            return scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Negative(float scalar)
        {
            return -scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Inverse(float scalar)
        {
            return 1f / scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Sign(float scalar)
        {
            return MathF.Sign(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float UnitStep(float scalar)
        {
            return scalar < 0f ? 0f : 1f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Abs(float scalar)
        {
            return MathF.Abs(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Power(float baseScalar, float scalar)
        {
            return MathF.Pow(baseScalar, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Sqrt(float scalar)
        {
            return MathF.Sqrt(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float SqrtOfAbs(float scalar)
        {
            return MathF.Sqrt(MathF.Abs(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Exp(float scalar)
        {
            return MathF.Exp(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float LogE(float scalar)
        {
            return MathF.Log(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Log2(float scalar)
        {
            return MathF.Log2(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Log10(float scalar)
        {
            return MathF.Log10(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Log(float baseScalar, float scalar)
        {
            return MathF.Log(scalar, baseScalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Cos(float scalar)
        {
            return MathF.Cos(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Sin(float scalar)
        {
            return MathF.Sin(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Tan(float scalar)
        {
            return MathF.Tan(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float ArcCos(float scalar)
        {
            return MathF.Acos(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float ArcSin(float scalar)
        {
            return MathF.Asin(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float ArcTan(float scalar)
        {
            return MathF.Atan(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float ArcTan2(float scalarX, float scalarY)
        {
            return MathF.Atan2(scalarY, scalarX);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Cosh(float scalar)
        {
            return MathF.Cosh(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Sinh(float scalar)
        {
            return MathF.Sinh(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Tanh(float scalar)
        {
            return MathF.Tanh(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Sinc(float scalar)
        {
            if (float.IsInfinity(scalar))
                return 0f;

            return IsZero(scalar)
                ? 1f
                : MathF.Sin(scalar) / scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid(float scalar)
        {
            return !float.IsNaN(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsFiniteNumber(float scalar)
        {
            return float.IsFinite(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(float scalar)
        {
            return scalar == 0f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(float scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar > -ZeroEpsilon && scalar < ZeroEpsilon
                : scalar == 0f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(float scalar)
        {
            return scalar > -ZeroEpsilon && scalar < ZeroEpsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(float scalar)
        {
            return scalar != 0f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(float scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar <= -ZeroEpsilon || scalar >= ZeroEpsilon
                : scalar != 0f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearZero(float scalar)
        {
            return scalar <= -ZeroEpsilon || scalar >= ZeroEpsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPositive(float scalar)
        {
            return scalar > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegative(float scalar)
        {
            return scalar < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotPositive(float scalar)
        {
            return scalar <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNegative(float scalar)
        {
            return scalar >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearPositive(float scalar)
        {
            return scalar < -ZeroEpsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearNegative(float scalar)
        {
            return scalar > ZeroEpsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetScalarFromText(string text)
        {
            return float.Parse(text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetScalarFromNumber(int value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetScalarFromNumber(uint value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetScalarFromNumber(long value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetScalarFromNumber(ulong value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetScalarFromNumber(float value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetScalarFromNumber(double value)
        {
            return (float)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetScalarFromRational(long numerator, long denominator)
        {
            return (float)(numerator / (double)denominator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetScalarFromRandom(System.Random randomGenerator, double minValue, double maxValue)
        {
            return (float)(minValue + (maxValue - minValue) * randomGenerator.NextDouble());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToText(float scalar)
        {
            return scalar.ToString("G");
        }
    }
}