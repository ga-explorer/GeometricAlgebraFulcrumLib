using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using EuclideanGeometryLib.BasicShapes.Lines;
using EuclideanGeometryLib.BasicShapes.Lines.Immutable;
using EuclideanGeometryLib.Borders.Space2D.Immutable;
using EuclideanGeometryLib.Computers.Intersections;
using EuclideanGeometryLib.Random;

namespace EuclideanGeometryLib.Benchmarks.Benchmarks.GeometricAlgebra
{
    public class GaBenchmark4
    {
        private readonly List<LineSegment2D> _lineSegmentsList1
            = new List<LineSegment2D>();

        private readonly List<LineSegment2D> _lineSegmentsList2
            = new List<LineSegment2D>();

        private readonly GcLimitedLineIntersector2D _computer
            = new GcLimitedLineIntersector2D();


        public bool[] ResultsGa { get; private set; }

        public bool[] ResultsVa { get; private set; }

        public bool[] ResultsVaOptimized { get; private set; }


        [Params(2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16)]
        //[Params(5, 6, 7, 8)]
        public int OperationsCountLog2 { get; set; } = 10;

        public int OperationsCount => 1 << OperationsCountLog2;


        [Params(0, 0.25, 0.5, 0.75, 1)]
        public double IntersectionsFactor { get; set; } = 0.5;


        [GlobalSetup]
        public void Setup()
        {
            var randGen = new RandomGeometryGenerator(10);

            var boundingBox = BoundingBox2D.Create(-100, -100, 100, 100);

            var intersectionsCount = (int)(OperationsCount * IntersectionsFactor);
            for (var i = 0; i < OperationsCount; i++)
            {
                var p1 = randGen.GetPointInside(boundingBox);
                var p2 = randGen.GetPointInside(boundingBox);

                var lineSegment1 = LineSegment2D.Create(p1, p2);
                _lineSegmentsList1.Add(lineSegment1);

                if (intersectionsCount > 0)
                {
                    var p3 = randGen.GetPointInside(boundingBox);
                    var p0 = lineSegment1.GetPointAt(randGen.GetNumber());
                    var p4 = 2 * p0 - p3;

                    _lineSegmentsList2.Add(LineSegment2D.Create(p3, p4));

                    intersectionsCount--;
                }
                else
                {
                    var v = randGen.GetUnitVector2D();

                    var p3 = p1 + randGen.GetNumber(1, 5) * v;
                    var p4 = p2 + randGen.GetNumber(1, 5) * v;

                    _lineSegmentsList2.Add(LineSegment2D.Create(p3, p4));
                }
            }

            ResultsGa = new bool[OperationsCount];
            ResultsVa = new bool[OperationsCount];
            ResultsVaOptimized = new bool[OperationsCount];
        }


        [Benchmark(Baseline = true)]
        public bool[] ComputeUsingGa()
        {
            for (var i = 0; i < OperationsCount; i++)
            {
                _computer.SetLineAsLineSegment(_lineSegmentsList1[i]);
                ResultsGa[i] = _computer.TestIntersection(_lineSegmentsList2[i]);
            }

            return ResultsGa;
        }

        [Benchmark]
        public bool[] ComputeUsingVa()
        {
            for (var i = 0; i < OperationsCount; i++)
            {
                _computer.SetLineAsLineSegment(_lineSegmentsList1[i]);
                ResultsVa[i] = _computer.TestIntersectionVa(_lineSegmentsList2[i]);
            }

            return ResultsVa;
        }

        [Benchmark]
        public bool[] ComputeUsingVaOptimized()
        {
            for (var i = 0; i < OperationsCount; i++)
            {
                _computer.SetLineAsLineSegment(_lineSegmentsList1[i]);
                ResultsVaOptimized[i] = _computer.TestIntersectionVaOptimized(_lineSegmentsList2[i]);
            }

            return ResultsVaOptimized;
        }


        public void Validate(int operationsCountLog2 = 10, double intersectionsFactor = 0.25)
        {
            OperationsCountLog2 = operationsCountLog2;
            IntersectionsFactor = intersectionsFactor;

            Setup();

            ComputeUsingGa();
            ComputeUsingVa();
            ComputeUsingVaOptimized();

            for (var i = 0; i < OperationsCount; i++)
            {
                var resultGa = ResultsGa[i];
                var resultVa = ResultsVa[i];
                var resultVaOptimized = ResultsVaOptimized[i];

                if (resultVa != resultGa)
                    throw new InvalidOperationException();

                if (resultVa != resultVaOptimized)
                    throw new InvalidOperationException();
            }
        }
    }
}