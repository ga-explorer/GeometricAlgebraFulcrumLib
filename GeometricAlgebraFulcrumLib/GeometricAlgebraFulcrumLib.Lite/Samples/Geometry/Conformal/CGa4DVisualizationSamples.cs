using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Interpolation;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Operations;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars.Harmonic;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves.Polar;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using WebComposerLib.Colors;

namespace GeometricAlgebraFulcrumLib.Lite.Samples.Geometry.Conformal;

public static class CGa4DVisualizationSamples
{
    public static RGaConformalSpace4D CGa
        => RGaConformalSpace4D.Instance;
    
    public static IParametricScalar GetRadiusCurve1(double maxTime)
    {
        var parameterRange = Float64ScalarRange.Create(0, maxTime);

        var rCurve = CosWaveParametricScalar.Create(parameterRange, 1, 5, 6);

        return rCurve;
    }

    public static IParametricCurve2D GetPositionCurve1(double maxTime)
    {
        var freqHz = 1 / maxTime;
        var freq = 2 * Math.PI * freqHz;

        var parameterRange = Float64ScalarRange.Create(0, maxTime);

        var rCurve = CosWaveParametricScalar.Create(parameterRange, 3, 5, 3);
        var thetaCurve = LinearParametricScalar.Create(2 * freq);
        
        var curve = PolarCurve2D.Create(
            parameterRange,
            rCurve,
            thetaCurve
        );

        return curve;
    }

    public static IParametricCurve2D GetPositionCurve2(double maxTime)
    {
        var freqHz = 1 / maxTime;
        var freq = 2 * Math.PI * freqHz;

        var parameterRange = Float64ScalarRange.Create(0, maxTime);

        var rCurve = CosWaveParametricScalar.Create(parameterRange, 3, 5, 1);
        var thetaCurve = LinearParametricScalar.Create(1 * freq);
        
        var curve = PolarCurve2D.Create(
            parameterRange,
            rCurve,
            thetaCurve
        );

        return curve;
    }
    
    
    /// <summary>
    /// Define a CGA line using two parametric point curves
    /// </summary>
    public static void ParametricLineExample1()
    {
        const double maxTime = 24;
        const int frameRate = 10;

        Console.WriteLine("Animated Example Started ..");

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);
        
        // This curve defines the parametric position of the first point
        var point1Curve =
            GetPositionCurve1(maxTime);

        // This curve defines the parametric position of the second point
        var point2Curve =
            GetPositionCurve2(maxTime);

        // Define the line element
        var flatLine =
            CGa.DefineFlatLineFromPoints(
                point1Curve,
                point2Curve
            );
        
        // Prepare latex expressions
        var latexDictionary = new Dictionary<string, string>()
        {
            {"point1", @"P_{1}"},
            {"point2", @"P_{2}"}
        };
        
        // Initialize visualizer for 2D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing2D(@"Parametric 2D Line Example 1", latexDictionary);
        
        // Draw the two position curves
        CGa.Visualizer
            .SetCurveStyleTube(0.03)
            .DrawCurve(
                Color.Red, 
                point1Curve, 
                animationSpecs.FrameTimeRange
            ).DrawCurve(
                Color.Green, 
                point2Curve, 
                animationSpecs.FrameTimeRange
            );
        
        // Draw the two points
        CGa.Visualizer
            .SetPointStyle(0.07)
            .DrawPoint(
                Color.Red, 
                point1Curve
            ).DrawPoint(
                Color.Green, 
                point2Curve
            );

        // Draw the parametric line
        CGa.Visualizer
            .SetFlatStyle(
                0.06, 
                12,
                true, 
                1, 
                1,
                1
            ).DrawFlatLine2D(Color.Bisque, flatLine);
        
        // Draw LaTeX expressions
        var latexOffset = Float64Vector2D.CreateSymmetricVector(0.5);

        CGa.Visualizer
            .DrawLaTeX("point1", point1Curve.GetOffsetCurve(latexOffset))
            .DrawLaTeX("point2", point2Curve.GetOffsetCurve(latexOffset));
        
        // Save the HTML animation file
        CGa.Visualizer.SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");
    }
    
    /// <summary>
    /// Define a CGA line using tangent to a parametric curve
    /// </summary>
    public static void ParametricLineExample2()
    {
        const double maxTime = 24;
        const int frameRate = 10;

        Console.WriteLine("Animated Example Started ..");

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);
        
        // This curve defines the parametric position of the first point
        var positionCurve =
            GetPositionCurve1(maxTime);

        var vectorCurve =
            positionCurve.GetTangentCurve();

        // Define the line element
        var flatLine =
            CGa.DefineFlatLine(
                positionCurve,
                vectorCurve
            );
        
        // Prepare latex expressions
        var latexDictionary = new Dictionary<string, string>()
        {
            {"point", @"P"}
        };
        
        // Initialize visualizer for 2D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing2D(@"Parametric 2D Line Example 2", latexDictionary);
        
        // Draw the position curve
        CGa.Visualizer
            .SetCurveStyleTube(0.05)
            .DrawCurve(
                Color.Red, 
                positionCurve, 
                animationSpecs.FrameTimeRange
            );
        
        // Draw the parametric line
        CGa.Visualizer
            .SetFlatStyle(
                0.065, 
                12,
                true, 
                1, 
                1,
                0.65
            ).DrawFlatLine2D(Color.Bisque, flatLine);
        
        // Draw LaTeX expressions
        var latexOffset = Float64Vector2D.CreateSymmetricVector(0.5);

        CGa.Visualizer
            .DrawLaTeX("point", positionCurve.GetOffsetCurve(latexOffset));
        
        // Save the HTML animation file
        CGa.Visualizer.SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");
    }
    
    /// <summary>
    /// Define a CGA point-pair using two parametric point curves
    /// </summary>
    public static void ParametricPointPairExample1()
    {
        const double maxTime = 24;
        const int frameRate = 10;

        Console.WriteLine("Animated Example Started ..");

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);
        
        // This curve defines the parametric position of the first point
        var point1Curve =
            GetPositionCurve1(maxTime);

        // This curve defines the parametric position of the second point
        var point2Curve =
            GetPositionCurve2(maxTime);

        // Define the point-pair element
        var roundPointPair =
            CGa.DefineRealRoundPointPairFromPoints(
                point1Curve,
                point2Curve
            );
        
        // Prepare latex expressions
        var latexDictionary = new Dictionary<string, string>()
        {
            {"point1", @"P_{1}"},
            {"point2", @"P_{2}"}
        };
        
        // Initialize visualizer for 2D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing2D(@"Parametric 2D Point-Pair Example 1", latexDictionary);
        
        // Draw the two point curves
        CGa.Visualizer
            .SetCurveStyleTube(0.03)
            .DrawCurve(
                Color.Red.SetAlpha(0.2), 
                point1Curve, 
                animationSpecs.FrameTimeRange
            ).DrawCurve(
                Color.Blue.SetAlpha(0.2), 
                point2Curve, 
                animationSpecs.FrameTimeRange
            );
        
        // Draw the parametric point-pair
        CGa.Visualizer
            .SetRoundStyle(
                0.08, 
                true, 
                false, 
                1, 
                1
            ).DrawRoundPointPair2D(Color.Bisque, roundPointPair);
        
        // Draw LaTeX expressions
        var latexOffset = Float64Vector2D.CreateSymmetricVector(0.5);

        CGa.Visualizer
            .DrawLaTeX("point1", point1Curve.GetOffsetCurve(latexOffset))
            .DrawLaTeX("point2", point2Curve.GetOffsetCurve(latexOffset));
        
        // Save the HTML animation file
        CGa.Visualizer.SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");
    }
    
    /// <summary>
    /// Define a CGA point-pair using a parametric center curve and a radius curve
    /// </summary>
    public static void ParametricPointPairExample2()
    {
        const double maxTime = 24;
        const int frameRate = 10;

        Console.WriteLine("Animated Example Started ..");

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);
        
        // This curve defines the parametric radius of the circle containing the point-pair
        var radiusCurve =
            GetRadiusCurve1(maxTime);

        // This curve defines the parametric center of the circle containing the point-pair
        var centerCurve =
            GetPositionCurve1(maxTime);

        // Define the direction of the point pair as the tangent vector to the center curve
        var vectorCurve =
            centerCurve.GetTangentCurve();

        // Define the point-pair element
        var roundPointPair =
            CGa.DefineRoundPointPair(
                radiusCurve,
                centerCurve,
                vectorCurve
            );
        
        // Prepare latex expressions
        var latexDictionary = new Dictionary<string, string>()
        {
            {"center", @"C"}
        };
        
        // Initialize visualizer for 2D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing2D(@"Parametric 2D Point-Pair Example 2", latexDictionary);

        // Draw the position curve
        CGa.Visualizer
            .SetCurveStyleTube(0.03)
            .DrawCurve(
                Color.Red.SetAlpha(0.2), 
                centerCurve, 
                animationSpecs.FrameTimeRange
            );
        
        // Draw the parametric point-pair
        CGa.Visualizer
            .SetRoundStyle(
                0.08, 
                true, 
                false, 
                1, 
                1
            ).DrawRoundPointPair2D(Color.Bisque, roundPointPair);
        
        // Draw LaTeX expressions
        var latexOffset = Float64Vector2D.CreateSymmetricVector(0.5);

        CGa.Visualizer.DrawLaTeX(
            "center", 
            centerCurve.GetOffsetCurve(latexOffset)
        );
        
        // Save the HTML animation file
        CGa.Visualizer.SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");
    }
    
    /// <summary>
    /// Define a CGA circle using two parametric point curves and a fixed point
    /// </summary>
    public static void ParametricCircleExample1()
    {
        const double maxTime = 24;
        const int frameRate = 30;

        Console.WriteLine("Animated Example Started ..");

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);
        
        // This curve defines the parametric position of the first point
        var point1Curve =
            GetPositionCurve1(maxTime);

        // This curve defines the parametric position of the second point
        var point2Curve =
            GetPositionCurve2(maxTime);

        // This constant curve defines the position of the third point
        var point3Curve = 
            ConstantParametricCurve2D.Create(2, 2);

        // Define the circle element
        var roundCircle =
            CGa.DefineRoundCircleFromPoints(
                point1Curve,
                point2Curve,
                point3Curve
            );
        
        // Prepare latex expressions
        var latexDictionary = new Dictionary<string, string>()
        {
            {"point1", @"P_{1}"},
            {"point2", @"P_{2}"},
            {"point3", @"P_{3}"}
        };
        
        // Initialize visualizer for 2D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing2D(@"Parametric 2D Circle Example 1", latexDictionary);

        // Draw the two position curves
        CGa.Visualizer
            .SetCurveStyleTube(0.03)
            .DrawCurve(
                Color.Red.SetAlpha(0.2), 
                point1Curve, 
                animationSpecs.FrameTimeRange
            ).DrawCurve(
                Color.Green.SetAlpha(0.2), 
                point2Curve, 
                animationSpecs.FrameTimeRange
            );

        // Draw the 3 points defining the circle
        CGa.Visualizer
            .SetPointStyle(0.08)
            .DrawPoint(Color.Red, point1Curve)
            .DrawPoint(Color.Green, point2Curve)
            .DrawPoint(Color.Blue, point3Curve.Point);
        
        // Draw the parametric circle
        CGa.Visualizer
            .SetRoundStyle(
                0.04, 
                true, 
                false, 
                1, 
                1
            ).DrawRoundCircle2D(Color.Bisque, roundCircle);

        // Draw LaTeX expressions
        var latexOffset = Float64Vector2D.CreateSymmetricVector(0.5);

        CGa.Visualizer
            .DrawLaTeX("point1", point1Curve.GetOffsetCurve(latexOffset))
            .DrawLaTeX("point2", point2Curve.GetOffsetCurve(latexOffset))
            .DrawLaTeX("point3", point3Curve.Point + latexOffset);
        
        // Save the HTML animation file
        CGa.Visualizer.SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");
    }
    
    /// <summary>
    /// Define a CGA circle using a parametric center curve and a radius curve
    /// </summary>
    public static void ParametricCircleExample2()
    {
        const double maxTime = 24;
        const int frameRate = 10;

        Console.WriteLine("Animated Example Started ..");

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);
        
        // This curve defines the parametric radius of the circle containing the circle
        var radiusCurve =
            GetRadiusCurve1(maxTime);

        // This curve defines the parametric center of the circle containing the circle
        var centerCurve =
            GetPositionCurve1(maxTime);
        
        // Define the circle element
        var roundCircle =
            CGa.DefineRoundCircle(
                radiusCurve,
                centerCurve
            );
        
        // Prepare latex expressions
        var latexDictionary = new Dictionary<string, string>()
        {
            {"center", @"C"}
        };
        
        // Initialize visualizer for 2D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing2D(@"Parametric 2D Circle Example 2", latexDictionary);
        
        // Draw the center curve
        CGa.Visualizer
            .SetCurveStyleTube(0.03)
            .DrawCurve(
                Color.Red.SetAlpha(0.2), 
                centerCurve, 
                animationSpecs.FrameTimeRange
            );
        
        // Draw the parametric circle
        CGa.Visualizer
            .SetRoundStyle(
                0.04, 
                true, 
                false, 
                1, 
                1
            ).DrawRoundCircle2D(Color.Bisque, roundCircle);
        
        // Draw LaTeX expressions
        var latexOffset = Float64Vector2D.CreateSymmetricVector(0.5);

        CGa.Visualizer.DrawLaTeX(
            "center", 
            centerCurve.GetOffsetCurve(latexOffset)
        );
        
        // Save the HTML animation file
        CGa.Visualizer.SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");
    }
    
    /// <summary>
    /// Define two parametric circles and draw them
    /// and their animated intersection circle (imaginary or real)
    /// </summary>
    public static void ParametricCircleCircleIntersectionExample()
    {
        const double maxTime = 36;
        const int frameRate = 10;

        Console.WriteLine("Animated Example Started ..");

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);
        
        // Define a parametric real circle
        // This curve defines the parametric center of the circle
        var positionCurve1 =
            GetPositionCurve1(maxTime);
        
        // Define the circle based on the parametric center with fixed radius
        var roundCircle1 =
            CGa.DefineRealRoundCircle(
                2,
                positionCurve1
            );

        // Define a parametric real circle
        // This curve defines the parametric center of the circle
        var positionCurve2 =
            GetPositionCurve2(maxTime);
        
        // Define the circle based on the parametric center with fixed radius
        var roundCircle2 =
            CGa.DefineRealRoundCircle(
                1.5,
                positionCurve2
            );

        // Define the parametric intersection point-pair of the two circles
        var intersectionElement =
            roundCircle1.Intersect(roundCircle2);

        // Prepare latex expressions
        var latexDictionary = new Dictionary<string, string>()
        {
            {"element1", @"A"},
            {"element2", @"B"},
            {"intersectionElement", @"A \cap B"},
        };
        
        // Initialize visualizer for 2D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing2D(@"Parametric 2D Circle-Circle Intersection Example", latexDictionary);
        
        // Draw curves for circle centers
        //Ga.Visualizer
        //    .SetCurveStyleTube(0.03)
        //    .DrawCurve(
        //        Color.Red.SetAlpha(0.2), 
        //        positionCurve1, 
        //        animationSpecs.TimeRange
        //    ).DrawCurve(
        //        Color.Green.SetAlpha(0.2), 
        //        positionCurve2, 
        //        animationSpecs.TimeRange
        //    );
        
        // Draw the two circles and their intersection circle
        // The circle containing the circle is also shown
        CGa.Visualizer
            .SetRoundStyle(
                0.065, 
                true, 
                false, 
                1, 
                1,
                0.4
            )
            .DrawRoundCircle2D(Color.Red.SetAlpha(0.4), roundCircle1)
            .DrawRoundCircle2D(Color.Green.SetAlpha(0.4), roundCircle2)
            .DrawRoundPointPair2D(Color.Yellow, intersectionElement);
        
        // Draw the line segment between the two circle centers
        CGa.Visualizer
            //.SetCurveStyleDashed(5, 3, 16)
            //.SetCurveStyleSolid()
            .SetCurveStyleTube(0.065)
            .DrawLineCurve(
                Color.Yellow,
                positionCurve1, 
                positionCurve2
            );

        // Draw LaTeX expressions
        CGa.Visualizer
            .DrawLaTeX(
                "element1",
                roundCircle1.GetRoundSurfacePointCurve2D(
                    Float64Vector2D.Symmetric, 
                    0.5
                )
            ).DrawLaTeX(
                "element2",
                roundCircle2.GetRoundSurfacePointCurve2D(
                    Float64Vector2D.Symmetric, 
                    0.5
                )
            ).DrawLaTeX(
                "intersectionElement",
                intersectionElement.GetRoundSurfacePointCurve2D(
                    Float64Vector2D.E2, 
                    0.5
                )
            ).SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");
    }
    
    /// <summary>
    /// Define a parametric circle and line, and draw them
    /// and their animated intersection point-pair (imaginary or real)
    /// </summary>
    public static void ParametricCircleLineIntersectionExample()
    {
        const double maxTime = 36;
        const int frameRate = 30;

        Console.WriteLine("Animated Example Started ..");

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);
        
        // Define a parametric real circle
        // This curve defines the parametric center of the circle
        var positionCurve1 =
            GetPositionCurve1(maxTime);
        
        // Define the circle based on the parametric center with fixed radius
        var roundCircle =
            CGa.DefineRealRoundCircle(
                4,
                positionCurve1
            );

        // Encode a parametric line
        // This curve defines the parametric position of the line
        var positionCurve2 =
            GetPositionCurve2(maxTime);
        
        // Define the line based on the parametric position with normal equal to
        // the curve tangent
        var flatLine =
            CGa.DefineFlatLine(
                positionCurve2,
                positionCurve2.GetTangentCurve()
            );

        // Define the parametric intersection point-pair of the circle and line
        var intersectionElement =
            roundCircle.Intersect(flatLine);

        var (p1Curve, p2Curve) = 
            intersectionElement.GetRoundPointPairCurves2D();

        var p12Curve = 
            intersectionElement.GetRoundCenterCurve2D();
        
        // Prepare latex expressions
        var latexDictionary = new Dictionary<string, string>()
        {
            {"element1", @"A"},
            {"element2", @"B"},
            {"intersectionElement", @"A \cap B"},
        };
        
        // Initialize visualizer for 2D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing2D(@"Parametric 2D Circle-Line Intersection Example", latexDictionary);
        
        // Draw curves for circle centers
        //Ga.Visualizer
        //    .SetCurveStyleTube(0.03)
        //    .DrawCurve(
        //        Color.Red.SetAlpha(0.2), 
        //        positionCurve1, 
        //        animationSpecs.TimeRange
        //    ).DrawCurve(
        //        Color.Green.SetAlpha(0.2), 
        //        positionCurve2, 
        //        animationSpecs.TimeRange
        //    );
        
        // Draw the circle and line and their intersection point-pair
        // The circle containing the circle is also shown
        CGa.Visualizer
            .SetFlatStyle(
                0.065, 
                12,
                false, 
                0, 
                0,
                0.8
            )
            .SetRoundStyle(
                0.08, 
                true, 
                false, 
                1, 
                1,
                0.8
            )
            .DrawRoundCircle2D(Color.Red.SetAlpha(0.4), roundCircle)
            .DrawFlatLine2D(Color.Green.SetAlpha(0.8), flatLine)
            .DrawRoundPointPair2D(Color.Yellow, intersectionElement);

        // Draw the line segments between the circle center and point-pair
        // midpoint and two edge points
        CGa.Visualizer
            .SetCurveStyleDashed(5, 3, 16)
            //.SetCurveStyleSolid()
            //.SetCurveStyleTube(0.065)
            .DrawLineCurve(
                Color.Yellow,
                positionCurve1,
                p1Curve
            ).DrawLineCurve(
                Color.Yellow,
                positionCurve1,
                p2Curve
            ).SetCurveStyleSolid()
            .DrawLineCurve(
                Color.Yellow,
                positionCurve1,
                p12Curve
            );

        // Draw LaTeX expressions
        CGa.Visualizer
            .DrawLaTeX(
                "element1",
                flatLine.GetSurfacePointCurve2D(
                    Float64Vector2D.Symmetric,
                    5,
                    0.5
                )
            ).DrawLaTeX(
                "element2",
                roundCircle.GetRoundSurfacePointCurve2D(
                    Float64Vector2D.Symmetric, 
                    0.5
                )
            ).DrawLaTeX(
                "intersectionElement",
                intersectionElement.GetRoundSurfacePointCurve2D(
                    Float64Vector2D.E2, 
                    1
                )
            ).SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");
    }
    
    /// <summary>
    /// Define a parametric circle and a circle, and draw them
    /// and the circle reflection and projection on the circle
    /// </summary>
    public static void ParametricCircleOnCircleReflectionExample()
    {
        const double maxTime = 36;
        const int frameRate = 30;

        Console.WriteLine("Animated Example Started ..");

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);
        
        // Encode a parametric real circle
        // This curve defines the parametric center of the circle
        var positionCurve1 =
            GetPositionCurve1(maxTime);
        
        // Define the circle based on the parametric center with fixed radius
        var roundCircle1 =
            CGa.DefineRealRoundCircle(
                3,
                positionCurve1
            );

        // Encode a parametric circle
        // This curve defines the parametric center of the circle
        var positionCurve2 =
            GetPositionCurve2(maxTime);
        
        // Define the circle based on the parametric position with normal equal to
        // the curve tangent
        var roundCircle2 =
            CGa.DefineRealRoundCircle(
                2,
                positionCurve2
            );
        
        // Define the reflection of the circle on the circle as a parametric
        var reflectionElement =
            roundCircle2.ReflectOn(roundCircle1);

        // Prepare latex expressions
        var latexDictionary = new Dictionary<string, string>()
        {
            {"element1", @"A"},
            {"element2", @"B"},
            {"reflectionElement", @"A B A^{-1}"},
        };
        
        // Initialize visualizer for 2D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing2D(@"Parametric 2D Circle On Circle Reflection Example", latexDictionary);
        
        // Draw curves for circle centers
        //Ga.Visualizer
        //    .SetCurveStyleTube(0.03)
        //    .DrawCurve(
        //        Color.Red.SetAlpha(0.2), 
        //        positionCurve1, 
        //        animationSpecs.TimeRange
        //    ).DrawCurve(
        //        Color.Green.SetAlpha(0.2), 
        //        positionCurve2, 
        //        animationSpecs.TimeRange
        //    );
        
        // Draw the two circles and their intersection circle
        // The circle containing the circle is also shown
        CGa.Visualizer
            .SetRoundStyle(
                0.04, 
                true, 
                false, 
                1, 
                1,
                0.6
            )
            .DrawRoundCircle2D(Color.Red.SetAlpha(0.4), roundCircle1)
            .DrawRoundCircle2D(Color.Green.SetAlpha(0.4), roundCircle2)
            .DrawRoundCircle2D(Color.Yellow, reflectionElement);
        
        // Draw LaTeX expressions
        CGa.Visualizer
            .DrawLaTeX(
                "element1",
                roundCircle1.GetRoundSurfacePointCurve2D(
                    Float64Vector2D.Symmetric, 
                    0.5
                )
            ).DrawLaTeX(
                "element2",
                roundCircle2.GetRoundSurfacePointCurve2D(
                    -Float64Vector2D.Symmetric, 
                    0.5
                )
            ).DrawLaTeX(
                "reflectionElement",
                reflectionElement.GetRoundSurfacePointCurve2D(
                    Float64Vector2D.E2, 
                    0.5
                )
            ).SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");

    }
    
    /// <summary>
    /// Define a parametric line and a circle, and draw them
    /// and the circle reflection and projection on the line
    /// </summary>
    public static void ParametricCircleOnLineReflectionExample()
    {
        const double maxTime = 36;
        const int frameRate = 10;

        Console.WriteLine("Animated Example Started ..");

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);
        
        // Encode a parametric real line
        // This curve defines the parametric position and normal of the line
        var positionCurve1 =
            ConstantParametricCurve2D.Create(
                animationSpecs.FrameTimeRange,
                Float64Vector2D.Create(0, 0),
                Float64Vector2D.Create(0, 1)
            );
            //GetPositionCurve1(maxTime);
        
        // Define the line based on the parametric position
        var flatLine =
            CGa.DefineFlatLine(
                positionCurve1,
                positionCurve1.GetTangentCurve()
            );

        // Encode a parametric circle
        // This curve defines the parametric center of the circle
        var positionCurve2 =
            GetPositionCurve2(maxTime);
        
        // Define the circle based on the parametric position with normal equal to
        // the curve tangent
        var roundCircle =
            CGa.DefineRealRoundCircle(
                2,
                positionCurve2
            );
        
        // Define the reflection of the circle on the line as a parametric
        var reflectionElement =
            roundCircle.ReflectOn(flatLine);

        // Prepare latex expressions
        var latexDictionary = new Dictionary<string, string>()
        {
            {"element1", @"A"},
            {"element2", @"B"},
            {"reflectionElement", @"A B A^{-1}"},
        };
        
        // Initialize visualizer for 2D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing2D(@"Parametric 2D Circle On Line Reflection Example", latexDictionary);
        
        // Draw curves for circle centers
        //Ga.Visualizer
        //    .SetCurveStyleTube(0.03)
        //    .DrawCurve(
        //        Color.Red.SetAlpha(0.2), 
        //        positionCurve1, 
        //        animationSpecs.TimeRange
        //    ).DrawCurve(
        //        Color.Green.SetAlpha(0.2), 
        //        positionCurve2, 
        //        animationSpecs.TimeRange
        //    );
        
        // Draw the two circles and their intersection circle
        // The circle containing the circle is also shown
        CGa.Visualizer
            .SetFlatStyle(
                0.04, 
                6,
                true, 
                1, 
                1,
                0.6
            ).SetRoundStyle(
                0.04, 
                true, 
                false, 
                1, 
                1,
                0.6
            )
            .DrawFlatLine2D(Color.Red.SetAlpha(0.4), flatLine)
            .DrawRoundCircle2D(Color.Green.SetAlpha(0.4), roundCircle)
            .DrawRoundCircle2D(Color.Yellow, reflectionElement);
        
        // Draw LaTeX expressions
        CGa.Visualizer
            .DrawLaTeX(
                "element1",
                flatLine.GetSurfacePointCurve2D(
                    Float64Vector2D.E2, 
                    5, 
                    0.5
                )
            ).DrawLaTeX(
                "element2",
                roundCircle.GetRoundSurfacePointCurve2D(
                    Float64Vector2D.Create(1, 1),
                    0.5
                )
            ).DrawLaTeX(
                "reflectionElement",
                reflectionElement.GetRoundSurfacePointCurve2D(
                    Float64Vector2D.Create(-1, 1),
                    0.5
                )
            ).SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");

    }
    
    /// <summary>
    /// Define a parametric line and a point-pair, and draw them
    /// and the point-pair reflection and projection on the line
    /// </summary>
    public static void ParametricPointPairOnLineReflectionExample()
    {
        const double maxTime = 24;
        const int frameRate = 10;

        Console.WriteLine("Animated Example Started ..");

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);
        
        // Encode a parametric real line
        // This curve defines the parametric position and normal of the line
        var positionCurve1 =
            ConstantParametricCurve2D.Create(
                animationSpecs.FrameTimeRange,
                Float64Vector2D.Create(0, 0),
                Float64Vector2D.Create(0, 1)
            );
            //GetPositionCurve1(maxTime);
        
        // Define the line based on the parametric position
        var flatLine =
            CGa.DefineFlatLine(
                positionCurve1,
                positionCurve1.GetTangentCurve()
            );

        // Encode a parametric point-pair
        // This curve defines the parametric center of the circle
        var positionCurve2 =
            GetPositionCurve2(maxTime);
        
        // Define the point-pair based on the parametric position with normal equal to
        // the curve tangent
        var roundPointPair =
            CGa.DefineRealRoundPointPair(
                2,
                positionCurve2,
                positionCurve2.GetTangentCurve()
            );

        // Define the projection of the point-pair on the line as a parametric
        var projectionElement =
            roundPointPair.ProjectOn(flatLine);
        
        // Define the reflection of the point-pair on the line as a parametric
        var reflectionElement =
            roundPointPair.ReflectOn(flatLine);

        // Prepare latex expressions
        var latexDictionary = new Dictionary<string, string>()
        {
            {"element1", @"A"},
            {"element2", @"B"},
            {"projectionElement", @"\left(B\bullet A\right)A^{-1}"},
            {"reflectionElement", @"A B A^{-1}"},
        };
        
        // Initialize visualizer for 2D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing2D(@"Parametric 2D Point-Pair On Line Reflection Example", latexDictionary);
        
        // Draw curves for circle centers
        //Ga.Visualizer
        //    .SetCurveStyleTube(0.03)
        //    .DrawCurve(
        //        Color.Red.SetAlpha(0.2), 
        //        positionCurve1, 
        //        animationSpecs.TimeRange
        //    ).DrawCurve(
        //        Color.Green.SetAlpha(0.2), 
        //        positionCurve2, 
        //        animationSpecs.TimeRange
        //    );
        
        // Draw the line and point-pair and their intersection
        // The circle containing the point-pair is also shown
        CGa.Visualizer
            .SetFlatStyle(
                0.04, 
                6,
                true, 
                1, 
                1,
                0.6
            ).SetRoundStyle(
                0.04, 
                true, 
                false, 
                1, 
                1,
                0.6
            )
            .DrawFlatLine2D(Color.Red.SetAlpha(0.4), flatLine)
            .DrawRoundPointPair2D(Color.Green.SetAlpha(0.4), roundPointPair)
            .DrawRoundPointPair2D(Color.Blue, projectionElement)
            .DrawRoundPointPair2D(Color.Yellow, reflectionElement);
        
        // Draw LaTeX expressions
        CGa.Visualizer
            .DrawLaTeX(
                "element1",
                flatLine.GetSurfacePointCurve2D(
                    Float64Vector2D.E2, 
                    5,
                    0.5
                )
            ).DrawLaTeX(
                "element2",
                roundPointPair.GetRoundSurfacePointCurve2D(
                    Float64Vector2D.E2, 
                    0.5
                )
            ).DrawLaTeX(
                "projectionElement",
                projectionElement.GetRoundSurfacePointCurve2D(
                    Float64Vector2D.E2, 
                    0.5
                )
            ).DrawLaTeX(
                "reflectionElement",
                reflectionElement.GetRoundSurfacePointCurve2D(
                    Float64Vector2D.E2, 
                    0.5
                )
            ).SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");
    }

    public static void ParametricCircleInterpolationExample()
    {
        const double maxTime = 12;
        const int frameRate = 50;

        Console.WriteLine("Animated Example Started ..");

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);
        
        // Encode two parametric circle blades
        var roundCircle1 =
            CGa.DefineRoundCircle(
                4,
                Float64Vector2D.Create(5, 5)
            );

        var roundCircle2 =
            CGa.DefineRoundCircle(
                -9,
                Float64Vector2D.Create(-5, -5),
                Float64Bivector2D.E21
            );

        // Initialize visualizer for 2D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing2D(@"Parametric 2D Circle Interpolation Example");
        
        // Draw the two circles
        CGa.Visualizer
            .SetRoundStyle(
                0.065, 
                true, 
                false, 
                1, 
                1,
                0.35
            )
            .DrawRound(Color.Red, roundCircle1)
            .DrawRound(Color.Green, roundCircle2);

        // Create a parametric periodic interpolation of the two circles
        var lerpCircle = 
            RGaConformalParametricElement.Create(
                CGa,
                animationSpecs.FrameTimeRange,
                t => 
                    (t / maxTime)
                        .CosWave(0, 1, 1)
                        .LerpCircle2D(
                            roundCircle1, 
                            roundCircle2
                        )
            );

        // Draw the animated parametric circle
        CGa.Visualizer.DrawRoundCircle2D(Color.Bisque, lerpCircle);

        // Draw LaTeX expressions
        CGa.Visualizer.SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");
    }
}