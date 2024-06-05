using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Core.Modeling.Statistics.Continuous;

namespace GeometricAlgebraFulcrumLib.Core.Samples.Modeling.Statistics
{
    public static class HistogramSamples
    {
        public static void Example1()
        {
            var hist1 =
                SparseRegularHistogram.CreateEmpty(
                    -2.5,
                    3.5,
                    6
                );

            hist1.SetBinHeight(1, 0.4);
            hist1.SetBinHeight(2, 0.25);
            hist1.SetBinHeight(4, 0.35);

            var s =
                hist1.StoredBinData.ToImmutableArray();

            Console.WriteLine(hist1.GetPdfMatlabCode());
            Console.WriteLine();

            Console.WriteLine(hist1.GetCdfMatlabCode());
            Console.WriteLine();

            Console.WriteLine(hist1.GetIdfMatlabCode());
            Console.WriteLine();
        }

        public static void Example2()
        {
            //var hist = 
            //    SparseRegularHistogram.CreateNormal(
            //        0, 
            //        1, 
            //        1025,
            //        1e-5
            //    ).MapDomainUsingAffine(Math.PI, Math.PI / 2);

            var hist1 =
                SparseRegularHistogram.CreateUniform(
                    -Math.PI,
                    Math.PI
                );

            var pdf1 =
                hist1.GetProbabilityDensityFunction();

            Console.WriteLine(hist1.GetPdfMatlabCode());
            Console.WriteLine();

            var hist2 =
                hist1.MapDomain(Math.Sin, 1025);

            var pdf2 =
                hist2.GetProbabilityDensityFunction();

            Console.WriteLine(hist2.GetPdfMatlabCode());
            Console.WriteLine();
        }
    }
}
