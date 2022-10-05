using System;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra
{
    /// <summary>
    /// A scalar processor for PeterO.Numbers.EDecimal numbers
    /// https://github.com/peteroupc/Numbers
    /// </summary>
    public sealed class ScalarAlgebraEDecimalProcessor
        : IScalarAlgebraNumericProcessor<EDecimal>
    {
        public static ScalarAlgebraEDecimalProcessor DefaultProcessor { get; }
            = new ScalarAlgebraEDecimalProcessor();
        

        public EContext NumericalContext { get; set; }
            = EContext.Binary128;

        public bool IsNumeric 
            => true;

        public bool IsSymbolic 
            => false;

        public EDecimal ScalarZero 
            => EDecimal.Zero;

        public EDecimal ScalarOne 
            => EDecimal.One;

        public EDecimal ScalarMinusOne 
            => -EDecimal.One;

        public EDecimal ScalarTwo 
            => 2;

        public EDecimal ScalarMinusTwo 
            => -2;

        public EDecimal ScalarTen 
            => 10;

        public EDecimal ScalarMinusTen 
            => -10;

        public EDecimal ScalarPi { get; }

        public EDecimal ScalarTwoPi { get; }

        public EDecimal ScalarPiOver2 { get; }

        public EDecimal ScalarE { get; }

        public EDecimal ScalarDegreeToRadian { get; }

        public EDecimal ScalarRadianToDegree { get; }


        private ScalarAlgebraEDecimalProcessor()
        {
            ScalarPi = EDecimal.PI(NumericalContext);
            ScalarTwoPi = EDecimal.PI(NumericalContext) * 2;
            ScalarPiOver2 = EDecimal.PI(NumericalContext) / 2;
            ScalarE = EDecimal.One.Exp(NumericalContext);
            ScalarDegreeToRadian = EDecimal.PI(NumericalContext) / 180;
            ScalarRadianToDegree = 180 / EDecimal.PI(NumericalContext);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal Add(EDecimal scalar1, EDecimal scalar2)
        {
            return scalar1 + scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal Subtract(EDecimal scalar1, EDecimal scalar2)
        {
            return scalar1 - scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal Times(EDecimal scalar1, EDecimal scalar2)
        {
            return scalar1 * scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal NegativeTimes(EDecimal scalar1, EDecimal scalar2)
        {
            return -(scalar1 * scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal Divide(EDecimal scalar1, EDecimal scalar2)
        {
            return scalar1 / scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal NegativeDivide(EDecimal scalar1, EDecimal scalar2)
        {
            return -(scalar1 / scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal Positive(EDecimal scalar)
        {
            return scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal Negative(EDecimal scalar)
        {
            return -scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal Inverse(EDecimal scalar)
        {
            return EDecimal.One / scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal Sign(EDecimal scalar)
        {
            return scalar.Sign;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal UnitStep(EDecimal scalar)
        {
            return scalar.IsNegative ? EDecimal.Zero : EDecimal.One;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal Abs(EDecimal scalar)
        {
            return scalar.Abs();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal Sqrt(EDecimal scalar)
        {
            return scalar
                .Sqrt(NumericalContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal SqrtOfAbs(EDecimal scalar)
        {
            return scalar
                .Abs()
                .Sqrt(NumericalContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal Exp(EDecimal scalar)
        {
            return scalar
                .Exp(NumericalContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal LogE(EDecimal scalar)
        {
            return scalar
                .Log(NumericalContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal Log2(EDecimal scalar)
        {
            return scalar
                .LogN(2, NumericalContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal Log10(EDecimal scalar)
        {
            return scalar
                .Log10(NumericalContext);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal Power(EDecimal baseScalar, EDecimal scalar)
        {
            return baseScalar
                .Pow(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal Log(EDecimal baseScalar, EDecimal scalar)
        {
            return scalar
                .LogN(baseScalar, NumericalContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal Cos(EDecimal scalar)
        {
            return EDecimal.FromDouble(
                scalar
                    .ToDouble()
                    .Cos()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal Sin(EDecimal scalar)
        {
            return EDecimal.FromDouble(
                scalar
                    .ToDouble()
                    .Sin()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal Tan(EDecimal scalar)
        {
            return EDecimal.FromDouble(
                scalar
                    .ToDouble()
                    .Tan()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal ArcCos(EDecimal scalar)
        {
            return EDecimal.FromDouble(
                scalar
                    .ToDouble()
                    .ArcCos()
                    .Radians
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal ArcSin(EDecimal scalar)
        {
            return EDecimal.FromDouble(
                scalar
                    .ToDouble()
                    .ArcSin()
                    .Radians
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal ArcTan(EDecimal scalar)
        {
            return EDecimal.FromDouble(
                scalar
                    .ToDouble()
                    .ArcTan()
                    .Radians
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal ArcTan2(EDecimal scalarX, EDecimal scalarY)
        {
            return EDecimal.FromDouble(
                Math.Atan2(scalarY.ToDouble(), scalarX.ToDouble())
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal Cosh(EDecimal scalar)
        {
            return (scalar.Exp(NumericalContext) + (-scalar).Exp(NumericalContext)) / 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal Sinh(EDecimal scalar)
        {
            return (scalar.Exp(NumericalContext) - (-scalar).Exp(NumericalContext)) / 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal Tanh(EDecimal scalar)
        {
            var sp = scalar.Exp(NumericalContext);
            var sn = (-scalar).Exp(NumericalContext);

            return (sp - sn) / (sp + sn);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal Sinc(EDecimal scalar)
        {
            return IsZero(scalar) 
                ? ScalarOne 
                : Sin(scalar) / scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid(EDecimal scalar)
        {
            return !scalar.IsNaN();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsFiniteNumber(EDecimal scalar)
        {
            return scalar.IsFinite;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(EDecimal scalar)
        {
            return scalar.IsZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(EDecimal scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? IsNearZero(scalar)
                : scalar.IsZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(EDecimal scalar)
        {
            //TODO: Correctly handle this case
            return scalar.IsZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(EDecimal scalar)
        {
            return !scalar.IsZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(EDecimal scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? !IsNearZero(scalar)
                : !scalar.IsZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearZero(EDecimal scalar)
        {
            return !IsNearZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal GetScalarFromText(string text)
        {
            return EDecimal.FromString(text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal GetScalarFromNumber(int value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal GetScalarFromNumber(uint value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal GetScalarFromNumber(long value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal GetScalarFromNumber(ulong value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal GetScalarFromNumber(float value)
        {
            return EDecimal.FromSingle(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal GetScalarFromNumber(double value)
        {
            return EDecimal.FromDouble(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal GetScalarFromRational(long numerator, long denominator)
        {
            return (
                ERational.FromInt64(numerator) / 
                ERational.FromInt64(denominator)
            ).ToEDecimalExactIfPossible(NumericalContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EDecimal GetScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
        {
            return EDecimal.FromDouble(minValue + (maxValue - minValue) * randomGenerator.NextDouble());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToText(EDecimal scalar)
        {
            return scalar.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPositive(EDecimal scalar)
        {
            return scalar.CompareToValue(0) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegative(EDecimal scalar)
        {
            return scalar.CompareToValue(0) < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotPositive(EDecimal scalar)
        {
            return scalar.CompareToValue(0) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNegative(EDecimal scalar)
        {
            return scalar.CompareToValue(0) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearPositive(EDecimal scalar)
        {
            return scalar.CompareToValue(0) < 0 && IsNotNearZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearNegative(EDecimal scalar)
        {
            return scalar.CompareToValue(0) > 0 && IsNotNearZero(scalar);
        }
    }
}