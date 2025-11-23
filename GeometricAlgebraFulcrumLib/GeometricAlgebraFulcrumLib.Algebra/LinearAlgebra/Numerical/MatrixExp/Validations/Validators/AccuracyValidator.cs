using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.MatrixExp.Validations.TestCases;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.MatrixExp.Validations.Validators;

/// <summary>
/// Comprehensive accuracy validation for matrix exponential algorithms.
/// Implements multiple validation methods including functional equations,
/// error bounds, and comparison with analytical solutions.
/// </summary>
public static class AccuracyValidator
{
    /// <summary>
    /// Validation methods available for testing accuracy.
    /// </summary>
    [Flags]
    public enum ValidationMethod
    {
        None = 0,
        FunctionalEquations = 1 << 0,
        AnalyticalComparison = 1 << 1,
        BackwardError = 1 << 2,
        ForwardError = 1 << 3,
        ResidualBounds = 1 << 4,
        OrthogonalityPreservation = 1 << 5,
        DeterminantProperty = 1 << 6,
        SymmetryPreservation = 1 << 7,
        ConditionNumberAnalysis = 1 << 8,
        ConvergenceAnalysis = 1 << 9,
        All = FunctionalEquations | AnalyticalComparison | BackwardError | 
              ForwardError | ResidualBounds | OrthogonalityPreservation | 
              DeterminantProperty | SymmetryPreservation | ConditionNumberAnalysis | 
              ConvergenceAnalysis
    }

    /// <summary>
    /// Validation result for a single test case.
    /// </summary>
    public sealed class ValidationResult
    {
        public required string TestName { get; init; }
        public required string AlgorithmName { get; init; }
        public required bool Passed { get; init; }
        public required double MaxError { get; init; }
        public required double RelativeError { get; init; }
        public required double BackwardError { get; init; }
        public required ValidationMethod MethodsUsed { get; init; }
        public required TimeSpan ComputationTime { get; init; }
        public required Dictionary<string, double> DetailedErrors { get; init; }
        public required List<string> Warnings { get; init; }
        public required List<string> Failures { get; init; }
        public string Details { get; init; } = string.Empty;
        public Dictionary<string, object> Metadata { get; init; } = new();
    }

    /// <summary>
    /// Configuration for accuracy validation.
    /// </summary>
    public sealed class Config
    {
        public ValidationMethod Methods { get; set; } = ValidationMethod.All;
        public double AbsoluteTolerance { get; set; } = 1e-12;
        public double RelativeTolerance { get; set; } = 1e-10;
        public double BackwardErrorTolerance { get; set; } = 1e-14;
        public double OrthogonalityTolerance { get; set; } = 1e-12;
        public bool IncludeDetailedAnalysis { get; set; } = true;
        public bool StopOnFirstFailure { get; set; } = false;
        public int MaxIterationsForConvergence { get; set; } = 100;
        public double ConvergenceTolerance { get; set; } = 1e-15;
    }

    /// <summary>
    /// Validates the accuracy of a matrix exponential computation.
    /// </summary>
    public static ValidationResult ValidateAccuracy(
        MatrixTestCaseGenerator.TestMatrix testCase,
        Matrix<double> computedResult,
        string algorithmName,
        TimeSpan computationTime,
        Config? config = null)
    {
        config ??= new Config();
        
        var detailedErrors = new Dictionary<string, double>();
        var warnings = new List<string>();
        var failures = new List<string>();
        var metadata = new Dictionary<string, object>();

        var maxError = 0.0;
        var relativeError = 0.0;
        var backwardError = 0.0;
        var passed = true;

        try
        {
            // Method 1: Functional equations validation
            if (config.Methods.HasFlag(ValidationMethod.FunctionalEquations))
            {
                var funcResult = ValidateFunctionalEquations(testCase.Matrix, computedResult, config);
                detailedErrors.AddRange(funcResult.errors);
                warnings.AddRange(funcResult.warnings);
                if (!funcResult.passed) failures.AddRange(funcResult.failures);
                maxError = Math.Max(maxError, funcResult.maxError);
                passed &= funcResult.passed;
            }

            // Method 2: Analytical solution comparison
            if (config.Methods.HasFlag(ValidationMethod.AnalyticalComparison) && 
                testCase.AnalyticalSolution != null)
            {
                var analyticalResult = ValidateAgainstAnalyticalSolution(
                    computedResult, testCase.AnalyticalSolution, config);
                detailedErrors.AddRange(analyticalResult.errors);
                warnings.AddRange(analyticalResult.warnings);
                if (!analyticalResult.passed) failures.AddRange(analyticalResult.failures);
                maxError = Math.Max(maxError, analyticalResult.maxError);
                relativeError = Math.Max(relativeError, analyticalResult.relativeError);
                passed &= analyticalResult.passed;
            }

            // Method 3: Backward error analysis
            if (config.Methods.HasFlag(ValidationMethod.BackwardError))
            {
                var backwardResult = ComputeBackwardError(testCase.Matrix, computedResult, config);
                detailedErrors["BackwardError"] = backwardResult.error;
                backwardError = backwardResult.error;
                if (!backwardResult.passed)
                {
                    failures.Add($"Backward error {backwardResult.error:E2} exceeds tolerance {config.BackwardErrorTolerance:E2}");
                    passed = false;
                }
            }

            // Method 4: Forward error bounds
            if (config.Methods.HasFlag(ValidationMethod.ForwardError))
            {
                var forwardResult = EstimateForwardError(testCase.Matrix, computedResult, config);
                detailedErrors.AddRange(forwardResult.errors);
                warnings.AddRange(forwardResult.warnings);
                if (!forwardResult.passed) failures.AddRange(forwardResult.failures);
                passed &= forwardResult.passed;
            }

            // Method 5: Residual bounds
            if (config.Methods.HasFlag(ValidationMethod.ResidualBounds))
            {
                var residualResult = ValidateResidualBounds(testCase.Matrix, computedResult, config);
                detailedErrors.AddRange(residualResult.errors);
                warnings.AddRange(residualResult.warnings);
                if (!residualResult.passed) failures.AddRange(residualResult.failures);
                passed &= residualResult.passed;
            }

            // Method 6: Orthogonality preservation (for skew-symmetric matrices)
            if (config.Methods.HasFlag(ValidationMethod.OrthogonalityPreservation) &&
                testCase.Category == MatrixTestCaseGenerator.MatrixCategory.SkewSymmetric)
            {
                var orthResult = ValidateOrthogonality(computedResult, config);
                detailedErrors.AddRange(orthResult.errors);
                warnings.AddRange(orthResult.warnings);
                if (!orthResult.passed) failures.AddRange(orthResult.failures);
                passed &= orthResult.passed;
            }

            // Method 7: Determinant property validation
            if (config.Methods.HasFlag(ValidationMethod.DeterminantProperty))
            {
                var detResult = ValidateDeterminantProperty(testCase.Matrix, computedResult, config);
                detailedErrors.AddRange(detResult.errors);
                warnings.AddRange(detResult.warnings);
                if (!detResult.passed) failures.AddRange(detResult.failures);
                passed &= detResult.passed;
            }

            // Method 8: Symmetry preservation
            if (config.Methods.HasFlag(ValidationMethod.SymmetryPreservation) &&
                testCase.Category == MatrixTestCaseGenerator.MatrixCategory.Symmetric)
            {
                var symResult = ValidateSymmetryPreservation(testCase.Matrix, computedResult, config);
                detailedErrors.AddRange(symResult.errors);
                warnings.AddRange(symResult.warnings);
                if (!symResult.passed) failures.AddRange(symResult.failures);
                passed &= symResult.passed;
            }

            // Method 9: Condition number analysis
            if (config.Methods.HasFlag(ValidationMethod.ConditionNumberAnalysis))
            {
                var condResult = AnalyzeConditionNumber(testCase.Matrix, computedResult, config);
                detailedErrors.AddRange(condResult.errors);
                warnings.AddRange(condResult.warnings);
                metadata.AddRange(condResult.metadata);
            }

            // Method 10: Convergence analysis
            if (config.Methods.HasFlag(ValidationMethod.ConvergenceAnalysis))
            {
                var convResult = AnalyzeConvergence(testCase.Matrix, algorithmName, config);
                warnings.AddRange(convResult.warnings);
                metadata.AddRange(convResult.metadata);
            }

            // Compute overall relative error if not set
            if (relativeError == 0.0 && maxError > 0.0)
            {
                var referenceDiff = testCase.AnalyticalSolution?.FrobeniusNorm() ?? computedResult.FrobeniusNorm();
                relativeError = referenceDiff > 0 ? maxError / referenceDiff : maxError;
            }
        }
        catch (Exception ex)
        {
            failures.Add($"Validation failed with exception: {ex.Message}");
            passed = false;
        }

        return new ValidationResult
        {
            TestName = testCase.Name,
            AlgorithmName = algorithmName,
            Passed = passed,
            MaxError = maxError,
            RelativeError = relativeError,
            BackwardError = backwardError,
            MethodsUsed = config.Methods,
            ComputationTime = computationTime,
            DetailedErrors = detailedErrors,
            Warnings = warnings,
            Failures = failures,
            Details = GenerateValidationDetails(detailedErrors, warnings, failures),
            Metadata = metadata
        };
    }

    #region Validation Methods

    private static (bool passed, double maxError, Dictionary<string, double> errors, 
                   List<string> warnings, List<string> failures) 
        ValidateFunctionalEquations(Matrix<double> matrix, Matrix<double> expA, Config config)
    {
        var errors = new Dictionary<string, double>();
        var warnings = new List<string>();
        var failures = new List<string>();
        var maxError = 0.0;
        var passed = true;

        try
        {
            var n = matrix.Size();
            var tolerance = config.AbsoluteTolerance;

            // Test 1: exp(A) * exp(-A) = I
            var expMinusA = (-matrix).MatrixExpViaPadeSimple(config.AbsoluteTolerance);
            var product = expA * expMinusA;
            var identity = Matrix<double>.Build.DenseIdentity(n);
            var identityError = (product - identity).FrobeniusNorm();
            
            errors["IdentityProperty"] = identityError;
            maxError = Math.Max(maxError, identityError);
            
            if (identityError > tolerance)
            {
                failures.Add($"Identity property failed: ||exp(A)*exp(-A) - I|| = {identityError:E2}");
                passed = false;
            }

            // Test 2: exp(0) = I (if matrix is near zero)
            if (matrix.FrobeniusNorm() < 1e-14)
            {
                var zeroError = (expA - identity).FrobeniusNorm();
                errors["ZeroMatrix"] = zeroError;
                maxError = Math.Max(maxError, zeroError);
                
                if (zeroError > tolerance)
                {
                    failures.Add($"Zero matrix property failed: ||exp(0) - I|| = {zeroError:E2}");
                    passed = false;
                }
            }

            // Test 3: exp(A + B) = exp(A)*exp(B) for commuting matrices
            // We'll test with A and kI where k is a scalar
            var k = 0.1;
            var kI = k * identity;
            var expAkI = (matrix + kI).MatrixExpViaPadeSimple(config.AbsoluteTolerance);
            var expkI = (Math.Exp(k) * identity);
            var expectedProduct = expA * expkI;
            var commutingError = (expAkI - expectedProduct).FrobeniusNorm();
            
            errors["CommutingProperty"] = commutingError;
            maxError = Math.Max(maxError, commutingError);
            
            if (commutingError > tolerance * 10) // More lenient for commuting property
            {
                warnings.Add($"Commuting property deviation: ||exp(A+kI) - exp(A)*exp(kI)|| = {commutingError:E2}");
            }

            // Test 4: Scaling property: exp(tA) where t is small
            if (matrix.FrobeniusNorm() < 10) // Only for reasonably sized matrices
            {
                var t = 0.5;
                var expTA = (t * matrix).MatrixExpViaPadeSimple(config.AbsoluteTolerance);
                
                // Compare with series approximation for small tA
                var tA = t * matrix;
                var seriesApprox = identity + tA + 0.5 * (tA * tA);
                var scalingError = (expTA - seriesApprox).FrobeniusNorm();
                
                errors["ScalingProperty"] = scalingError;
                maxError = Math.Max(maxError, scalingError);
                
                if (scalingError > tolerance * 100)
                {
                    warnings.Add($"Scaling property large deviation: {scalingError:E2}");
                }
            }
        }
        catch (Exception ex)
        {
            failures.Add($"Functional equations validation failed: {ex.Message}");
            passed = false;
        }

        return (passed, maxError, errors, warnings, failures);
    }

    private static (bool passed, double maxError, double relativeError,
                   Dictionary<string, double> errors, List<string> warnings, List<string> failures)
        ValidateAgainstAnalyticalSolution(Matrix<double> computed, Matrix<double> analytical, Config config)
    {
        var errors = new Dictionary<string, double>();
        var warnings = new List<string>();
        var failures = new List<string>();

        var difference = computed - analytical;
        var absoluteError = difference.FrobeniusNorm();
        var relativeError = absoluteError / Math.Max(analytical.FrobeniusNorm(), 1e-16);
        
        errors["AbsoluteError"] = absoluteError;
        errors["RelativeError"] = relativeError;
        
        var passed = absoluteError <= config.AbsoluteTolerance && 
                     relativeError <= config.RelativeTolerance;

        if (!passed)
        {
            failures.Add($"Analytical comparison failed: absolute={absoluteError:E2}, relative={relativeError:E2}");
        }

        // Element-wise analysis
        var maxElementError = 0.0;
        var n = computed.RowCount;
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                var elemError = Math.Abs(computed[i, j] - analytical[i, j]);
                maxElementError = Math.Max(maxElementError, elemError);
            }
        }
        
        errors["MaxElementError"] = maxElementError;

        return (passed, absoluteError, relativeError, errors, warnings, failures);
    }

    private static (bool passed, double error) ComputeBackwardError(
        Matrix<double> matrix, Matrix<double> expA, Config config)
    {
        try
        {
            // Backward error: ||exp(A+E) - F|| where F is computed result
            // We approximate this by checking how well F satisfies the differential equation
            
            // For exp(A), we have d/dt exp(tA)|_{t=1} = A * exp(A) = exp(A) * A
            var productLeft = matrix * expA;
            var productRight = expA * matrix;
            
            // For normal matrices, these should be equal
            var backwardError = (productLeft - productRight).FrobeniusNorm() / 
                               Math.Max(expA.FrobeniusNorm(), 1.0);
            
            var passed = backwardError <= config.BackwardErrorTolerance;
            
            return (passed, backwardError);
        }
        catch
        {
            return (false, double.PositiveInfinity);
        }
    }

    private static (bool passed, Dictionary<string, double> errors, 
                   List<string> warnings, List<string> failures)
        EstimateForwardError(Matrix<double> matrix, Matrix<double> expA, Config config)
    {
        var errors = new Dictionary<string, double>();
        var warnings = new List<string>();
        var failures = new List<string>();
        var passed = true;

        try
        {
            var matrixNorm = matrix.OneNorm();
            var resultNorm = expA.FrobeniusNorm();
            
            // Estimate forward error using theoretical bounds
            // ||exp(A)|| ≤ exp(||A||)
            var theoreticalBound = Math.Exp(matrixNorm);
            var boundRatio = resultNorm / theoreticalBound;
            
            errors["TheoreticalBoundRatio"] = boundRatio;
            
            if (boundRatio > 1.1) // Allow 10% tolerance for numerical errors
            {
                warnings.Add($"Result norm {resultNorm:E2} exceeds theoretical bound {theoreticalBound:E2}");
            }
            
            // Condition number based error estimate
            var conditionNumber = matrix.EstimateConditionNumber(MatrixPropertiesUtils.AnalyzeProperties(matrix));
            var machineEps = 2.22e-16;
            var expectedError = conditionNumber * machineEps * Math.Exp(matrixNorm);
            
            errors["EstimatedForwardError"] = expectedError;
            
            if (expectedError > config.RelativeTolerance)
            {
                warnings.Add($"Expected forward error {expectedError:E2} may exceed tolerance due to conditioning");
            }
        }
        catch (Exception ex)
        {
            failures.Add($"Forward error estimation failed: {ex.Message}");
            passed = false;
        }

        return (passed, errors, warnings, failures);
    }

    private static (bool passed, Dictionary<string, double> errors,
                   List<string> warnings, List<string> failures)
        ValidateResidualBounds(Matrix<double> matrix, Matrix<double> expA, Config config)
    {
        var errors = new Dictionary<string, double>();
        var warnings = new List<string>();
        var failures = new List<string>();
        var passed = true;

        try
        {
            var n = matrix.Size();
            var identity = Matrix<double>.Build.DenseIdentity(n);
            
            // Residual: ||exp(A) - (I + A + A²/2! + A³/3! + ...)|| for small ||A||
            if (matrix.FrobeniusNorm() < 1.0)
            {
                var A2 = matrix * matrix;
                var A3 = A2 * matrix;
                var taylorApprox = identity + matrix + 0.5 * A2 + (1.0/6.0) * A3;
                
                var residual = (expA - taylorApprox).FrobeniusNorm();
                errors["TaylorResidual"] = residual;
                
                // Estimate expected residual
                var expectedResidual = Math.Pow(matrix.FrobeniusNorm(), 4) / 24.0;
                errors["ExpectedTaylorResidual"] = expectedResidual;
                
                if (residual > expectedResidual * 100)
                {
                    warnings.Add($"Taylor residual {residual:E2} much larger than expected {expectedResidual:E2}");
                }
            }
            
            // Matrix equation residual: ||A * exp(A) - exp(A) * A|| (for normal matrices)
            var leftProduct = matrix * expA;
            var rightProduct = expA * matrix;
            var commutatorResidual = (leftProduct - rightProduct).FrobeniusNorm();
            
            errors["CommutatorResidual"] = commutatorResidual;
            
            var expectedCommutatorResidual = config.AbsoluteTolerance * expA.FrobeniusNorm();
            if (commutatorResidual > expectedCommutatorResidual * 10)
            {
                warnings.Add($"Large commutator residual suggests numerical issues: {commutatorResidual:E2}");
            }
        }
        catch (Exception ex)
        {
            failures.Add($"Residual bounds validation failed: {ex.Message}");
            passed = false;
        }

        return (passed, errors, warnings, failures);
    }

    private static (bool passed, Dictionary<string, double> errors,
                   List<string> warnings, List<string> failures)
        ValidateOrthogonality(Matrix<double> expA, Config config)
    {
        var errors = new Dictionary<string, double>();
        var warnings = new List<string>();
        var failures = new List<string>();

        var (isOrthogonal, maxError) = expA.VerifyOrthogonality(config.OrthogonalityTolerance);
        
        errors["OrthogonalityError"] = maxError;
        
        var passed = isOrthogonal;
        if (!passed)
        {
            failures.Add($"Orthogonality test failed: max error = {maxError:E2}");
        }

        // Check determinant (should be ±1 for orthogonal matrices)
        try
        {
            var det = expA.Determinant();
            var detError = Math.Abs(Math.Abs(det) - 1.0);
            errors["DeterminantError"] = detError;
            
            if (detError > config.OrthogonalityTolerance * 10)
            {
                warnings.Add($"Determinant {det:F6} deviates from ±1 by {detError:E2}");
            }
        }
        catch (Exception ex)
        {
            warnings.Add($"Could not compute determinant: {ex.Message}");
        }

        return (passed, errors, warnings, failures);
    }

    private static (bool passed, Dictionary<string, double> errors,
                   List<string> warnings, List<string> failures)
        ValidateDeterminantProperty(Matrix<double> matrix, Matrix<double> expA, Config config)
    {
        var errors = new Dictionary<string, double>();
        var warnings = new List<string>();
        var failures = new List<string>();
        var passed = true;

        try
        {
            var trace = 0.0;
            var n = matrix.Size();
            for (var i = 0; i < n; i++)
            {
                trace += matrix[i, i];
            }
            
            var expectedDet = Math.Exp(trace);
            var computedDet = Math.Abs(expA.Determinant());
            
            var detError = Math.Abs(computedDet - expectedDet) / Math.Max(Math.Abs(expectedDet), 1.0);
            errors["DeterminantPropertyError"] = detError;
            
            if (detError > config.RelativeTolerance * 10)
            {
                failures.Add($"Determinant property failed: computed={computedDet:E6}, expected={expectedDet:E6}");
                passed = false;
            }
            else if (detError > config.RelativeTolerance)
            {
                warnings.Add($"Determinant property has large relative error: {detError:E2}");
            }
        }
        catch (Exception ex)
        {
            warnings.Add($"Could not validate determinant property: {ex.Message}");
        }

        return (passed, errors, warnings, failures);
    }

    private static (bool passed, Dictionary<string, double> errors,
                   List<string> warnings, List<string> failures)
        ValidateSymmetryPreservation(Matrix<double> matrix, Matrix<double> expA, Config config)
    {
        var errors = new Dictionary<string, double>();
        var warnings = new List<string>();
        var failures = new List<string>();

        // Check if exp(A) is symmetric when A is symmetric
        var symmetryError = 0.0;
        var n = expA.Size();
        
        for (var i = 0; i < n; i++)
        {
            for (var j = i + 1; j < n; j++)
            {
                var error = Math.Abs(expA[i, j] - expA[j, i]);
                symmetryError = Math.Max(symmetryError, error);
            }
        }
        
        errors["SymmetryError"] = symmetryError;
        
        var passed = symmetryError <= config.AbsoluteTolerance;
        if (!passed)
        {
            failures.Add($"Symmetry preservation failed: max error = {symmetryError:E2}");
        }

        return (passed, errors, warnings, failures);
    }

    private static (Dictionary<string, double> errors, List<string> warnings, 
                   Dictionary<string, object> metadata)
        AnalyzeConditionNumber(Matrix<double> matrix, Matrix<double> expA, Config config)
    {
        var errors = new Dictionary<string, double>();
        var warnings = new List<string>();
        var metadata = new Dictionary<string, object>();

        try
        {
            var matrixCondition = matrix.EstimateConditionNumber(MatrixPropertiesUtils.AnalyzeProperties(matrix));
            var resultCondition = expA.EstimateConditionNumber(MatrixPropertiesUtils.AnalyzeProperties(expA));
            
            errors["InputConditionNumber"] = matrixCondition;
            errors["ResultConditionNumber"] = resultCondition;
            
            metadata["ConditionNumberRatio"] = resultCondition / Math.Max(matrixCondition, 1.0);
            metadata["LogConditionNumber"] = Math.Log10(Math.Max(matrixCondition, 1.0));
            
            if (matrixCondition > 1e12)
            {
                warnings.Add($"Input matrix is severely ill-conditioned (κ = {matrixCondition:E2})");
            }
            else if (matrixCondition > 1e6)
            {
                warnings.Add($"Input matrix is ill-conditioned (κ = {matrixCondition:E2})");
            }
            
            if (resultCondition > matrixCondition * 1000)
            {
                warnings.Add($"Result condition number ({resultCondition:E2}) much larger than input ({matrixCondition:E2})");
            }
        }
        catch (Exception ex)
        {
            warnings.Add($"Condition number analysis failed: {ex.Message}");
        }

        return (errors, warnings, metadata);
    }

    private static (List<string> warnings, Dictionary<string, object> metadata)
        AnalyzeConvergence(Matrix<double> matrix, string algorithmName, Config config)
    {
        var warnings = new List<string>();
        var metadata = new Dictionary<string, object>();

        // This is a placeholder for convergence analysis
        // In a full implementation, this would analyze convergence rates
        // for iterative methods, series truncation errors, etc.
        
        var matrixNorm = matrix.OneNorm();
        metadata["MatrixNorm"] = matrixNorm;
        metadata["AlgorithmUsed"] = algorithmName;
        
        if (matrixNorm > 100)
        {
            warnings.Add($"Large matrix norm ({matrixNorm:E2}) may affect convergence");
        }

        return (warnings, metadata);
    }

    #endregion

    private static string GenerateValidationDetails(Dictionary<string, double> errors,
        List<string> warnings, List<string> failures)
    {
        var details = new System.Text.StringBuilder();
        
        if (errors.Count > 0)
        {
            details.AppendLine("Error Analysis:");
            foreach (var (key, value) in errors)
            {
                details.AppendLine($"  {key}: {value:E6}");
            }
        }
        
        if (warnings.Count > 0)
        {
            details.AppendLine("Warnings:");
            foreach (var warning in warnings)
            {
                details.AppendLine($"  - {warning}");
            }
        }
        
        if (failures.Count > 0)
        {
            details.AppendLine("Failures:");
            foreach (var failure in failures)
            {
                details.AppendLine($"  - {failure}");
            }
        }
        
        return details.ToString();
    }
}

/// <summary>
/// Extension methods for dictionary operations.
/// </summary>
internal static class DictionaryExtensions
{
    public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, 
        Dictionary<TKey, TValue> other) where TKey : notnull
    {
        foreach (var (key, value) in other)
        {
            dictionary[key] = value;
        }
    }
}