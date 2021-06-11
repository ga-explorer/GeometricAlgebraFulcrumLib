using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using EuclideanGeometryLib.BasicShapes.Lines.Immutable;
using EuclideanGeometryLib.BasicShapes.Triangles;
using EuclideanGeometryLib.BasicShapes.Triangles.Immutable;
using EuclideanGeometryLib.Borders.Space3D.Immutable;
using EuclideanGeometryLib.Computers.Intersections;
using EuclideanGeometryLib.Random;

namespace EuclideanGeometryLib.Benchmarks.Benchmarks.GeometricAlgebra
{
    public class GaBenchmark6
    {
        private readonly List<LineSegment3D> _lineSegmentsList
            = new List<LineSegment3D>();

        private readonly List<Triangle3D> _trianglesList
            = new List<Triangle3D>();

        private readonly GcLimitedLineIntersector3D _computer
            = new GcLimitedLineIntersector3D();


        public bool[] ResultsGa { get; private set; }

        public bool[] ResultsVa { get; private set; }


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

            var boundingBox = BoundingBox3D.CreateFromPoints(-100, -100, -100, 100, 100, 100);

            var intersectionsCount = (int)(OperationsCount * IntersectionsFactor);
            for (var i = 0; i < OperationsCount; i++)
            {
                var p1 = randGen.GetPointInside(boundingBox);
                var p2 = randGen.GetPointInside(boundingBox);
                var p3 = randGen.GetPointInside(boundingBox);

                var triangle = Triangle3D.Create(p1, p2, p3);
                _trianglesList.Add(triangle);

                if (intersectionsCount > 0)
                {
                    var p4 = randGen.GetPointInside(boundingBox);
                    var p0 = triangle.GetPointAt(
                        randGen.GetNumber(),
                        randGen.GetNumber(),
                        randGen.GetNumber()
                    );
                    var p5 = 2 * p0 - p4;

                    _lineSegmentsList.Add(LineSegment3D.Create(p4, p5));

                    intersectionsCount--;
                }
                else
                {
                    var v = randGen.GetUnitVector3D();

                    var p4 = randGen.GetPointInside(triangle) + randGen.GetNumber(1, 5) * v;
                    var p5 = randGen.GetPointInside(triangle) + randGen.GetNumber(1, 5) * v;

                    _lineSegmentsList.Add(LineSegment3D.Create(p4, p5));
                }
            }

            ResultsGa = new bool[OperationsCount];
            ResultsVa = new bool[OperationsCount];
        }


        [Benchmark(Baseline = true)]
        public bool[] ComputeUsingGa()
        {
            for (var i = 0; i < OperationsCount; i++)
            {
                _computer.SetLineAsLineSegment(_lineSegmentsList[i]);
                ResultsGa[i] = _computer.TestIntersection(_trianglesList[i]);
            }

            return ResultsGa;
        }

        [Benchmark]
        public bool[] ComputeUsingVa()
        {
            for (var i = 0; i < OperationsCount; i++)
            {
                _computer.SetLineAsLineSegment(_lineSegmentsList[i]);
                ResultsVa[i] = _computer.TestIntersectionVa(_trianglesList[i]);
            }

            return ResultsVa;
        }


        public void Validate(int operationsCountLog2 = 10, double intersectionsFactor = 0.25)
        {
            OperationsCountLog2 = operationsCountLog2;
            IntersectionsFactor = intersectionsFactor;

            Setup();

            ComputeUsingGa();
            ComputeUsingVa();

            for (var i = 0; i < OperationsCount; i++)
            {
                var resultGa = ResultsGa[i];
                var resultVa = ResultsVa[i];

                if (resultVa != resultGa)
                    throw new InvalidOperationException();
            }
        }
    }
}