using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using NumericalGeometryLib.Accelerators.BIH.Space2D;
using NumericalGeometryLib.Accelerators.Grids;
using NumericalGeometryLib.Accelerators.Grids.Space2D;
using WebComposerLib.Svg.Styles;

namespace GraphicsComposerLib.Rendering.Svg.DrawingBoard
{
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
        public static SvgDrawingBoardLayer DrawLineSegment(this SvgDrawingBoardLayer drawingLayer, IFloat64Tuple2D point1, IFloat64Tuple2D point2)
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
        public static SvgDrawingBoardLayer DrawCircle(this SvgDrawingBoardLayer drawingLayer, IFloat64Tuple2D center, double radius)
        {
            drawingLayer.DrawCircle(
                center.X,
                center.Y,
                radius
            );

            return drawingLayer;
        }

        public static SvgDrawingBoardLayer DrawCircles(this SvgDrawingBoardLayer drawingLayer, IEnumerable<IFloat64Tuple2D> circleCentersList, double radius)
        {
            foreach (var center in circleCentersList)
                drawingLayer.DrawCircle(
                    center.X,
                    center.Y,
                    radius
                );

            return drawingLayer;
        }

        public static SvgDrawingBoardLayer DrawCircleMarker(this SvgDrawingBoardLayer drawingLayer, IFloat64Tuple2D center, int pixelsRadius)
        {
            drawingLayer.DrawCircleMarker(
                center.X,
                center.Y,
                pixelsRadius
            );

            return drawingLayer;
        }

        public static SvgDrawingBoardLayer DrawCircleMarkers(this SvgDrawingBoardLayer drawingLayer, IEnumerable<IFloat64Tuple2D> circleCentersList, int pixelRadius)
        {
            foreach (var center in circleCentersList)
                drawingLayer.DrawCircleMarker(
                    center.X,
                    center.Y,
                    pixelRadius
                );

            return drawingLayer;
        }


        public static SvgDrawingBoardLayer DrawSquare(this SvgDrawingBoardLayer drawingLayer, IFloat64Tuple2D center, double radius)
        {
            drawingLayer.DrawSquare(
                center.X,
                center.Y,
                radius
            );

            return drawingLayer;
        }

        public static SvgDrawingBoardLayer DrawSquares(this SvgDrawingBoardLayer drawingLayer, IEnumerable<IFloat64Tuple2D> circleCentersList, double radius)
        {
            foreach (var center in circleCentersList)
                drawingLayer.DrawSquare(
                    center.X,
                    center.Y,
                    radius
                );

            return drawingLayer;
        }

        public static SvgDrawingBoardLayer DrawSquareMarker(this SvgDrawingBoardLayer drawingLayer, IFloat64Tuple2D center, int pixelsRadius)
        {
            drawingLayer.DrawSquareMarker(
                center.X,
                center.Y,
                pixelsRadius
            );

            return drawingLayer;
        }

        public static SvgDrawingBoardLayer DrawSquareMarkers(this SvgDrawingBoardLayer drawingLayer, IEnumerable<IFloat64Tuple2D> circleCentersList, int pixelRadius)
        {
            foreach (var center in circleCentersList)
                drawingLayer.DrawSquareMarker(
                    center.X,
                    center.Y,
                    pixelRadius
                );

            return drawingLayer;
        }


        public static SvgDrawingBoardLayer DrawRectangle(this SvgDrawingBoardLayer drawingLayer, IFloat64Tuple2D point1, IFloat64Tuple2D point2)
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


        public static SvgDrawingBoardLayer DrawText(this SvgDrawingBoardLayer drawingLayer, string text, IFloat64Tuple2D position)
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


        public static SvgDrawingBoard DrawGrid(this SvgDrawingBoard drawingBoard, IAccGrid2D<ILineSegment2D> grid)
        {
            return drawingBoard.DrawGrid(grid, new AccGridDrawingSettings2D());
        }

        public static SvgDrawingBoard DrawGrid(this SvgDrawingBoard drawingBoard, IAccGrid2D<ILineSegment2D> grid, AccGridDrawingSettings2D drawingSettings)
        {
            //Draw grid lines on a separate layer
            if (drawingSettings.DrawGridLines)
            {
                var cellLengthX = grid.GetCellLengthX();
                var cellLengthY = grid.GetCellLengthY();

                drawingBoard.AddFrontLayer("Grid Lines");
                drawingBoard.ActiveLayer.ActiveStyle =
                    SvgDrawingBoardLayerActiveStyle.Default;

                drawingBoard
                    .ActiveLayer
                    .SetDefaultPen(1, drawingSettings.GridLinesColor);

                for (var ix = 0; ix <= grid.CellsCountX; ix++)
                {
                    var x = grid.BoundingBox.MinX + ix * cellLengthX;

                    drawingBoard.ActiveLayer.DrawLineSegment(
                        x, grid.BoundingBox.MinY,
                        x, grid.BoundingBox.MaxY
                    );
                }

                for (var iy = 0; iy <= grid.CellsCountY; iy++)
                {
                    var y = grid.BoundingBox.MinY + iy * cellLengthY;

                    drawingBoard.ActiveLayer.DrawLineSegment(
                        grid.BoundingBox.MinX, y,
                        grid.BoundingBox.MaxX, y
                    );
                }
            }

            //Draw active grid cells on a separate layer
            if (drawingSettings.DrawActiveCells)
            {
                var gridInfo = AccGridInfo2D.Create(grid);

                drawingBoard.AddFrontLayer("Active Cells");
                drawingBoard.ActiveLayer.ActiveStyle =
                    SvgDrawingBoardLayerActiveStyle.Default;

                drawingBoard
                    .ActiveLayer
                    .SetDefaultPen(2, drawingSettings.GridLinesColor)
                    .SetDefaultFill(
                        drawingSettings.ActiveCellsFillColor,
                        drawingSettings.ActiveCellsFillOpacity
                    );

                foreach (var gridCellInfo in gridInfo.NonEmptyGridCells)
                    drawingBoard
                        .ActiveLayer
                        .DrawBoundingBox(gridCellInfo.BoundingBox);
            }

            //Draw geometric objects on a separate layer
            if (drawingSettings.DrawGeometricObjects)
            {
                drawingBoard.AddFrontLayer("Geometric Objects");
                drawingBoard.ActiveLayer.ActiveStyle =
                    SvgDrawingBoardLayerActiveStyle.Default;

                drawingBoard
                    .ActiveLayer
                    .SetDefaultPen(1, drawingSettings.GeometricObjectsColor)
                    .DrawLineSegments(grid);
            }

            return drawingBoard;
        }

        public static SvgDrawingBoard DrawGridCells(this SvgDrawingBoard drawingBoard, IAccGrid2D<ILineSegment2D> grid, IEnumerable<IntTuple2D> cellIndicesList, AccGridDrawingSettings2D drawingSettings)
        {
            //Draw selected cells
            drawingBoard
                .ActiveLayer
                .SetPen(2, drawingSettings.GridLinesColor)
                .SetFill(
                    drawingSettings.ActiveCellsFillColor,
                    drawingSettings.ActiveCellsFillOpacity
                );

            foreach (var cellIndex in cellIndicesList)
                drawingBoard.ActiveLayer.DrawBoundingBox(
                    grid.GetCellBoundingBox(
                        cellIndex.X,
                        cellIndex.Y
                    )
                );


            ////Draw geometric objects
            //if (drawingSettings.DrawGeometricObjects)
            //    drawingBoard
            //        .SetPen(1, drawingSettings.GeometricObjectsColor)
            //        .DrawLineSegments(grid);

            return drawingBoard;
        }

        public static SvgDrawingBoard DrawGridLineTraversal(this SvgDrawingBoard drawingBoard, AccGridLineTraverser2D traversalData, AccGridDrawingSettings2D drawingSettings)
        {
            //Draw traversed cells
            drawingBoard
                .ActiveLayer
                .SetPen(2, drawingSettings.GridLinesColor)
                .SetFill(
                    drawingSettings.ActiveCellsFillColor,
                    drawingSettings.ActiveCellsFillOpacity
                );

            foreach (var cellIndex in traversalData.GetCellIndices())
                drawingBoard.ActiveLayer.DrawBoundingBox(
                    traversalData.Grid.GetCellBoundingBox(
                        cellIndex.X,
                        cellIndex.Y
                    )
                );

            //Draw traversing line
            drawingBoard
                .ActiveLayer
                .SetPen(2, drawingSettings.GeometricObjectsColor, 12, 6)
                .DrawLine(traversalData.Line);

            drawingBoard
                .ActiveLayer
                .SetPen(1, drawingSettings.MarkerLineColor);

            var p1 = traversalData.Line.GetOrigin();
            var p2 = traversalData.Line.GetDirection() + p1;

            if (drawingBoard.IsPointVisible(p1.X, p1.Y))
                drawingBoard
                    .ActiveLayer
                    .SetFill(Color.Red)
                    .DrawCircleMarker(
                        p1.X,
                        p1.Y,
                        drawingSettings.MarkerPixelSize
                    );

            if (drawingBoard.IsPointVisible(p2.X, p2.Y))
                drawingBoard
                    .ActiveLayer
                    .SetFill(Color.Blue)
                    .DrawCircleMarker(
                        p2.X,
                        p2.Y,
                        drawingSettings.MarkerPixelSize
                    );

            return drawingBoard;
        }


        public static SvgDrawingBoard DrawBihNodeInfo(this SvgDrawingBoard drawingBoard, AccBihNodeInfo2D bihNodeInfo)
        {
            var node = bihNodeInfo.Node;

            //Draw bounding box of node in a separate layer
            if (bihNodeInfo.HasBoundingBox)
            {
                drawingBoard.AddFrontLayer("Node Bounding Box");
                drawingBoard.ActiveLayer.ActiveStyle =
                    SvgDrawingBoardLayerActiveStyle.Default;

                if (bihNodeInfo.IsLeaf)
                    drawingBoard
                        .ActiveLayer
                        .SetDefaultFill(Color.DarkGreen, 0.1);
                else
                    drawingBoard
                        .ActiveLayer
                        .SetDefaultFill(Color.Black, 0);

                drawingBoard
                    .ActiveLayer
                    .SetDefaultPen(2, Color.Black)
                    .DrawBoundingBox(bihNodeInfo.BoundingBox);
            }

            //Draw geometric objects of this leaf nodes in a separate layer
            drawingBoard.AddFrontLayer("Node Geometric Objects");

            if (bihNodeInfo.IsLeaf)
            {
                drawingBoard.ActiveLayer.ActiveStyle =
                    SvgDrawingBoardLayerActiveStyle.Default;

                drawingBoard
                    .ActiveLayer
                    .SetDefaultPen(2, Color.DarkGreen);

                foreach (var shape in node.Contents)
                    drawingBoard
                        .ActiveLayer
                        .DrawShape(shape);
            }
            else
            {
                drawingBoard.ActiveLayer.ActiveStyle =
                    SvgDrawingBoardLayerActiveStyle.Current;

                //Draw geometric objects of this node's children
                foreach (var shape in node.Contents)
                {
                    var inLeftChild =
                        node.HasLeftChild && node.LeftChild.Contains(shape);

                    var inRightChild =
                        node.HasRightChild && node.RightChild.Contains(shape);

                    Color penColor;
                    //If geometric object is in both children, this is an error
                    if (inLeftChild && inRightChild)
                        penColor = Color.Yellow;
                    //Geometric object in left child only
                    else if (inLeftChild)
                        penColor = Color.Blue;
                    //Geometric object in right child only
                    else if (inRightChild)
                        penColor = Color.Red;
                    //Geometric object is in neither children, this is an error
                    else
                        penColor = Color.White;

                    drawingBoard
                        .ActiveLayer
                        .SetPen(3, penColor)
                        .DrawShape(shape);
                }

                //Draw bounding boxes of left and right child nodes
                var leftChildInfo = bihNodeInfo.GetLeftChildInfo();
                if (bihNodeInfo.HasLeftChild && leftChildInfo.HasBoundingBox)
                    drawingBoard
                        .ActiveLayer
                        .SetPen(1, Color.Red)
                        .SetFill(Color.Red, 0.15)
                        .DrawBoundingBox(leftChildInfo.BoundingBox);

                var rightChildInfo = bihNodeInfo.GetRightChildInfo();
                if (bihNodeInfo.HasRightChild && rightChildInfo.HasBoundingBox)
                    drawingBoard
                        .ActiveLayer
                        .SetPen(1, Color.Blue)
                        .SetFill(Color.Blue, 0.15)
                        .DrawBoundingBox(rightChildInfo.BoundingBox);
            }

            return drawingBoard;
        }
        #endregion

    }
}