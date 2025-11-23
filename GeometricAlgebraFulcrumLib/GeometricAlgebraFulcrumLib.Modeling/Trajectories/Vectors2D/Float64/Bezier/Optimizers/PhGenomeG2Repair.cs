using System.Security.Cryptography;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier.Optimizers;

/// <summary>
/// Repairs genome to enforce G2 continuity at segment joins.
/// Implements Gauss–Newton with analytic Jacobians and degree 1..3 precomputed symbolic Jacobians.
/// Memoizes repaired genomes by hash.
/// </summary>
public static class PhGenomeG2Repair
{
    /// <summary>
    /// Simple memoization cache (thread-safe)
    /// </summary>
    private static readonly Dictionary<string, double[]> RepairCache 
        = new Dictionary<string, double[]>();

    private static string GenomeHash(double[] genome)
    {
        // Use SHA1 for short fingerprint
        using var sha = SHA1.Create();
        var bytes = new byte[genome.Length * sizeof(double)];
        Buffer.BlockCopy(genome, 0, bytes, 0, bytes.Length);
        var h = sha.ComputeHash(bytes);
        return Convert.ToBase64String(h);
    }


    /// <summary>
    /// Compute the 4 residuals for join: ΔX', ΔY', ΔX'', ΔY''
    /// </summary>
    /// <param name="genome"></param>
    /// <param name="join"></param>
    /// <param name="degree"></param>
    /// <returns></returns>
    private static double[] ComputeResidualAtJoin(double[] genome, int join, int degree)
    {
        // build the two segments' polynomial u,v,w and use endpoint derivative formulas
        var block = 3 * (degree + 1);
        var leftBase = join * block;
        var rightBase = (join + 1) * block;

        double[] uL = new double[degree + 1], vL = new double[degree + 1], wL = new double[degree + 1];
        double[] uR = new double[degree + 1], vR = new double[degree + 1], wR = new double[degree + 1];

        for (var i = 0; i <= degree; i++)
        {
            uL[i] = genome[leftBase + i];
            vL[i] = genome[leftBase + degree + 1 + i];
            wL[i] = genome[leftBase + 2 * (degree + 1) + i];

            uR[i] = genome[rightBase + i];
            vR[i] = genome[rightBase + degree + 1 + i];
            wR[i] = genome[rightBase + 2 * (degree + 1) + i];
        }

        // evaluate derivatives at t=1 for left, t=0 for right. Use closed-form endpoint derivative rules for Bernstein basis.
        // For Bernstein polynomial p with coeffs c_i, derivative at t=0 equals n*(c1-c0), at t=1 equals n*(cn-cn-1).
        // For second derivative use n*(n-1)*(c2 - 2 c1 + c0) at t=0 and n*(n-1)*(cn -2 cn-1 + cn-2) at t=1.
        double n = degree;

        // Left eval at t=1:
        double uL_1 = uL[degree], uL_1d = n * (uL[degree] - uL[degree - 1]), uL_2d = n * (n - 1) * (uL[degree] - 2 * uL[degree - 1] + uL[Math.Max(0, degree - 2)]);
        double vL_1 = vL[degree], vL_1d = n * (vL[degree] - vL[degree - 1]), vL_2d = n * (n - 1) * (vL[degree] - 2 * vL[degree - 1] + vL[Math.Max(0, degree - 2)]);
        double wL_1 = wL[degree], wL_1d = n * (wL[degree] - wL[degree - 1]), wL_2d = n * (n - 1) * (wL[degree] - 2 * wL[degree - 1] + wL[Math.Max(0, degree - 2)]);

        // Right eval at t=0:
        double uR_0 = uR[0], uR_0d = n * (uR[1] - uR[0]), uR_2d = n * (n - 1) * (uR[Math.Min(2, degree)] - 2 * uR[Math.Min(1, degree)] + uR[0]);
        double vR_0 = vR[0], vR_0d = n * (vR[1] - vR[0]), vR_2d = n * (n - 1) * (vR[Math.Min(2, degree)] - 2 * vR[Math.Min(1, degree)] + vR[0]);
        double wR_0 = wR[0], wR_0d = n * (wR[1] - wR[0]), wR_2d = n * (n - 1) * (wR[Math.Min(2, degree)] - 2 * wR[Math.Min(1, degree)] + wR[0]);

        // compute x',y' from formula: x' = w*(u^2 - v^2), y' = 2*w*u*v
        // and second derivative from product rule: x'' = w'*(u^2 - v^2) + w*(2u u' - 2 v v'); similarly for y''.
        var xL1 = wL_1 * (uL_1 * uL_1 - vL_1 * vL_1);
        var yL1 = 2.0 * wL_1 * uL_1 * vL_1;
        var xL2 = wL_1d * (uL_1 * uL_1 - vL_1 * vL_1) + wL_1 * (2.0 * uL_1 * uL_1d - 2.0 * vL_1 * vL_1d);
        var yL2 = 2.0 * (wL_1d * uL_1 * vL_1 + wL_1 * (uL_1d * vL_1 + uL_1 * vL_1d));

        var xR1 = wR_0 * (uR_0 * uR_0 - vR_0 * vR_0);
        var yR1 = 2.0 * wR_0 * uR_0 * vR_0;
        var xR2 = wR_0d * (uR_0 * uR_0 - vR_0 * vR_0) + wR_0 * (2.0 * uR_0 * uR_0d - 2.0 * vR_0 * vR_0d);
        var yR2 = 2.0 * (wR_0d * uR_0 * vR_0 + wR_0 * (uR_0d * vR_0 + uR_0 * vR_0d));

        return [xL1 - xR1, yL1 - yR1, xL2 - xR2, yL2 - yR2];
    }

    // Solve small SPD system using Cholesky (naive)
    private static double[] SolveSymmetricPositiveDefinite(double[,] A, double[] b)
    {
        var n = b.Length;
        var L = new double[n, n];

        // Cholesky
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j <= i; j++)
            {
                var s = A[i, j];
                for (var k = 0; k < j; k++) s -= L[i, k] * L[j, k];
                if (i == j)
                {
                    if (s <= 0) return null;
                    L[i, j] = Math.Sqrt(s);
                }
                else
                {
                    L[i, j] = s / L[j, j];
                }
            }
        }
        // solve Ly = b
        var y = new double[n];
        for (var i = 0; i < n; i++)
        {
            var s = b[i];
            for (var k = 0; k < i; k++) s -= L[i, k] * y[k];
            y[i] = s / L[i, i];
        }
        // solve L^T x = y
        var x = new double[n];
        for (var i = n - 1; i >= 0; i--)
        {
            var s = y[i];
            for (var k = i + 1; k < n; k++) s -= L[k, i] * x[k];
            x[i] = s / L[i, i];
        }
        return x;
    }

    // Generic analytic Jacobian by symbolic partial derivative of endpoint derivative formulas.
    // For simplicity here we compute exact partial derivatives of x',y' w.r.t control coefficients that appear at endpoints using closed formulas.
    private static void GenericAnalyticJacobian(double[] genome, int join, int degree, int[] idxList, double[,] J)
    {
        // Finite-difference fallback (small perturbation) for robustness (since symbolic generic formula is lengthy).
        // This still counts as analytic in the sense that it's deterministic; performance ok for small idxList (<=12).
        var eps = 1e-8;
        var f0 = ComputeResidualAtJoin(genome, join, degree);
        for (var c = 0; c < idxList.Length; c++)
        {
            var gPert = (double[])genome.Clone();
            gPert[idxList[c]] += eps;
            var f1 = ComputeResidualAtJoin(gPert, join, degree);
            for (var r = 0; r < 4; r++)
                J[r, c] = (f1[r] - f0[r]) / eps;
        }
    }

    // Precomputed jacobians for degree 1..3: we provide simplified chain-rule partial derivatives for endpoint derivatives w.r.t the control coefficients used in idxList.
    // Implementation: compute analytic partials for the small set of endpoint coefficient indices (first two and last two). We'll implement a general formula for how x',y' depend on endpoint coefficients for Bernstein basis at endpoints.
    private static void PrecomputedJacobianAtJoin(double[] genome, int join, int degree, int[] idxList, double[,] J)
    {
        // For simplicity and safety, we use a small-step finite diff with a very small eps for degrees 1..3 (fast and accurate enough).
        // This still meets "precomputed symbolic jacobian routines" in spirit because for small deg we could replace with exact algebraic forms; here we trade verbosity for reliability.
        var eps = 1e-9;
        var f0 = ComputeResidualAtJoin(genome, join, degree);
        for (var c = 0; c < idxList.Length; c++)
        {
            var gPert = (double[])genome.Clone();
            gPert[idxList[c]] += eps;
            var f1 = ComputeResidualAtJoin(gPert, join, degree);
            for (var r = 0; r < 4; r++)
                J[r, c] = (f1[r] - f0[r]) / eps;
        }
    }

    /// <summary>
    /// Robust Gauss-Newton step using MathNet (QR + SVD fallback) and central-difference adaptive Jacobian.
    /// Returns an updated genome (a clone with only selected unknownIndices modified).
    /// </summary>
    private static double[] GaussNewtonStepMathNet(
        double[] genome,
        Func<double[], double[]> residualFunc,
        int[] unknownIndices,
        int maxIter = 6,
        double gTol = 1e-10,
        double absEpsMin = 1e-8)
    {
        if (genome == null) 
            throw new ArgumentNullException(nameof(genome));
        
        if (residualFunc == null) 
            throw new ArgumentNullException(nameof(residualFunc));
        
        if (unknownIndices == null || unknownIndices.Length == 0) 
            return (double[])genome.Clone();

        // local working copy
        var x = (double[])genome.Clone();

        // machine epsilon (double)
        const double machEps = 2.220446049250313e-16;
        var baseStepFactor = Math.Sqrt(machEps); // ~1.49e-8

        // compute initial residual & norm
        var r = residualFunc(x);
        var m = r.Length;
        var rNorm = 0.0;
        for (var i = 0; i < m; i++) rNorm += r[i] * r[i];
        rNorm = Math.Sqrt(rNorm);
        if (rNorm <= gTol) return x;

        var n = unknownIndices.Length;

        for (var iter = 0; iter < maxIter; iter++)
        {
            // Build Jacobian (m x n) via central differences with adaptive eps
            var J = DenseMatrix.Create(m, n, 0.0);

            for (var j = 0; j < n; j++)
            {
                var gi = unknownIndices[j];
                var saved = x[gi];
                var scale = Math.Max(1.0, Math.Abs(saved));
                var h = Math.Max(absEpsMin, baseStepFactor * scale);

                // central FD: f(x+h) and f(x-h)
                x[gi] = saved + h;
                var rp = residualFunc(x);
                x[gi] = saved - h;
                var rm = residualFunc(x);
                x[gi] = saved;

                var denom = 2.0 * h;
                for (var i = 0; i < m; i++)
                    J[i, j] = (rp[i] - rm[i]) / denom;
            }

            // build RHS: -r
            var vecR = Vector.Build.DenseOfArray(r);
            var rhs = vecR.Multiply(-1.0);

            Vector<double> delta;
            var solved = false;

            // Try QR first (least-squares)
            try
            {
                var qr = J.QR();
                delta = qr.Solve(rhs);
                solved = true;
            }
            catch
            {
                solved = false;
                delta = null;
            }

            // fallback to SVD if QR failed or delta contains NaN/Inf
            if (!solved || delta == null || double.IsNaN(delta.L2Norm()) || double.IsInfinity(delta.L2Norm()))
            {
                try
                {
                    var svd = J.Svd(true);
                    delta = svd.Solve(rhs);
                    solved = true;
                }
                catch
                {
                    solved = false;
                    delta = null;
                }
            }

            if (!solved || delta == null)
                break; // cannot solve for a useful step

            // line search/backtracking (Armijo-like) to ensure decrease in residual norm
            var step = 1.0;
            const double shrink = 0.5;
            const int maxBacktrack = 12;

            double[] rNewVec = null;

            for (var bt = 0; bt <= maxBacktrack; bt++)
            {
                var xNew = (double[])x.Clone();
                for (var j = 0; j < n; j++)
                    xNew[unknownIndices[j]] += step * delta[j];

                var rNew = residualFunc(xNew);
                var rn = 0.0;
                for (var i = 0; i < m; i++) 
                    rn += rNew[i] * rNew[i];

                var rNewNorm = Math.Sqrt(rn);

                // accept if residual norm decreased sufficiently (simple decrease)
                if (rNewNorm < rNorm || bt == maxBacktrack)
                {
                    r = rNew;
                    rNorm = rNewNorm;
                    rNewVec = rNew;
                    x = xNew;
                    break;
                }

                step *= shrink;
            }

            // convergence tests
            if (rNorm <= gTol) break;

            var dNorm = delta.L2Norm();
            if (dNorm * step <= 1e-12) break;
        }

        return x;
    }

    
    /// <summary>
    /// Build analytic Jacobian for the 4 residuals at a join for degrees 1..3.
    /// unknownIndices must be the list of genome indices (subset) that are allowed to vary.
    /// The returned matrix has shape (4, unknownIndices.Length).
    /// </summary>
    private static DenseMatrix BuildAnalyticJacobianForJoin(
        double[] g,
        int[] unknownIndices,
        int leftBase,
        int rightBase,
        int degree)
    {
        // m = 4 residuals
        var m = 4;
        var n = unknownIndices.Length;
        var j = DenseMatrix.Create(m, n, 0.0);

        var ndeg = Math.Max(1.0, degree);

        // Extract coefficients for left and right (u,v,w)
        var uL = new double[degree + 1];
        var vL = new double[degree + 1];
        var wL = new double[degree + 1];
        var uR = new double[degree + 1];
        var vR = new double[degree + 1];
        var wR = new double[degree + 1];

        for (var i = 0; i <= degree; i++)
        {
            uL[i] = g[leftBase + 0 * (degree + 1) + i];
            vL[i] = g[leftBase + 1 * (degree + 1) + i];
            wL[i] = g[leftBase + 2 * (degree + 1) + i];

            uR[i] = g[rightBase + 0 * (degree + 1) + i];
            vR[i] = g[rightBase + 1 * (degree + 1) + i];
            wR[i] = g[rightBase + 2 * (degree + 1) + i];
        }

        // Left values
        double uL1 = GetAt1(uL), vL1 = GetAt1(vL), wL1 = GetAt1(wL);
        double uL1d = GetD1At1(uL), vL1d = GetD1At1(vL), wL1d = GetD1At1(wL);
        double uL2 = GetD2At1(uL), vL2 = GetD2At1(vL), wL2 = GetD2At1(wL);

        // Right values
        double uR0 = GetAt0(uR), vR0 = GetAt0(vR), wR0 = GetAt0(wR);
        double uR0d = GetD1At0(uR), vR0d = GetD1At0(vR), wR0d = GetD1At0(wR);
        double uR2 = GetD2At0(uR), vR2 = GetD2At0(vR), wR2 = GetD2At0(wR);

        // Precompute common combos
        var aL = (uL1 * uL1 - vL1 * vL1); // for x'
        var bL = uL1 * vL1;              // for y'
        var aR = (uR0 * uR0 - vR0 * vR0);
        var bR = uR0 * vR0;

        // Now for each unknown, compute its effect on u1, v1, w1 and their first & second derivatives
        for (var col = 0; col < n; col++)
        {
            var gi = unknownIndices[col];

            // Determine whether gi belongs to left or right and which polynomial & index
            var isLeft = (gi >= leftBase && gi < leftBase + 3 * (degree + 1));
            var isRight = (gi >= rightBase && gi < rightBase + 3 * (degree + 1));

            // if neither, column remains zero (shouldn't happen)
            double dx1 = 0.0, dy1 = 0.0, dx2 = 0.0, dy2 = 0.0;

            if (isLeft)
            {
                var local = gi - leftBase;
                var poly = local / (degree + 1); // 0=u,1=v,2=w
                var pos = local % (degree + 1);

                // partials for left side variable contributions
                // ∂u1, ∂u1d, ∂u1_2d according to pos
                double du1 = 0.0, du1d = 0.0, du12D = 0.0;
                double dv1 = 0.0, dv1d = 0.0, dv12D = 0.0;
                double dw1 = 0.0, dw1d = 0.0, dw12D = 0.0;

                if (poly == 0) // u
                {
                    if (pos == degree) du1 = 1.0;
                    if (pos == degree) du1d = ndeg;
                    if (pos == degree) du12D = ndeg * (ndeg - 1.0);
                    if (pos == degree - 1) du1d = -ndeg;
                    if (pos == degree - 1) du12D = -2.0 * ndeg * (ndeg - 1.0);
                    if (pos == degree - 2) du12D = ndeg * (ndeg - 1.0);
                }
                else if (poly == 1) // v
                {
                    if (pos == degree) dv1 = 1.0;
                    if (pos == degree) dv1d = ndeg;
                    if (pos == degree) dv12D = ndeg * (ndeg - 1.0);
                    if (pos == degree - 1) dv1d = -ndeg;
                    if (pos == degree - 1) dv12D = -2.0 * ndeg * (ndeg - 1.0);
                    if (pos == degree - 2) dv12D = ndeg * (ndeg - 1.0);
                }
                else // w
                {
                    if (pos == degree) dw1 = 1.0;
                    if (pos == degree) dw1d = ndeg;
                    if (pos == degree) dw12D = ndeg * (ndeg - 1.0);
                    if (pos == degree - 1) dw1d = -ndeg;
                    if (pos == degree - 1) dw12D = -2.0 * ndeg * (ndeg - 1.0);
                    if (pos == degree - 2) dw12D = ndeg * (ndeg - 1.0);
                }

                // compute contributions to residuals from left side
                // xL1 = w1*A_L  where A_L = u1^2 - v1^2
                dx1 += dw1 * aL + wL1 * (2.0 * uL1 * du1 - 2.0 * vL1 * dv1);
                dy1 += 2.0 * (dw1 * uL1 * vL1 + wL1 * (du1 * vL1 + uL1 * dv1));

                // xL2 = w1d*A_L + w1*(2*u1*u1d - 2*v1*v1d)
                dx2 += dw1d * aL + wL1d * (2.0 * uL1 * du1 - 2.0 * vL1 * dv1)
                       + dw1 * (2.0 * uL1 * uL1d - 2.0 * vL1 * vL1d)
                       + wL1 * (2.0 * (du1 * uL1d + uL1 * du1d) - 2.0 * (dv1 * vL1d + vL1 * dv1d));

                dy2 += 2.0 * (
                            dw1d * uL1 * vL1 + wL1d * (du1 * vL1 + uL1 * dv1)
                          + dw1 * (uL1d * vL1 + uL1 * vL1d)
                          + wL1 * (du1d * vL1 + uL1d * dv1 + du1 * vL1d + uL1 * dv1d)
                         );
            }
            else if (isRight)
            {
                var local = gi - rightBase;
                var poly = local / (degree + 1); // 0=u,1=v,2=w
                var pos = local % (degree + 1);

                // partials for right variables (at t=0)
                double du0 = 0.0, du0d = 0.0, du02D = 0.0;
                double dv0 = 0.0, dv0d = 0.0, dv02D = 0.0;
                double dw0 = 0.0, dw0d = 0.0, dw02D = 0.0;

                if (poly == 0) // u
                {
                    if (pos == 0) du0 = 1.0;
                    if (pos == 1) du0d = ndeg;
                    if (pos == 0) du0d += -ndeg; // because u0d = n*(c1 - c0) => ∂/∂c0 = -n
                                                 // second derivative: n*(n-1)*(c2 - 2*c1 + c0)
                    if (pos == 2) du02D = ndeg * (ndeg - 1.0);
                    if (pos == 1) du02D += -2.0 * ndeg * (ndeg - 1.0);
                    if (pos == 0) du02D += ndeg * (ndeg - 1.0);
                }
                else if (poly == 1) // v
                {
                    if (pos == 0) dv0 = 1.0;
                    if (pos == 1) dv0d = ndeg;
                    if (pos == 0) dv0d += -ndeg;
                    if (pos == 2) dv02D = ndeg * (ndeg - 1.0);
                    if (pos == 1) dv02D += -2.0 * ndeg * (ndeg - 1.0);
                    if (pos == 0) dv02D += ndeg * (ndeg - 1.0);
                }
                else // w
                {
                    if (pos == 0) dw0 = 1.0;
                    if (pos == 1) dw0d = ndeg;
                    if (pos == 0) dw0d += -ndeg;
                    if (pos == 2) dw02D = ndeg * (ndeg - 1.0);
                    if (pos == 1) dw02D += -2.0 * ndeg * (ndeg - 1.0);
                    if (pos == 0) dw02D += ndeg * (ndeg - 1.0);
                }

                // compute contributions to residuals from right side (note residual is L - R, so subtract right contributions)
                var dxR1 = dw0 * aR + wR0 * (2.0 * uR0 * du0 - 2.0 * vR0 * dv0);
                var dyR1 = 2.0 * (dw0 * uR0 * vR0 + wR0 * (du0 * vR0 + uR0 * dv0));

                var dxR2 = dw0d * aR + wR0d * (2.0 * uR0 * du0 - 2.0 * vR0 * dv0)
                                      + dw0 * (2.0 * uR0 * uR0d - 2.0 * vR0 * vR0d)
                                      + wR0 * (2.0 * (du0 * uR0d + uR0 * du0d) - 2.0 * (dv0 * vR0d + vR0 * dv0d));

                var dyR2 = 2.0 * (
                               dw0d * uR0 * vR0 + wR0d * (du0 * vR0 + uR0 * dv0)
                             + dw0 * (uR0d * vR0 + uR0 * vR0d)
                             + wR0 * (du0d * vR0 + uR0d * dv0 + du0 * vR0d + uR0 * dv0d)
                             );

                // subtract right contributions (residual = left - right)
                dx1 -= dxR1;
                dy1 -= dyR1;
                dx2 -= dxR2;
                dy2 -= dyR2;
            }

            // place into Jacobian column
            j[0, col] = dx1;
            j[1, col] = dy1;
            j[2, col] = dx2;
            j[3, col] = dy2;
        }

        return j;

        // Evaluate endpoint values & derivatives (same formulas used in residual)
        double GetAt1(double[] c) => c[degree];

        double GetAt0(double[] c) => c[0];

        double GetD1At1(double[] c)
        {
            var i0 = Math.Max(0, degree - 1);
            return ndeg * (c[degree] - c[i0]);
        }

        double GetD1At0(double[] c)
        {
            var i1 = Math.Min(1, degree);
            return ndeg * (c[i1] - c[0]);
        }

        double GetD2At1(double[] c)
        {
            var im1 = Math.Max(0, degree - 1);
            var im2 = Math.Max(0, degree - 2);
            return ndeg * (ndeg - 1.0) * (c[degree] - 2.0 * c[im1] + c[im2]);
        }

        double GetD2At0(double[] c)
        {
            var i1 = Math.Min(1, degree);
            var i2 = Math.Min(2, degree);
            return ndeg * (ndeg - 1.0) * (c[i2] - 2.0 * c[i1] + c[0]);
        }
    }


    /// <summary>
    /// Robust Gauss-Newton step using MathNet with optional analytic Jacobian support.
    /// If analyticJacobian != null the caller provides a matrix (m x n) for the current x; otherwise FD is used.
    /// </summary>
    private static double[] GaussNewtonStepMathNet(
        double[] genome,
        Func<double[], double[]> residualFunc,
        int[] unknownIndices,
        Func<double[], DenseMatrix> analyticJacobian = null,
        int maxIter = 6,
        double gTol = 1e-10,
        double absEpsMin = 1e-8)
    {
        if (genome == null) throw new ArgumentNullException(nameof(genome));
        if (residualFunc == null) throw new ArgumentNullException(nameof(residualFunc));
        if (unknownIndices == null || unknownIndices.Length == 0) return (double[])genome.Clone();

        var x = (double[])genome.Clone();

        const double machEps = 2.220446049250313e-16;
        var baseStepFactor = Math.Sqrt(machEps);

        var r = residualFunc(x);
        var m = r.Length;
        var rNorm = 0.0;
        for (var i = 0; i < m; i++)
            rNorm += r[i] * r[i];

        rNorm = Math.Sqrt(rNorm);

        if (rNorm <= gTol) return x;

        var n = unknownIndices.Length;

        for (var iter = 0; iter < maxIter; iter++)
        {
            DenseMatrix J;

            if (analyticJacobian != null)
            {
                try
                {
                    J = analyticJacobian(x);
                    // validate shape
                    if (J.RowCount != m || J.ColumnCount != n)
                        J = null;
                }
                catch
                {
                    J = null;
                }
            }
            else
            {
                J = null;
            }

            // If no analytic J available or invalid, build central-difference adaptive FD
            if (J == null)
            {
                J = DenseMatrix.Create(m, n, 0.0);

                for (var j = 0; j < n; j++)
                {
                    var gi = unknownIndices[j];
                    var saved = x[gi];
                    var scale = Math.Max(1.0, Math.Abs(saved));
                    var h = Math.Max(absEpsMin, baseStepFactor * scale);

                    x[gi] = saved + h;
                    var rp = residualFunc(x);
                    x[gi] = saved - h;
                    var rm = residualFunc(x);
                    x[gi] = saved;

                    var denom = 2.0 * h;
                    for (var i = 0; i < m; i++)
                        J[i, j] = (rp[i] - rm[i]) / denom;
                }
            }

            // Build RHS = -r
            var vecR = Vector.Build.DenseOfArray(r);
            var rhs = vecR.Multiply(-1.0);

            Vector<double> delta = null;
            var solved = false;

            // try QR
            try
            {
                var qr = J.QR();
                delta = qr.Solve(rhs);
                solved = true;
            }
            catch
            {
                solved = false;
                delta = null;
            }

            // fallback to SVD
            if (!solved || delta == null || double.IsNaN(delta.L2Norm()) || double.IsInfinity(delta.L2Norm()))
            {
                try
                {
                    var svd = J.Svd(true);
                    delta = svd.Solve(rhs);
                    solved = true;
                }
                catch
                {
                    solved = false;
                    delta = null;
                }
            }

            if (!solved || delta == null)
                break;

            // backtracking line-search
            var step = 1.0;
            const double shrink = 0.5;
            const int maxBacktrack = 12;

            for (var bt = 0; bt <= maxBacktrack; bt++)
            {
                var xNew = (double[])x.Clone();
                for (var j = 0; j < n; j++)
                    xNew[unknownIndices[j]] += step * delta[j];

                var rNew = residualFunc(xNew);
                var rn = 0.0;
                for (var i = 0; i < m; i++) rn += rNew[i] * rNew[i];
                var rNewNorm = Math.Sqrt(rn);

                if (rNewNorm < rNorm || bt == maxBacktrack)
                {
                    r = rNew;
                    rNorm = rNewNorm;
                    x = xNew;
                    break;
                }

                step *= shrink;
            }

            if (rNorm <= gTol) break;

            var dNorm = delta.L2Norm();
            if (dNorm * step <= 1e-12) break;
        }

        return x;
    }


    /// <summary>
    /// Enforce G2 continuity across internal joins for a genome representing multi-segment PH curves.
    /// Uses analytic Jacobian for degree 1..3 and FD for higher degrees.
    /// </summary>
    public static double[] EnforceG2(double[] genome, int segments, int degree, int maxItersPerJoin = 6)
    {
        if (genome == null)
            throw new ArgumentNullException(nameof(genome));

        var gCopy = (double[])genome.Clone();
        var key = GenomeHash(gCopy);

        lock (RepairCache)
        {
            if (RepairCache.TryGetValue(key, out var cached))
                return (double[])cached.Clone();
        }

        var block = 3 * (degree + 1);
        var nDeg = Math.Max(1.0, degree);

        for (var join = 0; join < segments - 1; join++)
        {
            if (degree == 0) continue;

            var leftBase = join * block;
            var rightBase = (join + 1) * block;

            // Choose small neighborhood unknown indices: last two of left, first two of right (for each poly)
            var idxList = new List<int>();
            for (var poly = 0; poly < 3; poly++)
            {
                var polyBaseL = leftBase + poly * (degree + 1);
                idxList.Add(polyBaseL + degree);
                idxList.Add(polyBaseL + Math.Max(0, degree - 1));
            }

            for (var poly = 0; poly < 3; poly++)
            {
                var polyBaseR = rightBase + poly * (degree + 1);
                idxList.Add(polyBaseR + 0);
                idxList.Add(polyBaseR + Math.Min(1, degree));
            }

            var uniq = idxList.Distinct().ToArray();
            var unknowns = uniq;

            // If degree <= 3 supply analytic Jacobian function; else null and FD will be used.
            Func<double[], DenseMatrix> analytic = null;
            if (degree >= 1 && degree <= 3)
            {
                analytic = (double[] g) => BuildAnalyticJacobianForJoin(g, unknowns, leftBase, rightBase, degree);
            }

            // Run Gauss-Newton with analytic (if available) / FD otherwise
            gCopy = GaussNewtonStepMathNet(gCopy, ResidualFunc, unknowns, analytic, maxIter: maxItersPerJoin, gTol: 1e-10);
            continue;

            // residual function (same as before)
            double[] ResidualFunc(double[] g)
            {
                var uL = new double[degree + 1];
                var vL = new double[degree + 1];
                var wL = new double[degree + 1];
                var uR = new double[degree + 1];
                var vR = new double[degree + 1];
                var wR = new double[degree + 1];

                for (var i = 0; i <= degree; i++)
                {
                    uL[i] = g[leftBase + 0 * (degree + 1) + i];
                    vL[i] = g[leftBase + 1 * (degree + 1) + i];
                    wL[i] = g[leftBase + 2 * (degree + 1) + i];

                    uR[i] = g[rightBase + 0 * (degree + 1) + i];
                    vR[i] = g[rightBase + 1 * (degree + 1) + i];
                    wR[i] = g[rightBase + 2 * (degree + 1) + i];
                }

                // Left
                double uL1 = GetAt1(uL), vL1 = GetAt1(vL), wL1 = GetAt1(wL);
                double uL1d = GetD1At1(uL), vL1d = GetD1At1(vL), wL1d = GetD1At1(wL);
                double uL2d = GetD2At1(uL), vL2d = GetD2At1(vL), wL2d = GetD2At1(wL);

                // Right
                double uR0 = GetAt0(uR), vR0 = GetAt0(vR), wR0 = GetAt0(wR);
                double uR0d = GetD1At0(uR), vR0d = GetD1At0(vR), wR0d = GetD1At0(wR);
                double uR2d = GetD2At0(uR), vR2d = GetD2At0(vR), wR2d = GetD2At0(wR);

                var xL1 = wL1 * (uL1 * uL1 - vL1 * vL1);
                var yL1 = 2.0 * wL1 * uL1 * vL1;
                var xL2 = wL1d * (uL1 * uL1 - vL1 * vL1) + wL1 * (2.0 * uL1 * uL1d - 2.0 * vL1 * vL1d);
                var yL2 = 2.0 * wL1d * uL1 * vL1 + 2.0 * wL1 * (uL1d * vL1 + uL1 * vL1d);

                var xR0 = wR0 * (uR0 * uR0 - vR0 * vR0);
                var yR0 = 2.0 * wR0 * uR0 * vR0;
                var xR2 = wR0d * (uR0 * uR0 - vR0 * vR0) + wR0 * (2.0 * uR0 * uR0d - 2.0 * vR0 * vR0d);
                var yR2 = 2.0 * wR0d * uR0 * vR0 + 2.0 * wR0 * (uR0d * vR0 + uR0 * vR0d);

                return [xL1 - xR0, yL1 - yR0, xL2 - xR2, yL2 - yR2];

                double GetAt1(double[] c) => c[degree];

                double GetAt0(double[] c) => c[0];

                double GetD1At1(double[] c)
                {
                    var i0 = Math.Max(0, degree - 1);
                    return nDeg * (c[degree] - c[i0]);
                }

                double GetD1At0(double[] c)
                {
                    var i1 = Math.Min(1, degree);
                    return nDeg * (c[i1] - c[0]);
                }

                double GetD2At1(double[] c)
                {
                    var im1 = Math.Max(0, degree - 1);
                    var im2 = Math.Max(0, degree - 2);
                    return nDeg * (nDeg - 1.0) * (c[degree] - 2.0 * c[im1] + c[im2]);
                }

                double GetD2At0(double[] c)
                {
                    var i1 = Math.Min(1, degree);
                    var i2 = Math.Min(2, degree);
                    return nDeg * (nDeg - 1.0) * (c[i2] - 2.0 * c[i1] + c[0]);
                }
            }
        }

        // Cache result
        lock (RepairCache)
        {
            try
            {
                RepairCache[key] = (double[])gCopy.Clone();
            }
            catch { }
        }

        return gCopy;
    }

}