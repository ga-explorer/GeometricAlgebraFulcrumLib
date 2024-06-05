using System;
using BenchmarkDotNet.Attributes;

namespace GeometricAlgebraFulcrumLib.Benchmarks.Structures;

[SimpleJob]
public class ScalarOperations2Benchmarks
{
    public const int Count = 100000000;

    public Random RandomGenerator { get; } = new Random();


    [Benchmark(Baseline = true)]
    public double AddBaseline()
    {
        var s = 0d;

        for (var i = 0; i < Count; i++)
        {
            var v1 = RandomGenerator.NextDouble();
            var v2 = RandomGenerator.NextDouble();
            var v3 = RandomGenerator.NextDouble();

            var v = v1 + v2;
            s = v;
        }

        return s;
    }
    
    [Benchmark]
    public double Add()
    {
        var s = 0d;

        for (var i = 0; i < Count; i++)
        {
            var v1 = RandomGenerator.NextDouble();
            var v2 = RandomGenerator.NextDouble();
            var v3 = RandomGenerator.NextDouble();

            var v = v1 + v2;
            s = v + v3;
        }

        return s;
    }
    
    [Benchmark]
    public double Subtract()
    {
        var s = 0d;

        for (var i = 0; i < Count; i++)
        {
            var v1 = RandomGenerator.NextDouble();
            var v2 = RandomGenerator.NextDouble();
            var v3 = RandomGenerator.NextDouble();

            var v = v1 + v2;
            s = v - v3;
        }

        return s;
    }

    [Benchmark]
    public double Times()
    {
        var s = 0d;

        for (var i = 0; i < Count; i++)
        {
            var v1 = RandomGenerator.NextDouble();
            var v2 = RandomGenerator.NextDouble();
            var v3 = RandomGenerator.NextDouble();

            var v = v1 + v2;
            s = v * v3;
        }

        return s;
    }

    [Benchmark]
    public double Sqrt()
    {
        var s = 0d;

        for (var i = 0; i < Count; i++)
        {
            var v1 = RandomGenerator.NextDouble();
            var v2 = RandomGenerator.NextDouble();
            var v3 = RandomGenerator.NextDouble();

            var v = v1 + v2;
            s = Math.Sqrt(v);
        }

        return s;
    }
    
    [Benchmark]
    public double Sin()
    {
        var s = 0d;

        for (var i = 0; i < Count; i++)
        {
            var v1 = RandomGenerator.NextDouble();
            var v2 = RandomGenerator.NextDouble();
            var v3 = RandomGenerator.NextDouble();

            var v = v1 + v2;
            s = Math.Sin(v);
        }

        return s;
    }
    
    [Benchmark]
    public double Cos()
    {
        var s = 0d;

        for (var i = 0; i < Count; i++)
        {
            var v1 = RandomGenerator.NextDouble();
            var v2 = RandomGenerator.NextDouble();
            var v3 = RandomGenerator.NextDouble();

            var v = v1 + v2;
            s = Math.Cos(v);
        }

        return s;
    }
}