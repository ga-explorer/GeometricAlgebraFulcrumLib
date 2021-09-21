using System;
using System.Drawing;
using System.Linq;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.Borders;
using EuclideanGeometryLib.Borders.Space2D.Immutable;
using EuclideanGeometryLib.Borders.Space2D.Mutable;
using EuclideanGeometryLib.Computers.Voronoi;
using EuclideanGeometryLib.Random;
using GraphicsComposerLib.Svg.DrawingBoard;

namespace GraphicsComposerLib.Samples.Samples.Computers.Voronoi
{
    public static class Sample1
    {
        public static void Execute()
        {
            //Create a set of random points
            var randGen = new Random(10);
            var boundingBox = BoundingBox2D.Create(-100, -100, 100, 100);

            var pointsArray = 
                boundingBox
                    .GetSubdivisions(10, 10)
                    .Cast<BoundingBox2D>()
                    .Select(bb => randGen.GetPointInside(bb))
                    .Cast<ITuple2D>()
                    .ToArray();

            var computer = new VoronoiComputer2D();

            var triangulation = 
                computer.ComputeDelaunayTriangulation(pointsArray);

            var drawingBoard = 
                MutableBoundingBox2D
                    .CreateFromPoints(triangulation.Points.Cast<ITuple2D>())
                    .CreateDrawingBoard(2);

            drawingBoard
                .ActiveLayer
                .SetDefaultPen(3, Color.DarkBlue)
                .DrawLineSegments(
                    triangulation.Edges
                );

            drawingBoard
                .ActiveLayer
                .SetDefaultPen(3, Color.DarkRed)
                .SetFill(Color.Blue, 0.2)
                .DrawCircleMarker(
                    triangulation.Points.BoundingSphere.Center,
                    5
                )
                .DrawCircle(
                    triangulation.Points.BoundingSphere.Center,
                    triangulation.Points.BoundingSphere.Radius
                )
                .DrawTriangle(
                    triangulation.Points.BoundingTriangle
                );

            drawingBoard
                .ActiveLayer
                .SetDefaultPen(1, Color.Black)
                .SetFill(Color.DarkOliveGreen)
                .DrawCircleMarkers(
                    triangulation.Points.DataPoints.Cast<ITuple2D>(), 
                    2
                );

            drawingBoard.SaveToPngFile("Delaunay Triangulation.png");
        }
    }
}
