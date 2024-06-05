using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars.Harmonic;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves.Spherical;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Interpolation;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Visualizer;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Angles;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Operations;
using SixLabors.ImageSharp;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Samples.Modeling.Geometry.Conformal;

public static class CGa5DVisualizationSamples
{
    public static RGaConformalSpace5D CGa
        => RGaConformalSpace5D.Instance;

    public static IFloat64ParametricScalar GetRadiusCurve1(double maxTime)
    {
        var parameterRange = Float64ScalarRange.Create(0, maxTime);

        var rCurve = CosWaveParametricScalar.Create(parameterRange, 1, 5, 6);

        return rCurve;
    }

    public static IParametricCurve3D GetPositionCurve1(double maxTime)
    {
        var freqHz = 1 / maxTime;
        var freq = 2 * Math.PI * freqHz;

        var parameterRange = Float64ScalarRange.Create(0, maxTime);

        var rCurve = CosWaveParametricScalar.Create(parameterRange, 3, 5, 3);
        var thetaCurve = LinearParametricScalar.Create(2 * freq);
        var phiCurve = LinearParametricScalar.Create(1 * freq);

        var curve = SphericalCurve3D.Create(
            parameterRange,
            rCurve,
            thetaCurve,
            phiCurve
        );

        return curve;
    }

    public static IParametricCurve3D GetPositionCurve2(double maxTime)
    {
        var freqHz = 1 / maxTime;
        var freq = 2 * Math.PI * freqHz;

        var parameterRange = Float64ScalarRange.Create(0, maxTime);

        var rCurve = CosWaveParametricScalar.Create(parameterRange, 3, 5, 1);
        var thetaCurve = LinearParametricScalar.Create(1 * freq);
        var phiCurve = LinearParametricScalar.Create(3 * freq);

        var curve = SphericalCurve3D.Create(
            parameterRange,
            rCurve,
            thetaCurve,
            phiCurve
        );

        return curve;
    }


    /// <summary>
    /// Define and draw static flats
    /// </summary>
    public static void StaticFlatsExample()
    {
        var flatPoint = CGa.EncodeOpnsFlatPoint(
            LinFloat64Vector3D.Create(4, 4, 4)
        );

        var flatLine = CGa.EncodeOpnsFlatLine(
            LinFloat64Vector3D.Create(1, 1, 1),
            LinFloat64Vector3D.Create(1, 1, 1).GetNormal()
        );

        var flatPlane = CGa.EncodeOpnsFlatPlane(
            3,
            LinFloat64Vector3D.Create(1, 1, 1).Negative()
        );

        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .BeginDrawing3D("Static Flats Example");

        CGa.Visualizer
            .SetFlatStyle(
                0.06,
                6,
                true,
                1,
                1,
                0.5
            )
            .DrawOpnsBlade(flatPoint, Color.Yellow)
            .DrawOpnsBlade(flatLine, Color.Green)
            .DrawOpnsBlade(flatPlane, Color.Blue.SetAlpha(0.7));

        CGa.Visualizer.SaveHtmlFile();
    }

    /// <summary>
    /// Define and draw static rounds
    /// </summary>
    public static void StaticRoundsExample()
    {
        var roundPoint =
            CGa.EncodeIpnsRoundPoint(4, 4, 4);

        var roundPointPair =
            CGa.EncodeOpnsRoundPointPair(
                LinFloat64Vector3D.Create(3, 3, 3),
                LinFloat64Vector3D.Create(-4, 4, -4).GetNormal()
            );

        var roundCircle =
            CGa.EncodeIpnsRoundCircle(
                4,
                LinFloat64Vector3D.Create(-2, -2, -2),
                LinFloat64Vector3D.Create(1, 1, 1)
            ).CGaUnDual();

        var roundSphere =
            CGa.EncodeIpnsRoundSphere(
                4,
                LinFloat64Vector3D.Create(-5, -5, -5)
            ).CGaUnDual();

        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .BeginDrawing3D("Static Rounds Example");

        CGa.Visualizer
            .SetRoundStyle(
                0.08,
                true,
                false,
                1,
                1,
                0.5
            )
            .DrawIpnsBlade(roundPoint, Color.Yellow)
            .DrawOpnsBlade(roundPointPair, Color.Green)
            .DrawOpnsBlade(roundCircle, Color.Blue)
            .DrawOpnsBlade(roundSphere, Color.Red.SetAlpha(0.7));

        CGa.Visualizer.SaveHtmlFile();
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

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing3D(@"Parametric 3D Line Example 1", latexDictionary);

        // Draw the two position curves
        CGa.Visualizer
            .SetCurveStyleTube(0.05)
            .DrawCurve(
                Color.Red.SetAlpha(0.2),
                point1Curve,
                animationSpecs.FrameTimeRange
            ).DrawCurve(
                Color.Green.SetAlpha(0.2),
                point2Curve,
                animationSpecs.FrameTimeRange
            );

        // Draw the two points
        CGa.Visualizer
            .SetPointStyle(0.08)
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
                0.065,
                12,
                true,
                1,
                1,
                0.65
            ).DrawFlatLine3D(Color.Bisque, flatLine);

        // Draw LaTeX expressions
        var latexOffset = LinFloat64Vector3D.VectorSymmetric(0.25);

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

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing3D(@"Parametric 3D Line Example 2", latexDictionary);

        // Draw the two position curves
        CGa.Visualizer
            .SetCurveStyleTube(0.05)
            .DrawCurve(
                Color.Red.SetAlpha(0.2),
                positionCurve,
                animationSpecs.FrameTimeRange
            );

        // Draw the two points
        CGa.Visualizer
            .SetPointStyle(0.08)
            .DrawPoint(
                Color.Red,
                positionCurve
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
            ).DrawFlatLine3D(Color.Bisque, flatLine);

        // Draw LaTeX expressions
        var latexOffset = LinFloat64Vector3D.VectorSymmetric(0.25);

        CGa.Visualizer
            .DrawLaTeX("point", positionCurve.GetOffsetCurve(latexOffset));

        // Save the HTML animation file
        CGa.Visualizer.SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");
    }

    /// <summary>
    /// Define a CGA plane using three parametric point curves
    /// </summary>
    public static void ParametricPlaneExample1()
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

        // This constant curve defines the fixed position of the 3rd point
        var point3Curve =
            ConstantParametricCurve3D.Create(1, 2, 1);

        // Define the plane element
        var flatPlane =
            CGa.DefineFlatPlaneFromPoints(
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

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing3D(@"Parametric 3D Plane Example 1", latexDictionary);

        // Draw the two position curves
        CGa.Visualizer
            .SetCurveStyleTube(0.05)
            .DrawCurve(
                Color.Red.SetAlpha(0.2),
                point1Curve,
                animationSpecs.FrameTimeRange
            ).DrawCurve(
                Color.Green.SetAlpha(0.2),
                point2Curve,
                animationSpecs.FrameTimeRange
            );

        // Draw the two points
        CGa.Visualizer
            .SetPointStyle(0.08)
            .DrawPoint(
                Color.Red,
                point1Curve
            ).DrawPoint(
                Color.Green,
                point2Curve
            ).DrawPoint(
                Color.Blue,
                point3Curve.Point
            );

        // Draw the parametric plane
        CGa.Visualizer
            .SetFlatStyle(
                0.065,
                6,
                true,
                1,
                1,
                0.5
            ).DrawFlatPlane3D(Color.Bisque.SetAlpha(0.65), flatPlane);

        // Draw LaTeX expressions
        var latexOffset = LinFloat64Vector3D.VectorSymmetric(0.75);

        CGa.Visualizer
            .DrawLaTeX("point1", point1Curve.GetOffsetCurve(latexOffset))
            .DrawLaTeX("point2", point2Curve.GetOffsetCurve(latexOffset))
            .DrawLaTeX("point3", point3Curve.Point.VectorAdd(latexOffset));

        // Save the HTML animation file
        CGa.Visualizer.SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");
    }

    /// <summary>
    /// Define a CGA plane using one parametric curve, the normal to the plane is
    /// the tangent to the vector
    /// </summary>
    public static void ParametricPlaneExample2()
    {
        const double maxTime = 24;
        const int frameRate = 10;

        Console.WriteLine("Animated Example Started ..");

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);

        // This curve defines the parametric position of the plane
        var positionCurve =
            GetPositionCurve1(maxTime);

        // This curve defines the parametric normal to the plane
        var normalCurve =
            positionCurve.GetTangentCurve();

        // Define the line element
        var flatPlane =
            CGa.DefineFlatPlane(
                positionCurve,
                normalCurve
            );

        // Prepare latex expressions
        var latexDictionary = new Dictionary<string, string>()
        {
            {"point", @"P"}
        };

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing3D(@"Parametric 3D Plane Example 2", latexDictionary);

        // Draw the position curve
        CGa.Visualizer
            .SetCurveStyleTube(0.05)
            .DrawCurve(
                Color.Red.SetAlpha(0.2),
                positionCurve,
                animationSpecs.FrameTimeRange
            );

        // Draw the parametric plane
        CGa.Visualizer
            .SetFlatStyle(
                0.065,
                6,
                true,
                1,
                1,
                0.5
            ).DrawFlatPlane3D(Color.Bisque.SetAlpha(0.65), flatPlane);

        // Draw LaTeX expressions
        var latexOffset = LinFloat64Vector3D.VectorSymmetric(0.5);

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

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing3D(@"Parametric 3D Point-Pair Example 1", latexDictionary);

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
            ).DrawRoundPointPair3D(Color.Bisque, roundPointPair);

        // Draw LaTeX expressions
        var latexOffset = LinFloat64Vector3D.VectorSymmetric(0.5);

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

        // This curve defines the parametric radius of the sphere containing the point-pair
        var radiusCurve =
            GetRadiusCurve1(maxTime);

        // This curve defines the parametric center of the sphere containing the point-pair
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

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing3D(@"Parametric 3D Point-Pair Example 2", latexDictionary);

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
            ).DrawRoundPointPair3D(Color.Bisque, roundPointPair);

        // Draw LaTeX expressions
        var latexOffset = LinFloat64Vector3D.VectorSymmetric(0.5);

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
            ConstantParametricCurve3D.Create(2, 2, -2);

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

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing3D(@"Parametric 3D Circle Example 1", latexDictionary);

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
            ).DrawRoundCircle3D(Color.Bisque, roundCircle);

        // Draw LaTeX expressions
        var latexOffset = LinFloat64Vector3D.VectorSymmetric(0.75);

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

        // This curve defines the parametric radius of the sphere containing the circle
        var radiusCurve =
            GetRadiusCurve1(maxTime);

        // This curve defines the parametric center of the sphere containing the circle
        var centerCurve =
            GetPositionCurve1(maxTime);

        // Define the normal direction of the circle as the tangent vector to the center curve
        var normalCurve =
            centerCurve.GetTangentCurve();

        // Define the circle element
        var roundCircle =
            CGa.DefineRoundCircle(
                radiusCurve,
                centerCurve,
                normalCurve
            );

        // Prepare latex expressions
        var latexDictionary = new Dictionary<string, string>()
        {
            {"center", @"C"}
        };

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing3D(@"Parametric 3D Circle Example 2", latexDictionary);

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
            ).DrawRoundCircle3D(Color.Bisque, roundCircle);

        // Draw LaTeX expressions
        var latexOffset = LinFloat64Vector3D.VectorSymmetric(0.5);

        CGa.Visualizer.DrawLaTeX(
            "center",
            centerCurve.GetOffsetCurve(latexOffset)
        );

        // Save the HTML animation file
        CGa.Visualizer.SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");
    }

    /// <summary>
    /// Define a CGA sphere using two parametric point curves and two fixed points
    /// </summary>
    public static void ParametricSphereExample1()
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
            ConstantParametricCurve3D.Create(2, 2, -2);

        // This constant curve defines the position of the 4th point
        var point4Curve =
            ConstantParametricCurve3D.Create(-2, -2, 1);

        // Define the sphere element
        var roundSphere =
            CGa.DefineRealRoundSphereFromPoints(
                point1Curve,
                point2Curve,
                point3Curve,
                point4Curve
            );

        // Define 4 circles on the sphere by taking each 3 points together
        var roundCircle1 =
            CGa.DefineRoundCircleFromPoints(
                point1Curve,
                point2Curve,
                point3Curve
            );

        var roundCircle2 =
            CGa.DefineRoundCircleFromPoints(
                point1Curve,
                point2Curve,
                point4Curve
            );

        var roundCircle3 =
            CGa.DefineRoundCircleFromPoints(
                point1Curve,
                point3Curve,
                point4Curve
            );

        var roundCircle4 =
            CGa.DefineRoundCircleFromPoints(
                point2Curve,
                point3Curve,
                point4Curve
            );

        // Prepare latex expressions
        var latexDictionary = new Dictionary<string, string>()
        {
            {"point1", @"P_{1}"},
            {"point2", @"P_{2}"},
            {"point3", @"P_{3}"},
            {"point4", @"P_{4}"}
        };

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing3D(@"Parametric 3D Sphere Example 1", latexDictionary);

        //// Draw the two position curves
        //Ga.Visualizer
        //    .SetCurveStyleTube(0.03)
        //    .DrawCurve(
        //        Color.Red.SetAlpha(0.2), 
        //        point1Curve, 
        //        animationSpecs.TimeRange
        //    ).DrawCurve(
        //        Color.Green.SetAlpha(0.2), 
        //        point2Curve, 
        //        animationSpecs.TimeRange
        //    );

        // Draw the 4 points defining the sphere
        CGa.Visualizer
            .SetPointStyle(0.08)
            .DrawPoint(Color.Red, point1Curve)
            .DrawPoint(Color.Green, point2Curve)
            .DrawPoint(Color.Blue, point3Curve.Point)
            .DrawPoint(Color.Yellow, point4Curve.Point);

        // Draw the parametric sphere and circles
        CGa.Visualizer
            .SetRoundStyle(
                0.04,
                false,
                false,
                0,
                0
            ).DrawRoundSphere3D(Color.Bisque.SetAlpha(0.4), roundSphere)
            .DrawRoundCircle3D(Color.Red.SetAlpha(0.6), roundCircle1)
            .DrawRoundCircle3D(Color.Green.SetAlpha(0.6), roundCircle2)
            .DrawRoundCircle3D(Color.Blue.SetAlpha(0.6), roundCircle3)
            .DrawRoundCircle3D(Color.Yellow.SetAlpha(0.6), roundCircle4);

        // Draw LaTeX expressions
        var latexOffset = LinFloat64Vector3D.VectorSymmetric(1);

        CGa.Visualizer
            .DrawLaTeX("point1", point1Curve.GetOffsetCurve(latexOffset))
            .DrawLaTeX("point2", point2Curve.GetOffsetCurve(latexOffset))
            .DrawLaTeX("point3", point3Curve.Point + latexOffset)
            .DrawLaTeX("point4", point4Curve.Point + latexOffset);

        // Save the HTML animation file
        CGa.Visualizer.SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");
    }

    /// <summary>
    /// Define a CGA sphere using a parametric center curve and a radius curve
    /// </summary>
    public static void ParametricSphereExample2()
    {
        const double maxTime = 24;
        const int frameRate = 10;

        Console.WriteLine("Animated Example Started ..");

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);

        // This curve defines the parametric radius of the sphere
        var radiusCurve =
            GetRadiusCurve1(maxTime);

        // This curve defines the parametric center of the sphere
        var centerCurve =
            GetPositionCurve1(maxTime);

        // Define the point-pair element
        var roundSphere =
            CGa.DefineRoundSphere(
                radiusCurve,
                centerCurve
            );

        // Prepare latex expressions
        var latexDictionary = new Dictionary<string, string>()
        {
            {"center", @"C"}
        };

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .SetGrid(14, 1, 0.5)
            .BeginDrawing3D(@"Parametric 3D Sphere Example 2", latexDictionary);

        // Draw the center curve
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
                0.04,
                true
            ).DrawRoundSphere3D(Color.Bisque.SetAlpha(0.4), roundSphere);

        // Draw LaTeX expressions
        var latexOffset = LinFloat64Vector3D.VectorSymmetric(0.25);

        CGa.Visualizer.DrawLaTeX(
            "center",
            centerCurve.GetOffsetCurve(latexOffset)
        );

        // Save the HTML animation file
        CGa.Visualizer.SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");
    }

    /// <summary>
    /// Define two parametric spheres and draw them
    /// and their animated intersection circle (imaginary or real)
    /// </summary>
    public static void ParametricSphereSphereIntersectionExample()
    {
        const double maxTime = 36;
        const int frameRate = 10;

        Console.WriteLine("Animated Example Started ..");

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);

        // Encode a parametric real sphere
        // This curve defines the parametric center of the sphere
        var positionCurve1 =
            GetPositionCurve1(maxTime);

        // Define the sphere based on the parametric center with fixed radius
        var roundSphere1 =
            CGa.DefineRealRoundSphere(
                2,
                positionCurve1
            );

        // Encode a parametric real sphere
        // This curve defines the parametric center of the sphere
        var positionCurve2 =
            GetPositionCurve2(maxTime);

        // Define the sphere based on the parametric center with fixed radius
        var roundSphere2 =
            CGa.DefineRealRoundSphere(
                1.5,
                positionCurve2
            );

        // Define the intersection circle of the two spheres as a parametric element
        var intersectionElement =
            roundSphere1.Intersect(roundSphere2);

        // Prepare latex expressions
        var latexDictionary = new Dictionary<string, string>()
        {
            {"element1", @"A"},
            {"element2", @"B"},
            {"intersectionElement", @"A \cap B"},
        };

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing3D(@"Parametric 3D Sphere-Sphere Intersection Example", latexDictionary);

        // Draw curves for sphere centers
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

        // Draw the two spheres and their intersection circle
        // The sphere containing the circle is also shown
        CGa.Visualizer
            .SetRoundStyle(
                0.065,
                true,
                false,
                1,
                1,
                0.4
            )
            .DrawRoundSphere3D(Color.Red.SetAlpha(0.4), roundSphere1)
            .DrawRoundSphere3D(Color.Green.SetAlpha(0.4), roundSphere2)
            .DrawRoundCircle3D(Color.Yellow, intersectionElement);

        // Draw the line segment between the two sphere centers
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
                positionCurve1.GetOffsetCurve(
                    LinFloat64Vector3D.VectorSymmetric(2.5)
                )
            ).DrawLaTeX(
                "element2",
                positionCurve2.GetOffsetCurve(
                    LinFloat64Vector3D.VectorSymmetric(2)
                )
            ).DrawLaTeX(
                "intersectionElement",
                intersectionElement.GetSurfacePointCurve3D(
                    LinFloat64Vector3D.E2,
                    0.5,
                    0
                )
            ).SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");
    }

    /// <summary>
    /// Define a parametric sphere and plane, and draw them
    /// and their animated intersection circle (imaginary or real)
    /// </summary>
    public static void ParametricSpherePlaneIntersectionExample()
    {
        const double maxTime = 36;
        const int frameRate = 10;

        Console.WriteLine("Animated Example Started ..");

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);

        // Encode a parametric real sphere
        // This curve defines the parametric center of the sphere
        var positionCurve1 =
            GetPositionCurve1(maxTime);

        // Define the sphere based on the parametric center with fixed radius
        var roundSphere =
            CGa.DefineRealRoundSphere(
                2,
                positionCurve1
            );

        // Encode a parametric plane
        // This curve defines the parametric position of the plane
        var positionCurve2 =
            GetPositionCurve2(maxTime);

        // Define the plane based on the parametric position with normal equal to
        // the curve tangent
        var flatPlane =
            CGa.DefineFlatPlane(
                positionCurve2,
                positionCurve2.GetTangentCurve()
            );

        // Define the intersection circle of the sphere and plane as a parametric element
        var intersectionElement =
            roundSphere.Intersect(flatPlane);

        // Prepare latex expressions
        var latexDictionary = new Dictionary<string, string>()
        {
            {"element1", @"A"},
            {"element2", @"B"},
            {"intersectionElement", @"A \cap B"},
        };

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing3D(@"Parametric 3D Sphere-Plane Intersection Example", latexDictionary);

        // Draw curves for sphere centers
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

        // Draw the sphere and plane and their intersection circle
        // The sphere containing the circle is also shown
        CGa.Visualizer
            .SetFlatStyle(
                0.065,
                10,
                true,
                1,
                1,
                0.4
            )
            .SetRoundStyle(
                0.065,
                true,
                false,
                0.5,
                0.5,
                0.4
            )
            .DrawRoundSphere3D(Color.Red.SetAlpha(0.4), roundSphere)
            .DrawFlatPlane3D(Color.Green.SetAlpha(0.4), flatPlane)
            .DrawRoundCircle3D(Color.Yellow, intersectionElement);

        // Draw the line segment between the sphere center and circle center
        CGa.Visualizer
            .SetCurveStyleDashed(5, 3, 16)
            //.SetCurveStyleSolid()
            //.SetCurveStyleTube(0.065)
            .DrawLineCurve(
                Color.Yellow,
                roundSphere.GetRoundCenterCurve3D(),
                intersectionElement.GetRoundCenterCurve3D()
            );

        // Draw LaTeX expressions
        CGa.Visualizer
            .DrawLaTeX(
                "element1",
                positionCurve1.GetOffsetCurve(
                    LinFloat64Vector3D.VectorSymmetric(2.5)
                )
            ).DrawLaTeX(
                "element2",
                positionCurve2.GetOffsetCurve(
                    LinFloat64Vector3D.VectorSymmetric(2)
                )
            ).DrawLaTeX(
                "intersectionElement",
                intersectionElement.GetSurfacePointCurve3D(
                    LinFloat64Vector3D.E2,
                    0.5,
                    0
                )
            ).SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");
    }

    /// <summary>
    /// Define a parametric sphere and line, and draw them
    /// and their animated intersection point-pair (imaginary or real)
    /// </summary>
    public static void ParametricSphereLineIntersectionExample()
    {
        const double maxTime = 36;
        const int frameRate = 30;

        Console.WriteLine("Animated Example Started ..");

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);

        // Encode a parametric real sphere
        // This curve defines the parametric center of the sphere
        var positionCurve1 =
            GetPositionCurve1(maxTime);

        // Define the sphere based on the parametric center with fixed radius
        var roundSphere =
            CGa.DefineRealRoundSphere(
                4,
                positionCurve1
            );

        // Encode a parametric line
        // This curve defines the parametric position of the line
        var positionCurve2 =
            GetPositionCurve2(maxTime);

        // Define the plane based on the parametric position with normal equal to
        // the curve tangent
        var flatLine =
            CGa.DefineFlatLine(
                positionCurve2,
                positionCurve2.GetTangentCurve()
            );

        // Define the intersection point-pair of the sphere and plane as a parametric element
        var intersectionElement =
            roundSphere.Intersect(flatLine);

        var (p1Curve, p2Curve) =
            intersectionElement.GetRoundPointPairCurves3D();

        var p12Curve =
            intersectionElement.GetRoundCenterCurve3D();

        // Define a circle with center same as sphere and passing
        // through the point pair (TODO: can we do this using CGA directly?)
        var roundCircle =
            CGa.DefineRealRoundCircle(
                positionCurve1.GetDistanceCurve(p1Curve),
                positionCurve1,
                positionCurve1.GetPlaneNormalCurve(p1Curve, p2Curve)
            );

        // Prepare latex expressions
        var latexDictionary = new Dictionary<string, string>()
        {
            {"element1", @"A"},
            {"element2", @"B"},
            {"intersectionElement", @"A \cap B"},
        };

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing3D(@"Parametric 3D Sphere-Line Intersection Example", latexDictionary);

        // Draw curves for sphere centers
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

        // Draw the sphere and line and their intersection point-pair
        // The sphere containing the circle is also shown
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
            .DrawRoundSphere3D(Color.Red.SetAlpha(0.4), roundSphere)
            .DrawFlatLine3D(Color.Green.SetAlpha(0.8), flatLine)
            .DrawRoundPointPair3D(Color.Yellow, intersectionElement)
            .SetRoundStyle(
                0.05,
                true,
                false,
                1,
                1,
                0.5
            )
            .DrawRoundCircle3D(Color.Blue, roundCircle);

        // Draw the line segments between the sphere center and point-pair
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
                roundSphere.GetRoundSurfacePointCurve3D(
                    LinFloat64Vector3D.E2,
                    0.5
                )
            ).DrawLaTeX(
                "element2",
                flatLine.GetSurfacePointCurve3D(
                    LinFloat64Vector3D.E2,
                    5,
                    1
                )
            ).DrawLaTeX(
                "intersectionElement",
                intersectionElement.GetSurfacePointCurve3D(
                    LinFloat64Vector3D.E2,
                    0.5,
                    0
                )
            ).SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");
    }

    /// <summary>
    /// Define a parametric sphere and a circle, and draw them
    /// and their animated intersection point-pair (imaginary or real)
    /// </summary>
    public static void ParametricSphereCircleIntersectionExample()
    {
        const double maxTime = 36;
        const int frameRate = 30;

        Console.WriteLine("Animated Example Started ..");

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);

        // Encode a parametric real sphere
        // This curve defines the parametric center of the sphere
        var positionCurve1 =
            GetPositionCurve1(maxTime);

        // Define the sphere based on the parametric center with fixed radius
        var roundSphere =
            CGa.DefineRealRoundSphere(
                3,
                positionCurve1
            );

        // Encode a parametric line
        // This curve defines the parametric position of the circle
        var positionCurve2 =
            GetPositionCurve2(maxTime);

        // Define the plane based on the parametric position with normal equal to
        // the curve tangent
        var roundCircle =
            CGa.DefineRealRoundCircle(
                2,
                positionCurve2,
                positionCurve2.GetTangentCurve()
            );

        // Define the intersection point-pair of the sphere and circle as a parametric element
        var intersectionElement =
            roundSphere.Intersect(roundCircle);

        var (p1Curve, p2Curve) =
            intersectionElement.GetRoundPointPairCurves3D();

        var p12Curve =
            intersectionElement.GetRoundCenterCurve3D();

        // Define a circle with center same as sphere and passing
        // through the point pair (TODO: can we do this using CGA directly?)
        var roundCircle2 =
            CGa.DefineRealRoundCircle(
                positionCurve1.GetDistanceCurve(p1Curve),
                positionCurve1,
                positionCurve1.GetPlaneNormalCurve(p1Curve, p2Curve)
            );

        // Prepare latex expressions
        var latexDictionary = new Dictionary<string, string>()
        {
            {"element1", @"A"},
            {"element2", @"B"},
            {"intersectionElement", @"A \cap B"},
        };

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing3D(@"Parametric 3D Sphere-Circle Intersection Example", latexDictionary);

        // Draw curves for sphere centers
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

        // Draw the sphere and line and their intersection point-pair
        // The sphere containing the circle is also shown
        CGa.Visualizer
            .SetRoundStyle(
                0.05,
                true,
                false,
                0.5,
                0.5,
                0.4
            )
            .DrawRoundSphere3D(Color.Red.SetAlpha(0.4), roundSphere)
            .DrawRoundCircle3D(Color.Green.SetAlpha(0.8), roundCircle)
            .DrawRoundPointPair3D(Color.Yellow, intersectionElement)
            //.SetRoundStyle(
            //    0.05, 
            //    true, 
            //    false, 
            //    1, 
            //    1,
            //    0.5
            //)
            .DrawRoundCircle3D(Color.Blue, roundCircle2);

        // Draw the line segments between the sphere center and point-pair
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
                roundSphere.GetRoundSurfacePointCurve3D(
                    LinFloat64Vector3D.Symmetric,
                    0.5
                )
            ).DrawLaTeX(
                "element2",
                roundCircle.GetRoundSurfacePointCurve3D(
                    LinFloat64Vector3D.E2,
                    0.5
                )
            ).DrawLaTeX(
                "intersectionElement",
                intersectionElement.GetRoundSurfacePointCurve3D(
                    LinFloat64Vector3D.E2,
                    0.5
                )
            ).SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");
    }

    /// <summary>
    /// Define a parametric sphere and a circle, and draw them
    /// and the circle reflection and projection on the sphere
    /// </summary>
    public static void ParametricCircleOnSphereReflectionExample()
    {
        const double maxTime = 36;
        const int frameRate = 30;

        Console.WriteLine("Animated Example Started ..");

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);

        // Encode a parametric real sphere
        // This curve defines the parametric center of the sphere
        var positionCurve1 =
            GetPositionCurve1(maxTime);

        // Define the sphere based on the parametric center with fixed radius
        var roundSphere =
            CGa.DefineRealRoundSphere(
                3,
                positionCurve1
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
                positionCurve2,
                positionCurve2.GetTangentCurve()
            );

        // Define the projection of the circle on the sphere as a parametric element
        var projectionElement =
            roundCircle.ProjectOn(roundSphere);

        // Define the reflection of the circle on the sphere as a parametric element
        var reflectionElement =
            roundCircle.ReflectOn(roundSphere);

        // Prepare latex expressions
        var latexDictionary = new Dictionary<string, string>()
        {
            {"element1", @"A"},
            {"element2", @"B"},
            {"projectionElement", @"\left(B\bullet A\right)A^{-1}"},
            {"reflectionElement", @"A B A^{-1}"},
        };

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing3D(@"Parametric 3D Circle On Sphere Reflection Example", latexDictionary);

        // Draw curves for sphere centers
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

        // Draw the two spheres and their intersection circle
        // The sphere containing the circle is also shown
        CGa.Visualizer
            .SetRoundStyle(
                0.04,
                true,
                false,
                1,
                1,
                0.6
            )
            .DrawRoundSphere3D(Color.Red.SetAlpha(0.4), roundSphere)
            .DrawRoundCircle3D(Color.Green.SetAlpha(0.4), roundCircle)
            .DrawRoundCircle3D(Color.Blue, projectionElement)
            .DrawRoundCircle3D(Color.Yellow, reflectionElement);

        // Draw LaTeX expressions
        CGa.Visualizer
            .DrawLaTeX(
                "element1",
                roundSphere.GetRoundSurfacePointCurve3D(
                    LinFloat64Vector3D.SymmetricPpn,
                    0.75
                )
            ).DrawLaTeX(
                "element2",
                roundCircle.GetRoundSurfacePointCurve3D(
                    LinFloat64Vector3D.Symmetric,
                    0.75
                )
            ).DrawLaTeX(
                "projectionElement",
                projectionElement.GetRoundSurfacePointCurve3D(
                    LinFloat64Vector3D.E2,
                    0.75
                )
            ).DrawLaTeX(
                "reflectionElement",
                reflectionElement.GetRoundSurfacePointCurve3D(
                    LinFloat64Vector3D.E2,
                    0.75
                )
            ).SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");

    }

    /// <summary>
    /// Define a parametric plane and a circle, and draw them
    /// and the circle reflection and projection on the plane
    /// </summary>
    public static void ParametricCircleOnPlaneReflectionExample()
    {
        const double maxTime = 36;
        const int frameRate = 10;

        Console.WriteLine("Animated Example Started ..");

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);

        // Encode a parametric real plane
        // This curve defines the parametric position and normal of the plane
        var positionCurve1 =
            ConstantParametricCurve3D.Create(
                animationSpecs.FrameTimeRange,
                LinFloat64Vector3D.Create(0, 0, 2),
                LinFloat64Vector3D.Create(0, 1, 0)
            );
        //GetPositionCurve1(maxTime);

        // Define the plane based on the parametric position
        var flatPlane =
            CGa.DefineFlatPlane(
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
                positionCurve2,
                positionCurve2.GetTangentCurve()
            );

        // Define the projection of the circle on the plane as a parametric element
        var projectionElement =
            roundCircle.ProjectOn(flatPlane);

        // Define the reflection of the circle on the plane as a parametric element
        var reflectionElement =
            roundCircle.ReflectOn(flatPlane);

        // Prepare latex expressions
        var latexDictionary = new Dictionary<string, string>()
        {
            {"element1", @"A"},
            {"element2", @"B"},
            {"projectionElement", @"\left(B\bullet A\right)A^{-1}"},
            {"reflectionElement", @"A B A^{-1}"},
        };

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing3D(@"Parametric 3D Circle On Plane Reflection Example", latexDictionary);

        // Draw curves for sphere centers
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

        // Draw the two spheres and their intersection circle
        // The sphere containing the circle is also shown
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
            .DrawFlatPlane3D(Color.Red.SetAlpha(0.4), flatPlane)
            .DrawRoundCircle3D(Color.Green.SetAlpha(0.4), roundCircle)
            .DrawRoundCircle3D(Color.Blue, projectionElement)
            .DrawRoundCircle3D(Color.Yellow, reflectionElement);

        // Draw LaTeX expressions
        CGa.Visualizer
            .DrawLaTeX(
                "element1",
                positionCurve1.GetOffsetCurve(
                    flatPlane.GetSurfacePointCurve3D(
                        LinFloat64Vector3D.E2,
                        6,
                        0.5
                    )
                )
            ).DrawLaTeX(
                "element2",
                positionCurve2.GetOffsetCurve(
                    LinFloat64Vector3D.VectorSymmetric(2.5)
                )
            ).DrawLaTeX(
                "projectionElement",
                projectionElement.GetSurfacePointCurve3D(
                    LinFloat64Vector3D.E2,
                    0.5,
                    0
                )
            ).DrawLaTeX(
                "reflectionElement",
                reflectionElement.GetSurfacePointCurve3D(
                    LinFloat64Vector3D.E2,
                    0.5,
                    0
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

        // Encode two parametric circles
        var roundCircle1 =
            CGa.DefineRoundCircle(
                4,
                LinFloat64Vector3D.Create(5, 5, 0),
                LinFloat64Vector3D.Create(2, 1, 1)
            );

        var roundCircle2 =
            CGa.DefineRoundCircle(
                -9,
                LinFloat64Vector3D.Create(-5, -5, 2),
                LinFloat64Vector3D.Create(0, 0, 1)
            );

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing3D(@"Parametric 3D Circle Interpolation Example");

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
                        .LerpCircle3D(
                            roundCircle1,
                            roundCircle2
                        )
            );

        // Draw the animated parametric circle
        CGa.Visualizer.DrawRoundCircle3D(
            Color.Bisque,
            lerpCircle
        );

        // Draw LaTeX expressions
        CGa.Visualizer.SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");
    }

    public static void ParametricCircleRotationExample1()
    {
        const double maxTime = 9;
        const int frameRate = 30;

        Console.WriteLine("Animated Example Started ..");

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);

        // Define a parametric angle that cycles from 0 to 2 Pi
        var rotationAngle =
            ComputedParametricPolarAngle.CreateCosWaveCycles(maxTime, 1);
        //ComputedParametricAngle.CreateLinearCycles(maxTime, 1);

        // Define the rotation axis flat line element
        var rotationAxisPoint = LinFloat64Vector3D.Create(0, 1, 0);
        var rotationAxisVector = LinFloat64Vector3D.Create(1, 2, 3);

        var rotationAxis =
            CGa.DefineFlatLine(
                rotationAxisPoint,
                rotationAxisVector
            );

        // Create a static line to be rotated
        var flatLine1 =
            CGa.DefineFlatLine(
                LinFloat64Vector3D.Create(-1, 3, -3),
                LinFloat64Vector3D.E3
            );

        // Create a static circle to be rotated
        var roundCircle1 =
            CGa.DefineRealRoundCircle(
                2,
                LinFloat64Vector3D.Create(-1, 3, -3),
                LinFloat64Bivector3D.E12
            );

        // Rotate the line and circle using the parametric angle and static axis
        var flatLine2 =
            flatLine1.RotateUsing(
                rotationAngle,
                rotationAxisPoint,
                rotationAxisVector
            );

        var roundCircle2 =
            roundCircle1.RotateUsing(
                rotationAngle,
                rotationAxisPoint,
                rotationAxisVector
            );

        // Prepare latex expressions
        var latexDictionary = new Dictionary<string, string>()
        {
            {"axis", @"a"},
            {"line1", @"l"},
            {"line2", @"R l R^{\sim}"},
            {"circle1", @"C"},
            {"circle2", @"R C R^{\sim}"},
            {"angle", @"\alpha"}
        };

        // Initialize visualizer for 3D animated drawing
        CGa.Visualizer
            .SetWorkingFolder(@"D:\Projects\Study\Web\Babylon.js")
            .SetAnimationSpecs(animationSpecs)
            .BeginDrawing3D(@"Parametric 3D Circle Rotation Example 1", latexDictionary);

        // Draw the axis
        CGa.Visualizer
            .SetFlatStyle(
                0.065,
                7,
                false,
                0,
                0,
                0
            ).DrawElement(Color.Bisque, rotationAxis);

        // Draw the lines
        CGa.Visualizer
            .SetFlatStyle(
                0.065,
                7,
                false,
                0,
                0,
                0.5
            ).DrawFlat(Color.Red, flatLine1)
            .DrawFlatLine3D(Color.Green, flatLine2);

        // Draw the circles
        CGa.Visualizer
            .SetRoundStyle(
                0.065,
                true,
                false,
                1,
                1,
                0.5
            ).DrawRound(Color.Red, roundCircle1)
            .DrawRoundCircle3D(Color.Green, roundCircle2);

        // Draw the circular path of the rotated circle center
        var center =
            roundCircle1.ProjectPositionOnFlat3D(rotationAxis);

        var radius =
            center.GetDistanceToPoint(roundCircle1.CenterToVector3D());

        var direction1 =
            center.GetUnitDirectionTo(roundCircle1.CenterToVector3D());

        var direction2 =
            rotationAxis.DirectionToVector3D().VectorUnitCross(direction1);

        var alphaPosition =
            rotationAngle
                .MapAngles(a => a.HalfPolarAngle())
                .CreatePolarCurve(1, center, direction1, direction2);

        //ComputedParametricCurve3D.Create(
        //    rotationAngle.ParameterRange,
        //    t =>
        //        {
        //            var a = 
        //                rotationAngle.GetAngle(t) / 2;

        //            return center + 1 * (a.Cos() * direction1 + a.Sin() * direction2);
        //        }
        //);

        CGa.Visualizer
            .SetCurveStyleTube(0.03)
            .DrawCircleCurve(
                Color.Green,
                center,
                LinFloat64Vector3D.Create(1, 2, 3),
                radius
            ).DrawPoint(Color.Green, center)
            .SetCurveStyleSolid()
            .DrawLineCurve(
                Color.Red,
                center,
                roundCircle1.CenterToVector3D()
            )
            .DrawLineCurve(
                Color.Red,
                center,
                roundCircle2.GetRoundCenterCurve3D()
            )
            .SetSurfaceStyleThin()
            .DrawCircleSector(
                Color.Green,
                center,
                direction1,
                direction2,
                0.7,
                rotationAngle
            );

        // Draw LaTeX expressions
        CGa.Visualizer
            .DrawLaTeX(
                "line",
                rotationAxis.SurfacePointToVector3D(
                    LinFloat64Vector3D.E2,
                    5,
                    0.5
                )
            ).DrawLaTeX(
                "line1",
                flatLine1.SurfacePointToVector3D(
                    LinFloat64Vector3D.E2,
                    6,
                    0.5
                )
            ).DrawLaTeX(
                "line2",
                flatLine2.GetSurfacePointCurve3D(
                    LinFloat64Vector3D.E2,
                    6,
                    0.5
                )
            ).DrawLaTeX(
                "circle1",
                roundCircle1.SurfacePointToVector3D(
                    LinFloat64Vector3D.E2,
                    0,
                    0.5
                )
            ).DrawLaTeX(
                "circle2",
                roundCircle2.GetSurfacePointCurve3D(
                    LinFloat64Vector3D.E2,
                    0,
                    0.5
                )
            ).DrawLaTeX(
                "angle",
                alphaPosition
            );

        // Save the HTML animation file
        CGa.Visualizer.SaveHtmlFile();

        Console.WriteLine("Animated Example Finished.");
    }
}