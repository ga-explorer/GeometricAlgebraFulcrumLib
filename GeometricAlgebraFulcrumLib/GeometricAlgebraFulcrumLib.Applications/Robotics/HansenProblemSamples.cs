using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Applications.Robotics;

/// <summary>
/// https://en.wikipedia.org/wiki/Hansen's_problem
/// </summary>
public static class HansenProblemSamples
{
    //public static void TimingExample1()
    //{
    //    var randomGen = new Random(10);

    //    var t1 = DateTime.Now;
    //    for (var i = 0; i < 10000000; i++)
    //    {
    //        //var origin = Float64Vector2D.Zero;
    //        var origin = 10 * randomGen.GetVector2D();

    //        var pointA = origin + randomGen.GetVector2D(1);
    //        var pointB = origin + randomGen.GetVector2D(0);
    //        var pointP1 = origin + randomGen.GetVector2D(2);
    //        var pointP2 = origin + randomGen.GetVector2D(3);

    //        var data = HansenProblemData2D.Create(pointA, pointB, pointP1, pointP2);

    //        data.SolveUsingVGa();
    //    }

    //    var t2 = DateTime.Now;

    //    Console.WriteLine(t2 - t1);
    //}

    public static void ValidationExample1()
    {
        var randomGen = new Random(10);

        for (var i = 0; i < 1000; i++)
        {
            var origin = LinFloat64Vector2D.Zero; //10 * randomGen.GetVector2D();

            //var pointA = origin + randomGen.GetVector2D();
            //var pointB = origin + randomGen.GetVector2D();
            //var pointP1 = origin + randomGen.GetVector2D();
            //var pointP2 = origin + randomGen.GetVector2D();

            //const double maxLength = 5;
            //var vA = randomGen.GetVector2D(1).ToUnitVector();
            //var vB = randomGen.GetVector2D(0).ToUnitVector();

            //var pointA = origin + maxLength * randomGen.NextDouble() * vA;
            //var pointB = origin + maxLength * randomGen.NextDouble() * vB;
            //var pointP1 = origin - maxLength * randomGen.NextDouble() * vB;
            //var pointP2 = origin - maxLength * randomGen.NextDouble() * vA;

            var angle = randomGen.GetPolarAngle();

            var pointA = LinFloat64Vector2D.Create(-1, 5).RotateBy(angle);
            var pointB = LinFloat64Vector2D.Create(6, 4).RotateBy(angle);
            var pointP1 = LinFloat64Vector2D.Create(-2, -1).RotateBy(angle);
            var pointP2 = LinFloat64Vector2D.Create(7, -3).RotateBy(angle);

            //var pointA = LinFloat64Vector2D.Create(-4, 2).RotateBy(angle);
            //var pointB = LinFloat64Vector2D.Create(10, 6).RotateBy(angle);
            //var pointP1 = LinFloat64Vector2D.Create(6, -2).RotateBy(angle);
            //var pointP2 = LinFloat64Vector2D.Create(2, 8).RotateBy(angle);

            var data = HansenProblemData2D.Create(
                pointA,
                pointB,
                pointP1,
                pointP2
            );

            var (p1, p2) = data.SolveUsingTrig();

            Debug.Assert(
                (pointP1 - p1).IsNearZero(1e-7) &&
                (pointP2 - p2).IsNearZero(1e-7)
            );

            (p1, p2) = data.SolveUsingTrigOpt();

            Debug.Assert(
                (pointP1 - p1).IsNearZero(1e-7) &&
                (pointP2 - p2).IsNearZero(1e-7)
            );

            (p1, p2) = data.SolveUsingComplex();

            Debug.Assert(
                (pointP1 - p1).IsNearZero(1e-7) &&
                (pointP2 - p2).IsNearZero(1e-7)
            );

            (p1, p2) = data.SolveUsingComplexOpt();

            Debug.Assert(
                (pointP1 - p1).IsNearZero(1e-7) &&
                (pointP2 - p2).IsNearZero(1e-7)
            );

            (p1, p2) = data.SolveUsingVGa();

            Debug.Assert(
                (pointP1 - p1).IsNearZero(1e-7) &&
                (pointP2 - p2).IsNearZero(1e-7)
            );

            (p1, p2) = data.SolveUsingVGaOpt();

            Debug.Assert(
                (pointP1 - p1).IsNearZero(1e-7) &&
                (pointP2 - p2).IsNearZero(1e-7)
            );

            (p1, p2) = data.SolveUsingCGa();

            Debug.Assert(
                (pointP1 - p1).IsNearZero(1e-7) &&
                (pointP2 - p2).IsNearZero(1e-7)
            );

            (p1, p2) = data.SolveUsingCGaOpt();

            Debug.Assert(
                (pointP1 - p1).IsNearZero(1e-7) &&
                (pointP2 - p2).IsNearZero(1e-7)
            );
        }

        // Just for initializing GA lookup tables before computations start
        //var ga = RGaConformalSpace4D.Instance;

        //var data = HansenData2D.Create(
        //    Float64Vector2D.Create(-1, 5),
        //    Float64Vector2D.Create(6, 4),
        //    Float64Vector2D.Create(-2, -1),
        //    Float64Vector2D.Create(7, -3)
        //);

        //var data = HansenProblemData2D.Create(
        //    Float64Vector2D.Create(-2, 0),
        //    Float64Vector2D.Create(2, 0),
        //    Float64Vector2D.Create(0, 1),
        //    Float64Vector2D.Create(-0.5, -0.5)
        //);

        //data.SolveUsingTrig(
        //    out var p1X, 
        //    out var p1Y, 
        //    out var p2X, 
        //    out var p2Y
        //);

        //var p2 = data.SolveUsingPGaPaco();
        //var p3 = data.SolveUsingCGaCassini();
        //var p4 = data.SolveUsingCGaCollins();

        //var p1 = Float64Vector2D.Create(p1X, p1Y);
        //var p2 = Float64Vector2D.Create(p2X, p2Y);

        //Console.WriteLine(data);
        ////Console.WriteLine($"VGA method result        : {p1.ToTupleString()}; {p2.ToTupleString()}");
        ////Console.WriteLine($"CGA Paco method result   : {p2.ToTupleString()}");
        ////Console.WriteLine($"CGA Cassini method result: {p3.ToTupleString()}");
        ////Console.WriteLine($"CGA Collins method result: {p4.ToTupleString()}");
        //Console.WriteLine();
    }
}