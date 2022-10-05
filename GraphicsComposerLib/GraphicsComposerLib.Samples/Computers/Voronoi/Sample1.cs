using System;

using System.Linq;
using GraphicsComposerLib.Rendering.Svg.DrawingBoard;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.Borders;
using NumericalGeometryLib.Borders.Space2D.Immutable;
using NumericalGeometryLib.Borders.Space2D.Mutable;
using NumericalGeometryLib.Computers.Voronoi;
using NumericalGeometryLib.Random;
using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Samples.Computers.Voronoi
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
