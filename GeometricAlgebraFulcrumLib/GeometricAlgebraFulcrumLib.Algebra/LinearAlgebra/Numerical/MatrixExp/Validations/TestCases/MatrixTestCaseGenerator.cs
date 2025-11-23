using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Matrices;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.MatrixExp.Validations.TestCases;

/// <summary>
/// Comprehensive test case generator for matrix exponential validation.
/// Generates matrices across different categories, sizes, and spectral properties.
/// </summary>
public static class MatrixTestCaseGenerator
{
    /// <summary>
    /// Categories of test matrices to generate.
    /// </summary>
    [Flags]
    public enum MatrixCategory
    {
        None = 0,
        GeneralDense = 1 << 0,
        Symmetric = 1 << 1,
        SymmetricPositiveDefinite = 1 << 2,
        SymmetricIndefinite = 1 << 3,
        SkewSymmetric = 1 << 4,
        UpperTriangular = 1 << 5,
        LowerTriangular = 1 << 6,
        Diagonal = 1 << 7,
        NearSingular = 1 << 8,
        IllConditioned = 1 << 9,
        Sparse = 1 << 10,
        Structured = 1 << 11,
        AnalyticalSolution = 1 << 12,
        All = GeneralDense | Symmetric | SymmetricPositiveDefinite | SymmetricIndefinite | 
              SkewSymmetric | UpperTriangular | LowerTriangular | Diagonal | 
              NearSingular | IllConditioned | Sparse | Structured | AnalyticalSolution
    }

    /// <summary>
    /// Spectral properties for test matrix generation.
    /// </summary>
    [Flags]
    public enum SpectralProperty
    {
        None = 0,
        RealEigenvalues = 1 << 0,
        ComplexConjugatePairs = 1 << 1,
        ClusteredEigenvalues = 1 << 2,
        WidelySeparatedEigenvalues = 1 << 3,
        ZeroEigenvalues = 1 << 4,
        NegativeEigenvalues = 1 << 5,
        LargeEigenvalues = 1 << 6,
        SmallEigenvalues = 1 << 7,
        All = RealEigenvalues | ComplexConjugatePairs | ClusteredEigenvalues | 
              WidelySeparatedEigenvalues | ZeroEigenvalues | NegativeEigenvalues | 
              LargeEigenvalues | SmallEigenvalues
    }

    /// <summary>
    /// Test matrix with metadata.
    /// </summary>
    public sealed class TestMatrix
    {
        public required Matrix<double> Matrix { get; init; }
        public required string Name { get; init; }
        public required MatrixCategory Category { get; init; }
        public required SpectralProperty SpectralProperties { get; init; }
        public required int Size { get; init; }
        public double ConditionNumber { get; init; }
        public double OneNorm { get; init; }
        public double FrobeniusNorm { get; init; }
        public Matrix<double>? AnalyticalSolution { get; init; }
        public string Description { get; init; } = string.Empty;
        public Dictionary<string, object> Metadata { get; init; } = new();
    }

    /// <summary>
    /// Configuration for test case generation.
    /// </summary>
    public sealed class GenerationConfig
    {
        public MatrixCategory Categories { get; set; } = MatrixCategory.All;
        public SpectralProperty SpectralProperties { get; set; } = SpectralProperty.All;
        public int[] Dimensions { get; set; } = [1, 2, 3, 4, 8, 16, 32, 64, 128, 256];
        public int CasesPerCategory { get; set; } = 5;
        public Random Random { get; set; } = new Random(42);
        public double Tolerance { get; set; } = 1e-14;
        public bool IncludeAnalyticalSolutions { get; set; } = true;
        public bool IncludeMetadata { get; set; } = true;
    }

    /// <summary>
    /// Generates comprehensive test cases for matrix exponential validation.
    /// </summary>
    public static List<TestMatrix> GenerateTestCases(GenerationConfig config)
    {
        var testCases = new List<TestMatrix>();
        var categories = Enum.GetValues<MatrixCategory>()
            .Where(c => c != MatrixCategory.None && c != MatrixCategory.All && config.Categories.HasFlag(c));

        foreach (var category in categories)
        {
            foreach (var dimension in config.Dimensions)
            {
                for (var i = 0; i < config.CasesPerCategory; i++)
                {
                    try
                    {
                        var testMatrix = GenerateTestMatrix(category, dimension, config, i);
                        if (testMatrix != null)
                            testCases.Add(testMatrix);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Warning: Failed to generate {category} matrix of size {dimension}: {ex.Message}");
                    }
                }
            }
        }

        return testCases;
    }

    /// <summary>
    /// Generates a single test matrix for the specified category and dimension.
    /// </summary>
    private static TestMatrix? GenerateTestMatrix(MatrixCategory category, int dimension, 
        GenerationConfig config, int caseIndex)
    {
        var matrix = category switch
        {
            MatrixCategory.GeneralDense => GenerateGeneralDenseMatrix(dimension, config.Random),
            MatrixCategory.Symmetric => GenerateSymmetricMatrix(dimension, config.Random),
            MatrixCategory.SymmetricPositiveDefinite => GenerateSymmetricPositiveDefiniteMatrix(dimension, config.Random),
            MatrixCategory.SymmetricIndefinite => GenerateSymmetricIndefiniteMatrix(dimension, config.Random),
            MatrixCategory.SkewSymmetric => GenerateSkewSymmetricMatrix(dimension, config.Random),
            MatrixCategory.UpperTriangular => GenerateUpperTriangularMatrix(dimension, config.Random),
            MatrixCategory.LowerTriangular => GenerateLowerTriangularMatrix(dimension, config.Random),
            MatrixCategory.Diagonal => GenerateDiagonalMatrix(dimension, config.Random),
            MatrixCategory.NearSingular => GenerateNearSingularMatrix(dimension, config.Random),
            MatrixCategory.IllConditioned => GenerateIllConditionedMatrix(dimension, config.Random),
            MatrixCategory.Sparse => GenerateSparseMatrix(dimension, config.Random),
            MatrixCategory.Structured => GenerateStructuredMatrix(dimension, config.Random, caseIndex),
            MatrixCategory.AnalyticalSolution => GenerateAnalyticalSolutionMatrix(dimension, config.Random, caseIndex),
            _ => null
        };

        if (matrix == null) return null;

        // Detect spectral properties
        var spectralProps = DetectSpectralProperties(matrix, config.Tolerance);

        // Filter by required spectral properties
        if (config.SpectralProperties != SpectralProperty.All && 
            (spectralProps & config.SpectralProperties) == SpectralProperty.None)
        {
            return null;
        }

        // Compute matrix properties
        var conditionNumber = EstimateConditionNumber(matrix);
        var oneNorm = matrix.OneNorm();
        var frobeniusNorm = matrix.FrobeniusNorm();

        // Generate analytical solution if applicable
        Matrix<double>? analyticalSolution = null;
        if (config.IncludeAnalyticalSolutions)
        {
            analyticalSolution = ComputeAnalyticalSolution(matrix, category);
        }

        var name = $"{category}_{dimension}x{dimension}_{caseIndex}";
        var description = GenerateDescription(category, dimension, spectralProps, conditionNumber);

        var metadata = new Dictionary<string, object>();
        if (config.IncludeMetadata)
        {
            PopulateMetadata(metadata, matrix, category, spectralProps);
        }

        return new TestMatrix
        {
            Matrix = matrix,
            Name = name,
            Category = category,
            SpectralProperties = spectralProps,
            Size = dimension,
            ConditionNumber = conditionNumber,
            OneNorm = oneNorm,
            FrobeniusNorm = frobeniusNorm,
            AnalyticalSolution = analyticalSolution,
            Description = description,
            Metadata = metadata
        };
    }

    #region Matrix Generation Methods

    private static Matrix<double> GenerateGeneralDenseMatrix(int n, Random random)
    {
        var matrix = Matrix<double>.Build.Dense(n, n);
        var normal = new Normal(0, 1, random);
        
        for (var i = 0; i < n; i++)
        for (var j = 0; j < n; j++)
        {
            matrix[i, j] = normal.Sample();
        }
        
        return matrix;
    }

    private static Matrix<double> GenerateSymmetricMatrix(int n, Random random)
    {
        var matrix = Matrix<double>.Build.Dense(n, n);
        var normal = new Normal(0, 1, random);
        
        for (var i = 0; i < n; i++)
        {
            matrix[i, i] = normal.Sample();
            for (var j = i + 1; j < n; j++)
            {
                var value = normal.Sample();
                matrix[i, j] = value;
                matrix[j, i] = value;
            }
        }
        
        return matrix;
    }

    private static Matrix<double> GenerateSymmetricPositiveDefiniteMatrix(int n, Random random)
    {
        var A = GenerateGeneralDenseMatrix(n, random);
        return A.TransposeAndMultiply(A) + 0.1 * Matrix<double>.Build.DenseIdentity(n);
    }

    private static Matrix<double> GenerateSymmetricIndefiniteMatrix(int n, Random random)
    {
        var symmetric = GenerateSymmetricMatrix(n, random);
        
        // Ensure it has both positive and negative eigenvalues
        var eigenVals = new double[n];
        for (var i = 0; i < n / 2; i++)
            eigenVals[i] = Math.Abs(random.NextDouble()) + 0.1;
        for (var i = n / 2; i < n; i++)
            eigenVals[i] = -(Math.Abs(random.NextDouble()) + 0.1);
        
        var Q = GenerateOrthogonalMatrix(n, random);
        var D = Matrix<double>.Build.DenseIdentity(n);
        for (var i = 0; i < n; i++)
            D[i, i] = eigenVals[i];
            
        return Q * D * Q.Transpose();
    }

    private static Matrix<double> GenerateSkewSymmetricMatrix(int n, Random random)
    {
        var matrix = Matrix<double>.Build.Dense(n, n);
        var normal = new Normal(0, 1, random);
        
        for (var i = 0; i < n; i++)
        {
            for (var j = i + 1; j < n; j++)
            {
                var value = normal.Sample();
                matrix[i, j] = value;
                matrix[j, i] = -value;
            }
        }
        
        return matrix;
    }

    private static Matrix<double> GenerateUpperTriangularMatrix(int n, Random random)
    {
        var matrix = Matrix<double>.Build.Dense(n, n);
        var normal = new Normal(0, 1, random);
        
        for (var i = 0; i < n; i++)
        for (var j = i; j < n; j++)
        {
            matrix[i, j] = normal.Sample();
        }
        
        return matrix;
    }

    private static Matrix<double> GenerateLowerTriangularMatrix(int n, Random random)
    {
        var matrix = Matrix<double>.Build.Dense(n, n);
        var normal = new Normal(0, 1, random);
        
        for (var i = 0; i < n; i++)
        for (var j = 0; j <= i; j++)
        {
            matrix[i, j] = normal.Sample();
        }
        
        return matrix;
    }

    private static Matrix<double> GenerateDiagonalMatrix(int n, Random random)
    {
        var matrix = Matrix<double>.Build.Dense(n, n);
        var normal = new Normal(0, 1, random);
        
        for (var i = 0; i < n; i++)
        {
            matrix[i, i] = normal.Sample();
        }
        
        return matrix;
    }

    private static Matrix<double> GenerateNearSingularMatrix(int n, Random random)
    {
        var matrix = GenerateGeneralDenseMatrix(n, random);
        
        // Make one singular value very small
        var svd = matrix.Svd();
        var singularValues = svd.S.ToArray();
        singularValues[^1] = 1e-12; // Make smallest singular value tiny
        
        var S = Matrix<double>.Build.DenseIdentity(n);
        for (var i = 0; i < Math.Min(n, singularValues.Length); i++)
        {
            S[i, i] = singularValues[i];
        }
        
        return svd.U * S * svd.VT;
    }

    private static Matrix<double> GenerateIllConditionedMatrix(int n, Random random)
    {
        var conditionNumber = Math.Pow(10, 6 + random.NextDouble() * 6); // Condition number 10^6 to 10^12
        
        var U = GenerateOrthogonalMatrix(n, random);
        var V = GenerateOrthogonalMatrix(n, random);
        
        var singularValues = new double[n];
        singularValues[0] = 1.0;
        for (var i = 1; i < n - 1; i++)
        {
            singularValues[i] = Math.Pow(conditionNumber, -random.NextDouble());
        }
        singularValues[n - 1] = 1.0 / conditionNumber;
        
        var S = Matrix<double>.Build.DenseIdentity(n);
        for (var i = 0; i < n; i++)
        {
            S[i, i] = singularValues[i];
        }
        
        return U * S * V.Transpose();
    }

    private static Matrix<double> GenerateSparseMatrix(int n, Random random)
    {
        var matrix = Matrix<double>.Build.Dense(n, n);
        var normal = new Normal(0, 1, random);
        var sparsity = 0.1 + random.NextDouble() * 0.2; // 10-30% non-zero elements
        
        var numNonZero = (int)(n * n * sparsity);
        for (var k = 0; k < numNonZero; k++)
        {
            var i = random.Next(n);
            var j = random.Next(n);
            matrix[i, j] = normal.Sample();
        }
        
        return matrix;
    }

    private static Matrix<double> GenerateStructuredMatrix(int n, Random random, int caseIndex)
    {
        return caseIndex % 5 switch
        {
            0 => GenerateHankelMatrix(n, random),
            1 => GenerateToeplitzMatrix(n, random),
            2 => GenerateCauchyMatrix(n, random),
            3 => GenerateVandermondMatrix(n, random),
            4 => GenerateCirculantMatrix(n, random),
            _ => GenerateHankelMatrix(n, random)
        };
    }

    private static Matrix<double> GenerateAnalyticalSolutionMatrix(int n, Random random, int caseIndex)
    {
        return caseIndex % 4 switch
        {
            0 => GenerateNilpotentMatrix(n, random),
            1 => Matrix<double>.Build.DenseIdentity(n) * random.NextDouble(),
            2 => GenerateSimpleRotationMatrix(n, random),
            3 => GenerateBlockDiagonalMatrix(n, random),
            _ => GenerateNilpotentMatrix(n, random)
        };
    }

    #endregion

    #region Specific Structured Matrix Generators

    private static Matrix<double> GenerateHankelMatrix(int n, Random random)
    {
        var matrix = Matrix<double>.Build.Dense(n, n);
        var normal = new Normal(0, 1, random);
        var values = new double[2 * n - 1];
        
        for (var k = 0; k < values.Length; k++)
            values[k] = normal.Sample();
        
        for (var i = 0; i < n; i++)
        for (var j = 0; j < n; j++)
        {
            matrix[i, j] = values[i + j];
        }
        
        return matrix;
    }

    private static Matrix<double> GenerateToeplitzMatrix(int n, Random random)
    {
        var matrix = Matrix<double>.Build.Dense(n, n);
        var normal = new Normal(0, 1, random);
        var values = new double[2 * n - 1];
        
        for (var k = 0; k < values.Length; k++)
            values[k] = normal.Sample();
        
        for (var i = 0; i < n; i++)
        for (var j = 0; j < n; j++)
        {
            matrix[i, j] = values[n - 1 + i - j];
        }
        
        return matrix;
    }

    private static Matrix<double> GenerateCauchyMatrix(int n, Random random)
    {
        var matrix = Matrix<double>.Build.Dense(n, n);
        var x = new double[n];
        var y = new double[n];
        
        for (var i = 0; i < n; i++)
        {
            x[i] = random.NextDouble() * 10;
            y[i] = random.NextDouble() * 10 + 15; // Ensure x[i] != y[j]
        }
        
        for (var i = 0; i < n; i++)
        for (var j = 0; j < n; j++)
        {
            matrix[i, j] = 1.0 / (x[i] + y[j]);
        }
        
        return matrix;
    }

    private static Matrix<double> GenerateVandermondMatrix(int n, Random random)
    {
        var matrix = Matrix<double>.Build.Dense(n, n);
        var points = new double[n];
        
        for (var i = 0; i < n; i++)
            points[i] = random.NextDouble() * 4 - 2; // Points in [-2, 2]
        
        for (var i = 0; i < n; i++)
        for (var j = 0; j < n; j++)
        {
            matrix[i, j] = Math.Pow(points[i], j);
        }
        
        return matrix;
    }

    private static Matrix<double> GenerateCirculantMatrix(int n, Random random)
    {
        var matrix = Matrix<double>.Build.Dense(n, n);
        var normal = new Normal(0, 1, random);
        var firstRow = new double[n];
        
        for (var j = 0; j < n; j++)
            firstRow[j] = normal.Sample();
        
        for (var i = 0; i < n; i++)
        for (var j = 0; j < n; j++)
        {
            matrix[i, j] = firstRow[(j - i + n) % n];
        }
        
        return matrix;
    }

    private static Matrix<double> GenerateNilpotentMatrix(int n, Random random)
    {
        var matrix = Matrix<double>.Build.Dense(n, n);
        var normal = new Normal(0, 0.5, random);
        
        // Create upper triangular nilpotent matrix
        for (var i = 0; i < n - 1; i++)
        {
            matrix[i, i + 1] = Math.Abs(normal.Sample()) + 0.1;
        }
        
        return matrix;
    }

    private static Matrix<double> GenerateSimpleRotationMatrix(int n, Random random)
    {
        if (n != 2) return GenerateSkewSymmetricMatrix(n, random);
        
        var theta = random.NextDouble() * 2 * Math.PI;
        return Matrix<double>.Build.DenseOfArray(new[,] {
            { Math.Cos(theta), -Math.Sin(theta) },
            { Math.Sin(theta), Math.Cos(theta) }
        });
    }

    private static Matrix<double> GenerateBlockDiagonalMatrix(int n, Random random)
    {
        var matrix = Matrix<double>.Build.Dense(n, n);
        var normal = new Normal(0, 1, random);
        
        var i = 0;
        while (i < n)
        {
            var blockSize = Math.Min(random.Next(1, 4), n - i); // Block sizes 1-3
            
            for (var bi = 0; bi < blockSize; bi++)
            for (var bj = 0; bj < blockSize; bj++)
            {
                matrix[i + bi, i + bj] = normal.Sample();
            }
            
            i += blockSize;
        }
        
        return matrix;
    }

    private static Matrix<double> GenerateOrthogonalMatrix(int n, Random random)
    {
        var A = GenerateGeneralDenseMatrix(n, random);
        var qr = A.QR();
        return qr.Q;
    }

    #endregion

    #region Utility Methods

    private static SpectralProperty DetectSpectralProperties(Matrix<double> matrix, double tolerance)
    {
        var properties = SpectralProperty.None;
        
        try
        {
            var eigendecomposition = matrix.Evd();
            var eigenvalues = eigendecomposition.EigenValues.ToArray();
            
            var realCount = eigenvalues.Count(ev => Math.Abs(ev.Imaginary) < tolerance);
            var complexCount = eigenvalues.Length - realCount;
            
            if (realCount == eigenvalues.Length)
                properties |= SpectralProperty.RealEigenvalues;
            
            if (complexCount > 0)
                properties |= SpectralProperty.ComplexConjugatePairs;
            
            var realParts = eigenvalues.Select(ev => ev.Real).ToArray();
            var maxReal = realParts.Max();
            var minReal = realParts.Min();
            
            if (eigenvalues.Any(ev => Math.Abs(ev.Magnitude) < tolerance))
                properties |= SpectralProperty.ZeroEigenvalues;
            
            if (eigenvalues.Any(ev => ev.Real < -tolerance))
                properties |= SpectralProperty.NegativeEigenvalues;
            
            if (maxReal > 10)
                properties |= SpectralProperty.LargeEigenvalues;
            
            if (eigenvalues.All(ev => ev.Magnitude < 1))
                properties |= SpectralProperty.SmallEigenvalues;
            
            // Check for clustering
            var sortedMagnitudes = eigenvalues.Select(ev => ev.Magnitude).OrderBy(x => x).ToArray();
            var gaps = new List<double>();
            for (var i = 1; i < sortedMagnitudes.Length; i++)
                gaps.Add(sortedMagnitudes[i] - sortedMagnitudes[i - 1]);
            
            var avgGap = gaps.Average();
            var smallGaps = gaps.Count(g => g < avgGap * 0.1);
            
            if (smallGaps > eigenvalues.Length / 3)
                properties |= SpectralProperty.ClusteredEigenvalues;
            
            var largeGaps = gaps.Count(g => g > avgGap * 10);
            if (largeGaps > 0)
                properties |= SpectralProperty.WidelySeparatedEigenvalues;
        }
        catch
        {
            // If eigenvalue computation fails, return basic properties
            properties = SpectralProperty.RealEigenvalues;
        }
        
        return properties;
    }

    private static double EstimateConditionNumber(Matrix<double> matrix)
    {
        try
        {
            return matrix.ConditionNumber();
        }
        catch
        {
            try
            {
                var svd = matrix.Svd();
                var singularValues = svd.S.ToArray();
                return singularValues.Max() / Math.Max(singularValues.Min(), 1e-16);
            }
            catch
            {
                return double.PositiveInfinity;
            }
        }
    }

    private static Matrix<double>? ComputeAnalyticalSolution(Matrix<double> matrix, MatrixCategory category)
    {
        try
        {
            return category switch
            {
                MatrixCategory.Diagonal => ComputeDiagonalExponential(matrix),
                MatrixCategory.AnalyticalSolution => ComputeSpecialCaseExponential(matrix),
                _ => null
            };
        }
        catch
        {
            return null;
        }
    }

    private static Matrix<double> ComputeDiagonalExponential(Matrix<double> matrix)
    {
        var n = matrix.Size();
        var result = Matrix<double>.Build.Dense(n, n);
        
        for (var i = 0; i < n; i++)
        {
            result[i, i] = Math.Exp(matrix[i, i]);
        }
        
        return result;
    }

    private static Matrix<double> ComputeSpecialCaseExponential(Matrix<double> matrix)
    {
        var n = matrix.Size();
        
        // Check for nilpotent matrices
        var matrixSquared = matrix * matrix;
        var isNilpotent2 = matrixSquared.ForAll(x => Math.Abs(x) < 1e-12);
        
        if (isNilpotent2)
        {
            return Matrix<double>.Build.DenseIdentity(n) + matrix;
        }
        
        var matrixCubed = matrixSquared * matrix;
        var isNilpotent3 = matrixCubed.ForAll(x => Math.Abs(x) < 1e-12);
        
        if (isNilpotent3)
        {
            return Matrix<double>.Build.DenseIdentity(n) + matrix + 0.5 * matrixSquared;
        }
        
        return matrix; // Fallback
    }

    private static string GenerateDescription(MatrixCategory category, int dimension, 
        SpectralProperty spectralProps, double conditionNumber)
    {
        var desc = $"{dimension}×{dimension} {category} matrix";
        
        if (conditionNumber < 1e3)
            desc += " (well-conditioned)";
        else if (conditionNumber < 1e6)
            desc += " (moderately ill-conditioned)";
        else if (conditionNumber < 1e12)
            desc += " (ill-conditioned)";
        else
            desc += " (severely ill-conditioned)";
        
        if (spectralProps.HasFlag(SpectralProperty.ComplexConjugatePairs))
            desc += ", complex eigenvalues";
        else if (spectralProps.HasFlag(SpectralProperty.RealEigenvalues))
            desc += ", real eigenvalues";
        
        return desc;
    }

    private static void PopulateMetadata(Dictionary<string, object> metadata, Matrix<double> matrix,
        MatrixCategory category, SpectralProperty spectralProps)
    {
        metadata["MatrixProperties"] = MatrixPropertiesUtils.AnalyzeProperties(matrix);
        metadata["SpectralRadius"] = EstimateSpectralRadius(matrix);
        metadata["Rank"] = EstimateRank(matrix);
        metadata["IsNormal"] = IsApproximatelyNormal(matrix);
        metadata["GenerationTimestamp"] = DateTime.UtcNow;
    }

    private static double EstimateSpectralRadius(Matrix<double> matrix)
    {
        try
        {
            var eigenvalues = matrix.Evd().EigenValues;
            return eigenvalues.Max(ev => ev.Magnitude);
        }
        catch
        {
            return matrix.OneNorm(); // Upper bound fallback
        }
    }

    private static int EstimateRank(Matrix<double> matrix, double tolerance = 1e-12)
    {
        try
        {
            var svd = matrix.Svd();
            return svd.S.Count(s => s > tolerance);
        }
        catch
        {
            return matrix.Size(); // Assume full rank
        }
    }

    private static bool IsApproximatelyNormal(Matrix<double> matrix, double tolerance = 1e-12)
    {
        try
        {
            var AAT = matrix * matrix.Transpose();
            var ATA = matrix.Transpose() * matrix;
            var diff = AAT - ATA;
            return diff.ForAll(x => Math.Abs(x) < tolerance);
        }
        catch
        {
            return false;
        }
    }

    #endregion
}