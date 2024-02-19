using GeometricAlgebraFulcrumLib.Lite.Geometry;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space2D.Mutable;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Computers.Voronoi;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Svg.DrawingBoard;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Lite.Samples.Graphics.Computers.Voronoi;

public static class Sample1
{
    public static void Execute()
    {
        //Create a set of random points
        var randGen = new System.Random(10);
        var boundingBox = BoundingBox2D.Create(-100, -100, 100, 100);

        var pointsArray = 
            boundingBox
                .GetSubdivisions(10, 10)
                .Cast<BoundingBox2D>()
                .Select(bb => randGen.GetPointInside(bb))
                .Cast<IFloat64Vector2D>()
                .ToArray();

        var computer = new VoronoiComputer2D();

        var triangulation = 
            computer.ComputeDelaunayTriangulation(pointsArray);

        var drawingBoard = 
            MutableBoundingBox2D
                .CreateFromPoints(triangulation.Points.Cast<IFloat64Vector2D>())
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
                triangulation.Points.DataPoints.Cast<IFloat64Vector2D>(), 
                2
            );

        drawingBoard.SaveToPngFile("Delaunay Triangulation.png");
    }
}