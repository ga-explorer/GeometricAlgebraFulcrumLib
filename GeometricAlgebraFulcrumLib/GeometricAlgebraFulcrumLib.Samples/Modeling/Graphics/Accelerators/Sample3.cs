using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH.Space2D.Traversal;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Computers.Intersections;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Svg.DrawingBoard;
using GeometricAlgebraFulcrumLib.Modeling.Statistics;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Samples.Modeling.Graphics.Accelerators;

/// <summary>
/// This sample constructs a random collection of line segments in 2D.
/// Then a BIH is constructed and the intersections of a line
/// are computed in two ways to validate their equivalence
/// </summary>
public static class Sample3
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

        //Create BIH
        var bih = objectsList.ToBih2D();
        var bihRootNodeInfo = bih.GetRootNodeInfo();
        Console.Out.WriteLine(bihRootNodeInfo.Describe());

        //Find intersections of a line with the objects
        var intersector = new GcLineIntersector2D();

        //Generate 5 random lines and intersect each with geometric objects
        var linesData =
            new List<Tuple<Line2D, double[], AccBihLineTraversalState2D[]>>();

        for (var i = 0; i < 5; i++)
        {
            var lineOrigin = randGen.GetPointInside(boundingBox);
            var lineDirection = randGen.GetUnitLinVector2D();

            //if (i != 2) continue;

            var line1 = Line2D.Create(lineOrigin, lineDirection);

            intersector.SetLine(lineOrigin, lineDirection);

            var tList1 =
                intersector.ComputeIntersections(bih, true);

            //var t = intersector.ComputeFirstIntersection(bih);
            //var tList1 = new List<double>();
            //if (t.Item1) tList1.Add(t.Item2);

            linesData.Add(Tuple.Create(
                line1,
                tList1.Select(t => t.Item1).ToArray(),
                intersector.BihLineTraversalStates.ToArray()
            ));
        }

        //Select one line
        var results = linesData[2];
        var line = results.Item1;

        var stateIndex = 0;
        foreach (var state in results.Item3)
        {
            //Create drawing board and draw traversal steps
            var drawingBoard = boundingBox.CreateDrawingBoard(8);

            //Draw current BIH node
            drawingBoard.DrawBihNodeInfo(
                bih.GetNodeInfo(state.BihNode.NodeId)
            );

            //Draw full intersecting line
            drawingBoard.AddFrontLayer("Line");
            drawingBoard.ActiveLayer.ActiveStyle =
                SvgDrawingBoardLayerActiveStyle.Current;

            drawingBoard
                .ActiveLayer
                .SetPen(2, Color.Black, 6, 12)
                .DrawLine(line);

            //Draw Second range of line parameter in red
            //if (state.ParameterRange2.IsFinite())
            {
                drawingBoard
                    .ActiveLayer
                    .SetPen(2, Color.Blue)
                    .DrawLine(
                        line,
                        state.LineParameterMinValue,
                        state.LineParameterMaxValue
                    );

                if (!double.IsInfinity(state.LineParameterMinValue))
                    drawingBoard
                        .ActiveLayer
                        .SetPen(1, Color.Black)
                        .SetFill(Color.Red)
                        .DrawCircleMarker(
                            line.GetPointAt(state.LineParameterMinValue),
                            10
                        );

                if (!double.IsInfinity(state.LineParameterMaxValue))
                    drawingBoard
                        .ActiveLayer
                        .SetPen(1, Color.Black)
                        .SetFill(Color.Yellow)
                        .DrawCircleMarker(
                            line.GetPointAt(state.LineParameterMaxValue),
                            10
                        );
            }

            var nodeId = state.BihNode.NodeId;

            //Save to file
            var fileName1 = stateIndex.ToString("000-")
                            + nodeId
                            + (state.BihNode.IsLeaf ? "-leaf" : "")
                            + ".png";
            var filePath1 = Path.Combine(Directory.GetCurrentDirectory(), fileName1);
            drawingBoard.SaveToPngFile(filePath1);

            stateIndex++;
        }

        //Draw final result in a separate drawing
        var db1 = boundingBox.CreateDrawingBoard(8);

        //Draw all geometric objects in light salmon color
        db1
            .ActiveLayer
            .SetPen(2, Color.LightSalmon)
            .DrawShapes(bih);

        //Draw bounding box of geometric objects
        db1
            .ActiveLayer
            .SetPen(2, Color.Black)
            .SetFill(Color.Black, 0.25)
            .DrawBoundingBox(boundingBox);

        //Draw intersecting lines and their intersection points
        foreach (var lineData in linesData)
        {
            var line2 = lineData.Item1;
            var pointsList =
                line2.GetPointsAt(lineData.Item2).Cast<ILinFloat64Vector2D>();

            db1
                .ActiveLayer
                .SetPen(2, Color.Black)
                .DrawLine(line2);

            db1
                .ActiveLayer
                .SetPen(1, Color.Black)
                .SetFill(Color.White)
                .DrawCircleMarkers(pointsList, 5);
        }

        //Save to file
        db1.SaveToPngFile(
            Path.Combine(Directory.GetCurrentDirectory(), "bih.png")
        );
    }
}