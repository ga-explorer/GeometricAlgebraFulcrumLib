using System.Numerics;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Fixed128;
#nullable enable

/// <summary>
/// Small BigInteger extensions used (integer sqrt).
/// </summary>
static class BigIntegerExtensions
{
    /// <summary>
    /// Integer square root floor(sqrt(n)) for non-negative BigInteger n.
    /// </summary>
    public static BigInteger IntegerSqrt(BigInteger n)
    {
        if (n.Sign < 0) throw new ArgumentException("Negative argument");
        if (n.IsZero) return BigInteger.Zero;
        // Newton method starting with 1 << ((bitlength + 1)/2)
        var bitLen = (int)n.GetBitLength();
        var x = BigInteger.One << ((bitLen + 1) / 2);
        while (true)
        {
            var nx = (x + n / x) >> 1;
            if (nx == x || nx == x - 1) // stabilized
            {
                if (nx * nx > n) return nx - 1;
                return nx;
            }
            x = nx;
        }
    }

    /// <summary>
    /// Return bit length of BigInteger.
    /// </summary>
    public static int GetBitLength(this BigInteger value)
    {
        if (value.IsZero) return 0;
        value = BigInteger.Abs(value);
        var bytes = value.ToByteArray();
        // Most significant byte may have leading zeros; compute from highest byte
        int msb = bytes[^1];
        var bitsInMsb = 8;
        while ((msb & (1 << (bitsInMsb - 1))) == 0) bitsInMsb--;
        return (bytes.Length - 1) * 8 + bitsInMsb;
    }
}