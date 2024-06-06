using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Borders;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Accelerators;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Accelerators.Grids;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Accelerators.Grids.Space2D;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Computers.Intersections;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Svg.DrawingBoard;
using GeometricAlgebraFulcrumLib.Core.Modeling.Statistics;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Samples.Modeling.Graphics.Accelerators;

/// <summary>
/// This sample constructs a random collection of line segments in 2D.
/// Then a Grid is constructed and the intersections of a line
/// are computed in two ways to validate their equivalence
/// </summary>
public static class Sample2
{
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

        //Create Grid
        var grid = objectsList.ToGrid2D();

        var drawingBoard =
            grid
                .BoundingBox
                .GetMutableBoundingBox()
                .UpdateSizeByFactor(0.1d)
                .CreateDrawingBoard(8);

        var drawingSettings = new AccGridDrawingSettings2D()
        {
            DrawActiveCells = true
        };

        drawingBoard.DrawGrid(grid, drawingSettings);

        var fileName = "grid.svg";
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        drawingBoard.SaveToSvgFile(filePath);

        //Find intersections of a line with the objects
        var intersector = new GcLineIntersector2D();

        //Generate 5 random lines and intersect each with geometric objects
        var linesData = new List<Tuple<Line2D, double[]>>();

        for (var i = 0; i < 5; i++)
        {
            var lineOrigin = randGen.GetPointInside(boundingBox);
            var lineDirection = randGen.GetUnitLinVector2D();

            var line = Line2D.Create(lineOrigin, lineDirection);

            intersector.SetLine(lineOrigin, lineDirection);

            //var t = intersector.ComputeFirstIntersection(grid);
            //var tList1 = new List<double>();
            //if (t.Item1) tList1.Add(t.Item2);

            var tList1 = intersector.ComputeIntersections(grid);

            linesData.Add(Tuple.Create(
                line,
                tList1.Select(t => t.Item1).ToArray()
            ));
        }

        ////For each line draw the traversed grid cells
        //var traversalData = grid.GetLineTraversalData(linesData[1].Item1);

        //drawingBoard.DrawGridLineTraversal(traversalData, drawingSettings);

        //Create drawing boards and draw intersections
        var db1 = boundingBox.CreateDrawingBoard(8);

        //Draw all geometric objects in light salmon color
        db1
            .ActiveLayer
            .SetPen(2, Color.LightSalmon)
            .DrawShapes(grid);

        //Draw bounding box of geometric objects
        db1
            .ActiveLayer
            .SetPen(2, Color.Black)
            .SetFill(Color.Black, 0.25)
            .DrawBoundingBox(boundingBox);

        //Draw intersecting lines and their intersection points
        foreach (var lineData in linesData)
        {
            var line = lineData.Item1;
            var pointsList =
                line.GetPointsAt(lineData.Item2).Cast<ILinFloat64Vector2D>();

            db1
                .ActiveLayer
                .SetPen(2, Color.Black)
                .DrawLine(line);

            db1
                .ActiveLayer
                .SetPen(1, Color.Black)
                .SetFill(Color.White)
                .DrawCircleMarkers(pointsList, 5);
        }

        //Save to file
        db1.SaveToPngFile(
            Path.Combine(Directory.GetCurrentDirectory(), "grid.png")
        );
    }
}