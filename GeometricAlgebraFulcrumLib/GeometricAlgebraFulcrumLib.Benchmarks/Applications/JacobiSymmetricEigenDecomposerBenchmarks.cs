using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BenchmarkDotNet.Attributes;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;

namespace GeometricAlgebraFulcrumLib.Benchmarks.Applications;

[SimpleJob]
public class JacobiSymmetricEigenDecomposerBenchmarks
{
    public static void Validate()
    {
        var bm = new JacobiSymmetricEigenDecomposerBenchmarks();

        bm.Setup();

        var n = bm.Size;

        for (var i = 0; i < bm.MatrixList1.Count; i++)
        {
            var m1 = bm.MatrixList1[i];
            var evd1 = new JacobiSymmetricEigenDecomposer(m1);
            evd1.EigenDecompose();

            var m2 = bm.MatrixList2[i];
            var evd2 = new JacobiSymmetricEigenDecomposer(m2);
            evd2.EigenDecomposeManual();

            var m3 = bm.MatrixList3[i];
            var evd3 = new JacobiSymmetricEigenDecomposer(m3);
            evd3.EigenDecomposeLibrary();

            var ev1 = evd1.EigenValues.OrderBy(s => s).ToArray();
            var ev2 = evd2.EigenValues.OrderBy(s => s).ToArray();
            var ev3 = evd3.EigenValues.OrderBy(s => s).ToArray();

            for (var j = 0; j < n; j++)
            {
                Debug.Assert(Math.Abs(ev3[j] - ev1[j]) < 1e-10);
                Debug.Assert(Math.Abs(ev3[j] - ev2[j]) < 1e-10);
            }
        }
    }


    public int Size { get; set; } = 6;

    public List<double[,]> MatrixList1 { get; } = new List<double[,]>();

    public List<double[,]> MatrixList2 { get; } = new List<double[,]>();

    public List<double[,]> MatrixList3 { get; } = new List<double[,]>();


    [GlobalSetup]
    public void Setup()
    {
        var randomGen = new Random(10);

        const int n = 10000;
        
        for (var k = 0; k < n; k++)
        {
            var matrix1 = new double[Size, Size];
            var matrix2 = new double[Size, Size];
            var matrix3 = new double[Size, Size];

            for (var i = 0; i < Size; i++)
            for (var j = i; j < Size; j++)
            {
                var s = randomGen.GetInt32(20);

                matrix1[i, j] = s;
                matrix1[j, i] = s;

                matrix2[i, j] = s;
                matrix2[j, i] = s;

                matrix3[i, j] = s;
                matrix3[j, i] = s;
            }

            MatrixList1.Add(matrix1);
            MatrixList2.Add(matrix2);
            MatrixList3.Add(matrix3);
        }
    }
    
    //[Benchmark]
    public void DecomposeGenerated()
    {
        foreach (var matrix in MatrixList1)
        {
            new JacobiSymmetricEigenDecomposer(matrix).EigenDecompose();
        }
    }
    
    //[Benchmark]
    public void DecomposeManual()
    {
        foreach (var matrix in MatrixList2)
        {
            new JacobiSymmetricEigenDecomposer(matrix).EigenDecomposeManual();
        }
    }
    
    //[Benchmark]
    public void DecomposeLibrary()
    {
        foreach (var matrix in MatrixList3)
        {
            new JacobiSymmetricEigenDecomposer(matrix).EigenDecomposeLibrary();
        }
    }
    
    [Benchmark]
    public void DecomposeStepGenerated()
    {
        foreach (var matrix in MatrixList1)
        {
            new JacobiSymmetricEigenDecomposer(matrix).EigenDecomposeStep();
        }
    }
    
    [Benchmark]
    public void DecomposeStepManual()
    {
        foreach (var matrix in MatrixList2)
        {
            new JacobiSymmetricEigenDecomposer(matrix).EigenDecomposeStepManual();
        }
    }

}