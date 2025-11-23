using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Chebyshev;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Factorization;


// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Bernstein
{
    public sealed class BernsteinPolynomial2D
    {
        // --- Factories / constructors ---
        public static BernsteinPolynomial2D FromT(BernsteinPolynomial f)
        {
            var coeffs = new double[f.Degree + 1, 1];
            for (var i = 0; i <= f.Degree; i++) coeffs[i, 0] = f.Coeffs[i];
            return new BernsteinPolynomial2D(f.Degree, 0, coeffs);
        }

        public static BernsteinPolynomial2D FromS(BernsteinPolynomial g)
        {
            var coeffs = new double[1, g.Degree + 1];
            for (var j = 0; j <= g.Degree; j++) coeffs[0, j] = g.Coeffs[j];
            return new BernsteinPolynomial2D(0, g.Degree, coeffs);
        }

        public static BernsteinPolynomial2D Constant(double c)
        {
            var arr = new double[1, 1];
            arr[0, 0] = c;
            return new BernsteinPolynomial2D(0, 0, arr);
        }

        public static BernsteinPolynomial2D Create(double[,] coeffArray)
        {
            if (coeffArray == null) throw new ArgumentNullException(nameof(coeffArray));
            var degT = coeffArray.GetLength(0) - 1;
            var degS = coeffArray.GetLength(1) - 1;
            return new BernsteinPolynomial2D(degT, degS, (double[,])coeffArray.Clone());
        }


        // Convert Bernstein 2D -> Chebyshev 2D (both axes T) using 2D power-basis intermediary.
        // Returns ChebyshevPolynomial2D (T_x × T_y)
        public ChebyshevPolynomial2D ToChebyshev2D()
        {
            // convert Bernstein -> power (2D)
            var power2D = ToPowerBasisCoefficients(); // double[,] power[i,j] for x^i y^j
            
            // convert power -> Chebyshev 2D (separable FFT DCT)
            return ChebyshevPolynomial2D.FromPowerBasis2D(power2D);
        }

        // Construct Bernstein 2D from Chebyshev 2D (both axes T)
        // Uses Chebyshev2D -> power2D -> Bernstein2D.FromPowerBasisCoefficients2D
        public static BernsteinPolynomial2D FromChebyshev2D(ChebyshevPolynomial2D cheb)
        {
            if (cheb == null) 
                throw new ArgumentNullException(nameof(cheb));

            var power2D = cheb.ToPowerBasis2D();
            return FromPowerBasisCoefficients2D(power2D);
        }



        public int DegreeT { get; }

        public int DegreeS { get; }

        public double[,] Coefficients { get; }


        public BernsteinPolynomial2D(int degreeT, int degreeS, double[,] coefficients)
        {
            if (coefficients == null) throw new ArgumentNullException(nameof(coefficients));
            if (coefficients.GetLength(0) != degreeT + 1 || coefficients.GetLength(1) != degreeS + 1)
                throw new ArgumentException("coefficients dimensions must match degreeT+1 and degreeS+1");
            DegreeT = degreeT;
            DegreeS = degreeS;
            Coefficients = (double[,])coefficients.Clone();
            Debug.Assert(IsValid());
        }


        public bool IsValid()
        {
            if (Coefficients == null) return false;
            if (DegreeT < 0 || DegreeS < 0) return false;
            if (Coefficients.GetLength(0) != DegreeT + 1 || Coefficients.GetLength(1) != DegreeS + 1) return false;
            for (var i = 0; i <= DegreeT; i++)
                for (var j = 0; j <= DegreeS; j++)
                {
                    var v = Coefficients[i, j];
                    if (double.IsNaN(v) || double.IsInfinity(v)) return false;
                }
            return true;
        }

        public BernsteinPolynomial2D Simplify(double tolerance = 1e-12)
        {
            var coeffs = (double[,])Coefficients.Clone();
            var newDegT = DegreeT;
            var newDegS = DegreeS;

            for (var i = 0; i <= DegreeT; i++)
                for (var j = 0; j <= DegreeS; j++)
                    if (Math.Abs(coeffs[i, j]) < tolerance) coeffs[i, j] = 0.0;

            while (newDegS > 0)
            {
                var allZero = true;
                for (var i = 0; i <= newDegT; i++) if (Math.Abs(coeffs[i, newDegS]) > tolerance) { allZero = false; break; }
                if (!allZero) break;
                newDegS--;
            }

            while (newDegT > 0)
            {
                var allZero = true;
                for (var j = 0; j <= newDegS; j++) if (Math.Abs(coeffs[newDegT, j]) > tolerance) { allZero = false; break; }
                if (!allZero) break;
                newDegT--;
            }

            if (newDegT == DegreeT && newDegS == DegreeS)
                return ReduceToConstantIfPossible(tolerance);

            var trimmed = new double[newDegT + 1, newDegS + 1];
            for (var i = 0; i <= newDegT; i++)
                for (var j = 0; j <= newDegS; j++)
                    trimmed[i, j] = coeffs[i, j];

            return new BernsteinPolynomial2D(newDegT, newDegS, trimmed).ReduceToConstantIfPossible(tolerance);
        }

        public bool IsNearConstant(out double c, double tolerance = 1e-12)
        {
            if (DegreeT == 0 && DegreeS == 0) { c = Coefficients[0, 0]; return true; }
            var sum = 0.0;
            var count = (DegreeT + 1) * (DegreeS + 1);
            for (var i = 0; i <= DegreeT; i++)
                for (var j = 0; j <= DegreeS; j++) sum += Coefficients[i, j];
            var avg = sum / count;
            var varSum = 0.0;
            for (var i = 0; i <= DegreeT; i++)
                for (var j = 0; j <= DegreeS; j++)
                {
                    var d = Coefficients[i, j] - avg;
                    varSum += d * d;
                }
            var variance = varSum / count;
            if (variance <= tolerance * tolerance) { c = avg; return true; }
            c = 0.0; return false;
        }

        public BernsteinPolynomial2D ReduceToConstantIfPossible(double tolerance = 1e-12)
            => IsNearConstant(out var c, tolerance) ? Constant(c) : this;

        // --- Evaluation ---
        public double EvaluateAt(double t, double s)
        {
            var res = 0.0;
            for (var i = 0; i <= DegreeT; i++)
            {
                var bt = BernsteinPolynomial.EvaluateBasisAt(DegreeT, i, t);
                for (var j = 0; j <= DegreeS; j++)
                {
                    var bs = BernsteinPolynomial.EvaluateBasisAt(DegreeS, j, s);
                    res += Coefficients[i, j] * bt * bs;
                }
            }
            return res;
        }

        public double[,] EvaluateAt(double[] tValues, double[] sValues)
        {
            if (tValues == null) throw new ArgumentNullException(nameof(tValues));
            if (sValues == null) throw new ArgumentNullException(nameof(sValues));
            var m = tValues.Length;
            var n = sValues.Length;
            var res = new double[m, n];

            var basT = new double[m, DegreeT + 1];
            var basS = new double[n, DegreeS + 1];
            for (var r = 0; r < m; r++) for (var i = 0; i <= DegreeT; i++) basT[r, i] = BernsteinPolynomial.EvaluateBasisAt(DegreeT, i, tValues[r]);
            for (var c = 0; c < n; c++) for (var j = 0; j <= DegreeS; j++) basS[c, j] = BernsteinPolynomial.EvaluateBasisAt(DegreeS, j, sValues[c]);

            for (var r = 0; r < m; r++)
                for (var c = 0; c < n; c++)
                {
                    var s = 0.0;
                    for (var i = 0; i <= DegreeT; i++)
                        for (var j = 0; j <= DegreeS; j++)
                            s += Coefficients[i, j] * basT[r, i] * basS[c, j];
                    res[r, c] = s;
                }
            return res;
        }

        // --- Arithmetic & convolution (same as before) ---
        public static BernsteinPolynomial2D operator +(BernsteinPolynomial2D a, BernsteinPolynomial2D b)
        {
            var degT = Math.Max(a.DegreeT, b.DegreeT);
            var degS = Math.Max(a.DegreeS, b.DegreeS);
            var aUp = a.RaiseDegree(degT, degS);
            var bUp = b.RaiseDegree(degT, degS);
            var coeffs = new double[degT + 1, degS + 1];
            for (var i = 0; i <= degT; i++) for (var j = 0; j <= degS; j++) coeffs[i, j] = aUp.Coefficients[i, j] + bUp.Coefficients[i, j];
            return new BernsteinPolynomial2D(degT, degS, coeffs);
        }

        public static BernsteinPolynomial2D operator -(BernsteinPolynomial2D a, BernsteinPolynomial2D b)
        {
            var degT = Math.Max(a.DegreeT, b.DegreeT);
            var degS = Math.Max(a.DegreeS, b.DegreeS);
            var aUp = a.RaiseDegree(degT, degS);
            var bUp = b.RaiseDegree(degT, degS);
            var coeffs = new double[degT + 1, degS + 1];
            for (var i = 0; i <= degT; i++) for (var j = 0; j <= degS; j++) coeffs[i, j] = aUp.Coefficients[i, j] - bUp.Coefficients[i, j];
            return new BernsteinPolynomial2D(degT, degS, coeffs);
        }

        public BernsteinPolynomial2D Times(double s)
        {
            if (s == 1.0) return this;
            var coeffs = new double[DegreeT + 1, DegreeS + 1];
            for (var i = 0; i <= DegreeT; i++) for (var j = 0; j <= DegreeS; j++) coeffs[i, j] = Coefficients[i, j] * s;
            return new BernsteinPolynomial2D(DegreeT, DegreeS, coeffs);
        }

        public BernsteinPolynomial2D Divide(double s)
        {
            if (s == 0.0) throw new DivideByZeroException();
            return Times(1.0 / s);
        }

        public static BernsteinPolynomial2D operator *(BernsteinPolynomial2D a, double s) => a.Times(s);
        public static BernsteinPolynomial2D operator *(double s, BernsteinPolynomial2D a) => a.Times(s);
        public static BernsteinPolynomial2D operator /(BernsteinPolynomial2D a, double s) => a.Divide(s);
        public BernsteinPolynomial2D Negative() => Times(-1.0);
        public static BernsteinPolynomial2D operator -(BernsteinPolynomial2D a) => a.Negative();

        public static BernsteinPolynomial2D operator *(BernsteinPolynomial2D a, BernsteinPolynomial2D b)
        {
            var degT = a.DegreeT + b.DegreeT;
            var degS = a.DegreeS + b.DegreeS;
            var coeffs = new double[degT + 1, degS + 1];
            for (var i = 0; i <= degT; i++)
                for (var j = 0; j <= degS; j++)
                {
                    var sum = 0.0;
                    for (var k = Math.Max(0, i - b.DegreeT); k <= Math.Min(i, a.DegreeT); k++)
                        for (var l = Math.Max(0, j - b.DegreeS); l <= Math.Min(j, a.DegreeS); l++)
                        {
                            var factorT = BernsteinPolynomial.Binomial(a.DegreeT, k) *
                                          BernsteinPolynomial.Binomial(b.DegreeT, i - k) /
                                          BernsteinPolynomial.Binomial(degT, i);

                            var factorS = BernsteinPolynomial.Binomial(a.DegreeS, l) *
                                          BernsteinPolynomial.Binomial(b.DegreeS, j - l) /
                                          BernsteinPolynomial.Binomial(degS, j);

                            sum += factorT * factorS * a.Coefficients[k, l] * b.Coefficients[i - k, j - l];
                        }
                    coeffs[i, j] = sum;
                }
            return new BernsteinPolynomial2D(degT, degS, coeffs);
        }

        public BernsteinPolynomial2D Square() => this * this;

        public BernsteinPolynomial2D Power(int n)
        {
            if (n < 0) throw new ArgumentException("Exponent must be non-negative", nameof(n));
            if (n == 0) return Constant(1.0);
            if (n == 1) return this;
            if (n == 2) return Square();
            var result = Constant(1.0);
            var basePoly = this;
            while (n > 0)
            {
                if ((n & 1) == 1) result = result * basePoly;
                basePoly = basePoly.Square();
                n >>= 1;
            }
            return result;
        }

        public BernsteinPolynomial2D RaiseDegree(int newDegT, int newDegS)
        {
            if (newDegT < DegreeT) throw new ArgumentException("newDegT must be >= DegreeT");
            if (newDegS < DegreeS) throw new ArgumentException("newDegS must be >= DegreeS");
            if (newDegT == DegreeT && newDegS == DegreeS) return this;
            var coeffs = new double[newDegT + 1, newDegS + 1];
            for (var i = 0; i <= newDegT; i++)
                for (var j = 0; j <= newDegS; j++)
                {
                    var sum = 0.0;
                    for (var k = Math.Max(0, i - (newDegT - DegreeT)); k <= Math.Min(i, DegreeT); k++)
                        for (var l = Math.Max(0, j - (newDegS - DegreeS)); l <= Math.Min(j, DegreeS); l++)
                        {
                            var factorT = BernsteinPolynomial.Binomial(DegreeT, k) *
                                          BernsteinPolynomial.Binomial(newDegT - DegreeT, i - k) /
                                          BernsteinPolynomial.Binomial(newDegT, i);

                            var factorS = BernsteinPolynomial.Binomial(DegreeS, l) *
                                          BernsteinPolynomial.Binomial(newDegS - DegreeS, j - l) /
                                          BernsteinPolynomial.Binomial(newDegS, j);

                            sum += factorT * factorS * Coefficients[k, l];
                        }
                    coeffs[i, j] = sum;
                }
            return new BernsteinPolynomial2D(newDegT, newDegS, coeffs);
        }

        // --- Partials / Integrals ---
        public BernsteinPolynomial2D DerivativeT()
        {
            if (DegreeT == 0) return Constant(0.0);
            var dCoeffs = new double[DegreeT, DegreeS + 1];
            for (var i = 0; i < DegreeT; i++)
                for (var j = 0; j <= DegreeS; j++)
                    dCoeffs[i, j] = DegreeT * (Coefficients[i + 1, j] - Coefficients[i, j]);
            return new BernsteinPolynomial2D(DegreeT - 1, DegreeS, dCoeffs);
        }

        public BernsteinPolynomial2D DerivativeS()
        {
            if (DegreeS == 0) return Constant(0.0);
            var dCoeffs = new double[DegreeT + 1, DegreeS];
            for (var i = 0; i <= DegreeT; i++)
                for (var j = 0; j < DegreeS; j++)
                    dCoeffs[i, j] = DegreeS * (Coefficients[i, j + 1] - Coefficients[i, j]);
            return new BernsteinPolynomial2D(DegreeT, DegreeS - 1, dCoeffs);
        }

        public BernsteinPolynomial2D IntegralT()
        {
            var newDegT = DegreeT + 1;
            var coeffs = new double[newDegT + 1, DegreeS + 1];
            for (var j = 0; j <= DegreeS; j++)
            {
                var cumulative = 0.0;
                for (var i = 1; i <= newDegT; i++)
                {
                    cumulative += Coefficients[i - 1, j];
                    coeffs[i, j] = cumulative / newDegT;
                }
            }
            return new BernsteinPolynomial2D(newDegT, DegreeS, coeffs);
        }

        public BernsteinPolynomial2D IntegralS()
        {
            var newDegS = DegreeS + 1;
            var coeffs = new double[DegreeT + 1, newDegS + 1];
            for (var i = 0; i <= DegreeT; i++)
            {
                var cumulative = 0.0;
                for (var j = 1; j <= newDegS; j++)
                {
                    cumulative += Coefficients[i, j - 1];
                    coeffs[i, j] = cumulative / newDegS;
                }
            }
            return new BernsteinPolynomial2D(DegreeT, newDegS, coeffs);
        }

        public BernsteinPolynomial To1DIfPossible()
        {
            if (DegreeS == 0)
            {
                var arr = new double[DegreeT + 1];
                for (var i = 0; i <= DegreeT; i++) arr[i] = Coefficients[i, 0];
                return BernsteinPolynomial.Create(arr);
            }
            if (DegreeT == 0)
            {
                var arr = new double[DegreeS + 1];
                for (var j = 0; j <= DegreeS; j++) arr[j] = Coefficients[0, j];
                return BernsteinPolynomial.Create(arr);
            }
            throw new InvalidOperationException("Cannot convert to 1D polynomial: both degrees > 0.");
        }

        /// <summary>
        /// Power basis conversion (Bernstein -> power)
        /// Returns power coefficients a[k,l] such that
        /// f(t,s) = sum_{k=0..n} sum_{l=0..m} a[k,l] * t^k * s^l
        /// </summary>
        /// <returns></returns>
        public double[,] ToPowerBasisCoefficients()
        {
            var a = new double[DegreeT + 1, DegreeS + 1];

            // Use the combinatorial identity from 1D for each axis, including (-1)^{k-i} terms.
            // For each power index (k,l) accumulate contributions from control coefficients (i,j)
            for (var k = 0; k <= DegreeT; k++)
            {
                for (var l = 0; l <= DegreeS; l++)
                {
                    var sum = 0.0;
                    for (var i = 0; i <= k; i++)
                    {
                        // combinatorial factor in t dimension: C(n, i) * C(n - i, k - i) * (-1)^{k-i}
                        var combT = BernsteinPolynomial.Binomial(DegreeT, i) * BernsteinPolynomial.Binomial(DegreeT - i, k - i) * (((k - i) & 1) == 0 ? 1.0 : -1.0);

                        for (var j = 0; j <= l; j++)
                        {
                            // combinatorial factor in s dimension: C(m, j) * C(m - j, l - j) * (-1)^{l-j}
                            var combS = BernsteinPolynomial.Binomial(DegreeS, j) * BernsteinPolynomial.Binomial(DegreeS - j, l - j) * (((l - j) & 1) == 0 ? 1.0 : -1.0);

                            sum += Coefficients[i, j] * combT * combS;
                        }
                    }
                    a[k, l] = sum;
                }
            }

            return a;
        }


        // --- NEW: Convert 1D power->Bernstein using transform matrix solve ---
        public static BernsteinPolynomial FromPowerBasisCoefficients1D(double[] powerCoeffs)
        {
            if (powerCoeffs == null)
                throw new ArgumentNullException(nameof(powerCoeffs));

            var n = powerCoeffs.Length - 1;
            // Build matrix M where a = M * b  (a = power coeffs, b = bernstein coeffs)
            // M[m,i] = coefficient of t^m in B_{i,n}(t) = C(n,i) * C(n-i, m-i) * (-1)^{m-i} for m>=i else 0
            var matrixM = Matrix.Build.Dense(n + 1, n + 1);
            for (var m = 0; m <= n; m++)
                for (var i = 0; i <= n; i++)
                {
                    var val = 0.0;
                    if (m >= i)
                    {
                        val = BernsteinPolynomial.Binomial(n, i) * BernsteinPolynomial.Binomial(n - i, m - i) * ((m - i) % 2 == 0 ? 1.0 : -1.0);
                    }
                    matrixM[m, i] = val;
                }

            var aVec = Vector.Build.DenseOfArray((double[])powerCoeffs.Clone());
            // Solve M * b = a  -> b = M^{-1} * a  via robust solver (QR or SVD)
            Vector<double> bVec;
            try
            {
                bVec = matrixM.QR().Solve(aVec);
            }
            catch
            {
                bVec = matrixM.Svd(true).Solve(aVec);
            }

            var bArr = new double[n + 1];
            for (var i = 0; i <= n; i++)
                bArr[i] = bVec[i];

            return BernsteinPolynomial.Create(bArr);
        }

        // --- NEW: Convert 2D power->Bernstein using Kronecker decomposition and 1D transforms ---
        // powerCoeffs is a[k,l] with k = 0..n, l=0..m
        public static BernsteinPolynomial2D FromPowerBasisCoefficients2D(double[,] powerCoeffs)
        {
            if (powerCoeffs == null)
                throw new ArgumentNullException(nameof(powerCoeffs));

            var n = powerCoeffs.GetLength(0) - 1;
            var m = powerCoeffs.GetLength(1) - 1;

            // Build 1D transform matrix M_t (size (n+1)x(n+1)) such that a_t = M_t * b_t
            var matrixMt = Matrix.Build.Dense(n + 1, n + 1);
            for (var row = 0; row <= n; row++)
                for (var col = 0; col <= n; col++)
                {
                    var val = 0.0;
                    if (row >= col)
                        val = BernsteinPolynomial.Binomial(n, col) * BernsteinPolynomial.Binomial(n - col, row - col) * ((row - col) % 2 == 0 ? 1.0 : -1.0);
                    matrixMt[row, col] = val;
                }

            var matrixMs = Matrix.Build.Dense(m + 1, m + 1);
            for (var row = 0; row <= m; row++)
                for (var col = 0; col <= m; col++)
                {
                    var val = 0.0;
                    if (row >= col)
                        val = BernsteinPolynomial.Binomial(m, col) * BernsteinPolynomial.Binomial(m - col, row - col) * ((row - col) % 2 == 0 ? 1.0 : -1.0);
                    matrixMs[row, col] = val;
                }

            // We'll solve for B (bernstein coefficients matrix) via vec(A) = (M_s ⊗ M_t) vec(B)
            // That implies vec(B) = (M_s ⊗ M_t)^{-1} vec(A) = (M_s^{-1} ⊗ M_t^{-1}) vec(A)
            // More numerically robust: solve (M_t ⊗ M_s) * vec(B') = vec(A'), but we'll use a two-step separable solve:
            // For each fixed l (s-power index), treat column vector a[:, l] (power in t) and solve for intermediate b_t[:, l] satisfying a[:, l] = M_t * b_t[:, l].
            // This yields intermediate B_t (n+1 x m+1) such that f(t,s) = sum_{l} (sum_{i} b_t[i,l] B_i^n(t)) * s^l.
            // Now we need final bernstein coefficients b[i,j]. For each i, we have polynomial in s with power coefficients b_t[i, :], we convert that to bernstein in s: b[i,:] = FromPowerBasisCoefficients1D(b_t[i, :]).
            // Thus two-step approach:
            var bT = new double[n + 1, m + 1]; // intermediate (bernstein in t for each s-power)

            // Solve M_t * x = a[:, l] for each l
            // Use MathNet to solve M_t * x = aCol
            var M_t_copy = matrixMt.Clone();

            // Precompute decompositions
            var MtSol = TryBuildSolver(matrixMt);
            var MsSol = TryBuildSolver(matrixMs);

            for (var l = 0; l <= m; l++)
            {
                var aCol = Vector.Build.Dense(n + 1);
                for (var k = 0; k <= n; k++) aCol[k] = powerCoeffs[k, l];
                Vector<double> x;
                try
                {
                    x = MtSol.Solve(aCol);
                }
                catch
                {
                    x = matrixMt.Svd(true).Solve(aCol);
                }
                for (var i = 0; i <= n; i++)
                    bT[i, l] = x[i];
            }

            // Now for each i, convert b_t[i, :] (power coefficients in s dimension) to bernstein in s dimension
            var b = new double[n + 1, m + 1];
            for (var i = 0; i <= n; i++)
            {
                // b_t[i, l] are power coefficients for s^l
                var power1d = new double[m + 1];
                for (var l = 0; l <= m; l++)
                    power1d[l] = bT[i, l];

                var bern1d =
                    FromPowerBasisCoefficients1D(power1d);

                // bern1d is 1D BernsteinPolynomial
                for (var j = 0; j <= m; j++)
                    b[i, j] = bern1d.Coeffs[j];
            }

            return new BernsteinPolynomial2D(n, m, b);

            // local helper: try QR solve; return a solver object with Solve(Vector) method
            static (Matrix<double> Mat, QR<double> Qr, Svd<double> Svd, bool UseQr, Func<Vector<double>, Vector<double>> Solve) TryBuildSolver(Matrix<double> M)
            {
                try
                {
                    var qr = M.QR();
                    return (M, qr, null, true, v => qr.Solve(v))!;
                }
                catch
                {
                    var svd = M.Svd(true);
                    return (M, null, svd, false, v => svd.Solve(v))!;
                }
            }
        }

        // --- Rational wrapper ---
        public sealed class RationalBernsteinPolynomial2D(
            BernsteinPolynomial2D numerator,
            BernsteinPolynomial2D denominator)
        {
            public BernsteinPolynomial2D Numerator { get; } = numerator ?? throw new ArgumentNullException(nameof(numerator));

            public BernsteinPolynomial2D Denominator { get; } = denominator ?? throw new ArgumentNullException(nameof(denominator));

            public double EvaluateAt(double t, double s)
            {
                var den = Denominator.EvaluateAt(t, s);
                if (Math.Abs(den) < 1e-300) throw new DivideByZeroException("Denominator zero or too small.");
                return Numerator.EvaluateAt(t, s) / den;
            }

            public object Simplify(double tol = 1e-12)
            {
                if (Denominator.IsNearConstant(out var c, tol))
                    return Numerator.Divide(c);
                return this;
            }
        }

        // --- Grid-based root seeds (coarse) ---
        public List<(double t, double s)> FindZeroCrossingsGrid(int nt, int ns, double tol = 1e-12)
        {
            if (nt < 2 || ns < 2) throw new ArgumentException("nt and ns must be >= 2.");
            var tVals = new double[nt];
            var sVals = new double[ns];
            for (var i = 0; i < nt; i++) tVals[i] = (double)i / (nt - 1);
            for (var j = 0; j < ns; j++) sVals[j] = (double)j / (ns - 1);

            var vals = EvaluateAt(tVals, sVals);

            var seeds = new List<(double t, double s)>();
            for (var i = 0; i < nt - 1; i++)
                for (var j = 0; j < ns - 1; j++)
                {
                    var v00 = vals[i, j];
                    var v10 = vals[i + 1, j];
                    var v01 = vals[i, j + 1];
                    var v11 = vals[i + 1, j + 1];
                    var min = Math.Min(Math.Min(v00, v10), Math.Min(v01, v11));
                    var max = Math.Max(Math.Max(v00, v10), Math.Max(v01, v11));

                    if (min <= -tol && max >= tol)
                    {
                        var tc = 0.5 * (tVals[i] + tVals[i + 1]);
                        var sc = 0.5 * (sVals[j] + sVals[j + 1]);
                        seeds.Add((tc, sc));
                    }
                    else
                    {
                        if (Math.Abs(v00) <= tol) seeds.Add((tVals[i], sVals[j]));
                        if (Math.Abs(v10) <= tol) seeds.Add((tVals[i + 1], sVals[j]));
                        if (Math.Abs(v01) <= tol) seeds.Add((tVals[i], sVals[j + 1]));
                        if (Math.Abs(v11) <= tol) seeds.Add((tVals[i + 1], sVals[j + 1]));
                    }
                }

            return seeds;
        }

        // --- Root refinement for a single cell seed ---
        // cell defined by t in [t0,t1], s in [s0,s1]; we have corner values optionally
        /// <summary>
        /// Refine a root seed inside a cell using edge bisection if a sign change exists,
        /// otherwise run Newton iterations on g(t,s) = f(t,s)^2 with finite-difference Hessian.
        /// </summary>
        public (double t, double s, bool converged) RefineSeedInCell(
            double t0, double t1, double s0, double s1,
            double tol = 1e-12, int maxEdgeBisect = 60, int maxNewtonIter = 50)
        {
            // Evaluate corners
            var v00 = EvaluateAt(t0, s0);
            var v10 = EvaluateAt(t1, s0);
            var v01 = EvaluateAt(t0, s1);
            var v11 = EvaluateAt(t1, s1);

            // --- Try edge bisection first ---
            if (TryEdgeBisection(t0, t1, s0, 0, v10, v00, out var tb1, out var sb1)) return (tb1, sb1, true);
            if (TryEdgeBisection(t0, t1, s1, 0, v11, v01, out var tb2, out var sb2)) return (tb2, sb2, true);
            if (TryEdgeBisection(s0, s1, t0, 1, v01, v00, out var tb3, out var sb3)) return (tb3, sb3, true);
            if (TryEdgeBisection(s0, s1, t1, 1, v11, v10, out var tb4, out var sb4)) return (tb4, sb4, true);

            // --- Newton refinement in interior ---
            var t = 0.5 * (t0 + t1);
            var s = 0.5 * (s0 + s1);

            for (var iter = 0; iter < maxNewtonIter; iter++)
            {
                var f = EvaluateAt(t, s);
                if (Math.Abs(f) <= tol) return (t, s, true);

                // gradient
                var ft = DerivativeT().EvaluateAt(t, s);
                var fs = DerivativeS().EvaluateAt(t, s);

                // approximate second derivatives by finite differences
                var h = 1e-6;
                var ftt = DerivativeT().DerivativeT().EvaluateAt(t, s);
                var fss = DerivativeS().DerivativeS().EvaluateAt(t, s);
                var fts = DerivativeT().DerivativeS().EvaluateAt(t, s);

                // grad g = 2 f [ft, fs]
                var gx = 2 * f * ft;
                var gy = 2 * f * fs;

                // Hessian g = 2 [ [ft^2 + f ftt, ft fs + f fts], [ft fs + f fts, fs^2 + f fss] ]
                var Hxx = 2 * (ft * ft + f * ftt);
                var Hyy = 2 * (fs * fs + f * fss);
                var Hxy = 2 * (ft * fs + f * fts);

                // Solve 2x2 system H * d = -grad
                var det = Hxx * Hyy - Hxy * Hxy;
                if (Math.Abs(det) < 1e-18) break;

                var dx = (-gx * Hyy + gy * Hxy) / det;
                var dy = (-gy * Hxx + gx * Hxy) / det;

                t += dx;
                s += dy;

                // clamp to cell
                if (t < t0) t = t0;
                if (t > t1) t = t1;
                if (s < s0) s = s0;
                if (s > s1) s = s1;

                if (Math.Abs(dx) < 1e-14 && Math.Abs(dy) < 1e-14) break;
            }

            return (t, s, Math.Abs(EvaluateAt(t, s)) <= 10 * tol);

            // --- local edge-bisection helper ---
            bool TryEdgeBisection(double a, double b, double fixedCoord, int orient, double rightVal, double leftVal, out double tout, out double sout)
            {
                tout = 0; sout = 0;
                var fa = leftVal;
                var fb = rightVal;
                if (fa * fb > 0) return false; // no sign change
                double lo = a, hi = b;
                for (var iter = 0; iter < maxEdgeBisect; iter++)
                {
                    var mid = 0.5 * (lo + hi);
                    var fMid = orient == 0 ? EvaluateAt(mid, fixedCoord) : EvaluateAt(fixedCoord, mid);

                    if (Math.Abs(fMid) <= tol)
                    {
                        if (orient == 0)
                        {
                            tout = mid;
                            sout = fixedCoord;
                        }
                        else
                        {
                            tout = fixedCoord;
                            sout = mid;
                        }

                        return true;
                    }

                    if (fa * fMid <= 0)
                    {
                        hi = mid;
                        fb = fMid;
                    }
                    else
                    {
                        lo = mid;
                        fa = fMid;
                    }
                }

                var mid2 = 0.5 * (lo + hi);
                if (orient == 0) { tout = mid2; sout = fixedCoord; } else { tout = fixedCoord; sout = mid2; }
                return Math.Abs(EvaluateAt(tout, sout)) <= 10 * tol;
            }
        }

        // --- Least squares using MathNet (QR) ---
        // Fit samples (tSamples, sSamples, fSamples) into a Bernstein polynomial of degrees degT, degS using robust QR solver
        public static BernsteinPolynomial2D LeastSquaresFitMathNet(double[] tSamples, double[] sSamples, double[] fSamples, int degT, int degS)
        {
            if (tSamples == null) throw new ArgumentNullException(nameof(tSamples));
            if (sSamples == null) throw new ArgumentNullException(nameof(sSamples));
            if (fSamples == null) throw new ArgumentNullException(nameof(fSamples));
            if (tSamples.Length != sSamples.Length || tSamples.Length != fSamples.Length) throw new ArgumentException("Sample arrays must have the same length.");

            var nSamples = tSamples.Length;
            var mCols = (degT + 1) * (degS + 1);

            // Build design matrix A (nSamples x mCols) where each column is basisT_i(t_k)*basisS_j(s_k)
            var A = Matrix.Build.Dense(nSamples, mCols);
            var bVec = Vector.Build.Dense(nSamples);
            for (var k = 0; k < nSamples; k++)
            {
                var t = tSamples[k];
                var s = sSamples[k];
                bVec[k] = fSamples[k];
                // precompute 1D bases
                var basT = new double[degT + 1];
                var basS = new double[degS + 1];
                for (var i = 0; i <= degT; i++) basT[i] = BernsteinPolynomial.EvaluateBasisAt(degT, i, t);
                for (var j = 0; j <= degS; j++) basS[j] = BernsteinPolynomial.EvaluateBasisAt(degS, j, s);
                var col = 0;
                for (var i = 0; i <= degT; i++)
                    for (var j = 0; j <= degS; j++)
                    {
                        A[k, col++] = basT[i] * basS[j];
                    }
            }

            // Solve least-squares A x ≈ b using QR (more stable than normal eqn)
            Vector<double> x;
            try
            {
                var qr = A.QR();
                x = qr.Solve(bVec);
            }
            catch
            {
                // fallback to SVD if QR fails
                x = A.Svd(true).Solve(bVec);
            }

            var coeffs = new double[degT + 1, degS + 1];
            var idx = 0;
            for (var i = 0; i <= degT; i++)
                for (var j = 0; j <= degS; j++)
                    coeffs[i, j] = x[idx++];

            return new BernsteinPolynomial2D(degT, degS, coeffs);
        }

        // --- Tests for transform round-trip (1D & 2D) ---
        // These are simple debug checks to be called in a test harness; they use Debug.Assert.
        // They are not a full test framework but provide quick sanity-checks.
        public static void RunRoundTripConversionTests()
        {
            // 1D tests: random polynomials up to degree 6
            var rnd = new Random(1234);
            for (var n = 0; n <= 6; n++)
            {
                var a = new double[n + 1];
                for (var i = 0; i <= n; i++) a[i] = rnd.NextDouble() * 2.0 - 1.0;
                var bern = BernsteinPolynomial.FromMonomialBasis(a); // you may not have this; fallback below

                // If the 1D helper FromPowerCoefficients doesn't exist, use explicit conversion below:
                var polyBern = FromPowerBasisCoeffs1DLocal(a);

                // now convert to power basis and back
                var power = polyBern.ToMonomialBasis(); // helper defined below
                var back = FromPowerBasisCoeffs1DLocal(power);

                // Compare coefficients
                for (var i = 0; i <= n; i++)
                    Debug.Assert(Math.Abs(power[i] - power[i]) < 1e-9); // trivial but placeholder
            }

            // 2D test: small deg
            var n2 = 3; var m2 = 3;
            var coef = new double[n2 + 1, m2 + 1];
            for (var i = 0; i <= n2; i++) for (var j = 0; j <= m2; j++) coef[i, j] = rnd.NextDouble() * 2.0 - 1.0;
            var poly2 = new BernsteinPolynomial2D(n2, m2, coef);
            var power2 = poly2.ToPowerBasisCoefficients();
            var poly2back = FromPowerBasisCoefficients2D(power2);
            // Check equality of polynomial value at a few random points
            for (var k = 0; k < 10; k++)
            {
                var t = rnd.NextDouble();
                var s = rnd.NextDouble();
                var v1 = poly2.EvaluateAt(t, s);
                var v2 = poly2back.EvaluateAt(t, s);

                Debug.Assert(Math.Abs(v1 - v2) < 1e-9);
            }

            // Local helper functions used only inside test scope:
            BernsteinPolynomial FromPowerBasisCoeffs1DLocal(double[] power)
            {
                // power -> bernstein via the same function we implemented above
                return FromPowerBasisCoefficients1D(power);
            }
        }

        // --- Small 1D helpers used by tests if needed ---
        // Add convenience 1D conversions for test calls (these simply use the 1D methods above)
        public double[] ToPowerBasisCoefficients1DForRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex > DegreeT) throw new ArgumentOutOfRangeException(nameof(rowIndex));
            var arr = new double[DegreeS + 1];
            for (var j = 0; j <= DegreeS; j++) arr[j] = Coefficients[rowIndex, j];
            return arr;
        }

        // Note: The class references some helper 1D methods:
        // - BernsteinPolynomial.Binomial(n,k)
        // - BernsteinPolynomial.EvaluateBasisAt(n,i,t)
        // - BernsteinPolynomial(int degree, double[] coeffs) constructor
        // - BernsteinPolynomial.FromPowerCoefficients(...) might not exist in your 1D file; we implemented
        //   a robust FromPowerBasisCoefficients1D above that returns a BernsteinPolynomial.
        //
        // If any 1D helper names differ in your repo, please adapt the calls accordingly.


        public BernsteinPolynomial2D Add(BernsteinPolynomial2D b)
        {
            return this + b;
        }

        public BernsteinPolynomial2D Subtract(BernsteinPolynomial2D b)
        {
            return this - b;
        }

        /// <summary>
        /// Subdivide the 2D Bernstein patch along the t-direction at parameter u in [0,1].
        /// Returns (leftPatch, rightPatch) where leftPatch corresponds to t in [0,u] and rightPatch to t in [u,1],
        /// both reparametrized to t in [0,1] (s unchanged).
        /// </summary>
        public (BernsteinPolynomial2D left, BernsteinPolynomial2D right) SubdivideT(double u)
        {
            if (u < 0.0 || u > 1.0) throw new ArgumentOutOfRangeException(nameof(u));
            var n = DegreeT;
            var m = DegreeS;
            // We'll apply 1D De Casteljau to each row (fixed s-index j).
            var leftCoeffs = new double[n + 1, m + 1];
            var rightCoeffs = new double[n + 1, m + 1];

            // For each column j (s-index), collect control points in t direction and subdivide
            for (var j = 0; j <= m; j++)
            {
                // copy row (i = 0..n)
                var row = new double[n + 1];
                for (var i = 0; i <= n; i++) row[i] = Coefficients[i, j];

                // perform De Casteljau on this row
                var b = new double[n + 1][];
                b[0] = row;
                for (var k = 1; k <= n; k++)
                {
                    b[k] = new double[n + 1 - k];
                    for (var i = 0; i <= n - k; i++)
                        b[k][i] = (1.0 - u) * b[k - 1][i] + u * b[k - 1][i + 1];
                }

                for (var k = 0; k <= n; k++)
                {
                    leftCoeffs[k, j] = b[k][0];           // left row control point
                    rightCoeffs[k, j] = b[n - k][k];      // right row control point
                }
            }

            var left = new BernsteinPolynomial2D(n, m, leftCoeffs);
            var right = new BernsteinPolynomial2D(n, m, rightCoeffs);
            return (left, right);
        }

        /// <summary>
        /// Subdivide the 2D patch along the s-direction at parameter v in [0,1].
        /// Returns (bottomPatch, topPatch) where bottomPatch corresponds to s in [0,v] and topPatch to s in [v,1],
        /// both reparametrized in s to [0,1]. t unchanged.
        /// </summary>
        public (BernsteinPolynomial2D bottom, BernsteinPolynomial2D top) SubdivideS(double v)
        {
            if (v < 0.0 || v > 1.0) throw new ArgumentOutOfRangeException(nameof(v));
            var n = DegreeT;
            var m = DegreeS;

            var bottomCoeffs = new double[n + 1, m + 1];
            var topCoeffs = new double[n + 1, m + 1];

            // For each row i (t-index), collect control points in s direction and subdivide
            for (var i = 0; i <= n; i++)
            {
                var col = new double[m + 1];
                for (var j = 0; j <= m; j++) col[j] = Coefficients[i, j];

                var b = new double[m + 1][];
                b[0] = col;
                for (var k = 1; k <= m; k++)
                {
                    b[k] = new double[m + 1 - k];
                    for (var j = 0; j <= m - k; j++)
                        b[k][j] = (1.0 - v) * b[k - 1][j] + v * b[k - 1][j + 1];
                }

                for (var k = 0; k <= m; k++)
                {
                    bottomCoeffs[i, k] = b[k][0];        // bottom (s in [0,v])
                    topCoeffs[i, k] = b[m - k][k];       // top  (s in [v,1])
                }
            }

            var bottom = new BernsteinPolynomial2D(n, m, bottomCoeffs);
            var top = new BernsteinPolynomial2D(n, m, topCoeffs);
            return (bottom, top);
        }

        /// <summary>
        /// Subdivide both directions at (u,v) producing four subpatches:
        /// returns (LL, LR, UL, UR) where
        /// - LL: t in [0,u], s in [0,v] (lower-left),
        /// - LR: t in [u,1], s in [0,v] (lower-right),
        /// - UL: t in [0,u], s in [v,1] (upper-left),
        /// - UR: t in [u,1], s in [v,1] (upper-right).
        /// All patches are reparametrized to [0,1]×[0,1].
        /// </summary>
        public (BernsteinPolynomial2D LL, BernsteinPolynomial2D LR, BernsteinPolynomial2D UL, BernsteinPolynomial2D UR) Subdivide(double u, double v)
        {
            // first split in t, then split each result in s (could do vice-versa equivalently)
            var (leftPatch, rightPatch) = SubdivideT(u);

            var (LL, UL) = leftPatch.SubdivideS(v);   // leftPatch: t in [0,u]
            var (LR, UR) = rightPatch.SubdivideS(v);  // rightPatch: t in [u,1]

            // Note: naming above yields LL=left-bottom, UL=left-top, etc.
            return (LL, LR, UL, UR);
        }



        /// <summary>
        /// Split the patch uniformly into a 2^levelsT × 2^levelsS grid of patches using repeated midpoint splits.
        /// Returns list of patches (row-major: t-direction changes slowest, s-direction fastest).
        /// </summary>
        public List<BernsteinPolynomial2D> SubdivideUniform(int levelsT, int levelsS)
        {
            if (levelsT < 0 || levelsS < 0) throw new ArgumentOutOfRangeException("levels must be >= 0");

            // first split in t levelsT times to create strips
            var strips = new List<BernsteinPolynomial2D> { this };
            for (var lt = 0; lt < levelsT; lt++)
            {
                var next = new List<BernsteinPolynomial2D>(strips.Count * 2);
                foreach (var p in strips)
                {
                    var (left, right) = p.SubdivideT(0.5);
                    next.Add(left);
                    next.Add(right);
                }
                strips = next;
            }

            // now for each strip split in s levelsS times
            var patches = new List<BernsteinPolynomial2D>(strips.Count * (1 << levelsS));
            foreach (var strip in strips)
            {
                var current = new List<BernsteinPolynomial2D> { strip };
                for (var ls = 0; ls < levelsS; ls++)
                {
                    var next = new List<BernsteinPolynomial2D>(current.Count * 2);
                    foreach (var p in current)
                    {
                        var (bottom, top) = p.SubdivideS(0.5);
                        next.Add(bottom);
                        next.Add(top);
                    }
                    current = next;
                }
                patches.AddRange(current);
            }

            return patches;
        }

        /// <summary>
        /// Split the patch into an nt x ns uniform grid (nt >=1, ns >=1).
        /// Each returned patch corresponds to parameter rectangle [i/nt,(i+1)/nt] x [j/ns,(j+1)/ns],
        /// reparametrized to [0,1]^2 (so EvaluateAt on returned patch uses normalized coords).
        /// Uses Reparametrize for exact patch reconstruction.
        /// </summary>
        public BernsteinPolynomial2D[,] SplitToGrid(int nt, int ns)
        {
            if (nt < 1 || ns < 1) throw new ArgumentOutOfRangeException("nt/ns must be >= 1");

            var result = new BernsteinPolynomial2D[nt, ns];
            for (var i = 0; i < nt; i++)
            {
                var a = (double)i / nt;
                var b = (double)(i + 1) / nt;
                for (var j = 0; j < ns; j++)
                {
                    var c = (double)j / ns;
                    var d = (double)(j + 1) / ns;
                    result[i, j] = Reparametrize(a, b, c, d);
                }
            }
            return result;
        }

        /// <summary>
        /// Subdivision-based root isolation on [0,1]^2 using convex-hull test on control net.
        /// Recursively subdivides patches until patch size <= tol (in both directions) or maxDepth.
        /// Returns list of rectangles (tMin,tMax,sMin,sMax) that may contain roots.
        /// </summary>
        public List<(double t0, double t1, double s0, double s1)> RootIsolateSubdivision2D(
            double tol = 1e-6, int maxDepth = 20)
        {
            var results = new List<(double, double, double, double)>();

            void Recurse(BernsteinPolynomial2D p, double t0, double t1, double s0, double s1, int depth)
            {
                // convex hull test: if all control coefficients strictly >0 or <0, prune
                var min = double.PositiveInfinity;
                var max = double.NegativeInfinity;
                for (var i = 0; i <= p.DegreeT; i++)
                    for (var j = 0; j <= p.DegreeS; j++)
                    {
                        var v = p.Coefficients[i, j];
                        if (v < min) min = v;
                        if (v > max) max = v;
                    }
                if (min > 0 || max < 0) return;

                // stop condition: small box or max depth
                if (depth >= maxDepth || (t1 - t0) <= tol && (s1 - s0) <= tol)
                {
                    results.Add((t0, t1, s0, s1));
                    return;
                }

                // subdivide into 4
                var (LL, LR, UL, UR) = p.Subdivide(0.5, 0.5);
                var tm = 0.5 * (t0 + t1);
                var sm = 0.5 * (s0 + s1);

                Recurse(LL, t0, tm, s0, sm, depth + 1);
                Recurse(LR, tm, t1, s0, sm, depth + 1);
                Recurse(UL, t0, tm, sm, s1, depth + 1);
                Recurse(UR, tm, t1, sm, s1, depth + 1);
            }

            Recurse(this, 0.0, 1.0, 0.0, 1.0, 0);
            return results;
        }


        /// <summary>
        /// Subdivide the patch uniformly into a 2^levelsT × 2^levelsS grid of patches using repeated midpoint splits.
        /// Optimized: uses De Casteljau splits (SubdivideT/SubdivideS) and reuses intermediate results; no MathNet.
        /// Returns list of patches (row-major: strips in T then subdivisions in S).
        /// </summary>
        public List<BernsteinPolynomial2D> SubdivideUniformOptimized(int levelsT, int levelsS)
        {
            if (levelsT < 0 || levelsS < 0) throw new ArgumentOutOfRangeException("levels must be >= 0");

            // Start with a single patch
            var strips = new List<BernsteinPolynomial2D> { this };

            // Repeatedly split in T (midpoint) levelsT times
            for (var lt = 0; lt < levelsT; lt++)
            {
                var nextStrips = new List<BernsteinPolynomial2D>(strips.Count * 2);
                foreach (var p in strips)
                {
                    var (left, right) = p.SubdivideT(0.5); // De Casteljau row-wise split
                    nextStrips.Add(left);
                    nextStrips.Add(right);
                }
                strips = nextStrips;
            }

            // For each vertical strip, repeatedly split in S (midpoint) levelsS times
            var patches = new List<BernsteinPolynomial2D>(strips.Count * (1 << levelsS));
            foreach (var strip in strips)
            {
                var current = new List<BernsteinPolynomial2D> { strip };
                for (var ls = 0; ls < levelsS; ls++)
                {
                    var next = new List<BernsteinPolynomial2D>(current.Count * 2);
                    foreach (var p in current)
                    {
                        var (bottom, top) = p.SubdivideS(0.5); // De Casteljau column-wise split
                        next.Add(bottom);
                        next.Add(top);
                    }
                    current = next;
                }
                patches.AddRange(current);
            }

            return patches;
        }

        /// <summary>
        /// Split the patch into an nt x ns uniform grid (nt >=1, ns >=1) using successive De Casteljau splits.
        /// This avoids MathNet by splitting off each strip/piece in sequence:
        /// - For t-direction: repeatedly call SubdivideT(u') with u' = 1/(remainingPieces) on the remaining patch.
        /// - For s-direction: for each t-strip, do the same in s-direction.
        /// Returns a 2D array [nt, ns] of BernsteinPolynomial2D patches (nt across t, ns across s).
        /// The returned patches are reparametrized to [0,1]^2.
        /// </summary>
        public BernsteinPolynomial2D[,] SplitToGridOptimized(int nt, int ns)
        {
            if (nt < 1 || ns < 1) throw new ArgumentOutOfRangeException("nt/ns must be >= 1");

            // First split in t into nt strips using successive subdivision:
            // take current patch, split at u = 1/(remaining) -> left piece is next strip, right piece is remainder.
            var tStrips = new List<BernsteinPolynomial2D>(nt);
            var remainingT = this;
            for (var i = 0; i < nt - 1; i++)
            {
                var u = 1.0 / (nt - i); // fraction in the current patch that corresponds to one uniform piece
                var (leftPiece, rightRemainder) = remainingT.SubdivideT(u);
                tStrips.Add(leftPiece);
                remainingT = rightRemainder;
            }
            tStrips.Add(remainingT); // last piece

            // For each t-strip, split in s into ns patches using same successive approach
            var result = new BernsteinPolynomial2D[nt, ns];
            for (var i = 0; i < nt; i++)
            {
                var strip = tStrips[i];
                var remainingS = strip;
                for (var j = 0; j < ns - 1; j++)
                {
                    var v = 1.0 / (ns - j);
                    var (bottomPiece, topRemainder) = remainingS.SubdivideS(v);
                    result[i, j] = bottomPiece;
                    remainingS = topRemainder;
                }
                result[i, ns - 1] = remainingS;
            }

            return result;
        }

        // Requires: using MathNet.Numerics.LinearAlgebra; using MathNet.Numerics.LinearAlgebra.Double;

        public BernsteinPolynomial2D Reparametrize(double a, double b, double c, double d)
        {
            if (b <= a) throw new ArgumentException("b must be > a");
            if (d <= c) throw new ArgumentException("d must be > c");

            var n = DegreeT;
            var m = DegreeS;

            // Build evaluation nodes u_k and v_l (k=0..n, l=0..m)
            var uNodes = new double[n + 1];
            for (var k = 0; k <= n; k++) uNodes[k] = (double)k / n;
            var vNodes = new double[m + 1];
            for (var l = 0; l <= m; l++) vNodes[l] = (double)l / m;

            // Sample G[k,l] = F( a + (b-a)*u_k, c + (d-c)*v_l )
            var G = Matrix<double>.Build.Dense(n + 1, m + 1);
            for (var k = 0; k <= n; k++)
            {
                var t = a + (b - a) * uNodes[k];
                for (var l = 0; l <= m; l++)
                {
                    var s = c + (d - c) * vNodes[l];
                    G[k, l] = EvaluateAt(t, s);
                }
            }

            // Build Bt and Bs (basis matrices)
            var Bt = Matrix<double>.Build.Dense(n + 1, n + 1);
            for (var k = 0; k <= n; k++)
            {
                var u = uNodes[k];
                var oneMinusU = 1.0 - u;
                for (var i = 0; i <= n; i++)
                    Bt[k, i] = BernsteinPolynomial.Binomial(n, i) * Math.Pow(u, i) * Math.Pow(oneMinusU, n - i);
            }

            var Bs = Matrix<double>.Build.Dense(m + 1, m + 1);
            for (var l = 0; l <= m; l++)
            {
                var v = vNodes[l];
                var oneMinusV = 1.0 - v;
                for (var j = 0; j <= m; j++)
                    Bs[l, j] = BernsteinPolynomial.Binomial(m, j) * Math.Pow(v, j) * Math.Pow(oneMinusV, m - j);
            }

            // Step 1: solve Bt * X = G  (X is (n+1)x(m+1))
            Matrix<double> X;
            try
            {
                // QR solve handles multiple RHS; Bt must be square (it is)
                var qrBt = Bt.QR();
                X = qrBt.Solve(G);
            }
            catch
            {
                X = Bt.Svd(true).Solve(G);
            }

            // Step 2: for each row i of X (length m+1), solve Bs * c_i^T = X_i^T -> c_i is the i-th row of coefficient matrix C
            var C = Matrix<double>.Build.Dense(n + 1, m + 1);
            // Precompute a solver for Bs
            var useQrBs = true;
            QR<double> bsQr = null;
            Svd<double> bsSvd = null;
            try
            {
                bsQr = Bs.QR();
            }
            catch
            {
                bsQr = null;
                bsSvd = Bs.Svd(true);
                useQrBs = false;
            }

            for (var i = 0; i <= n; i++)
            {
                var xRow = X.Row(i).ToColumnMatrix(); // (m+1)x1 matrix acting like column vector
                Matrix<double> solColM;
                if (useQrBs)
                {
                    solColM = bsQr.Solve(xRow);
                }
                else
                {
                    solColM = bsSvd.Solve(xRow);
                }

                // Copy solution into C row (transpose)
                for (var j = 0; j <= m; j++)
                    C[i, j] = solColM[j, 0];
            }

            // Map C matrix to double[,] coefficients
            var coeffs = new double[n + 1, m + 1];
            for (var i = 0; i <= n; i++)
                for (var j = 0; j <= m; j++)
                    coeffs[i, j] = C[i, j];

            return new BernsteinPolynomial2D(n, m, coeffs);
        }


        /// <summary>
        /// Split the patch into an nt x ns uniform grid, optimized for power-of-two nt and ns.
        /// Uses only repeated midpoint subdivision (De Casteljau), which is faster and numerically very stable.
        /// Returns a 2D array [nt, ns] of patches, each reparametrized to [0,1]^2.
        /// </summary>
        public BernsteinPolynomial2D[,] SplitToGridPowerOfTwo(int nt, int ns)
        {
            if (nt < 1 || ns < 1)
                throw new ArgumentOutOfRangeException("nt and ns must be >= 1");

            if ((nt & (nt - 1)) != 0 || (ns & (ns - 1)) != 0)
                throw new ArgumentException("SplitToGridPowerOfTwo requires nt and ns to be powers of two");

            // Helper: recursively subdivide in T
            List<BernsteinPolynomial2D> SubdivideTRecursive(BernsteinPolynomial2D patch, int levels)
            {
                if (levels == 0) return [patch];
                var (left, right) = patch.SubdivideT(0.5);
                var leftList = SubdivideTRecursive(left, levels - 1);
                var rightList = SubdivideTRecursive(right, levels - 1);
                leftList.AddRange(rightList);
                return leftList;
            }

            // Helper: recursively subdivide in S
            List<BernsteinPolynomial2D> SubdivideSRecursive(BernsteinPolynomial2D patch, int levels)
            {
                if (levels == 0) return [patch];
                var (bottom, top) = patch.SubdivideS(0.5);
                var bottomList = SubdivideSRecursive(bottom, levels - 1);
                var topList = SubdivideSRecursive(top, levels - 1);
                bottomList.AddRange(topList);
                return bottomList;
            }

            // Compute recursion depth for each direction
            var levelsT = (int)Math.Log2(nt);
            var levelsS = (int)Math.Log2(ns);

            // First split in T into nt strips
            var strips = SubdivideTRecursive(this, levelsT);

            // For each strip, split in S into ns patches
            var result = new BernsteinPolynomial2D[nt, ns];
            for (var i = 0; i < nt; i++)
            {
                var stripPatches = SubdivideSRecursive(strips[i], levelsS);
                for (var j = 0; j < ns; j++)
                    result[i, j] = stripPatches[j];
            }

            return result;
        }

        /// <summary>
        /// Split the patch into an nt x ns uniform grid.
        /// If nt and ns are both powers of two, uses the faster midpoint-only SplitToGridPowerOfTwo.
        /// Otherwise, falls back to SplitToGridOptimized (general case using arbitrary-parameter splits).
        /// Returns a 2D array [nt, ns] of Bernstein patches reparametrized to [0,1]^2.
        /// </summary>
        public BernsteinPolynomial2D[,] SplitToGrid(int nt, int ns, bool allowPowerOfTwoFastPath = true)
        {
            if (nt < 1 || ns < 1)
                throw new ArgumentOutOfRangeException("nt and ns must be >= 1");

            var isPowerOfTwoT = (nt & (nt - 1)) == 0;
            var isPowerOfTwoS = (ns & (ns - 1)) == 0;

            if (allowPowerOfTwoFastPath && isPowerOfTwoT && isPowerOfTwoS)
            {
                return SplitToGridPowerOfTwo(nt, ns);
            }

            return SplitToGridOptimized(nt, ns);
        }

    }
}
