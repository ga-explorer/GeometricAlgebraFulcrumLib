using WebComposerLib.Svg.Elements;
using WebComposerLib.Svg.Elements.Containers;
using WebComposerLib.Svg.Elements.Shape;

namespace WebComposerLib.Svg.Compositions.Grids
{
    public class SvgGcSquareGrid : ISvgGeometryComposer
    {
        public SvgGcSquareGridIDs GridElementsIDs { get; } 
            = new SvgGcSquareGridIDs();

        public ISvgGeometryComposerIDs ElementsIDs 
            => GridElementsIDs;

        private SvgGcSquareGridStyler _gridElementsStyler;
        public SvgGcSquareGridStyler GridElementsStyler
        {
            get =>
                _gridElementsStyler 
                ?? (_gridElementsStyler = new SvgGcSquareGridStyler());
            set => _gridElementsStyler = value;
        }

        public ISvgGeometryComposerStyler ElementsStyler 
            => GridElementsStyler;

        public double CenterX { get; set; } = 0;

        public double CenterY { get; set; } = 0;

        public double UnitSize { get; set; } = 10;

        public int SubUnitsPerUnit { get; set; } = 2;

        public int SubSubUnitsPerSubUnit { get; set; } = 5;

        public int SubSubUnitsPerUnit 
            => SubSubUnitsPerSubUnit * SubUnitsPerUnit;

        public int LeftUnitsCount { get; set; } = 5;

        public int RightUnitsCount { get; set; } = 5;

        public int LowerUnitsCount { get; set; } = 5;

        public int UpperUnitsCount { get; set; } = 5;

        public bool EnableBackground { get; set; } = true;

        public bool EnableUnitLines { get; set; } = true;

        public bool EnableSubUnitLines { get; set; } = true;

        public bool EnableSubSubUnitLines { get; set; } = true;

        public bool EnableAxes { get; set; } = true;

        public bool EnableBorder { get; set; } = true;

        public SvgElement Element { get; private set; }

        public int VerticalUnitLinesCount
            => LeftUnitsCount + RightUnitsCount + 1;

        public int HorizontalUnitLinesCount
            => LowerUnitsCount + UpperUnitsCount + 1;

        public int VerticalUnitsCount
            => LeftUnitsCount + RightUnitsCount;

        public int HorizontalUnitsCount
            => LowerUnitsCount + UpperUnitsCount;

        public int MinVerticalUnitLineIndex 
            => -LeftUnitsCount;

        public int MaxVerticalUnitLineIndex
            => RightUnitsCount;

        public int MinHorizontalUnitLineIndex
            => -LowerUnitsCount;

        public int MaxHorizontalUnitLineIndex
            => UpperUnitsCount;

        public int MinVerticalSubUnitLineIndex
            => -LeftUnitsCount * SubUnitsPerUnit;

        public int MaxVerticalSubUnitLineIndex
            => RightUnitsCount * SubUnitsPerUnit;

        public int MinHorizontalSubUnitLineIndex
            => -LowerUnitsCount * SubUnitsPerUnit;

        public int MaxHorizontalSubUnitLineIndex
            => UpperUnitsCount * SubUnitsPerUnit;

        public int MinVerticalSubSubUnitLineIndex
            => -LeftUnitsCount * SubSubUnitsPerUnit;

        public int MaxVerticalSubSubUnitLineIndex
            => RightUnitsCount * SubSubUnitsPerUnit;

        public int MinHorizontalSubSubUnitLineIndex
            => -LowerUnitsCount * SubSubUnitsPerUnit;

        public int MaxHorizontalSubSubUnitLineIndex
            => UpperUnitsCount * SubSubUnitsPerUnit;

        public double MinX 
            => CenterX - LeftUnitsCount * UnitSize;

        public double MaxX
            => CenterX + RightUnitsCount * UnitSize;

        public double MinY
            => CenterY - LowerUnitsCount * UnitSize;

        public double MaxY
            => CenterY + UpperUnitsCount * UnitSize;

        public double Width
            => VerticalUnitsCount * UnitSize;

        public double Height
            => HorizontalUnitsCount * UnitSize;


        public double GetUnitLineX(int lineIndex)
        {
            return CenterX + lineIndex * UnitSize;
        }

        public double GetUnitLineY(int lineIndex)
        {
            return CenterY + lineIndex * UnitSize;
        }

        public double GetSubUnitLineX(int lineIndex)
        {
            return CenterX + lineIndex * UnitSize / SubUnitsPerUnit;
        }

        public double GetSubUnitLineY(int lineIndex)
        {
            return CenterY + lineIndex * UnitSize / SubUnitsPerUnit;
        }

        public double GetSubSubUnitLineX(int lineIndex)
        {
            return CenterX + lineIndex * UnitSize / SubSubUnitsPerUnit;
        }

        public double GetSubSubUnitLineY(int lineIndex)
        {
            return CenterY + lineIndex * UnitSize / SubSubUnitsPerUnit;
        }


        private SvgElementGroup ComposeBackground()
        {
            //Create grid background
            var gridBackgroundGroup = SvgElementGroup.Create(GridElementsIDs.BackgroundAreaId);
            gridBackgroundGroup
                .Contents
                .AppendTitle("Grid Background")
                .Append(
                    SvgElementRectangle
                        .Create()
                        .SetRectangle(MinX, MinY, Width, Height)
                );

            return gridBackgroundGroup;
        }

        private SvgElementGroup ComposeSubSubUnitLines()
        {
            //TODO: Change the implementation to use def and use

            //Create grid lines
            var gridLinesGroup = SvgElementGroup.Create(GridElementsIDs.SubSubUnitLinesId);
            gridLinesGroup.Contents.AppendTitle("Grid Sub-Sub-Unit Lines");

            //Create vertical lines
            for (var i = MinVerticalSubSubUnitLineIndex; i <= MaxVerticalSubSubUnitLineIndex; i++)
            {
                var x = GetSubSubUnitLineX(i);

                gridLinesGroup.Contents.Append(
                    SvgElementLine
                        .Create()
                        .SetLine(x, MinY, x, MaxY)
                );
            }

            //Create horizontal lines
            for (var j = MinHorizontalSubSubUnitLineIndex; j <= MaxHorizontalSubSubUnitLineIndex; j++)
            {
                var y = GetSubSubUnitLineY(j);

                gridLinesGroup.Contents.Append(
                    SvgElementLine
                        .Create()
                        .SetLine(MinX, y, MaxX, y)
                );
            }

            return gridLinesGroup;
        }

        private SvgElementGroup ComposeSubUnitLines()
        {
            //Create grid lines
            var gridLinesGroup = SvgElementGroup.Create(GridElementsIDs.SubUnitLinesId);
            gridLinesGroup.Contents.AppendTitle("Grid Sub-Unit Lines");

            //Create vertical lines
            for (var i = MinVerticalSubUnitLineIndex; i <= MaxVerticalSubUnitLineIndex; i++)
            {
                var x = GetSubUnitLineX(i);

                gridLinesGroup.Contents.Append(
                    SvgElementLine
                        .Create()
                        .SetLine(x, MinY, x, MaxY)
                );
            }

            //Create horizontal lines
            for (var j = MinHorizontalSubUnitLineIndex; j <= MaxHorizontalSubUnitLineIndex; j++)
            {
                var y = GetSubUnitLineY(j);

                gridLinesGroup.Contents.Append(
                    SvgElementLine
                        .Create()
                        .SetLine(MinX, y, MaxX, y)
                );
            }

            return gridLinesGroup;
        }

        private SvgElementGroup ComposeUnitLines()
        {
            //Create grid lines
            var gridLinesGroup = SvgElementGroup.Create(GridElementsIDs.UnitLinesId);
            gridLinesGroup.Contents.AppendTitle("Grid Unit Lines");

            //Create vertical lines
            for (var i = MinVerticalUnitLineIndex; i <= MaxVerticalUnitLineIndex; i++)
            {
                var x = GetUnitLineX(i);

                gridLinesGroup.Contents.Append(
                    SvgElementLine
                        .Create()
                        .SetLine(x, MinY, x, MaxY)
                );
            }

            //Create horizontal lines
            for (var j = MinHorizontalUnitLineIndex; j <= MaxHorizontalUnitLineIndex; j++)
            {
                var y = GetUnitLineY(j);

                gridLinesGroup.Contents.Append(
                    SvgElementLine
                        .Create()
                        .SetLine(MinX, y, MaxX, y)
                );
            }

            return gridLinesGroup;
        }

        private SvgElementGroup ComposeAxes()
        {
            //Create grid central axes
            var gridAxesGroup = SvgElementGroup.Create(GridElementsIDs.AxisLinesId);
            gridAxesGroup
                .Contents
                .AppendTitle("Grid Axes")
                .Append(
                    SvgElementLine
                        .Create()
                        .SetLine(MinX, CenterY, MaxX, CenterY)
                )
                .Append(
                    SvgElementLine
                        .Create()
                        .SetLine(CenterX, MinY, CenterX, MaxY)
                );

            return gridAxesGroup;
        }

        private SvgElementGroup ComposeBorder()
        {
            //Create grid border
            var gridBorderGroup = SvgElementGroup.Create(GridElementsIDs.BorderLinesId);
            gridBorderGroup
                .Contents
                .AppendTitle("Grid Border")
                .Append(
                    SvgElementRectangle
                        .Create()
                        .SetRectangle(MinX, MinY, Width, Height)
                );

            gridBorderGroup.Style.FillOpacity.SetToNumber(0);

            return gridBorderGroup;
        }


        public SvgElementGroup ComposeGrid(bool applyStyles)
        {
            //Create grid root element and insert all its components
            var gridGroup = SvgElementGroup.Create(GridElementsIDs.ElementId);
            Element = gridGroup;

            gridGroup.Contents.AppendTitle("Rectangular Grid");

            if (EnableBackground)
                gridGroup.Contents.Append(ComposeBackground());

            if (EnableSubSubUnitLines)
                gridGroup.Contents.Append(ComposeSubSubUnitLines());

            if (EnableSubUnitLines)
                gridGroup.Contents.Append(ComposeSubUnitLines());

            if (EnableUnitLines)
                gridGroup.Contents.Append(ComposeUnitLines());

            if (EnableAxes)
                gridGroup.Contents.Append(ComposeAxes());

            if (EnableBorder)
                gridGroup.Contents.Append(ComposeBorder());

            if (applyStyles)
            {
                GridElementsStyler?.SelectGrid(gridGroup, GridElementsIDs);
                GridElementsStyler?.ApplyStyles();
            }

            return gridGroup;
        }

        public SvgElement Compose(bool applyStyles)
        {
            return ComposeGrid(applyStyles);
        }


        public override string ToString()
        {
            return Compose(true).ToString();
        }
    }
}
