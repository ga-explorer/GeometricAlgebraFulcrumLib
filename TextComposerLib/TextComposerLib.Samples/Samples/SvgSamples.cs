using System.Drawing;

namespace TextComposerLib.Samples.Samples
{
    internal static class SvgSamples
    {
    //    internal static string Task1()
    //    {
    //        //Construct SVG elements tree inside a root svg element
    //        var svg = SvgElementSvg
    //            .CreateRoot()
    //            .SetCanvasSize(640, 480, SvgValueLengthUnit.Pixels);

    //        //We can set properties of an element using dot cascade of methods as shown
    //        svg.ViewBox.SetTo(0, 0, 640, 480);

    //        //We can add child elements and contents (text and comments for example) in a similar way
    //        svg.Contents
    //            .AppendTitle("SVG Sample 1")
    //            .Append(SvgElementGroup.Create("group1"));

    //        //We can find a reference to an element in the tree using its id
    //        var group1 = svg.Contents.GetDescendantElement("group1") as SvgElementGroup;

    //        //We can use attribute values of elements to define attribute values of new elements
    //        group1?
    //            .Contents
    //            .Append(
    //                SvgElementRectangle
    //                    .Create()
    //                    .SetRectangle(0, 0, svg.ViewBox.Width, svg.ViewBox.Height)
    //                    .SetRadii(32, 32)
    //            )
    //            .Append(
    //                SvgElementCircle
    //                    .Create("circle1")
    //                    .SetCircle(320, 240, 128)
    //            );

    //        //We can change the style of any element as we need
    //        group1?
    //            .Style
    //            .Stroke.SetToRgb(Color.Black)
    //            .StrokeWidth.SetTo(2)
    //            .StrokeDashArray.SetToText(SvgValueLengthsList.Create(4, 2))
    //            //.StrokeDashArray.SetTo("4 2") //this is also good
    //            .Fill.SetToRgb(Color.White);

    //        var circle1 = group1?.Contents.GetDescendantElement("circle1") as SvgElementCircle;
    //        circle1?
    //            .Style
    //            .Stroke.SetToRgb(Color.Blue)
    //            .Fill.SetToRgb(Color.GreenYellow);

    //        return new SvgComposer()
    //            .AppendSvgFileHeader()
    //            .AppendTag(svg)
    //            .ToString();
    //    }

    //    internal static string Task2()
    //    {
    //        //Initialize grid parameters using an SVG square grid composer
    //        var gridComposer = new SvgGcSquareGrid
    //        {
    //            CenterX = 0,
    //            CenterY = 0,
    //            UnitSize = 100,
    //            SubUnitsPerUnit = 2,
    //            SubSubUnitsPerSubUnit = 5,
    //            EnableSubSubUnitLines = true,
    //            EnableSubUnitLines = true,
    //            LeftUnitsCount = 5,
    //            RightUnitsCount = 5,
    //            UpperUnitsCount = 5,
    //            LowerUnitsCount = 5
    //        };

    //        //Set root group element id
    //        gridComposer.ElementsIDs.ElementId = "squareGrid";

    //        //Style the composed grid components
    //        //Set background style
    //        gridComposer
    //            .GridElementsStyler
    //            .BackgroundAreaStyle
    //            .Paint.SetToRgb(Color.White);

    //        //Set sub-unit lines style
    //        gridComposer
    //            .GridElementsStyler
    //            .SubSubUnitLinesStyle
    //            .Paint.SetToRgb(Color.DarkOrange.LerpToWhite(2.0d / 3.0d))
    //            .StrokeWidth.SetTo(1);

    //        //Set sub-unit lines style
    //        gridComposer
    //            .GridElementsStyler
    //            .SubUnitLinesStyle
    //            .Paint.SetToRgb(Color.DarkOrange.LerpToWhite(1.0d / 3.0d))
    //            .StrokeWidth.SetTo(1);

    //        //Set unit lines style
    //        gridComposer
    //            .GridElementsStyler
    //            .UnitLinesStyle
    //            .Paint.SetToRgb(Color.DarkOrange)
    //            .StrokeWidth.SetTo(1);

    //        //Set axis lines style
    //        gridComposer
    //            .GridElementsStyler
    //            .AxisLinesStyle
    //            .Paint.SetToRgb(Color.DarkOrange.LerpToBlack(0.5))
    //            .StrokeWidth.SetTo(3);

    //        //Set border lines style
    //        gridComposer
    //            .GridElementsStyler
    //            .BorderLinesStyle
    //            .Paint.SetToRgb(Color.Black)
    //            .StrokeWidth.SetTo(5);

    //        //Compose the grid
    //        var gridGroup = gridComposer.ComposeGrid(true);


    //        //Create and return final SVG code
    //        var svg = SvgElementSvg
    //            .CreateRoot()
    //            .SetCanvasSize(1001, 1001, SvgValueLengthUnit.Pixels)
    //            .ViewBox.SetTo(-500, -500, 1001, 1001);

    //        svg.Contents
    //            .AppendTitle("SVG Sample 2")
    //            .Append(gridGroup);

    //        return new SvgComposer()
    //            .AppendSvgFileHeader()
    //            .AppendTag(svg)
    //            .ToString();
    //    }
    }
}