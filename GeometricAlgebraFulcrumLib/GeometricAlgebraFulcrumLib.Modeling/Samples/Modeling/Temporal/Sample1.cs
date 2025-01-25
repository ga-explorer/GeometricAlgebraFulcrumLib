using GeometricAlgebraFulcrumLib.Modeling.Graphics;
using GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Files;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Temporal
{
    public static class Sample1
    {
        public static void Example1()
        {
            const string workingFolder = @"D:\Projects\Study\POV-Ray\Samples";

            //var ts =
            //    TemporalFloat64Scalar
            //        .HalfCos()
            //        .Repeat(3)
            //        .MapTimeRangeUsing(1, 3)
            //        .MapValueRangeUsing(10, 20);

            //var ts =
            //    TemporalFloat64Scalar
            //        .Constant(-10, 0, 2)
            //        .Concat(
            //            TemporalFloat64Scalar.Constant(5, 0, 3)
            //        );

            var scalarList = new TemporalFloat64Scalar[]
            {
                TemporalFloat64Scalar.Ramp(),
                TemporalFloat64Scalar.FullSin().FlipValueRange(),
                TemporalFloat64Scalar.Ramp().FlipTimeRange()
            };

            //var scalarList = new TemporalFloat64Scalar[]
            //{
            //    TemporalFloat64Scalar.Constant(10),
            //    TemporalFloat64Scalar.Constant(7),
            //    TemporalFloat64Scalar.Constant(-2),
            //    TemporalFloat64Scalar.Constant(3),
            //};

            //var ts =
            //    scalarList.Concat();

            var ts =
                scalarList.ConcatBlend(0.5);//.Repeat(3);

            //var signal = ts.GetSampledSignal(1024);

            var plotImage = ts.Plot();

            plotImage.SaveAsPng(
                workingFolder.GetFilePath("Plot.png"), 
                new PngEncoder(){ColorType = PngColorType.RgbWithAlpha}
            );

            var code = 
                ts.GetMatlabPlotCode(1024);

            Console.WriteLine(code);
            Console.WriteLine();
        }

        
        public static void Example2()
        {
            const string workingFolder = @"D:\Projects\Study\POV-Ray\Samples";

            var scalar1 = TemporalFloat64Scalar.SmoothRectangle().MapValueRangeTo(0, 1);
            var scalar2 = TemporalFloat64Scalar.Triangle().MapValueRangeTo(0, 1);

            var scalar = (scalar1 + scalar2).MapValueRangeTo(-1, 1);

            scalar = TemporalFloat64Scalar.Triangle().FlipValueRange().Blend(
                TemporalFloat64Scalar.Triangle(), 
                -1, 
                1
            );

            var plotImage = scalar.Plot();

            plotImage.SaveAsPng(
                workingFolder.GetFilePath("Plot.png"), 
                new PngEncoder(){ColorType = PngColorType.RgbWithAlpha}
            );

            //var code = 
            //    ts.GetMatlabPlotCode(1024);

            //Console.WriteLine(code);
            //Console.WriteLine();
        }

        public static void Example3()
        {
            const string workingFolder = @"D:\Projects\Study\POV-Ray\Samples";

            var composer = new TemporalFloat64ScalarComposer();

            composer.AppendSmoothRectangle(3, 10, 20, 3);

            var scalar = composer.ComposeScalar();

            var plotImage = scalar.Plot();

            plotImage.SaveAsPng(
                workingFolder.GetFilePath("Plot.png"), 
                new PngEncoder(){ColorType = PngColorType.RgbWithAlpha}
            );

            //var code = 
            //    ts.GetMatlabPlotCode(1024);

            //Console.WriteLine(code);
            //Console.WriteLine();
        }
    }
}
