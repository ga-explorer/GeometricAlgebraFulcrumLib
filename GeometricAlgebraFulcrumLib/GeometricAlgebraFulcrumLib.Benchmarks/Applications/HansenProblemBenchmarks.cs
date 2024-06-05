using System;
using BenchmarkDotNet.Attributes;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Samples.Modeling.Geometry;

namespace GeometricAlgebraFulcrumLib.Benchmarks.Applications;

[SimpleJob]
public class HansenProblemBenchmarks
{
    public HansenProblemData2D[] DataRecords { get; private set; }


    [GlobalSetup]
    public void Setup()
    {
        var randomGen = new Random(10);

        const int n = 1000000;
        DataRecords = new HansenProblemData2D[n];
        
        for (var i = 0; i < DataRecords.Length; i++)
        {
            var origin = 10 * randomGen.GetLinVector2D();

            var pointA = origin + randomGen.GetLinVector2D();
            var pointB = origin + randomGen.GetLinVector2D();
            var pointP1 = origin + randomGen.GetLinVector2D();
            var pointP2 = origin + randomGen.GetLinVector2D();

            DataRecords[i] = HansenProblemData2D.Create(
                pointA, 
                pointB, 
                pointP1, 
                pointP2
            );

            //DataRecords[i] = HansenProblemData2D.Create(
            //    Float64Vector2D.Create(-1, 5),
            //    Float64Vector2D.Create(6, 4),
            //    Float64Vector2D.Create(-2, -1),
            //    Float64Vector2D.Create(7, -3)
            //);

            //DataRecords[i] = SnelliusPothenotData2D.Create(
            //    Float64Vector2D.Create(5, 6), 
            //    Float64Vector2D.Create(0, 1),
            //    Float64Vector2D.Create(3, 12),
            //    Float64Vector2D.Create(8, -3)
            //);
        }
    }
    
    [Benchmark]
    public double Baseline()
    {
        var s = 0d;

        foreach (var data in DataRecords)
        {
            HansenProblemData2D.SolveUsingNone(
                data,
                out var p1X, 
                out var p1Y, 
                out var p2X, 
                out var p2Y
            );

            s = p1X * p2X + p1Y * p2Y;
        }

        return s;
    }

    [Benchmark]
    public double SolveUsingTrig()
    {
        var s = 0d;

        foreach (var data in DataRecords)
        {
            HansenProblemData2D.SolveUsingTrig(
                data,
                out var p1X, 
                out var p1Y, 
                out var p2X, 
                out var p2Y
            );

            s = p1X * p2X + p1Y * p2Y;
        }

        return s;
    }
    
    [Benchmark]
    public double SolveUsingTrigOpt()
    {
        var s = 0d;

        foreach (var data in DataRecords)
        {
            HansenProblemData2D.SolveUsingTrigOpt(
                data,
                out var p1X, 
                out var p1Y, 
                out var p2X, 
                out var p2Y
            );

            s = p1X * p2X + p1Y * p2Y;
        }

        return s;
    }

    [Benchmark]
    public double SolveUsingComplex()
    {
        var s = 0d;

        foreach (var data in DataRecords)
        {
            HansenProblemData2D.SolveUsingComplex(
                data,
                out var p1X, 
                out var p1Y, 
                out var p2X, 
                out var p2Y
            );

            s = p1X * p2X + p1Y * p2Y;
        }

        return s;
    }
    
    [Benchmark]
    public double SolveUsingComplexOpt()
    {
        var s = 0d;

        foreach (var data in DataRecords)
        {
            HansenProblemData2D.SolveUsingComplexOpt(
                data,
                out var p1X, 
                out var p1Y, 
                out var p2X, 
                out var p2Y
            );

            s = p1X * p2X + p1Y * p2Y;
        }

        return s;
    }

    [Benchmark]
    public double SolveUsingVGa()
    {
        var s = 0d;

        foreach (var data in DataRecords)
        {
            HansenProblemData2D.SolveUsingVGa(
                data,
                out var p1X, 
                out var p1Y, 
                out var p2X, 
                out var p2Y
            );

            s = p1X * p2X + p1Y * p2Y;
        }

        return s;
    }
    
    [Benchmark]
    public double SolveUsingVGaOpt()
    {
        var s = 0d;

        foreach (var data in DataRecords)
        {
            HansenProblemData2D.SolveUsingVGaOpt(
                data,
                out var p1X, 
                out var p1Y, 
                out var p2X, 
                out var p2Y
            );

            s = p1X * p2X + p1Y * p2Y;
        }

        return s;
    }

    [Benchmark]
    public double SolveUsingCGa()
    {
        var s = 0d;

        foreach (var data in DataRecords)
        {
            HansenProblemData2D.SolveUsingCGa(
                data,
                out var p1X, 
                out var p1Y, 
                out var p2X, 
                out var p2Y
            );

            s = p1X * p2X + p1Y * p2Y;
        }

        return s;
    }
    
    [Benchmark]
    public double SolveUsingCGaOpt()
    {
        var s = 0d;

        foreach (var data in DataRecords)
        {
            HansenProblemData2D.SolveUsingCGaOpt(
                data,
                out var p1X, 
                out var p1Y, 
                out var p2X, 
                out var p2Y
            );

            s = p1X * p2X + p1Y * p2Y;
        }

        return s;
    }
}