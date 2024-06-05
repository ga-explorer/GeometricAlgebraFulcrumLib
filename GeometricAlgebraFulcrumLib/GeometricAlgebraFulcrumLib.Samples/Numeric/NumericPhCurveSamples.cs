using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Polynomials.PhCurves;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Samples.Numeric;

public static class NumericPhCurveSamples
{
    /// <summary>
    /// Canonical 2D PH curve of degree 5
    /// </summary>
    public static void Example1()
    {
        var point1 = LinFloat64Vector2D.Create((Float64Scalar)10, -1);
        var tangent1 = LinFloat64Vector2D.Create((Float64Scalar)1.2, (Float64Scalar)0.9);

        Console.WriteLine(@$"c'(1) = {tangent1}");
        Console.WriteLine(@$"c(1) = {point1}");
        Console.WriteLine();
            
        var phCurve = PhCurve2DDegree5Canonical.Create(
            point1,
            tangent1
        );

        var length = phCurve.GetLength();

        var cd0 = phCurve.GetHodographPoint(0);
        var cd1 = phCurve.GetHodographPoint(1);
        var cd5 = phCurve.GetHodographPoint(0.5d);

        var c0 = phCurve.GetCurvePoint(0);
        var c1 = phCurve.GetCurvePoint(1);
        var c5 = phCurve.GetCurvePoint(0.5d);

        Console.WriteLine(@$"c'(0) = {cd0}");
        Console.WriteLine(@$"c'(1) = {cd1}");
        Console.WriteLine(@$"c'(0.5) = {cd5}");
        Console.WriteLine();

        Console.WriteLine(@$"c(0) = {c0}");
        Console.WriteLine(@$"c(1) = {c1}");
        Console.WriteLine(@$"c(0.5) = {c5}");
        Console.WriteLine();

        var latexComposer = LaTeXComposerFloat64.DefaultComposer;

        Console.WriteLine(latexComposer.GetScalarText(length));
        Console.WriteLine();
    }

    /// <summary>
    /// Canonical 3D PH curve of degree 5
    /// </summary>
    public static void Example2()
    {
        var point1 = LinFloat64Vector3D.Create(10, -1, 3);
        var tangent1 = LinFloat64Vector3D.Create(1.2, 0.9, -0.5);

        Console.WriteLine(@$"c'(1) = {tangent1}");
        Console.WriteLine(@$"c(1) = {point1}");
        Console.WriteLine();

        var thetaRange = 
            (-89d).GetLinearRange(89d, 7)
            .Select(LinFloat64PolarAngle.CreateFromDegrees)
            .ToArray();

        var lengthArray = new double[thetaRange.Length, thetaRange.Length];

        var i = 0;
        foreach (var theta1 in thetaRange)
        {
            var j = 0;
            foreach (var theta2 in thetaRange)
            {
                var phCurve = PhCurve3DDegree5Canonical.Create(
                    point1,
                    tangent1,
                    theta1,
                    theta2
                );

                var length = phCurve.GetLength();

                lengthArray[i, j] = length;

                j++;

                var cd0 = phCurve.GetHodographPoint(0);
                var cd1 = phCurve.GetHodographPoint(1);
                var cd5 = phCurve.GetHodographPoint(0.5d);

                var c0 = phCurve.GetCurvePoint(0);
                var c1 = phCurve.GetCurvePoint(1);
                var c5 = phCurve.GetCurvePoint(0.5d);

                Console.WriteLine(@$"Theta1 = {theta1}");
                Console.WriteLine(@$"Theta2 = {theta2}");
                Console.WriteLine();

                Console.WriteLine(@$"c'(0) = {cd0}");
                Console.WriteLine(@$"c'(1) = {cd1}");
                Console.WriteLine(@$"c'(0.5) = {cd5}");
                Console.WriteLine();

                Console.WriteLine(@$"c(0) = {c0}");
                Console.WriteLine(@$"c(1) = {c1}");
                Console.WriteLine(@$"c(0.5) = {c5}");
                Console.WriteLine();
            }

            i++;
        }

        var latexComposer = LaTeXComposerFloat64.DefaultComposer;

        Console.WriteLine(latexComposer.GetArrayText(lengthArray));
        Console.WriteLine();
    }
        
    /// <summary>
    /// General 2D PH curve of degree 5
    /// </summary>
    public static void Example3()
    {
        var point0 = LinFloat64Vector2D.Create((Float64Scalar)1, 1);
        var tangent0 = LinFloat64Vector2D.Create((Float64Scalar)(-1.2), (Float64Scalar)(-0.9));

        var point1 = LinFloat64Vector2D.Create((Float64Scalar)10, -1);
        var tangent1 = LinFloat64Vector2D.Create((Float64Scalar)1.2, (Float64Scalar)0.9);

        Console.WriteLine(@$"c'(0) = {tangent0}");
        Console.WriteLine(@$"c'(1) = {tangent1}");
        Console.WriteLine();

        Console.WriteLine(@$"c(0) = {point0}");
        Console.WriteLine(@$"c(1) = {point1}");
        Console.WriteLine();

        {
            var phCurve00 = PhCurve2DDegree5.Create(
                point0,
                tangent0,
                point1,
                tangent1
            );

            var cd0 = phCurve00.GetHodographPoint(0);
            var cd1 = phCurve00.GetHodographPoint(1);
            var cd5 = phCurve00.GetHodographPoint(0.5d);

            var c0 = phCurve00.GetCurvePoint(0);
            var c1 = phCurve00.GetCurvePoint(1);
            var c5 = phCurve00.GetCurvePoint(0.5d);
                
            Console.WriteLine(@$"c'(0) = {cd0}");
            Console.WriteLine(@$"c'(1) = {cd1}");
            Console.WriteLine(@$"c'(0.5) = {cd5}");
            Console.WriteLine();

            Console.WriteLine(@$"c(0) = {c0}");
            Console.WriteLine(@$"c(1) = {c1}");
            Console.WriteLine(@$"c(0.5) = {c5}");
            Console.WriteLine();
        }

        {
            var phCurve = PhCurve2DDegree5.Create(
                point0,
                tangent0,
                point1,
                tangent1
            );

            var length = phCurve.GetLength();

            var cd0 = phCurve.GetHodographPoint(0);
            var cd1 = phCurve.GetHodographPoint(1);
            var cd5 = phCurve.GetHodographPoint(0.5d);

            var c0 = phCurve.GetCurvePoint(0);
            var c1 = phCurve.GetCurvePoint(1);
            var c5 = phCurve.GetCurvePoint(0.5d);

            Console.WriteLine(@$"c'(0) = {cd0}");
            Console.WriteLine(@$"c'(1) = {cd1}");
            Console.WriteLine(@$"c'(0.5) = {cd5}");
            Console.WriteLine();

            Console.WriteLine(@$"c(0) = {c0}");
            Console.WriteLine(@$"c(1) = {c1}");
            Console.WriteLine(@$"c(0.5) = {c5}");
            Console.WriteLine();

            var latexComposer = LaTeXComposerFloat64.DefaultComposer;

            Console.WriteLine(latexComposer.GetScalarText(length));
            Console.WriteLine();
        }
    }

    /// <summary>
    /// General 3D PH curve of degree 5
    /// </summary>
    public static void Example4()
    {
        var point0 = LinFloat64Vector3D.Create(1, 1, 0);
        var tangent0 = LinFloat64Vector3D.Create(-1.2, -0.9, 0.5);

        var point1 = LinFloat64Vector3D.Create(10, -1, 3);
        var tangent1 = LinFloat64Vector3D.Create(1.2, 0.9, -0.5);

        Console.WriteLine(@$"c'(0) = {tangent0}");
        Console.WriteLine(@$"c'(1) = {tangent1}");
        Console.WriteLine();

        Console.WriteLine(@$"c(0) = {point0}");
        Console.WriteLine(@$"c(1) = {point1}");
        Console.WriteLine();

        {
            var phCurve00 = PhCurve3DDegree5.Create(
                point0,
                tangent0,
                point1,
                tangent1
            );

            var cd0 = phCurve00.GetHodographPoint(0);
            var cd1 = phCurve00.GetHodographPoint(1);
            var cd5 = phCurve00.GetHodographPoint(0.5d);

            var c0 = phCurve00.GetCurvePoint(0);
            var c1 = phCurve00.GetCurvePoint(1);
            var c5 = phCurve00.GetCurvePoint(0.5d);

            Console.WriteLine(@$"Theta1 = {phCurve00.Theta1}");
            Console.WriteLine(@$"Theta2 = {phCurve00.Theta2}");
            Console.WriteLine();

            Console.WriteLine(@$"c'(0) = {cd0}");
            Console.WriteLine(@$"c'(1) = {cd1}");
            Console.WriteLine(@$"c'(0.5) = {cd5}");
            Console.WriteLine();

            Console.WriteLine(@$"c(0) = {c0}");
            Console.WriteLine(@$"c(1) = {c1}");
            Console.WriteLine(@$"c(0.5) = {c5}");
            Console.WriteLine();
        }

        var thetaRange = 
            (-89d).GetLinearRange(89d, 7)
            .Select(LinFloat64PolarAngle.CreateFromDegrees)
            .ToArray();

        var lengthArray = new double[thetaRange.Length, thetaRange.Length];

        var i = 0;
        foreach (var theta1 in thetaRange)
        {
            var j = 0;
            foreach (var theta2 in thetaRange)
            {
                var phCurve = PhCurve3DDegree5.Create(
                    point0,
                    tangent0,
                    point1,
                    tangent1,
                    theta1,
                    theta2
                );

                var length = phCurve.GetLength();

                lengthArray[i, j] = length;

                j++;

                var cd0 = phCurve.GetHodographPoint(0);
                var cd1 = phCurve.GetHodographPoint(1);
                var cd5 = phCurve.GetHodographPoint(0.5d);

                var c0 = phCurve.GetCurvePoint(0);
                var c1 = phCurve.GetCurvePoint(1);
                var c5 = phCurve.GetCurvePoint(0.5d);

                Console.WriteLine(@$"Theta1 = {theta1}");
                Console.WriteLine(@$"Theta2 = {theta2}");
                Console.WriteLine();

                Console.WriteLine(@$"c'(0) = {cd0}");
                Console.WriteLine(@$"c'(1) = {cd1}");
                Console.WriteLine(@$"c'(0.5) = {cd5}");
                Console.WriteLine();

                Console.WriteLine(@$"c(0) = {c0}");
                Console.WriteLine(@$"c(1) = {c1}");
                Console.WriteLine(@$"c(0.5) = {c5}");
                Console.WriteLine();
            }

            i++;
        }

        var latexComposer = LaTeXComposerFloat64.DefaultComposer;

        Console.WriteLine(latexComposer.GetArrayText(lengthArray));
        Console.WriteLine();
    }
}