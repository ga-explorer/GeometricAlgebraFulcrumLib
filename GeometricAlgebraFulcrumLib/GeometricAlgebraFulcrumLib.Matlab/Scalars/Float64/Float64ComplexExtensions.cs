using System;
using System.Numerics;
using System.Runtime;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

/// <summary>
/// Extension methods for the Complex type provided by System.Numerics
/// </summary>
public static class Float64ComplexExtensions
{
    /// <summary>
    /// Gets the squared magnitude of the <c>Complex</c> number.
    /// </summary>
    /// <param name="complex">The <see cref="T:MathNet.Numerics.Complex32" /> number to perform this operation on.</param>
    /// <returns>The squared magnitude of the <c>Complex</c> number.</returns>
    public static double MagnitudeSquared(this Complex32 complex)
    {
        return complex.Real * (double)complex.Real + complex.Imaginary * (double)complex.Imaginary;
    }

    /// <summary>
    /// Gets the squared magnitude of the <c>Complex</c> number.
    /// </summary>
    /// <param name="complex">The <see cref="T:System.Numerics.Complex" /> number to perform this operation on.</param>
    /// <returns>The squared magnitude of the <c>Complex</c> number.</returns>
    public static double MagnitudeSquared(this Complex complex)
    {
        return complex.Real * complex.Real + complex.Imaginary * complex.Imaginary;
    }

    /// <summary>
    /// Gets the unity of this complex (same argument, but on the unit circle; exp(I*arg))
    /// </summary>
    /// <returns>The unity of this <c>Complex</c>.</returns>
    public static Complex Sign(this Complex complex)
    {
        if (double.IsPositiveInfinity(complex.Real) && double.IsPositiveInfinity(complex.Imaginary))
            return new Complex(0.70710678118654757, 0.70710678118654757);
        if (double.IsPositiveInfinity(complex.Real) && double.IsNegativeInfinity(complex.Imaginary))
            return new Complex(0.70710678118654757, -0.70710678118654757);
        if (double.IsNegativeInfinity(complex.Real) && double.IsPositiveInfinity(complex.Imaginary))
            return new Complex(-0.70710678118654757, -0.70710678118654757);
        if (double.IsNegativeInfinity(complex.Real) && double.IsNegativeInfinity(complex.Imaginary))
            return new Complex(-0.70710678118654757, 0.70710678118654757);
        var num = SpecialFunctions.Hypotenuse(complex.Real, complex.Imaginary);
        return num == 0.0 ? Complex.Zero : new Complex(complex.Real / num, complex.Imaginary / num);
    }

    /// <summary>
    /// Gets the conjugate of the <c>Complex</c> number.
    /// </summary>
    /// <param name="complex">The <see cref="T:System.Numerics.Complex" /> number to perform this operation on.</param>
    /// <remarks>
    /// The semantic of <i>setting the conjugate</i> is such that
    /// <code>
    /// // a, b of type Complex32
    /// a.Conjugate = b;
    /// </code>
    /// is equivalent to
    /// <code>
    /// // a, b of type Complex32
    /// a = b.Conjugate
    /// </code>
    /// </remarks>
    /// <returns>The conjugate of the <see cref="T:System.Numerics.Complex" /> number.</returns>
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static Complex Conjugate(this Complex complex)
    {
        return Complex.Conjugate(complex);
    }

    /// <summary>
    /// Returns the multiplicative inverse of a complex number.
    /// </summary>
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static Complex Reciprocal(this Complex complex)
    {
        return Complex.Reciprocal(complex);
    }

    /// <summary>
    /// Exponential of this <c>Complex</c> (exp(x), E^x).
    /// </summary>
    /// <param name="complex">The <see cref="T:System.Numerics.Complex" /> number to perform this operation on.</param>
    /// <returns>The exponential of this complex number.</returns>
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static Complex Exp(this Complex complex)
    {
        return Complex.Exp(complex);
    }

    /// <summary>
    /// Natural Logarithm of this <c>Complex</c> (Base E).
    /// </summary>
    /// <param name="complex">The <see cref="T:System.Numerics.Complex" /> number to perform this operation on.</param>
    /// <returns>The natural logarithm of this complex number.</returns>
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static Complex Ln(this Complex complex)
    {
        return Complex.Log(complex);
    }

    /// <summary>
    /// Common Logarithm of this <c>Complex</c> (Base 10).
    /// </summary>
    /// <returns>The common logarithm of this complex number.</returns>
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static Complex Log10(this Complex complex)
    {
        return Complex.Log10(complex);
    }

    /// <summary>
    /// Logarithm of this <c>Complex</c> with custom base.
    /// </summary>
    /// <returns>The logarithm of this complex number.</returns>
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static Complex Log(this Complex complex, double baseValue)
    {
        return Complex.Log(complex, baseValue);
    }

    /// <summary>
    /// Raise this <c>Complex</c> to the given value.
    /// </summary>
    /// <param name="complex">The <see cref="T:System.Numerics.Complex" /> number to perform this operation on.</param>
    /// <param name="exponent">The exponent.</param>
    /// <returns>The complex number raised to the given exponent.</returns>
    public static Complex Power(this Complex complex, Complex exponent)
    {
        if (!complex.IsZero())
            return Complex.Pow(complex, exponent);
        if (exponent.IsZero())
            return Complex.One;
        if (exponent.Real > 0.0)
            return Complex.Zero;
        if (exponent.Real >= 0.0)
            return new Complex(double.NaN, double.NaN);
        return exponent.Imaginary != 0.0 ? new Complex(double.PositiveInfinity, double.PositiveInfinity) : new Complex(double.PositiveInfinity, 0.0);
    }

    /// <summary>
    /// Raise this <c>Complex</c> to the inverse of the given value.
    /// </summary>
    /// <param name="complex">The <see cref="T:System.Numerics.Complex" /> number to perform this operation on.</param>
    /// <param name="rootExponent">The root exponent.</param>
    /// <returns>
    /// The complex raised to the inverse of the given exponent.
    /// </returns>
    public static Complex Root(this Complex complex, Complex rootExponent)
    {
        return Complex.Pow(complex, 1.0 / rootExponent);
    }

    /// <summary>
    /// The Square (power 2) of this <c>Complex</c>
    /// </summary>
    /// <param name="complex">The <see cref="T:System.Numerics.Complex" /> number to perform this operation on.</param>
    /// <returns>The square of this complex number.</returns>
    public static Complex Square(this Complex complex)
    {
        return complex.IsReal() ? new Complex(complex.Real * complex.Real, 0.0) : new Complex(complex.Real * complex.Real - complex.Imaginary * complex.Imaginary, 2.0 * complex.Real * complex.Imaginary);
    }

    /// <summary>
    /// The Square Root (power 1/2) of this <c>Complex</c>
    /// </summary>
    /// <param name="complex">The <see cref="T:System.Numerics.Complex" /> number to perform this operation on.</param>
    /// <returns>The square root of this complex number.</returns>
    public static Complex SquareRoot(this Complex complex)
    {
        if (complex.IsRealNonNegative())
            return new Complex(Math.Sqrt(complex.Real), 0.0);
        var d1 = Math.Abs(complex.Real);
        var d2 = Math.Abs(complex.Imaginary);
        double num1;
        if (d1 >= d2)
        {
            var num2 = complex.Imaginary / complex.Real;
            num1 = Math.Sqrt(d1) * Math.Sqrt(0.5 * (1.0 + Math.Sqrt(1.0 + num2 * num2)));
        }
        else
        {
            var num3 = complex.Real / complex.Imaginary;
            num1 = Math.Sqrt(d2) * Math.Sqrt(0.5 * (Math.Abs(num3) + Math.Sqrt(1.0 + num3 * num3)));
        }
        return complex.Real < 0.0 ? complex.Imaginary < 0.0 ? new Complex(d2 / (2.0 * num1), -num1) : new Complex(d2 / (2.0 * num1), num1) : new Complex(num1, complex.Imaginary / (2.0 * num1));
    }

    /// <summary>
    /// Evaluate all square roots of this <c>Complex</c>.
    /// </summary>
    public static (Complex, Complex) SquareRoots(this Complex complex)
    {
        var complex1 = complex.SquareRoot();
        return (complex1, -complex1);
    }

    /// <summary>
    /// Evaluate all cubic roots of this <c>Complex</c>.
    /// </summary>
    public static (Complex, Complex, Complex) CubicRoots(this Complex complex)
    {
        var magnitude = Math.Pow(complex.Magnitude, 1.0 / 3.0);
        var phase = complex.Phase / 3.0;
        return (Complex.FromPolarCoordinates(magnitude, phase), Complex.FromPolarCoordinates(magnitude, phase + 2.0 * Math.PI / 3.0), Complex.FromPolarCoordinates(magnitude, phase - 2.0 * Math.PI / 3.0));
    }

    /// <summary>
    /// Gets a value indicating whether the <c>Complex32</c> is zero.
    /// </summary>
    /// <param name="complex">The <see cref="T:System.Numerics.Complex" /> number to perform this operation on.</param>
    /// <returns><c>true</c> if this instance is zero; otherwise, <c>false</c>.</returns>
    public static bool IsZero(this Complex complex)
    {
        return complex is { Real: 0.0, Imaginary: 0.0 };
    }

    /// <summary>
    /// Gets a value indicating whether the <c>Complex32</c> is one.
    /// </summary>
    /// <param name="complex">The <see cref="T:System.Numerics.Complex" /> number to perform this operation on.</param>
    /// <returns><c>true</c> if this instance is one; otherwise, <c>false</c>.</returns>
    public static bool IsOne(this Complex complex)
    {
        return complex is { Real: 1.0, Imaginary: 0.0 };
    }

    /// <summary>
    /// Gets a value indicating whether the <c>Complex32</c> is the imaginary unit.
    /// </summary>
    /// <returns><c>true</c> if this instance is ImaginaryOne; otherwise, <c>false</c>.</returns>
    /// <param name="complex">The <see cref="T:System.Numerics.Complex" /> number to perform this operation on.</param>
    public static bool IsImaginaryOne(this Complex complex)
    {
        return complex is { Real: 0.0, Imaginary: 1.0 };
    }

    /// <summary>
    /// Gets a value indicating whether the provided <c>Complex32</c>evaluates
    /// to a value that is not a number.
    /// </summary>
    /// <param name="complex">The <see cref="T:System.Numerics.Complex" /> number to perform this operation on.</param>
    /// <returns>
    /// <c>true</c> if this instance is <c>NaN</c>; otherwise,
    /// <c>false</c>.
    /// </returns>
    public static bool IsNaN(this Complex complex)
    {
        return double.IsNaN(complex.Real) || double.IsNaN(complex.Imaginary);
    }

    /// <summary>
    /// Gets a value indicating whether the provided <c>Complex32</c> evaluates to an
    /// infinite value.
    /// </summary>
    /// <param name="complex">The <see cref="T:System.Numerics.Complex" /> number to perform this operation on.</param>
    /// <returns>
    ///     <c>true</c> if this instance is infinite; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// True if it either evaluates to a complex infinity
    /// or to a directed infinity.
    /// </remarks>
    public static bool IsInfinity(this Complex complex)
    {
        return double.IsInfinity(complex.Real) || double.IsInfinity(complex.Imaginary);
    }

    /// <summary>
    /// Gets a value indicating whether the provided <c>Complex32</c> is real.
    /// </summary>
    /// <param name="complex">The <see cref="T:System.Numerics.Complex" /> number to perform this operation on.</param>
    /// <returns><c>true</c> if this instance is a real number; otherwise, <c>false</c>.</returns>
    public static bool IsReal(this Complex complex)
    {
        return complex.Imaginary == 0.0;
    }

    /// <summary>
    /// Gets a value indicating whether the provided <c>Complex32</c> is real and not negative, that is &gt;= 0.
    /// </summary>
    /// <param name="complex">The <see cref="T:System.Numerics.Complex" /> number to perform this operation on.</param>
    /// <returns>
    ///     <c>true</c> if this instance is real nonnegative number; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsRealNonNegative(this Complex complex)
    {
        return complex is { Imaginary: 0.0, Real: >= 0.0 };
    }

    /// <summary>
    /// Returns a Norm of a value of this type, which is appropriate for measuring how
    /// close this value is to zero.
    /// </summary>
    public static double Norm(this Complex complex)
    {
        return complex.MagnitudeSquared();
    }

    /// <summary>
    /// Returns a Norm of a value of this type, which is appropriate for measuring how
    /// close this value is to zero.
    /// </summary>
    public static double Norm(this Complex32 complex)
    {
        return complex.MagnitudeSquared;
    }

    /// <summary>
    /// Returns a Norm of the difference of two values of this type, which is
    /// appropriate for measuring how close together these two values are.
    /// </summary>
    public static double NormOfDifference(this Complex complex, Complex otherValue)
    {
        return (complex - otherValue).MagnitudeSquared();
    }

    /// <summary>
    /// Returns a Norm of the difference of two values of this type, which is
    /// appropriate for measuring how close together these two values are.
    /// </summary>
    public static double NormOfDifference(this Complex32 complex, Complex32 otherValue)
    {
        return (complex - otherValue).MagnitudeSquared;
    }

    /// <summary>
    /// Creates a complex number based on a string. The string can be in the
    /// following formats (without the quotes): 'n', 'ni', 'n +/- ni',
    /// 'ni +/- n', 'n,n', 'n,ni,' '(n,n)', or '(n,ni)', where n is a double.
    /// </summary>
    /// <returns>
    /// A complex number containing the value specified by the given string.
    /// </returns>
    /// <param name="value">The string to parse.</param>
    public static Complex ToComplex(this string value)
    {
        return value.ToComplex(null);
    }

    ///// <summary>
    ///// Creates a complex number based on a string. The string can be in the
    ///// following formats (without the quotes): 'n', 'ni', 'n +/- ni',
    ///// 'ni +/- n', 'n,n', 'n,ni,' '(n,n)', or '(n,ni)', where n is a double.
    ///// </summary>
    ///// <returns>
    ///// A complex number containing the value specified by the given string.
    ///// </returns>
    ///// <param name="value">the string to parse.</param>
    ///// <param name="formatProvider">
    ///// An <see cref="T:System.IFormatProvider" /> that supplies culture-specific
    ///// formatting information.
    ///// </param>
    //public static Complex ToComplex(this string value, IFormatProvider formatProvider)
    //{
    //    value = value != null ? value.Trim() : throw new ArgumentNullException(nameof(value));
    //    if (value.Length == 0)
    //        throw new FormatException();
    //    if (value.StartsWith("(", StringComparison.Ordinal))
    //        value = value.EndsWith(")", StringComparison.Ordinal) ? value.Substring(1, value.Length - 2).Trim() : throw new FormatException();
    //    NumberFormatInfo numberFormatInfo = formatProvider.GetNumberFormatInfo();
    //    TextInfo textInfo = formatProvider.GetTextInfo();
    //    var keywords = new string[8]
    //    {
    //        textInfo.ListSeparator,
    //        numberFormatInfo.NaNSymbol,
    //        numberFormatInfo.NegativeInfinitySymbol,
    //        numberFormatInfo.PositiveInfinitySymbol,
    //        "+",
    //        "-",
    //        "i",
    //        "j"
    //    };
    //    var linkedList = new LinkedList<string>();
    //    GlobalizationHelper.Tokenize(linkedList.AddFirst(value), keywords, 0);
    //    var first = linkedList.First;
    //    var part1 = Float64ComplexExtensions.ParsePart(ref first, out var imaginary1, formatProvider);
    //    if (first == null)
    //        return !imaginary1 ? new Complex(part1, 0.0) : new Complex(0.0, part1);
    //    if (first.Value == textInfo.ListSeparator)
    //    {
    //        var next = first.Next;
    //        if (imaginary1)
    //            throw new FormatException();
    //        var part2 = Float64ComplexExtensions.ParsePart(ref next, out var _, formatProvider);
    //        return new Complex(part1, part2);
    //    }

    //    var part3 = Float64ComplexExtensions.ParsePart(ref first, out var imaginary2, formatProvider);
    //    if (!(imaginary1 ^ imaginary2))
    //        throw new FormatException();
    //    return !imaginary1 ? new Complex(part1, part3) : new Complex(part3, part1);
    //}

    ///// <summary>Parse a part (real or complex) from a complex number.</summary>
    ///// <param name="token">Start Token.</param>
    ///// <param name="imaginary">Is set to <c>true</c> if the part identified itself as being imaginary.</param>
    ///// <param name="format">
    ///// An <see cref="T:System.IFormatProvider" /> that supplies culture-specific
    ///// formatting information.
    ///// </param>
    ///// <returns>Resulting part as double.</returns>
    ///// <exception cref="T:System.FormatException" />
    //private static double ParsePart(
    //    ref LinkedListNode<string> token,
    //    out bool imaginary,
    //    IFormatProvider format)
    //{
    //    imaginary = false;
    //    if (token == null)
    //        throw new FormatException();
    //    if (token.Value == "+")
    //    {
    //        token = token.Next;
    //        if (token == null)
    //            throw new FormatException();
    //    }
    //    var flag = false;
    //    if (token.Value == "-")
    //    {
    //        flag = true;
    //        token = token.Next;
    //        if (token == null)
    //            throw new FormatException();
    //    }
    //    if (string.Compare(token.Value, "i", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(token.Value, "j", StringComparison.OrdinalIgnoreCase) == 0)
    //    {
    //        imaginary = true;
    //        token = token.Next;
    //        if (token == null)
    //            return flag ? -1.0 : 1.0;
    //    }
    //    var num = GlobalizationHelper.ParseDouble(ref token, format.GetCultureInfo());
    //    if (token != null && (string.Compare(token.Value, "i", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(token.Value, "j", StringComparison.OrdinalIgnoreCase) == 0))
    //    {
    //        if (imaginary)
    //            throw new FormatException();
    //        imaginary = true;
    //        token = token.Next;
    //    }
    //    return !flag ? num : -num;
    //}

    /// <summary>
    /// Converts the string representation of a complex number to a double-precision complex number equivalent.
    /// A return value indicates whether the conversion succeeded or failed.
    /// </summary>
    /// <param name="value">
    /// A string containing a complex number to convert.
    /// </param>
    /// <param name="result">The parsed value.</param>
    /// <returns>
    /// If the conversion succeeds, the result will contain a complex number equivalent to value.
    /// Otherwise, the result will contain Complex.Zero.  This parameter is passed uninitialized.
    /// </returns>
    public static bool TryToComplex(this string value, out Complex result)
    {
        return value.TryToComplex(null, out result);
    }

    /// <summary>
    /// Converts the string representation of a complex number to double-precision complex number equivalent.
    /// A return value indicates whether the conversion succeeded or failed.
    /// </summary>
    /// <param name="value">
    /// A string containing a complex number to convert.
    /// </param>
    /// <param name="formatProvider">
    /// An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information about value.
    /// </param>
    /// <param name="result">The parsed value.</param>
    /// <returns>
    /// If the conversion succeeds, the result will contain a complex number equivalent to value.
    /// Otherwise, the result will contain complex32.Zero.  This parameter is passed uninitialized
    /// </returns>
    public static bool TryToComplex(
        this string value,
        IFormatProvider formatProvider,
        out Complex result)
    {
        try
        {
            result = value.ToComplex(formatProvider);
            return true;
        }
        catch (ArgumentNullException)
        {
            result = Complex.Zero;
            return false;
        }
        catch (FormatException)
        {
            result = Complex.Zero;
            return false;
        }
    }

    /// <summary>
    /// Creates a <c>Complex32</c> number based on a string. The string can be in the
    /// following formats (without the quotes): 'n', 'ni', 'n +/- ni',
    /// 'ni +/- n', 'n,n', 'n,ni,' '(n,n)', or '(n,ni)', where n is a double.
    /// </summary>
    /// <returns>
    /// A complex number containing the value specified by the given string.
    /// </returns>
    /// <param name="value">the string to parse.</param>
    public static Complex32 ToComplex32(this string value)
    {
        return Complex32.Parse(value);
    }

    /// <summary>
    /// Creates a <c>Complex32</c> number based on a string. The string can be in the
    /// following formats (without the quotes): 'n', 'ni', 'n +/- ni',
    /// 'ni +/- n', 'n,n', 'n,ni,' '(n,n)', or '(n,ni)', where n is a double.
    /// </summary>
    /// <returns>
    /// A complex number containing the value specified by the given string.
    /// </returns>
    /// <param name="value">the string to parse.</param>
    /// <param name="formatProvider">
    /// An <see cref="T:System.IFormatProvider" /> that supplies culture-specific
    /// formatting information.
    /// </param>
    public static Complex32 ToComplex32(this string value, IFormatProvider formatProvider)
    {
        return Complex32.Parse(value, formatProvider);
    }

    /// <summary>
    /// Converts the string representation of a complex number to a single-precision complex number equivalent.
    /// A return value indicates whether the conversion succeeded or failed.
    /// </summary>
    /// <param name="value">
    /// A string containing a complex number to convert.
    /// </param>
    /// <param name="result">The parsed value.</param>
    /// <returns>
    /// If the conversion succeeds, the result will contain a complex number equivalent to value.
    /// Otherwise, the result will contain complex32.Zero.  This parameter is passed uninitialized.
    /// </returns>
    public static bool TryToComplex32(this string value, out Complex32 result)
    {
        return Complex32.TryParse(value, out result);
    }

    /// <summary>
    /// Converts the string representation of a complex number to single-precision complex number equivalent.
    /// A return value indicates whether the conversion succeeded or failed.
    /// </summary>
    /// <param name="value">
    /// A string containing a complex number to convert.
    /// </param>
    /// <param name="formatProvider">
    /// An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information about value.
    /// </param>
    /// <param name="result">The parsed value.</param>
    /// <returns>
    /// If the conversion succeeds, the result will contain a complex number equivalent to value.
    /// Otherwise, the result will contain Complex.Zero.  This parameter is passed uninitialized.
    /// </returns>
    public static bool TryToComplex32(
        this string value,
        IFormatProvider formatProvider,
        out Complex32 result)
    {
        return Complex32.TryParse(value, formatProvider, out result);
    }
}