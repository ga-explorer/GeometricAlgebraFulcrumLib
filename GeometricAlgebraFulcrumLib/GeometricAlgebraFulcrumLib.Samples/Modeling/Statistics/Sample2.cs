using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Images;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using MathNet.Numerics.Statistics;

namespace GeometricAlgebraFulcrumLib.Samples.Modeling.Statistics
{
    public static class Sample2
    {
        public static void Execute()
        {
            var randGen = new Random(10);

            var levyGenerator = new StableDistributionRandomGenerator(randGen);

            var histogramCount = 2048;
            var lowerLimit = -10d;
            var upperLimit = 10d;

            var histList = new List<Histogram>();

            var maxValue = double.NegativeInfinity;
            var beta = 0.5d;
            for (var alpha = 0.4d; alpha <= 2d; alpha += 0.025d)
            {
                levyGenerator.SetParameters(alpha, 1d, beta, 0d);

                histList.Add(
                    levyGenerator.CreateHistogram(lowerLimit, upperLimit, histogramCount)
                );

                var value = histList[^1].MaxBucketCount();

                if (maxValue < value)
                    maxValue = value;
            }

            for (var i = 0; i < histList.Count; i++)
                histList[i].SaveHistogramImage(
                    $@"D:\Projects\Active\LvqLib\Hist{i:0000}.png",
                    maxValue
                );
        }
    }
}
