﻿using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Borders;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Accelerators.BIH;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Accelerators.Grids;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Computers.Intersections;
using GeometricAlgebraFulcrumLib.Core.Modeling.Statistics;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Samples.Modeling.Graphics.Accelerators;

public static class Sample4
{
    public static string ValidateLineTraversal(IReadOnlyList<ILineSegment2D> lineSegmentsList)
    {
        var composer = new LinearTextComposer();

        var lineSegmentsBih = lineSegmentsList.ToBih2D();
        var lineSegmentsGrid = lineSegmentsList.ToGrid2D();

        var randGen = new Random(10);

        var intersector = new GcLineIntersector2D();

        for (var i = 0; i < 1000; i++)
        {
            var origin = randGen.GetPointInside(lineSegmentsBih.BoundingBox);
            var direction = randGen.GetPointInside(lineSegmentsBih.BoundingBox) - origin;

            intersector.SetLine(origin, direction);

            var result1 =
                intersector
                    .ComputeIntersections(lineSegmentsList)
                    .OrderBy(r => r.Item1)
                    .ToArray();

            var result2 =
                intersector
                    .ComputeIntersections(lineSegmentsBih)
                    .OrderBy(r => r.Item1)
                    .ToArray();

            var result31 =
                intersector
                    .ComputeIntersections(lineSegmentsGrid)
                    .OrderBy(r => r.Item1);

            var result32 = new Dictionary<ILineSegment2D, double>();
            foreach (var r in result31)
                if (!result32.ContainsKey(r.Item2))
                    result32.Add(r.Item2, r.Item1);

            var result3 =
                result32
                    .Select(
                        p => new Tuple<double, ILineSegment2D>(
                            p.Value, p.Key
                        )).ToArray();

            if (result1.Length != result2.Length || result1.Length != result3.Length)
            {
                composer.AppendAtNewLine("Intersection Count Mismatch");
                return composer.ToString();
            }

            for (var j = 0; j < result1.Length; j++)
            {
                if (Math.Abs(result1[j].Item1 - result2[j].Item1) > 1e-10 ||
                    !ReferenceEquals(result1[j].Item2, result2[j].Item2)
                   )
                {
                    composer.AppendAtNewLine("Intersection Mismatch");
                    return composer.ToString();
                }

                if (Math.Abs(result1[j].Item1 - result3[j].Item1) > 1e-10 ||
                    !ReferenceEquals(result1[j].Item2, result3[j].Item2)
                   )
                {
                    composer.AppendAtNewLine("Intersection Mismatch");
                    return composer.ToString();
                }
            }
        }

        return composer.ToString();
    }


    public static void Execute()
    {
        var randGen = new Random(10);

        var boundingBox = BoundingBox2D.Create(-160, -120, 160, 120);
        var divisions = boundingBox.GetSubdivisions(8, 8);

        //Generate one object per bounding box division
        var objectsList = new List<LineSegment2D>();
        for (var ix = 0; ix < divisions.GetLength(0) - 1; ix++)
            for (var iy = 0; iy < divisions.GetLength(1) - 1; iy++)
            {
                if (randGen.GetNumber() > 0.1)
                {
                    objectsList.Add(randGen.GetLineSegmentInside(divisions[ix, iy]));
                    continue;
                }

                var p1 = randGen.GetPointInside(divisions[ix, iy]);
                var p2 = randGen.GetPointInside(divisions[ix + 1, iy + 1]);

                var lineSegment = LineSegment2D.Create(p1, p2);

                objectsList.Add(lineSegment);
            }

        var result = ValidateLineTraversal(objectsList);

        Console.Out.Write(result);
    }
}