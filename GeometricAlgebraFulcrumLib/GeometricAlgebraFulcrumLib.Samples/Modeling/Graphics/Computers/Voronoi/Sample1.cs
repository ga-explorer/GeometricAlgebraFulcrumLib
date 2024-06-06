using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Borders;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Borders.Space2D.Mutable;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Computers.Voronoi;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Svg.DrawingBoard;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Samples.Modeling.Graphics.Computers.Voronoi;

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
                .Cast<ILinFloat64Vector2D>()
                .ToArray();

        var computer = new VoronoiComputer2D();

        var triangulation =
            computer.ComputeDelaunayTriangulation(pointsArray);

        var drawingBoard =
            MutableBoundingBox2D
                .CreateFromPoints(triangulation.Points.Cast<ILinFloat64Vector2D>())
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
                triangulation.Points.DataPoints.Cast<ILinFloat64Vector2D>(),
                2
            );

        drawingBoard.SaveToPngFile("Delaunay Triangulation.png");
    }
}