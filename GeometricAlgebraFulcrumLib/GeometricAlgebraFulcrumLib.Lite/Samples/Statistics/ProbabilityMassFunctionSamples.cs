using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.Lite.Statistics.Discrete;

namespace GeometricAlgebraFulcrumLib.Lite.Samples.Statistics
{
    public static class ProbabilityMassFunctionSamples
    {
        public static void Example1()
        {
            const int domainSampleCount = 513;

            //var pmf1 = ProbabilityMassFunction.CreateSampledNormal(0, 0.5, domainSampleCount, 1e-12);
            
            var pmf1 = 
                DiscreteProbabilityMassFunction.CreateBinomial(
                    domainSampleCount, 
                    0.5, 
                    1e-12
                ).ResetDomain(2, 4);

            var pmf2 = 
                DiscreteProbabilityMassFunction.CreateUniform(
                    1, 
                    3, 
                    domainSampleCount
                );

            var pmf =
                pmf1.JoinDomain(pmf2, domainSampleCount);
                //(3 * pmf2 + 2 * pmf1 - 5).TrimProbabilities();

                //pmf1.MapDomain(
                //    pmf2,
                //    (x, y) => 3 * y + 2 * x - 5, 
                //    domainSampleCount
                //).TrimProbabilities();

            Console.WriteLine(pmf1.GetMatlabCode());
            Console.WriteLine();

            Console.WriteLine(pmf2.GetMatlabCode());
            Console.WriteLine();

            Console.WriteLine(pmf.GetMatlabCode());
            Console.WriteLine();

            Console.WriteLine(pmf.GetProbability(2, 2.5));
            Console.WriteLine();
        }

        public static void Example2()
        {
            const int domainSampleCount = 513;

            var pmf1 = 
                DiscreteProbabilityMassFunction.CreateBinomial(
                    domainSampleCount, 
                    0.5, 
                    1e-12
                ).ResetDomain(2, 4);

            var pmf2 = 
                DiscreteProbabilityMassFunction.CreateBinomial(
                    domainSampleCount, 
                    0.5, 
                    1e-12
                ).ResetDomain(2, 4);

            var pmf = pmf1 - pmf2;

            Console.WriteLine(pmf1.GetMatlabCode());
            Console.WriteLine();

            Console.WriteLine(pmf2.GetMatlabCode());
            Console.WriteLine();

            Console.WriteLine(pmf.GetMatlabCode());
            Console.WriteLine();
            
            Console.WriteLine(pmf.GetStandardDeviation());
            Console.WriteLine();
        }

        public static void Example3()
        {
            const int domainSampleCount = 513;
            const double domainFirstValue = -10;
            const double domainLastValue = 10;

            var alpha = 0.4d;
            var beta = 0.5d;

            var levyGenerator = new StableDistributionRandomGenerator(
                new System.Random(10)
            );

            levyGenerator.SetParameters(alpha, 1d, beta, 0d);

            var pmf1 = 
                DiscreteProbabilityMassFunction.CreateFromHistogram(
                    levyGenerator,
                    domainFirstValue,
                    domainLastValue,
                    domainSampleCount,
                    10000000
                );

            var pmfRandomGen = PmfRandomGenerator.Create(
                new System.Random(10),
                pmf1.GetInverseCdfArray(1 << 16 + 1)
            );

            var pmf2 = 
                DiscreteProbabilityMassFunction.CreateFromHistogram(
                    pmfRandomGen,
                    domainFirstValue,
                    domainLastValue,
                    domainSampleCount,
                    10000000
                );

            Console.WriteLine(pmf1.GetMatlabCode());
            Console.WriteLine();
            
            Console.WriteLine(pmf2.GetMatlabCode());
            Console.WriteLine();

            Console.WriteLine(pmf1.GetProbability(2, 2.5));
            Console.WriteLine();
            
            Console.WriteLine(pmf2.GetProbability(2, 2.5));
            Console.WriteLine();
        }
    }
}
