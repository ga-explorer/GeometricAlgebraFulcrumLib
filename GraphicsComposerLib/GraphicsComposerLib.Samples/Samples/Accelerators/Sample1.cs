using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using DataStructuresLib.Random;
using EuclideanGeometryLib.Accelerators.BIH;
using EuclideanGeometryLib.BasicShapes.Lines;
using EuclideanGeometryLib.BasicShapes.Lines.Immutable;
using EuclideanGeometryLib.Borders;
using EuclideanGeometryLib.Borders.Space2D.Immutable;
using EuclideanGeometryLib.Random;
using GraphicsComposerLib.Svg.DrawingBoard;

namespace GraphicsComposerLib.Samples.Samples.Accelerators
{
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
}
