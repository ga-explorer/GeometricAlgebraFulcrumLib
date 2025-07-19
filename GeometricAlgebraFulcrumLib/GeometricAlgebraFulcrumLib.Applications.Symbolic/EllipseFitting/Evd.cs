namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.EllipseFitting;

public static class Evd
{
    private const int MaxIterations = 30;
    private const double Eps = 1e-12;

    public static EvdResult Symmetric(double[,] matrix)
    {
        var n = matrix.GetLength(0);
        if (n != matrix.GetLength(1))
            throw new ArgumentException("Matrix must be square.");

        // Initialize eigenvectors as identity matrix
        var eigenVectors = CreateIdentity(n);

        // Step 1: Tridiagonalize the matrix using Householder reflections
        var d = new double[n]; // Diagonal elements
        var e = new double[n]; // Sub-diagonal elements
        HouseholderTridiagonalization(matrix, n, d, e, eigenVectors);

        // Step 2: Apply implicit QR iteration to diagonalize T
        ImplicitQrSweepWithVectors(d, e, n, eigenVectors);

        // Step 3: Sort eigenvalues and eigenvectors descending
        SortDescending(d, eigenVectors, n);

        return new EvdResult(d, eigenVectors);
    }

    /// <summary>
    /// Step 1: Householder Tridiagonalization
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="n"></param>
    /// <param name="d"></param>
    /// <param name="e"></param>
    /// <param name="eigenVectors"></param>
    private static void HouseholderTridiagonalization(double[,] matrix, int n, double[] d, double[] e, double[,] eigenVectors)
    {
        for (var k = 0; k < n - 1; k++)
        {
            double scale = 0;
            for (var i = k + 1; i < n; i++)
                scale += Math.Abs(matrix[i, k]);

            if (scale == 0)
            {
                e[k] = matrix[k + 1, k];
                d[k] = 0;
                continue;
            }

            double sigma = 0;
            for (var i = k + 1; i < n; i++)
            {
                matrix[i, k] /= scale;
                sigma += matrix[i, k] * matrix[i, k];
            }

            var alpha = Math.Sqrt(sigma);
            if (matrix[k + 1, k] >= 0) alpha = -alpha;

            var beta = sigma - matrix[k + 1, k] * alpha;
            matrix[k + 1, k] -= alpha;
            d[k] = alpha;

            // Apply reflection to remaining columns
            for (var j = k + 1; j < n; j++)
            {
                double g = 0;
                for (var i = k + 1; i < n; i++)
                    g += matrix[i, k] * matrix[i, j];

                g /= beta;

                for (var i = k + 1; i < n; i++)
                    matrix[i, j] -= g * matrix[i, k];
            }

            // Update eigenvector matrix V
            for (var r = 0; r < n; r++)
            {
                double g = 0;
                for (var i = k + 1; i < n; i++)
                    g += matrix[i, k] * eigenVectors[r, i];

                g /= beta;

                for (var i = k + 1; i < n; i++)
                    eigenVectors[r, i] -= g * matrix[i, k];
            }

            e[k] = scale * beta;
            d[k] = matrix[k, k];
        }

        d[n - 1] = matrix[n - 1, n - 1];
        e[n - 1] = 0;
    }

    /// <summary>
    /// Step 2: QR Iteration with Implicit Shift (Wilkinson)
    /// </summary>
    /// <param name="d"></param>
    /// <param name="e"></param>
    /// <param name="n"></param>
    /// <param name="eigenVectors"></param>
    /// <exception cref="Exception"></exception>
    private static void ImplicitQrSweepWithVectors(double[] d, double[] e, int n, double[,] eigenVectors)
    {
        var l = 0;
        var iter = 0;

        while (l < n)
        {
            var m = FindConvergencePoint(d, e, n, l);
            if (m == l)
            {
                l++;
                iter = 0;
                continue;
            }

            if (iter++ >= MaxIterations)
                throw new Exception("QR iteration did not converge.");

            var mu = ComputeWilkinsonShift(d[m], d[m + 1], e[m]);
            ApplyImplicitQrStepWithVectors(d, e, n, m, l, mu, eigenVectors);
        }
    }

    private static void ApplyImplicitQrStepWithVectors(double[] d, double[] e, int n, int m, int l, double mu, double[,] eigenVectors)
    {
        var g = d[m] - mu;
        var p = g;
        double c = 1.0, s = 0.0;

        for (var i = m; i < l; i++)
        {
            var f = s * e[i];
            var b = c * e[i];

            if (Math.Abs(f) > Eps)
            {
                var r = Math.Sqrt(f * f + g * g);
                c = g / r;
                s = f / r;
                g = d[i + 1] - mu;
                var temp = (d[i] - g) * s + 2 * c * b;
                p = s * temp;

                d[i] = g + p;
                d[i + 1] = g - p;
                e[i] = temp + b;
                g = -s * b + c * temp;
            }
            else
            {
                g = d[i + 1] - mu;
                p = -f;
                e[i] = g;
                d[i + 1] = g + f;
            }

            // Apply rotation to eigenvectors
            for (var v = 0; v < n; v++)
            {
                var y = eigenVectors[v, i];
                var z = eigenVectors[v, i + 1];
                eigenVectors[v, i] = y * c - z * s;
                eigenVectors[v, i + 1] = z * c + y * s;
            }
        }

        if (l < n)
            d[l] -= p;
    }

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

    private static double ComputeWilkinsonShift(double dM, double dM1, double eM)
    {
        var delta = (dM1 - dM) / 2.0;
        var sigma = Math.Sign(delta) * Math.Sqrt(delta * delta + eM * eM);
        return dM1 - (eM * eM) / (delta + sigma);
    }

    /// <summary>
    /// Step 3: Sorting Eigenvalues and Eigenvectors
    /// </summary>
    /// <param name="d"></param>
    /// <param name="eigenVectors"></param>
    /// <param name="n"></param>
    private static void SortDescending(double[] d, double[,] eigenVectors, int n)
    {
        for (var i = 0; i < n; i++)
        {
            var maxIndex = i;
            for (var j = i + 1; j < n; j++)
            {
                if (d[j] > d[maxIndex])
                    maxIndex = j;
            }

            if (maxIndex != i)
            {
                // Swap eigenvalues
                var temp = d[i];
                d[i] = d[maxIndex];
                d[maxIndex] = temp;

                // Swap eigenvectors
                for (var k = 0; k < n; k++)
                {
                    temp = eigenVectors[k, i];
                    eigenVectors[k, i] = eigenVectors[k, maxIndex];
                    eigenVectors[k, maxIndex] = temp;
                }
            }
        }
    }

    /// <summary>
    /// Helper methods
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    private static double[,] CreateIdentity(int n)
    {
        var eye = new double[n, n];
        for (var i = 0; i < n; i++)
            eye[i, i] = 1.0;
        return eye;
    }

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
    /// Example usage
    /// </summary>
    public static void ExampleUsage()
    {
        const int n = 4;
        double[,] a = {
            { 4, 1, 2, 1 },
            { 1, 5, 1, 2 },
            { 2, 1, 6, 1 },
            { 1, 2, 1, 7 }
        };

        var result = Evd.Symmetric(a);

        Console.WriteLine("Eigenvalues:");
        PrintArray("", result.EigenValues);

        Console.WriteLine("Eigenvectors:");
        PrintMatrix(result.EigenVectors, n);
    }
}