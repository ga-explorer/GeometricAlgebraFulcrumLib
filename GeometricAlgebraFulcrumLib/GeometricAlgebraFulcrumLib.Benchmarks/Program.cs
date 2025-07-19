using BenchmarkDotNet.Running;
using GeometricAlgebraFulcrumLib.Benchmarks.Applications;
using GeometricAlgebraFulcrumLib.Benchmarks.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Benchmarks;

class Program
{
    static void Main(string[] args)
    {
        //var benckmark = new ClarkeBenchmark()
        //{
        //    VSpaceDimensions = 48
        //};

        //benckmark.Setup();
        //benckmark.Validate();


        //BenchmarkRunner.Run<ClarkeBenchmark>();
        //BenchmarkRunner.Run<DictionaryJoinBenchmarks>();
        //BenchmarkRunner.Run<GenerationsBenchmarks>();
        //BenchmarkRunner.Run<HansenProblemBenchmarks>();
        //BenchmarkRunner.Run<SnelliusPothenotProblemBenchmarks>();
        //BenchmarkRunner.Run<ScalarOperationsBenchmarks>();
        //BenchmarkRunner.Run<GaalopComparisonsBenchmarks>();
        //BenchmarkRunner.Run<IndexSetBenchmarks>();
        //BenchmarkRunner.Run<BilinearProductsBenchmarks>();
        //BenchmarkRunner.Run<MetricBenchmarks>();
        
        //MetricBenchmarks.Validate();
        //IndexSetBenchmarks.Validate();
        //UnaryOperationsBenchmarks.Validate();
        //BilinearProductsBenchmarks.Validate();
        //BilinearProductsBenchmarks.TestGrades(
        //    "ECp",
        //    (kv1, kv2) => kv1.ECp(kv2)
        //);

        BenchmarkRunner.Run<JacobiSymmetricEigenDecomposerBenchmarks>();
        //JacobiSymmetricEigenDecomposerBenchmarks.Validate();
    }
}