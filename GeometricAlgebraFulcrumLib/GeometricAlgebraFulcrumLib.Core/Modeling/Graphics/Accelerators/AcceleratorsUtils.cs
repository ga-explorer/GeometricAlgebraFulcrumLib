using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Accelerators.BIH.Space2D;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Accelerators.Grids;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Accelerators.Grids.Space2D;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Svg.DrawingBoard;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Tuples;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Accelerators;

public static class AcceleratorsUtils
{

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
}