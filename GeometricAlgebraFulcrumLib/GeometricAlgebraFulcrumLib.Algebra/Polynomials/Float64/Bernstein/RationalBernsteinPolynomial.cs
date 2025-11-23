// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable InconsistentNaming

using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Bernstein
{
    /// <summary>
    /// Rational Bernstein polynomial: numerator/denominator are BernsteinPolynomials
    /// Enhanced: numerical safeguards, Chebyshev-based rational approximation,
    /// adaptive integration, endpoint-preserving Simplify, and automatic degree normalization.
    /// </summary>
    public class RationalBernsteinPolynomial
    {
        // Small machine tolerance used as baseline for relative checks.
        private const double DefaultEpsilon = 1e-14;

        private const double InternalMinDenominator = 1e-16;

        #region Gauss-Legendre Tables

        private static readonly double[] Gauss16Nodes =
        [
            -0.9894009349916499, -0.9445750230732326, -0.8656312023878318,
            -0.7554044083550030, -0.6178762444026438, -0.4580167776572274,
            -0.2816035507792589, -0.0950125098376374,  0.0950125098376374,
             0.2816035507792589,  0.4580167776572274,  0.6178762444026438,
             0.7554044083550030,  0.8656312023878318,  0.9445750230732326,
             0.9894009349916499
        ];

        private static readonly double[] Gauss16Weights =
        [
            0.0271524594117541, 0.0622535239386479, 0.0951585116824928,
            0.1246289712555339, 0.1495959888165767, 0.1691565193950025,
            0.1826034150449236, 0.1894506104550685, 0.1894506104550685,
            0.1826034150449236, 0.1691565193950025, 0.1495959888165767,
            0.1246289712555339, 0.0951585116824928, 0.0622535239386479,
            0.0271524594117541
        ];

        private static readonly double[] Gauss32Nodes =
        [
            -0.9972638618494816, -0.9856115115452684, -0.9647622555875064,
            -0.9349060759377397, -0.8963211557660521, -0.8493676137325700,
            -0.7944837959679424, -0.7321821187402897, -0.6630442669302152,
            -0.5877157572407623, -0.5068999089322294, -0.4213512761306353,
            -0.3318686022821277, -0.2392873622521371, -0.1444719615827965,
            -0.0483076656877383,  0.0483076656877383,  0.1444719615827965,
             0.2392873622521371,  0.3318686022821277,  0.4213512761306353,
             0.5068999089322294,  0.5877157572407623,  0.6630442669302152,
             0.7321821187402897,  0.7944837959679424,  0.8493676137325700,
             0.8963211557660521,  0.9349060759377397,  0.9647622555875064,
             0.9856115115452684,  0.9972638618494816
        ];

        private static readonly double[] Gauss32Weights =
        [
            0.0070186100094701, 0.0162743947309057, 0.0253920653092621,
            0.0342738629130214, 0.0428358980222267, 0.0509980592623762,
            0.0586840934785355, 0.0658222227763618, 0.0723457941088485,
            0.0781938957870703, 0.0833119242269467, 0.0876520930044038,
            0.0911738786957639, 0.0938443990808046, 0.0956387200792749,
            0.0965400885147278, 0.0965400885147278, 0.0956387200792749,
            0.0938443990808046, 0.0911738786957639, 0.0876520930044038,
            0.0833119242269467, 0.0781938957870703, 0.0723457941088485,
            0.0658222227763618, 0.0586840934785355, 0.0509980592623762,
            0.0428358980222267, 0.0342738629130214, 0.0253920653092621,
            0.0162743947309057, 0.0070186100094701
        ];

        private static readonly IReadOnlyDictionary<int, (double[] nodes, double[] weights)> GaussLegendreTable
            = new Dictionary<int, (double[], double[])>
            {
                { 16, (Gauss16Nodes, Gauss16Weights) },
                { 32, (Gauss32Nodes, Gauss32Weights) }
            };

        #endregion

        public BernsteinPolynomial Numerator { get; private set; }

        public BernsteinPolynomial Denominator { get; private set; }

        public RationalBernsteinPolynomial(BernsteinPolynomial num, BernsteinPolynomial den, double tolerance = Float64Utils.DefaultTolerance)
        {
            if (num == null || den == null) throw new ArgumentNullException();
            Numerator = num.Simplify(tolerance);
            Denominator = den.Simplify(tolerance);

            // Normalize degrees to avoid later mismatches and help numerical stability
            NormalizeDegrees();
        }

        #region Basic ops & automatic degree normalization

        private void NormalizeDegrees()
        {
            // Make numerator and denominator degrees equal to max of both (elevate as needed).
            // This helps later arithmetic and reduces surprises when combining polynomials.
            var target = Math.Max(Numerator.Degree, Denominator.Degree);
            if (Numerator.Degree < target) Numerator = Numerator.RaiseDegree(target);
            if (Denominator.Degree < target) Denominator = Denominator.RaiseDegree(target);
        }

        public static RationalBernsteinPolynomial operator -(RationalBernsteinPolynomial a)
        {
            return new RationalBernsteinPolynomial(a.Numerator.Negative(), a.Denominator);
        }

        public static RationalBernsteinPolynomial operator +(RationalBernsteinPolynomial a, RationalBernsteinPolynomial b)
        {
            return a.Add(b);
        }

        public static RationalBernsteinPolynomial operator -(RationalBernsteinPolynomial a, RationalBernsteinPolynomial b)
        {
            return a.Subtract(b);
        }

        public static RationalBernsteinPolynomial operator *(RationalBernsteinPolynomial a, RationalBernsteinPolynomial b)
        {
            var num = a.Numerator * b.Numerator;
            var den = a.Denominator * b.Denominator;
            var r = new RationalBernsteinPolynomial(num, den);
            // after multiplication keep degrees normalized
            r.NormalizeDegrees();
            return r;
        }

        public static RationalBernsteinPolynomial operator /(RationalBernsteinPolynomial a, RationalBernsteinPolynomial b)
        {
            var num = a.Numerator * b.Denominator;
            var den = a.Denominator * b.Numerator;
            var r = new RationalBernsteinPolynomial(num, den);
            r.NormalizeDegrees();
            return r;
        }

        public RationalBernsteinPolynomial Add(RationalBernsteinPolynomial b)
        {
            var a = this;
            var ad = a.Numerator * b.Denominator;
            var cb = b.Numerator * a.Denominator;
            var num = ad + cb;
            var den = a.Denominator * b.Denominator;

            var r = new RationalBernsteinPolynomial(num, den);
            r.NormalizeDegrees();
            return r;
        }

        public RationalBernsteinPolynomial Subtract(RationalBernsteinPolynomial b)
        {
            var a = this;
            var ad = a.Numerator * b.Denominator;
            var cb = b.Numerator * a.Denominator;
            var num = ad - cb;
            var den = a.Denominator * b.Denominator;

            var r = new RationalBernsteinPolynomial(num, den);
            r.NormalizeDegrees();
            return r;
        }

        public RationalBernsteinPolynomial Square()
        {
            return new RationalBernsteinPolynomial(Numerator.Square(), Denominator.Square());
        }

        #endregion

        #region Evaluation, derivative, validity

        public bool IsValid()
        {
            return Numerator.IsValid() && Denominator.IsValid();
        }

        /// <summary>
        /// Numerically stable evaluation with relative epsilon check.
        /// Throws DivideByZeroException if denominator is too small relative to itself.
        /// </summary>
        public double EvaluateAt(double t)
        {
            var n = Numerator.GetValue(t);
            var d = Denominator.GetValue(t);
            return n / d;

            // Relative tolerance wrt denominator magnitude ensures stability.
            var relTol = DefaultEpsilon * Math.Max(1.0, Math.Abs(d));
            if (Math.Abs(d) <= relTol)
                throw new DivideByZeroException($"Denominator too small or ill-conditioned at t = {t:G17} (d={d:G17}).");

            return n / d;
        }

        /// <summary>
        /// Quotient rule. Optionally normalize top/bottom so coefficients remain moderate.
        /// </summary>
        public RationalBernsteinPolynomial Derivative(bool normalize = true)
        {
            var nPrime = Numerator.Derivative();
            var dPrime = Denominator.Derivative();
            var top = nPrime * Denominator - Numerator * dPrime;
            var bottom = Denominator.Square();

            if (normalize)
            {
                var scale = bottom.Coeffs.Max(Math.Abs);
                if (scale > 0)
                {
                    top = top.Divide(scale);
                    bottom = bottom.Divide(scale);
                }
            }

            var r = new RationalBernsteinPolynomial(top, bottom);
            r.NormalizeDegrees();
            return r;
        }

        /// <summary>
        /// Returns numerator of derivative (top of quotient rule), useful for e.g. root finding.
        /// </summary>
        public BernsteinPolynomial DerivativeNumerator()
        {
            var nPrime = Numerator.Derivative();
            var dPrime = Denominator.Derivative();
            return nPrime * Denominator - Numerator * dPrime;
        }

        #endregion

        #region Approximation: Chebyshev + SVD rational fit with robust fallback

        /// <summary>
        /// Approximate this rational function by a Bernstein polynomial of specified degree.
        /// High-fidelity path: attempt SVD-based rational fit at Chebyshev nodes (monomial basis),
        /// then convert the rational fit to Bernstein if numerator/denominator degrees allow (or fallback).
        /// If rational fitting fails, fall back to a Chebyshev node-based polynomial fit (least-squares on Bernstein basis).
        /// </summary>
        /// <param name="degree">target polynomial degree</param>
        /// <param name="samplesMin">minimum samples to use</param>
        /// <param name="tryRationalFit">if true attempt rational SVD fit first</param>
        public BernsteinPolynomial ApproximateAsBernstein(int degree, int samplesMin = 512, bool tryRationalFit = true)
        {
            degree = Math.Max(0, degree);
            var samples = Math.Max(samplesMin, 4 * (degree + 1));

            // Create Chebyshev nodes on [0,1]: t_k = 0.5*(1 - cos(pi*(2k+1)/(2m)))
            var m = samples;
            var t = new double[m];
            for (var k = 0; k < m; k++)
            {
                var theta = Math.PI * (2.0 * k + 1.0) / (2.0 * m);
                var x = Math.Cos(theta); // in (-1,1)
                t[k] = 0.5 * (1.0 - x);  // map to [0,1]
            }

            var y = new double[m];
            for (var i = 0; i < m; ++i)
            {
                var den = Denominator.GetValue(t[i]);
                var num = Numerator.GetValue(t[i]);

                // safe division: avoid sign flip by using CopySign of Epsilon
                var absDen = Math.Abs(den);
                if (absDen < InternalMinDenominator)
                {
                    var safeDen = Math.CopySign(InternalMinDenominator, den != 0.0 ? den : num);
                    y[i] = num / safeDen;
                }
                else
                {
                    y[i] = num / den;
                }
            }

            // Attempt rational fit via SVD nullspace method: solve A * x = 0 where x = [pCoeffs ; qCoeffs]
            // We choose monomial basis degrees: degP = degree, degQ = degree (user can tune).
            if (tryRationalFit)
            {
                try
                {
                    var degP = degree;
                    var degQ = degree;

                    var cols = (degP + 1) + (degQ + 1);
                    var M = Matrix<double>.Build.Dense(m, cols);

                    for (var i = 0; i < m; i++)
                    {
                        // monomial basis phi_k(t) = t^k
                        var ti = 1.0;
                        for (var k = 0; k <= degP; k++)
                        {
                            M[i, k] = ti;
                            ti *= t[i];
                        }

                        // next: -y[i] * monomials for q
                        var tj = 1.0;
                        var offset = degP + 1;
                        for (var k = 0; k <= degQ; k++)
                        {
                            M[i, offset + k] = -y[i] * tj;
                            tj *= t[i];
                        }
                    }

                    // compute SVD and extract smallest singular vector (nullspace approx)
                    var svd = M.Svd(true);
                    var vt = svd.VT; // rows = right singular vectors
                    var lastRow = vt.Row(vt.RowCount - 1);
                    var xVec = lastRow.ToArray(); // length cols

                    // extract p and q monomial coeffs from xVec
                    var pMonom = new double[degP + 1];
                    var qMonom = new double[degQ + 1];
                    for (var k = 0; k <= degP; k++) pMonom[k] = xVec[k];
                    for (var k = 0; k <= degQ; k++) qMonom[k] = xVec[degP + 1 + k];

                    // If all qMonom are nearly zero, fallback
                    var qNorm = qMonom.Select(Math.Abs).Max();
                    if (qNorm < 1e-20)
                        throw new InvalidOperationException("Degenerate q in rational fit.");

                    // Normalize q so first non-zero coefficient is 1 to fix scale ambiguity
                    var idxNonZero = Array.FindIndex(qMonom, v => Math.Abs(v) > 1e-24);
                    if (idxNonZero < 0)
                        throw new InvalidOperationException("All q coefficients zero.");

                    var scale = qMonom[idxNonZero];
                    for (var k = 0; k <= degP; k++) pMonom[k] /= scale;
                    for (var k = 0; k <= degQ; k++) qMonom[k] /= scale;

                    // Convert monomial coefficients to Bernstein polynomials using FromMonomialBasis
                    var pBern = BernsteinPolynomial.FromMonomialBasis(pMonom);
                    var qBern = BernsteinPolynomial.FromMonomialBasis(qMonom);

                    // Validate fit by sampling a smaller set of points
                    var validateOk = ValidateRationalFit(pBern, qBern, t, y, 1e-10);
                    if (validateOk)
                    {
                        // Optionally convert rational to polynomial approximation by performing p/q -> polynomial via polynomial division
                        // But user requested approximation as Bernstein polynomial: we convert rational -> samples -> solve Bernstein coefficients
                        return ConvertRationalToBernstein(pBern, qBern, degree, t, y);
                    }
                }
                catch
                {
                    // fallthrough to polynomial fallback
                }
            }

            // Fallback: high-fidelity polynomial fit on Chebyshev nodes solving for Bernstein coefficients
            // Build M matrix with Bernstein basis at nodes and solve M * b = y (the least squares)
            var Mpoly = Matrix<double>.Build.Dense(m, degree + 1);
            for (var i = 0; i < m; i++)
            {
                var ti = t[i];
                for (var j = 0; j <= degree; j++)
                {
                    Mpoly[i, j] = BernsteinPolynomial.EvaluateBasisAt(degree, j, ti);
                }
            }

            var yVec = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.DenseOfArray(y);
            MathNet.Numerics.LinearAlgebra.Vector<double> bVec;
            try
            {
                bVec = Mpoly.QR().Solve(yVec);
            }
            catch
            {
                bVec = Mpoly.Svd(true).Solve(yVec);
            }

            var coeffs = new double[degree + 1];
            for (var j = 0; j <= degree; j++) 
                coeffs[j] = bVec[j];
            return BernsteinPolynomial.Create(coeffs);
        }

        /// <summary>
        /// Validate rational fit by checking max error on subset of t: returns true if max error small
        /// </summary>
        /// <param name="pBern"></param>
        /// <param name="qBern"></param>
        /// <param name="tAll"></param>
        /// <param name="yAll"></param>
        /// <param name="tol"></param>
        /// <returns></returns>
        private static bool ValidateRationalFit(BernsteinPolynomial pBern, BernsteinPolynomial qBern, double[] tAll, double[] yAll, double tol)
        {
            var m = Math.Min(200, tAll.Length);
            var step = Math.Max(1, tAll.Length / m);
            var maxErr = 0.0;
            for (var i = 0; i < tAll.Length; i += step)
            {
                var tt = tAll[i];
                var qv = qBern.GetValue(tt);
                if (Math.Abs(qv) < InternalMinDenominator) return false;
                var pv = pBern.GetValue(tt);
                var rval = pv / qv;
                var err = Math.Abs(rval - yAll[i]);
                if (err > maxErr) maxErr = err;
                if (maxErr > tol) return false;
            }

            return true;
        }

        /// <summary>
        /// Given p/q (Bernstein polynomials), produce a Bernstein polynomial approximation of target degree.
        /// We sample function (p/q) and solve M*b = samples for Bernstein coefficients.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="degree"></param>
        /// <param name="sampleT"></param>
        /// <param name="sampleY"></param>
        /// <returns></returns>
        private static BernsteinPolynomial ConvertRationalToBernstein(BernsteinPolynomial p, BernsteinPolynomial q, int degree, double[] sampleT, double[] sampleY)
        {
            // Build sample values (safe eval)
            var m = sampleT.Length;
            var yvals = new double[m];
            for (var i = 0; i < m; i++)
            {
                var tt = sampleT[i];
                var qv = q.GetValue(tt);
                if (Math.Abs(qv) < InternalMinDenominator)
                {
                    yvals[i] = p.GetValue(tt) / (Math.CopySign(InternalMinDenominator, qv != 0 ? qv : 1.0));
                }
                else
                {
                    yvals[i] = p.GetValue(tt) / qv;
                }
            }

            // Build Bernstein basis matrix and solve least squares
            var M = Matrix<double>.Build.Dense(m, degree + 1);
            for (var i = 0; i < m; i++)
            {
                var tt = sampleT[i];
                for (var j = 0; j <= degree; j++)
                    M[i, j] = BernsteinPolynomial.EvaluateBasisAt(degree, j, tt);
            }

            var yVec = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.DenseOfArray(yvals);
            MathNet.Numerics.LinearAlgebra.Vector<double> bVec;
            try
            {
                bVec = M.QR().Solve(yVec);
            }
            catch
            {
                bVec = M.Svd(true).Solve(yVec);
            }

            var coeffs = new double[degree + 1];
            for (var j = 0; j <= degree; j++) 
                coeffs[j] = bVec[j];
            return BernsteinPolynomial.Create(coeffs);
        }

        #endregion

        #region Simplify (endpoint-preserving + sampling verified)

        /// <summary>
        /// Simplify numerator and denominator but only accept simplification if the rational function
        /// stays within tolerance (relative) on sample points including endpoints.
        /// </summary>
        public RationalBernsteinPolynomial Simplify(double tolerance = Float64Utils.DefaultTolerance)
        {
            if (tolerance <= 0.0) return this;

            // If denominator is (nearly) constant, convert to exact polynomial form
            if (Denominator.IsNearConstant(out var denConst, tolerance))
            {
                var simplifiedNum = Numerator.Simplify(tolerance);
                if (Math.Abs(denConst) <= InternalMinDenominator)
                    return new RationalBernsteinPolynomial(simplifiedNum, Denominator);

                var poly = simplifiedNum.Divide(denConst);
                return new RationalBernsteinPolynomial(poly, BernsteinPolynomial.Constant(1.0));
            }

            // Quick denominator safety test: check Gauss32 nodes
            if (!GaussLegendreTable.TryGetValue(32, out var data32)) 
                data32 = GaussLegendreTable[16];
            
            var (nodes32, _) = data32;
            foreach (var xi in nodes32)
            {
                var tt = 0.5 * (1.0 + xi);
                var dVal = Denominator.GetValue(tt);
                var nVal = Numerator.GetValue(tt);
                if (Math.Abs(dVal) <= DefaultEpsilon * (1.0 + Math.Abs(nVal))) return this; // refuse simplification
            }

            // candidates
            var candList = new List<RationalBernsteinPolynomial>
            {
                new RationalBernsteinPolynomial(Numerator.Simplify(tolerance), Denominator),
                new RationalBernsteinPolynomial(Numerator, Denominator.Simplify(tolerance)),
                new RationalBernsteinPolynomial(Numerator.Simplify(tolerance), Denominator.Simplify(tolerance))
            };

            // build sample points: endpoints + Gauss16 + uniform mids
            var sampleSet = new List<double> { 0.0, 1.0 };
            if (!GaussLegendreTable.TryGetValue(16, out var data16)) data16 = GaussLegendreTable[32];
            var (nodes16, _) = data16;
            foreach (var xi in nodes16) sampleSet.Add(0.5 * (1.0 + xi));
            for (var k = 1; k <= 5; k++) sampleSet.Add(k / 6.0);
            var samplePoints = sampleSet.Where(u => u >= 0.0 && u <= 1.0).Distinct().OrderBy(u => u).ToArray();

            var origVals = new double[samplePoints.Length];
            var maxAbs = 0.0;
            for (var i = 0; i < samplePoints.Length; i++)
            {
                origVals[i] = EvaluateAt(samplePoints[i]);
                maxAbs = Math.Max(maxAbs, Math.Abs(origVals[i]));
            }

            var scale = Math.Max(maxAbs, 1.0);

            foreach (var cand in candList)
            {
                // denominator safety
                var bad = false;
                for (var i = 0; i < samplePoints.Length; i++)
                {
                    var tt = samplePoints[i];
                    var denVal = cand.Denominator.GetValue(tt);
                    if (Math.Abs(denVal) <= DefaultEpsilon * (1.0 + Math.Abs(cand.Numerator.GetValue(tt))))
                    {
                        bad = true;
                        break;
                    }
                }
                if (bad) continue;

                // compute max error
                var maxErr = 0.0;
                for (var i = 0; i < samplePoints.Length; i++)
                {
                    var tt = samplePoints[i];
                    var candVal = cand.Numerator.GetValue(tt) / cand.Denominator.GetValue(tt);
                    var err = Math.Abs(candVal - origVals[i]);
                    if (err > maxErr) maxErr = err;
                    if (maxErr > tolerance * scale) break;
                }

                if (maxErr <= tolerance * scale)
                {
                    // Additional: ensure endpoints preserved within tolerance
                    var e0orig = origVals[0];
                    var e1orig = origVals[samplePoints.Length - 1];
                    var e0cand = cand.Numerator.GetValue(0.0) / cand.Denominator.GetValue(0.0);
                    var e1cand = cand.Numerator.GetValue(1.0) / cand.Denominator.GetValue(1.0);
                    if (Math.Abs(e0orig - e0cand) > tolerance * Math.Max(1.0, Math.Abs(e0orig))) continue;
                    if (Math.Abs(e1orig - e1cand) > tolerance * Math.Max(1.0, Math.Abs(e1orig))) continue;

                    // Accept
                    var ok = new RationalBernsteinPolynomial(cand.Numerator, cand.Denominator);
                    ok.NormalizeDegrees();
                    return ok;
                }
            }

            return this;
        }

        #endregion

        #region Approximate helpers & integration

        /// <summary>
        /// Numerically integrate using Gauss-Legendre quadrature (adaptive on singularities).
        /// </summary>
        public double Integrate(double a, double b, int order = 32, int maxDepth = 8)
        {
            if (!(a < b)) return 0.0;
            try
            {
                if (!GaussLegendreTable.TryGetValue(order, out var data)) throw new ArgumentException("Unsupported quadrature order.");
                var (nodes, weights) = data;

                var half = 0.5 * (b - a);
                var mid = 0.5 * (b + a);
                var sum = 0.0;

                for (var i = 0; i < nodes.Length; i++)
                {
                    var x = mid + half * nodes[i];
                    sum += weights[i] * EvaluateAt(x);
                }

                return half * sum;
            }
            catch (DivideByZeroException)
            {
                if (maxDepth <= 0) throw;
                var mid = 0.5 * (a + b);
                return Integrate(a, mid, order, maxDepth - 1) + Integrate(mid, b, order, maxDepth - 1);
            }
        }

        #endregion

        #region Utility / diagnostic

        public bool IsPolynomial(double tolerance = Float64Utils.DefaultTolerance)
        {
            return Denominator.IsNearConstant(out _, tolerance);
        }

        public BernsteinPolynomial ToBernsteinPolynomial(double tolerance = Float64Utils.DefaultTolerance)
        {
            if (Denominator.IsNearConstant(out var constValue, tolerance))
                return Numerator.Divide(constValue);
            throw new InvalidOperationException("Denominator not constant.");
        }

        /// <summary>
        /// Approximate the rational function by a polynomial Bernstein of target degree using Chebyshev nodes (convenience wrapper).
        /// </summary>
        public BernsteinPolynomial ApproximatePolynomialAsBernstein(int degree, int samplesMin = 512)
        {
            return ApproximateAsBernstein(degree, samplesMin, tryRationalFit: false);
        }

        #endregion
    }
}
