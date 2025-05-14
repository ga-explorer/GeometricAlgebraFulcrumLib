using GeometricAlgebraFulcrumLib.Benchmarks.Structures;

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
        IndexSetBenchmarks.Validate();
        
    }
}