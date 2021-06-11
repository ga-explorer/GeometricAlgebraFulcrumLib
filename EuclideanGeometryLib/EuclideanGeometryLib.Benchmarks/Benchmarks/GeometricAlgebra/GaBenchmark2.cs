using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicShapes.Triangles.Immutable;
using EuclideanGeometryLib.Borders.Space3D.Immutable;
using EuclideanGeometryLib.Computers.Reflections;
using EuclideanGeometryLib.Random;

namespace EuclideanGeometryLib.Benchmarks.Benchmarks.GeometricAlgebra
{
    public class GaBenchmark2
    {
        private readonly List<Triangle3D> _trianglesList
            = new List<Triangle3D>();

        private readonly List<Tuple3D> _pointsList
            = new List<Tuple3D>();

        private readonly GcTriangleReflector3D _computer
            = new GcTriangleReflector3D();

        public Tuple3D[] ResultsGa { get; private set; }

        public Tuple3D[] ResultsVa { get; private set; }


        [Params(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16)]
        //[Params(5, 6, 7, 8)]
        public int OperationsCountLog2 { get; set; } = 10;

        public int OperationsCount => 1 << OperationsCountLog2;



        [GlobalSetup]
        public void Setup()
        {
            var randGen = new RandomGeometryGenerator(10);

            var boundingBox = BoundingBox3D.CreateFromPoints(-100, -100,-100, 100, 100, 100);

            for (var i = 0; i < OperationsCount; i++)
            {
                var p1 = randGen.GetPointInside(boundingBox);
                var p2 = randGen.GetPointInside(boundingBox);
                var p3 = randGen.GetPointInside(boundingBox);
                var p = randGen.GetPointInside(boundingBox);

                _trianglesList.Add(Triangle3D.Create(p1, p2, p3));
                _pointsList.Add(p);
            }

            ResultsGa = new Tuple3D[OperationsCount];
            ResultsVa = new Tuple3D[OperationsCount];
        }

        [Benchmark(Baseline = true)]
        public Tuple3D[] ComputeUsingGa()
        {
            for (var i = 0; i < OperationsCount; i++)
            {
                _computer.Triangle = _trianglesList[i];
                ResultsGa[i] = _computer.ReflectPoint(_pointsList[i]);
            }

            return ResultsGa;
        }

        [Benchmark]
        public Tuple3D[] ComputeUsingVa()
        {
            for (var i = 0; i < OperationsCount; i++)
            {
                _computer.Triangle = _trianglesList[i];
                ResultsVa[i] = _computer.ReflectPointVa(_pointsList[i]);
            }

            return ResultsVa;
        }


        public double Validate(int operationsCountLog2 = 10)
        {
            OperationsCountLog2 = operationsCountLog2;

            Setup();

            ComputeUsingGa();
            ComputeUsingVa();

            var maxDiff = 0.0d;
            for (var i = 0; i < OperationsCount; i++)
            {
                var diff = (ResultsGa[i] - ResultsVa[i]).Abs();

                if (maxDiff < diff.X) maxDiff = diff.X;
                if (maxDiff < diff.Y) maxDiff = diff.Y;
                if (maxDiff < diff.Z) maxDiff = diff.Z;
            }

            return maxDiff;
        }
    }
}