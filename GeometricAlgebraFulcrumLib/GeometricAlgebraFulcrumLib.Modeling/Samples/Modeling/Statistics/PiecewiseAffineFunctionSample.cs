using GeometricAlgebraFulcrumLib.Modeling.Statistics.Continuous;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Statistics
{
    public static class PiecewiseAffineFunctionSample
    {
        public static void Example1()
        {
            var func = new PiecewiseAffineFunction();

            func.InsertBreakpoint(-1, -0.5);
            func.InsertBreakpoint(1, 1, 2, 3);
            func.InsertBreakpoint(3, 4, 4, 5);
            func.InsertBreakpoint(5, 6, 7, 7);
            func.InsertBreakpoint(7, 2);

            var code = func.GetMatlabCode();

            Console.WriteLine(code);
            Console.WriteLine();
        }

        public static void Example2()
        {
            const int sampleCount = 1024 + 1;
            const double angleTolerance = 0.1 * Math.PI / 180;

            var func1 =
                PiecewiseAffineFunction.CreateContinuous(
                    x => Math.Exp(-x * x / 2) / Math.Sqrt(2 * Math.PI),
                    -6,
                    6,
                    sampleCount
                );

            var func2 =
                PiecewiseAffineFunction.CreateContinuous(
                    x => Math.Exp(-x * x / 2) / Math.Sqrt(2 * Math.PI),
                    -6,
                    6,
                    sampleCount,
                    angleTolerance
                );

            Console.WriteLine(func1.BreakpointCount);
            Console.WriteLine(func2.BreakpointCount);

            var code1 = func1.GetMatlabCode();
            var code2 = func2.GetMatlabCode();

            Console.WriteLine(code1);
            Console.WriteLine();

            Console.WriteLine(code2);
            Console.WriteLine();
        }

        public static void Example3()
        {
            var pdf = ProbabilityDistributionFunction.CreateNormal(
                2, 1
            );

            var area = pdf.PwaFunction.GetArea();

            Console.WriteLine(area.ToString("G"));
            Console.WriteLine();

            var code = pdf.PwaFunction.GetMatlabCode();

            Console.WriteLine(code);
            Console.WriteLine();
        }
    }
}
