using System.Drawing;
using System.Drawing.Imaging;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Statistics
{
    public static class Sample1
    {
        public static void SaveHistogramImage(string fileName, IReadOnlyList<double> yValues, double yMax)
        {
            var imageSize = yValues.Count;

            var image = new Bitmap(imageSize, imageSize);

            for (var j = 0; j < imageSize; j++)
            {
                var y = imageSize * (yValues[j] / yMax);

                for (var i = 0; i < y; i++)
                    image.SetPixel(j, imageSize - i - 1, Color.DarkBlue);
            }

            image.Save($@"D:\Projects\Active\LvqLib\{fileName}.png", ImageFormat.Png);
        }


        public static void Execute()
        {
            var randGen = new Random(10);

            var levyGenerator = new StableDistributionRandomGenerator(randGen);
            levyGenerator.SetAsNegativeLevy();

            var histogramCount = 2048;
            var lowerLimit = -10d;
            var upperLimit = 10d;

            var histogram =
                levyGenerator.CreateHistogram(lowerLimit, upperLimit, histogramCount);

            var (xValues, yValues) =
                histogram.CreateDataArrays();


            var zValues = xValues.LevyPdf().ToArray();

            var yArea = yValues.Sum() * (upperLimit - lowerLimit) / histogramCount;
            var zArea = zValues.Sum() * (upperLimit - lowerLimit) / histogramCount;
            var scalingFactor = zArea / yArea;

            var errorValue = double.NegativeInfinity;
            for (var i = 0; i < xValues.Length; i++)
            {
                yValues[i] *= scalingFactor;

                errorValue = Math.Max(
                    errorValue,
                    Math.Abs(zValues[i] - yValues[i])
                );
            }

            var yMax = yValues.Max();
            var zMax = zValues.Max();

            Console.WriteLine($"Error value = {errorValue:G}");
            Console.WriteLine();

            SaveHistogramImage("Simulated", yValues, yMax * 1.1d);
            SaveHistogramImage("Actual", zValues, zMax * 1.1d);
        }
    }
}
