using System.Numerics;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.Utilities.MathNetNumerics.Schur.Utils
{
    /// <summary>
    /// Householder reflection utilities for QR decomposition and Schur form computation.
    /// Translation of Julia's householder.jl file.
    /// </summary>
    public static class HouseholderReflector
    {
        /// <summary>
        /// Compute Householder reflector for complex vector.
        /// Translation of Julia's _reflector! function.
        /// Returns the reflection coefficient τ and modifies x in-place.
        /// </summary>
        public static Complex ComputeReflector(MathNet.Numerics.LinearAlgebra.Vector<Complex> x)
        {
            var n = x.Count;
            if (n < 1) return Complex.Zero;

            var sfmin = NumericalUtils.SafeMin() / Precision.DoublePrecision;
            var alpha = x[0];
            var alphaR = alpha.Real;
            var alphaI = alpha.Imaginary;

            // Compute norm of tail
            var xnorm = 0.0;
            for (var i = 1; i < n; i++)
            {
                var xi = x[i];
                xnorm += xi.Real * xi.Real + xi.Imaginary * xi.Imaginary;
            }
            xnorm = Math.Sqrt(xnorm);

            if (xnorm == 0.0 && alphaI == 0.0)
            {
                return Complex.Zero;
            }

            var beta = -Math.Sign(alphaR) * NumericalUtils.Hypot3(alphaR, alphaI, xnorm);
            var kount = 0;
            var smallBeta = Math.Abs(beta) < sfmin;

            if (smallBeta)
            {
                // Recompute xnorm and beta if needed for accuracy
                var rsfmin = 1.0 / sfmin;
                while (smallBeta && kount < 20)
                {
                    kount++;
                    for (var j = 1; j < n; j++)
                    {
                        x[j] *= rsfmin;
                    }
                    beta *= rsfmin;
                    alphaR *= rsfmin;
                    alphaI *= rsfmin;
                    smallBeta = Math.Abs(beta) < sfmin;
                }

                // Now beta ∈ [sfmin, 1]
                xnorm = 0.0;
                for (var i = 1; i < n; i++)
                {
                    var xi = x[i];
                    xnorm += xi.Real * xi.Real + xi.Imaginary * xi.Imaginary;
                }
                xnorm = Math.Sqrt(xnorm);
                alpha = new Complex(alphaR, alphaI);
                beta = -Math.Sign(alphaR) * NumericalUtils.Hypot3(alphaR, alphaI, xnorm);
            }

            var tau = new Complex((beta - alphaR) / beta, -alphaI / beta);
            var t = new Complex(1.0, 0.0) / (alpha - beta);

            for (var j = 1; j < n; j++)
            {
                x[j] *= t;
            }

            for (var j = 0; j < kount; j++)
            {
                beta *= sfmin;
            }

            x[0] = new Complex(beta, 0.0);
            return tau;
        }

        /// <summary>
        /// Compute Householder reflector for real vector.
        /// </summary>
        public static double ComputeReflector(MathNet.Numerics.LinearAlgebra.Vector<double> x)
        {
            var n = x.Count;
            if (n <= 1) return 0.0;

            var sfmin = 2.0 * NumericalUtils.SafeMin() / Precision.DoublePrecision;
            var alpha = x[0];
            
            var xnorm = 0.0;
            for (var i = 1; i < n; i++)
            {
                xnorm += x[i] * x[i];
            }
            xnorm = Math.Sqrt(xnorm);

            if (xnorm == 0.0)
            {
                return 0.0;
            }

            var beta = -Math.Sign(alpha) * Math.Sqrt(alpha * alpha + xnorm * xnorm);
            var kount = 0;
            var smallBeta = Math.Abs(beta) < sfmin;

            if (smallBeta)
            {
                var rsfmin = 1.0 / sfmin;
                while (smallBeta && kount < 20)
                {
                    kount++;
                    for (var j = 1; j < n; j++)
                    {
                        x[j] *= rsfmin;
                    }
                    beta *= rsfmin;
                    alpha *= rsfmin;
                    smallBeta = Math.Abs(beta) < sfmin;
                }

                xnorm = 0.0;
                for (var i = 1; i < n; i++)
                {
                    xnorm += x[i] * x[i];
                }
                xnorm = Math.Sqrt(xnorm);
                beta = -Math.Sign(alpha) * Math.Sqrt(alpha * alpha + xnorm * xnorm);
            }

            var tau = (beta - alpha) / beta;
            var t = 1.0 / (alpha - beta);

            for (var j = 1; j < n; j++)
            {
                x[j] *= t;
            }

            for (var j = 0; j < kount; j++)
            {
                beta *= sfmin;
            }

            x[0] = beta;
            return tau;
        }

        /// <summary>
        /// Apply Householder reflector H = I - τ * v * v^H to matrix matrixA from the left.
        /// matrixA := H * matrixA
        /// </summary>
        public static void ApplyReflectorLeft(Matrix<Complex> matrixA, MathNet.Numerics.LinearAlgebra.Vector<Complex> v, Complex tau,
                                            int startRow = 0, int startCol = 0)
        {
            if (tau == Complex.Zero) return;

            var m = matrixA.RowCount;
            var n = matrixA.ColumnCount;
            var vlen = v.Count;

            // Apply H to matrixA
            for (var j = startCol; j < n; j++)
            {
                var sum = Complex.Zero;
                
                // Compute v^H * matrixA[:, j]
                if (startRow < m)
                    sum = matrixA[startRow, j];
                
                for (var i = 1; i < vlen && (startRow + i) < m; i++)
                {
                    sum += Complex.Conjugate(v[i]) * matrixA[startRow + i, j];
                }

                sum *= tau;

                // matrixA[:, j] := matrixA[:, j] - τ * sum * v
                if (startRow < m)
                    matrixA[startRow, j] -= sum;
                
                for (var i = 1; i < vlen && (startRow + i) < m; i++)
                {
                    matrixA[startRow + i, j] -= sum * v[i];
                }
            }
        }

        /// <summary>
        /// Apply Householder reflector H = I - τ * v * v^H to matrix matrixA from the right.
        /// matrixA := matrixA * H
        /// </summary>
        public static void ApplyReflectorRight(Matrix<Complex> matrixA, MathNet.Numerics.LinearAlgebra.Vector<Complex> v, Complex tau,
                                             int startRow = 0, int startCol = 0)
        {
            if (tau == Complex.Zero) return;

            var m = matrixA.RowCount;
            var n = matrixA.ColumnCount;
            var vlen = v.Count;

            // Apply H to matrixA from right
            for (var i = startRow; i < m; i++)
            {
                var sum = Complex.Zero;
                
                // Compute matrixA[i, :] * v
                if (startCol < n)
                    sum = matrixA[i, startCol];
                
                for (var j = 1; j < vlen && (startCol + j) < n; j++)
                {
                    sum += matrixA[i, startCol + j] * v[j];
                }

                sum *= Complex.Conjugate(tau);

                // matrixA[i, :] := matrixA[i, :] - sum * τ^* * v^H
                if (startCol < n)
                    matrixA[i, startCol] -= sum;
                
                for (var j = 1; j < vlen && (startCol + j) < n; j++)
                {
                    matrixA[i, startCol + j] -= sum * Complex.Conjugate(v[j]);
                }
            }
        }

        /// <summary>
        /// Real version of ApplyReflectorLeft.
        /// </summary>
        public static void ApplyReflectorLeft(Matrix<double> matrixA, MathNet.Numerics.LinearAlgebra.Vector<double> v, double tau,
                                            int startRow = 0, int startCol = 0)
        {
            if (tau == 0.0) return;

            var m = matrixA.RowCount;
            var n = matrixA.ColumnCount;
            var vlen = v.Count;

            for (var j = startCol; j < n; j++)
            {
                var sum = 0.0;
                
                if (startRow < m)
                    sum = matrixA[startRow, j];
                
                for (var i = 1; i < vlen && (startRow + i) < m; i++)
                {
                    sum += v[i] * matrixA[startRow + i, j];
                }

                sum *= tau;

                if (startRow < m)
                    matrixA[startRow, j] -= sum;
                
                for (var i = 1; i < vlen && (startRow + i) < m; i++)
                {
                    matrixA[startRow + i, j] -= sum * v[i];
                }
            }
        }

        /// <summary>
        /// Real version of ApplyReflectorRight.
        /// </summary>
        public static void ApplyReflectorRight(Matrix<double> matrixA, MathNet.Numerics.LinearAlgebra.Vector<double> v, double tau,
                                             int startRow = 0, int startCol = 0)
        {
            if (tau == 0.0) return;

            var m = matrixA.RowCount;
            var n = matrixA.ColumnCount;
            var vlen = v.Count;

            for (var i = startRow; i < m; i++)
            {
                var sum = 0.0;
                
                if (startCol < n)
                    sum = matrixA[i, startCol];
                
                for (var j = 1; j < vlen && (startCol + j) < n; j++)
                {
                    sum += matrixA[i, startCol + j] * v[j];
                }

                sum *= tau;

                if (startCol < n)
                    matrixA[i, startCol] -= sum;
                
                for (var j = 1; j < vlen && (startCol + j) < n; j++)
                {
                    matrixA[i, startCol + j] -= sum * v[j];
                }
            }
        }
    }
}