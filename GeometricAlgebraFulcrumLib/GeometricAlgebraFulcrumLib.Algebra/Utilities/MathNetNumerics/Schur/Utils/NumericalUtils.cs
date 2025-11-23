using System.Numerics;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.Utilities.MathNetNumerics.Schur.Utils
{
    /// <summary>
    /// Numerical utility functions for safe and stable computations.
    /// Corresponds to Julia's util.jl file.
    /// </summary>
    public static class NumericalUtils
    {
        /// <summary>
        /// matrixA "safe" version of the smallest floating-point number, such that 1/sfmin does not overflow.
        /// </summary>
        public static double SafeMin()
        {
            var sfmin = double.Epsilon;
            var small = 1.0 / double.MaxValue;
            if (small >= sfmin)
            {
                sfmin = small * (1.0 + Precision.DoublePrecision);
            }
            return sfmin;
        }

        /// <summary>
        /// Complex version of absolute value that uses sum of absolute values of real and imaginary parts.
        /// This corresponds to Julia's abs1 function.
        /// </summary>
        public static double Abs1(Complex z)
        {
            return Math.Abs(z.Real) + Math.Abs(z.Imaginary);
        }

        /// <summary>
        /// Real version of abs1 (just regular absolute value).
        /// </summary>
        public static double Abs1(double x)
        {
            return Math.Abs(x);
        }

        /// <summary>
        /// Multiply an array by a real number cto/cfrom expressed as a ratio,
        /// computing without over/underflow where possible.
        /// Translation of LAPACK's DLASCL.
        /// </summary>
        public static void SafeScale<T>(Matrix<T> matrixA, double cfrom, double cto) where T : struct, IEquatable<T>, IFormattable
        {
            if (double.IsNaN(cto))
                throw new ArgumentException("cto must be a valid number");
            if (double.IsNaN(cfrom) || cfrom == 0.0)
                throw new ArgumentException("cfrom must be a valid nonzero number");

            var smlnum = SafeMin();
            var bignum = 1.0 / smlnum;
            var cfromc = cfrom;
            var ctoc = cto;
            var done = false;

            while (!done)
            {
                var cfrom1 = cfromc * smlnum;
                double cto1, mul;

                if (cfrom1 == cfromc)
                {
                    // cfromc is ±∞; multiplier is signed 0 or NaN
                    mul = ctoc / cfromc;
                    done = true;
                }
                else
                {
                    cto1 = ctoc / bignum;
                    if (cto1 == ctoc)
                    {
                        // ctoc is 0 or ±∞, so is correct multiplier
                        mul = ctoc;
                        done = true;
                        cfromc = 1.0;
                    }
                    else if (Math.Abs(cfrom1) > Math.Abs(ctoc) && ctoc != 0)
                    {
                        mul = smlnum;
                        done = false;
                        cfromc = 1.0;
                    }
                    else if (Math.Abs(cto1) > Math.Abs(cfromc))
                    {
                        mul = bignum;
                        done = false;
                        cfromc = cfrom1;
                    }
                    else
                    {
                        mul = ctoc / cfromc;
                        done = true;
                    }
                }

                matrixA.MapInplace(x => MultiplyByScalar(x, mul));
            }
        }

        private static T MultiplyByScalar<T>(T value, double scalar) where T : struct, IEquatable<T>, IFormattable
        {
            if (typeof(T) == typeof(double))
                return (T)(object)((double)(object)value * scalar);
            if (typeof(T) == typeof(Complex))
            {
                var c = (Complex)(object)value;
                return (T)(object)(c * scalar);
            }
            throw new NotSupportedException($"Type {typeof(T)} is not supported");
        }

        /// <summary>
        /// Protected hypot function for 3 arguments to avoid overflow.
        /// Translation of Julia's _hypot3.
        /// </summary>
        public static double Hypot3(double x, double y, double z)
        {
            var xa = Math.Abs(x);
            var ya = Math.Abs(y);
            var za = Math.Abs(z);
            var w = Math.Max(xa, Math.Max(ya, za));
            
            if (w == 0.0) return 0.0;
            
            var rw = 1.0 / w;
            return w * Math.Sqrt((rw * xa) * (rw * xa) + (rw * ya) * (rw * ya) + (rw * za) * (rw * za));
        }

        /// <summary>
        /// Protected norm computation using scaling to avoid overflow.
        /// Translation of Julia's _norm2.
        /// </summary>
        public static double SafeNorm2(MathNet.Numerics.LinearAlgebra.Vector<Complex> x)
        {
            var n = x.Count;
            if (n < 1) return 0.0;
            if (n == 1) return x[0].Magnitude;

            var scale = 0.0;
            var ssq = 0.0;

            for (var i = 0; i < n; i++)
            {
                var xi = x[i];
                var xr = Math.Abs(xi.Real);
                var xi_imag = Math.Abs(xi.Imaginary);

                if (xr != 0.0)
                {
                    if (scale < xr)
                    {
                        ssq = 1.0 + ssq * (scale / xr) * (scale / xr);
                        scale = xr;
                    }
                    else
                    {
                        ssq += (xr / scale) * (xr / scale);
                    }
                }

                if (xi_imag != 0.0)
                {
                    if (scale < xi_imag)
                    {
                        ssq = 1.0 + ssq * (scale / xi_imag) * (scale / xi_imag);
                        scale = xi_imag;
                    }
                    else
                    {
                        ssq += (xi_imag / scale) * (xi_imag / scale);
                    }
                }
            }

            return scale * Math.Sqrt(ssq);
        }

        /// <summary>
        /// Real version of SafeNorm2.
        /// </summary>
        public static double SafeNorm2(MathNet.Numerics.LinearAlgebra.Vector<double> x)
        {
            var n = x.Count;
            if (n < 1) return 0.0;
            if (n == 1) return Math.Abs(x[0]);

            var scale = 0.0;
            var ssq = 0.0;

            for (var i = 0; i < n; i++)
            {
                var xi = Math.Abs(x[i]);
                if (xi != 0.0)
                {
                    if (scale < xi)
                    {
                        ssq = 1.0 + ssq * (scale / xi) * (scale / xi);
                        scale = xi;
                    }
                    else
                    {
                        ssq += (xi / scale) * (xi / scale);
                    }
                }
            }

            return scale * Math.Sqrt(ssq);
        }

        /// <summary>
        /// Update scale and sumsq such that (scale^2 * sumsq) is increased by sum of |x[j]|^2.
        /// Based on LAPACK's ZLASSQ.
        /// </summary>
        public static (double scale, double sumsq) UpdateSumOfSquares(MathNet.Numerics.LinearAlgebra.Vector<Complex> x, double scale, double sumsq)
        {
            for (var i = 0; i < x.Count; i++)
            {
                var xr = Math.Abs(x[i].Real);
                var xi = Math.Abs(x[i].Imaginary);

                if (xr > 0 || double.IsNaN(xr))
                {
                    if (scale < xr)
                    {
                        sumsq = 1.0 + sumsq * (scale / xr) * (scale / xr);
                        scale = xr;
                    }
                    else
                    {
                        sumsq += (xr / scale) * (xr / scale);
                    }
                }

                if (xi > 0 || double.IsNaN(xi))
                {
                    if (scale < xi)
                    {
                        sumsq = 1.0 + sumsq * (scale / xi) * (scale / xi);
                        scale = xi;
                    }
                    else
                    {
                        sumsq += (xi / scale) * (xi / scale);
                    }
                }
            }

            return (scale, sumsq);
        }

        /// <summary>
        /// Scale matrix based on its norm to improve numerical stability.
        /// Returns (scaleA, cscale, anrm) tuple.
        /// </summary>
        public static (bool scaleA, double cscale, double anrm) ScaleMatrix<T>(Matrix<T> matrixA) 
            where T : struct, IEquatable<T>, IFormattable
        {
            var smlnum = Math.Sqrt(SafeMin()) / Precision.DoublePrecision;
            var bignum = 1.0 / smlnum;
            var anrm = matrixA.InfinityNorm();
            var scaleA = false;
            var cscale = 1.0;

            if (anrm > 0 && anrm < smlnum)
            {
                scaleA = true;
                cscale = smlnum;
            }
            else if (anrm > bignum)
            {
                scaleA = true;
                cscale = bignum;
            }

            if (scaleA)
            {
                SafeScale(matrixA, anrm, cscale);
            }

            return (scaleA, cscale, anrm);
        }
    }
}