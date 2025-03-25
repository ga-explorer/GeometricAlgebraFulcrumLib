using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Plots;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Files;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Temporal
{
    public static class Float64ScalarSignalSamples
    {
        public static void NormalizedSignalsExample()
        {
            const string workingFolder = @"D:\";

            var plotComposer = new Float64ScalarSignalPlotComposer(
                workingFolder, 
                1280, 
                1280
            );

            plotComposer.PlotToFileWithDerivatives(
                Float64ScalarSignal.FiniteHalfSinStep(),
                "FiniteHalfSinStep"
            );

            plotComposer.PlotToFileWithDerivatives(
                Float64ScalarSignal.PeriodicHalfSinStep(),
                "PeriodicHalfSinStep"
            );


            plotComposer.PlotToFileWithDerivatives(
                Float64ScalarSignal.FiniteRamp(),
                "FiniteRamp"
            );

            plotComposer.PlotToFileWithDerivatives(
                Float64ScalarSignal.PeriodicRamp(),
                "PeriodicRamp"
            );


            plotComposer.PlotToFileWithDerivatives(
                Float64ScalarSignal.FiniteSharpRectangle(),
                "FiniteSharpRectangle"
            );

            plotComposer.PlotToFileWithDerivatives(
                Float64ScalarSignal.PeriodicSharpRectangle(),
                "PeriodicSharpRectangle"
            );


            plotComposer.PlotToFileWithDerivatives(
                Float64ScalarSignal.FiniteSharpStep(),
                "FiniteSharpStep"
            );

            plotComposer.PlotToFileWithDerivatives(
                Float64ScalarSignal.PeriodicSharpStep(),
                "PeriodicSharpStep"
            );


            plotComposer.PlotToFileWithDerivatives(
                Float64ScalarSignal.FiniteSmoothRectangle(),
                "FiniteSmoothRectangle"
            );

            plotComposer.PlotToFileWithDerivatives(
                Float64ScalarSignal.PeriodicSmoothRectangle(),
                "PeriodicSmoothRectangle"
            );

            var s = Float64ScalarSignal.PeriodicSmoothRectangle();
            plotComposer.PlotToFile(
                Float64ScalarSignal.FiniteComputed(
                    Float64ScalarRange.SymmetricOne, 
                    t => s.GetDerivative1Value(t) - s.GetDerivative1ValueNumerical(t)
                ),
                "PeriodicSmoothRectangleD1Error"
            );
            
            plotComposer.PlotToFile(
                Float64ScalarSignal.FiniteComputed(
                    Float64ScalarRange.SymmetricOne, 
                    t => s.GetDerivative2Value(t) - s.GetDerivative2ValueNumerical(t)
                ),
                "PeriodicSmoothRectangleD2Error"
            );


            plotComposer.PlotToFileWithDerivatives(
                Float64ScalarSignal.FiniteSmoothStep(),
                "FiniteSmoothStep"
            );

            plotComposer.PlotToFileWithDerivatives(
                Float64ScalarSignal.PeriodicSmoothStep(),
                "PeriodicSmoothStep"
            );


            plotComposer.PlotToFileWithDerivatives(
                Float64ScalarSignal.FiniteTriangle(),
                "FiniteTriangle1"
            );

            plotComposer.PlotToFileWithDerivatives(
                Float64ScalarSignal.PeriodicTriangle(),
                "PeriodicTriangle1"
            );


            plotComposer.PlotToFileWithDerivatives(
                Float64ScalarSignal.FiniteTriangle(0.25),
                "FiniteTriangle2"
            );

            plotComposer.PlotToFileWithDerivatives(
                Float64ScalarSignal.PeriodicTriangle(0.25),
                "PeriodicTriangle2"
            );


            plotComposer.PlotToFileWithDerivatives(
                Float64ScalarSignal.FiniteTriangle(0.85),
                "FiniteTriangle3"
            );

            plotComposer.PlotToFileWithDerivatives(
                Float64ScalarSignal.PeriodicTriangle(0.85),
                "PeriodicTriangle3"
            );
        }

        public static void BasicSignalsExample()
        {
            const string workingFolder = @"D:\";

            var plotComposer = new Float64ScalarSignalPlotComposer(
                workingFolder, 
                1280, 
                1280
            );
            

            //plotComposer.PlotToFileWithDerivatives(
            //    Float64ScalarSignal.FiniteZero(),
            //    "FiniteZero"
            //);
            
            //plotComposer.PlotToFileWithDerivatives(
            //    Float64ScalarSignal.PeriodicZero(),
            //    "PeriodicZero"
            //);


            //plotComposer.PlotToFileWithDerivatives(
            //    Float64ScalarSignal.FiniteOne(),
            //    "FiniteOne"
            //);
            
            //plotComposer.PlotToFileWithDerivatives(
            //    Float64ScalarSignal.PeriodicOne(),
            //    "PeriodicOne"
            //);


            //plotComposer.PlotToFileWithDerivatives(
            //    Float64ScalarSignal.FiniteCos(),
            //    "FiniteCos"
            //);
            
            //plotComposer.PlotToFileWithDerivatives(
            //    Float64ScalarSignal.PeriodicCos(),
            //    "PeriodicCos"
            //);
            
            
            //plotComposer.PlotToFileWithDerivatives(
            //    Float64ScalarSignal.FiniteSin(),
            //    "FiniteSin"
            //);
            
            //plotComposer.PlotToFileWithDerivatives(
            //    Float64ScalarSignal.PeriodicSin(),
            //    "PeriodicSin"
            //);
            

            plotComposer.PlotToFileWithDerivatives(
                Float64ScalarSignal.FiniteComputed(
                    Float64ScalarRange.SymmetricOne, 
                    t1 => Math.Exp(t1) * Math.Sin(10 * t1 * Math.PI)
                ),
                "FiniteComputed"
            );
            
            plotComposer.PlotToFileWithDerivatives(
                Float64ScalarSignal.PeriodicComputed(
                    Float64ScalarRange.SymmetricOne, 
                    t1 => Math.Exp(t1) * Math.Sin(10 * t1 * Math.PI)
                ),
                "PeriodicComputed"
            );

        }

        
        public static void Example2()
        {
            const string workingFolder = @"D:\Projects\Study\POV-Ray\Samples";

            var scalar1 = Float64ScalarSignal.FiniteSmoothRectangle().MapValueRangeTo(0, 1);
            var scalar2 = Float64ScalarSignal.FiniteTriangle().MapValueRangeTo(0, 1);

            var scalar = (scalar1 + scalar2).MapValueRangeTo(-1, 1);

            scalar = Float64ScalarSignal.FiniteTriangle().FlipValueRange().Blend(
                Float64ScalarSignal.FiniteTriangle(), 
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

            var composer = new Float64ScalarSignalComposer();

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
