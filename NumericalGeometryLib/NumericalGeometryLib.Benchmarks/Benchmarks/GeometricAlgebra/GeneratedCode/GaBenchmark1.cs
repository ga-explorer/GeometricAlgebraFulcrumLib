using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.BasicShapes.Lines.Immutable;
using NumericalGeometryLib.Borders.Space2D.Immutable;
using NumericalGeometryLib.Computers.Reflections;
using NumericalGeometryLib.Random;

namespace NumericalGeometryLib.Benchmarks.Benchmarks.GeometricAlgebra.GeneratedCode
{
    public class GaBenchmark1
    {
        private readonly List<LineSegment2D> _lineSegmentsList
            = new List<LineSegment2D>();

        private readonly List<Tuple2D> _pointsList
            = new List<Tuple2D>();

        private readonly GcLineSegmentReflector2D _computer
            = new GcLineSegmentReflector2D();

        public Tuple2D[] ResultsGa { get; private set; }

        public Tuple2D[] ResultsVa { get; private set; }


        [Params(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16)]
        //[Params(5, 6, 7, 8)]
        public int OperationsCountLog2 { get; set; } = 10;

        public int OperationsCount => 1 << OperationsCountLog2;



        [GlobalSetup]
        public void Setup()
        {
            var randGen = new System.Random(10);

            var boundingBox = BoundingBox2D.Create(-100, -100, 100, 100);

            for (var i = 0; i < OperationsCount; i++)
            {
                var p1 = randGen.GetPointInside(boundingBox);
                var p2 = randGen.GetPointInside(boundingBox);
                var p = randGen.GetPointInside(boundingBox);

                _lineSegmentsList.Add(LineSegment2D.Create(p1, p2));
                _pointsList.Add(p);
            }

            ResultsGa = new Tuple2D[OperationsCount];
            ResultsVa = new Tuple2D[OperationsCount];
        }

        [Benchmark(Baseline = true)]
        public Tuple2D[] ComputeUsingGa()
        {
            for (var i = 0; i < OperationsCount; i++)
            {
                _computer.LineSegment = _lineSegmentsList[i];
                ResultsGa[i] = _computer.ReflectPoint(_pointsList[i]);
            }

            return ResultsGa;
        }

        [Benchmark]
        public Tuple2D[] ComputeUsingVa()
        {
            for (var i = 0; i < OperationsCount; i++)
            {
                _computer.LineSegment = _lineSegmentsList[i];
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
            }

            return maxDiff;
        }
    }
}
