using BenchmarkDotNet.Running;
using GeometricAlgebraFulcrumLib.Benchmarks.GAPoT;

namespace GeometricAlgebraFulcrumLib.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            //var benckmark = new SkrClarkeBenchmark();

            BenchmarkRunner.Run<SkrClarkeBenchmark>();
        }
    }
}
