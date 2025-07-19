namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.EllipseFitting;

using System;

public sealed class SymmetricEigenDecomposer
{
    
    /// <summary>
    /// Helper: Print array
    /// </summary>
    /// <param name="label"></param>
    /// <param name="arr"></param>
    public static void PrintArray(string label, double[] arr)
    {
        Console.WriteLine(label);
        foreach (var val in arr)
        {
            Console.Write($"{val:F6} ");
        }
        Console.WriteLine("\n");
    }
    
    /// <summary>
    /// Helper method to print a matrix
    /// </summary>
    /// <param name="a"></param>
    /// <param name="n"></param>
    public static void PrintMatrix(double[,] a, int n)
    {
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                Console.Write($"{a[i, j]:F6} ");
            }
            Console.WriteLine();
        }
    }


    /// <summary>
    /// Example usage
    /// </summary>
    public static void ExampleUse()
    {
        const int n = 4;
        double[,] a = {
                { 4, 1, 2, 1 },
                { 1, 5, 1, 2 },
                { 2, 1, 6, 1 },
                { 1, 2, 1, 7 }
            };

        var d = new double[n]; // Diagonal entries
        var e = new double[n]; // Sub-diagonal entries

        Console.WriteLine("Original Matrix:");
        PrintMatrix(a, n);

        Tridiagonalize(a, n, d, e);

        Console.WriteLine("\nTridiagonal Form:");
        PrintMatrix(a, n);

        Console.WriteLine("\nDiagonal (d):");
        for (var i = 0; i < n; i++) Console.Write($"{d[i]:F6} ");
        Console.WriteLine();

        Console.WriteLine("Sub-diagonal (e):");
        for (var i = 0; i < n; i++) Console.Write($"{e[i]:F6} ");
        Console.WriteLine();
    }

    /// <summary>
    /// Example usage
    /// </summary>
    public static void ExampleUseSymmetricTridiagonalQr()
    {
        const int n = 4;

        // Tridiagonal matrix stored as diagonal (d) and sub-diagonal (e)
        double[] d = [4, 5, 6, 7]; // Diagonal entries
        double[] e = [1, 1, 1];    // Sub-diagonal entries

        Console.WriteLine("Initial diagonal:");
        PrintArray("", d);

        Console.WriteLine("Initial sub-diagonal:");
        PrintArray("", e);

        QrStep(d, e, n);

        Console.WriteLine("Eigenvalues after QR iteration:");
        PrintArray("", d);
    }


    private const int MaxIterations = 30;
    private const double Eps = 1e-12;


    /// <summary>
    /// Tridiagonalize a real symmetric matrix using Householder reflections.
    /// After this process:
    /// - The matrix will be in tridiagonal form.
    /// - The diagonal and sub-diagonal entries contain eigenvalue information.
    /// - The rest of the matrix contains reflector vectors used to reconstruct Q.
    /// </summary>
    public static void Tridiagonalize(double[,] a, int n, double[] d, double[] e)
    {
        // A: Input symmetric matrix (n x n), gets overwritten with tridiagonal form and reflectors
        // d: Output array for diagonal elements
        // e: Output array for sub-diagonal elements

        for (var k = 0; k < n - 1; k++)
        {
            // ----------------------------
            // Step 1: Compute Householder vector for column k
            // ----------------------------

            double scale = 0;
            for (var i = k + 1; i < n; i++)
            {
                scale += Math.Abs(a[i, k]);
            }

            if (scale == 0)
            {
                // If the lower part of the column is zero, no reflection needed
                e[k] = a[k + 1, k];
                d[k] = 0;
                continue;
            }

            // Normalize the lower part of the column
            double sigma = 0;
            for (var i = k + 1; i < n; i++)
            {
                a[i, k] /= scale;
                sigma += a[i, k] * a[i, k];
            }

            var alpha = Math.Sqrt(sigma);
            if (a[k + 1, k] >= 0) alpha = -alpha;

            var beta = sigma - a[k + 1, k] * alpha;
            a[k + 1, k] -= alpha;

            d[k] = alpha;
            e[k] = scale * beta;

            // ----------------------------
            // Step 2: Apply reflection to remaining columns (to update A)
            // ----------------------------

            for (var j = k + 1; j < n; j++)
            {
                double g = 0;
                for (var i = k + 1; i < n; i++)
                {
                    g += a[i, k] * a[i, j];
                }

                g /= beta;

                for (var i = k + 1; i < n; i++)
                {
                    a[i, j] -= g * a[i, k];
                }
            }

            // ----------------------------
            // Step 3: Apply reflection from the right (symmetry preservation)
            // ----------------------------

            for (var i = k + 1; i < n; i++)
            {
                a[k, i] = a[i, k]; // Store updated values symmetrically
            }
        }

        // Set final diagonal element
        d[n - 1] = a[n - 1, n - 1];

        // Clear out unused value
        e[n - 1] = 0;
    }
    
    /// <summary>
    /// Implicit QR iteration on a symmetric tridiagonal matrix.
    /// After execution:
    /// - d contains eigenvalues in ascending order
    /// - e contains negligible values (off-diagonals)
    /// </summary>
    public static void QrStep(double[] d, double[] e, int n)
    {
        if (n <= 1) return;

        var iterNum = 0;
        var l = 0;

        while (l < n)
        {
            // Find the smallest index m such that e[m] is negligible or m == l
            var m = FindConvergencePoint(d, e, n, l);

            if (m == l)
            {
                // Eigenvalue found at d[l], move to next
                l++;
                iterNum = 0;
                continue;
            }

            if (iterNum++ >= MaxIterations)
                throw new Exception("QR iteration did not converge.");

            // Compute Wilkinson shift
            var mu = ComputeWilkinsonShift(d[m], d[m + 1], e[m]);

            // Implicit QR sweep from m to l
            ImplicitQrSweep(d, e, n, m, l, mu);
        }

        // Sort eigenvalues after QR iteration
        Array.Sort(d);
    }

    /// <summary>
    /// Finds the smallest index m such that e[m] ~ 0 (deflation point).
    /// </summary>
    private static int FindConvergencePoint(double[] d, double[] e, int n, int l)
    {
        int m;
        for (m = l; m < n - 1; m++)
        {
            var s = Math.Abs(d[m]) + Math.Abs(d[m + 1]);
            if (Math.Abs(e[m]) <= Eps * s)
                break;
        }
        return m;
    }

    /// <summary>
    /// Computes the Wilkinson shift:
    /// μ = d_k+1 - δ/(2σ), where σ = sign(δ) * sqrt(δ² + e_k²)
    /// </summary>
    private static double ComputeWilkinsonShift(double dM, double dM1, double eM)
    {
        var delta = (dM1 - dM) / 2.0;
        var sigma = Math.Sign(delta) * Math.Sqrt(delta * delta + eM * eM);
        return dM1 - eM * eM / (delta + sigma);
    }

    /// <summary>
    /// Performs an implicit QR sweep with shift mu.
    /// Applies Givens rotations to chase the bulge.
    /// </summary>
    private static void ImplicitQrSweep(double[] d, double[] e, int n, int m, int l, double mu)
    {
        var g = d[m] - mu;
        var p = g;
        var c = 1.0;
        var s = 0.0;

        for (var i = m; i < l; i++)
        {
            var f = s * e[i];
            var b = c * e[i];

            if (Math.Abs(f) < Eps)
            {
                // No rotation needed
                g = d[i + 1] - mu;
                s = f = e[i];
                p = g;
                continue;
            }

            // Compute Givens rotation
            var r = Math.Sqrt(f * f + g * g);
            if (r < Eps) continue;

            c = g / r;
            s = f / r;
            g = d[i + 1] - mu;

            var temp = (d[i] - g) * s + 2.0 * c * b;
            p = s * temp;

            // Update diagonal and off-diagonal
            d[i] = g + p;
            d[i + 1] = g - p;
            e[i] = temp + b;
            g = -s * b + c * temp;
        }

        // Apply final rotation to last element
        if (l < n)
            d[l] -= p;
    }


}