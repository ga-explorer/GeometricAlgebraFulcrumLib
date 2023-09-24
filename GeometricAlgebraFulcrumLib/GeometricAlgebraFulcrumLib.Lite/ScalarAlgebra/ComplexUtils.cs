using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra
{
    public static class ComplexUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearReal(this Complex c, double epsilon = 1e-12d)
        {
            return c.Imaginary.IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearImaginary(this Complex c, double epsilon = 1e-12d)
        {
            return c.Real.IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double RotateToReal(this Complex c)
        {
            return c.Real.Sign() * c.Magnitude;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearZero(this Complex c, double epsilon = 1e-12d)
        {
            return (c.Real.Square() + c.Imaginary.Square()).IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearOne(this Complex c, double epsilon = 1e-12d)
        {
            return ((c.Real - 1d).Square() + c.Imaginary.Square()).IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearMinusOne(this Complex c, double epsilon = 1e-12d)
        {
            return ((c.Real + 1d).Square() + c.Imaginary.Square()).IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearConjugateTo(this Complex c1, Complex c2, double epsilon = 1e-12)
        {
            return (c1.Real - c2.Real).IsNearZero(epsilon) &&
                   (c1.Imaginary + c2.Imaginary).IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearConjugateTo(this Complex c1, double c2Real, double c2Imaginary, double epsilon = 1e-12)
        {
            return (c1.Real - c2Real).IsNearZero(epsilon) &&
                   (c1.Imaginary + c2Imaginary).IsNearZero(epsilon);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NthRootOfOne(this int n, int k)
        {
            var angle = 2d * Math.PI * k.Mod(n) / n;

            return new Complex(
                Math.Cos(angle),
                Math.Sin(angle)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NthRootOfOne(this int n)
        {
            var angle = 2d * Math.PI / n;

            return new Complex(
                Math.Cos(angle),
                Math.Sin(angle)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sum(this IEnumerable<Complex> numbers)
        {
            return numbers.Aggregate(Complex.Zero, (a, b) => a + b);
        }

        
        public static StringBuilder Append(this StringBuilder s, Complex number)
        {
            if (number.Real != 0)
                s.Append(number.Real.ToString("G"));

            if (number.Imaginary != 0)
                s.Append(number.Imaginary.ToString("G")).Append(" i");

            return s;
        }

        public static StringBuilder Append(this StringBuilder s, Complex number, string doubleFormatString)
        {
            if (number.Real != 0)
                s.Append(number.Real.ToString(doubleFormatString));

            if (number.Imaginary != 0)
                s.Append(number.Imaginary.ToString(doubleFormatString)).Append(" i");

            return s;
        }

        public static StringBuilder AppendLine(this StringBuilder s, Complex number, string doubleFormatString)
        {
            if (number.Real != 0)
                s.Append(number.Real.ToString(doubleFormatString));

            if (number.Imaginary != 0)
                s.Append(number.Imaginary.ToString(doubleFormatString)).Append(" i");

            s.AppendLine();

            return s;
        }

        public static StringBuilder AppendLine(this StringBuilder s, Complex number)
        {
            if (number.Real != 0)
                s.Append(number.Real.ToString("G"));

            if (number.Imaginary != 0)
                s.Append(number.Imaginary.ToString("G")).Append(" i");

            s.AppendLine();

            return s;
        }

        public static StringBuilder AppendComplexNumber(this StringBuilder s, double numberReal, double numberImaginary, string doubleFormatString)
        {
            if (numberReal != 0)
                s.Append(numberReal.ToString(doubleFormatString));

            if (numberImaginary != 0)
                s.Append(numberImaginary.ToString(doubleFormatString)).Append(" i");

            return s;
        }

        public static StringBuilder AppendComplexNumber(this StringBuilder s, double numberReal, double numberImaginary)
        {
            if (numberReal != 0)
                s.Append(numberReal.ToString("G"));

            if (numberImaginary != 0)
                s.Append(numberImaginary.ToString("G")).Append(" i");

            return s;
        }
    }
}
