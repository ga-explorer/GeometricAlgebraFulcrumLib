using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using DataStructuresLib.Random;
using NumericalGeometryLib.BasicOperations;
using NumericalGeometryLib.BasicShapes.Lines;
using NumericalGeometryLib.BasicShapes.Lines.Immutable;
using NumericalGeometryLib.Borders.Space2D.Immutable;
using NumericalGeometryLib.Computers.Intersections;
using NumericalGeometryLib.Random;

namespace NumericalGeometryLib.Benchmarks.Benchmarks.BasicOperations
{
    public class BasicOperationsBenchmark1
    {
        private readonly List<LineSegment2D> _lineSegmentsList1
            = new List<LineSegment2D>();

        private readonly List<LineSegment2D> _lineSegmentsList2
            = new List<LineSegment2D>();

        private readonly GcLimitedLineIntersector2D _computer
            = new GcLimitedLineIntersector2D();


        public Tuple<bool, double>[] ResultsGa1 { get; private set; }

        public Tuple<bool, double>[] ResultsGa2 { get; private set; }


        //[Params(2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16)]
        [Params(5, 6, 7, 8)]
        public int OperationsCountLog2 { get; set; } = 10;

        public int OperationsCount => 1 << OperationsCountLog2;


        //[Params(0, 0.25, 0.5, 0.75, 1)]
        public double IntersectionsFactor { get; set; } = 0.5;


        [GlobalSetup]
        public void Setup()
        {
            var randGen = new System.Random(10);

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

            ResultsGa1 = new Tuple<bool, double>[OperationsCount];
            ResultsGa2 = new Tuple<bool, double>[OperationsCount];
        }

        [Benchmark(Baseline = true)]
        public Tuple<bool, double>[] ComputeUsingGa1()
        {
            for (var i = 0; i < OperationsCount; i++)
            {
                ResultsGa1[i] = _computer
                    .SetLineAsLineSegment(_lineSegmentsList1[i])
                    .ComputeIntersection(_lineSegmentsList2[i]);
            }

            return ResultsGa1;
        }

        //This is slawer by 20% than using the intersection computer
        [Benchmark]
        public Tuple<bool, double>[] ComputeUsingGa2()
        {
            for (var i = 0; i < OperationsCount; i++)
            {
                ResultsGa2[i] = _lineSegmentsList1[i]
                    .ToLimitedLine()
                    .ComputeIntersection(_lineSegmentsList2[i]);
            }

            return ResultsGa1;
        }


        public double Validate(int operationsCountLog2 = 10, double intersectionsFactor = 0.25)
        {
            OperationsCountLog2 = operationsCountLog2;
            IntersectionsFactor = intersectionsFactor;

            Setup();

            ComputeUsingGa1();
            ComputeUsingGa2();

            var maxDiff = 0.0d;
            for (var i = 0; i < OperationsCount; i++)
            {
                var resultGa1 = ResultsGa1[i];
                var resultGa2 = ResultsGa2[i];

                if (resultGa2.Item1 != resultGa1.Item1)
                    throw new InvalidOperationException();

                var diff = Math.Abs(resultGa1.Item2 - resultGa2.Item2);
                if (maxDiff < diff) maxDiff = diff;
            }

            return maxDiff;
        }
    }
}
