using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra
{
    public sealed class ScalarAlgebraComplexProcessor
        : IScalarAlgebraProcessor<Complex>
    {
        public static ScalarAlgebraComplexProcessor DefaultProcessor { get; }
            = new ScalarAlgebraComplexProcessor();


        public Complex ScalarZero { get; } 
            = Complex.Zero;
        
        public Complex ScalarOne { get; } 
            = Complex.One;
        
        public Complex ScalarMinusOne { get; } 
            = -Complex.One;
        
        public Complex ScalarTwo { get; } 
            = 2d;
        
        public Complex ScalarMinusTwo { get; } 
            = -2d;
        
        public Complex ScalarTen { get; } 
            = 10d;
        
        public Complex ScalarMinusTen { get; } 
            = -10d;
        
        public Complex ScalarPi { get; } 
            = Math.PI;
        
        public Complex ScalarE { get; } 
            = Math.E;

        public bool IsNumeric 
            => true;

        public bool IsSymbolic 
            => false;

        public double ZeroEpsilon { get; set; }
            = 1e-13d;


        private ScalarAlgebraComplexProcessor()
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Add(Complex scalar1, Complex scalar2)
        {
            return scalar1 + scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Subtract(Complex scalar1, Complex scalar2)
        {
            return scalar1 - scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Times(Complex scalar1, Complex scalar2)
        {
            return scalar1 * scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex NegativeTimes(Complex scalar1, Complex scalar2)
        {
            return -scalar1 * scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Divide(Complex scalar1, Complex scalar2)
        {
            return scalar1 / scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex NegativeDivide(Complex scalar1, Complex scalar2)
        {
            return -scalar1 / scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Positive(Complex scalar)
        {
            return scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Negative(Complex scalar)
        {
            return -scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Inverse(Complex scalar)
        {
            return 1d / scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Abs(Complex scalar)
        {
            return Complex.Abs(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Power(Complex baseScalar, Complex scalar)
        {
            return Complex.Pow(baseScalar, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Sqrt(Complex scalar)
        {
            return Complex.Sqrt(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex SqrtOfAbs(Complex scalar)
        {
            return Math.Sqrt(Complex.Abs(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Exp(Complex scalar)
        {
            return Complex.Exp(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex LogE(Complex scalar)
        {
            return Complex.Log(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Log2(Complex scalar)
        {
            return Complex.Log(scalar, 2d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Log10(Complex scalar)
        {
            return Complex.Log10(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Log(Complex baseScalar, Complex scalar)
        {
            return Complex.Log(scalar, baseScalar.Real);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Cos(Complex scalar)
        {
            return Complex.Cos(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Sin(Complex scalar)
        {
            return Complex.Sin(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Tan(Complex scalar)
        {
            return Complex.Tan(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex ArcCos(Complex scalar)
        {
            return Complex.Acos(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex ArcSin(Complex scalar)
        {
            return Complex.Asin(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex ArcTan(Complex scalar)
        {
            return Complex.Atan(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex ArcTan2(Complex scalarX, Complex scalarY)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Cosh(Complex scalar)
        {
            return Complex.Cosh(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Sinh(Complex scalar)
        {
            return Complex.Sinh(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Tanh(Complex scalar)
        {
            return Complex.Tanh(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid(Complex scalar)
        {
            return !scalar.IsNaN();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(Complex scalar)
        {
            return scalar.Real == 0d && scalar.Imaginary == 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(Complex scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar.Magnitude < ZeroEpsilon
                : scalar.Real == 0d && scalar.Imaginary == 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(Complex scalar)
        {
            return scalar.Magnitude < ZeroEpsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(Complex scalar)
        {
            return scalar.Real != 0d || scalar.Imaginary != 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(Complex scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar.Magnitude >= ZeroEpsilon
                : scalar.Real != 0d || scalar.Imaginary != 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearZero(Complex scalar)
        {
            return scalar.Magnitude >= ZeroEpsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPositive(Complex scalar)
        {
            return scalar.Real > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegative(Complex scalar)
        {
            return scalar.Real < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotPositive(Complex scalar)
        {
            return scalar.Real <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNegative(Complex scalar)
        {
            return scalar.Real >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearPositive(Complex scalar)
        {
            return scalar.Real >= ZeroEpsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearNegative(Complex scalar)
        {
            return scalar.Real <= -ZeroEpsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex GetScalarFromText(string text)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex GetScalarFromNumber(int value)
        {
            return new Complex(value, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex GetScalarFromNumber(uint value)
        {
            return new Complex(value, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex GetScalarFromNumber(long value)
        {
            return new Complex(value, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex GetScalarFromNumber(ulong value)
        {
            return new Complex(value, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex GetScalarFromNumber(float value)
        {
            return new Complex(value, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex GetScalarFromNumber(double value)
        {
            return new Complex(value, 0d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex GetScalarFromRational(long numerator, long denominator)
        {
            return new Complex(numerator / (double) denominator, 0d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex GetScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
        {
            var magnitude = 
                minValue + (maxValue - minValue) * randomGenerator.NextDouble();

            var angle = 
                2d * Math.PI * randomGenerator.NextDouble();

            return Complex.FromPolarCoordinates(magnitude, angle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToText(Complex scalar)
        {
            return scalar.ToString("G");
        }
    }
}