using System.Globalization;
using System.Numerics;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Fixed128;

/// <summary>
/// Arbitrary-sign fixed-point number that stores values of the form:
///     value = (sign ? -1 : +1) * (Integer + Fraction / 2^128)
/// where Integer and Fraction are UInt128. Fraction is always in [0, 2^128-1].
/// Absolute value is strictly less than 2^128 (since Integer &lt; 2^128).
/// </summary>
public readonly struct Real128 : IComparable<Real128>, IEquatable<Real128>
{
    // Public fields (immutable)
    public readonly bool Negative;
    public readonly UInt128 Integer;   // integral part (0 .. 2^128-1)
    public readonly UInt128 Fraction;  // fractional numerator (0 .. 2^128-1), representing Fraction / 2^128

    // Constants
    public static readonly Real128 Zero = new Real128(false, default, default);
    public static readonly Real128 One  = new Real128(false, (UInt128)1, default);
    public static readonly Real128 MinusOne = new Real128(true, (UInt128)1, default);
    private const int FractionBits = 128;

    #region Constructors / Factory
    public Real128(bool negative, UInt128 integer, UInt128 fraction)
    {
        Negative = negative;
        Integer = integer;
        Fraction = fraction;
    }

    /// <summary>
    /// Construct from integer-only value (signed long).
    /// </summary>
    public Real128(long value)
    {
        if (value < 0)
        {
            Negative = true;
            var abs = unchecked((ulong)(-value));
            Integer = abs;
        }
        else
        {
            Negative = false;
            Integer = (UInt128)value;
        }
        Fraction = default;
    }

    /// <summary>
    /// Construct from unsigned integer.
    /// </summary>
    public Real128(ulong value)
    {
        Negative = false;
        Integer = value;
        Fraction = default;
    }

    /// <summary>
    /// Create from scaled BigInteger: scaled = signed * (value * 2^128)
    /// Used internally for arithmetic convenience.
    /// </summary>
    private static Real128 FromScaledBigInteger(BigInteger scaled)
    {
        // scaled may be negative
        var negative = scaled < 0;
        if (negative) scaled = BigInteger.Negate(scaled);

        // integerPart = scaled >> 128
        var intPart = scaled >> FractionBits;
        var fracPart = scaled & ((BigInteger.One << FractionBits) - 1);

        // convert intPart and fracPart to UInt128
        var intU = BigIntegerToUInt128(intPart);
        var fracU = BigIntegerToUInt128(fracPart);

        return new Real128(negative, intU, fracU);
    }

    /// <summary>
    /// Convert Real128 to signed scaled BigInteger = signed * ( (Integer << 128) + Fraction )
    /// </summary>
    private BigInteger ToScaledBigInteger()
    {
        var high = UInt128ToBigInteger(Integer);
        var low  = UInt128ToBigInteger(Fraction);
        var scaled = (high << FractionBits) | low;
        return Negative ? BigInteger.Negate(scaled) : scaled;
    }
    #endregion

    #region UInt128 <-> BigInteger helpers
    /// <summary>
    /// Convert a UInt128 to BigInteger. 
    /// Assumes UInt128 supports shifting and casting to ulong for low/high 64-bit parts.
    /// If your UInt128 type uses different methods, replace this helper accordingly.
    /// </summary>
    private static BigInteger UInt128ToBigInteger(UInt128 u)
    {
        // Attempt to extract low and high 64-bit parts
        // (ulong)u gives low 64 bits; (u >> 64) >> cast to ulong gives high 64 bits.
        var low = (ulong)u;
        var shifted = u >> 64;
        var high = (ulong)shifted;
        var res = (new BigInteger(high) << 64) | new BigInteger(low);
        return res;
    }

    /// <summary>
    /// Convert a non-negative BigInteger (assumed &lt; 2^128) to UInt128.
    /// </summary>
    private static UInt128 BigIntegerToUInt128(BigInteger b)
    {
        if (b.Sign < 0) throw new ArgumentOutOfRangeException(nameof(b), "Expected non-negative BigInteger");
        // Extract low/high 64-bit words
        var mask64 = (BigInteger.One << 64) - 1;
        var low = (ulong)(b & mask64);
        var high = (ulong)((b >> 64) & mask64);

        // Build UInt128 from high/low assuming UInt128 can be constructed from (UInt128(high) << 64) | low
        // There's no standard constructor, but we can create by high << 64 + low using shifts and casts.
        var res = ((UInt128)high << 64) | low;
        return res;
    }
    #endregion

    #region Basic operators: + - * /
    public static Real128 operator +(Real128 a, Real128 b)
    {
        // add scaled integers
        var ascaled = a.ToScaledBigInteger();
        var bscaled = b.ToScaledBigInteger();
        var sum = ascaled + bscaled;
        return FromScaledBigInteger(sum);
    }

    public static Real128 operator -(Real128 a, Real128 b)
    {
        var ascaled = a.ToScaledBigInteger();
        var bscaled = b.ToScaledBigInteger();
        var diff = ascaled - bscaled;
        return FromScaledBigInteger(diff);
    }

    public static Real128 operator -(Real128 v)
    {
        if (v.Integer == (UInt128)0 && v.Fraction == (UInt128)0) return v;
        return new Real128(!v.Negative, v.Integer, v.Fraction);
    }

    public static Real128 operator *(Real128 a, Real128 b)
    {
        // Use scaled representation: productScaled = (ascaled * bscaled) >> 128
        var ascaled = a.ToScaledBigInteger();
        var bscaled = b.ToScaledBigInteger();

        var prod = ascaled * bscaled;
        // Now prod is scaled by 2^256 relative to integer; we need to shift right by 128 to get scaled by 2^128
        var scaledResult = prod >> FractionBits;

        return FromScaledBigInteger(scaledResult);
    }

    public static Real128 operator /(Real128 a, Real128 b)
    {
        if (b.IsZero) throw new DivideByZeroException();
        var ascaled = a.ToScaledBigInteger();
        var bscaled = b.ToScaledBigInteger();

        // We want (ascaled / bscaled) * 2^128 scaled result:
        // resultScaled = (ascaled << 128) / bscaled
        var numerator = ascaled << FractionBits;
        var resultScaled = BigInteger.Divide(numerator, bscaled);
        return FromScaledBigInteger(resultScaled);
    }
    #endregion

    #region Comparisons, equality
    public int CompareTo(Real128 other)
    {
        var a = this.ToScaledBigInteger();
        var b = other.ToScaledBigInteger();
        return a.CompareTo(b);
    }

    public static bool operator ==(Real128 a, Real128 b) => a.Equals(b);
    public static bool operator !=(Real128 a, Real128 b) => !a.Equals(b);
    public static bool operator <(Real128 a, Real128 b) => a.CompareTo(b) < 0;
    public static bool operator >(Real128 a, Real128 b) => a.CompareTo(b) > 0;
    public static bool operator <=(Real128 a, Real128 b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Real128 a, Real128 b) => a.CompareTo(b) >= 0;

    public bool Equals(Real128 other)
    {
        return Negative == other.Negative && Integer == other.Integer && Fraction == other.Fraction;
    }

    public override bool Equals(object? obj)
    {
        return obj is Real128 r && Equals(r);
    }

    public override int GetHashCode()
    {
        // Mix fields
        unchecked
        {
            var h = Negative ? -137 : 137;
            h = (h * 397) ^ Integer.GetHashCode();
            h = (h * 397) ^ Fraction.GetHashCode();
            return h;
        }
    }
    #endregion

    #region Helpers / Predicates / Conversions
    public bool IsZero => Integer == (UInt128)0 && Fraction == (UInt128)0;
    public bool IsIntegral => Fraction == (UInt128)0;
    public int Sign => IsZero ? 0 : (Negative ? -1 : 1);

    /// <summary>
    /// Approximate conversion to double. May lose precision for very large or very precise values.
    /// </summary>
    public double ToDouble()
    {
        // Convert integer and fraction separately to doubles
        var intPart = UInt128ToBigInteger(Integer);
        var fracPart = UInt128ToBigInteger(Fraction);

        var dInt = (double)intPart;
        var dFrac = (double)fracPart / Math.Pow(2.0, FractionBits);
        var res = dInt + dFrac;
        return Negative ? -res : res;
    }

    /// <summary>
    /// Approximate conversion to decimal (128-bit decimal). Precision loss possible.
    /// </summary>
    public decimal ToDecimal()
    {
        var intPart = UInt128ToBigInteger(Integer);
        var fracPart = UInt128ToBigInteger(Fraction);
        var dInt = (decimal)intPart;
        var dFrac = (decimal)fracPart / (decimal)BigInteger.Pow(2, FractionBits);
        var res = dInt + dFrac;
        return Negative ? -res : res;
    }

    public override string ToString()
    {
        // Produce decimal string using double for the fractional portion where necessary
        if (IsZero) return "0";

        // integer part as decimal string
        var intPart = UInt128ToBigInteger(Integer);
        var sInt = intPart.ToString(CultureInfo.InvariantCulture);

        if (Fraction == (UInt128)0)
        {
            return (Negative ? "-" : "") + sInt;
        }

        // For the fraction, produce 38 decimal digits (approx 128 bits ~ 38.5 decimal digits).
        // We'll compute decimal fraction by scaling fraction to decimal digits.
        const int decDigits = 38;
        var fracNum = UInt128ToBigInteger(Fraction);
        var scale = BigInteger.Pow(10, decDigits);
        var scaled = (fracNum * scale) >> FractionBits; // floor
        var fracStr = scaled.ToString().PadLeft(decDigits, '0');

        // Trim trailing zeros
        fracStr = fracStr.TrimEnd('0');
        if (fracStr.Length == 0) return (Negative ? "-" : "") + sInt;
        return string.Concat(Negative ? "-" : "", sInt, ".", fracStr);
    }

    public static Real128 Parse(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) throw new FormatException("Input string empty");
        s = s.Trim();
        var negative = false;
        if (s[0] == '+' || s[0] == '-')
        {
            negative = s[0] == '-';
            s = s.Substring(1).Trim();
        }

        var parts = s.Split('.', 2);
        var intPart = BigInteger.Zero;
        if (parts.Length >= 1 && parts[0].Length > 0)
            intPart = BigInteger.Parse(parts[0], CultureInfo.InvariantCulture);

        var fracPart = BigInteger.Zero;
        if (parts.Length == 2 && parts[1].Length > 0)
        {
            // convert decimal fraction to scaled fraction: fracDecimal / 10^d -> fracScaled = round(fracDecimal * 2^128 / 10^d)
            var decStr = parts[1].TrimEnd('0');
            if (decStr.Length > 0)
            {
                var fracDec = BigInteger.Parse(decStr, CultureInfo.InvariantCulture);
                var scaleDec = BigInteger.Pow(10, decStr.Length);
                var numerator = fracDec * BigInteger.Pow(2, FractionBits);
                fracPart = BigInteger.Divide(numerator + scaleDec / 2, scaleDec); // rounding
            }
        }

        var intU = BigIntegerToUInt128(intPart);
        var fracU = BigIntegerToUInt128(fracPart);
        return new Real128(negative, intU, fracU);
    }
    #endregion

    #region Math-like functions (high-precision where feasible)
    public static Real128 Abs(Real128 v) => v.Negative ? -v : v;

    /// <summary>
    /// Floor: largest integer &lt;= value
    /// </summary>
    public static Real128 Floor(Real128 v)
    {
        if (v.IsZero) return v;
        if (!v.Negative)
        {
            // positive: just drop fraction
            return new Real128(false, v.Integer, (UInt128)0);
        }
        else
        {
            // negative: if fraction is zero, integer part is already exact; otherwise result is -(integer+1)
            if (v.Fraction == (UInt128)0)
                return v;
            else
            {
                // -(Integer + 1)
                var newInt = v.Integer + (UInt128)1;
                return new Real128(true, newInt, (UInt128)0);
            }
        }
    }

    public static Real128 Ceiling(Real128 v)
    {
        if (v.IsZero) return v;
        if (v.Negative)
        {
            // negative: drop fraction (i.e., move toward zero)
            return new Real128(true, v.Integer, (UInt128)0);
        }
        else
        {
            if (v.Fraction == (UInt128)0)
                return v;
            else
            {
                var newInt = v.Integer + (UInt128)1;
                return new Real128(false, newInt, (UInt128)0);
            }
        }
    }

    public static Real128 Truncate(Real128 v) => v.Negative ? Ceiling(v) : Floor(v);

    public static Real128 Round(Real128 v)
    {
        // Round to nearest integer, ties to even implemented via fractional comparison to 0.5
        if (v.IsZero) return v;
        var half = (UInt128)1 << (FractionBits - 1);
        if (v.Negative)
        {
            if (v.Fraction > half) // more negative than -n.5
                return new Real128(true, v.Integer + (UInt128)1, (UInt128)0);
            if (v.Fraction < half)
                return new Real128(true, v.Integer, (UInt128)0);
            // tie: half exactly -> round to even
            var integerIsEven = (((ulong)(v.Integer & (UInt128)1)) == 0);
            return integerIsEven ? new Real128(true, v.Integer, (UInt128)0) : new Real128(true, v.Integer + (UInt128)1, (UInt128)0);
        }
        else
        {
            if (v.Fraction > half)
                return new Real128(false, v.Integer + (UInt128)1, (UInt128)0);
            if (v.Fraction < half)
                return new Real128(false, v.Integer, (UInt128)0);
            var integerIsEven = (((ulong)(v.Integer & (UInt128)1)) == 0);
            return integerIsEven ? new Real128(false, v.Integer, (UInt128)0) : new Real128(false, v.Integer + (UInt128)1, (UInt128)0);
        }
    }

    /// <summary>
    /// Square root computed with Newton's method on the scaled integer value to high precision.
    /// Returns NaN for negative inputs (throws here).
    /// </summary>
    public static Real128 Sqrt(Real128 v)
    {
        if (v.Negative) throw new ArithmeticException("Sqrt of negative value");
        if (v.IsZero) return Zero;

        // Work with scaled integer S = (Integer << 128) + Fraction; we want sqrt(value)
        // Let scaledS = S (scaled by 2^128). The actual value is scaledS / 2^128.
        // sqrt(value) = sqrt(scaledS / 2^128) = sqrt(scaledS) / 2^64.
        // So if we compute sqrtScaled = floor(sqrt(scaledS) << 64), then
        // resultScaled = sqrtScaled (representing value*2^128)
        var S = v.ToScaledBigInteger(); // non-negative
        
        // Compute sqrtS = floor(sqrt(S))
        var sqrtS = BigIntegerExtensions.IntegerSqrt(S);
        
        // sqrt(value) = sqrtS / 2^64 (but we need scaled by 2^128 => multiply by 2^128)
        // Instead, compute resultScaled = (sqrtS << 64)
        var resultScaled = sqrtS << 64;
        
        // To get rounding-tighter, do one Newton iteration on resultScaled maybe; but this is sufficiently good.
        return FromScaledBigInteger(resultScaled);
    }

    #endregion

    #region Transcendentals (double-backed)
    // NOTE: For these functions we convert to double and call Math. This loses precision beyond double,
    // but is simple and consistent. If you need high-precision transcendental functions, we can implement
    // them later using arbitrary-precision series or continued fractions.
    public static Real128 Exp(Real128 v)
    {
        var d = v.ToDouble();
        return FromDouble(Math.Exp(d));
    }

    public static Real128 Log(Real128 v)
    {
        var d = v.ToDouble();
        if (d <= 0.0) throw new ArithmeticException("Log domain error");
        return FromDouble(Math.Log(d));
    }

    public static Real128 Sin(Real128 v) => FromDouble(Math.Sin(v.ToDouble()));
    public static Real128 Cos(Real128 v) => FromDouble(Math.Cos(v.ToDouble()));
    public static Real128 Tan(Real128 v) => FromDouble(Math.Tan(v.ToDouble()));
    public static Real128 Asin(Real128 v) => FromDouble(Math.Asin(v.ToDouble()));
    public static Real128 Acos(Real128 v) => FromDouble(Math.Acos(v.ToDouble()));
    public static Real128 Atan(Real128 v) => FromDouble(Math.Atan(v.ToDouble()));
    #endregion

    #region Conversions from double/decimal/integers
    public static Real128 FromDouble(double d)
    {
        if (double.IsNaN(d) || double.IsInfinity(d))
            throw new ArgumentException("double value not finite", nameof(d));
        var negative = d < 0.0;
        if (negative) d = -d;
        var intpart = Math.Floor(d);
        var frac = d - intpart;
        // convert integer to BigInteger, then to UInt128 (may overflow if >= 2^128)
        var bigInt = new BigInteger(intpart);
        var i = BigIntegerToUInt128(bigInt);
        var fracScaled = new BigInteger(frac * Math.Pow(2.0, FractionBits) + 0.5);
        if (fracScaled >= (BigInteger.One << FractionBits)) // round-up effect
        {
            i = i + (UInt128)1;
            fracScaled -= (BigInteger.One << FractionBits);
        }
        var f = BigIntegerToUInt128(fracScaled);
        return new Real128(negative, i, f);
    }

    public static Real128 FromDecimal(decimal d)
    {
        var negative = d < 0;
        if (negative) d = -d;
        var intPart = Math.Floor(d);
        var frac = d - intPart;
        var bigInt = new BigInteger((double)intPart);
        var i = BigIntegerToUInt128(bigInt);
        // Convert fractional decimal to scaled fraction
        var scale = (decimal)BigInteger.Pow(2, FractionBits);
        var fracScaled = new BigInteger(frac * scale + 0.5m);
        if (fracScaled >= (BigInteger.One << FractionBits))
        {
            i = i + (UInt128)1;
            fracScaled -= (BigInteger.One << FractionBits);
        }
        var f = BigIntegerToUInt128(fracScaled);
        return new Real128(negative, i, f);
    }
    #endregion
}