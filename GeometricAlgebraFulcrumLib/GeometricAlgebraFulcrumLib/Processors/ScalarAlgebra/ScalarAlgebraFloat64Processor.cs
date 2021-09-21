using System;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra
{
    public sealed class ScalarAlgebraFloat64Processor 
        : IScalarAlgebraProcessor<double>
    {
        public static ScalarAlgebraFloat64Processor DefaultProcessor { get; }
            = new ScalarAlgebraFloat64Processor();


        public double ZeroEpsilon { get; set; }
            = 1e-13d;

        public bool IsNumeric 
            => true;

        public bool IsSymbolic 
            => false;

        public double ScalarZero 
            => 0d;

        public double ScalarOne 
            => 1d;

        public double ScalarMinusOne 
            => -1d;

        public double ScalarTwo 
            => 2d;

        public double ScalarMinusTwo 
            => -2d;

        public double ScalarTen 
            => 10d;

        public double ScalarMinusTen 
            => -10d;

        public double ScalarPi 
            => Math.PI;

        public double ScalarE 
            => Math.E;


        private ScalarAlgebraFloat64Processor()
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Add(double scalar1, double scalar2)
        {
            return scalar1 + scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Subtract(double scalar1, double scalar2)
        {
            return scalar1 - scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Times(double scalar1, double scalar2)
        {
            return scalar1 * scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double NegativeTimes(double scalar1, double scalar2)
        {
            return -(scalar1 * scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Divide(double scalar1, double scalar2)
        {
            return scalar1 / scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double NegativeDivide(double scalar1, double scalar2)
        {
            return -(scalar1 / scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Positive(double scalar)
        {
            return scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Negative(double scalar)
        {
            return -scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Inverse(double scalar)
        {
            return 1d / scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Abs(double scalar)
        {
            return Math.Abs(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Sqrt(double scalar)
        {
            return Math.Sqrt(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double SqrtOfAbs(double scalar)
        {
            return Math.Sqrt(Math.Abs(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Exp(double scalar)
        {
            return Math.Exp(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double LogE(double scalar)
        {
            return Math.Log(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Log2(double scalar)
        {
            return Math.Log2(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Log10(double scalar)
        {
            return Math.Log10(scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Power(double baseScalar, double scalar)
        {
            return Math.Pow(baseScalar, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Log(double baseScalar, double scalar)
        {
            return Math.Log(scalar, baseScalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Cos(double scalar)
        {
            return Math.Cos(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Sin(double scalar)
        {
            return Math.Sin(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Tan(double scalar)
        {
            return Math.Tan(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ArcCos(double scalar)
        {
            return Math.Acos(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ArcSin(double scalar)
        {
            return Math.Asin(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ArcTan(double scalar)
        {
            return Math.Atan(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ArcTan2(double scalarX, double scalarY)
        {
            return Math.Atan2(scalarY, scalarX);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Cosh(double scalar)
        {
            return Math.Cosh(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Sinh(double scalar)
        {
            return Math.Sinh(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Tanh(double scalar)
        {
            return Math.Tanh(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid(double scalar)
        {
            return !double.IsNaN(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(double scalar)
        {
            return scalar == 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(double scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar > -ZeroEpsilon && scalar < ZeroEpsilon
                : scalar == 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(double scalar)
        {
            return scalar > -ZeroEpsilon && scalar < ZeroEpsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(double scalar)
        {
            return scalar != 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(double scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar <= -ZeroEpsilon || scalar >= ZeroEpsilon
                : scalar != 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearZero(double scalar)
        {
            return scalar <= -ZeroEpsilon || scalar >= ZeroEpsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarFromText(string text)
        {
            return double.Parse(text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarFromNumber(int value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarFromNumber(uint value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarFromNumber(long value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarFromNumber(ulong value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarFromNumber(float value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarFromNumber(double value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarFromRational(long numerator, long denominator)
        {
            return numerator / (double) denominator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
        {
            return minValue + (maxValue - minValue) * randomGenerator.NextDouble();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToText(double scalar)
        {
            return scalar.ToString("G");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPositive(double scalar)
        {
            return scalar > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegative(double scalar)
        {
            return scalar < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotPositive(double scalar)
        {
            return scalar <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNegative(double scalar)
        {
            return scalar >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearPositive(double scalar)
        {
            return scalar < -ZeroEpsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearNegative(double scalar)
        {
            return scalar > ZeroEpsilon;
        }
    }
}