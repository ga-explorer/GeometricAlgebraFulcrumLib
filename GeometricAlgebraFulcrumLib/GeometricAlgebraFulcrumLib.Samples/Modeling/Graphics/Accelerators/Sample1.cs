using System;
using System.Collections.Generic;
using System.IO;
using GeometricAlgebraFulcrumLib.Modeling.Geometry;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Svg.DrawingBoard;
using GeometricAlgebraFulcrumLib.Modeling.Statistics;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Samples.Modeling.Graphics.Accelerators;

/// <summary>
/// This sample constructs a random collection of line segments in 2D.
/// Then a BIH is constructed and a drawing is generated for each BIH node
/// to show is construction
/// </summary>
public static class Sample1
{
    public static void Execute()
    {
        var randGen = new Random(10);

        var boundingBox = BoundingBox2D.Create(-160, -160, 120, 120);
        var divisions = boundingBox.GetSubdivisions(8, 8);

        //Generate one object per bounding box division
        var objectsList = new List<ILineSegment2D>();
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

        //Draw one drawing per BIH node and save to file
        foreach (var bihNodeInfo in bihRootNodeInfo.GetNodesInfo())
        {
            var drawingBoard = boundingBox.CreateDrawingBoard(8);

            //Draw all geometric objects of root BIH node
            drawingBoard.ActiveLayer.LayerName = "Geometric Objects";
            drawingBoard.ActiveLayer.ActiveStyle =
                SvgDrawingBoardLayerActiveStyle.Default;

            drawingBoard
                .ActiveLayer
                .SetDefaultPen(1, Color.SaddleBrown)
                .DrawShapes(bihRootNodeInfo.Node.Contents);

            //Draw current BIH node
            drawingBoard.DrawBihNodeInfo(bihNodeInfo);

            var fileName = bihNodeInfo.NodeId + ".png";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            drawingBoard.SaveToPngFile(filePath);
        }
    }
}