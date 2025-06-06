﻿using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Interpolation;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Operations;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Geometry.CGa;

public static class CGa4DVisualizationSamples
{
    public static CGaFloat64GeometricSpace4D CGa
        => CGaFloat64GeometricSpace4D.Instance;

    public static Float64ScalarSignal GetRadiusCurve1(double maxTime)
    {
        var timeRange = Float64ScalarRange.Create(0, maxTime);

        var rCurve = Float64ScalarSignal.FiniteCosWave(timeRange, 1, 5, 6);

        return rCurve;
    }

    public static Float64Path2D GetPositionCurve1(double maxTime)
    {
        var freqHz = 1 / maxTime;
        var freq = Math.Tau * freqHz;

        var timeRange = Float64ScalarRange.Create(0, maxTime);

        var rCurve = Float64ScalarSignal.FiniteCosWave(timeRange, 3, 5, 3);
        var thetaCurve = Float64ScalarSignal.FiniteRamp(timeRange, 0, 2 * freq);

        var curve = Float64PolarPath2D.Finite(
            timeRange,
            rCurve,
            thetaCurve
        );

        return curve;
    }

    public static Float64Path2D GetPositionCurve2(double maxTime)
    {
        var freqHz = 1 / maxTime;
        var freq = Math.Tau * freqHz;

        var timeRange = Float64ScalarRange.Create(0, maxTime);

        var rCurve = Float64ScalarSignal.FiniteCosWave(timeRange, 3, 5, 1);
        var thetaCurve = Float64ScalarSignal.FiniteRamp(timeRange, 0, 1 * freq);

        var curve = Float64PolarPath2D.Finite(
            timeRange,
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

        var samplingSpecs =
            Float64SamplingSpecs.Create(frameRate, maxTime);

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
        
        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer.BeginDrawing2D(
            @"D:\Projects\Study\Web\Babylon.js",
            samplingSpecs
        );
        
        CGa.VisualizerAnimationComposer.SetTitle(
            "Parametric 2D Line 1"
        );

        CGa.VisualizerKaTeXComposer.AddLaTeXCode(
            new Dictionary<string, string>()
            {
                {"point1", "P_{1}"},
                {"point2", "P_{2}"}
            }
        );

        // Draw the two position curves
        CGa.Visualizer.AnimationComposer
            .SetCurveStyleTube(0.03)
            .DrawCurve(
                Color.Red,
                point1Curve,
                samplingSpecs.TimeRange
            ).DrawCurve(
                Color.Green,
                point2Curve,
                samplingSpecs.TimeRange
            );

        // Draw the two points
        CGa.Visualizer.AnimationComposer
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
        var latexOffset = LinFloat64Vector2D.VectorSymmetric(0.5);

        CGa.Visualizer.AnimationComposer
            .DrawLaTeX("point1", point1Curve.GetOffsetCurve(latexOffset))
            .DrawLaTeX("point2", point2Curve.GetOffsetCurve(latexOffset));

        // Save the HTML animation file
        CGa.Visualizer.AnimationComposer.SaveHtmlFile();

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

        var samplingSpecs =
            Float64SamplingSpecs.Create(frameRate, maxTime);

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

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer.BeginDrawing2D(
            @"D:\Projects\Study\Web\Babylon.js",
            samplingSpecs
        );
        
        CGa.VisualizerAnimationComposer.SetTitle(
            "Parametric 2D Line 2"
        );

        CGa.VisualizerKaTeXComposer.AddLaTeXCode(
            new Dictionary<string, string>()
            {
                {"point", "P"}
            }
        );

        // Draw the position curve
        CGa.Visualizer.AnimationComposer
            .SetCurveStyleTube(0.05)
            .DrawCurve(
                Color.Red,
                positionCurve,
                samplingSpecs.TimeRange
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
        var latexOffset = LinFloat64Vector2D.VectorSymmetric(0.5);

        CGa.Visualizer.AnimationComposer
            .DrawLaTeX("point", positionCurve.GetOffsetCurve(latexOffset));

        // Save the HTML animation file
        CGa.Visualizer.AnimationComposer.SaveHtmlFile();

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

        var samplingSpecs =
            Float64SamplingSpecs.Create(frameRate, maxTime);

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

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer.BeginDrawing2D(
            @"D:\Projects\Study\Web\Babylon.js",
            samplingSpecs
        );
        
        CGa.VisualizerAnimationComposer.SetTitle(
            "Parametric 2D Point-Pair 1"
        );

        CGa.VisualizerKaTeXComposer.AddLaTeXCode(
            new Dictionary<string, string>()
            {
                {"point1", "P_{1}"},
                {"point2", "P_{2}"}
            }
        );

        // Draw the two point curves
        CGa.Visualizer.AnimationComposer
            .SetCurveStyleTube(0.03)
            .DrawCurve(
                Color.Red.SetAlpha(0.2),
                point1Curve,
                samplingSpecs.TimeRange
            ).DrawCurve(
                Color.Blue.SetAlpha(0.2),
                point2Curve,
                samplingSpecs.TimeRange
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
        var latexOffset = LinFloat64Vector2D.VectorSymmetric(0.5);

        CGa.Visualizer.AnimationComposer
            .DrawLaTeX("point1", point1Curve.GetOffsetCurve(latexOffset))
            .DrawLaTeX("point2", point2Curve.GetOffsetCurve(latexOffset));

        // Save the HTML animation file
        CGa.Visualizer.AnimationComposer.SaveHtmlFile();

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

        var samplingSpecs =
            Float64SamplingSpecs.Create(frameRate, maxTime);

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

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer.BeginDrawing2D(
            @"D:\Projects\Study\Web\Babylon.js",
            samplingSpecs
        );
        
        CGa.VisualizerAnimationComposer.SetTitle(
            "Parametric 2D Point-Pair 2"
        );

        CGa.VisualizerKaTeXComposer.AddLaTeXCode(
            new Dictionary<string, string>()
            {
                {"center", "C"}
            }
        );

        // Draw the position curve
        CGa.Visualizer.AnimationComposer
            .SetCurveStyleTube(0.03)
            .DrawCurve(
                Color.Red.SetAlpha(0.2),
                centerCurve,
                samplingSpecs.TimeRange
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
        var latexOffset = LinFloat64Vector2D.VectorSymmetric(0.5);

        CGa.Visualizer.AnimationComposer.DrawLaTeX(
            "center",
            centerCurve.GetOffsetCurve(latexOffset)
        );

        // Save the HTML animation file
        CGa.Visualizer.AnimationComposer.SaveHtmlFile();

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

        var samplingSpecs =
            Float64SamplingSpecs.Create(frameRate, maxTime);

        // This curve defines the parametric position of the first point
        var point1Curve =
            GetPositionCurve1(maxTime);

        // This curve defines the parametric position of the second point
        var point2Curve =
            GetPositionCurve2(maxTime);

        // This constant curve defines the position of the third point
        var point3Curve =
            Float64ConstantPath2D.Finite(2, 2);

        // Define the circle element
        var roundCircle =
            CGa.DefineRoundCircleFromPoints(
                point1Curve,
                point2Curve,
                point3Curve
            );

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer.BeginDrawing2D(
            @"D:\Projects\Study\Web\Babylon.js",
            samplingSpecs
        );
        
        CGa.VisualizerAnimationComposer.SetTitle(
            "Parametric 2D Circle 1"
        );

        CGa.VisualizerKaTeXComposer.AddLaTeXCode(
            new Dictionary<string, string>()
            {
                {"point1", "P_{1}"},
                {"point2", "P_{2}"},
                {"point3", "P_{3}"}
            }
        );

        // Draw the two position curves
        CGa.Visualizer.AnimationComposer
            .SetCurveStyleTube(0.03)
            .DrawCurve(
                Color.Red.SetAlpha(0.2),
                point1Curve,
                samplingSpecs.TimeRange
            ).DrawCurve(
                Color.Green.SetAlpha(0.2),
                point2Curve,
                samplingSpecs.TimeRange
            );

        // Draw the 3 points defining the circle
        CGa.Visualizer.AnimationComposer
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
        var latexOffset = LinFloat64Vector2D.VectorSymmetric(0.5);

        CGa.Visualizer.AnimationComposer
            .DrawLaTeX("point1", point1Curve.GetOffsetCurve(latexOffset))
            .DrawLaTeX("point2", point2Curve.GetOffsetCurve(latexOffset))
            .DrawLaTeX("point3", point3Curve.Point + latexOffset);

        // Save the HTML animation file
        CGa.Visualizer.AnimationComposer.SaveHtmlFile();

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

        var samplingSpecs =
            Float64SamplingSpecs.Create(frameRate, maxTime);

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

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer.BeginDrawing2D(
            @"D:\Projects\Study\Web\Babylon.js",
            samplingSpecs
        );
        
        CGa.VisualizerAnimationComposer.SetTitle(
            "Parametric 2D Circle 2"
        );

        CGa.VisualizerKaTeXComposer.AddLaTeXCode(
            new Dictionary<string, string>()
            {
                {"center", "C"}
            }
        );

        // Draw the center curve
        CGa.Visualizer.AnimationComposer
            .SetCurveStyleTube(0.03)
            .DrawCurve(
                Color.Red.SetAlpha(0.2),
                centerCurve,
                samplingSpecs.TimeRange
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
        var latexOffset = LinFloat64Vector2D.VectorSymmetric(0.5);

        CGa.Visualizer.AnimationComposer.DrawLaTeX(
            "center",
            centerCurve.GetOffsetCurve(latexOffset)
        );

        // Save the HTML animation file
        CGa.Visualizer.AnimationComposer.SaveHtmlFile();

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

        var samplingSpecs =
            Float64SamplingSpecs.Create(frameRate, maxTime);

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
            roundCircle1.Meet(roundCircle2);

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer.BeginDrawing2D(
            @"D:\Projects\Study\Web\Babylon.js",
            samplingSpecs
        );
        
        CGa.VisualizerAnimationComposer.SetTitle(
            "Parametric 2D Circle-Circle Intersection"
        );

        CGa.VisualizerKaTeXComposer.AddLaTeXCode(
            new Dictionary<string, string>()
            {
                {"element1", "A"},
                {"element2", "B"},
                {"intersectionElement", @"A \cap B"}
            }
        );

        // Draw curves for circle centers
        //Ga.Visualizer
        //    .SetCurveStyleTube(0.03)
        //    .DrawCurve(
        //        Color.Red.SetAlpha(0.2), 
        //        positionCurve1, 
        //        samplingSpecs.TimeRange
        //    ).DrawCurve(
        //        Color.Green.SetAlpha(0.2), 
        //        positionCurve2, 
        //        samplingSpecs.TimeRange
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
        CGa.Visualizer.AnimationComposer
            //.SetCurveStyleDashed(5, 3, 16)
            //.SetCurveStyleSolid()
            .SetCurveStyleTube(0.065)
            .DrawLineCurve(
                Color.Yellow,
                positionCurve1,
                positionCurve2
            );

        // Draw LaTeX expressions
        CGa.Visualizer.AnimationComposer
            .DrawLaTeX(
                "element1",
                roundCircle1.GetRoundSurfacePointCurve2D(
                    LinFloat64Vector2D.Symmetric,
                    0.5
                )
            ).DrawLaTeX(
                "element2",
                roundCircle2.GetRoundSurfacePointCurve2D(
                    LinFloat64Vector2D.Symmetric,
                    0.5
                )
            ).DrawLaTeX(
                "intersectionElement",
                intersectionElement.GetRoundSurfacePointCurve2D(
                    LinFloat64Vector2D.E2,
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

        var samplingSpecs =
            Float64SamplingSpecs.Create(frameRate, maxTime);

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
            roundCircle.Meet(flatLine);

        var (p1Curve, p2Curve) =
            intersectionElement.GetRoundPointPairCurves2D();

        var p12Curve =
            intersectionElement.GetRoundCenterCurve2D();

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer.BeginDrawing2D(
            @"D:\Projects\Study\Web\Babylon.js",
            samplingSpecs
        );
        
        CGa.VisualizerAnimationComposer.SetTitle(
            "Parametric 2D Circle-Line Intersection"
        );

        CGa.VisualizerKaTeXComposer.AddLaTeXCode(
            new Dictionary<string, string>()
            {
                {"element1", "A"},
                {"element2", "B"},
                {"intersectionElement", @"A \cap B"}
            }
        );

        // Draw curves for circle centers
        //Ga.Visualizer
        //    .SetCurveStyleTube(0.03)
        //    .DrawCurve(
        //        Color.Red.SetAlpha(0.2), 
        //        positionCurve1, 
        //        samplingSpecs.TimeRange
        //    ).DrawCurve(
        //        Color.Green.SetAlpha(0.2), 
        //        positionCurve2, 
        //        samplingSpecs.TimeRange
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
        CGa.Visualizer.AnimationComposer
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
        CGa.Visualizer.AnimationComposer
            .DrawLaTeX(
                "element1",
                flatLine.GetSurfacePointCurve2D(
                    LinFloat64Vector2D.Symmetric,
                    5,
                    0.5
                )
            ).DrawLaTeX(
                "element2",
                roundCircle.GetRoundSurfacePointCurve2D(
                    LinFloat64Vector2D.Symmetric,
                    0.5
                )
            ).DrawLaTeX(
                "intersectionElement",
                intersectionElement.GetRoundSurfacePointCurve2D(
                    LinFloat64Vector2D.E2,
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

        var samplingSpecs =
            Float64SamplingSpecs.Create(frameRate, maxTime);

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

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer.BeginDrawing2D(
            @"D:\Projects\Study\Web\Babylon.js",
            samplingSpecs
        );
        
        CGa.VisualizerAnimationComposer.SetTitle(
            "Parametric 2D Circle On Circle Reflection"
        );

        CGa.VisualizerKaTeXComposer.AddLaTeXCode(
            new Dictionary<string, string>()
            {
                {"element1", "A"},
                {"element2", "B"},
                {"reflectionElement", "A B A^{-1}"}
            }
        );

        // Draw curves for circle centers
        //Ga.Visualizer
        //    .SetCurveStyleTube(0.03)
        //    .DrawCurve(
        //        Color.Red.SetAlpha(0.2), 
        //        positionCurve1, 
        //        samplingSpecs.TimeRange
        //    ).DrawCurve(
        //        Color.Green.SetAlpha(0.2), 
        //        positionCurve2, 
        //        samplingSpecs.TimeRange
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
        CGa.Visualizer.AnimationComposer
            .DrawLaTeX(
                "element1",
                roundCircle1.GetRoundSurfacePointCurve2D(
                    LinFloat64Vector2D.Symmetric,
                    0.5
                )
            ).DrawLaTeX(
                "element2",
                roundCircle2.GetRoundSurfacePointCurve2D(
                    -LinFloat64Vector2D.Symmetric,
                    0.5
                )
            ).DrawLaTeX(
                "reflectionElement",
                reflectionElement.GetRoundSurfacePointCurve2D(
                    LinFloat64Vector2D.E2,
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

        var samplingSpecs =
            Float64SamplingSpecs.Create(frameRate, maxTime);

        // Encode a parametric real line
        // This curve defines the parametric position and normal of the line
        var positionCurve1 =
            Float64ConstantPath2D.Finite(
                samplingSpecs.TimeRange,
                LinFloat64Vector2D.Create(0, 0),
                LinFloat64Vector2D.Create(0, 1)
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

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer.BeginDrawing2D(
            @"D:\Projects\Study\Web\Babylon.js",
            samplingSpecs
        );
        
        CGa.VisualizerAnimationComposer.SetTitle(
            "Parametric 2D Circle On Line Reflection"
        );

        CGa.VisualizerKaTeXComposer.AddLaTeXCode(
            new Dictionary<string, string>()
            {
                {"element1", "A"},
                {"element2", "B"},
                {"reflectionElement", "A B A^{-1}"}
            }
        );

        // Draw curves for circle centers
        //Ga.Visualizer
        //    .SetCurveStyleTube(0.03)
        //    .DrawCurve(
        //        Color.Red.SetAlpha(0.2), 
        //        positionCurve1, 
        //        samplingSpecs.TimeRange
        //    ).DrawCurve(
        //        Color.Green.SetAlpha(0.2), 
        //        positionCurve2, 
        //        samplingSpecs.TimeRange
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
        CGa.Visualizer.AnimationComposer
            .DrawLaTeX(
                "element1",
                flatLine.GetSurfacePointCurve2D(
                    LinFloat64Vector2D.E2,
                    5,
                    0.5
                )
            ).DrawLaTeX(
                "element2",
                roundCircle.GetRoundSurfacePointCurve2D(
                    LinFloat64Vector2D.Create(1, 1),
                    0.5
                )
            ).DrawLaTeX(
                "reflectionElement",
                reflectionElement.GetRoundSurfacePointCurve2D(
                    LinFloat64Vector2D.Create(-1, 1),
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

        var samplingSpecs =
            Float64SamplingSpecs.Create(frameRate, maxTime);

        // Encode a parametric real line
        // This curve defines the parametric position and normal of the line
        var positionCurve1 =
            Float64ConstantPath2D.Finite(
                samplingSpecs.TimeRange,
                LinFloat64Vector2D.Create(0, 0),
                LinFloat64Vector2D.Create(0, 1)
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

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer.BeginDrawing2D(
            @"D:\Projects\Study\Web\Babylon.js",
            samplingSpecs
        );
        
        CGa.VisualizerAnimationComposer.SetTitle(
            "Parametric 2D Point-Pair On Line Reflection"
        );

        CGa.VisualizerKaTeXComposer.AddLaTeXCode(
            new Dictionary<string, string>()
            {
                {"element1", "A"},
                {"element2", "B"},
                {"projectionElement", @"\left(B\bullet A\right)A^{-1}"},
                {"reflectionElement", "A B A^{-1}"}
            }
        );

        // Draw curves for circle centers
        //Ga.Visualizer
        //    .SetCurveStyleTube(0.03)
        //    .DrawCurve(
        //        Color.Red.SetAlpha(0.2), 
        //        positionCurve1, 
        //        samplingSpecs.TimeRange
        //    ).DrawCurve(
        //        Color.Green.SetAlpha(0.2), 
        //        positionCurve2, 
        //        samplingSpecs.TimeRange
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
        CGa.Visualizer.AnimationComposer
            .DrawLaTeX(
                "element1",
                flatLine.GetSurfacePointCurve2D(
                    LinFloat64Vector2D.E2,
                    5,
                    0.5
                )
            ).DrawLaTeX(
                "element2",
                roundPointPair.GetRoundSurfacePointCurve2D(
                    LinFloat64Vector2D.E2,
                    0.5
                )
            ).DrawLaTeX(
                "projectionElement",
                projectionElement.GetRoundSurfacePointCurve2D(
                    LinFloat64Vector2D.E2,
                    0.5
                )
            ).DrawLaTeX(
                "reflectionElement",
                reflectionElement.GetRoundSurfacePointCurve2D(
                    LinFloat64Vector2D.E2,
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

        var samplingSpecs =
            Float64SamplingSpecs.Create(frameRate, maxTime);

        // Encode two parametric circle blades
        var roundCircle1 =
            CGa.DefineRoundCircle(
                4,
                LinFloat64Vector2D.Create(5, 5)
            );

        var roundCircle2 =
            CGa.DefineRoundCircle(
                -9,
                LinFloat64Vector2D.Create(-5, -5),
                LinFloat64Bivector2D.E21
            );
        
        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer.BeginDrawing2D(
            @"D:\Projects\Study\Web\Babylon.js",
            samplingSpecs
        );
        
        CGa.VisualizerAnimationComposer.SetTitle(
            "Parametric 2D Circle Interpolation"
        );

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
            CGaFloat64ParametricElement.Create(
                CGa,
                samplingSpecs.TimeRange,
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
        CGa.Visualizer.AnimationComposer.SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");
    }
}