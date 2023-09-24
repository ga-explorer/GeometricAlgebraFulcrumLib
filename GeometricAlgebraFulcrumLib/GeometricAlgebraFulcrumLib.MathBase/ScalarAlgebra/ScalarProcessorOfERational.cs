using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra
{
    /// <summary>
    /// A scalar processor for PeterO.Numbers.ERational numbers
    /// https://github.com/peteroupc/Numbers
    /// </summary>
    public sealed class ScalarProcessorOfERational
        : INumericScalarProcessor<ERational>
    {
        public static ScalarProcessorOfERational DefaultProcessor { get; }
            = new ScalarProcessorOfERational();


        public EContext NumericalContext { get; set; }
            = EContext.Binary128;

        public bool IsNumeric
            => true;

        public bool IsSymbolic
            => false;

        public ERational ScalarZero
            => ERational.Zero;

        public ERational ScalarOne
            => ERational.One;

        public ERational ScalarMinusOne
            => -ERational.One;

        public ERational ScalarTwo
            => 2;

        public ERational ScalarMinusTwo
            => -2;

        public ERational ScalarTen
            => 10;

        public ERational ScalarMinusTen
            => -10;

        public ERational ScalarPi { get; }

        public ERational ScalarTwoPi { get; }

        public ERational ScalarPiOver2 { get; }

        public ERational ScalarE { get; }

        public ERational ScalarDegreeToRadian { get; }

        public ERational ScalarRadianToDegree { get; }


        private ScalarProcessorOfERational()
        {
            ScalarPi = EFloat.PI(NumericalContext);
            ScalarTwoPi = EFloat.PI(NumericalContext) * 2;
            ScalarPiOver2 = EFloat.PI(NumericalContext) / 2;
            ScalarE = EFloat.One.Exp(NumericalContext);
            ScalarDegreeToRadian = EFloat.PI(NumericalContext) / 180;
            ScalarRadianToDegree = 180 / EFloat.PI(NumericalContext);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational Add(ERational scalar1, ERational scalar2)
        {
            return scalar1 + scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational Subtract(ERational scalar1, ERational scalar2)
        {
            return scalar1 - scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational Times(ERational scalar1, ERational scalar2)
        {
            return scalar1 * scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational Times(IntegerSign sign, ERational scalar)
        {
            return sign.Value * scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational NegativeTimes(ERational scalar1, ERational scalar2)
        {
            return -(scalar1 * scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational Divide(ERational scalar1, ERational scalar2)
        {
            return scalar1 / scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational NegativeDivide(ERational scalar1, ERational scalar2)
        {
            return -(scalar1 / scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational Positive(ERational scalar)
        {
            return scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational Negative(ERational scalar)
        {
            return -scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational Inverse(ERational scalar)
        {
            return ERational.One / scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational Sign(ERational scalar)
        {
            return scalar.Sign;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational UnitStep(ERational scalar)
        {
            return scalar.IsNegative ? ERational.Zero : ERational.One;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational Abs(ERational scalar)
        {
            return scalar.Abs();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational Sqrt(ERational scalar)
        {
            return scalar
                .ToEFloatExactIfPossible(NumericalContext)
                .Sqrt(NumericalContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational SqrtOfAbs(ERational scalar)
        {
            return scalar
                .Abs()
                .ToEFloatExactIfPossible(NumericalContext)
                .Sqrt(NumericalContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational Exp(ERational scalar)
        {
            return scalar
                .ToEFloatExactIfPossible(NumericalContext)
                .Exp(NumericalContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational LogE(ERational scalar)
        {
            return scalar
                .ToEFloatExactIfPossible(NumericalContext)
                .Log(NumericalContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational Log2(ERational scalar)
        {
            return scalar
                .ToEFloatExactIfPossible(NumericalContext)
                .LogN(2, NumericalContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational Log10(ERational scalar)
        {
            return scalar
                .ToEFloatExactIfPossible(NumericalContext)
                .Log10(NumericalContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational Power(ERational baseScalar, ERational scalar)
        {
            return baseScalar
                .ToEFloatExactIfPossible(NumericalContext)
                .Pow(scalar.ToEFloatExactIfPossible(NumericalContext));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational Log(ERational baseScalar, ERational scalar)
        {
            return scalar
                .ToEFloatExactIfPossible(NumericalContext)
                .LogN(
                    baseScalar.ToEFloatExactIfPossible(NumericalContext),
                    NumericalContext
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational Cos(ERational scalar)
        {
            return scalar
                .ToDouble()
                .Cos();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational Sin(ERational scalar)
        {
            return scalar
                .ToDouble()
                .Sin();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational Tan(ERational scalar)
        {
            return scalar
                .ToDouble()
                .Tan();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational ArcCos(ERational scalar)
        {
            return scalar
                .ToDouble()
                .ArcCos()
                .Radians
                .Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational ArcSin(ERational scalar)
        {
            return scalar
                .ToDouble()
                .ArcSin()
                .Radians
                .Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational ArcTan(ERational scalar)
        {
            return scalar
                .ToDouble()
                .ArcTan()
                .Radians
                .Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational ArcTan2(ERational scalarX, ERational scalarY)
        {
            return Math.Atan2(scalarY.ToDouble(), scalarX.ToDouble());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational Cosh(ERational scalar)
        {
            var s = scalar.ToEFloatExactIfPossible(NumericalContext);

            return (s.Exp(NumericalContext) + (-s).Exp(NumericalContext)) / 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational Sinh(ERational scalar)
        {
            var s = scalar.ToEFloatExactIfPossible(NumericalContext);

            return (s.Exp(NumericalContext) - (-s).Exp(NumericalContext)) / 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational Tanh(ERational scalar)
        {
            var s = scalar.ToEFloatExactIfPossible(NumericalContext);
            var sp = s.Exp(NumericalContext);
            var sn = (-s).Exp(NumericalContext);

            return (sp - sn) / (sp + sn);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational Sinc(ERational scalar)
        {
            return IsZero(scalar)
                ? ScalarOne
                : Sin(scalar) / scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid(ERational scalar)
        {
            return !scalar.IsNaN();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsFiniteNumber(ERational scalar)
        {
            return scalar.IsFinite;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(ERational scalar)
        {
            return scalar.IsZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(ERational scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? IsNearZero(scalar)
                : scalar.IsZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(ERational scalar)
        {
            //TODO: Correctly handle this case
            return scalar.IsZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(ERational scalar)
        {
            return !scalar.IsZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(ERational scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? !IsNearZero(scalar)
                : !scalar.IsZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearZero(ERational scalar)
        {
            return !IsNearZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational GetScalarFromText(string text)
        {
            return ERational.FromString(text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational GetScalarFromNumber(int value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational GetScalarFromNumber(uint value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational GetScalarFromNumber(long value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational GetScalarFromNumber(ulong value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational GetScalarFromNumber(float value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational GetScalarFromNumber(double value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational GetScalarFromRational(long numerator, long denominator)
        {
            return numerator / (ERational)denominator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ERational GetScalarFromRandom(System.Random randomGenerator, double minValue, double maxValue)
        {
            return minValue + (maxValue - minValue) * randomGenerator.NextDouble();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToText(ERational scalar)
        {
            return scalar.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPositive(ERational scalar)
        {
            return scalar.CompareToValue(0) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegative(ERational scalar)
        {
            return scalar.CompareToValue(0) < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotPositive(ERational scalar)
        {
            return scalar.CompareToValue(0) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNegative(ERational scalar)
        {
            return scalar.CompareToValue(0) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearPositive(ERational scalar)
        {
            return scalar.CompareToValue(0) < 0 && IsNotNearZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearNegative(ERational scalar)
        {
            return scalar.CompareToValue(0) > 0 && IsNotNearZero(scalar);
        }
    }
}