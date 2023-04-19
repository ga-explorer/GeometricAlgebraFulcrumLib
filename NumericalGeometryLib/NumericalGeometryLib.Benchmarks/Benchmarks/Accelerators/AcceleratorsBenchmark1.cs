using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using DataStructuresLib.Random;
using NumericalGeometryLib.Accelerators.BIH;
using NumericalGeometryLib.Accelerators.BIH.Space2D;
using NumericalGeometryLib.Accelerators.Grids;
using NumericalGeometryLib.Accelerators.Grids.Space2D;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.BasicShapes.Lines;
using NumericalGeometryLib.Borders;
using NumericalGeometryLib.Borders.Space1D.Immutable;
using NumericalGeometryLib.Borders.Space2D.Immutable;
using NumericalGeometryLib.Computers.Intersections;
using NumericalGeometryLib.Random;
using TextComposerLib.Text;
using TextComposerLib.Text.Linear;

namespace NumericalGeometryLib.Benchmarks.Benchmarks.Accelerators
{
    public class AcceleratorsBenchmark1
    {
        private System.Random RandomGenerator { get; set; }

        private GcLineIntersector2D Computer1 { get; }
            = new GcLineIntersector2D();

        private GcLimitedLineIntersector2D Computer2 { get; }
            = new GcLimitedLineIntersector2D();

        public List<ILineSegment2D> LineSegmentsList { get; }
            = new List<ILineSegment2D>();

        public AccBih2D<ILineSegment2D> LineSegmentsBih { get; private set; }

        public AccGrid2D<ILineSegment2D> LineSegmentsGrid { get; private set; }

        public List<ILine2D> LinesList { get; }
            = new List<ILine2D>();

        [Params(10, 70, 130, 190)]//, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160, 170, 180, 190)]
        public int LineSegmentsCount { get; set; }

        //[Params(true, false)]
        public bool UseLimitedLines { get; set; } = true;


        [GlobalSetup]
        public void Setup()
        {
            RandomGenerator = new System.Random(10);
            LinesList.Clear();
            LineSegmentsList.Clear();

            var boundingBox = BoundingBox2D.Create(0, 0, 1024, 768);

            var boundingBoxes = boundingBox.GetSubdivisions(16, 12);

            for (var k = 0; k < 20; k++)
            {
                var line = RandomGenerator
                    .GetLineSegmentInside(boundingBox)
                    .ToLine();

                LinesList.Add(line);
            }

            var indicesList = new List<IntTuple2D>(
                boundingBoxes.GetLength(0) * boundingBoxes.GetLength(1)
            );

            for (var i = 0; i < boundingBoxes.GetLength(0); i++)
            for (var j = 0; j < boundingBoxes.GetLength(1); j++)
                indicesList.Add(new IntTuple2D(i, j));

            for (var k = 0; k < LineSegmentsCount; k++)
            {
                var m = RandomGenerator.GetInteger(indicesList.Count - 1);
                var index = indicesList[m];
                var bb = boundingBoxes[index.X, index.Y];

                var maxR = bb.GetShortestSideLength() / 3;
                var minR = maxR / 4;

                var pointsList = RandomGenerator.GetPolygonPoints2D(
                    10,
                    bb.GetMidPoint(),
                    RandomGenerator.GetNumbers(10, minR, maxR).ToArray()
                ).Cast<IFloat64Tuple2D>();

                //var lineSegment = RandomGenerator.GetLineSegment2D(
                //    boundingBoxes[index.ItemX, index.ItemY]
                //);

                LineSegmentsList.AddRange(pointsList.ToLineSegments(true));

                indicesList.RemoveAt(m);
            }

            LineSegmentsBih = LineSegmentsList.ToBih2D();
            LineSegmentsGrid = LineSegmentsList.ToGrid2D();
        }


        //[Benchmark]
        public List<double>[] GetAllIntersectionsFromList()
        {
            var result = new List<double>[LinesList.Count];

            if (UseLimitedLines)
            {
                for (var i = 0; i < LinesList.Count; i++)
                {
                    Computer2.SetLine(LinesList[i], BoundingBox1D.Create(0, 1));

                    result[i] = Computer2
                        .ComputeIntersections(LineSegmentsList)
                        .Select(v => v.Item1)
                        .ToList();
                }

                return result;
            }

            for (var i = 0; i < LinesList.Count; i++)
            {
                Computer1.SetLine(LinesList[i]);

                result[i] = Computer1
                    .ComputeIntersections(LineSegmentsList)
                    .Select(v => v.Item1)
                    .ToList();
            }

            return result;
        }

        [Benchmark]
        public List<double>[] GetAllIntersectionsFromBih()
        {
            var result = new List<double>[LinesList.Count];

            if (UseLimitedLines)
            {
                for (var i = 0; i < LinesList.Count; i++)
                {
                    Computer2.SetLine(LinesList[i], BoundingBox1D.Create(0, 1));

                    result[i] = Computer2
                        .ComputeIntersections(LineSegmentsBih)
                        .Select(v => v.Item1)
                        .ToList();
                }

                return result;
            }

            for (var i = 0; i < LinesList.Count; i++)
            {
                Computer1.SetLine(LinesList[i]);

                result[i] = Computer1
                    .ComputeIntersections(LineSegmentsBih)
                    .Select(v => v.Item1)
                    .ToList();
            }

            return result;
        }

        [Benchmark]
        public List<double>[] GetAllIntersectionsFromGrid()
        {
            var result = new List<double>[LinesList.Count];

            if (UseLimitedLines)
            {
                for (var i = 0; i < LinesList.Count; i++)
                {
                    Computer2.SetLine(LinesList[i], BoundingBox1D.Create(0, 1));

                    result[i] = Computer2
                        .ComputeIntersections(LineSegmentsGrid)
                        .Select(v => v.Item1)
                        .ToList();
                }

                return result;
            }

            for (var i = 0; i < LinesList.Count; i++)
            {
                Computer1.SetLine(LinesList[i]);

                result[i] = Computer1
                    .ComputeIntersections(LineSegmentsGrid)
                    .Select(v => v.Item1)
                    .ToList();
            }

            return result;
        }

        public string CompareGetAllIntersections()
        {
            var resultList = GetAllIntersectionsFromList();
            var resultBih = GetAllIntersectionsFromBih();
            var resultGrid = GetAllIntersectionsFromGrid();

            var composer = new LinearTextComposer();

            composer
                .AppendAtNewLine("Comparing all intersections:")
                .IncreaseIndentation();

            for (var i = 0; i < LinesList.Count; i++)
            {
                var r1 = resultList[i].Distinct().OrderBy(v => v).ToArray();
                var r2 = resultBih[i].Distinct().OrderBy(v => v).ToArray();
                var r3 = resultGrid[i].Distinct().OrderBy(v => v).ToArray();

                if (r1.Length == 0 && r2.Length == 0 && r3.Length == 0)
                    continue;

                composer
                    .AppendLineAtNewLine("Line " + (i + 1).ToString("00") + ":")
                    .IncreaseIndentation()
                    .AppendAtNewLine("List: ")
                    .AppendLine(
                        r1.Select(v => v.ToString("N13")).Concatenate(", ")
                    )
                    .AppendAtNewLine(" BIH: ")
                    .AppendLine(
                        r2.Select(v => v.ToString("N13")).Concatenate(", ")
                    )
                    .AppendAtNewLine("Grid: ")
                    .AppendLine(
                        r3.Select(v => v.ToString("N13")).Concatenate(", ")
                    )
                    .DecreaseIndentation()
                    .AppendLine();
            }

            return composer.ToString();
        }


        //[Benchmark]
        public Tuple<bool, double, double>[] GetEdgeIntersectionsFromList()
        {
            var result = new Tuple<bool, double, double>[LinesList.Count];

            if (UseLimitedLines)
            {
                for (var i = 0; i < LinesList.Count; i++)
                {
                    Computer2.SetLine(LinesList[i], BoundingBox1D.Create(0, 1));

                    var n = Computer2.ComputeEdgeIntersections(LineSegmentsList);
                    result[i] = Tuple.Create(n.Item1, n.Item2, n.Item3);
                }

                return result;
            }

            for (var i = 0; i < LinesList.Count; i++)
            {
                Computer1.SetLine(LinesList[i]);

                var n = Computer1.ComputeEdgeIntersections(LineSegmentsList);
                result[i] = Tuple.Create(n.Item1, n.Item2, n.Item3);
            }

            return result;
        }

        [Benchmark]
        public Tuple<bool, double, double>[] GetEdgeIntersectionsFromBih()
        {
            var result = new Tuple<bool, double, double>[LinesList.Count];

            if (UseLimitedLines)
            {
                for (var i = 0; i < LinesList.Count; i++)
                {
                    Computer2.SetLine(LinesList[i], BoundingBox1D.Create(0, 1));

                    var n = Computer2.ComputeEdgeIntersections(LineSegmentsBih);
                    result[i] = Tuple.Create(n.Item1, n.Item2, n.Item3);
                }

                return result;
            }

            for (var i = 0; i < LinesList.Count; i++)
            {
                Computer1.SetLine(LinesList[i]);

                var n = Computer1.ComputeEdgeIntersections(LineSegmentsBih);
                result[i] = Tuple.Create(n.Item1, n.Item2, n.Item3);
            }

            return result;
        }

        [Benchmark]
        public Tuple<bool, double, double>[] GetEdgeIntersectionsFromGrid()
        {
            var result = new Tuple<bool, double, double>[LinesList.Count];

            if (UseLimitedLines)
            {
                for (var i = 0; i < LinesList.Count; i++)
                {
                    Computer2.SetLine(LinesList[i], BoundingBox1D.Create(0, 1));

                    var n = Computer2.ComputeEdgeIntersections(LineSegmentsGrid);
                    result[i] = Tuple.Create(n.Item1, n.Item2, n.Item3);
                }

                return result;
            }

            for (var i = 0; i < LinesList.Count; i++)
            {
                Computer1.SetLine(LinesList[i]);

                var n = Computer1.ComputeEdgeIntersections(LineSegmentsGrid);
                result[i] = Tuple.Create(n.Item1, n.Item2, n.Item3);
            }

            return result;
        }

        public string CompareGetEdgeIntersections()
        {
            var r1 = GetEdgeIntersectionsFromList();
            var r2 = GetEdgeIntersectionsFromBih();
            var r3 = GetEdgeIntersectionsFromGrid();

            var composer = new LinearTextComposer();

            composer
                .AppendAtNewLine("Comparing edge intersections:")
                .IncreaseIndentation();

            composer
                .AppendAtNewLine("List: ")
                .AppendLine(
                    r1.Where(v => v.Item1)
                        .Select(v => "(" + v.Item2.ToString("N13") + ", " + v.Item3.ToString("N13") + ")")
                        .Concatenate(", ")
                );

            composer
                .AppendAtNewLine(" BIH: ")
                .AppendLine(
                    r2.Where(v => v.Item1)
                        .Select(v => "(" + v.Item2.ToString("N13") + ", " + v.Item3.ToString("N13") + ")")
                        .Concatenate(", ")
                );

            composer
                .AppendAtNewLine("Grid: ")
                .AppendLine(
                    r3.Where(v => v.Item1)
                        .Select(v => "(" + v.Item2.ToString("N13") + ", " + v.Item3.ToString("N13") + ")")
                        .Concatenate(", ")
                );

            return composer.ToString();
        }


        //[Benchmark]
        public Tuple<bool, double>[] GetFirstIntersectionFromList()
        {
            var result = new Tuple<bool, double>[LinesList.Count];

            if (UseLimitedLines)
            {
                for (var i = 0; i < LinesList.Count; i++)
                {
                    Computer2.SetLine(LinesList[i], BoundingBox1D.Create(0, 1));

                    var n = Computer2.ComputeFirstIntersection(LineSegmentsList);
                    result[i] = Tuple.Create(n.Item1, n.Item2);
                }

                return result;
            }

            for (var i = 0; i < LinesList.Count; i++)
            {
                Computer1.SetLine(LinesList[i]);

                var n = Computer1.ComputeFirstIntersection(LineSegmentsList);
                result[i] = Tuple.Create(n.Item1, n.Item2);
            }

            return result;
        }

        //[Benchmark]
        public Tuple<bool, double>[] GetFirstIntersectionFromBih()
        {
            var result = new Tuple<bool, double>[LinesList.Count];

            if (UseLimitedLines)
            {
                for (var i = 0; i < LinesList.Count; i++)
                {
                    Computer2.SetLine(LinesList[i], BoundingBox1D.Create(0, 1));

                    var n = Computer2.ComputeFirstIntersection(LineSegmentsBih);
                    result[i] = Tuple.Create(n.Item1, n.Item2);
                }

                return result;
            }

            for (var i = 0; i < LinesList.Count; i++)
            {
                Computer1.SetLine(LinesList[i]);

                var n = Computer1.ComputeFirstIntersection(LineSegmentsBih);
                result[i] = Tuple.Create(n.Item1, n.Item2);
            }

            return result;
        }

        //[Benchmark]
        public Tuple<bool, double>[] GetFirstIntersectionFromGrid()
        {
            var result = new Tuple<bool, double>[LinesList.Count];

            if (UseLimitedLines)
            {
                for (var i = 0; i < LinesList.Count; i++)
                {
                    Computer2.SetLine(LinesList[i], BoundingBox1D.Create(0, 1));

                    var n = Computer2.ComputeFirstIntersection(LineSegmentsGrid);
                    result[i] = Tuple.Create(n.Item1, n.Item2);
                }

                return result;
            }

            for (var i = 0; i < LinesList.Count; i++)
            {
                Computer1.SetLine(LinesList[i]);

                var n = Computer1.ComputeFirstIntersection(LineSegmentsGrid);
                result[i] = Tuple.Create(n.Item1, n.Item2);
            }

            return result;
        }

        public string CompareGetFirstIntersection()
        {
            var r1 = GetFirstIntersectionFromList();
            var r2 = GetFirstIntersectionFromBih();
            var r3 = GetFirstIntersectionFromGrid();

            var composer = new LinearTextComposer();

            composer
                .AppendAtNewLine("Comparing first intersection:")
                .IncreaseIndentation();

            composer
                .AppendAtNewLine("List: ")
                .AppendLine(
                    r1.Where(v => v.Item1)
                        .Select(v => v.Item2.ToString("N13"))
                        .Concatenate(", ")
                );

            composer
                .AppendAtNewLine(" BIH: ")
                .AppendLine(
                    r2.Where(v => v.Item1)
                        .Select(v => v.Item2.ToString("N13"))
                        .Concatenate(", ")
                );

            composer
                .AppendAtNewLine("Grid: ")
                .AppendLine(
                    r3.Where(v => v.Item1)
                        .Select(v => v.Item2.ToString("N13"))
                        .Concatenate(", ")
                );

            return composer.ToString();
        }


        //[Benchmark]
        public Tuple<bool, double>[] GetLastIntersectionFromList()
        {
            var result = new Tuple<bool, double>[LinesList.Count];

            if (UseLimitedLines)
            {
                for (var i = 0; i < LinesList.Count; i++)
                {
                    Computer2.SetLine(LinesList[i], BoundingBox1D.Create(0, 1));

                    var n = Computer2.ComputeLastIntersection(LineSegmentsList);
                    result[i] = Tuple.Create(n.Item1, n.Item2);
                }

                return result;
            }

            for (var i = 0; i < LinesList.Count; i++)
            {
                Computer1.SetLine(LinesList[i]);

                var n = Computer1.ComputeLastIntersection(LineSegmentsList);
                result[i] = Tuple.Create(n.Item1, n.Item2);
            }

            return result;
        }

        //[Benchmark]
        public Tuple<bool, double>[] GetLastIntersectionFromBih()
        {
            var result = new Tuple<bool, double>[LinesList.Count];

            if (UseLimitedLines)
            {
                for (var i = 0; i < LinesList.Count; i++)
                {
                    Computer2.SetLine(LinesList[i], BoundingBox1D.Create(0, 1));

                    var n = Computer2.ComputeLastIntersection(LineSegmentsBih);
                    result[i] = Tuple.Create(n.Item1, n.Item2);
                }

                return result;
            }

            for (var i = 0; i < LinesList.Count; i++)
            {
                Computer1.SetLine(LinesList[i]);

                var n = Computer1.ComputeLastIntersection(LineSegmentsBih);
                result[i] = Tuple.Create(n.Item1, n.Item2);
            }

            return result;
        }

        //[Benchmark]
        public Tuple<bool, double>[] GetLastIntersectionFromGrid()
        {
            var result = new Tuple<bool, double>[LinesList.Count];

            if (UseLimitedLines)
            {
                for (var i = 0; i < LinesList.Count; i++)
                {
                    Computer2.SetLine(LinesList[i], BoundingBox1D.Create(0, 1));

                    var n = Computer2.ComputeLastIntersection(LineSegmentsGrid);
                    result[i] = Tuple.Create(n.Item1, n.Item2);
                }

                return result;
            }

            for (var i = 0; i < LinesList.Count; i++)
            {
                Computer1.SetLine(LinesList[i]);

                var n = Computer1.ComputeLastIntersection(LineSegmentsGrid);
                result[i] = Tuple.Create(n.Item1, n.Item2);
            }

            return result;
        }

        public string CompareGetLastIntersection()
        {
            var r1 = GetLastIntersectionFromList();
            var r2 = GetLastIntersectionFromBih();
            var r3 = GetLastIntersectionFromGrid();

            var composer = new LinearTextComposer();

            composer
                .AppendAtNewLine("Comparing last intersection:")
                .IncreaseIndentation();

            composer
                .AppendAtNewLine("List: ")
                .AppendLine(
                    r1.Where(v => v.Item1)
                        .Select(v => v.Item2.ToString("N13"))
                        .Concatenate(", ")
                );

            composer
                .AppendAtNewLine(" BIH: ")
                .AppendLine(
                    r2.Where(v => v.Item1)
                        .Select(v => v.Item2.ToString("N13"))
                        .Concatenate(", ")
                );

            composer
                .AppendAtNewLine("Grid: ")
                .AppendLine(
                    r3.Where(v => v.Item1)
                        .Select(v => v.Item2.ToString("N13"))
                        .Concatenate(", ")
                );

            return composer.ToString();
        }


        //[Benchmark]
        public bool[] TestIntersectionFromList()
        {
            var result = new bool[LinesList.Count];

            if (UseLimitedLines)
            {
                for (var i = 0; i < LinesList.Count; i++)
                {
                    Computer2.SetLine(LinesList[i], BoundingBox1D.Create(0, 1));

                    result[i] = Computer2.TestIntersection(LineSegmentsList);
                }

                return result;
            }

            for (var i = 0; i < LinesList.Count; i++)
            {
                Computer1.SetLine(LinesList[i]);

                result[i] = Computer1.TestIntersection(LineSegmentsList);
            }

            return result;
        }

        [Benchmark]
        public bool[] TestIntersectionFromBih()
        {
            var result = new bool[LinesList.Count];

            if (UseLimitedLines)
            {
                for (var i = 0; i < LinesList.Count; i++)
                {
                    Computer2.SetLine(LinesList[i], BoundingBox1D.Create(0, 1));

                    result[i] = Computer2.TestIntersection(LineSegmentsBih);
                }

                return result;
            }

            for (var i = 0; i < LinesList.Count; i++)
            {
                Computer1.SetLine(LinesList[i]);

                result[i] = Computer1.TestIntersection(LineSegmentsBih);
            }

            return result;
        }

        [Benchmark]
        public bool[] TestIntersectionFromGrid()
        {
            var result = new bool[LinesList.Count];

            if (UseLimitedLines)
            {
                for (var i = 0; i < LinesList.Count; i++)
                {
                    Computer2.SetLine(LinesList[i], BoundingBox1D.Create(0, 1));

                    result[i] = Computer2.TestIntersection(LineSegmentsGrid);
                }

                return result;
            }

            for (var i = 0; i < LinesList.Count; i++)
            {
                Computer1.SetLine(LinesList[i]);

                result[i] = Computer1.TestIntersection(LineSegmentsGrid);
            }

            return result;
        }

        public string CompareTestIntersection()
        {
            var r1 = TestIntersectionFromList();
            var r2 = TestIntersectionFromBih();
            var r3 = TestIntersectionFromGrid();

            var composer = new LinearTextComposer();

            composer
                .AppendAtNewLine("Comparing test intersection:")
                .IncreaseIndentation();

            composer
                .AppendAtNewLine("List: ")
                .AppendLine(r1.Select(v => v ? "1" : "0").Concatenate());

            composer
                .AppendAtNewLine(" BIH: ")
                .AppendLine(r2.Select(v => v ? "1" : "0").Concatenate());

            composer
                .AppendAtNewLine("Grid: ")
                .AppendLine(r3.Select(v => v ? "1" : "0").Concatenate());

            return composer.ToString();
        }
    }
}
