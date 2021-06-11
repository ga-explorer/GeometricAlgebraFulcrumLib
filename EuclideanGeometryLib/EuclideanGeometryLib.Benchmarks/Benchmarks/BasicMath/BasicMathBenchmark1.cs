using System;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace EuclideanGeometryLib.Benchmarks.Benchmarks.BasicMath
{
    public class BasicMathBenchmark1
    {
        public const int NumbersCount = 1 << 15;


        public readonly Tuple<int, int>[] NumbersList 
            = new Tuple<int, int>[NumbersCount];


        [GlobalSetup]
        public void Setup()
        {
            var randGen = new System.Random(10);

            for (var i = 0; i < NumbersCount; i++)
            {
                var a = randGen.Next();
                var b = randGen.Next(100) + 1;

                NumbersList[i] = new Tuple<int, int>(a, b);
            }
        }

        [Benchmark]
        public int[] ComputeMod1()
        {
            var result = new int[NumbersCount];

            for (var i = 0; i < NumbersCount; i++)
            {
                var numbers = NumbersList[i];
                var a = numbers.Item1;
                var b = numbers.Item2;

                var r = a % b;
                result[i] = (r < 0) ? (r + b) : r;
            }

            return result;
        }

        [Benchmark]
        public int[] ComputeMod2()
        {
            var result = new int[NumbersCount];


            for (var i = 0; i < NumbersCount; i++)
            {
                var numbers = NumbersList[i];
                var a = numbers.Item1;
                var b = numbers.Item2;

                result[i] = (a % b + b) % b;
            }

            return result;
        }

        public bool Validate()
        {
            Setup();

            var results1 = ComputeMod1();
            var results2 = ComputeMod2();

            return !results1
                .Where((t, i) => t != results2[i])
                .Any();
        }
    }
}
