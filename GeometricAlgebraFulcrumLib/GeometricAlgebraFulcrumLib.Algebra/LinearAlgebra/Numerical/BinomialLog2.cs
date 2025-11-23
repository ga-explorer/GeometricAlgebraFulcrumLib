namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;

public static class BinomialLog2
{
    private static readonly double Ln2Inv = 1d / Math.Log(2);
    private const double TwoPi = 2.0 * Math.PI;

    /// <summary>
    /// Approximates log2(binomial(n, k)) using Stirling's approximation.
    /// Handles very large n and k efficiently without overflow.
    /// </summary>
    /// <param name="n">Total number of items</param>
    /// <param name="k">Number of selected items</param>
    /// <returns>log2(binomial(n, k))</returns>
    public static double BinomialLog2ViaStirling(this long n, long k)
    {
        if (k < 0 || k > n)
            return double.NegativeInfinity;

        if (k == 0 || k == n)
            return 0.0;

        // Use symmetry: C(n,k) = C(n, n-k)
        k = Math.Min(k, n - k);

        // Convert to double (safe for n up to ~1e15, but Stirling works well for n > 20)
        double dn = n;
        double dk = k;
        double dnk = n - k;

        // Compute log(n!) using Stirling's approximation with 1/(12n) correction
        var logNFact = FactorialLnViaStirling(dn);
        var logKFact = FactorialLnViaStirling(dk);
        var logNkFact = FactorialLnViaStirling(dnk);

        // log2(C(n,k)) = (ln(n!) - ln(k!) - ln((n-k)!)) / ln(2)
        return (logNFact - logKFact - logNkFact) * Ln2Inv;
    }

    /// <summary>
    /// Stirling's approximation for ln(n!)
    /// ln(n!) ≈ n*ln(n) - n + 0.5*ln(2πn) + 1/(12n) - 1/(360n^3)
    /// </summary>
    public static double FactorialLnViaStirling(this double m)
    {
        if (m == 0) return 0.0;

        var logM = Math.Log(m);
        var invM = 1.0 / m;
        var invM2 = invM * invM;

        var term = m * logM - m
                   + 0.5 * Math.Log(TwoPi * m)
                   + invM / 12
                   - invM2 * invM / 360; // 1/(360 m^3)

        return term;
    }

    /// <summary>
    /// Approximates log2(binomial(n, k)) using entropy-based approximation
    /// useful when k ≈ αn for α between 0 and 1
    /// </summary>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static double BinomialLog2ViaEntropy(this long n, long k)
    {
        if (k == 0 || k == n) return 0.0;
        if (k < 0 || k > n) return double.NegativeInfinity;

        var alpha = (double)k / n;
        if (alpha is <= 0 or >= 1) return 0.0;

        var h = -alpha * Math.Log2(alpha) - (1 - alpha) * Math.Log2(1 - alpha);
        var correction = 0.5 * Math.Log2(2 * Math.PI * n * alpha * (1 - alpha));

        return n * h - correction;
    }
}