//using MathNet.Numerics.LinearAlgebra;
//using MatrixExponential.Validation.TestCases;
//using MatrixExponential.Validation.TestRunner;
//using MatrixExponential.Validation.Validators;
//using Microsoft.CodeAnalysis.Options;

//namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.MatrixExp.Validations;

///// <summary>
///// Main entry point for the Matrix Exponential Validation Suite.
///// Provides comprehensive testing and validation of matrix exponential algorithms
///// across diverse matrix categories and spectral properties.
///// </summary>
//class Program
//{
//    static async Task<int> MainExamples(string[] args)
//    {
//        var rootCommand = new RootCommand("Matrix Exponential Validation Suite")
//        {
//            Description = "Comprehensive validation and testing suite for matrix exponential algorithms"
//        };

//        // Quick test command
//        var quickCommand = new Command("quick", "Run a quick validation test with minimal test cases");
//        quickCommand.SetHandler(async () => await RunQuickValidation());
//        rootCommand.AddCommand(quickCommand);

//        // Full test command
//        var fullCommand = new Command("full", "Run the complete validation suite");
        
//        var dimensionsOption = new Option<int[]>(
//            "--dimensions",
//            () => new[] { 1, 2, 3, 4, 8, 16, 32, 64, 128, 256 },
//            "Matrix dimensions to test");
        
//        var categoriesOption = new Option<string[]>(
//            "--categories", 
//            () => new[] { "all" },
//            "Matrix categories to test (all, dense, symmetric, skew, triangular, etc.)");
        
//        var algorithmsOption = new Option<string[]>(
//            "--algorithms",
//            () => new[] { "Pade", "Schur", "Eigen", "SkewSymmetric", "Taylor" },
//            "Algorithms to test");
        
//        var outputOption = new Option<string>(
//            "--output",
//            () => "ValidationResults",
//            "Output directory for results");

//        var toleranceOption = new Option<double>(
//            "--tolerance",
//            () => 1e-12,
//            "Numerical tolerance for validation");

//        var casesPerCategoryOption = new Option<int>(
//            "--cases-per-category",
//            () => 5,
//            "Number of test cases per category");

//        var timeoutOption = new Option<double>(
//            "--timeout",
//            () => 60.0,
//            "Maximum test time in seconds");

//        fullCommand.AddOption(dimensionsOption);
//        fullCommand.AddOption(categoriesOption);
//        fullCommand.AddOption(algorithmsOption);
//        fullCommand.AddOption(outputOption);
//        fullCommand.AddOption(toleranceOption);
//        fullCommand.AddOption(casesPerCategoryOption);
//        fullCommand.AddOption(timeoutOption);

//        fullCommand.SetHandler(async (int[] dimensions, string[] categories, string[] algorithms, 
//                                     string output, double tolerance, int casesPerCategory, double timeout) =>
//        {
//            await RunFullValidation(dimensions, categories, algorithms, output, tolerance, casesPerCategory, timeout);
//        }, dimensionsOption, categoriesOption, algorithmsOption, outputOption, 
//           toleranceOption, casesPerCategoryOption, timeoutOption);

//        rootCommand.AddCommand(fullCommand);

//        // Performance benchmark command
//        var benchmarkCommand = new Command("benchmark", "Run performance benchmarks");
        
//        var benchmarkSizeOption = new Option<int>(
//            "--size",
//            () => 64,
//            "Matrix size for benchmarks");
        
//        var benchmarkIterationsOption = new Option<int>(
//            "--iterations",
//            () => 10,
//            "Number of iterations per benchmark");

//        benchmarkCommand.AddOption(benchmarkSizeOption);
//        benchmarkCommand.AddOption(benchmarkIterationsOption);
        
//        benchmarkCommand.SetHandler(async (int size, int iterations) =>
//        {
//            await RunPerformanceBenchmark(size, iterations);
//        }, benchmarkSizeOption, benchmarkIterationsOption);

//        rootCommand.AddCommand(benchmarkCommand);

//        // Accuracy analysis command
//        var accuracyCommand = new Command("accuracy", "Run detailed accuracy analysis");
        
//        var matrixFileOption = new Option<string?>(
//            "--matrix-file",
//            "Load matrices from file (JSON format)");
        
//        var analysisMethodsOption = new Option<string[]>(
//            "--methods",
//            () => new[] { "all" },
//            "Validation methods to use");

//        accuracyCommand.AddOption(matrixFileOption);
//        accuracyCommand.AddOption(analysisMethodsOption);
//        accuracyCommand.AddOption(outputOption);

//        accuracyCommand.SetHandler(async (string? matrixFile, string[] methods, string output) =>
//        {
//            await RunAccuracyAnalysis(matrixFile, methods, output);
//        }, matrixFileOption, analysisMethodsOption, outputOption);

//        rootCommand.AddCommand(accuracyCommand);

//        try
//        {
//            return await rootCommand.InvokeAsync(args);
//        }
//        catch (Exception ex)
//        {
//            Console.ForegroundColor = ConsoleColor.Red;
//            Console.WriteLine($"Error: {ex.Message}");
//            Console.ResetColor();
//            return 1;
//        }
//    }

//    /// <summary>
//    /// Runs a quick validation with minimal test cases for rapid feedback.
//    /// </summary>
//    private static async Task RunQuickValidation()
//    {
//        Console.WriteLine("=== Quick Validation Mode ===");
//        Console.WriteLine("Running minimal test suite for rapid feedback...\n");

//        var config = new ValidationSuite.Config
//        {
//            TestCaseConfig = new MatrixTestCaseGenerator.GenerationConfig
//            {
//                Categories = MatrixTestCaseGenerator.MatrixCategory.GeneralDense |
//                            MatrixTestCaseGenerator.MatrixCategory.Symmetric |
//                            MatrixTestCaseGenerator.MatrixCategory.SkewSymmetric |
//                            MatrixTestCaseGenerator.MatrixCategory.Diagonal,
//                Dimensions = new[] { 1, 2, 4, 8, 16 },
//                CasesPerCategory = 2,
//                Random = new Random(42)
//            },
//            ValidationConfig = new AccuracyValidator.Config
//            {
//                Methods = AccuracyValidator.ValidationMethod.FunctionalEquations |
//                         AccuracyValidator.ValidationMethod.BackwardError |
//                         AccuracyValidator.ValidationMethod.OrthogonalityPreservation,
//                AbsoluteTolerance = 1e-10,
//                RelativeTolerance = 1e-8
//            },
//            AlgorithmsToTest = new[] { "Pade", "Schur", "SkewSymmetric" },
//            OutputDirectory = "QuickValidationResults",
//            ReportFormats = new[] { ValidationSuite.ReportFormat.Json, ValidationSuite.ReportFormat.Html },
//            MaxTestTimeSeconds = 30.0
//        };

//        var suite = new ValidationSuite(config);
//        var results = await suite.RunValidationSuiteAsync();

//        if (results.FailedTests > 0)
//        {
//            Console.ForegroundColor = ConsoleColor.Yellow;
//            Console.WriteLine($"\nQuick validation completed with {results.FailedTests} failures.");
//            Console.WriteLine("Run 'full' command for comprehensive testing.");
//            Console.ResetColor();
//        }
//        else
//        {
//            Console.ForegroundColor = ConsoleColor.Green;
//            Console.WriteLine("\nAll quick validation tests passed!");
//            Console.ResetColor();
//        }
//    }

//    /// <summary>
//    /// Runs the complete validation suite with all specified options.
//    /// </summary>
//    private static async Task RunFullValidation(int[] dimensions, string[] categories, string[] algorithms,
//        string output, double tolerance, int casesPerCategory, double timeout)
//    {
//        Console.WriteLine("=== Full Validation Suite ===");
//        Console.WriteLine("Running comprehensive matrix exponential validation...\n");

//        var matrixCategories = ParseCategories(categories);
        
//        var config = new ValidationSuite.Config
//        {
//            TestCaseConfig = new MatrixTestCaseGenerator.GenerationConfig
//            {
//                Categories = matrixCategories,
//                Dimensions = dimensions,
//                CasesPerCategory = casesPerCategory,
//                Tolerance = tolerance,
//                Random = new Random(42),
//                IncludeAnalyticalSolutions = true,
//                IncludeMetadata = true
//            },
//            ValidationConfig = new AccuracyValidator.Config
//            {
//                Methods = AccuracyValidator.ValidationMethod.All,
//                AbsoluteTolerance = tolerance,
//                RelativeTolerance = tolerance * 100,
//                BackwardErrorTolerance = tolerance,
//                OrthogonalityTolerance = tolerance,
//                IncludeDetailedAnalysis = true
//            },
//            AlgorithmsToTest = algorithms,
//            OutputDirectory = output,
//            ReportFormats = new[] { 
//                ValidationSuite.ReportFormat.Json, 
//                ValidationSuite.ReportFormat.Html, 
//                ValidationSuite.ReportFormat.Csv 
//            },
//            MaxTestTimeSeconds = timeout,
//            IncludePerformanceBenchmarks = true,
//            GenerateDetailedReports = true
//        };

//        var suite = new ValidationSuite(config);
//        var results = await suite.RunValidationSuiteAsync();

//        // Generate additional analysis
//        await GenerateDetailedAnalysis(results, output);

//        if (results.FailedTests > results.TotalTests * 0.05) // More than 5% failure rate
//        {
//            Console.ForegroundColor = ConsoleColor.Red;
//            Console.WriteLine($"\nValidation completed with significant issues: {results.FailedTests}/{results.TotalTests} failures");
//            Console.ResetColor();
//        }
//        else if (results.FailedTests > 0)
//        {
//            Console.ForegroundColor = ConsoleColor.Yellow;
//            Console.WriteLine($"\nValidation completed with minor issues: {results.FailedTests}/{results.TotalTests} failures");
//            Console.ResetColor();
//        }
//        else
//        {
//            Console.ForegroundColor = ConsoleColor.Green;
//            Console.WriteLine("\nAll validation tests passed successfully!");
//            Console.ResetColor();
//        }
//    }

//    /// <summary>
//    /// Runs performance benchmarks for matrix exponential algorithms.
//    /// </summary>
//    private static async Task RunPerformanceBenchmark(int size, int iterations)
//    {
//        Console.WriteLine($"=== Performance Benchmark (Matrix Size: {size}x{size}) ===");
//        Console.WriteLine($"Running {iterations} iterations per algorithm...\n");

//        var random = new Random(42);
//        var algorithms = new[] { "Pade", "Schur", "Eigen", "Taylor" };
//        var results = new Dictionary<string, List<TimeSpan>>();

//        // Generate test matrices
//        var testMatrices = new List<Matrix<double>>
//        {
//            random.GetSquareRealMatrix(size),
//            random.GetSymmetricRealMatrix(size),
//            random.GetSkewSymmetricRealMatrix(size)
//        };

//        foreach (var algorithm in algorithms)
//        {
//            results[algorithm] = new List<TimeSpan>();
//            Console.Write($"Benchmarking {algorithm,-15}: ");
            
//            for (var i = 0; i < iterations; i++)
//            {
//                var matrix = testMatrices[i % testMatrices.Count];
//                var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                
//                try
//                {
//                    var suite = new ValidationSuite();
//                    var methodInfo = typeof(ValidationSuite).GetMethod("ExecuteAlgorithm", 
//                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    
//                    if (methodInfo != null)
//                    {
//                        methodInfo.Invoke(suite, new object[] { matrix, algorithm });
//                    }
                    
//                    stopwatch.Stop();
//                    results[algorithm].Add(stopwatch.Elapsed);
//                    Console.Write(".");
//                }
//                catch
//                {
//                    stopwatch.Stop();
//                    Console.Write("x");
//                }
//            }
            
//            var avgTime = results[algorithm].Average(t => t.TotalMilliseconds);
//            Console.WriteLine($" Avg: {avgTime:F2}ms");
//        }

//        // Generate benchmark report
//        Console.WriteLine("\n=== Benchmark Results ===");
//        foreach (var (algorithm, times) in results)
//        {
//            if (times.Count > 0)
//            {
//                var avg = times.Average(t => t.TotalMilliseconds);
//                var min = times.Min(t => t.TotalMilliseconds);
//                var max = times.Max(t => t.TotalMilliseconds);
//                Console.WriteLine($"{algorithm,-15}: Avg {avg,8:F2}ms, Min {min,8:F2}ms, Max {max,8:F2}ms");
//            }
//        }
//    }

//    /// <summary>
//    /// Runs detailed accuracy analysis on specific matrices.
//    /// </summary>
//    private static async Task RunAccuracyAnalysis(string? matrixFile, string[] methods, string output)
//    {
//        Console.WriteLine("=== Accuracy Analysis ===");
        
//        if (matrixFile != null && !File.Exists(matrixFile))
//        {
//            Console.WriteLine($"Error: Matrix file '{matrixFile}' not found.");
//            return;
//        }

//        Console.WriteLine("Running detailed accuracy analysis...\n");

//        var validationMethods = ParseValidationMethods(methods);
        
//        var config = new AccuracyValidator.Config
//        {
//            Methods = validationMethods,
//            AbsoluteTolerance = 1e-14,
//            RelativeTolerance = 1e-12,
//            BackwardErrorTolerance = 1e-15,
//            IncludeDetailedAnalysis = true
//        };

//        // Generate or load test cases
//        List<MatrixTestCaseGenerator.TestMatrix> testCases;
        
//        if (matrixFile != null)
//        {
//            // Load matrices from file (implementation would parse JSON/other formats)
//            Console.WriteLine("Custom matrix loading not implemented in this example.");
//            testCases = MatrixTestCaseGenerator.GenerateTestCases(new MatrixTestCaseGenerator.GenerationConfig
//            {
//                Categories = MatrixTestCaseGenerator.MatrixCategory.AnalyticalSolution,
//                Dimensions = new[] { 4, 8 },
//                CasesPerCategory = 3
//            });
//        }
//        else
//        {
//            testCases = MatrixTestCaseGenerator.GenerateTestCases(new MatrixTestCaseGenerator.GenerationConfig
//            {
//                Categories = MatrixTestCaseGenerator.MatrixCategory.All,
//                Dimensions = new[] { 2, 4, 8, 16 },
//                CasesPerCategory = 2,
//                IncludeAnalyticalSolutions = true
//            });
//        }

//        // Run detailed analysis
//        var analysisResults = new List<AccuracyValidator.ValidationResult>();
        
//        foreach (var testCase in testCases.Take(10)) // Limit for demonstration
//        {
//            Console.WriteLine($"Analyzing: {testCase.Name}");
            
//            try
//            {
//                var computed = testCase.Matrix.MatrixExpViaPadeSimple(config.AbsoluteTolerance);
//                var result = AccuracyValidator.ValidateAccuracy(testCase, computed, "Pade", TimeSpan.Zero, config);
//                analysisResults.Add(result);
                
//                Console.WriteLine($"  Max Error: {result.MaxError:E2}");
//                Console.WriteLine($"  Relative Error: {result.RelativeError:E2}");
//                Console.WriteLine($"  Passed: {result.Passed}");
//                Console.WriteLine();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"  Error: {ex.Message}\n");
//            }
//        }

//        // Save detailed analysis report
//        Directory.CreateDirectory(output);
//        var reportPath = Path.Combine(output, $"accuracy_analysis_{DateTime.Now:yyyyMMdd_HHmmss}.json");
        
//        var report = new
//        {
//            Timestamp = DateTime.UtcNow,
//            TestCases = testCases.Count,
//            Results = analysisResults,
//            Summary = new
//            {
//                TotalTests = analysisResults.Count,
//                PassedTests = analysisResults.Count(r => r.Passed),
//                AverageError = analysisResults.Average(r => r.MaxError),
//                WorstError = analysisResults.Max(r => r.MaxError)
//            }
//        };

//        await File.WriteAllTextAsync(reportPath, System.Text.Json.JsonSerializer.Serialize(report, 
//            new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));

//        Console.WriteLine($"Detailed analysis report saved: {reportPath}");
//    }

//    private static MatrixTestCaseGenerator.MatrixCategory ParseCategories(string[] categories)
//    {
//        if (categories.Length == 1 && categories[0].ToLowerInvariant() == "all")
//            return MatrixTestCaseGenerator.MatrixCategory.All;

//        var result = MatrixTestCaseGenerator.MatrixCategory.None;
//        foreach (var category in categories)
//        {
//            result |= category.ToLowerInvariant() switch
//            {
//                "dense" => MatrixTestCaseGenerator.MatrixCategory.GeneralDense,
//                "symmetric" => MatrixTestCaseGenerator.MatrixCategory.Symmetric,
//                "spd" or "positivedefinite" => MatrixTestCaseGenerator.MatrixCategory.SymmetricPositiveDefinite,
//                "indefinite" => MatrixTestCaseGenerator.MatrixCategory.SymmetricIndefinite,
//                "skew" or "skewsymmetric" => MatrixTestCaseGenerator.MatrixCategory.SkewSymmetric,
//                "upper" or "uppertriangular" => MatrixTestCaseGenerator.MatrixCategory.UpperTriangular,
//                "lower" or "lowertriangular" => MatrixTestCaseGenerator.MatrixCategory.LowerTriangular,
//                "triangular" => MatrixTestCaseGenerator.MatrixCategory.UpperTriangular | 
//                               MatrixTestCaseGenerator.MatrixCategory.LowerTriangular,
//                "diagonal" => MatrixTestCaseGenerator.MatrixCategory.Diagonal,
//                "singular" => MatrixTestCaseGenerator.MatrixCategory.NearSingular,
//                "illconditioned" => MatrixTestCaseGenerator.MatrixCategory.IllConditioned,
//                "sparse" => MatrixTestCaseGenerator.MatrixCategory.Sparse,
//                "structured" => MatrixTestCaseGenerator.MatrixCategory.Structured,
//                "analytical" => MatrixTestCaseGenerator.MatrixCategory.AnalyticalSolution,
//                _ => MatrixTestCaseGenerator.MatrixCategory.None
//            };
//        }

//        return result == MatrixTestCaseGenerator.MatrixCategory.None 
//            ? MatrixTestCaseGenerator.MatrixCategory.GeneralDense 
//            : result;
//    }

//    private static AccuracyValidator.ValidationMethod ParseValidationMethods(string[] methods)
//    {
//        if (methods.Length == 1 && methods[0].ToLowerInvariant() == "all")
//            return AccuracyValidator.ValidationMethod.All;

//        var result = AccuracyValidator.ValidationMethod.None;
//        foreach (var method in methods)
//        {
//            result |= method.ToLowerInvariant() switch
//            {
//                "functional" => AccuracyValidator.ValidationMethod.FunctionalEquations,
//                "analytical" => AccuracyValidator.ValidationMethod.AnalyticalComparison,
//                "backward" => AccuracyValidator.ValidationMethod.BackwardError,
//                "forward" => AccuracyValidator.ValidationMethod.ForwardError,
//                "residual" => AccuracyValidator.ValidationMethod.ResidualBounds,
//                "orthogonal" => AccuracyValidator.ValidationMethod.OrthogonalityPreservation,
//                "determinant" => AccuracyValidator.ValidationMethod.DeterminantProperty,
//                "symmetry" => AccuracyValidator.ValidationMethod.SymmetryPreservation,
//                "condition" => AccuracyValidator.ValidationMethod.ConditionNumberAnalysis,
//                "convergence" => AccuracyValidator.ValidationMethod.ConvergenceAnalysis,
//                _ => AccuracyValidator.ValidationMethod.None
//            };
//        }

//        return result == AccuracyValidator.ValidationMethod.None 
//            ? AccuracyValidator.ValidationMethod.FunctionalEquations 
//            : result;
//    }

//    private static async Task GenerateDetailedAnalysis(ValidationSuite.SuiteResults results, string outputDir)
//    {
//        var analysisPath = Path.Combine(outputDir, $"detailed_analysis_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
        
//        using var writer = new StreamWriter(analysisPath);
        
//        await writer.WriteLineAsync("=== DETAILED VALIDATION ANALYSIS ===");
//        await writer.WriteLineAsync($"Generated: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC");
//        await writer.WriteLineAsync($"Total Duration: {results.TotalDuration}");
//        await writer.WriteLineAsync();

//        await writer.WriteLineAsync("=== FAILURE ANALYSIS ===");
//        var failures = results.TestExecutions.Where(t => !t.Success).ToList();
//        if (failures.Any())
//        {
//            var failuresByAlgorithm = failures.GroupBy(f => f.AlgorithmName);
//            foreach (var group in failuresByAlgorithm)
//            {
//                await writer.WriteLineAsync($"{group.Key}: {group.Count()} failures");
//                foreach (var failure in group.Take(5)) // Limit output
//                {
//                    await writer.WriteLineAsync($"  - {failure.TestCaseName}: {failure.ErrorMessage}");
//                }
//                if (group.Count() > 5)
//                    await writer.WriteLineAsync($"  ... and {group.Count() - 5} more");
//                await writer.WriteLineAsync();
//            }
//        }
//        else
//        {
//            await writer.WriteLineAsync("No failures detected.");
//        }

//        await writer.WriteLineAsync("=== PERFORMANCE ANALYSIS ===");
//        foreach (var algorithm in results.AlgorithmSummaries.Values)
//        {
//            await writer.WriteLineAsync($"{algorithm.Name}:");
//            await writer.WriteLineAsync($"  Success Rate: {100.0 * algorithm.PassedTests / algorithm.TotalTests:F1}%");
//            await writer.WriteLineAsync($"  Average Time: {algorithm.AverageExecutionTime:F2}ms");
//            await writer.WriteLineAsync($"  Worst Error: {algorithm.WorstCaseError:E2}");
//            if (algorithm.ProblematicCategories.Any())
//            {
//                await writer.WriteLineAsync($"  Problematic Categories: {string.Join(", ", algorithm.ProblematicCategories)}");
//            }
//            await writer.WriteLineAsync();
//        }

//        Console.WriteLine($"Detailed analysis saved: {analysisPath}");
//    }
//}