using System;
using BenchmarkDotNet.Attributes;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Applications.Robotics;

namespace GeometricAlgebraFulcrumLib.Benchmarks.Applications;

[SimpleJob]
public class SnelliusPothenotProblemBenchmarks
{
    public SnelliusPothenotProblemData2D[] DataRecords { get; private set; }


    [GlobalSetup]
    public void Setup()
    {
        // Just for initializing GA lookup tables before computations start
        //var cga = XGaConformalSpace4D.Instance;

        var random = new Random(10);

        DataRecords = new SnelliusPothenotProblemData2D[1000000];

        for (var i = 0; i < DataRecords.Length; i++)
        {
            var a = 5 * random.GetLinVector2D();
            var b = 5 * random.GetLinVector2D();
            var c = 5 * random.GetLinVector2D();
            var p = 20 * random.GetLinVector2D();

            DataRecords[i] = SnelliusPothenotProblemData2D.Create(
                a, 
                b, 
                c, 
                p
            );

            //DataRecords[i] = SnelliusPothenotProblemData2D.Create(
            //    Float64Vector2D.Create(-7, -1),
            //    Float64Vector2D.Create(1, 5),
            //    Float64Vector2D.Create(15.6, 6),
            //    Float64Vector2D.Create(3.12, -18.5)
            //);

            //DataRecords[i] = SnelliusPothenotData2D.Create(
            //    Float64Vector2D.Create(5, 6), 
            //    Float64Vector2D.Create(0, 1),
            //    Float64Vector2D.Create(3, 12),
            //    Float64Vector2D.Create(8, -3)
            //);
        }
        
        
    }
    

    [Benchmark(Baseline = true)]
    public double Baseline()
    {
        var s = 0d;

        foreach (var data in DataRecords)
        {
            SnelliusPothenotProblemData2D.SolveUsingNone(
                data,
                out var pX, 
                out var pY
            );

            s = pX + pY;
        }

        return s;
    }
    
    [Benchmark]
    public double SolveUsingPierlot()
    {
        var s = 0d;

        foreach (var data in DataRecords)
        {
            SnelliusPothenotProblemData2D.SolveUsingPierlot(
                data,
                out var pX, 
                out var pY
            );

            s = pX + pY;
        }

        return s;
    }
    
    [Benchmark]
    public double SolveUsingPierlot2()
    {
        var s = 0d;

        foreach (var data in DataRecords)
        {
            SnelliusPothenotProblemData2D.SolveUsingPierlot2(
                data,
                out var pX, 
                out var pY
            );

            s = pX + pY;
        }

        return s;
    }

    [Benchmark]
    public double SolveUsingVga()
    {
        var s = 0d;

        foreach (var data in DataRecords)
        {
            SnelliusPothenotProblemData2D.SolveUsingVGa(
                data,
                out var pX, 
                out var pY
            );

            s = pX + pY;
        }

        return s;
    }
    
    [Benchmark]
    public double SolveUsingPGaPaco()
    {
        var s = 0d;

        foreach (var data in DataRecords)
        {
            SnelliusPothenotProblemData2D.SolveUsingPGaPaco(
                data,
                out var pX, 
                out var pY
            );

            s = pX + pY;
        }

        return s;
    }

    [Benchmark]
    public double SolveUsingCGaPaco()
    {
        var s = 0d;

        foreach (var data in DataRecords)
        {
            SnelliusPothenotProblemData2D.SolveUsingCGaPaco(
                data,
                out var pX, 
                out var pY
            );

            s = pX + pY;
        }

        return s;
    }

    [Benchmark]
    public double SolveUsingCGaCassini()
    {
        var s = 0d;

        foreach (var data in DataRecords)
        {
            SnelliusPothenotProblemData2D.SolveUsingCGaCassini(
                data,
                out var pX, 
                out var pY
            );

            s = pX + pY;
        }

        return s;
    }
    
    [Benchmark]
    public double SolveUsingCGaCollins()
    {
        var s = 0d;

        foreach (var data in DataRecords)
        {
            SnelliusPothenotProblemData2D.SolveUsingCGaCollins(
                data,
                out var pX, 
                out var pY
            );

            s = pX + pY;
        }

        return s;
    }
}