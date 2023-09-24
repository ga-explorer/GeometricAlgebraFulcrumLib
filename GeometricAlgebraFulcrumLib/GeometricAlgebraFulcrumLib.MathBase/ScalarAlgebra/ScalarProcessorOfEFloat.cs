using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra
{
    /// <summary>
    /// A scalar processor for PeterO.Numbers.EFloat numbers
    /// https://github.com/peteroupc/Numbers
    /// </summary>
    public sealed class ScalarProcessorOfEFloat
        : INumericScalarProcessor<EFloat>
    {
        public static ScalarProcessorOfEFloat DefaultProcessor { get; }
            = new ScalarProcessorOfEFloat();


        public EContext NumericalContext { get; set; }
            = EContext.Binary128;

        public bool IsNumeric
            => true;

        public bool IsSymbolic
            => false;

        public EFloat ScalarZero
            => EFloat.Zero;

        public EFloat ScalarOne
            => EFloat.One;

        public EFloat ScalarMinusOne
            => -EFloat.One;

        public EFloat ScalarTwo
            => 2;

        public EFloat ScalarMinusTwo
            => -2;

        public EFloat ScalarTen
            => 10;

        public EFloat ScalarMinusTen
            => -10;

        public EFloat ScalarPi { get; }

        public EFloat ScalarTwoPi { get; }

        public EFloat ScalarPiOver2 { get; }

        public EFloat ScalarE { get; }

        public EFloat ScalarDegreeToRadian { get; }

        public EFloat ScalarRadianToDegree { get; }


        private ScalarProcessorOfEFloat()
        {
            ScalarPi = EFloat.PI(NumericalContext);
            ScalarTwoPi = EFloat.PI(NumericalContext) * 2;
            ScalarPiOver2 = EFloat.PI(NumericalContext) / 2;
            ScalarE = EFloat.One.Exp(NumericalContext);
            ScalarDegreeToRadian = EFloat.PI(NumericalContext) / 180;
            ScalarRadianToDegree = 180 / EFloat.PI(NumericalContext);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat Add(EFloat scalar1, EFloat scalar2)
        {
            return scalar1 + scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat Subtract(EFloat scalar1, EFloat scalar2)
        {
            return scalar1 - scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat Times(EFloat scalar1, EFloat scalar2)
        {
            return scalar1 * scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat Times(IntegerSign sign, EFloat scalar)
        {
            if (sign.IsZero) return ScalarZero;

            return sign.IsPositive
                ? scalar
                : -scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat NegativeTimes(EFloat scalar1, EFloat scalar2)
        {
            return -(scalar1 * scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat Divide(EFloat scalar1, EFloat scalar2)
        {
            return scalar1 / scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat NegativeDivide(EFloat scalar1, EFloat scalar2)
        {
            return -(scalar1 / scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat Positive(EFloat scalar)
        {
            return scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat Negative(EFloat scalar)
        {
            return -scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat Inverse(EFloat scalar)
        {
            return EFloat.One / scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat Sign(EFloat scalar)
        {
            return scalar.Sign;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat UnitStep(EFloat scalar)
        {
            return scalar.IsNegative ? EFloat.Zero : EFloat.One;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat Abs(EFloat scalar)
        {
            return scalar.Abs();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat Sqrt(EFloat scalar)
        {
            return scalar
                .Sqrt(NumericalContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat SqrtOfAbs(EFloat scalar)
        {
            return scalar
                .Abs()
                .Sqrt(NumericalContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat Exp(EFloat scalar)
        {
            return scalar
                .Exp(NumericalContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat LogE(EFloat scalar)
        {
            return scalar
                .Log(NumericalContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat Log2(EFloat scalar)
        {
            return scalar
                .LogN(2, NumericalContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat Log10(EFloat scalar)
        {
            return scalar
                .Log10(NumericalContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat Power(EFloat baseScalar, EFloat scalar)
        {
            return baseScalar
                .Pow(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat Log(EFloat baseScalar, EFloat scalar)
        {
            return scalar
                .LogN(baseScalar, NumericalContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat Cos(EFloat scalar)
        {
            return scalar
                .ToDouble()
                .Cos();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat Sin(EFloat scalar)
        {
            return scalar
                .ToDouble()
                .Sin();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat Tan(EFloat scalar)
        {
            return scalar
                .ToDouble()
                .Tan();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat ArcCos(EFloat scalar)
        {
            return scalar
                .ToDouble()
                .ArcCos()
                .Radians
                .Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat ArcSin(EFloat scalar)
        {
            return scalar
                .ToDouble()
                .ArcSin()
                .Radians
                .Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat ArcTan(EFloat scalar)
        {
            return scalar
                .ToDouble()
                .ArcTan()
                .Radians
                .Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat ArcTan2(EFloat scalarX, EFloat scalarY)
        {
            return Math.Atan2(scalarY.ToDouble(), scalarX.ToDouble());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat Cosh(EFloat scalar)
        {
            return (scalar.Exp(NumericalContext) + (-scalar).Exp(NumericalContext)) / 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat Sinh(EFloat scalar)
        {
            return (scalar.Exp(NumericalContext) - (-scalar).Exp(NumericalContext)) / 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat Tanh(EFloat scalar)
        {
            var sp = scalar.Exp(NumericalContext);
            var sn = (-scalar).Exp(NumericalContext);

            return (sp - sn) / (sp + sn);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat Sinc(EFloat scalar)
        {
            return IsZero(scalar)
                ? ScalarOne
                : Sin(scalar) / scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid(EFloat scalar)
        {
            return !scalar.IsNaN();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsFiniteNumber(EFloat scalar)
        {
            return scalar.IsFinite;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(EFloat scalar)
        {
            return scalar.IsZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(EFloat scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? IsNearZero(scalar)
                : scalar.IsZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(EFloat scalar)
        {
            //TODO: Correctly handle this case
            return scalar.IsZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(EFloat scalar)
        {
            return !scalar.IsZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(EFloat scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? !IsNearZero(scalar)
                : !scalar.IsZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearZero(EFloat scalar)
        {
            return !IsNearZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat GetScalarFromText(string text)
        {
            return EFloat.FromString(text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat GetScalarFromNumber(int value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat GetScalarFromNumber(uint value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat GetScalarFromNumber(long value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat GetScalarFromNumber(ulong value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat GetScalarFromNumber(float value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat GetScalarFromNumber(double value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat GetScalarFromRational(long numerator, long denominator)
        {
            return (
                ERational.FromInt64(numerator) /
                ERational.FromInt64(denominator)
            ).ToEFloatExactIfPossible(NumericalContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EFloat GetScalarFromRandom(System.Random randomGenerator, double minValue, double maxValue)
        {
            return minValue + (maxValue - minValue) * randomGenerator.NextDouble();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToText(EFloat scalar)
        {
            return scalar.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPositive(EFloat scalar)
        {
            return scalar.CompareToValue(0) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegative(EFloat scalar)
        {
            return scalar.CompareToValue(0) < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotPositive(EFloat scalar)
        {
            return scalar.CompareToValue(0) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNegative(EFloat scalar)
        {
            return scalar.CompareToValue(0) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearPositive(EFloat scalar)
        {
            return scalar.CompareToValue(0) < 0 && IsNotNearZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearNegative(EFloat scalar)
        {
            return scalar.CompareToValue(0) > 0 && IsNotNearZero(scalar);
        }
    }
}