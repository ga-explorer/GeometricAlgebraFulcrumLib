namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Chebyshev;

public sealed class ChebyshevPolynomial2DMixed
{
    public int DegreeT { get; }
    public int DegreeS { get; }
    public ChebyshevBasis BasisT { get; }
    public ChebyshevBasis BasisS { get; }
    public double[,] Coeffs { get; } // [p,q]

    private ChebyshevPolynomial2D cachedTT; // cache T×T version if converted

    public ChebyshevPolynomial2DMixed(
        int degreeT,
        int degreeS,
        double[,] coeffs,
        ChebyshevBasis basisT,
        ChebyshevBasis basisS)
    {
        if (coeffs == null) throw new ArgumentNullException(nameof(coeffs));
        if (coeffs.GetLength(0) != degreeT + 1 || coeffs.GetLength(1) != degreeS + 1)
            throw new ArgumentException("Coefficient array dimensions mismatch");

        DegreeT = degreeT;
        DegreeS = degreeS;
        BasisT = basisT;
        BasisS = basisS;
        Coeffs = (double[,])coeffs.Clone();
    }

    /// <summary>
    /// Evaluate directly via axis-wise Clenshaw in the stored basis.
    /// Always works, no conversion.
    /// </summary>
    public double EvaluateDirect(double t, double s)
    {
        var temp = new double[DegreeT + 1];
        for (var p = 0; p <= DegreeT; p++)
        {
            var val = (BasisS == ChebyshevBasis.T)
                ? EvaluateClenshaw(Coeffs.GetRow(p), s, useU: false)
                : EvaluateClenshaw(Coeffs.GetRow(p), s, useU: true);

            temp[p] = val;
        }

        return (BasisT == ChebyshevBasis.T)
            ? EvaluateClenshaw(temp, t, useU: false)
            : EvaluateClenshaw(temp, t, useU: true);
    }

    /// <summary>
    /// Evaluate using cached conversion to pure T×T, then delegate.
    /// Faster if you call many evaluations on the same object.
    /// </summary>
    public double EvaluateViaTT(double t, double s)
    {
        if (cachedTT == null)
        {
            var coeffsTT = ToBasis(ChebyshevBasis.T, ChebyshevBasis.T);
            cachedTT = new ChebyshevPolynomial2D(DegreeT, DegreeS, coeffsTT);
        }
        return cachedTT.EvaluateAt(t, s);
    }

    /// <summary>
    /// General-purpose EvaluateAt. Choose fast-path or direct.
    /// </summary>
    public double EvaluateAt(double t, double s, bool fastPath = false)
    {
        return fastPath ? EvaluateViaTT(t, s) : EvaluateDirect(t, s);
    }

    // ---------------- Conversion methods ----------------

    public ChebyshevPolynomial2D ToTT()
    {
        var coeffs = ToBasis(ChebyshevBasis.T, ChebyshevBasis.T);
        return new ChebyshevPolynomial2D(DegreeT, DegreeS, coeffs);
    }

    public double[,] ToBasis(ChebyshevBasis newT, ChebyshevBasis newS)
    {
        var n = DegreeT;
        var m = DegreeS;
        var current = (double[,])Coeffs.Clone();

        // Convert s-axis if needed
        if (BasisS != newS)
        {
            var converted = new double[n + 1, m + 1];
            for (var p = 0; p <= n; p++)
            {
                var row = current.GetRow(p);
                if (BasisS == ChebyshevBasis.T && newS == ChebyshevBasis.U)
                    row = new ChebyshevPolynomialT(m, row).ToChebyshevU().Coeffs;
                else if (BasisS == ChebyshevBasis.U && newS == ChebyshevBasis.T)
                    row = new ChebyshevPolynomialU(m, row).ToChebyshevT().Coeffs;

                for (var q = 0; q <= m; q++) converted[p, q] = row[q];
            }
            current = converted;
        }

        // Convert t-axis if needed
        if (BasisT != newT)
        {
            var converted = new double[n + 1, m + 1];
            for (var q = 0; q <= m; q++)
            {
                var col = new double[n + 1];
                for (var p = 0; p <= n; p++) col[p] = current[p, q];

                if (BasisT == ChebyshevBasis.T && newT == ChebyshevBasis.U)
                    col = new ChebyshevPolynomialT(n, col).ToChebyshevU().Coeffs;
                else if (BasisT == ChebyshevBasis.U && newT == ChebyshevBasis.T)
                    col = new ChebyshevPolynomialU(n, col).ToChebyshevT().Coeffs;

                for (var p = 0; p <= n; p++) converted[p, q] = col[p];
            }
            current = converted;
        }

        return current;
    }

    // ---------------- Utilities ----------------

    private static double EvaluateClenshaw(double[] coeffs, double x, bool useU)
    {
        if (!useU)
        {
            double b1 = 0.0, b2 = 0.0;
            for (var k = coeffs.Length - 1; k >= 1; k--)
            {
                var b = 2 * x * b1 - b2 + coeffs[k];
                b2 = b1; b1 = b;
            }
            return x * b1 - b2 + coeffs[0];
        }
        else
        {
            double b1 = 0.0, b2 = 0.0;
            for (var k = coeffs.Length - 1; k >= 0; k--)
            {
                var b = 2 * x * b1 - b2 + coeffs[k];
                b2 = b1; b1 = b;
            }
            return b1;
        }
    }







    /// Evaluate at (t,s)
    public double EvaluateAt(double t, double s)
    {
        // evaluate inner sum for each p
        var temp = new double[DegreeT + 1];
        for (var p = 0; p <= DegreeT; p++)
        {
            var val = 0.0;
            if (BasisS == ChebyshevBasis.T)
            {
                val = EvaluateClenshaw(Coeffs.GetRow(p), s, useU: false);
            }
            else // U-basis in s
            {
                val = EvaluateClenshaw(Coeffs.GetRow(p), s, useU: true);
            }
            temp[p] = val;
        }

        // evaluate outer sum in t
        if (BasisT == ChebyshevBasis.T)
            return EvaluateClenshaw(temp, t, useU: false);
        else
            return EvaluateClenshaw(temp, t, useU: true);
    }

}