using System;
using BenchmarkDotNet.Running;
using NumericalGeometryLib.Benchmarks.Benchmarks.GeometricAlgebra.Products;

namespace NumericalGeometryLib.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            //var benchmark = new GaBenchmark1() {VSpaceDimension = 12};

            //benchmark.Setup();

            //benchmark.EGpBinaryTrie();
            //benchmark.EGpSparseList();

            BenchmarkRunner.Run<GaBenchmark1>();
        }
    }
}
