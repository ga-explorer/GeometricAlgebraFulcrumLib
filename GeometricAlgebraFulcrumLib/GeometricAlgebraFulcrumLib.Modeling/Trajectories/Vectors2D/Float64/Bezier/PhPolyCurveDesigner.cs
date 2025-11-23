using MathNet.Numerics.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Bernstein; // for LinFloat64Vector2D

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier
{
    /// <summary>
    /// Designer that builds 5 connected PH segments (quadratic u,v,w per segment)
    /// satisfying C2 continuity. Returns a list of GeneratorData objects suitable
    /// for constructing PhBernsteinCurve2D instances.
    /// </summary>
    public class PhPolyCurveDesigner
    {
        public const int Segments = 5;

        private readonly Random _rand;


        public double FiniteDifferenceEpsilon { get; set; } = 1e-6;

        public double Tolerance { get; set; } = 1e-9;

        public int MaxIterations { get; set; } = 300;

        public int RandomRestarts { get; set; } = 50;


        public PhPolyCurveDesigner(int? seed = null)
        {
            _rand = seed.HasValue ? new Random(seed.Value) : new Random();
        }


        /// <summary>
        /// Build 5 connected PH segments (quadratic generators each) forming a C2 poly-curve.
        /// If interiorPoints is null, 4 random interior joints are generated.
        /// useAnalyticJacobian toggles analytic Jacobian (true) or finite-difference (false).
        /// Returns a list of PhBernsteinCurve2D.GeneratorData (one per segment).
        /// </summary>
        public List<PhBernsteinCurve2D.GeneratorData> BuildRandomC2PolyCurve(LinFloat64Vector2D startPt, LinFloat64Vector2D endPt, List<LinFloat64Vector2D>? interiorPoints = null, bool useAnalyticJacobian = true)
        {
            var joints = new List<double[]> { new[] { startPt[0], startPt[1] } };

            if (interiorPoints != null)
            {
                if (interiorPoints.Count != 4)
                    throw new ArgumentException("interiorPoints must contain exactly 4 points or be null.");
                
                joints.AddRange(
                    interiorPoints.Select(p => new[] { p[0], p[1] })
                );
            }
            else
            {
                for (var i = 1; i <= 4; i++)
                {
                    var t = (double)i / (Segments);
                    var x = (1 - t) * startPt[0] + t * endPt[0] + (_rand.NextDouble() - 0.5) * 0.5;
                    var y = (1 - t) * startPt[1] + t * endPt[1] + (_rand.NextDouble() - 0.5) * 0.5;
                    
                    joints.Add([x, y]);
                }
            }

            joints.Add([endPt[0], endPt[1]]);

            var nVars = Segments * 9;
            var x0 = Vector<double>.Build.Dense(nVars);

            for (var attempt = 0; attempt < RandomRestarts; attempt++)
            {
                InitializeRandomGuess(x0, joints);

                var ok = SolveForGenerators(x0, joints, out var solution, useAnalyticJacobian);

                if (ok) return UnpackSolutionToGeneratorData(solution, joints);
            }

            throw new InvalidOperationException("Failed to converge to a C2 solution after multiple restarts.");
        }


        /// <summary>
        /// Initialization helper
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="joints"></param>
        private void InitializeRandomGuess(Vector<double> x0, List<double[]> joints)
        {
            for (var seg = 0; seg < Segments; seg++)
            {
                var p0 = joints[seg];
                var p1 = joints[seg + 1];
                var dx = p1[0] - p0[0];
                var dy = p1[1] - p0[1];
                var len = Math.Sqrt(dx * dx + dy * dy);
                if (len < 1e-9) len = 1.0;

                var tx = dx / len;
                var ty = dy / len;
                var theta = Math.Atan2(ty, tx);
                var half = theta / 2.0;
                var scale = 1.0 + 0.2 * (_rand.NextDouble() - 0.5);
                var u0 = Math.Cos(half) * scale;
                var v0 = Math.Sin(half) * scale;

                var theta1 = theta + (_rand.NextDouble() - 0.5) * 0.4;
                var half1 = theta1 / 2.0;
                var scale1 = 1.0 + 0.2 * (_rand.NextDouble() - 0.5);
                var u1 = Math.Cos(half1) * scale1;
                var v1 = Math.Sin(half1) * scale1;

                var midU = 0.5 * (u0 + u1) * (1 + 0.3 * (_rand.NextDouble() - 0.5));
                var midV = 0.5 * (v0 + v1) * (1 + 0.3 * (_rand.NextDouble() - 0.5));

                var uCoefs = SolveQuadraticFromSamples(u0, midU, u1);
                var vCoefs = SolveQuadraticFromSamples(v0, midV, v1);
                var wCoefs = new[] { 1.0 + 0.1 * (_rand.NextDouble() - 0.5), 0.05 * (_rand.NextDouble() - 0.5), 0.02 * (_rand.NextDouble() - 0.5) };

                var baseIdx = seg * 9;
                x0[baseIdx + 0] = uCoefs[0];
                x0[baseIdx + 1] = uCoefs[1];
                x0[baseIdx + 2] = uCoefs[2];

                x0[baseIdx + 3] = vCoefs[0];
                x0[baseIdx + 4] = vCoefs[1];
                x0[baseIdx + 5] = vCoefs[2];

                x0[baseIdx + 6] = wCoefs[0];
                x0[baseIdx + 7] = wCoefs[1];
                x0[baseIdx + 8] = wCoefs[2];
            }
        }

        private static double[] SolveQuadraticFromSamples(double v0, double vMid, double v1)
        {
            var A = v0;
            var B = 4.0 * vMid - 3.0 * v0 - v1;
            var C = v1 - A - B;

            return [A, B, C];
        }

        /// <summary>
        /// Nonlinear solver (LM)
        /// </summary>
        /// <param name="xInit"></param>
        /// <param name="joints"></param>
        /// <param name="solution"></param>
        /// <param name="useAnalyticJacobian"></param>
        /// <returns></returns>
        private bool SolveForGenerators(Vector<double> xInit, List<double[]> joints, out Vector<double> solution, bool useAnalyticJacobian)
        {
            var nVars = xInit.Count;
            var mRes = 10 + 8 + 8 + 5; // pos + first deriv continuity + second deriv continuity + scale fix

            var x = xInit.Clone();
            var r = Vector<double>.Build.Dense(mRes);

            var lambda = 1e-3;
            var prevNorm = double.PositiveInfinity;

            for (var iter = 0; iter < MaxIterations; iter++)
            {
                EvaluateResiduals(x, joints, r);

                var norm = r.L2Norm();
                if (norm < Tolerance) { solution = x.Clone(); return true; }

                var J = useAnalyticJacobian
                    ? BuildAnalyticJacobian(x, joints)
                    : BuildFiniteDifferenceJacobian(x, joints, r);

                var JT = J.Transpose();
                var A = JT * J;
                for (var d = 0; d < A.RowCount; d++)
                    A[d, d] += lambda;

                var rhs = -JT * r;
                Vector<double> delta;
                try
                {
                    delta = A.Solve(rhs);
                }
                catch
                {
                    lambda *= 10;
                    if (lambda > 1e12) break;
                    continue;
                }

                var xCandidate = x + delta;
                var rCandidate = Vector<double>.Build.Dense(mRes);
                EvaluateResiduals(xCandidate, joints, rCandidate);

                var normCandidate = rCandidate.L2Norm();

                if (normCandidate < norm)
                {
                    x = xCandidate;
                    r = rCandidate;
                    norm = normCandidate;
                    lambda = Math.Max(lambda / 10.0, 1e-12);
                }
                else
                {
                    lambda *= 10.0;
                    if (lambda > 1e12) break;
                }

                if (delta.L2Norm() < 1e-12 * (1.0 + x.L2Norm())) break;
                if (Math.Abs(prevNorm - norm) < 1e-14) break;
                prevNorm = norm;
            }

            EvaluateResiduals(x, joints, r);
            if (r.L2Norm() < 1e-7)
            {
                solution = x.Clone();
                return true;
            }

            solution = null;
            return false;
        }

        /// <summary>
        /// Finite-difference Jacobian
        /// </summary>
        /// <param name="x"></param>
        /// <param name="joints"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        private Matrix<double> BuildFiniteDifferenceJacobian(Vector<double> x, List<double[]> joints, Vector<double> r)
        {
            var nVars = x.Count;
            var mRes = r.Count;
            var J = Matrix<double>.Build.Dense(mRes, nVars);

            for (var k = 0; k < nVars; k++)
            {
                var orig = x[k];
                var h = FiniteDifferenceEpsilon * Math.Max(1.0, Math.Abs(orig));
                x[k] = orig + h;
                var rp = Vector<double>.Build.Dense(mRes);

                EvaluateResiduals(x, joints, rp);

                var col = (rp - r) / h;
                for (var i = 0; i < mRes; i++) J[i, k] = col[i];
                x[k] = orig;
            }

            return J;
        }

        /// <summary>
        /// Analytic Jacobian: (kept same structure as earlier implementation)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="joints"></param>
        /// <returns></returns>
        private static Matrix<double> BuildAnalyticJacobian(Vector<double> x, List<double[]> joints)
        {
            var nVars = x.Count;
            var mRes = 10 + 8 + 8 + 5;
            var J = Matrix<double>.Build.Dense(mRes, nVars);

            var idxRow = 0;

            // 1) Position constraints (integrals)
            for (var seg = 0; seg < Segments; seg++)
            {
                var u = GetCoeffs(x, seg, 0);
                var v = GetCoeffs(x, seg, 3);
                var w = GetCoeffs(x, seg, 6);

                var uc = u;
                var vc = v;
                var wc = w;

                var Pxc = new double[5];
                var Pyc = new double[5];

                for (var i = 0; i <= 2; i++)
                    for (var j = 0; j <= 2; j++)
                        Pxc[i + j] += uc[i] * uc[j];

                for (var i = 0; i <= 2; i++)
                    for (var j = 0; j <= 2; j++)
                        Pxc[i + j] -= vc[i] * vc[j];

                for (var i = 0; i <= 2; i++)
                    for (var j = 0; j <= 2; j++)
                        Pyc[i + j] += 2.0 * uc[i] * vc[j];

                var baseIdx = seg * 9;
                Func<int, double> moment = (m) => 1.0 / (m + 1);

                // u-coeffs
                for (var k = 0; k <= 2; k++)
                {
                    var integrandX = new double[7];
                    var integrandY = new double[7];

                    for (var i = 0; i <= 2; i++)
                        for (var j = 0; j <= 2; j++)
                        {
                            var deg = i + (j + k);
                            integrandX[deg] += 2.0 * wc[i] * uc[j];
                            integrandY[deg] += 2.0 * wc[i] * vc[j];
                        }

                    double ix = 0.0, iy = 0.0;
                    for (var m = 0; m < integrandX.Length; m++)
                    {
                        if (Math.Abs(integrandX[m]) > 0.0) ix += integrandX[m] * moment(m);
                        if (Math.Abs(integrandY[m]) > 0.0) iy += integrandY[m] * moment(m);
                    }

                    J[idxRow + 0, baseIdx + k] = ix;
                    J[idxRow + 1, baseIdx + k] = iy;
                }

                // v-coeffs
                for (var k = 0; k <= 2; k++)
                {
                    var integrandX = new double[7];
                    var integrandY = new double[7];

                    for (var i = 0; i <= 2; i++)
                        for (var j = 0; j <= 2; j++)
                        {
                            var deg = i + (j + k);
                            integrandX[deg] += -2.0 * wc[i] * vc[j];
                            integrandY[deg] += 2.0 * wc[i] * uc[j];
                        }

                    double ix = 0.0, iy = 0.0;
                    for (var m = 0; m < integrandX.Length; m++)
                    {
                        if (Math.Abs(integrandX[m]) > 0.0) ix += integrandX[m] * moment(m);
                        if (Math.Abs(integrandY[m]) > 0.0) iy += integrandY[m] * moment(m);
                    }

                    J[idxRow + 0, baseIdx + 3 + k] = ix;
                    J[idxRow + 1, baseIdx + 3 + k] = iy;
                }

                // w-coeffs
                for (var k = 0; k <= 2; k++)
                {
                    double ix = 0.0, iy = 0.0;
                    for (var m = 0; m <= 4; m++)
                    {
                        var coefX = Pxc[m];
                        var coefY = Pyc[m];
                        var deg = m + k;
                        ix += coefX / (deg + 1.0);
                        iy += coefY / (deg + 1.0);
                    }

                    J[idxRow + 0, baseIdx + 6 + k] = ix;
                    J[idxRow + 1, baseIdx + 6 + k] = iy;
                }

                idxRow += 2;
            }

            // 2) First derivative continuity (evaluation at t=1 left, t=0 right)
            for (var joint = 1; joint <= 4; joint++)
            {
                var segL = joint - 1;
                var segR = joint;
                var baseL = segL * 9;
                var baseR = segR * 9;

                var tL = 1.0;
                var tR = 0.0;

                var uL = GetCoeffs(x, segL, 0);
                var vL = GetCoeffs(x, segL, 3);
                var wL = GetCoeffs(x, segL, 6);
                double uLt = EvalQuad(uL, tL), vLt = EvalQuad(vL, tL), wLt = EvalQuad(wL, tL);

                var uR = GetCoeffs(x, segR, 0);
                var vR = GetCoeffs(x, segR, 3);
                var wR = GetCoeffs(x, segR, 6);
                double uRt = EvalQuad(uR, tR), vRt = EvalQuad(vR, tR), wRt = EvalQuad(wR, tR);

                for (var k = 0; k <= 2; k++)
                {
                    var tkL = Math.Pow(tL, k);
                    J[idxRow + 0, baseL + k] = wLt * 2.0 * uLt * tkL;
                    J[idxRow + 1, baseL + k] = wLt * 2.0 * vLt * tkL;
                }

                for (var k = 0; k <= 2; k++)
                {
                    var tkL = Math.Pow(tL, k);
                    J[idxRow + 0, baseL + 3 + k] = wLt * (-2.0 * vLt * tkL);
                    J[idxRow + 1, baseL + 3 + k] = wLt * (2.0 * uLt * tkL);
                }

                var Pleft = new[] { uLt * uLt - vLt * vLt, 2.0 * uLt * vLt };
                for (var k = 0; k <= 2; k++)
                {
                    var tkL = Math.Pow(tL, k);
                    J[idxRow + 0, baseL + 6 + k] = tkL * Pleft[0];
                    J[idxRow + 1, baseL + 6 + k] = tkL * Pleft[1];
                }

                for (var k = 0; k <= 2; k++)
                {
                    var tkR = Math.Pow(tR, k);
                    J[idxRow + 0, baseR + k] = -wRt * 2.0 * uRt * tkR;
                    J[idxRow + 1, baseR + k] = -wRt * 2.0 * vRt * tkR;
                }

                for (var k = 0; k <= 2; k++)
                {
                    var tkR = Math.Pow(tR, k);
                    J[idxRow + 0, baseR + 3 + k] = -wRt * (-2.0 * vRt * tkR);
                    J[idxRow + 1, baseR + 3 + k] = -wRt * (2.0 * uRt * tkR);
                }

                var Pright = new[] { uRt * uRt - vRt * vRt, 2.0 * uRt * vRt };
                for (var k = 0; k <= 2; k++)
                {
                    var tkR = Math.Pow(tR, k);
                    J[idxRow + 0, baseR + 6 + k] = -tkR * Pright[0];
                    J[idxRow + 1, baseR + 6 + k] = -tkR * Pright[1];
                }

                idxRow += 2;
            }

            // 3) Second derivative continuity rows
            for (var joint = 1; joint <= 4; joint++)
            {
                var segL = joint - 1;
                var segR = joint;
                var baseL = segL * 9;
                var baseR = segR * 9;
                var tL = 1.0;
                var tR = 0.0;

                double[] uL = GetCoeffs(x, segL, 0), vL = GetCoeffs(x, segL, 3), wL = GetCoeffs(x, segL, 6);
                double uLt = EvalQuad(uL, tL), vLt = EvalQuad(vL, tL), wLt = EvalQuad(wL, tL);
                double uLpt = EvalQuadDerivative(uL, tL), vLpt = EvalQuadDerivative(vL, tL), wLpt = EvalQuadDerivative(wL, tL);

                var PxL = uLt * uLt - vLt * vLt;
                var PyL = 2.0 * uLt * vLt;
                var PxtL = 2.0 * (uLt * uLpt - vLt * vLpt);
                var PytL = 2.0 * (uLpt * vLt + uLt * vLpt);

                double[] uR = GetCoeffs(x, segR, 0), vR = GetCoeffs(x, segR, 3), wR = GetCoeffs(x, segR, 6);
                double uRt = EvalQuad(uR, tR), vRt = EvalQuad(vR, tR), wRt = EvalQuad(wR, tR);
                double uRpt = EvalQuadDerivative(uR, tR), vRpt = EvalQuadDerivative(vR, tR), wRpt = EvalQuadDerivative(wR, tR);
                var PxR = uRt * uRt - vRt * vRt;
                var PyR = 2.0 * uRt * vRt;
                var PxtR = 2.0 * (uRt * uRpt - vRt * vRpt);
                var PytR = 2.0 * (uRpt * vRt + uRt * vRpt);

                // left
                for (var k = 0; k <= 2; k++)
                {
                    var dukt = Math.Pow(tL, k);
                    var dukpt = (k == 0) ? 0.0 : k * Math.Pow(tL, k - 1);
                    var dPxDa = 2.0 * uLt * dukt;
                    var dPyDa = 2.0 * vLt * dukt;
                    var dPxtDa = 2.0 * (dukt * uLpt + uLt * dukpt);
                    var dPytDa = 2.0 * (dukpt * vLt + dukt * vLpt);
                    var dr2XDa = wLpt * dPxDa + wLt * dPxtDa;
                    var dr2YDa = wLpt * dPyDa + wLt * dPytDa;
                    J[idxRow + 0, baseL + k] = dr2XDa;
                    J[idxRow + 1, baseL + k] = dr2YDa;
                }

                for (var k = 0; k <= 2; k++)
                {
                    var dvkt = Math.Pow(tL, k);
                    var dvkpt = (k == 0) ? 0.0 : k * Math.Pow(tL, k - 1);
                    var dPxDb = -2.0 * vLt * dvkt;
                    var dPyDb = 2.0 * uLt * dvkt;
                    var dPxtDb = 2.0 * (-(dvkt * vLpt) - vLt * dvkpt);
                    var dPytDb = 2.0 * (uLt * dvkpt + uLpt * dvkt);
                    var dr2XDb = wLpt * dPxDb + wLt * dPxtDb;
                    var dr2YDb = wLpt * dPyDb + wLt * dPytDb;
                    J[idxRow + 0, baseL + 3 + k] = dr2XDb;
                    J[idxRow + 1, baseL + 3 + k] = dr2YDb;
                }

                for (var k = 0; k <= 2; k++)
                {
                    var dw = Math.Pow(tL, k);
                    var dwp = (k == 0) ? 0.0 : k * Math.Pow(tL, k - 1);
                    var dr2XDc = dwp * PxL + dw * PxtL;
                    var dr2YDc = dwp * PyL + dw * PytL;
                    J[idxRow + 0, baseL + 6 + k] = dr2XDc;
                    J[idxRow + 1, baseL + 6 + k] = dr2YDc;
                }

                // right (subtract)
                for (var k = 0; k <= 2; k++)
                {
                    var dukt = Math.Pow(tR, k);
                    var dukpt = (k == 0) ? 0.0 : k * Math.Pow(tR, k - 1);
                    var dPxDa = 2.0 * uRt * dukt;
                    var dPyDa = 2.0 * vRt * dukt;
                    var dPxtDa = 2.0 * (dukt * uRpt + uRt * dukpt);
                    var dPytDa = 2.0 * (dukpt * vRt + dukt * vRpt);
                    var dr2XDa = wRpt * dPxDa + wRt * dPxtDa;
                    var dr2YDa = wRpt * dPyDa + wRt * dPytDa;
                    J[idxRow + 0, baseR + k] = -dr2XDa;
                    J[idxRow + 1, baseR + k] = -dr2YDa;
                }

                for (var k = 0; k <= 2; k++)
                {
                    var dvkt = Math.Pow(tR, k);
                    var dvkpt = (k == 0) ? 0.0 : k * Math.Pow(tR, k - 1);
                    var dPxDb = -2.0 * vRt * dvkt;
                    var dPyDb = 2.0 * uRt * dvkt;
                    var dPxtDb = 2.0 * (-(dvkt * vRpt) - vRt * dvkpt);
                    var dPytDb = 2.0 * (uRt * dvkpt + uRpt * dvkt);
                    var dr2XDb = wRpt * dPxDb + wRt * dPxtDb;
                    var dr2YDb = wRpt * dPyDb + wRt * dPytDb;
                    J[idxRow + 0, baseR + 3 + k] = -dr2XDb;
                    J[idxRow + 1, baseR + 3 + k] = -dr2YDb;
                }

                for (var k = 0; k <= 2; k++)
                {
                    var dw = Math.Pow(tR, k);
                    var dwp = (k == 0) ? 0.0 : k * Math.Pow(tR, k - 1);
                    var dr2XDc = dwp * PxR + dw * PxtR;
                    var dr2YDc = dwp * PyR + dw * PytR;
                    J[idxRow + 0, baseR + 6 + k] = -dr2XDc;
                    J[idxRow + 1, baseR + 6 + k] = -dr2YDc;
                }

                idxRow += 2;
            }

            // 4) scale-fix rows
            for (var seg = 0; seg < Segments; seg++)
            {
                var base1 = seg * 9;
                J[idxRow, base1 + 6 + 0] = 1.0;
                idxRow++;
            }

            return J;
        }

        /// <summary>
        /// Residual evaluator
        /// </summary>
        /// <param name="x"></param>
        /// <param name="joints"></param>
        /// <param name="r"></param>
        private static void EvaluateResiduals(Vector<double> x, List<double[]> joints, Vector<double> r)
        {
            var idx = 0;

            // position integrals
            for (var seg = 0; seg < Segments; seg++)
            {
                var u = GetCoeffs(x, seg, 0);
                var v = GetCoeffs(x, seg, 3);
                var w = GetCoeffs(x, seg, 6);

                var integral = Integrate_wP(u, v, w);
                var delta = new[] { joints[seg + 1][0] - joints[seg][0], joints[seg + 1][1] - joints[seg][1] };
                r[idx++] = integral[0] - delta[0];
                r[idx++] = integral[1] - delta[1];
            }

            // first derivative continuity
            for (var joint = 1; joint <= 4; joint++)
            {
                var segL = joint - 1;
                var segR = joint;
                var rL = Eval_rPrime_at(x, segL, 1.0);
                var rR = Eval_rPrime_at(x, segR, 0.0);
                r[idx++] = rL[0] - rR[0];
                r[idx++] = rL[1] - rR[1];
            }

            // second derivative continuity
            for (var joint = 1; joint <= 4; joint++)
            {
                var segL = joint - 1;
                var segR = joint;
                var r2L = Eval_rDoublePrime_at(x, segL, 1.0);
                var r2R = Eval_rDoublePrime_at(x, segR, 0.0);
                r[idx++] = r2L[0] - r2R[0];
                r[idx++] = r2L[1] - r2R[1];
            }

            // scale fix
            for (var seg = 0; seg < Segments; seg++)
            {
                var w = GetCoeffs(x, seg, 6);
                r[idx++] = w[0] - 1.0;
            }
        }

        /// <summary>
        /// Small helpers
        /// </summary>
        /// <param name="x"></param>
        /// <param name="seg"></param>
        /// <param name="offsetInSeg"></param>
        /// <returns></returns>
        private static double[] GetCoeffs(Vector<double> x, int seg, int offsetInSeg)
        {
            var baseIdx = seg * 9 + offsetInSeg;
            return [x[baseIdx + 0], x[baseIdx + 1], x[baseIdx + 2]];
        }

        private static double[] Eval_rPrime_at(Vector<double> x, int seg, double t)
        {
            var u = GetCoeffs(x, seg, 0);
            var v = GetCoeffs(x, seg, 3);
            var w = GetCoeffs(x, seg, 6);

            var ut = EvalQuad(u, t);
            var vt = EvalQuad(v, t);
            var wt = EvalQuad(w, t);
            var px = ut * ut - vt * vt;
            var py = 2.0 * ut * vt;
            return [wt * px, wt * py];
        }

        private static double[] Eval_rDoublePrime_at(Vector<double> x, int seg, double t)
        {
            var u = GetCoeffs(x, seg, 0);
            var v = GetCoeffs(x, seg, 3);
            var w = GetCoeffs(x, seg, 6);

            var ut = EvalQuad(u, t);
            var vt = EvalQuad(v, t);
            var upt = EvalQuadDerivative(u, t);
            var vpt = EvalQuadDerivative(v, t);
            var wt = EvalQuad(w, t);
            var wpt = EvalQuadDerivative(w, t);

            var Px = ut * ut - vt * vt;
            var Py = 2.0 * ut * vt;
            var Pxt = 2.0 * (ut * upt - vt * vpt);
            var Pyt = 2.0 * (upt * vt + ut * vpt);

            var r2x = wpt * Px + wt * Pxt;
            var r2y = wpt * Py + wt * Pyt;
            return [r2x, r2y];
        }

        private static double EvalQuad(double[] c, double t)
        {
            return c[0] + c[1] * t + c[2] * t * t;
        }

        private static double EvalQuadDerivative(double[] c, double t)
        {
            return c[1] + 2.0 * c[2] * t;
        }

        private static double[] Integrate_wP(double[] u, double[] v, double[] w)
        {
            var uc = u; var vc = v; var wc = w;

            var Pxc = new double[5];
            var Pyc = new double[5];

            for (var i = 0; i <= 2; i++)
                for (var j = 0; j <= 2; j++)
                    Pxc[i + j] += uc[i] * uc[j];

            for (var i = 0; i <= 2; i++)
                for (var j = 0; j <= 2; j++)
                    Pxc[i + j] -= vc[i] * vc[j];

            for (var i = 0; i <= 2; i++)
                for (var j = 0; j <= 2; j++)
                    Pyc[i + j] += 2.0 * uc[i] * vc[j];

            var Ixc = new double[7];
            var Iyc = new double[7];

            for (var i = 0; i <= 2; i++)
                for (var j = 0; j <= 4; j++)
                {
                    Ixc[i + j] += wc[i] * Pxc[j];
                    Iyc[i + j] += wc[i] * Pyc[j];
                }

            double ix = 0.0, iy = 0.0;
            for (var m = 0; m < Ixc.Length; m++)
            {
                ix += Ixc[m] / (m + 1.0);
                iy += Iyc[m] / (m + 1.0);
            }

            return [ix, iy];
        }

        /// <summary>
        /// Convert solution -> GeneratorData
        /// </summary>
        /// <param name="sol"></param>
        /// <param name="joints"></param>
        /// <returns></returns>
        private static List<PhBernsteinCurve2D.GeneratorData> UnpackSolutionToGeneratorData(Vector<double> sol, List<double[]> joints)
        {
            var result = new List<PhBernsteinCurve2D.GeneratorData>();
            for (var seg = 0; seg < Segments; seg++)
            {
                var baseIdx = seg * 9;
                var uCoefs = new[] { sol[baseIdx + 0], sol[baseIdx + 1], sol[baseIdx + 2] };
                var vCoefs = new[] { sol[baseIdx + 3], sol[baseIdx + 4], sol[baseIdx + 5] };
                var wCoefs = new[] { sol[baseIdx + 6], sol[baseIdx + 7], sol[baseIdx + 8] };

                // Convert coefficient arrays (quadratic) to BernsteinPolynomial objects.
                // Assumes BernsteinPolynomial has a constructor taking power basis coefficients a0 + a1 t + a2 t^2
                // If your BernsteinPolynomial factory uses different ordering or expects Bernstein basis, adjust accordingly.
                var uPoly = BernsteinPolynomial.Create(uCoefs);
                var vPoly = BernsteinPolynomial.Create(vCoefs);
                var wPoly = BernsteinPolynomial.Create(wCoefs);

                var p0 = joints[seg];
                var p0Vec = LinFloat64Vector2D.Create(p0[0], p0[1]);

                var gen = new PhBernsteinCurve2D.GeneratorData
                {
                    U = uPoly,
                    V = vPoly,
                    W = wPoly,
                    P0 = p0Vec,
                    S0 = 0.0
                };

                result.Add(gen);
            }

            return result;
        }
    }
}

//using MathNet.Numerics.LinearAlgebra;

//namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier
//{
//    public class PhPolyCurveDesigner(int? seed = null)
//    {
//        public class PhSegmentGenerators
//        {
//            public double[] U = new double[3]; // length 3
//            public double[] V = new double[3]; // length 3
//            public double[] W = new double[3]; // length 3
//        }


//        public const int Segments = 5;
        
//        private readonly Random _rand = 
//            seed.HasValue ? new Random(seed.Value) : new Random();


//        public double FiniteDifferenceEpsilon { get; set; } = 1e-6;
        
//        public double Tolerance { get; set; } = 1e-9;
        
//        public int MaxIterations { get; set; } = 300;
        
//        public int RandomRestarts { get; set; } = 5;


//        /// <summary>
//        /// Build 5 connected PH segments (quadratic generators each) forming C2 poly-curve.
//        /// useAnalyticJacobian toggles analytic Jacobian (true) or finite-difference (false).
//        /// </summary>
//        public List<PhSegmentGenerators> BuildRandomC2PolyCurve(double[] startPt, double[] endPt, List<double[]>? interiorPoints = null, bool useAnalyticJacobian = true)
//        {
//            if (startPt.Length != 2 || endPt.Length != 2)
//                throw new ArgumentException("Points must be 2D arrays length 2.");

//            var joints = new List<double[]> { startPt };

//            if (interiorPoints != null)
//            {
//                if (interiorPoints.Count != 4)
//                    throw new ArgumentException("interiorPoints must contain exactly 4 points or be null.");
//                joints.AddRange(interiorPoints);
//            }
//            else
//            {
//                for (var i = 1; i <= 4; i++)
//                {
//                    var t = (double)i / (Segments);
//                    var x = (1 - t) * startPt[0] + t * endPt[0] + (_rand.NextDouble() - 0.5) * 0.5;
//                    var y = (1 - t) * startPt[1] + t * endPt[1] + (_rand.NextDouble() - 0.5) * 0.5;
                    
//                    joints.Add([x, y]);
//                }
//            }
//            joints.Add(endPt);

//            var nVars = Segments * 9;
//            var x0 = Vector<double>.Build.Dense(nVars);

//            for (var attempt = 0; attempt < RandomRestarts; attempt++)
//            {
//                InitializeRandomGuess(x0, joints);

//                var ok = SolveForGenerators(x0, joints, out var solution, useAnalyticJacobian);

//                if (ok) return UnpackSolution(solution);
//            }

//            throw new InvalidOperationException("Failed to converge to a C2 solution after multiple restarts.");
//        }


//        private void InitializeRandomGuess(Vector<double> x0, List<double[]> joints)
//        {
//            for (var seg = 0; seg < Segments; seg++)
//            {
//                var p0 = joints[seg];
//                var p1 = joints[seg + 1];
//                var dx = p1[0] - p0[0];
//                var dy = p1[1] - p0[1];
//                var len = Math.Sqrt(dx * dx + dy * dy);
//                if (len < 1e-9) len = 1.0;

//                double tx = dx / len, ty = dy / len;
//                var theta = Math.Atan2(ty, tx);
//                var half = theta / 2.0;
//                var scale = 1.0 + 0.2 * (_rand.NextDouble() - 0.5);
//                var u0 = Math.Cos(half) * scale;
//                var v0 = Math.Sin(half) * scale;

//                var theta1 = theta + (_rand.NextDouble() - 0.5) * 0.4;
//                var half1 = theta1 / 2.0;
//                var scale1 = 1.0 + 0.2 * (_rand.NextDouble() - 0.5);
//                var u1 = Math.Cos(half1) * scale1;
//                var v1 = Math.Sin(half1) * scale1;

//                var midU = 0.5 * (u0 + u1) * (1 + 0.3 * (_rand.NextDouble() - 0.5));
//                var midV = 0.5 * (v0 + v1) * (1 + 0.3 * (_rand.NextDouble() - 0.5));

//                var uCoefs = SolveQuadraticFromSamples(u0, midU, u1);
//                var vCoefs = SolveQuadraticFromSamples(v0, midV, v1);
//                var wCoefs = new[] { 1.0 + 0.1 * (_rand.NextDouble() - 0.5), 0.05 * (_rand.NextDouble() - 0.5), 0.02 * (_rand.NextDouble() - 0.5) };

//                var baseIdx = seg * 9;
//                x0[baseIdx + 0] = uCoefs[0];
//                x0[baseIdx + 1] = uCoefs[1];
//                x0[baseIdx + 2] = uCoefs[2];

//                x0[baseIdx + 3] = vCoefs[0];
//                x0[baseIdx + 4] = vCoefs[1];
//                x0[baseIdx + 5] = vCoefs[2];

//                x0[baseIdx + 6] = wCoefs[0];
//                x0[baseIdx + 7] = wCoefs[1];
//                x0[baseIdx + 8] = wCoefs[2];
//            }
//        }

//        private static double[] SolveQuadraticFromSamples(double v0, double vMid, double v1)
//        {
//            var A = v0;
//            var B = 4.0 * vMid - 3.0 * v0 - v1;
//            var C = v1 - A - B;
//            return [A, B, C];
//        }

//        private bool SolveForGenerators(Vector<double> xInit, List<double[]> joints, out Vector<double> solution, bool useAnalyticJacobian)
//        {
//            var nVars = xInit.Count;
//            var mRes = 10 + 8 + 8 + 5; // pos + first derivative continuity + second derivative continuity + scale fix

//            var x = xInit.Clone();
//            var r = Vector<double>.Build.Dense(mRes);

//            var lambda = 1e-3;
//            var prevNorm = double.PositiveInfinity;

//            for (var iter = 0; iter < MaxIterations; iter++)
//            {
//                EvaluateResiduals(x, joints, r);

//                var norm = r.L2Norm();
//                if (norm < Tolerance) { solution = x.Clone(); return true; }

//                Matrix<double> J = useAnalyticJacobian 
//                    ? BuildAnalyticJacobian(x, joints) 
//                    : BuildFiniteDifferenceJacobian(x, joints, r);

//                var JT = J.Transpose();
//                var A = JT * J;
//                for (var d = 0; d < A.RowCount; d++) 
//                    A[d, d] += lambda;

//                var rhs = -JT * r;
//                Vector<double> delta;
//                try
//                {
//                    delta = A.Solve(rhs);
//                }
//                catch
//                {
//                    lambda *= 10;
//                    if (lambda > 1e12) break;
//                    continue;
//                }

//                var xCandidate = x + delta;
//                var rCandidate = Vector<double>.Build.Dense(mRes);
//                EvaluateResiduals(xCandidate, joints, rCandidate);
                
//                var normCandidate = rCandidate.L2Norm();

//                if (normCandidate < norm)
//                {
//                    x = xCandidate;
//                    r = rCandidate;
//                    norm = normCandidate;
//                    lambda = Math.Max(lambda / 10.0, 1e-12);
//                }
//                else
//                {
//                    lambda *= 10.0;
//                    if (lambda > 1e12) break;
//                }

//                if (delta.L2Norm() < 1e-12 * (1.0 + x.L2Norm())) break;
//                if (Math.Abs(prevNorm - norm) < 1e-14) break;
//                prevNorm = norm;
//            }

//            EvaluateResiduals(x, joints, r);
//            if (r.L2Norm() < 1e-7)
//            {
//                solution = x.Clone();
//                return true;
//            }

//            solution = null;
//            return false;
//        }

//        private Matrix<double> BuildFiniteDifferenceJacobian(Vector<double> x, List<double[]> joints, Vector<double> r)
//        {
//            var nVars = x.Count;
//            var mRes = r.Count;
//            var J = Matrix<double>.Build.Dense(mRes, nVars);

//            for (var k = 0; k < nVars; k++)
//            {
//                var orig = x[k];
//                var h = FiniteDifferenceEpsilon * Math.Max(1.0, Math.Abs(orig));
//                x[k] = orig + h;
//                var rp = Vector<double>.Build.Dense(mRes);
                
//                EvaluateResiduals(x, joints, rp);
                
//                var col = (rp - r) / h;
//                for (var i = 0; i < mRes; i++) J[i, k] = col[i];
//                x[k] = orig;
//            }

//            return J;
//        }

//        private static Matrix<double> BuildAnalyticJacobian(Vector<double> x, List<double[]> joints)
//        {
//            var nVars = x.Count;
//            var mRes = 10 + 8 + 8 + 5;
//            var J = Matrix<double>.Build.Dense(mRes, nVars);

//            // Precompute per-segment polynomial coefficients for u,v,w and their derivatives
//            // We'll need powers of t up to degree 6 for integrals
//            var idxRow = 0;

//            // 1) Position constraints (integrals) -> for each segment produce 2 rows (x,y)
//            for (var seg = 0; seg < Segments; seg++)
//            {
//                var u = GetCoeffs(x, seg, 0);
//                var v = GetCoeffs(x, seg, 3);
//                var w = GetCoeffs(x, seg, 6);

//                // compute polynomial coefficients for u(t), v(t), w(t)
//                // u(t) = a0 + a1 t + a2 t^2  -> coefficients for t^0..t^2
//                // Px = u^2 - v^2 -> polynomial degree up to 4
//                // Py = 2 u v -> degree up to 4
//                // Integrand components: Ix(t) = w(t) * Px(t) -> degree up to 6
//                // Iy(t) similarly

//                // Build coefficient arrays for u, v, w as length-3 arrays
//                var uc = u; // length 3
//                var vc = v;
//                var wc = w;

//                // Compute Px coefficients Pxc[m] for m=0..4 such that Px(t)=sum Pxc[m]*t^m
//                var Pxc = new double[5];
//                var Pyc = new double[5];

//                // Expand u^2
//                // u^2 = (a0 + a1 t + a2 t^2)^2 = sum_{i,j} a_i a_j t^{i+j}
//                for (var i = 0; i <= 2; i++)
//                    for (var j = 0; j <= 2; j++)
//                        Pxc[i + j] += uc[i] * uc[j];

//                // subtract v^2
//                for (var i = 0; i <= 2; i++)
//                    for (var j = 0; j <= 2; j++)
//                        Pxc[i + j] -= vc[i] * vc[j];

//                // Py = 2 u v
//                for (var i = 0; i <= 2; i++)
//                    for (var j = 0; j <= 2; j++)
//                        Pyc[i + j] += 2.0 * uc[i] * vc[j];

//                // Now compute integrand polynomial coefficients for Ix = w * Px, degree up to 6
//                var Ixc = new double[7]; // degree 0..6
//                var Iyc = new double[7];
//                for (var i = 0; i <= 2; i++) // w coeff index
//                    for (var j = 0; j <= 4; j++) // Px index
//                    {
//                        Ixc[i + j] += wc[i] * Pxc[j];
//                        Iyc[i + j] += wc[i] * Pyc[j];
//                    }

//                // For each variable (coeff index) compute partial integral coefficients
//                // Partial derivatives integrands:
//                // - w.r.t a_k (u coefficient): integrand is w(t) * dPx/da_k
//                //   dPx/da_k = 2 u(t) t^k => polynomial degree up to 3+ k (<= 5)
//                // - w.r.t b_k: w(t) * dPx/db_k with dPx/db_k = -2 v t^k and similarly for Py
//                // - w.r.t c_k: t^k * Px(t) -> integrand with degree up to 4 + k (<=6)
//                // We'll compute each by expanding and then integrating exactly termwise.
//                // Precompute powers for moments: integral of t^m from 0..1 is 1/(m+1)
//                Func<int, double> moment = (m) => 1.0 / (m + 1);

//                // For each variable in the global x vector, place its partials
//                for (var segVar = 0; segVar < Segments; segVar++)
//                {
//                    // Only columns corresponding to this segment are nonzero
//                    if (segVar != seg) continue;
//                }

//                // Now directly fill Jacobian columns for this segment (9 variables)
//                var baseIdx = seg * 9;

//                // For each coefficient a_k (u): k=0..2
//                for (var k = 0; k <= 2; k++)
//                {
//                    // Build integrand polynomial for d(Ix)/da_k = w(t) * (2 u(t) t^k)
//                    // So integrand coefficients = 2 * convolution of w coefficients with (u coefficients shifted by k)
//                    var integrandX = new double[7]; // deg 0..6
//                    var integrandY = new double[7];

//                    // compute polynomial Q(t) = u(t) * t^k -> coefficients q[m] = u_coefs[m-k] if m>=k
//                    var q = new double[3 + k]; // but easier: treat u(t) * t^k yields degree up to 2+k
//                    // We'll convolve w (deg up to2) with (2 * (u * t^k)) for Ix, and with (2 * (v * t^k)) suitably for Py sign
//                    // For Ix:
//                    for (var i = 0; i <= 2; i++) // w coeff
//                    {
//                        for (var j = 0; j <= 2; j++) // u coeff index
//                        {
//                            var deg = i + (j + k); // w_index + u_index + k
//                            integrandX[deg] += 2.0 * wc[i] * uc[j];
//                        }
//                    }
//                    // For Iy: derivative wrt a_k affects Py: dPy/da_k = 2 v(t) t^k
//                    for (var i = 0; i <= 2; i++)
//                    {
//                        for (var j = 0; j <= 2; j++)
//                        {
//                            var deg = i + (j + k);
//                            integrandY[deg] += 2.0 * wc[i] * vc[j];
//                        }
//                    }

//                    // integrate integrand coefficients:
//                    double ix = 0.0, iy = 0.0;
//                    for (var m = 0; m < integrandX.Length; m++)
//                    {
//                        if (Math.Abs(integrandX[m]) > 0.0) ix += integrandX[m] * moment(m);
//                        if (Math.Abs(integrandY[m]) > 0.0) iy += integrandY[m] * moment(m);
//                    }

//                    J[idxRow + 0, baseIdx + k] = ix; // x-position residual row
//                    J[idxRow + 1, baseIdx + k] = iy; // y-position residual row
//                }

//                // For each coefficient b_k (v): k=0..2
//                for (var k = 0; k <= 2; k++)
//                {
//                    var integrandX = new double[7];
//                    var integrandY = new double[7];

//                    // dPx/db_k = -2 v t^k => integrandX = w * (-2 v t^k)
//                    for (var i = 0; i <= 2; i++)
//                        for (var j = 0; j <= 2; j++)
//                        {
//                            var deg = i + (j + k);
//                            integrandX[deg] += -2.0 * wc[i] * vc[j];
//                        }
//                    // dPy/db_k = 2 u t^k => integrandY = w * 2 u t^k
//                    for (var i = 0; i <= 2; i++)
//                        for (var j = 0; j <= 2; j++)
//                        {
//                            var deg = i + (j + k);
//                            integrandY[deg] += 2.0 * wc[i] * uc[j];
//                        }

//                    double ix = 0.0, iy = 0.0;
//                    for (var m = 0; m < integrandX.Length; m++)
//                    {
//                        if (Math.Abs(integrandX[m]) > 0.0) ix += integrandX[m] * moment(m);
//                        if (Math.Abs(integrandY[m]) > 0.0) iy += integrandY[m] * moment(m);
//                    }

//                    J[idxRow + 0, baseIdx + 3 + k] = ix;
//                    J[idxRow + 1, baseIdx + 3 + k] = iy;
//                }

//                // For each coefficient c_k (w): k=0..2
//                for (var k = 0; k <= 2; k++)
//                {
//                    // integrand = t^k * Px(t) and t^k * Py(t)
//                    double ix = 0.0, iy = 0.0;
//                    for (var m = 0; m <= 4; m++) // Px degree 0..4
//                    {
//                        var coefX = Pxc[m];
//                        var coefY = Pyc[m];
//                        var deg = m + k;
//                        var val = 1.0 / (deg + 1);
//                        ix += coefX * val;
//                        iy += coefY * val;
//                    }

//                    J[idxRow + 0, baseIdx + 6 + k] = ix;
//                    J[idxRow + 1, baseIdx + 6 + k] = iy;
//                }

//                idxRow += 2;
//            }

//            // 2) First derivative continuity at joints (evaluations at t=1 (left) and t=0 (right))
//            // rows idxRow .. idxRow+7
//            for (var joint = 1; joint <= 4; joint++)
//            {
//                var segL = joint - 1;
//                var segR = joint;

//                // Evaluate P and necessary values at t=1 for left and t=0 for right
//                // We'll compute partial derivatives of r' = w P evaluated at those t values
//                // Fill two rows (x and y)
//                var baseL = segL * 9;
//                var baseR = segR * 9;

//                var tL = 1.0;
//                var tR = 0.0;

//                // Left seg
//                var uL = GetCoeffs(x, segL, 0);
//                var vL = GetCoeffs(x, segL, 3);
//                var wL = GetCoeffs(x, segL, 6);
//                double uLt = EvalQuad(uL, tL), vLt = EvalQuad(vL, tL), wLt = EvalQuad(wL, tL);

//                // Right seg
//                var uR = GetCoeffs(x, segR, 0);
//                var vR = GetCoeffs(x, segR, 3);
//                var wR = GetCoeffs(x, segR, 6);
//                double uRt = EvalQuad(uR, tR), vRt = EvalQuad(vR, tR), wRt = EvalQuad(wR, tR);

//                // Partial derivatives for left segment at tL
//                // For a_k: d r'/d a_k = w(t) * [2 u(t) t^k, 2 v(t) t^k]
//                for (var k = 0; k <= 2; k++)
//                {
//                    var tkL = Math.Pow(tL, k);
//                    var pxda_x = wLt * 2.0 * uLt * tkL;
//                    var pxda_y = wLt * 2.0 * vLt * tkL;
//                    J[idxRow + 0, baseL + k] = pxda_x;
//                    J[idxRow + 1, baseL + k] = pxda_y;
//                }

//                // For b_k: d r'/d b_k = w(t) * [-2 v t^k, 2 u t^k]
//                for (var k = 0; k <= 2; k++)
//                {
//                    var tkL = Math.Pow(tL, k);
//                    var valx = wLt * (-2.0 * vLt * tkL);
//                    var valy = wLt * (2.0 * uLt * tkL);
//                    J[idxRow + 0, baseL + 3 + k] = valx;
//                    J[idxRow + 1, baseL + 3 + k] = valy;
//                }

//                // For c_k: d r'/d c_k = t^k * [Px, Py]
//                var Pleft = new[] { uLt * uLt - vLt * vLt, 2.0 * uLt * vLt };
//                for (var k = 0; k <= 2; k++)
//                {
//                    var tkL = Math.Pow(tL, k);
//                    J[idxRow + 0, baseL + 6 + k] = tkL * Pleft[0];
//                    J[idxRow + 1, baseL + 6 + k] = tkL * Pleft[1];
//                }

//                // Partial derivatives for right segment at tR, subtract because residual is r'_L(1) - r'_R(0)
//                for (var k = 0; k <= 2; k++)
//                {
//                    var tkR = Math.Pow(tR, k);
//                    var valx = wRt * 2.0 * uRt * tkR;
//                    var valy = wRt * 2.0 * vRt * tkR;
//                    J[idxRow + 0, baseR + k] = -valx;
//                    J[idxRow + 1, baseR + k] = -valy;
//                }

//                for (var k = 0; k <= 2; k++)
//                {
//                    var tkR = Math.Pow(tR, k);
//                    var valx = wRt * (-2.0 * vRt * tkR);
//                    var valy = wRt * (2.0 * uRt * tkR);
//                    J[idxRow + 0, baseR + 3 + k] = -valx;
//                    J[idxRow + 1, baseR + 3 + k] = -valy;
//                }

//                var Pright = new[] { uRt * uRt - vRt * vRt, 2.0 * uRt * vRt };
//                for (var k = 0; k <= 2; k++)
//                {
//                    var tkR = Math.Pow(tR, k);
//                    J[idxRow + 0, baseR + 6 + k] = -tkR * Pright[0];
//                    J[idxRow + 1, baseR + 6 + k] = -tkR * Pright[1];
//                }

//                idxRow += 2;
//            }

//            // 3) Second derivative continuity rows
//            for (var joint = 1; joint <= 4; joint++)
//            {
//                var segL = joint - 1;
//                var segR = joint;
//                var baseL = segL * 9;
//                var baseR = segR * 9;
//                var tL = 1.0;
//                var tR = 0.0;

//                // get u,v,w and derivatives for left
//                double[] uL = GetCoeffs(x, segL, 0), vL = GetCoeffs(x, segL, 3), wL = GetCoeffs(x, segL, 6);
//                double uLt = EvalQuad(uL, tL), vLt = EvalQuad(vL, tL), wLt = EvalQuad(wL, tL);
//                double uLpt = EvalQuadDerivative(uL, tL), vLpt = EvalQuadDerivative(vL, tL), wLpt = EvalQuadDerivative(wL, tL);
                
//                // P and P'
//                var PxL = uLt * uLt - vLt * vLt;
//                var PyL = 2.0 * uLt * vLt;
//                var PxtL = 2.0 * (uLt * uLpt - vLt * vLpt);
//                var PytL = 2.0 * (uLpt * vLt + uLt * vLpt);

//                // right
//                double[] uR = GetCoeffs(x, segR, 0), vR = GetCoeffs(x, segR, 3), wR = GetCoeffs(x, segR, 6);
//                double uRt = EvalQuad(uR, tR), vRt = EvalQuad(vR, tR), wRt = EvalQuad(wR, tR);
//                double uRpt = EvalQuadDerivative(uR, tR), vRpt = EvalQuadDerivative(vR, tR), wRpt = EvalQuadDerivative(wR, tR);
//                var PxR = uRt * uRt - vRt * vRt;
//                var PyR = 2.0 * uRt * vRt;
//                var PxtR = 2.0 * (uRt * uRpt - vRt * vRpt);
//                var PytR = 2.0 * (uRpt * vRt + uRt * vRpt);

//                // For left segment, derivative of r'' = w' P + w P'
//                // We compute partial derivatives wrt each coefficient using product rule
//                // For a_k:
//                for (var k = 0; k <= 2; k++)
//                {
//                    var dukt = Math.Pow(tL, k);               // du/da_k
//                    var dukpt = (k == 0) ? 0.0 : k * Math.Pow(tL, k - 1); // du'/da_k
                    
//                    // dP/da_k:
//                    var dPxDa = 2.0 * uLt * dukt;
//                    var dPyDa = 2.0 * vLt * dukt;
                    
//                    // dP'/da_k = 2[(du/da_k) u' + u (du'/da_k) - (dv/da_k) v' - v (dv'/da_k)] for x component
//                    double dvkt = 0.0, dvkpt = 0.0; // dv/da_k = 0
//                    var dPxtDa = 2.0 * (dukt * uLpt + uLt * dukpt - 0.0);
//                    var dPytDa = 2.0 * (dukpt * vLt + uLt * 0.0 + uLpt * dukt + vLt * dukpt) - 0.0; 
                    
//                    // above formula can be simplified: for Py' expression use 2(u' v + u v'), derivative wrt a_k is:
//                    // d/d a_k [2(u' v + u v')] = 2( (du'/da_k) v + u' (dv/da_k) + (du/da_k) v' + u (dv'/da_k) )
//                    // dv/da_k = 0 and dv'/da_k = 0 since v coeffs differ; so reduces to 2(du'/da_k * v + (du/da_k) * v')
//                    // Implement accordingly to avoid error:
//                    dPytDa = 2.0 * (dukpt * vLt + dukt * vLpt);

//                    // Now apply product rule:
//                    // d r'' / d a_k = (dw'/d a_k) * P + w' * (dP/da_k) + (dw/da_k) * P' + w * (dP'/da_k)
//                    // but dw/da_k = 0 (w coefficients are separate), dw'/da_k = 0
//                    // So only terms: w' * dP/da_k + w * dP'/da_k
//                    var dr2XDa = wLpt * dPxDa + wLt * dPxtDa;
//                    var dr2YDa = wLpt * dPyDa + wLt * dPytDa;

//                    J[idxRow + 0, baseL + k] = dr2XDa;
//                    J[idxRow + 1, baseL + k] = dr2YDa;
//                }

//                // For b_k (v coefficients)
//                for (var k = 0; k <= 2; k++)
//                {
//                    var dvkt = Math.Pow(tL, k);
//                    var dvkpt = (k == 0) ? 0.0 : k * Math.Pow(tL, k - 1);

//                    var dPxDb = -2.0 * vLt * dvkt;
//                    var dPyDb = 2.0 * uLt * dvkt;

//                    var dPxtDb = 2.0 * ( - (dvkt * vLpt) - vLt * dvkpt ); // since u terms drop out
                    
//                    // better to compute using general formula:
//                    // dP'x = 2( (du/ dbk)*u' + u*(du'/dbk) - (dv/dbk)*v' - v*(dv'/dbk) )
//                    // du/dbk = 0, so first two terms zero, second two are - (dv/dbk)*v' - v*(dv'/dbk)
//                    dPxtDb = 2.0 * ( - (dvkt * vLpt) - vLt * dvkpt );

//                    // dP'y: derivative of 2(u' v + u v') wrt b_k: 2( u' (dv/dbk) + (du/dbk)*v' + u*(dv'/dbk) + (du'/dbk)*v )
//                    // reduces to 2( uL * dvkpt + uLpt * dvkt )
//                    var dPytDb = 2.0 * (uLt * dvkpt + uLpt * dvkt);

//                    var dr2XDb = wLpt * dPxDb + wLt * dPxtDb;
//                    var dr2YDb = wLpt * dPyDb + wLt * dPytDb;

//                    J[idxRow + 0, baseL + 3 + k] = dr2XDb;
//                    J[idxRow + 1, baseL + 3 + k] = dr2YDb;
//                }

//                // For c_k (w coefficients)
//                for (var k = 0; k <= 2; k++)
//                {
//                    // dw/ dc_k = t^k, dw'/dc_k = k t^{k-1}
//                    var dw = Math.Pow(tL, k);
//                    var dwp = (k == 0) ? 0.0 : k * Math.Pow(tL, k - 1);

//                    // d r'' / d c_k = (dw') P + w' (0) + (dw) P' + w (0) because P and P' don't depend on c
//                    // So dr2 = dwp * P + dw * P'
//                    var dr2XDc = dwp * PxL + dw * PxtL;
//                    var dr2YDc = dwp * PyL + dw * PytL;

//                    J[idxRow + 0, baseL + 6 + k] = dr2XDc;
//                    J[idxRow + 1, baseL + 6 + k] = dr2YDc;
//                }

//                // For right segment, subtract derivatives evaluated at t=0
//                for (var k = 0; k <= 2; k++)
//                {
//                    // a_k for right
//                    var dukt = Math.Pow(tR, k);
//                    var dukpt = (k == 0) ? 0.0 : k * Math.Pow(tR, k - 1);

//                    var dPxDa = 2.0 * uRt * dukt;
//                    var dPyDa = 2.0 * vRt * dukt;
//                    var dPxtDa = 2.0 * (dukt * uRpt + uRt * dukpt);
//                    var dPytDa = 2.0 * (dukpt * vRt + dukt * vRpt);

//                    var dr2XDa = wRpt * dPxDa + wRt * dPxtDa;
//                    var dr2YDa = wRpt * dPyDa + wRt * dPytDa;

//                    J[idxRow + 0, baseR + k] = -dr2XDa;
//                    J[idxRow + 1, baseR + k] = -dr2YDa;
//                }

//                for (var k = 0; k <= 2; k++)
//                {
//                    var dvkt = Math.Pow(tR, k);
//                    var dvkpt = (k == 0) ? 0.0 : k * Math.Pow(tR, k - 1);

//                    var dPxDb = -2.0 * vRt * dvkt;
//                    var dPyDb = 2.0 * uRt * dvkt;
//                    var dPxtDb = 2.0 * ( - (dvkt * vRpt) - vRt * dvkpt );
//                    var dPytDb = 2.0 * (uRt * dvkpt + uRpt * dvkt);

//                    var dr2XDb = wRpt * dPxDb + wRt * dPxtDb;
//                    var dr2YDb = wRpt * dPyDb + wRt * dPytDb;

//                    J[idxRow + 0, baseR + 3 + k] = -dr2XDb;
//                    J[idxRow + 1, baseR + 3 + k] = -dr2YDb;
//                }

//                for (var k = 0; k <= 2; k++)
//                {
//                    var dw = Math.Pow(tR, k);
//                    var dwp = (k == 0) ? 0.0 : k * Math.Pow(tR, k - 1);

//                    var dr2XDc = dwp * PxR + dw * PxtR;
//                    var dr2YDc = dwp * PyR + dw * PytR;

//                    J[idxRow + 0, baseR + 6 + k] = -dr2XDc;
//                    J[idxRow + 1, baseR + 6 + k] = -dr2YDc;
//                }

//                idxRow += 2;
//            }

//            // 4) scale-fix rows: derivative of w(0) - 1 = 0 -> derivative wrt c0 is 1
//            for (var seg = 0; seg < Segments; seg++)
//            {
//                var base1 = seg * 9;
//                J[idxRow, base1 + 6 + 0] = 1.0; // derivative wrt w0
                
//                // other columns remain zero
//                idxRow++;
//            }

//            return J;
//        }

//        // -------------------------
//        // Residual evaluator (same as before)
//        // -------------------------
//        private static void EvaluateResiduals(Vector<double> x, List<double[]> joints, Vector<double> r)
//        {
//            var idx = 0;

//            // 1) Position integrals
//            for (var seg = 0; seg < Segments; seg++)
//            {
//                var u = GetCoeffs(x, seg, 0);
//                var v = GetCoeffs(x, seg, 3);
//                var w = GetCoeffs(x, seg, 6);

//                var integral = Integrate_wP(u, v, w);
//                var delta = new[] { joints[seg + 1][0] - joints[seg][0], joints[seg + 1][1] - joints[seg][1] };
//                r[idx++] = integral[0] - delta[0];
//                r[idx++] = integral[1] - delta[1];
//            }

//            // 2) first derivative continuity
//            for (var joint = 1; joint <= 4; joint++)
//            {
//                int segL = joint - 1, segR = joint;
//                var rL = Eval_rPrime_at(x, segL, 1.0);
//                var rR = Eval_rPrime_at(x, segR, 0.0);
//                r[idx++] = rL[0] - rR[0];
//                r[idx++] = rL[1] - rR[1];
//            }

//            // 3) second derivative continuity
//            for (var joint = 1; joint <= 4; joint++)
//            {
//                var segL = joint - 1;
//                var segR = joint;
//                var r2L = Eval_rDoublePrime_at(x, segL, 1.0);
//                var r2R = Eval_rDoublePrime_at(x, segR, 0.0);
//                r[idx++] = r2L[0] - r2R[0];
//                r[idx++] = r2L[1] - r2R[1];
//            }

//            // 4) scale fix
//            for (var seg = 0; seg < Segments; seg++)
//            {
//                var w = GetCoeffs(x, seg, 6);
//                r[idx++] = w[0] - 1.0;
//            }
//        }

//        private static double[] GetCoeffs(Vector<double> x, int seg, int offsetInSeg)
//        {
//            var baseIdx = seg * 9 + offsetInSeg;
//            return [x[baseIdx + 0], x[baseIdx + 1], x[baseIdx + 2]];
//        }

//        private static double[] Eval_rPrime_at(Vector<double> x, int seg, double t)
//        {
//            var u = GetCoeffs(x, seg, 0);
//            var v = GetCoeffs(x, seg, 3);
//            var w = GetCoeffs(x, seg, 6);

//            var ut = EvalQuad(u, t);
//            var vt = EvalQuad(v, t);
//            var wt = EvalQuad(w, t);
//            var px = ut * ut - vt * vt;
//            var py = 2.0 * ut * vt;
//            return [wt * px, wt * py];
//        }

//        private static double[] Eval_rDoublePrime_at(Vector<double> x, int seg, double t)
//        {
//            var u = GetCoeffs(x, seg, 0);
//            var v = GetCoeffs(x, seg, 3);
//            var w = GetCoeffs(x, seg, 6);

//            var ut = EvalQuad(u, t);
//            var vt = EvalQuad(v, t);
//            var upt = EvalQuadDerivative(u, t);
//            var vpt = EvalQuadDerivative(v, t);
//            var wt = EvalQuad(w, t);
//            var wpt = EvalQuadDerivative(w, t);

//            var Px = ut * ut - vt * vt;
//            var Py = 2.0 * ut * vt;
//            var Pxt = 2.0 * (ut * upt - vt * vpt);
//            var Pyt = 2.0 * (upt * vt + ut * vpt);

//            var r2x = wpt * Px + wt * Pxt;
//            var r2y = wpt * Py + wt * Pyt;
//            return [r2x, r2y];
//        }

//        private static double EvalQuad(double[] c, double t)
//        {
//            return c[0] + c[1] * t + c[2] * t * t;
//        }

//        private static double EvalQuadDerivative(double[] c, double t)
//        {
//            return c[1] + 2.0 * c[2] * t;
//        }

//        private static double[] Integrate_wP(double[] u, double[] v, double[] w)
//        {
//            // Exact polynomial expansion integration (same approach as analytic Jacobian)
//            var uc = u; var vc = v; var wc = w;

//            var Pxc = new double[5];
//            var Pyc = new double[5];

//            for (var i = 0; i <= 2; i++)
//                for (var j = 0; j <= 2; j++)
//                    Pxc[i + j] += uc[i] * uc[j];

//            for (var i = 0; i <= 2; i++)
//                for (var j = 0; j <= 2; j++)
//                    Pxc[i + j] -= vc[i] * vc[j];

//            for (var i = 0; i <= 2; i++)
//                for (var j = 0; j <= 2; j++)
//                    Pyc[i + j] += 2.0 * uc[i] * vc[j];

//            var Ixc = new double[7];
//            var Iyc = new double[7];

//            for (var i = 0; i <= 2; i++)
//                for (var j = 0; j <= 4; j++)
//                {
//                    Ixc[i + j] += wc[i] * Pxc[j];
//                    Iyc[i + j] += wc[i] * Pyc[j];
//                }

//            double ix = 0.0, iy = 0.0;
//            for (var m = 0; m < Ixc.Length; m++)
//            {
//                ix += Ixc[m] / (m + 1.0);
//                iy += Iyc[m] / (m + 1.0);
//            }

//            return [ix, iy];
//        }

//        private static List<PhSegmentGenerators> UnpackSolution(Vector<double> sol)
//        {
//            var result = new List<PhSegmentGenerators>();
//            for (var seg = 0; seg < Segments; seg++)
//            {
//                var g = new PhSegmentGenerators();
//                var baseIdx = seg * 9;
//                for (var k = 0; k < 3; k++) g.U[k] = sol[baseIdx + k];
//                for (var k = 0; k < 3; k++) g.V[k] = sol[baseIdx + 3 + k];
//                for (var k = 0; k < 3; k++) g.W[k] = sol[baseIdx + 6 + k];
//                result.Add(g);
//            }
//            return result;
//        }
//    }
//}
