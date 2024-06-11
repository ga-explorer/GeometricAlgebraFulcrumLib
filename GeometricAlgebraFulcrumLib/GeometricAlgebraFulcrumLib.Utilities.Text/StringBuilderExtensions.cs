using System.Numerics;
using System.Text;

namespace GeometricAlgebraFulcrumLib.Utilities.Text
{
    public static class StringBuilderExtensions
    {
        
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
