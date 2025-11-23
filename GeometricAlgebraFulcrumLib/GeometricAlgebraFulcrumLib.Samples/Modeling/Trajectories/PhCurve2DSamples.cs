using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Bernstein;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ImageSharp.DrawingBoard;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ImageSharp.DrawingStyles;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Samplers;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;

namespace GeometricAlgebraFulcrumLib.Samples.Modeling.Trajectories
{
    public static class PhCurve2DSamples
    {
        public static void Example1()
        {
            const string workingFolder = @"D:\";

            var u = BernsteinPolynomial.Create(1, 2, 3);
            var v = BernsteinPolynomial.Create(-1, 1.5, 4);
            var w = BernsteinPolynomial.Constant(1);

            var phCurve = new PhBernsteinCurve2D(
                new PhBernsteinCurve2D.GeneratorData
                {
                    U = u, 
                    V = v, 
                    W = w, 
                    P0 = LinFloat64Vector2D.Zero, 
                    S0 = 0
                }, 
                false
            );

            var boundingBox = phCurve.GetBoundingBox();

            //var sampler = new Float64SampledGeneralPath2D(
            //    Float64ScalarRange.ZeroToOne,
            //    false
            //);

            var sampler = new SampledPhBernsteinCurve2D(false);

            sampler.Generate(phCurve);

            var pointList = 
                sampler.Points.ToArray();

            var segmentList =
                sampler.Segments.ToArray();

            var drawingBoard = ImageSharpDrawingBoard.Create(
                WorldToScreenMap2D.FromScreenWidth(
                    boundingBox, 
                    1600
                ),
                Color.White
            );

            drawingBoard.DrawRectangle(
                boundingBox,
                new ImageSharpLineDrawingStyle(Color.Black, 3)
            ).DrawPolyLine(
                pointList,
                new ImageSharpLineDrawingStyle(Color.Black, 1)
            ).DrawPoints(
                pointList,
                new ImageSharpPointMarkerDrawingStyle(Color.Black, Color.Black, 7)
            );

            drawingBoard.Image.SaveAsPng(
                Path.Combine(workingFolder, "PH Curve.png"),
                new PngEncoder()
                {
                    ColorType = PngColorType.RgbWithAlpha,
                    CompressionLevel = PngCompressionLevel.NoCompression,
                    TransparentColorMode = PngTransparentColorMode.Clear
                }
            );

            Console.WriteLine("Bounding box: " + boundingBox);
            Console.WriteLine("Corner count: " + pointList.Length);
        }

        public static void Example2()
        {
            const string workingFolder = @"D:\";

            var randGen = new Random(10);
            
            LinFloat64Vector2D point0 = (-5, 0);
            LinFloat64Vector2D point1 = (5, 0);

            var tangent0 = 10 * LinFloat64Vector2D.Create(2, 1);
            var tangent1 = 10 * LinFloat64Vector2D.Create(-1, 0);
            
            Console.WriteLine("Original constraints:");
            Console.WriteLine(point0.ToTupleString());
            Console.WriteLine(point1.ToTupleString());
            Console.WriteLine(tangent0.ToTupleString());
            Console.WriteLine(tangent1.ToTupleString());
            Console.WriteLine();

            var curveList = new List<PhBernsteinCurve2D>();

            for (var i = 0; i < 10; i++)
            {
                // Create random curve
                var curve2 = PhBernsteinCurve2D.CreateRandom(
                    randGen,
                    randGen.GetInt32(2, 5), 
                    randGen.GetInt32(2, 5), 
                    2
                );

                // Apply constraints to curve
                (_, curve2, _) = curve2.SetEndPoints(
                    point0, 
                    point1, 
                    tangent0, 
                    tangent1
                );
                
                var p0 = curve2.GetPoint(0);
                var p1 = curve2.GetPoint(1);

                var t0 = curve2.GetTangentVector(0);
                var t1 = curve2.GetTangentVector(1);

                Console.WriteLine($"Actual curve {i}:");
                Console.WriteLine(p0.ToTupleString(7));
                Console.WriteLine(p1.ToTupleString(7));
                Console.WriteLine(t0.ToTupleString(7));
                Console.WriteLine(t1.ToTupleString(7));
                Console.WriteLine();

                curveList.Add(curve2);
            }

            var boundingBox = 
                Float64BoundingBox2D.Containing<IFloat64BoundedShape2D>(curveList);

            var drawingBoard = ImageSharpDrawingBoard.Create(
                WorldToScreenMap2D.FromScreenWidth(
                    boundingBox, 
                    1600
                ),
                Color.White
            );

            //drawingBoard.DrawRectangle(
            //    boundingBox,
            //    new ImageSharpLineDrawingStyle(Color.Black, 3)
            //);
            
            //drawingBoard.DrawPolyLine(
            //    curve1.ToAdaptivePolyline(),
            //    new ImageSharpLineDrawingStyle(Color.Green, 1)
            //);

            //drawingBoard.DrawPolyLine(
            //    curve1.ToAdaptivePolyline().MapUsing(affineMap),
            //    new ImageSharpLineDrawingStyle(Color.LightGreen, 7)
            //);

            foreach (var curve2 in curveList)
                drawingBoard.DrawPolyLine(
                    curve2.ToAdaptivePolyline(),
                    new ImageSharpLineDrawingStyle(Color.Black, 1)
                );

            drawingBoard.Image.SaveAsPng(
                Path.Combine(workingFolder, "PH Poly Curve.png"),
                new PngEncoder()
                {
                    ColorType = PngColorType.RgbWithAlpha,
                    CompressionLevel = PngCompressionLevel.NoCompression,
                    TransparentColorMode = PngTransparentColorMode.Clear
                }
            );

            Console.WriteLine("Bounding box: " + boundingBox);
            
        }
        
        public static void GetAreaExample1()
        {
            // Example polylines with same endpoints
            var polyline1 = new List<LinFloat64Vector2D>
            {
                LinFloat64Vector2D.Create(0, 0),
                LinFloat64Vector2D.Create(1, 2),
                LinFloat64Vector2D.Create(2, 0)
            }.ToPolyline();

            var polyline2 = new List<LinFloat64Vector2D>
            {
                LinFloat64Vector2D.Create(0, 0),
                LinFloat64Vector2D.Create(0.5, 1),
                LinFloat64Vector2D.Create(1.5, 1),
                LinFloat64Vector2D.Create(2, 0)
            }.ToPolyline();

            var area = polyline1.GetArea(polyline2, 1e-5);
            Console.WriteLine($"Area between polylines: {area}");
        }

        public static void GetAreaExample2()
        {
            const string workingFolder = @"D:\";

            var randGen = new Random(10);
            
            LinFloat64Vector2D point0 = (-5, 0);
            LinFloat64Vector2D point1 = (5, 0);

            Console.WriteLine("Original endpoint constraints:");
            Console.WriteLine($"   Positions: {point0.ToTupleString()}, {point1.ToTupleString()}");
            Console.WriteLine();

            var curveList = new List<PhBernsteinCurve2D>();

            var i = 2;
            var j = 0;
            while (i > 0)
            {
                var uDegree = 3;
                var vDegree = 3;
                var wDegree = 3;

                var curve = PhBernsteinCurve2D.CreateRandom(randGen, uDegree, vDegree, wDegree);

                curve = curve.SetEndPoints(
                    point0, 
                    point1
                );

                var p0 = (curve.GetPoint(0) - point0).L2Norm();
                var p1 = (curve.GetPoint(1) - point1).L2Norm();

                Console.WriteLine($"Curve {i} endpoint constraint errors:");
                Console.WriteLine($"       Positions: {p0}, {p1}");
                Console.WriteLine();

                j++;

                if (p0 > 1e-5 || p1 > 1e-5)
                    continue;

                curveList.Add(curve);
                i--;
            }

            Console.WriteLine($"Found {curveList.Count} curves from {j} trials");

            var boundingBox = 
                Float64BoundingBox2D.Containing<IFloat64BoundedShape2D>(curveList);

            var drawingBoard = ImageSharpDrawingBoard.Create(
                WorldToScreenMap2D.FromScreenWidth(
                    boundingBox, 
                    1600
                ),
                Color.White
            );

            foreach (var curve2 in curveList)
                drawingBoard.DrawPolyLine(
                    curve2.ToAdaptivePolyline(),
                    new ImageSharpLineDrawingStyle(randGen.GetColor(), 1)
                );

            drawingBoard.Image.SaveAsPng(
                Path.Combine(workingFolder, "Get Area Example 2.png"),
                new PngEncoder()
                {
                    ColorType = PngColorType.RgbWithAlpha,
                    CompressionLevel = PngCompressionLevel.NoCompression,
                    TransparentColorMode = PngTransparentColorMode.Clear
                }
            );

            Console.WriteLine("Bounding box: " + boundingBox);
            
            var polyline1 = curveList[0].ToAdaptivePolyline();
            var polyline2 = curveList[1].ToAdaptivePolyline();

            var area = polyline1.GetArea(polyline2, 1e-5);
            Console.WriteLine($"Area between polylines: {area}");
        }

        public static void CuspDetectionExample()
        {
            const string workingFolder = @"D:\";

            var randGen = new Random(10);
             
            //var u = BernsteinPolynomial.CreateRandom(randGen, 3, -10, 10);
            //var v = BernsteinPolynomial.CreateRandom(randGen, 3, -10, 10);
            //var w = BernsteinPolynomial.FromRoots([0.1, 0.55, 0.85]);

            var p = BernsteinPolynomial.FromRoots(new[]{0.1, 0.85});
            var u = p * BernsteinPolynomial.CreateRandom(randGen, 1, -1, 1);
            var v = p * BernsteinPolynomial.CreateRandom(randGen, 1, -1, 1);
            var w = BernsteinPolynomial.FromRoots(new[]{0.55}) * BernsteinPolynomial.CreateRandom(randGen, 2, 0.1, 1);

            Console.WriteLine("U: " + u);
            Console.WriteLine("V: " + v);
            Console.WriteLine("W: " + w);
            Console.WriteLine("W roots at: " + w.FindRoots().Concatenate(", "));
            Console.WriteLine();

            var curve = new PhBernsteinCurve2D(
                new PhBernsteinCurve2D.GeneratorData
                {
                    U = u,
                    V = v,
                    W = w,
                    P0 = LinFloat64Vector2D.Zero,
                    S0 = 0
                }, 
                false
            );

            var cuspTimes = curve.FindCuspTimeValues();

            if (cuspTimes.Count > 0)
                Console.WriteLine("Cusps at: " + cuspTimes.Concatenate(", "));
            else
                Console.WriteLine("Cusps not found");

            Console.WriteLine();

            var curvePolyLine =
                curve.ToAdaptivePolyline();
                //curve.ToUniformTimePolyline(2029);

            var boundingBox = curvePolyLine.GetBoundingBox();

            Console.WriteLine("Bounding box: " + boundingBox);
            
            var drawingBoard = ImageSharpDrawingBoard.Create(
                WorldToScreenMap2D.FromScreenWidth(
                    boundingBox, 
                    1600
                ),
                Color.White
            );

            drawingBoard.DrawPolyLine(
                curvePolyLine,
                new ImageSharpLineDrawingStyle(Color.Black, 1)
            );

            drawingBoard.Image.SaveAsPng(
                Path.Combine(workingFolder, "Cusp Detection Example.png"),
                new PngEncoder()
                {
                    ColorType = PngColorType.RgbWithAlpha,
                    CompressionLevel = PngCompressionLevel.NoCompression,
                    TransparentColorMode = PngTransparentColorMode.Clear
                }
            );

        }

        public static void ConstrainedPositionsExample()
        {
            const string workingFolder = @"D:\";

            var randGen = new Random(10);
            
            LinFloat64Vector2D point0 = (-5, 0);
            LinFloat64Vector2D point1 = (5, 0);

            //var originalCurve = PhBernsteinCurve2D.CreateRandom(
            //    randGen, 2, 2, 3
            //).SetEndPoints(point0, point1);

            Console.WriteLine("Original endpoint constraints:");
            Console.WriteLine($"       Positions: {point0.ToTupleString()}, {point1.ToTupleString()}");
            Console.WriteLine();

            var curveList = new List<PhBernsteinCurve2D>();

            for (var i = 0; i < 100; i++)
            {
                var uDegree = 1; //randGen.GetInt32(3, 6);
                var vDegree = 1; //randGen.GetInt32(3, 6);
                var wDegree = 1; //randGen.GetInt32(3, 6);

                var curve = PhBernsteinCurve2D.CreateRandom(randGen, uDegree, vDegree, wDegree);

                curve = curve.SetEndPoints(
                    point0, 
                    point1
                );

                var p0 = (curve.GetPoint(0) - point0).L2Norm();
                var p1 = (curve.GetPoint(1) - point1).L2Norm();

                Console.WriteLine($"Curve {i} endpoint constraint errors:");
                Console.WriteLine($"       Positions: {p0}, {p1}");
                Console.WriteLine();

                if (p0 > 1e-5 || p1 > 1e-5)
                    continue;

                curveList.Add(curve);
            }

            Console.WriteLine($"Found {curveList.Count} curves");

            var boundingBox = 
                Float64BoundingBox2D.Containing<IFloat64BoundedShape2D>(curveList);

            var drawingBoard = ImageSharpDrawingBoard.Create(
                WorldToScreenMap2D.FromScreenWidth(
                    boundingBox, 
                    1600
                ),
                Color.White
            );

            foreach (var curve2 in curveList)
                drawingBoard.DrawPolyLine(
                    curve2.ToAdaptivePolyline(),
                    new ImageSharpLineDrawingStyle(Color.Black, 1)
                );

            drawingBoard.Image.SaveAsPng(
                Path.Combine(workingFolder, "Constrained Positions Example.png"),
                new PngEncoder()
                {
                    ColorType = PngColorType.RgbWithAlpha,
                    CompressionLevel = PngCompressionLevel.NoCompression,
                    TransparentColorMode = PngTransparentColorMode.Clear
                }
            );

            Console.WriteLine("Bounding box: " + boundingBox);
        }

        public static void ConstrainedTangentsExample()
        {
            const string workingFolder = @"D:\";

            var randGen = new Random(10);
            
            LinFloat64Vector2D point0 = (-5, 0);
            LinFloat64Vector2D point1 = (5, 0);

            var originalCurve = PhBernsteinCurve2D.CreateRandom(
                randGen, 2, 2, 3
            ).SetEndPoints(point0, point1);

            var tangent0 = originalCurve.GetTangentVector(0);
            var tangent1 = originalCurve.GetTangentVector(1);

            Console.WriteLine("Original endpoint constraints:");
            Console.WriteLine($"       Positions: {point0.ToTupleString()}, {point1.ToTupleString()}");
            Console.WriteLine($"        Tangents: {tangent0.ToTupleString()}, {tangent1.ToTupleString()}");
            Console.WriteLine();

            var curveList = new List<PhBernsteinCurve2D>();

            for (var i = 0; i < 100; i++)
            {
                var uDegree = 2; //randGen.GetInt32(3, 6);
                var vDegree = 2; //randGen.GetInt32(3, 6);
                var wDegree = 2; //randGen.GetInt32(3, 6);

                var curve = PhBernsteinCurve2D.CreateRandom(randGen, uDegree, vDegree, wDegree);

                (_, curve, _) = curve.SetEndPoints(
                    point0, 
                    point1, 
                    tangent0, 
                    tangent1
                );

                var p0 = (curve.GetPoint(0) - point0).L2Norm();
                var p1 = (curve.GetPoint(1) - point1).L2Norm();

                var t0 = (curve.GetTangentVector(0) - tangent0).L2Norm();
                var t1 = (curve.GetTangentVector(1) - tangent1).L2Norm();

                Console.WriteLine($"Curve {i} endpoint constraint errors:");
                Console.WriteLine($"       Positions: {p0}, {p1}");
                Console.WriteLine($"        Tangents: {t0}, {t1}");
                Console.WriteLine();

                if (p0 > 1e-5 || p1 > 1e-5 || t0 > 1e-5 || t1 > 1e-5)
                    continue;

                curveList.Add(curve);
            }

            Console.WriteLine($"Found {curveList.Count} curves");

            var boundingBox = 
                Float64BoundingBox2D.Containing<IFloat64BoundedShape2D>(curveList);

            var drawingBoard = ImageSharpDrawingBoard.Create(
                WorldToScreenMap2D.FromScreenWidth(
                    boundingBox, 
                    1600
                ),
                Color.White
            );

            foreach (var curve2 in curveList)
                drawingBoard.DrawPolyLine(
                    curve2.ToAdaptivePolyline(),
                    new ImageSharpLineDrawingStyle(Color.Black, 1)
                );

            drawingBoard.Image.SaveAsPng(
                Path.Combine(workingFolder, "Constrained Tangents Example.png"),
                new PngEncoder()
                {
                    ColorType = PngColorType.RgbWithAlpha,
                    CompressionLevel = PngCompressionLevel.NoCompression,
                    TransparentColorMode = PngTransparentColorMode.Clear
                }
            );

            Console.WriteLine("Bounding box: " + boundingBox);
        }

        public static void ConstrainedAccelerationsExample1()
        {
            const string workingFolder = @"D:\";

            var randGen = new Random(10);
            
            LinFloat64Vector2D point0 = (-5, 0);
            LinFloat64Vector2D point1 = (5, 0);

            var originalCurve = PhBernsteinCurve2D.CreateRandom(
                randGen, 3, 3, 3
            ).SetEndPoints(point0, point1);

            var tangent0 = originalCurve.GetTangentVector(0);
            var tangent1 = originalCurve.GetTangentVector(1);

            var acceleration0 = originalCurve.GetAccelerationVector(0);
            var acceleration1 = originalCurve.GetAccelerationVector(1);

            Console.WriteLine("Original endpoint constraints:");
            Console.WriteLine($"       Positions: {point0.ToTupleString()}, {point1.ToTupleString()}");
            Console.WriteLine($"        Tangents: {tangent0.ToTupleString()}, {tangent1.ToTupleString()}");
            Console.WriteLine($"   Accelerations: {acceleration0.ToTupleString()}, {acceleration1.ToTupleString()}");
            Console.WriteLine();

            var curveList = new List<PhBernsteinCurve2D>();

            for (var i = 0; i < 100; i++)
            {
                var uDegree = 3; //randGen.GetInt32(3, 6);
                var vDegree = 3; //randGen.GetInt32(3, 6);
                var wDegree = 3; //randGen.GetInt32(3, 6);

                var curve = PhBernsteinCurve2D.CreateRandom(randGen, uDegree, vDegree, wDegree);

                (_, curve, _) = curve.SetEndPoints(
                    point0, 
                    point1, 
                    tangent0, 
                    tangent1, 
                    acceleration0, 
                    acceleration1
                );

                var p0 = (curve.GetPoint(0) - point0).L2Norm();
                var p1 = (curve.GetPoint(1) - point1).L2Norm();

                var t0 = (curve.GetTangentVector(0) - tangent0).L2Norm();
                var t1 = (curve.GetTangentVector(1) - tangent1).L2Norm();

                var a0 = (curve.GetAccelerationVector(0) - acceleration0).L2Norm();
                var a1 = (curve.GetAccelerationVector(1) - acceleration1).L2Norm();

                Console.WriteLine($"Curve {i} endpoint constraint errors:");
                Console.WriteLine($"       Positions: {p0}, {p1}");
                Console.WriteLine($"        Tangents: {t0}, {t1}");
                Console.WriteLine($"   Accelerations: {a0}, {a1}");
                Console.WriteLine();

                if (p0 > 1e-5 || p1 > 1e-5 || t0 > 1e-5 || t1 > 1e-5 || a0 > 1e-5 || a1 > 1e-5)
                    continue;

                curveList.Add(curve);
            }

            Console.WriteLine($"Found {curveList.Count} curves");

            var boundingBox = 
                Float64BoundingBox2D.Containing<IFloat64BoundedShape2D>(curveList);

            var drawingBoard = ImageSharpDrawingBoard.Create(
                WorldToScreenMap2D.FromScreenWidth(
                    boundingBox, 
                    1600
                ),
                Color.White
            );

            foreach (var curve2 in curveList)
                drawingBoard.DrawPolyLine(
                    curve2.ToAdaptivePolyline(),
                    new ImageSharpLineDrawingStyle(Color.Black, 1)
                );

            drawingBoard.Image.SaveAsPng(
                Path.Combine(workingFolder, "Constrained Accelerations Example.png"),
                new PngEncoder()
                {
                    ColorType = PngColorType.RgbWithAlpha,
                    CompressionLevel = PngCompressionLevel.NoCompression,
                    TransparentColorMode = PngTransparentColorMode.Clear
                }
            );

            Console.WriteLine("Bounding box: " + boundingBox);
        }
        
        public static void ConstrainedAccelerationsExample2()
        {
            const string workingFolder = @"D:\";

            var randGen = new Random(10);
            
            LinFloat64Vector2D point0 = (-5, 0);
            LinFloat64Vector2D point1 = (5, 0);

            var originalCurve = PhBernsteinCurve2D.CreateRandom(
                randGen, 3, 3, 3
            ).SetEndPoints(point0, point1);

            var tangent0 = originalCurve.GetTangentVector(0);
            var tangent1 = originalCurve.GetTangentVector(1);

            var acceleration0 = originalCurve.GetAccelerationVector(0);
            var acceleration1 = originalCurve.GetAccelerationVector(1);

            Console.WriteLine("Original endpoint constraints:");
            Console.WriteLine($"       Positions: {point0.ToTupleString()}, {point1.ToTupleString()}");
            Console.WriteLine($"        Tangents: {tangent0.ToTupleString()}, {tangent1.ToTupleString()}");
            Console.WriteLine($"   Accelerations: {acceleration0.ToTupleString()}, {acceleration1.ToTupleString()}");
            Console.WriteLine();

            var curveList = new List<PhBernsteinCurve2D>();

            var i = 100;
            var j = 0;
            while (i > 0)
            {
                var uDegree = 3; //randGen.GetInt32(3, 6);
                var vDegree = 3; //randGen.GetInt32(3, 6);
                var wDegree = 3; //randGen.GetInt32(3, 6);

                var curve = PhBernsteinCurve2D.CreateRandom(randGen, uDegree, vDegree, wDegree);

                (_, curve, _) = curve.SetEndPoints(
                    point0, 
                    point1, 
                    tangent0, 
                    tangent1, 
                    acceleration0
                );

                var p0 = (curve.GetPoint(0) - point0).L2Norm();
                var p1 = (curve.GetPoint(1) - point1).L2Norm();

                var t0 = (curve.GetTangentVector(0) - tangent0).L2Norm();
                var t1 = (curve.GetTangentVector(1) - tangent1).L2Norm();

                var a0 = (curve.GetAccelerationVector(0) - acceleration0).L2Norm();

                Console.WriteLine($"Curve {i} endpoint constraint errors:");
                Console.WriteLine($"       Positions: {p0}, {p1}");
                Console.WriteLine($"        Tangents: {t0}, {t1}");
                Console.WriteLine($"   Accelerations: {a0}");
                Console.WriteLine();

                j++;

                if (p0 > 1e-5 || p1 > 1e-5 || t0 > 1e-5 || t1 > 1e-5 || a0 > 1e-5)
                    continue;

                var hasCusp = curve.HasCusps();
                var hasLoop = curve.HasLoops();

                if (hasCusp || hasLoop)
                    continue;

                curveList.Add(curve);
                i--;
            }

            Console.WriteLine($"Found {curveList.Count} curves from {j} curves");

            var boundingBox = 
                Float64BoundingBox2D.Containing<IFloat64BoundedShape2D>(curveList);

            var drawingBoard = ImageSharpDrawingBoard.Create(
                WorldToScreenMap2D.FromScreenWidth(
                    boundingBox, 
                    1600*5
                ),
                Color.White
            );

            foreach (var curve2 in curveList)
                drawingBoard.DrawPolyLine(
                    curve2.ToAdaptivePolyline(),
                    new ImageSharpLineDrawingStyle(Color.Black, 1)
                );

            drawingBoard.Image.SaveAsPng(
                Path.Combine(workingFolder, "Constrained Accelerations Example.png"),
                new PngEncoder()
                {
                    ColorType = PngColorType.RgbWithAlpha,
                    CompressionLevel = PngCompressionLevel.NoCompression,
                    TransparentColorMode = PngTransparentColorMode.Clear
                }
            );

            Console.WriteLine("Bounding box: " + boundingBox);
        }
        
        public static void LineJoinExample1()
        {
            const string workingFolder = @"D:\";
            const int degree = 5;
            const double scalingFactor = 10d;
            

            var randGen = new Random(10);

            var i = 100;
            while (i > 0)
            {
                var p1 = randGen.GetLinVector2D(1, 10);
                var p2 = randGen.GetLinVector2D(1, 10);
                var p3 = randGen.GetLinVector2D(1, 10);

                var angle = (p1 - p2).GetAngleCCw(p3 - p2).DegreesValue;
                if (angle < 1 || angle > 359) continue;

                i--;

                Console.Write($"Angle {100 - i:D2}: {angle:G} degrees ");

                var (found, phCurve) = 
                    PhBernsteinCurve2D.TryJoinLinesC2(randGen, degree, p1, p2, p3);

                if (!found)
                {
                    Console.WriteLine($"not found");
                    continue;
                }

                var phCurvePolyline = 
                    phCurve.ToAdaptivePolyline();

                var controlPolygon = 
                    new Float64Polyline2D(p1, p2, p3);

                var boundingBox = 
                    Float64BoundingBox2D.Containing(phCurvePolyline.Concat(controlPolygon));

                var drawingBoard = ImageSharpDrawingBoard.Create(
                    WorldToScreenMap2D.FromScreenWidth(
                        boundingBox, 
                        1600
                    ),
                    Color.White
                );

                drawingBoard.DrawPolyLine(
                    controlPolygon, 
                    new ImageSharpLineDrawingStyle(Color.Black, 1)
                );
                    
                drawingBoard.DrawPolyLine(
                    phCurvePolyline,
                    new ImageSharpLineDrawingStyle(Color.Blue, 3)
                );

                drawingBoard.Image.SaveAsPng(
                    Path.Combine(workingFolder, $"Line Join Example 1 {i:D3}.png"),
                    new PngEncoder()
                    {
                        ColorType = PngColorType.RgbWithAlpha,
                        CompressionLevel = PngCompressionLevel.NoCompression,
                        TransparentColorMode = PngTransparentColorMode.Clear
                    }
                );

                Console.WriteLine($"finished");
            }
        }

        public static void LineJoinExample2()
        {
            const string workingFolder = @"D:\";
            const int degree = 5;
            const double scalingFactor = 1d;

            var randGen = new Random(10);

            var angle1 = 5d;
            var angle2 = 360 - angle1;
            var angleCount = (angle2 - angle1 + 1).CeilingToInt32();
            var angleList = 
                angle1.GetLinearRange(angle2, angleCount)
                    .Select(a => a.DegreesToPolarAngle())
                    .ToArray();

            for (var i = 0; i < angleList.Length; i++)
            {
                var angle = angleList[i];

                var (found,  phCurve) =
                    PhBernsteinCurve2D.TryJoinLinesC2(randGen, degree, angle);

                if (!found) continue;
                
                var p0 = phCurve.GetPoint(0);
                var pMid = phCurve.GetPoint(0.5);
                var p1 = phCurve.GetPoint(1);
                
                Console.WriteLine($"Found solution for angle {i}: {angle.Degrees} degrees");
                    
                var boundingBox = 
                    Float64BoundingBox2D.Containing(
                        (-scalingFactor, -scalingFactor), 
                        (scalingFactor, scalingFactor)
                    );

                var drawingBoard = ImageSharpDrawingBoard.Create(
                    WorldToScreenMap2D.FromScreenWidth(
                        boundingBox, 
                        1600
                    ),
                    Color.White
                );

                drawingBoard.DrawPolyLine(
                    [p0, LinFloat64Vector2D.Zero, p1], 
                    new ImageSharpLineDrawingStyle(Color.Black, 1)
                );
                    
                drawingBoard.DrawLine(
                    (LinFloat64Vector2D.Zero, pMid),
                    new ImageSharpLineDrawingStyle(Color.Black, 1)
                );

                drawingBoard.DrawPolyLine(
                    phCurve.ToAdaptivePolyline(),
                    new ImageSharpLineDrawingStyle(Color.Blue, 3)
                );

                drawingBoard.Image.SaveAsPng(
                    Path.Combine(workingFolder, $"Line Join Example 2 {i:D3}.png"),
                    new PngEncoder()
                    {
                        ColorType = PngColorType.RgbWithAlpha,
                        CompressionLevel = PngCompressionLevel.NoCompression,
                        TransparentColorMode = PngTransparentColorMode.Clear
                    }
                );
            }
        }

        public static void OptimizationExample()
        {
            const string workingFolder = @"D:\";

            var randGen = new Random(10);
            
            LinFloat64Vector2D point0 = (-5, 0);
            LinFloat64Vector2D point1 = (5, 0);

            var originalCurve = PhBernsteinCurve2D.CreateRandom(
                randGen, 3, 3, 3
            ).SetEndPoints(point0, point1);

            var tangent0 = originalCurve.GetTangentVector(0);
            var tangent1 = originalCurve.GetTangentVector(1);

            var acceleration0 = originalCurve.GetAccelerationVector(0);
            var acceleration1 = originalCurve.GetAccelerationVector(1);

            Console.WriteLine("Original endpoint constraints:");
            Console.WriteLine($"       Positions: {point0.ToTupleString()}, {point1.ToTupleString()}");
            Console.WriteLine($"        Tangents: {tangent0.ToTupleString()}, {tangent1.ToTupleString()}");
            Console.WriteLine($"   Accelerations: {acceleration0.ToTupleString()}, {acceleration1.ToTupleString()}");
            Console.WriteLine();

            var curveList = new List<PhBernsteinCurve2D>();

            for (var i = 0; i < 100; i++)
            {
                var uDegree = 3; //randGen.GetInt32(3, 6);
                var vDegree = 3; //randGen.GetInt32(3, 6);
                var wDegree = 3; //randGen.GetInt32(3, 6);

                var curve = PhBernsteinCurve2D.CreateRandom(randGen, uDegree, vDegree, wDegree);

                (_, curve, _) = curve.SetEndPoints(
                    point0, 
                    point1, 
                    tangent0, 
                    tangent1, 
                    acceleration0, 
                    acceleration1
                );

                var p0 = (curve.GetPoint(0) - point0).L2Norm();
                var p1 = (curve.GetPoint(1) - point1).L2Norm();

                var t0 = (curve.GetTangentVector(0) - tangent0).L2Norm();
                var t1 = (curve.GetTangentVector(1) - tangent1).L2Norm();

                var a0 = (curve.GetAccelerationVector(0) - acceleration0).L2Norm();
                var a1 = (curve.GetAccelerationVector(1) - acceleration1).L2Norm();

                Console.WriteLine($"Curve {i} endpoint constraint errors:");
                Console.WriteLine($"       Positions: {p0}, {p1}");
                Console.WriteLine($"        Tangents: {t0}, {t1}");
                Console.WriteLine($"   Accelerations: {a0}, {a1}");
                Console.WriteLine();

                if (p0 > 1e-5 || p1 > 1e-5 || t0 > 1e-5 || t1 > 1e-5 || a0 > 1e-5 || a1 > 1e-5)
                    continue;

                curveList.Add(curve);
            }

            Console.WriteLine($"Found {curveList.Count} curves");

            var boundingBox = 
                Float64BoundingBox2D.Containing<IFloat64BoundedShape2D>(curveList);

            var drawingBoard = ImageSharpDrawingBoard.Create(
                WorldToScreenMap2D.FromScreenWidth(
                    boundingBox, 
                    1600
                ),
                Color.White
            );

            foreach (var curve2 in curveList)
                drawingBoard.DrawPolyLine(
                    curve2.ToAdaptivePolyline(),
                    new ImageSharpLineDrawingStyle(Color.Black, 1)
                );

            drawingBoard.Image.SaveAsPng(
                Path.Combine(workingFolder, "Constrained Accelerations Example.png"),
                new PngEncoder()
                {
                    ColorType = PngColorType.RgbWithAlpha,
                    CompressionLevel = PngCompressionLevel.NoCompression,
                    TransparentColorMode = PngTransparentColorMode.Clear
                }
            );

            Console.WriteLine("Bounding box: " + boundingBox);
        }
    }
}
