using GeometricAlgebraFulcrumLib.Optimization.Samples;

namespace GeometricAlgebraFulcrumLib.Optimization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CGpSamples.Example2();
            //GpuSgd.Sample1();
            //SvmSamples.Sample2();

            Console.WriteLine("Press any key ..");
            Console.ReadKey();
        }
    }
}
