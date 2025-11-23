using System.Diagnostics;
using System.Text.Json;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.MatrixExp.Validations.TestCases;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.MatrixExp.Validations.Validators;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.MatrixExp.Validations.TestRunner;

/// <summary>
/// Comprehensive validation suite for matrix exponential algorithms.
/// Coordinates test case generation, algorithm execution, accuracy validation, and reporting.
/// </summary>
public class ValidationSuite
{
    /// <summary>
    /// Configuration for the validation suite.
    /// </summary>
    public sealed class Config
    {
        public MatrixTestCaseGenerator.GenerationConfig TestCaseConfig { get; set; } = new();
        public AccuracyValidator.Config ValidationConfig { get; set; } = new();
        public string[] AlgorithmsToTest { get; set; } = ["Pade", "Schur", "Eigen", "SkewSymmetric", "Taylor"];
        public bool IncludePerformanceBenchmarks { get; set; } = true;
        public bool GenerateDetailedReports { get; set; } = true;
        public string OutputDirectory { get; set; } = "ValidationResults";
        public bool StopOnCriticalFailure { get; set; } = false;
        public double MaxTestTimeSeconds { get; set; } = 60.0;
        public int MaxConcurrentTests { get; set; } = Environment.ProcessorCount;
        public ReportFormat[] ReportFormats { get; set; } = [ReportFormat.Json, ReportFormat.Html, ReportFormat.Csv];
    }

    /// <summary>
    /// Supported report formats.
    /// </summary>
    public enum ReportFormat
    {
        Json,
        Html,
        Csv,
        Xml
    }

    /// <summary>
    /// Test execution result for a single test case and algorithm combination.
    /// </summary>
    public sealed class TestExecution
    {
        public required string TestCaseName { get; init; }
        public required string AlgorithmName { get; init; }
        public required bool Success { get; init; }
        public required TimeSpan ExecutionTime { get; init; }
        public required AccuracyValidator.ValidationResult ValidationResult { get; init; }
        public string? ErrorMessage { get; init; }
        public Dictionary<string, object> PerformanceMetrics { get; init; } = new();
        public long MemoryUsed { get; init; }
    }

    /// <summary>
    /// Complete validation suite results.
    /// </summary>
    public sealed class SuiteResults
    {
        public required DateTime StartTime { get; init; }
        public required DateTime EndTime { get; init; }
        public required TimeSpan TotalDuration { get; init; }
        public required int TotalTests { get; init; }
        public required int PassedTests { get; init; }
        public required int FailedTests { get; init; }
        public required List<TestExecution> TestExecutions { get; init; }
        public required Dictionary<string, AlgorithmSummary> AlgorithmSummaries { get; init; }
        public required Dictionary<string, CategorySummary> CategorySummaries { get; init; }
        public required List<string> CriticalIssues { get; init; }
        public required SystemInfo SystemInformation { get; init; }
    }

    /// <summary>
    /// Summary statistics for an algorithm.
    /// </summary>
    public sealed class AlgorithmSummary
    {
        public required string Name { get; init; }
        public required int TotalTests { get; init; }
        public required int PassedTests { get; init; }
        public required int FailedTests { get; init; }
        public required double AverageExecutionTime { get; init; }
        public required double AverageAccuracy { get; init; }
        public required double WorstCaseError { get; init; }
        public required List<string> ProblematicCategories { get; init; }
        public required Dictionary<string, double> PerformanceMetrics { get; init; }
    }

    /// <summary>
    /// Summary statistics for a matrix category.
    /// </summary>
    public sealed class CategorySummary
    {
        public required string Name { get; init; }
        public required int TotalTests { get; init; }
        public required int PassedTests { get; init; }
        public required double AverageAccuracy { get; init; }
        public required string BestAlgorithm { get; init; }
        public required string WorstAlgorithm { get; init; }
        public required List<string> Issues { get; init; }
    }

    /// <summary>
    /// System information for reproducibility.
    /// </summary>
    public sealed class SystemInfo
    {
        public required string Platform { get; init; }
        public required string RuntimeVersion { get; init; }
        public required int ProcessorCount { get; init; }
        public required long AvailableMemory { get; init; }
        public required DateTime Timestamp { get; init; }
        public required string HostName { get; init; }
    }

    private readonly Config _config;
    private readonly MatrixExpComposer _composer;

    public ValidationSuite(Config? config = null)
    {
        _config = config ?? new Config();
        _composer = new MatrixExpComposer(message => Console.WriteLine($"[Composer] {message}"));
    }

    /// <summary>
    /// Runs the complete validation suite.
    /// </summary>
    public async Task<SuiteResults> RunValidationSuiteAsync(CancellationToken cancellationToken = default)
    {
        var startTime = DateTime.UtcNow;
        var testExecutions = new List<TestExecution>();
        var criticalIssues = new List<string>();

        Console.WriteLine("=== Matrix Exponential Validation Suite ===");
        Console.WriteLine($"Started at: {startTime:yyyy-MM-dd HH:mm:ss} UTC");
        Console.WriteLine();

        try
        {
            // Ensure output directory exists
            Directory.CreateDirectory(_config.OutputDirectory);

            // Step 1: Generate test cases
            Console.WriteLine("Generating test cases...");
            var testCases = MatrixTestCaseGenerator.GenerateTestCases(_config.TestCaseConfig);
            Console.WriteLine($"Generated {testCases.Count} test cases");
            Console.WriteLine();

            // Step 2: Execute tests
            Console.WriteLine("Executing validation tests...");
            var semaphore = new SemaphoreSlim(_config.MaxConcurrentTests);
            var tasks = new List<Task<TestExecution?>>();

            foreach (var testCase in testCases)
            {
                foreach (var algorithmName in _config.AlgorithmsToTest)
                {
                    tasks.Add(ExecuteTestAsync(testCase, algorithmName, semaphore, cancellationToken));
                }
            }

            var results = await Task.WhenAll(tasks);
            testExecutions.AddRange(results.Where(r => r != null)!);

            Console.WriteLine($"Completed {testExecutions.Count} test executions");
            Console.WriteLine();

            // Step 3: Analyze results
            var endTime = DateTime.UtcNow;
            var suiteResults = AnalyzeResults(testExecutions, startTime, endTime, criticalIssues);

            // Step 4: Generate reports
            Console.WriteLine("Generating reports...");
            await GenerateReportsAsync(suiteResults);
            Console.WriteLine($"Reports saved to: {_config.OutputDirectory}");

            // Step 5: Print summary
            PrintSummary(suiteResults);

            return suiteResults;
        }
        catch (Exception ex)
        {
            criticalIssues.Add($"Suite execution failed: {ex.Message}");
            var endTime = DateTime.UtcNow;
            
            return new SuiteResults
            {
                StartTime = startTime,
                EndTime = endTime,
                TotalDuration = endTime - startTime,
                TotalTests = testExecutions.Count,
                PassedTests = testExecutions.Count(t => t.ValidationResult.Passed),
                FailedTests = testExecutions.Count(t => !t.ValidationResult.Passed),
                TestExecutions = testExecutions,
                AlgorithmSummaries = new Dictionary<string, AlgorithmSummary>(),
                CategorySummaries = new Dictionary<string, CategorySummary>(),
                CriticalIssues = criticalIssues,
                SystemInformation = GetSystemInfo()
            };
        }
    }

    /// <summary>
    /// Executes a single test case with a specific algorithm.
    /// </summary>
    private async Task<TestExecution?> ExecuteTestAsync(
        MatrixTestCaseGenerator.TestMatrix testCase,
        string algorithmName,
        SemaphoreSlim semaphore,
        CancellationToken cancellationToken)
    {
        await semaphore.WaitAsync(cancellationToken);
        
        try
        {
            var stopwatch = Stopwatch.StartNew();
            var memoryBefore = GC.GetTotalMemory(false);
            
            Matrix<double>? result = null;
            string? errorMessage = null;
            var success = false;

            try
            {
                using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                cts.CancelAfter(TimeSpan.FromSeconds(_config.MaxTestTimeSeconds));

                result = await Task.Run(() => ExecuteAlgorithm(testCase.Matrix, algorithmName), cts.Token);
                success = result != null;
            }
            catch (OperationCanceledException)
            {
                errorMessage = $"Test timed out after {_config.MaxTestTimeSeconds} seconds";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                // For SkewSymmetric algorithm, preserve the specific validation message
                if (algorithmName.ToLowerInvariant() == "skewsymmetric" &&
                    ex is InvalidOperationException)
                {
                    errorMessage = ex.Message; // This will be the descriptive message about skew-symmetric validation
                }
            }

            stopwatch.Stop();
            var memoryAfter = GC.GetTotalMemory(false);
            var memoryUsed = memoryAfter - memoryBefore;

            if (!success || result == null)
            {
                return new TestExecution
                {
                    TestCaseName = testCase.Name,
                    AlgorithmName = algorithmName,
                    Success = false,
                    ExecutionTime = stopwatch.Elapsed,
                    ValidationResult = new AccuracyValidator.ValidationResult
                    {
                        TestName = testCase.Name,
                        AlgorithmName = algorithmName,
                        Passed = false,
                        MaxError = double.IsPositiveInfinity(double.PositiveInfinity) ? 1e6 : double.PositiveInfinity,
                        RelativeError = double.IsPositiveInfinity(double.PositiveInfinity) ? 1e6 : double.PositiveInfinity,
                        BackwardError = double.IsPositiveInfinity(double.PositiveInfinity) ? 1e6 : double.PositiveInfinity,
                        MethodsUsed = AccuracyValidator.ValidationMethod.None,
                        ComputationTime = stopwatch.Elapsed,
                        DetailedErrors = new Dictionary<string, double>(),
                        Warnings = new List<string>(),
                        Failures = new List<string> { errorMessage ?? "Unknown error" }
                    },
                    ErrorMessage = errorMessage,
                    MemoryUsed = memoryUsed
                };
            }

            // Validate accuracy
            var validationResult = AccuracyValidator.ValidateAccuracy(
                testCase, result, algorithmName, stopwatch.Elapsed, _config.ValidationConfig);

            var performanceMetrics = new Dictionary<string, object>
            {
                ["ExecutionTimeMs"] = stopwatch.Elapsed.TotalMilliseconds,
                ["MemoryUsedBytes"] = memoryUsed,
                ["MatrixSize"] = testCase.Size,
                ["ConditionNumber"] = testCase.ConditionNumber,
                ["OneNorm"] = testCase.OneNorm,
                ["FrobeniusNorm"] = testCase.FrobeniusNorm
            };

            return new TestExecution
            {
                TestCaseName = testCase.Name,
                AlgorithmName = algorithmName,
                Success = success && validationResult.Passed,
                ExecutionTime = stopwatch.Elapsed,
                ValidationResult = validationResult,
                ErrorMessage = errorMessage,
                PerformanceMetrics = performanceMetrics,
                MemoryUsed = memoryUsed
            };
        }
        finally
        {
            semaphore.Release();
        }
    }

    /// <summary>
    /// Executes a specific algorithm on a matrix.
    /// </summary>
    private Matrix<double>? ExecuteAlgorithm(Matrix<double> matrix, string algorithmName)
    {
        try
        {
            return algorithmName.ToLowerInvariant() switch
            {
                "pade" => matrix.MatrixExpViaPadeSimple(1e-14),
                "schur" => matrix.ExpViaSchur().result,
                "eigen" => matrix.ExpViaEigendecomposition(1e-14).matrixExp,
                "skewsymmetric" => matrix.IsNearSkewSymmetric(1e-12)
                    ? matrix.MatrixExpSkewSymmetric().MatrixExp
                    : throw new InvalidOperationException($"Matrix is not skew-symmetric (tolerance: 1e-12)"),
                "taylor" => matrix.ExpViaTaylorScaled(),
                "composer" => _composer.ComputeExponential(matrix).MatrixExp,
                _ => throw new ArgumentException($"Unknown algorithm: {algorithmName}")
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Algorithm {algorithmName} failed on matrix {matrix.RowCount}x{matrix.ColumnCount}: {ex.Message}");
            throw; // Re-throw to preserve the original exception message
        }
    }

    /// <summary>
    /// Analyzes test results and generates summaries.
    /// </summary>
    private SuiteResults AnalyzeResults(List<TestExecution> testExecutions, DateTime startTime, 
        DateTime endTime, List<string> criticalIssues)
    {
        var totalTests = testExecutions.Count;
        var passedTests = testExecutions.Count(t => t.Success);
        var failedTests = totalTests - passedTests;

        // Algorithm summaries
        var algorithmSummaries = new Dictionary<string, AlgorithmSummary>();
        var algorithmGroups = testExecutions.GroupBy(t => t.AlgorithmName);

        foreach (var group in algorithmGroups)
        {
            var executions = group.ToList();
            var passed = executions.Count(e => e.Success);
            var failed = executions.Count - passed;
            var avgTime = executions.Average(e => e.ExecutionTime.TotalMilliseconds);
            var avgAccuracy = executions.Where(e => e.Success)
                .DefaultIfEmpty()
                .Average(e => e?.ValidationResult.MaxError ?? 1e6);
            var worstError = executions.Max(e => e.ValidationResult.MaxError);
            
            var problematicCategories = executions
                .Where(e => !e.Success)
                .Select(e => ExtractCategory(e.TestCaseName))
                .GroupBy(c => c)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            var performanceMetrics = new Dictionary<string, double>
            {
                ["AverageTimeMs"] = avgTime,
                ["MinTimeMs"] = executions.Min(e => e.ExecutionTime.TotalMilliseconds),
                ["MaxTimeMs"] = executions.Max(e => e.ExecutionTime.TotalMilliseconds),
                ["AverageMemoryMB"] = executions.Average(e => e.MemoryUsed) / (1024.0 * 1024.0)
            };

            algorithmSummaries[group.Key] = new AlgorithmSummary
            {
                Name = group.Key,
                TotalTests = executions.Count,
                PassedTests = passed,
                FailedTests = failed,
                AverageExecutionTime = avgTime,
                AverageAccuracy = avgAccuracy,
                WorstCaseError = worstError,
                ProblematicCategories = problematicCategories,
                PerformanceMetrics = performanceMetrics
            };
        }

        // Category summaries
        var categorySummaries = new Dictionary<string, CategorySummary>();
        var categoryGroups = testExecutions.GroupBy(t => ExtractCategory(t.TestCaseName));

        foreach (var group in categoryGroups)
        {
            var executions = group.ToList();
            var passed = executions.Count(e => e.Success);
            var avgAccuracy = executions.Where(e => e.Success)
                .DefaultIfEmpty()
                .Average(e => e?.ValidationResult.MaxError ?? 1e6);
            
            var algorithmPerformance = executions
                .GroupBy(e => e.AlgorithmName)
                .ToDictionary(g => g.Key, g => g.Average(e => e.Success ? 1.0 : 0.0));
            
            var bestAlgorithm = algorithmPerformance.OrderByDescending(kvp => kvp.Value).First().Key;
            var worstAlgorithm = algorithmPerformance.OrderBy(kvp => kvp.Value).First().Key;
            
            var issues = executions
                .Where(e => !e.Success)
                .Select(e => e.ErrorMessage ?? "Unknown error")
                .Distinct()
                .ToList();

            categorySummaries[group.Key] = new CategorySummary
            {
                Name = group.Key,
                TotalTests = executions.Count,
                PassedTests = passed,
                AverageAccuracy = avgAccuracy,
                BestAlgorithm = bestAlgorithm,
                WorstAlgorithm = worstAlgorithm,
                Issues = issues
            };
        }

        return new SuiteResults
        {
            StartTime = startTime,
            EndTime = endTime,
            TotalDuration = endTime - startTime,
            TotalTests = totalTests,
            PassedTests = passedTests,
            FailedTests = failedTests,
            TestExecutions = testExecutions,
            AlgorithmSummaries = algorithmSummaries,
            CategorySummaries = categorySummaries,
            CriticalIssues = criticalIssues,
            SystemInformation = GetSystemInfo()
        };
    }

    /// <summary>
    /// Generates reports in specified formats.
    /// </summary>
    private async Task GenerateReportsAsync(SuiteResults results)
    {
        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        
        foreach (var format in _config.ReportFormats)
        {
            try
            {
                switch (format)
                {
                    case ReportFormat.Json:
                        await GenerateJsonReportAsync(results, timestamp);
                        break;
                    case ReportFormat.Html:
                        await GenerateHtmlReportAsync(results, timestamp);
                        break;
                    case ReportFormat.Csv:
                        await GenerateCsvReportAsync(results, timestamp);
                        break;
                    case ReportFormat.Xml:
                        await GenerateXmlReportAsync(results, timestamp);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate {format} report: {ex.Message}");
            }
        }
    }

    private async Task GenerateJsonReportAsync(SuiteResults results, string timestamp)
    {
        var fileName = Path.Combine(_config.OutputDirectory, $"validation_report_{timestamp}.json");
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals
        };
        
        var json = JsonSerializer.Serialize(results, options);
        await File.WriteAllTextAsync(fileName, json);
    }

    private async Task GenerateHtmlReportAsync(SuiteResults results, string timestamp)
    {
        var fileName = Path.Combine(_config.OutputDirectory, $"validation_report_{timestamp}.html");
        var html = GenerateHtmlReport(results);
        await File.WriteAllTextAsync(fileName, html);
    }

    private async Task GenerateCsvReportAsync(SuiteResults results, string timestamp)
    {
        var fileName = Path.Combine(_config.OutputDirectory, $"validation_results_{timestamp}.csv");
        
        var csv = new System.Text.StringBuilder();
        csv.AppendLine("TestCase,Algorithm,Success,ExecutionTime,MaxError,RelativeError,BackwardError,ErrorMessage");
        
        foreach (var execution in results.TestExecutions)
        {
            csv.AppendLine($"{execution.TestCaseName},{execution.AlgorithmName},{execution.Success}," +
                          $"{execution.ExecutionTime.TotalMilliseconds},{execution.ValidationResult.MaxError}," +
                          $"{execution.ValidationResult.RelativeError},{execution.ValidationResult.BackwardError}," +
                          $"\"{execution.ErrorMessage?.Replace("\"", "\"\"") ?? ""}\"");
        }
        
        await File.WriteAllTextAsync(fileName, csv.ToString());
    }

    private async Task GenerateXmlReportAsync(SuiteResults results, string timestamp)
    {
        var fileName = Path.Combine(_config.OutputDirectory, $"validation_report_{timestamp}.xml");
        
        // Simple XML generation - in production, use XDocument or similar
        var xml = new System.Text.StringBuilder();
        xml.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        xml.AppendLine("<ValidationResults>");
        xml.AppendLine($"  <Summary TotalTests=\"{results.TotalTests}\" PassedTests=\"{results.PassedTests}\" " +
                      $"FailedTests=\"{results.FailedTests}\" Duration=\"{results.TotalDuration}\" />");
        xml.AppendLine("</ValidationResults>");
        
        await File.WriteAllTextAsync(fileName, xml.ToString());
    }

    private string GenerateHtmlReport(SuiteResults results)
    {
        var html = new System.Text.StringBuilder();
        html.AppendLine("<!DOCTYPE html>");
        html.AppendLine("<html><head><title>Matrix Exponential Validation Report</title>");
        html.AppendLine("<style>");
        html.AppendLine("body { font-family: Arial, sans-serif; margin: 20px; }");
        html.AppendLine("table { border-collapse: collapse; width: 100%; }");
        html.AppendLine("th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }");
        html.AppendLine("th { background-color: #f2f2f2; }");
        html.AppendLine(".pass { color: green; }");
        html.AppendLine(".fail { color: red; }");
        html.AppendLine("</style></head><body>");
        
        html.AppendLine("<h1>Matrix Exponential Validation Report</h1>");
        html.AppendLine($"<p><strong>Generated:</strong> {results.EndTime:yyyy-MM-dd HH:mm:ss} UTC</p>");
        html.AppendLine($"<p><strong>Duration:</strong> {results.TotalDuration}</p>");
        html.AppendLine($"<p><strong>Total Tests:</strong> {results.TotalTests}</p>");
        html.AppendLine($"<p><strong>Passed:</strong> <span class=\"pass\">{results.PassedTests}</span></p>");
        html.AppendLine($"<p><strong>Failed:</strong> <span class=\"fail\">{results.FailedTests}</span></p>");
        
        html.AppendLine("<h2>Algorithm Summary</h2>");
        html.AppendLine("<table>");
        html.AppendLine("<tr><th>Algorithm</th><th>Total Tests</th><th>Passed</th><th>Failed</th><th>Avg Time (ms)</th><th>Avg Error</th></tr>");
        
        foreach (var summary in results.AlgorithmSummaries.Values)
        {
            html.AppendLine($"<tr><td>{summary.Name}</td><td>{summary.TotalTests}</td>" +
                           $"<td class=\"pass\">{summary.PassedTests}</td>" +
                           $"<td class=\"fail\">{summary.FailedTests}</td>" +
                           $"<td>{summary.AverageExecutionTime:F2}</td>" +
                           $"<td>{summary.AverageAccuracy:E2}</td></tr>");
        }
        
        html.AppendLine("</table>");
        html.AppendLine("</body></html>");
        
        return html.ToString();
    }

    private void PrintSummary(SuiteResults results)
    {
        Console.WriteLine("=== Validation Suite Summary ===");
        Console.WriteLine($"Total Duration: {results.TotalDuration}");
        Console.WriteLine($"Total Tests: {results.TotalTests}");
        Console.WriteLine($"Passed: {results.PassedTests} ({100.0 * results.PassedTests / results.TotalTests:F1}%)");
        Console.WriteLine($"Failed: {results.FailedTests} ({100.0 * results.FailedTests / results.TotalTests:F1}%)");
        Console.WriteLine();

        Console.WriteLine("Algorithm Performance:");
        foreach (var summary in results.AlgorithmSummaries.Values.OrderByDescending(s => s.PassedTests))
        {
            var successRate = 100.0 * summary.PassedTests / summary.TotalTests;
            Console.WriteLine($"  {summary.Name,-15}: {successRate,6:F1}% success, {summary.AverageExecutionTime,8:F2}ms avg");
        }
        Console.WriteLine();

        if (results.CriticalIssues.Any())
        {
            Console.WriteLine("Critical Issues:");
            foreach (var issue in results.CriticalIssues)
            {
                Console.WriteLine($"  - {issue}");
            }
        }

        Console.WriteLine($"Detailed reports available in: {_config.OutputDirectory}");
    }

    private SystemInfo GetSystemInfo()
    {
        return new SystemInfo
        {
            Platform = Environment.OSVersion.ToString(),
            RuntimeVersion = Environment.Version.ToString(),
            ProcessorCount = Environment.ProcessorCount,
            AvailableMemory = GC.GetTotalMemory(false),
            Timestamp = DateTime.UtcNow,
            HostName = Environment.MachineName
        };
    }

    private static string ExtractCategory(string testCaseName)
    {
        var parts = testCaseName.Split('_');
        return parts.Length > 0 ? parts[0] : "Unknown";
    }
}