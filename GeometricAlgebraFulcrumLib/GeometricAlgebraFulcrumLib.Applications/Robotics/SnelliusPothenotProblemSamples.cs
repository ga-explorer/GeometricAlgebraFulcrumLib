using System.Diagnostics;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;

namespace GeometricAlgebraFulcrumLib.Applications.Robotics
{
    /// <summary>
    /// https://en.wikipedia.org/wiki/Snellius%E2%80%93Pothenot_problem
    /// https://github.com/jorgeven98/Resection-Problem-3D/blob/main/pothenot_GA.ipynb
    /// </summary>
    public static class SnelliusPothenotProblemSamples
    {
        public static Tuple<Triplet<LinFloat64Vector3D>, Triplet<LinFloat64Angle>> GetAngles(LinFloat64Vector3D pointA, LinFloat64Vector3D pointB, LinFloat64Vector3D pointC, LinFloat64Vector3D pointP)
        {
            var ua = LinFloat64Vector2D.Create(pointA.X - pointP.X, pointA.Y - pointP.Y);
            var ub = LinFloat64Vector2D.Create(pointB.X - pointP.X, pointB.Y - pointP.Y);
            var uc = LinFloat64Vector2D.Create(pointC.X - pointP.X, pointC.Y - pointP.Y);

            var thetaUa = ua.GetPolarAngle();
            var thetaUb = ub.GetPolarAngle();
            var thetaUc = uc.GetPolarAngle();

            Console.WriteLine(
                $"Angles ----> A: {thetaUa}, B: {thetaUb}, C: {thetaUc}"
            );

            var alpha = thetaUa.AngleSubtract(thetaUb.ScalarValue);
            var beta = thetaUb.AngleSubtract(thetaUc.ScalarValue);

            var a = pointA;
            var b = pointB;
            var c = pointC;

            // If P is aligned with A and B or B and C, a new assignment of
            // the points is necessary.
            if (beta.IsZeroOrFull())
            {
                alpha = thetaUb.AngleSubtract(thetaUa.ScalarValue);
                beta = thetaUa.AngleSubtract(thetaUc.ScalarValue);

                Console.WriteLine(
                    "The angle beta = 0 ---> The position of A and B will be interchanged."
                );

                (a, b) = (b, a);
            }

            if (alpha.IsZeroOrFull())
            {
                alpha = thetaUa.AngleSubtract(thetaUc.ScalarValue);
                beta = thetaUc.AngleSubtract(thetaUb.ScalarValue);

                Console.WriteLine(
                    "The angle alpha = 0 ---> The position of C and B will be interchanged."
                );

                (c, b) = (b, c);
            }

            // if we are working in 2 dimensions, gamma is not used.
            //var gamma = Float64PlanarAngle.Angle0;

            var gamma = LinFloat64PolarAngle.CreateFromVector(
                ua.Norm(),
                pointA.Z - pointP.Z
            ).RadiansToPolarAngle();

            return new Tuple<Triplet<LinFloat64Vector3D>, Triplet<LinFloat64Angle>>(
                new Triplet<LinFloat64Vector3D>(a, b, c),
                new Triplet<LinFloat64Angle>(alpha, beta, gamma)
            );
        }


        public static void ValidationExample1()
        {
            var randomGen = new Random(10);
            var angleArray =
                0d.GetLinearRange(360, 1000, true).ToArray();

            for (var i = 0; i < 1000; i++)
            {
                //var pointP = (10 * randomGen.GetVector2D()).RoundScalars();
                //var pointA = pointP + (10 * randomGen.GetVector2D()).RoundScalars();
                //var pointB = pointP + (10 * randomGen.GetVector2D()).RoundScalars();
                //var pointC = pointP + (10 * randomGen.GetVector2D()).RoundScalars();


                var angle = angleArray[i].RadiansToPolarAngle(); //randomGen.GetAngle();
                var pointP = LinFloat64Vector2D.Create(8, 0).RotateBy(angle);
                var pointA = LinFloat64Vector2D.Create(5, 0);
                var pointB = pointA.RotateBy(LinFloat64PolarAngle.Angle120);
                var pointC = pointA.RotateBy(LinFloat64PolarAngle.Angle240);


                //var pointP = Float64Vector2D.Create(7, -3);
                //var pointA = Float64Vector2D.Create(5, 6);
                //var pointB = Float64Vector2D.Create(0, 1);
                //var pointC = Float64Vector2D.Create(3, 12);


                //var angle = Float64PlanarAngle.Angle270;
                //var angle = randomGen.GetAngle();

                //var pointP = Float64Vector2D.Create(2, 2).RotateBy(angle);
                //var pointA = Float64Vector2D.Create(-4, 2).RotateBy(angle);
                //var pointB = Float64Vector2D.Create(0, 10).RotateBy(angle);
                //var pointC = Float64Vector2D.Create(10, 8).RotateBy(angle);


                var data = SnelliusPothenotProblemData2D.Create(
                    pointA,
                    pointB,
                    pointC,
                    pointP
                );

                var p1 = data.SolveUsingVGa();
                var error1 = (pointP - p1).Norm().Log10().ScalarValue;
                Debug.Assert(error1 < 0);

                var p2 = data.SolveUsingPGaPaco();
                var error2 = (pointP - p2).Norm().Log10().ScalarValue;
                Debug.Assert(error2 < 0);

                var p3 = data.SolveUsingCGaPaco();
                var error3 = (pointP - p3).Norm().Log10().ScalarValue;
                Debug.Assert(error3 < 0);

                var p4 = data.SolveUsingCGaCollins();
                var error4 = (pointP - p4).Norm().Log10().ScalarValue;
                Debug.Assert(error4 < 0);

                var p5 = data.SolveUsingCGaCassini();
                var error5 = (pointP - p5).Norm().Log10().ScalarValue;
                Debug.Assert(error5 < 0);
            }

            //var data = SnelliusPothenotData2D.Create(
            //    Float64Vector2D.Create(-7, -1),
            //    Float64Vector2D.Create(1, 5),
            //    Float64Vector2D.Create(15.6, 6),
            //    Float64Vector2D.Create(3.12, -18.5)
            //);

            //var data = SnelliusPothenotData2D.Create(
            //    Float64Vector2D.Create(5, 6), 
            //    Float64Vector2D.Create(0, 1),
            //    Float64Vector2D.Create(3, 12),
            //    Float64Vector2D.Create(8, -3)
            //);

            //data.SolveUsingVGa(out var pX, out var pY, out var _);
            //var p1 = Float64Vector2D.Create(pX, pY);

            //data.SolveUsingPGaPaco(out pX, out pY);
            //var p2 = Float64Vector2D.Create(pX, pY);

            //data.SolveUsingCGaCassini(out pX, out pY);
            //var p3 = Float64Vector2D.Create(pX, pY);

            //data.SolveUsingCGaCollins(out pX, out pY);
            //var p4 = Float64Vector2D.Create(pX, pY);

            //Console.WriteLine(data);
            ////Console.WriteLine($"VGA method result        : {p1.ToTupleString()}");
            ////Console.WriteLine($"CGA Paco method result   : {p2.ToTupleString()}");
            ////Console.WriteLine($"CGA Cassini method result: {p3.ToTupleString()}");
            ////Console.WriteLine($"CGA Collins method result: {p4.ToTupleString()}");
            //Console.WriteLine();
        }


        //private static double GetError(Float64Vector2D originalPoint, Float64Vector2D computedPoint)
        //{
        //    var errorValue = (originalPoint - computedPoint).Norm().Log10().Value;

        //    if (double.IsNaN(errorValue) || errorValue > 1) return 1;

        //    if (errorValue < -15) return -15;

        //    return errorValue;
        //}

        private static double GetError(LinFloat64Vector2D originalPoint, LinFloat64Vector2D computedPoint)
        {
            var errorValue = (originalPoint - computedPoint).Norm().ScalarValue;

            //if (double.IsNaN(errorValue) || errorValue > 10) return 10;

            return errorValue;
        }


        private static void SaveHeatMap(double xyRange, double[,] dataArray, string plotFilePath)
        {
            var model = new PlotModel { Title = "Heatmap" };

            model.Legends.Add(
                new Legend()
            //{
            //    Key = "s1",
            //    AllowUseFullExtent = true,
            //    EdgeRenderingMode = EdgeRenderingMode.PreferSharpness,
            //    IsLegendVisible = true,
            //    LegendOrientation = LegendOrientation.Vertical,
            //    LegendPlacement = LegendPlacement.Inside,
            //    LegendPosition = LegendPosition.RightTop,
            //    LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
            //    LegendBorder = OxyColors.Black,
            //    LegendMaxWidth = 64,
            //}
            );

            // Color axis (the X and Y axes are generated automatically)
            model.Axes.Add(
                new LinearColorAxis
                {
                    Palette = OxyPalettes.Rainbow(128)
                }
            );

            var heatMapSeries = new HeatMapSeries
            {
                //LegendKey = "s1",
                RenderInLegend = false,
                X0 = -xyRange,
                X1 = xyRange,
                Y0 = -xyRange,
                Y1 = xyRange,
                Interpolate = true,
                //RenderMethod = HeatMapRenderMethod.Bitmap,
                Data = dataArray
            };

            model.Series.Add(heatMapSeries);

            //model.IsLegendVisible = true;

            //OxyPlot.SkiaSharp.PdfExporter.Export(model, filePath + ".pdf", 1024, 768);

            OxyPlot.SkiaSharp.PngExporter.Export(
                model,
                plotFilePath,
                1024,
                1024,
                256
            );
        }

        public static void SaveCsv(double[,] dataArray, string csvFilePath)
        {
            var composer = new StringBuilder();

            for (var i = 0; i < dataArray.GetLength(0); i++)
            {
                for (var j = 0; j < dataArray.GetLength(1); j++)
                {
                    if (j > 0) composer.Append(", ");

                    composer.Append(dataArray[i, j].ToString("G"));
                }

                composer.AppendLine();
            }

            File.WriteAllText(csvFilePath, composer.ToString(), Encoding.UTF8);
        }

        public static void ErrorAnalysisExample1()
        {
            var point1 = LinFloat64Vector2D.Create(0, 1);
            var point2 = point1.RotateBy(LinFloat64PolarAngle.Angle120);
            var point3 = point1.RotateBy(LinFloat64PolarAngle.Angle240);

            const double range = 5;
            const int n = 1024;
            var xValues = (-range).GetLinearRange(range, n).ToArray();
            var yValues = (-range).GetLinearRange(range, n).ToArray();
            var errorArray = new double[n, n];

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    var originalPoint = LinFloat64Vector2D.Create(xValues[i], yValues[j]);

                    var data = SnelliusPothenotProblemData2D.Create(
                        point1,
                        point2,
                        point3,
                        originalPoint
                    );

                    var computedPoint = data.SolveUsingCGaCassini();

                    errorArray[i, j] = GetError(originalPoint, computedPoint);
                }
            }

            Console.WriteLine($"Error Range: [{errorArray.Min2D():G4}, {errorArray.Max2D():G4}]");
            Console.WriteLine();

            errorArray[0, 0] = -15;
            errorArray[n - 1, n - 1] = 1;

            SaveCsv(
                errorArray,
                @"D:\Projects\Study\Surveying\Snellius-Pothenot Problem\ErrorData.csv"
            );

            //SaveHeatMap(
            //    range,
            //    errorArray,
            //    @"D:\Projects\Study\Surveying\Snellius-Pothenot Problem\ErrorData.png"
            //);
        }
    }
}
