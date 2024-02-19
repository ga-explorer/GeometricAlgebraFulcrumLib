using BenchmarkDotNet.Running;
using GeometricAlgebraFulcrumLib.Benchmarks.Applications;
using GeometricAlgebraFulcrumLib.Benchmarks.Generations;

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
        BenchmarkRunner.Run<SnelliusPothenotProblemBenchmarks>();
    }
}