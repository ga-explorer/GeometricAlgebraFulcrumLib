using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Polynomials;
using GeometricAlgebraFulcrumLib.Modeling.Graphics;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Signals.Composers;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Algebra.Polynomials;

public static class ChebyshevSamples
{
    public static void Example1()
    {
        const int degree = 1000;
        var polynomial = DfChebyshevBasis.Create(degree);

        for (var i = 0; i < 10; i++)
        {
            var x = new Random().NextDouble();

            var y1 = polynomial.GetValue(x);
            var y2 = DfChebyshevBasis.GetChebyshevValue(degree, x);

            Console.WriteLine($"x = {x}");
            Console.WriteLine($"y1 = {y1}");
            Console.WriteLine($"y2 = {y2}");
            Console.WriteLine();
        }
    }

    public static void Example2()
    {
        const double tMin = -Math.PI;
        const double tMax = Math.PI;
        const int degree = 19;

        var func = Math.Sin;

        //var polynomial = 
        //    DfChebyshevPolynomial.Create(func, tMin, tMax, degree);

        var polynomial =
            DfChebyshevPolynomial.CreateApproximatingSin();

        var tValues =
            tMin
                .GetLinearRange(tMax, 10001, false)
                .ToImmutableArray();

        var y1Values = tValues.Select(func).CreateSignal(10000);
        var y2Values = polynomial.GetValues(tValues).CreateSignal(10000);
        var yDiff = y1Values - y2Values;

        var snrDb = y1Values.SignalToNoiseRatioDb(yDiff);

        yDiff.Plot(yDiff.SamplingSpecs.MinTime, yDiff.SamplingSpecs.MaxTime).SaveAsPng(@"D:\yDiff.png");

        foreach (var (i, c) in polynomial.ScalarCoefficients.IndexScalarPairs)
        {
            Console.WriteLine($"c{i} = {polynomial.ScalarCoefficients[i]}");
        }
        Console.WriteLine();

        Console.WriteLine($"SNR = {snrDb} db");
        Console.WriteLine();
    }

    public static void Example3()
    {
        var tMin = -1.5 * Math.PI;
        var tMax = 1.5 * Math.PI;

        var polynomial =
            DfChebyshevPolynomial.CreateApproximating(
                24,
                Math.Cos,
                tMin,
                tMax,
                10001
            );

        polynomial =
            DfChebyshevPolynomial.Create(
                polynomial
                    .ScalarCoefficients
                    .GetCopyByScalar(c => !c.IsNearZero(1e-15))
            );

        var fDt = polynomial.GetPolynomialDerivative1();

        var tValues =
            tMin
                .GetLinearRange(tMax, 10001, false)
                .ToImmutableArray();

        var y1Values = tValues.Select(t => -Math.Sin(t)).CreateSignal(10000);
        var y2Values = fDt.GetValues(tValues).CreateSignal(10000);
        var yDiff = y1Values - y2Values;

        var snrDb = y1Values.SignalToNoiseRatioDb(yDiff);

        y1Values.Plot(y1Values.SamplingSpecs.MinTime, y1Values.SamplingSpecs.MaxTime).SaveAsPng(@"D:\y1Values.png");
        y2Values.Plot(y2Values.SamplingSpecs.MinTime, y2Values.SamplingSpecs.MaxTime).SaveAsPng(@"D:\y2Values.png");
        yDiff.Plot(yDiff.SamplingSpecs.MinTime, yDiff.SamplingSpecs.MaxTime).SaveAsPng(@"D:\yDiff.png");

        Console.WriteLine($"SNR = {snrDb} db");
        Console.WriteLine();
    }

    public static void Example4()
    {
        var tMin = -0.5 * Math.PI;
        var tMax = 1.5 * Math.PI;

        var polynomial =
            DfChebyshevPolynomial.CreateApproximatingCos();

        var fDt = polynomial.GetPolynomialDerivative1();

        var tValues =
            tMin
                .GetLinearRange(tMax, 10001, false)
                .ToImmutableArray();

        var y1Values = tValues.Select(t => -Math.Sin(t)).CreateSignal(10000);
        var y2Values = fDt.GetValues(tValues).CreateSignal(10000);
        var yDiff = y1Values - y2Values;

        var snrDb = y1Values.SignalToNoiseRatioDb(yDiff);

        y1Values.Plot(y1Values.SamplingSpecs.MinTime, y1Values.SamplingSpecs.MaxTime).SaveAsPng(@"D:\y1Values.png");
        y2Values.Plot(y2Values.SamplingSpecs.MinTime, y2Values.SamplingSpecs.MaxTime).SaveAsPng(@"D:\y2Values.png");
        yDiff.Plot(yDiff.SamplingSpecs.MinTime, yDiff.SamplingSpecs.MaxTime).SaveAsPng(@"D:\yDiff.png");

        Console.WriteLine($"SNR = {snrDb} db");
        Console.WriteLine();
    }

    public static void Example5()
    {
        const double tMin = -1d;
        const double tMax = 1d;

        var p1 =
            DfChebyshevPolynomial.Create(
                LinFloat64Vector.Create(1, -0.7, 0.3, -0.1),
                -1, 1
            );

        var p2 =
            DfChebyshevPolynomial.Create(
                LinFloat64Vector.Create(-1.5, -0.75, -0.2, 1.1),
                -1, 1
            );

        var p3 = p1 * p2;

        var tValues =
            tMin
                .GetLinearRange(tMax, 10001, false)
                .ToImmutableArray();

        var p1Values = p1.GetValues(tValues).CreateSignal(10000);
        var p2Values = p2.GetValues(tValues).CreateSignal(10000);
        var p3Values = p3.GetValues(tValues).CreateSignal(10000);
        var p4Values = p1Values * p2Values;

        var pDiff = p4Values - p3Values;

        var snrDb = p4Values.SignalToNoiseRatioDb(pDiff);

        p1Values.Plot(p1Values.SamplingSpecs.MinTime, p1Values.SamplingSpecs.MaxTime).SaveAsPng(@"D:\p1Values.png");
        p2Values.Plot(p2Values.SamplingSpecs.MinTime, p2Values.SamplingSpecs.MaxTime).SaveAsPng(@"D:\p2Values.png");
        p3Values.Plot(p3Values.SamplingSpecs.MinTime, p3Values.SamplingSpecs.MaxTime).SaveAsPng(@"D:\p3Values.png");
        p4Values.Plot(p4Values.SamplingSpecs.MinTime, p4Values.SamplingSpecs.MaxTime).SaveAsPng(@"D:\p4Values.png");
        pDiff.Plot(pDiff.SamplingSpecs.MinTime, pDiff.SamplingSpecs.MaxTime).SaveAsPng(@"D:\pDiffValues.png");

        Console.WriteLine($"SNR = {snrDb} db");
        Console.WriteLine();
    }

}