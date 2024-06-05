using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Borders;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Borders.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Styles;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Svg.DrawingBoard;

public static class SvgDrawingBoardUtils
{

    public static SvgStyle UpdateFrom(this SvgStyle svgStyle, SvgDrawingBoardStyle srcStyle, bool updateStroke, bool updateFill, bool updateFont)
    {
        if (updateStroke)
            svgStyle
                .StrokeWidth.SetTo(srcStyle.PenWidth)
                .Stroke.SetToRgb(srcStyle.PenColor)
                .StrokeOpacity.SetToNumber(srcStyle.PenOpacity)
                .StrokeDashArray.SetTo(srcStyle.PenDashPattern);

        if (updateFill)
            svgStyle
                .Fill.SetToRgb(srcStyle.FillColor)
                .FillOpacity.SetToNumber(srcStyle.FillOpacity);

        if (updateFont)
            svgStyle
                .FontSize.SetTo(srcStyle.FontSize);

        return svgStyle;
    }


    #region SvgDrawingBoard Operations
    public static SvgDrawingBoard CreateDrawingBoard(this IBoundingBox2D boundingBox, int scalingFactor, double marginPercent = 0.1d)
    {
        var b = marginPercent == 0
            ? boundingBox
            : boundingBox
                .GetMutableBoundingBox()
                .UpdateSizeByFactor(marginPercent);

        return SvgDrawingBoard.Create(
            (int)(b.GetLengthX() * scalingFactor),
            (int)(b.GetLengthY() * scalingFactor)
        ).SetViewBox(
            b.GetMidX(),
            b.GetMidY(),
            b.GetLengthX()
        );
    }



    public static SvgDrawingBoardLayer DrawShape(this SvgDrawingBoardLayer drawingBoardLayer, IFiniteGeometricShape2D shape)
    {
        var lineSegment = shape as ILineSegment2D;
        if (!ReferenceEquals(lineSegment, null))
            return drawingBoardLayer.DrawLineSegment(lineSegment);

        var triangle = shape as ITriangle2D;
        if (!ReferenceEquals(triangle, null))
            return drawingBoardLayer.DrawTriangle(triangle);

        return drawingBoardLayer;
    }

    public static SvgDrawingBoardLayer DrawShapes(this SvgDrawingBoardLayer drawingBoardLayer, IEnumerable<IFiniteGeometricShape2D> shapesList)
    {
        foreach (var shape in shapesList)
        {
            var lineSegment = shape as ILineSegment2D;
            if (!ReferenceEquals(lineSegment, null))
            {
                drawingBoardLayer.DrawLineSegment(lineSegment);
                continue;
            }

            var triangle = shape as ITriangle2D;
            if (!ReferenceEquals(triangle, null))
            {
                drawingBoardLayer.DrawTriangle(triangle);
                continue;
            }
        }

        return drawingBoardLayer;
    }


    /// <summary>
    /// Draw a straight line segment between two points
    /// </summary>
    /// <param name="drawingLayer"></param>
    /// <param name="point1"></param>
    /// <param name="point2"></param>
    /// <returns></returns>
    public static SvgDrawingBoardLayer DrawLineSegment(this SvgDrawingBoardLayer drawingLayer, ILinFloat64Vector2D point1, ILinFloat64Vector2D point2)
    {
        drawingLayer.DrawLineSegment(
            point1.X,
            point1.Y,
            point2.X,
            point2.Y
        );

        return drawingLayer;
    }

    /// <summary>
    /// Draw a straight line segment between two points
    /// </summary>
    /// <param name="drawingLayer"></param>
    /// <param name="lineSegment"></param>
    /// <returns></returns>
    public static SvgDrawingBoardLayer DrawLineSegment(this SvgDrawingBoardLayer drawingLayer, ILineSegment2D lineSegment)
    {
        drawingLayer.DrawLineSegment(
            lineSegment.Point1X,
            lineSegment.Point1Y,
            lineSegment.Point2X,
            lineSegment.Point2Y
        );

        return drawingLayer;
    }

    /// <summary>
    /// Draw straight line segments
    /// </summary>
    /// <param name="drawingLayer"></param>
    /// <param name="lineSegmentsList"></param>
    /// <returns></returns>
    public static SvgDrawingBoardLayer DrawLineSegments(this SvgDrawingBoardLayer drawingLayer, IEnumerable<ILineSegment2D> lineSegmentsList)
    {
        foreach (var lineSegment in lineSegmentsList)
            drawingLayer.DrawLineSegment(
                lineSegment.Point1X,
                lineSegment.Point1Y,
                lineSegment.Point2X,
                lineSegment.Point2Y
            );

        return drawingLayer;
    }


    /// <summary>
    /// Draw a circle marker in the active layer of this drawing board
    /// </summary>
    /// <param name="drawingLayer"></param>
    /// <param name="center"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static SvgDrawingBoardLayer DrawCircle(this SvgDrawingBoardLayer drawingLayer, ILinFloat64Vector2D center, double radius)
    {
        drawingLayer.DrawCircle(
            center.X,
            center.Y,
            radius
        );

        return drawingLayer;
    }

    public static SvgDrawingBoardLayer DrawCircles(this SvgDrawingBoardLayer drawingLayer, IEnumerable<ILinFloat64Vector2D> circleCentersList, double radius)
    {
        foreach (var center in circleCentersList)
            drawingLayer.DrawCircle(
                center.X,
                center.Y,
                radius
            );

        return drawingLayer;
    }

    public static SvgDrawingBoardLayer DrawCircleMarker(this SvgDrawingBoardLayer drawingLayer, ILinFloat64Vector2D center, int pixelsRadius)
    {
        drawingLayer.DrawCircleMarker(
            center.X,
            center.Y,
            pixelsRadius
        );

        return drawingLayer;
    }

    public static SvgDrawingBoardLayer DrawCircleMarkers(this SvgDrawingBoardLayer drawingLayer, IEnumerable<ILinFloat64Vector2D> circleCentersList, int pixelRadius)
    {
        foreach (var center in circleCentersList)
            drawingLayer.DrawCircleMarker(
                center.X,
                center.Y,
                pixelRadius
            );

        return drawingLayer;
    }


    public static SvgDrawingBoardLayer DrawSquare(this SvgDrawingBoardLayer drawingLayer, ILinFloat64Vector2D center, double radius)
    {
        drawingLayer.DrawSquare(
            center.X,
            center.Y,
            radius
        );

        return drawingLayer;
    }

    public static SvgDrawingBoardLayer DrawSquares(this SvgDrawingBoardLayer drawingLayer, IEnumerable<ILinFloat64Vector2D> circleCentersList, double radius)
    {
        foreach (var center in circleCentersList)
            drawingLayer.DrawSquare(
                center.X,
                center.Y,
                radius
            );

        return drawingLayer;
    }

    public static SvgDrawingBoardLayer DrawSquareMarker(this SvgDrawingBoardLayer drawingLayer, ILinFloat64Vector2D center, int pixelsRadius)
    {
        drawingLayer.DrawSquareMarker(
            center.X,
            center.Y,
            pixelsRadius
        );

        return drawingLayer;
    }

    public static SvgDrawingBoardLayer DrawSquareMarkers(this SvgDrawingBoardLayer drawingLayer, IEnumerable<ILinFloat64Vector2D> circleCentersList, int pixelRadius)
    {
        foreach (var center in circleCentersList)
            drawingLayer.DrawSquareMarker(
                center.X,
                center.Y,
                pixelRadius
            );

        return drawingLayer;
    }


    public static SvgDrawingBoardLayer DrawRectangle(this SvgDrawingBoardLayer drawingLayer, ILinFloat64Vector2D point1, ILinFloat64Vector2D point2)
    {
        drawingLayer.DrawRectangle(
            point1.X,
            point1.Y,
            point2.X,
            point2.Y
        );

        return drawingLayer;
    }

    public static SvgDrawingBoardLayer DrawRectangle(this SvgDrawingBoardLayer drawingLayer, IBoundingBox2D boundingBox)
    {
        drawingLayer.DrawRectangle(
            boundingBox.MinX,
            boundingBox.MinY,
            boundingBox.MaxX,
            boundingBox.MaxY
        );

        return drawingLayer;
    }


    public static SvgDrawingBoardLayer DrawLine(this SvgDrawingBoardLayer drawingLayer, ILine2D line)
    {
        var lineSegment =
            drawingLayer
                .ParentDrawingBoard
                .GetViewBox()
                .ClipLine(line);

        return drawingLayer.DrawLineSegment(lineSegment);
    }

    public static SvgDrawingBoardLayer DrawLine(this SvgDrawingBoardLayer drawingLayer, ILine2D line, double lineParamMinValue, double lineParamMaxValue = double.PositiveInfinity)
    {
        var lineSegment =
            drawingLayer
                .ParentDrawingBoard
                .GetViewBox()
                .ClipLine(line, lineParamMinValue, lineParamMaxValue);

        return ReferenceEquals(lineSegment, null)
            ? drawingLayer
            : drawingLayer.DrawLineSegment(lineSegment);
    }


    public static SvgDrawingBoardLayer DrawTriangle(this SvgDrawingBoardLayer drawingLayer, ITriangle2D triangle)
    {
        drawingLayer.DrawPolygon(
            triangle.GetPoint1(),
            triangle.GetPoint2(),
            triangle.GetPoint3()
        );

        return drawingLayer;
    }

    public static SvgDrawingBoardLayer DrawTriangles(this SvgDrawingBoardLayer drawingLayer, IEnumerable<ITriangle2D> trianglesList)
    {
        foreach (var triangle in trianglesList)
            drawingLayer.DrawPolygon(
                triangle.GetPoint1(),
                triangle.GetPoint2(),
                triangle.GetPoint3()
            );

        return drawingLayer;
    }


    public static SvgDrawingBoardLayer DrawText(this SvgDrawingBoardLayer drawingLayer, string text, ILinFloat64Vector2D position)
    {
        drawingLayer.DrawText(text, position.X, position.Y);

        return drawingLayer;
    }


    public static SvgDrawingBoardLayer DrawBoundingBox(this SvgDrawingBoardLayer drawingLayer, IBoundingBox2D boundingBox)
    {
        var boardViewBox =
            drawingLayer.ParentDrawingBoard.GetViewBox();

        drawingLayer.DrawRectangle(
            Math.Max(boundingBox.MinX, boardViewBox.MinX),
            Math.Max(boundingBox.MinY, boardViewBox.MinY),
            Math.Min(boundingBox.MaxX, boardViewBox.MaxX),
            Math.Min(boundingBox.MaxY, boardViewBox.MaxY)
        );

        return drawingLayer;
    }


    #endregion

}