using BenchmarkDotNet.Running;
using GeometricAlgebraFulcrumLib.Benchmarks.GAPoT;

namespace GeometricAlgebraFulcrumLib.Benchmarks
{
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


            BenchmarkRunner.Run<ClarkeBenchmark>();
        }
    }
}
