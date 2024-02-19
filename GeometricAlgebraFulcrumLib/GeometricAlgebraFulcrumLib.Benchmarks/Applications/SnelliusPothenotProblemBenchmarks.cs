using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.Samples.Geometry;

namespace GeometricAlgebraFulcrumLib.Benchmarks.Applications;

[SimpleJob]
public class SnelliusPothenotProblemBenchmarks
{
    public SnelliusPothenotData2D[] DataRecords { get; private set; }


    [GlobalSetup]
    public void Setup()
    {
        // Just for initializing GA lookup tables before computations start
        var cga = RGaConformalSpace4D.Instance;

        var random = new Random(10);

        DataRecords = new SnelliusPothenotData2D[1000];

        for (var i = 0; i < DataRecords.Length; i++)
        {
            var a = random.GetVector2D(1, 5);
            var b = random.GetVector2D(1, 5);
            var c = random.GetVector2D(1, 5);
            var p = random.GetVector2D(1, 5);

            //DataRecords[i] = SnelliusPothenotData2D.Create(a, b, c, p);

            DataRecords[i] = SnelliusPothenotData2D.Create(
                Float64Vector2D.Create(-7, -1),
                Float64Vector2D.Create(1, 5),
                Float64Vector2D.Create(15.6, 6),
                Float64Vector2D.Create(3.12, -18.5)
            );

            //DataRecords[i] = SnelliusPothenotData2D.Create(
            //    Float64Vector2D.Create(5, 6), 
            //    Float64Vector2D.Create(0, 1),
            //    Float64Vector2D.Create(3, 12),
            //    Float64Vector2D.Create(8, -3)
            //);
        }
        
        
    }

    /// <summary>
    /// This is significantly faster that the Linq Join method
    /// </summary>
    [Benchmark]
    public Float64Vector2D[] SolveUsingVga()
    {
        return DataRecords.Select(
            data => data.SolveUsingVGa()
        ).ToArray();
    }
    
    [Benchmark]
    public Float64Vector2D[] SolveUsingCgaCassini()
    {
        return DataRecords.Select(
            data => data.SolveUsingCGaCassini()
        ).ToArray();
    }
    
    [Benchmark]
    public Float64Vector2D[] SolveUsingCGaCollins()
    {
        return DataRecords.Select(
            data => data.SolveUsingCGaCollins()
        ).ToArray();
    }
}