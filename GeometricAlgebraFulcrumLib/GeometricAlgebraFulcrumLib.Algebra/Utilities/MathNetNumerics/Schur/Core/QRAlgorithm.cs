using System.Numerics;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.MathNetNumerics.Schur.Utils;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.Utilities.MathNetNumerics.Schur.Core
{
    /// <summary>
    /// QR algorithm implementations for Schur decomposition.
    /// Translation of Julia's single and double shift QR methods.
    /// </summary>
    public static class QRAlgorithm
    {
        /// <summary>
        /// Single-shift QR iteration for complex Hessenberg matrices.
        /// Translation of Julia's singleShiftQR! function.
        /// </summary>
        public static void SingleShiftQR(Matrix<Complex> H, Matrix<Complex>? Q, Complex shift,
                                        int istart, int iend, MathNet.Numerics.LinearAlgebra.Vector<Complex> v)
        {
            var n = H.RowCount;
            var ulp = Precision.DoublePrecision;

            // Find the best starting point for the QR sweep
            var istart1 = -1;
            var h11s = Complex.Zero;
            var h21 = 0.0;
            var flag = false;

            // Look for two consecutive small subdiagonals
            for (var mm = iend - 1; mm >= istart + 1; mm--)
            {
                var h11 = H[mm, mm];
                var h22 = H[mm + 1, mm + 1];
                h11s = h11 - shift;
                h21 = H[mm + 1, mm].Real; // Should be real for Hessenberg form

                var s = NumericalUtils.Abs1(h11s) + Math.Abs(h21);
                h11s /= s;
                h21 /= s;
                v[0] = h11s;
                v[1] = new Complex(h21, 0);

                var h10 = H[mm, mm - 1].Real;
                if (Math.Abs(h10) * Math.Abs(h21) <= ulp * (NumericalUtils.Abs1(h11s) * (NumericalUtils.Abs1(h11) + NumericalUtils.Abs1(h22))))
                {
                    istart1 = mm;
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                istart1 = istart;
                var h11 = H[istart, istart];
                var h22 = H[istart + 1, istart + 1];
                h11s = h11 - shift;
                h21 = H[istart + 1, istart].Real;
                var s = NumericalUtils.Abs1(h11s) + Math.Abs(h21);
                h11s /= s;
                h21 /= s;
                v[0] = h11s;
                v[1] = new Complex(h21, 0);
            }

            // Perform QR sweep from istart1 to iend
            for (var k = istart1; k <= iend - 1; k++)
            {
                if (k > istart1)
                {
                    // Prepare to chase bulge
                    v[0] = H[k, k - 1];
                    v[1] = H[k + 1, k - 1];
                }

                // Compute Householder reflector
                var tau1 = HouseholderReflector.ComputeReflector(v);

                if (k > istart1)
                {
                    H[k, k - 1] = v[0];
                    H[k + 1, k - 1] = Complex.Zero;
                }

                var v2 = v[1];
                var tau2 = tau1.Real * v2;

                // Apply reflector to H from left: H := P^H * H
                for (var j = k; j < n; j++)
                {
                    var ss = Complex.Conjugate(tau1) * H[k, j] + tau2 * H[k + 1, j];
                    H[k, j] -= ss;
                    H[k + 1, j] -= ss * v2;
                }

                // Apply reflector to H from right: H := H * P
                for (var j = 0; j <= Math.Min(k + 2, iend); j++)
                {
                    var ss = tau1 * H[j, k] + tau2 * H[j, k + 1];
                    H[j, k] -= ss;
                    H[j, k + 1] -= ss * Complex.Conjugate(v2);
                }

                // Update Q if provided: Q := Q * P
                if (Q != null)
                {
                    for (var j = 0; j < n; j++)
                    {
                        var ss = tau1 * Q[j, k] + tau2 * Q[j, k + 1];
                        Q[j, k] -= ss;
                        Q[j, k + 1] -= ss * Complex.Conjugate(v2);
                    }
                }

                // Special scaling to ensure reality of subdiagonal
                if (k == istart1 && istart1 > istart)
                {
                    var t = Complex.One - tau1;
                    t /= t.Magnitude;
                    H[istart1 + 1, istart1] *= Complex.Conjugate(t);
                    
                    if (istart1 + 2 <= iend)
                    {
                        H[istart1 + 2, istart1 + 1] *= t;
                    }

                    for (var j = istart1; j <= iend; j++)
                    {
                        if (j != istart1 + 1)
                        {
                            if (n > j)
                            {
                                for (var i = j + 1; i < n; i++)
                                {
                                    H[j, i] *= t;
                                }
                            }
                            for (var i = 0; i < j; i++)
                            {
                                H[i, j] *= Complex.Conjugate(t);
                            }
                            if (Q != null)
                            {
                                for (var i = 0; i < n; i++)
                                {
                                    Q[i, j] *= Complex.Conjugate(t);
                                }
                            }
                        }
                    }
                }
            }

            // Ensure reality of tail element
            var tail = H[iend, iend - 1];
            if (tail.Imaginary != 0.0)
            {
                var rt = tail.Magnitude;
                H[iend, iend - 1] = new Complex(rt, 0);
                var t = tail / rt;

                if (n > iend)
                {
                    for (var i = iend + 1; i < n; i++)
                    {
                        H[iend, i] *= Complex.Conjugate(t);
                    }
                }
                
                for (var i = 0; i < iend; i++)
                {
                    H[i, iend] *= t;
                }
                
                if (Q != null)
                {
                    for (var i = 0; i < n; i++)
                    {
                        Q[i, iend] *= t;
                    }
                }
            }
        }

        /// <summary>
        /// Double-shift QR iteration for real Hessenberg matrices.
        /// Translation of Julia's doubleShiftQR! function.
        /// </summary>
        public static void DoubleShiftQR(Matrix<double> H, Matrix<double>? Q, Complex shift1, Complex shift2,
                                        int istart, int iend, MathNet.Numerics.LinearAlgebra.Vector<double> v)
        {
            var n = H.RowCount;
            double r1r = shift1.Real, r1i = shift1.Imaginary;
            double r2r = shift2.Real, r2i = shift2.Imaginary;
            
            v.Clear();

            // Look for two consecutive small subdiagonals
            var mx = istart;
            for (var m = iend - 2; m >= istart; m--)
            {
                // Determine whether starting double-shift QR iteration
                // at row m would make H[m,m-1] negligible.
                var H21s = H[m + 1, m];
                var s = Math.Abs(H[m, m] - r2r) + Math.Abs(r2i) + Math.Abs(H21s);
                H21s /= s;

                v[0] = H21s * H[m, m + 1] +
                       (H[m, m] - r1r) * ((H[m, m] - r2r) / s) - r1i * (r2i / s);
                v[1] = H21s * (H[m, m] + H[m + 1, m + 1] - r1r - r2r);
                v[2] = H21s * H[m + 2, m + 1];

                var vs = Math.Abs(v[0]) + Math.Abs(v[1]) + Math.Abs(v[2]);
                v[0] /= vs;
                v[1] /= vs;
                v[2] /= vs;

                if (m > istart && 
                    Math.Abs(H[m, m - 1]) * (Math.Abs(v[1]) + Math.Abs(v[2])) <=
                    Precision.DoublePrecision * Math.Abs(v[0]) *
                    (Math.Abs(H[m - 1, m - 1]) + Math.Abs(H[m, m]) + Math.Abs(H[m + 1, m + 1])))
                {
                    mx = m;
                    break;
                }
            }

            // Perform QR sweep from mx to iend
            for (var k = mx; k <= iend - 1; k++)
            {
                // Determine order of reflector
                var nr = Math.Min(3, iend - k + 1);
                
                if (k > mx)
                {
                    // Get column k-1 for bulge chasing
                    for (var ii = 0; ii < nr; ii++)
                    {
                        v[ii] = H[k + ii, k - 1];
                    }
                }

                // Compute Householder reflector for nr elements
                var vSubVector = MathNet.Numerics.LinearAlgebra.Double.DenseVector.Create(nr, i => v[i]);
                var tau1 = HouseholderReflector.ComputeReflector(vSubVector);
                
                // Copy back the modified vector
                for (var ii = 0; ii < nr; ii++)
                {
                    v[ii] = vSubVector[ii];
                }

                if (k > mx)
                {
                    H[k, k - 1] = v[0];
                    H[k + 1, k - 1] = 0.0;
                    if (k < iend - 1)
                    {
                        H[k + 2, k - 1] = 0.0;
                    }
                }
                else if (mx > istart)
                {
                    // Avoid problems when v[1] and v[2] underflow
                    H[k, k - 1] *= (1.0 - tau1);
                }

                var v2 = v[1];
                var tau2 = tau1 * v2;

                if (nr == 3)
                {
                    var v3 = v[2];
                    var tau3 = tau1 * v3;

                    // Apply G from left; transform H[:,k:n]
                    for (var j = k; j < n; j++)
                    {
                        var ss = H[k, j] + v2 * H[k + 1, j] + v3 * H[k + 2, j];
                        H[k, j] -= ss * tau1;
                        H[k + 1, j] -= ss * tau2;
                        H[k + 2, j] -= ss * tau3;
                    }

                    // Apply G from right; transform H[0:min(k+3,iend),:]
                    for (var j = 0; j <= Math.Min(k + 3, iend); j++)
                    {
                        var ss = H[j, k] + v2 * H[j, k + 1] + v3 * H[j, k + 2];
                        H[j, k] -= ss * tau1;
                        H[j, k + 1] -= ss * tau2;
                        H[j, k + 2] -= ss * tau3;
                    }

                    // Update Q if provided
                    if (Q != null)
                    {
                        for (var j = 0; j < Q.RowCount; j++)
                        {
                            var ss = Q[j, k] + v2 * Q[j, k + 1] + v3 * Q[j, k + 2];
                            Q[j, k] -= ss * tau1;
                            Q[j, k + 1] -= ss * tau2;
                            Q[j, k + 2] -= ss * tau3;
                        }
                    }
                }
                else if (nr == 2)
                {
                    // Apply G from left; transform H[:,k:n]
                    for (var j = k; j < n; j++)
                    {
                        var ss = H[k, j] + v2 * H[k + 1, j];
                        H[k, j] -= ss * tau1;
                        H[k + 1, j] -= ss * tau2;
                    }

                    // Apply G from right; transform H[0:iend,:]
                    for (var j = 0; j <= iend; j++)
                    {
                        var ss = H[j, k] + v2 * H[j, k + 1];
                        H[j, k] -= ss * tau1;
                        H[j, k + 1] -= ss * tau2;
                    }

                    // Update Q if provided
                    if (Q != null)
                    {
                        for (var j = 0; j < Q.RowCount; j++)
                        {
                            var ss = Q[j, k] + v2 * Q[j, k + 1];
                            Q[j, k] -= ss * tau1;
                            Q[j, k + 1] -= ss * tau2;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Compute 2x2 Schur form using Givens rotations.
        /// Translation of Julia's _gs2x2! function.
        /// </summary>
        public static (Complex eig1, Complex eig2) Compute2x2Schur(Matrix<double> H2, int index)
        {
            double a = H2[0, 0], b = H2[0, 1], c = H2[1, 0], d = H2[1, 1];
            double cs, sn;
            var half = 0.5;
            var small = 4 * Precision.DoublePrecision; // Discriminant threshold

            if (c == 0.0)
            {
                cs = 1.0;
                sn = 0.0;
            }
            else if (b == 0.0)
            {
                // Swap rows/cols
                cs = 0.0;
                sn = 1.0;
                (a, d) = (d, a);
                b = -c;
                c = 0.0;
            }
            else if ((a - d == 0.0) && (b * c < 0.0))
            {
                // Nothing to do
                cs = 1.0;
                sn = 0.0;
            }
            else
            {
                var asubd = a - d;
                var p = half * asubd;
                var bcmax = Math.Max(Math.Abs(b), Math.Abs(c));
                var bcmis = Math.Min(Math.Abs(b), Math.Abs(c)) * Math.Sign(b) * Math.Sign(c);
                var scale = Math.Max(Math.Abs(p), bcmax);
                var z = (p / scale) * p + (bcmax / scale) * bcmis;

                if (z >= small)
                {
                    // Real eigenvalues
                    z = p + Math.Sqrt(scale) * Math.Sqrt(z) * Math.Sign(p);
                    a = d + z;
                    d -= (bcmax / z) * bcmis;
                    var tau = Math.Sqrt(c * c + z * z);
                    cs = z / tau;
                    sn = c / tau;
                    b -= c;
                    c = 0.0;
                }
                else
                {
                    // Complex or almost equal real eigenvalues
                    var sigma = b + c;
                    var tau = Math.Sqrt(sigma * sigma + asubd * asubd);
                    cs = Math.Sqrt(half * (1.0 + Math.Abs(sigma) / tau));
                    sn = -(p / (tau * cs)) * Math.Sign(sigma);

                    // Apply rotations
                    var aa = a * cs + b * sn;
                    var bb = -a * sn + b * cs;
                    var cc = c * cs + d * sn;
                    var dd = -c * sn + d * cs;
                    a = aa * cs + cc * sn;
                    b = bb * cs + dd * sn;
                    c = -aa * sn + cc * cs;
                    d = -bb * sn + dd * cs;
                    var midad = half * (a + d);
                    a = midad;
                    d = a;

                    if (c != 0.0)
                    {
                        if (b != 0.0)
                        {
                            if (b * c >= 0.0)
                            {
                                // Real eigenvalues
                                var sab = Math.Sqrt(Math.Abs(b));
                                var sac = Math.Sqrt(Math.Abs(c));
                                p = sab * sac * Math.Sign(c);
                                tau = 1.0 / Math.Sqrt(Math.Abs(b + c));
                                a = midad + p;
                                d = midad - p;
                                b -= c;
                                c = 0.0;
                                var cs1 = sab * tau;
                                var sn1 = sac * tau;
                                var temp_cs = cs * cs1 - sn * sn1;
                                sn = cs * sn1 + sn * cs1;
                                cs = temp_cs;
                            }
                        }
                        else
                        {
                            b = -c;
                            c = 0.0;
                            (cs, sn) = (-sn, cs);
                        }
                    }
                }
            }

            // Update the 2x2 block
            H2[0, 0] = a;
            H2[0, 1] = b;
            H2[1, 0] = c;
            H2[1, 1] = d;

            // Compute eigenvalues
            Complex w1, w2;
            if (c == 0.0)
            {
                w1 = new Complex(a, 0.0);
                w2 = new Complex(d, 0.0);
            }
            else
            {
                var rti = Math.Sqrt(Math.Abs(b)) * Math.Sqrt(Math.Abs(c));
                w1 = new Complex(a, rti);
                w2 = new Complex(d, -rti);
            }

            return (w1, w2);
        }
    }
}