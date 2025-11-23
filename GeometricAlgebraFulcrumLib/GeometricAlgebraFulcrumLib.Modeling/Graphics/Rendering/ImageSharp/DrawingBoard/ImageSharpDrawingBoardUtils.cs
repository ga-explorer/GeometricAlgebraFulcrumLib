using System.Numerics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Polytopes.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ImageSharp.DrawingStyles;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
// ReSharper disable PossibleMultipleEnumeration

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ImageSharp.DrawingBoard;

public static class ImageSharpDrawingBoardUtils
{
    public static IImageProcessingContext DrawPoints(this IImageProcessingContext ctx, WorldToScreenMap2D wtsMap, IEnumerable<LinFloat64Vector2D> points, ImageSharpPointMarkerDrawingStyle pointStyle)
    {
        var pen = pointStyle.GetStrokePen();
        var brush = pointStyle.GetFillBrush();
        var size = pointStyle.Size;
        var halfSize = size / 2f;

        if (pointStyle.Shape is PointMarkerShape.Cross or PointMarkerShape.X)
        {
            foreach (var point in points)
            {
                var center = point.ToImageSharpPointF(wtsMap);

                if (pointStyle.Shape == PointMarkerShape.Cross)
                {
                    ctx.DrawLine(
                        pen, 
                        new PointF(center.X - halfSize, center.Y),
                        new PointF(center.X + halfSize, center.Y)
                    );

                    ctx.DrawLine(
                        pen,
                        new PointF(center.X, center.Y - halfSize),
                        new PointF(center.X, center.Y + halfSize)
                    );
                }
                else
                {
                    ctx.DrawLine(
                        pen,
                        new PointF(center.X - halfSize, center.Y - halfSize),
                        new PointF(center.X + halfSize, center.Y + halfSize)
                    );

                    ctx.DrawLine(
                        pen,
                        new PointF(center.X - halfSize, center.Y + halfSize),
                        new PointF(center.X + halfSize, center.Y - halfSize)
                    );
                }
            }

            return ctx;
        }

        IPath markerPath = pointStyle.Shape switch
        {
            PointMarkerShape.Circle => 
                new EllipsePolygon(new PointF(0, 0), halfSize),

            PointMarkerShape.Square =>
                new RectangularPolygon(-halfSize, -halfSize, size, size),

            PointMarkerShape.TriangleUp =>
                new Polygon([
                    new PointF(0, -halfSize), 
                    new PointF(-halfSize, halfSize), 
                    new PointF(halfSize, halfSize)
                ]),

            PointMarkerShape.TriangleDown =>
                new Polygon([
                    new PointF(0, halfSize), 
                    new PointF(-halfSize, -halfSize), 
                    new PointF(halfSize, -halfSize)
                ]),

            _ => throw new InvalidOperationException()
        };

        foreach (var point in points)
        {
            var center = point.ToImageSharpPointF(wtsMap);
            var markerShape = markerPath.Translate(center);

            ctx.Fill(brush, markerShape);
            ctx.Draw(pen, markerShape);
        }

        return ctx;
    }

    public static IImageProcessingContext DrawArrows(this IImageProcessingContext ctx, WorldToScreenMap2D wtsMap, IEnumerable<(LinFloat64Vector2D, LinFloat64PolarAngle)> pointAnglePairs, ImageSharpArrowHeadDrawingStyle style)
    {
        // TODO: add more arrow shapes

        var pen = style.GetStrokePen();
        var brush = style.GetFillBrush();

        var arrowPath = new Polygon([
            new PointF(0, 0),
            new PointF(-style.SizeX, 0.5f * style.SizeY),
            new PointF(-2f / 3 * style.SizeX, 0),
            new PointF(-style.SizeX, -0.5f * style.SizeY),
        ]);

        foreach (var (endPoint, angle) in pointAnglePairs)
        {
            var m2 = Matrix3x2.CreateTranslation(endPoint.ToImageSharpPointF(wtsMap));
            var m1 = Matrix3x2.CreateRotation(-(float)angle.RadiansValue);

            var arrowShape =
                arrowPath.Transform(m1 * m2);

            ctx.Fill(brush, arrowShape);
            ctx.Draw(pen, arrowShape);
        }

        return ctx;
    }
    
    public static IImageProcessingContext DrawPolyLine(this IImageProcessingContext ctx, WorldToScreenMap2D wtsMap, IReadOnlyList<LinFloat64Vector2D> points, ImageSharpLineDrawingStyle lineStyle)
    {
        var pen = lineStyle.GetStrokePen();

        for (var i = 0; i < points.Count - 1; i++)
        {
            var p1 = points[i].ToImageSharpPointF(wtsMap);
            var p2 = points[i + 1].ToImageSharpPointF(wtsMap);

            ctx.DrawLine(pen, p1, p2);
        }

        return ctx;
    }

    public static IImageProcessingContext DrawPolyLine(this IImageProcessingContext ctx, WorldToScreenMap2D wtsMap, IReadOnlyList<LinFloat64Vector2D> points, ImageSharpLineDrawingStyle lineStyle, ImageSharpPointMarkerDrawingStyle pointStyle)
    {
        return ctx
            .DrawPolyLine(wtsMap, points, lineStyle)
            .DrawPoints(wtsMap, points, pointStyle);
    }
    
    public static IImageProcessingContext DrawLine(this IImageProcessingContext ctx, WorldToScreenMap2D wtsMap, (LinFloat64Vector2D, LinFloat64Vector2D) pointPair, ImageSharpLineDrawingStyle style)
    {
        var pen = style.GetStrokePen();

        var p1 = pointPair.Item1.ToImageSharpPointF(wtsMap);
        var p2 = pointPair.Item2.ToImageSharpPointF(wtsMap);

        ctx.DrawLine(pen, p1, p2);

        return ctx;
    }

    public static IImageProcessingContext DrawLine(this IImageProcessingContext ctx, WorldToScreenMap2D wtsMap, IPair<LinFloat64Vector2D> pointPair, ImageSharpLineDrawingStyle style)
    {
        var pen = style.GetStrokePen();

        var p1 = pointPair.Item1.ToImageSharpPointF(wtsMap);
        var p2 = pointPair.Item2.ToImageSharpPointF(wtsMap);

        ctx.DrawLine(pen, p1, p2);

        return ctx;
    }
    
    public static IImageProcessingContext DrawLines(this IImageProcessingContext ctx, WorldToScreenMap2D wtsMap, IEnumerable<Float64LineSegment2D> pointPairs, ImageSharpLineDrawingStyle style)
    {
        var pen = style.GetStrokePen();

        foreach (var pointPair in pointPairs)
        {
            var p1 = pointPair.Item1.ToImageSharpPointF(wtsMap);
            var p2 = pointPair.Item2.ToImageSharpPointF(wtsMap);

            ctx.DrawLine(pen, p1, p2);
        }

        return ctx;
    }

    public static IImageProcessingContext DrawLines(this IImageProcessingContext ctx, WorldToScreenMap2D wtsMap, IEnumerable<IPair<LinFloat64Vector2D>> pointPairs, ImageSharpLineDrawingStyle style)
    {
        var pen = style.GetStrokePen();

        foreach (var pointPair in pointPairs)
        {
            var p1 = pointPair.Item1.ToImageSharpPointF(wtsMap);
            var p2 = pointPair.Item2.ToImageSharpPointF(wtsMap);

            ctx.DrawLine(pen, p1, p2);
        }

        return ctx;
    }

    public static IImageProcessingContext DrawLines(this IImageProcessingContext ctx, WorldToScreenMap2D wtsMap, IEnumerable<(LinFloat64Vector2D, LinFloat64Vector2D)> pointPairs, ImageSharpLineDrawingStyle style)
    {
        var pen = style.GetStrokePen();

        foreach (var (point1, point2) in pointPairs)
        {
            var p1 = point1.ToImageSharpPointF(wtsMap);
            var p2 = point2.ToImageSharpPointF(wtsMap);

            ctx.DrawLine(pen, p1, p2);
        }

        return ctx;
    }
        
    public static IImageProcessingContext DrawVectors(this IImageProcessingContext ctx, WorldToScreenMap2D wtsMap, LinFloat64Vector2D origin, IEnumerable<LinFloat64Vector2D> directions, ImageSharpLineDrawingStyle lineStyle)
    {
        var pointPairs =
            directions.Select(p => 
                (origin, origin + p)
            );

        return ctx.DrawLines(wtsMap, pointPairs, lineStyle);
    }

    public static IImageProcessingContext DrawVectors(this IImageProcessingContext ctx, WorldToScreenMap2D wtsMap, IEnumerable<(LinFloat64Vector2D, LinFloat64Vector2D)> pointDirectionPairs, ImageSharpLineDrawingStyle lineStyle)
    {
        var pointPairs =
            pointDirectionPairs.Select(p => 
                (p.Item1, p.Item2 + p.Item1)
            );

        return ctx.DrawLines(wtsMap, pointPairs, lineStyle);
    }
        
    public static IImageProcessingContext DrawVectors(this IImageProcessingContext ctx, WorldToScreenMap2D wtsMap, LinFloat64Vector2D origin, IEnumerable<LinFloat64Vector2D> directions, ImageSharpLineDrawingStyle lineStyle, ImageSharpArrowHeadDrawingStyle arrowStyle)
    {
        var pointPairs =
            directions.Select(p => 
                (origin, origin + p)
            );

        ctx.DrawLines(wtsMap, pointPairs, lineStyle);

        var pointAnglePairs =
            directions.Select(p => 
                (origin, p.GetArrowHeadAngle())
            );

        return ctx.DrawArrows(wtsMap, pointAnglePairs, arrowStyle);
    }

    public static IImageProcessingContext DrawVectors(this IImageProcessingContext ctx, WorldToScreenMap2D wtsMap, IEnumerable<(LinFloat64Vector2D, LinFloat64Vector2D)> pointDirectionPairs, ImageSharpLineDrawingStyle lineStyle, ImageSharpArrowHeadDrawingStyle arrowStyle)
    {
        var pointPairs =
            pointDirectionPairs.Select(p => 
                (p.Item1, p.Item1 + p.Item2)
            );

        ctx.DrawLines(wtsMap, pointPairs, lineStyle);

        var pointAnglePairs =
            pointDirectionPairs.Select(p => 
                (p.Item1 + p.Item2, p.Item2.GetArrowHeadAngle())
            );

        return ctx.DrawArrows(wtsMap, pointAnglePairs, arrowStyle);
    }

    public static IImageProcessingContext DrawCircle(this IImageProcessingContext ctx, WorldToScreenMap2D wtsMap, LinFloat64Vector2D center, double radius, ImageSharpLineDrawingStyle lineStyle)
    {
        var ellipseSize = wtsMap.MapSizeToImageSharpSizeF(2 * radius, 2 * radius);
        var ellipseCenter = center.ToImageSharpPointF(wtsMap);

        var ellipseShape = new EllipsePolygon(
            ellipseCenter,
            ellipseSize
        );

        return ctx.DrawPolygon(
            lineStyle.GetStrokePen(), 
            ellipseShape.Points.ToArray()
        );
    }
    
    public static IImageProcessingContext DrawSquare(this IImageProcessingContext ctx, WorldToScreenMap2D wtsMap, LinFloat64Vector2D center, double size, ImageSharpLineDrawingStyle lineStyle)
    {
        var rectSize = 
            wtsMap.MapSizeToImageSharpSizeF(size);
        
        var rectCorner =
            center.VectorAdd(-size / 2, size / 2).ToImageSharpPointF(wtsMap);

        var rectShape = new RectangularPolygon(
            rectCorner,
            rectSize
        );

        return ctx.DrawPolygon(
            lineStyle.GetStrokePen(), 
            rectShape.Points.ToArray()
        );
    }

    public static IImageProcessingContext DrawRectangle(this IImageProcessingContext ctx, WorldToScreenMap2D wtsMap, LinFloat64Vector2D center, double sizeX, double sizeY, ImageSharpLineDrawingStyle lineStyle)
    {
        var rectSize = 
            wtsMap.MapSizeToImageSharpSizeF(sizeX, sizeY);
        
        var rectCorner =
            center.VectorAdd(-sizeX / 2, sizeY / 2).ToImageSharpPointF(wtsMap);

        var rectShape = new RectangularPolygon(
            rectCorner,
            rectSize
        );

        return ctx.DrawPolygon(
            lineStyle.GetStrokePen(), 
            rectShape.Points.ToArray()
        );
    }
    
    public static IImageProcessingContext DrawRectangle(this IImageProcessingContext ctx, WorldToScreenMap2D wtsMap, Float64BoundingBox2D box, ImageSharpLineDrawingStyle lineStyle)
    {
        var leftTop = 
            wtsMap.MapPointToImageSharpPointF(box.MinPoint);
        
        var bottomRight = 
            wtsMap.MapPointToImageSharpPointF(box.MaxPoint);
        
        var rectShape = new RectangularPolygon(
            leftTop,
            bottomRight
        );

        return ctx.DrawPolygon(
            lineStyle.GetStrokePen(), 
            rectShape.Points.ToArray()
        );
    }
    

    public static IImageProcessingContext DrawVertexPoints(this IImageProcessingContext ctx, WorldToScreenMap2D wtsMap, Float64RegularPolygon2D polygon, ImageSharpPointMarkerDrawingStyle style)
    {
        return ctx.DrawPoints(wtsMap, polygon.VertexPoints, style);
    }
        
    public static IImageProcessingContext DrawSideMidPoints(this IImageProcessingContext ctx, WorldToScreenMap2D wtsMap, Float64RegularPolygon2D polygon, ImageSharpPointMarkerDrawingStyle style)
    {
        return ctx.DrawPoints(wtsMap, polygon.SideMidPoints, style);
    }

    public static IImageProcessingContext DrawSides(this IImageProcessingContext ctx, WorldToScreenMap2D wtsMap, Float64RegularPolygon2D polygon, ImageSharpLineDrawingStyle style)
    {
        return ctx.DrawLines(
            wtsMap, 
            polygon.Sides.Select(p => (p.Item1, p.Item2)), 
            style
        );
    }
        
    public static IImageProcessingContext DrawDiagonals(this IImageProcessingContext ctx, WorldToScreenMap2D wtsMap, Float64RegularPolygon2D polygon, ImageSharpLineDrawingStyle style)
    {
        return ctx.DrawLines(
            wtsMap, 
            polygon.Diagonals.Select(p => (p.Item1, p.Item2)), 
            style
        );
    }
        
    public static IImageProcessingContext DrawChords(this IImageProcessingContext ctx, WorldToScreenMap2D wtsMap, Float64RegularPolygon2D polygon, ImageSharpLineDrawingStyle style)
    {
        return ctx.DrawLines(
            wtsMap, 
            polygon.Chords.Select(p => (p.Item1, p.Item2)), 
            style
        );
    }
        
    public static IImageProcessingContext DrawSideNormals(this IImageProcessingContext ctx, WorldToScreenMap2D wtsMap, Float64RegularPolygon2D polygon, double length, ImageSharpLineDrawingStyle lineStyle, ImageSharpArrowHeadDrawingStyle arrowStyle)
    {
        var normals = polygon.SideUnitNormals.ToArray();
        var midPoints = polygon.SideMidPoints.ToArray();

        var pointDirectionPairs =
            polygon.Count.GetRange(i => (midPoints[i], length * normals[i]));

        return ctx.DrawVectors(
            wtsMap, 
            pointDirectionPairs, 
            lineStyle, 
            arrowStyle
        );
    }

}