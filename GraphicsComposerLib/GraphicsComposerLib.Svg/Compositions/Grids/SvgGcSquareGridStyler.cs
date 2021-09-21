using GraphicsComposerLib.Svg.Elements;
using GraphicsComposerLib.Svg.Elements.Containers;
using GraphicsComposerLib.Svg.Styles.SubStyles;

namespace GraphicsComposerLib.Svg.Compositions.Grids
{
    public sealed class SvgGcSquareGridStyler : ISvgGeometryComposerStyler
    {
        public SvgElementGroup ComposedGridElement { get; private set; }

        public SvgGcSquareGridIDs ComposedGridElementsIDs { get; private set; }

        public SvgElement ComposedElement 
            => ComposedGridElement;

        public ISvgGeometryComposerIDs ComposedElementsIDs
            => ComposedGridElementsIDs;


        public SvgSubStyleFill BackgroundAreaStyle { get; } 
            = SvgSubStyleFill.Create();

        public SvgSubStyleStroke SubSubUnitLinesStyle { get; } 
            = SvgSubStyleStroke.Create();

        public SvgSubStyleStroke SubUnitLinesStyle { get; }
            = SvgSubStyleStroke.Create();

        public SvgSubStyleStroke UnitLinesStyle { get; }
            = SvgSubStyleStroke.Create();

        public SvgSubStyleStroke AxisLinesStyle { get; }
            = SvgSubStyleStroke.Create();

        public SvgSubStyleStroke BorderLinesStyle { get; }
            = SvgSubStyleStroke.Create();


        public SvgGcSquareGridStyler()
        {
        }

        public SvgGcSquareGridStyler(SvgGcSquareGridIDs gridElementIDs)
        {
            ComposedGridElementsIDs = gridElementIDs;
        }

        public SvgGcSquareGridStyler(SvgElementGroup gridElement, SvgGcSquareGridIDs gridElementIDs)
        {
            ComposedGridElement = gridElement;
            ComposedGridElementsIDs = gridElementIDs;
        }


        public SvgGcSquareGridStyler SelectGrid(SvgElementGroup gridElement)
        {
            ComposedGridElement = gridElement;

            return this;
        }

        public SvgGcSquareGridStyler SelectGrid(SvgElementGroup gridElement, SvgGcSquareGridIDs gridElementIDs)
        {
            ComposedGridElement = gridElement;
            ComposedGridElementsIDs = gridElementIDs;

            return this;
        }


        public SvgElement ApplyStyles()
        {
            //Read main components into a dictionary for ease of reference
            var elementsDict = ComposedGridElement.GetElementsDictionary();

            //Set background style
            BackgroundAreaStyle.UpdateElementStyle(
                elementsDict.GetSvgElement(ComposedGridElementsIDs.BackgroundAreaId)
            );

            //This is also ok
            //elementsDict
            //    .GetSvgElement(ComposedGridElementsIDs.BackgroundAreaId)?
            //    .UpdateStyleFrom(BackgroundAreaStyle);

            //Set sub-sub-unit lines style
            elementsDict
                .GetSvgElement(ComposedGridElementsIDs.SubSubUnitLinesId)?
                .UpdateStyleFrom(SubSubUnitLinesStyle);

            //Set sub-unit lines style
            elementsDict
                .GetSvgElement(ComposedGridElementsIDs.SubUnitLinesId)?
                .UpdateStyleFrom(SubUnitLinesStyle);

            //Set unit lines style
            elementsDict
                .GetSvgElement(ComposedGridElementsIDs.UnitLinesId)?
                .UpdateStyleFrom(UnitLinesStyle);

            //Set axes style
            elementsDict
                .GetSvgElement(ComposedGridElementsIDs.AxisLinesId)?
                .UpdateStyleFrom(AxisLinesStyle);

            //Set border style
            elementsDict
                .GetSvgElement(ComposedGridElementsIDs.BorderLinesId)?
                .UpdateStyleFrom(BorderLinesStyle);

            return ComposedGridElement;
        }
    }
}
