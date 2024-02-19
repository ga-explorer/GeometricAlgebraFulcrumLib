using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using TextComposerLib.Text;
using TextComposerLib.Text.Columns;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Parametric;
using TextComposerLib.Text.Structured;

namespace TextComposerLib.Code.JavaScript.Obsolete;

public static class JsCodeComponentUtils
{
    public static JavaScriptAttributesDictionary SetValue(this JavaScriptAttributesDictionary composer, string key, double value)
    {
        composer.SetTextValue(
            key, 
            value.ToString("G"), 
            "0"
        );

        return composer;
    }

    public static JavaScriptAttributesDictionary SetValue(this JavaScriptAttributesDictionary composer, string key, double value, double valueDefault)
    {
        composer.SetTextValue(
            key, 
            value.ToString("G"), 
            valueDefault.ToString("G")
        );

        return composer;
    }

    public static JavaScriptAttributesDictionary SetNumbersArrayValue(this JavaScriptAttributesDictionary composer, string key, IEnumerable<int> value, string valueDefault)
    {
        composer.SetTextValue(
            key, 
            value.ToJavaScriptNumbersArrayText(), 
            valueDefault
        );

        return composer;
    }

    public static JavaScriptAttributesDictionary SetNumbersArrayValue(this JavaScriptAttributesDictionary composer, string key, IEnumerable<double> value, string valueDefault)
    {
        composer.SetTextValue(
            key, 
            value.ToJavaScriptNumbersArrayText(), 
            valueDefault
        );

        return composer;
    }

    public static JavaScriptAttributesDictionary SetValue(this JavaScriptAttributesDictionary composer, string key, int value)
    {
        composer.SetTextValue(
            key, 
            value.ToString(), 
            "0"
        );

        return composer;
    }

    public static JavaScriptAttributesDictionary SetValue(this JavaScriptAttributesDictionary composer, string key, int value, int valueDefault)
    {
        composer.SetTextValue(
            key, 
            value.ToString(), 
            valueDefault.ToString()
        );

        return composer;
    }

    public static JavaScriptAttributesDictionary SetValue(this JavaScriptAttributesDictionary composer, string key, bool value)
    {
        composer.SetTextValue(key, value ? "true" : "false");

        return composer;
    }

    public static JavaScriptAttributesDictionary SetValue(this JavaScriptAttributesDictionary composer, string key, bool value, bool valueDefault)
    {
        composer.SetTextValue(
            key, 
            value ? "true" : "false",
            valueDefault ? "true" : "false"
        );

        return composer;
    }

    public static JavaScriptAttributesDictionary SetRgbHexValue(this JavaScriptAttributesDictionary composer, string key, Color value)
    {
        composer.SetTextValue(
            key,
            value.ToJavaScriptRgbHexText()
        );

        return composer;
    }

    public static JavaScriptAttributesDictionary SetRgbHexValue(this JavaScriptAttributesDictionary composer, string key, Color value, Color valueDefault)
    {
        composer.SetTextValue(
            key,
            value.ToJavaScriptRgbHexText(),
            valueDefault.ToJavaScriptRgbHexText()
        );

        return composer;
    }

    public static JavaScriptAttributesDictionary SetRgbNumbersArrayValue(this JavaScriptAttributesDictionary composer, string key, Color value)
    {
        composer.SetTextValue(
            key,
            value.ToJavaScriptRgbNumbersArrayText()
        );

        return composer;
    }

    public static JavaScriptAttributesDictionary SetRgbNumbersArrayValue(this JavaScriptAttributesDictionary composer, string key, Color value, Color valueDefault)
    {
        composer.SetTextValue(
            key,
            value.ToJavaScriptRgbNumbersArrayText(),
            valueDefault.ToJavaScriptRgbNumbersArrayText()
        );

        return composer;
    }

    public static JavaScriptAttributesDictionary SetRgbaNumbersArrayValue(this JavaScriptAttributesDictionary composer, string key, Color value, Color valueDefault)
    {
        composer.SetTextValue(
            key,
            value.ToJavaScriptRgbaNumbersArrayText(),
            valueDefault.ToJavaScriptRgbaNumbersArrayText()
        );

        return composer;
    }

    public static JavaScriptAttributesDictionary SetRgbaNumbersArrayValue(this JavaScriptAttributesDictionary composer, string key, Color value)
    {
        composer.SetTextValue(
            key,
            value.ToJavaScriptRgbaNumbersArrayText()
        );

        return composer;
    }

    public static JavaScriptAttributesDictionary SetValue(this JavaScriptAttributesDictionary composer, string key, JsCodeComponent value, JsCodeComponent valueDefault)
    {
        composer.SetTextValue(
            key, 
            value?.GetJavaScriptVariableNameOrCode() ?? string.Empty, 
            valueDefault?.GetJavaScriptVariableNameOrCode() ?? string.Empty
        );

        return composer;
    }

    public static JavaScriptAttributesDictionary SetValue(this JavaScriptAttributesDictionary composer, string key, JsCodeComponent value)
    {
        composer.SetTextValue(
            key, 
            value?.GetJavaScriptVariableNameOrCode() ?? string.Empty
        );

        return composer;
    }


    public static string ToJavaScriptObjectsArrayText(this IEnumerable<JsCodeComponentWithAttributes> elementsList)
    {
        var elementsTextArray = 
            elementsList
                .Select(s => s.ToString())
                .ToArray();

        if (elementsTextArray.Length == 0)
            return "[]";

        var composer = new LinearTextComposer();

        composer
            .AppendLine("[")
            .IncreaseIndentation()
            .AppendAtNewLine(
                elementsTextArray.Concatenate("," + Environment.NewLine)
            )
            .DecreaseIndentation()
            .Append("]");

        return composer.ToString();
    }


    #region Number Values Conversion
    public static string ToJavaScriptNumbersArrayText(this IEnumerable<int> numbersList)
        => numbersList
            .Select(n => n.ToString())
            .Concatenate(", ", "[", "]");

    public static string ToJavaScriptNumbersArrayText(this IEnumerable<int> numbersList, int valuesPerLine)
    {
        var textArray = new List<string>[valuesPerLine];

        for (var i = 0; i < valuesPerLine; i++)
            textArray[i] = new List<string>();

        var col = 0;
        foreach (var value in numbersList)
        {
            textArray[col].Add(value.ToString());

            col++;
            if (col >= valuesPerLine) col = 0;
        }

        var table = new TextColumnsComposer(textArray);

        return new LinearTextComposer()
            .AppendLine("[")
            .IncreaseIndentation()
            .AppendAtNewLine(table.ToString())
            .DecreaseIndentation()
            .AppendAtNewLine("]")
            .ToString();
    }

    public static string ToJavaScriptNumbersArrayText(this IEnumerable<int> numbersList, int valuesPerLine, string commentPrefix)
    {
        var textArray = new List<string>[valuesPerLine + 1];

        for (var i = 0; i < valuesPerLine; i++)
            textArray[i] = new List<string>();

        var col = 0;
        var row = 0;
        foreach (var value in numbersList)
        {
            textArray[col].Add(value.ToString());

            col++;
            if (col >= valuesPerLine)
            {
                textArray[valuesPerLine].Add( 
                    commentPrefix + row
                );

                col = 0;
                row++;
            }
        }

        var table = new TextColumnsComposer(textArray);

        return new LinearTextComposer()
            .AppendLine("[")
            .IncreaseIndentation()
            .AppendAtNewLine(table.ToString())
            .DecreaseIndentation()
            .AppendAtNewLine("]")
            .ToString();
    }

    public static string ToJavaScriptNumbersArrayText(this IEnumerable<double> numbersList)
    {
        return numbersList
            .Select(n => n.ToString("G"))
            .Concatenate(", ", "[", "]");
    }

    public static string ToJavaScriptNumbersArrayText(this IEnumerable<double> numbersList, int valuesPerLine)
    {
        var textArray = new List<string>[valuesPerLine];

        for (var i = 0; i < valuesPerLine; i++)
            textArray[i] = new List<string>();

        var col = 0;
        foreach (var value in numbersList)
        {
            textArray[col].Add(value.ToString("G"));

            col++;
            if (col >= valuesPerLine) col = 0;
        }

        var table = new TextColumnsComposer(textArray);

        return new LinearTextComposer()
            .AppendLine("[")
            .IncreaseIndentation()
            .AppendAtNewLine(table.ToString())
            .DecreaseIndentation()
            .AppendAtNewLine("]")
            .ToString();
    }

    public static string ToJavaScriptNumbersArrayText(this IEnumerable<double> numbersList, int valuesPerLine, string commentPrefix)
    {
        var textArray = new List<string>[valuesPerLine + 1];

        for (var i = 0; i < valuesPerLine; i++)
            textArray[i] = new List<string>();

        var col = 0;
        var row = 0;
        foreach (var value in numbersList)
        {
            textArray[col].Add(value.ToString("G"));

            col++;
            if (col >= valuesPerLine)
            {
                textArray[valuesPerLine].Add( 
                    commentPrefix + row
                );

                col = 0;
                row++;
            }
        }

        var table = new TextColumnsComposer(textArray);

        return new LinearTextComposer()
            .AppendLine("[")
            .IncreaseIndentation()
            .AppendAtNewLine(table.ToString())
            .DecreaseIndentation()
            .AppendAtNewLine("]")
            .ToString();
    }
    #endregion


    #region Color Values Conversion
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToJavaScriptRgbInteger(this Color color)
    {
        return color.R + (color.G << 8) + (color.B << 16);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToJavaScriptRgbHexText(this Color color)
    {
        var colorValue = color.R + (color.G << 8) + (color.B << 16);

        return $"0x{colorValue:x6}";
    }


    public static double[] ToJavaScriptRgbNumbersArray(this Color color)
    {
        const double d = 1.0d / 255;

        return new[]
        {
            d * color.R,
            d * color.G,
            d * color.B
        };
    }

    public static double[] ToJavaScriptRgbaNumbersArray(this Color color)
    {
        const double d = 1.0d / 255;

        return new[]
        {
            d * color.R,
            d * color.G,
            d * color.B,
            d * color.A
        };
    }

    public static double[] ToJavaScriptRgbNumbersArray(this IEnumerable<Color> colorsList)
    {
        var numbersList = new List<double>();

        const double d = 1.0d / 255;
        var i = 0;
        foreach (var color in colorsList)
        {
            numbersList[i] = d * color.R;
            numbersList[i + 1] = d * color.G;
            numbersList[i + 2] = d * color.B;

            i += 3;
        }

        return numbersList.ToArray();
    }

    public static double[] ToJavaScriptRgbaNumbersArray(this IEnumerable<Color> colorsList)
    {
        var numbersList = new List<double>();

        const double d = 1.0d / 255;
        var i = 0;
        foreach (var color in colorsList)
        {
            numbersList[i] = d * color.R;
            numbersList[i + 1] = d * color.G;
            numbersList[i + 2] = d * color.B;
            numbersList[i + 3] = d * color.A;

            i += 4;
        }

        return numbersList.ToArray();
    }


    public static string ToJavaScriptRgbNumbersArrayText(this Color color)
    {
        const double d = 1.0d / 255;

        return new StringBuilder()
            .Append('[')
            .Append((d * color.R).ToString("G"))
            .Append(',')
            .Append((d * color.G).ToString("G"))
            .Append(',')
            .Append((d * color.B).ToString("G"))
            .Append(']')
            .ToString();
    }

    public static string ToJavaScriptRgbaNumbersArrayText(this Color color)
    {
        const double d = 1.0d / 255;

        return new StringBuilder()
            .Append('[')
            .Append((d * color.R).ToString("G"))
            .Append(',')
            .Append((d * color.G).ToString("G"))
            .Append(',')
            .Append((d * color.B).ToString("G"))
            .Append(',')
            .Append((d * color.A).ToString("G"))
            .Append(']')
            .ToString();
    }

    public static string ToJavaScriptRgbNumbersArrayText(this IEnumerable<Color> colorsList)
    {
        var colorsArray = colorsList.ToArray();

        if (colorsArray.Length == 0)
            return "[]";

        var textArray = new string[colorsArray.Length, 3];

        const double d = 1.0d / 255;
        var i = 0;
        var iMax = colorsArray.Length - 1;
        foreach (var color in colorsArray)
        {
            textArray[i, 0] = (d * color.R).ToString("G") + ", ";
            textArray[i, 1] = (d * color.G).ToString("G") + ", ";
            textArray[i, 2] = i < iMax
                ? (d * color.B).ToString("G") + ","
                : (d * color.B).ToString("G");

            i++;
        }

        var table = new TextColumnsComposer(textArray);

        return new LinearTextComposer()
            .AppendLine("[")
            .IncreaseIndentation()
            .AppendAtNewLine(table.ToString())
            .DecreaseIndentation()
            .AppendAtNewLine("]")
            .ToString();
    }

    public static string ToJavaScriptRgbNumbersArrayText(this IEnumerable<Color> colorsList, string commentPrefix)
    {
        var colorsArray = colorsList.ToArray();

        if (colorsArray.Length == 0)
            return "[]";

        var textArray = new string[colorsArray.Length, 4];

        const double d = 1.0d / 255;
        var i = 0;
        var iMax = colorsArray.Length - 1;
        foreach (var color in colorsArray)
        {
            textArray[i, 0] = (d * color.R).ToString("G") + ", ";
            textArray[i, 1] = (d * color.G).ToString("G") + ", ";
            textArray[i, 2] = i < iMax
                ? (d * color.B).ToString("G") + ","
                : (d * color.B).ToString("G");
            textArray[i, 3] = commentPrefix + i;

            i++;
        }

        var table = new TextColumnsComposer(textArray);

        return new LinearTextComposer()
            .AppendLine("[")
            .IncreaseIndentation()
            .AppendAtNewLine(table.ToString())
            .DecreaseIndentation()
            .AppendAtNewLine("]")
            .ToString();
    }

    public static string ToJavaScriptRgbaNumbersArrayText(this IEnumerable<Color> colorsList)
    {
        var colorsArray = colorsList.ToArray();

        if (colorsArray.Length == 0)
            return "[]";

        var textArray = new string[colorsArray.Length, 4];

        const double d = 1.0d / 255;
        var i = 0;
        var iMax = colorsArray.Length - 1;
        foreach (var color in colorsArray)
        {
            textArray[i, 0] = (d * color.R).ToString("G") + ", ";
            textArray[i, 1] = (d * color.G).ToString("G") + ", ";
            textArray[i, 2] = (d * color.B).ToString("G") + ", ";
            textArray[i, 3] = i < iMax
                ? (d * color.A).ToString("G") + ","
                : (d * color.A).ToString("G");

            i++;
        }

        var table = new TextColumnsComposer(textArray);

        return new LinearTextComposer()
            .AppendLine("[")
            .IncreaseIndentation()
            .AppendAtNewLine(table.ToString())
            .DecreaseIndentation()
            .AppendAtNewLine("]")
            .ToString();
    }

    public static string ToJavaScriptRgbaNumbersArrayText(this IEnumerable<Color> colorsList, string commentPrefix)
    {
        var colorsArray = colorsList.ToArray();

        if (colorsArray.Length == 0)
            return "[]";

        var textArray = new string[colorsArray.Length, 5];

        const double d = 1.0d / 255;
        var i = 0;
        var iMax = colorsArray.Length - 1;
        foreach (var color in colorsArray)
        {
            textArray[i, 0] = (d * color.R).ToString("G") + ", ";
            textArray[i, 1] = (d * color.G).ToString("G") + ", ";
            textArray[i, 2] = (d * color.B).ToString("G") + ", ";
            textArray[i, 3] = i < iMax
                ? (d * color.A).ToString("G") + ","
                : (d * color.A).ToString("G");
            textArray[i, 4] = commentPrefix + i;

            i++;
        }

        var table = new TextColumnsComposer(textArray);

        return new LinearTextComposer()
            .AppendLine("[")
            .IncreaseIndentation()
            .AppendAtNewLine(table.ToString())
            .DecreaseIndentation()
            .AppendAtNewLine("]")
            .ToString();
    }
    #endregion


    #region Html Code Generation
    public static string GetHtmlCode(this IJsCodeHtmlPageGenerator scriptGenerator)
    {
        var listComposer = new ListTextComposer(Environment.NewLine)
        {
            ActiveItemPrefix = @"<script src = """,
            ActiveItemSuffix = @""" ></script>"
        };

        listComposer.AddRange(scriptGenerator.JavaScriptIncludes);


        var template = new ParametricTextComposer(
            "#", "#",
            scriptGenerator.HtmlTemplateText.Trim()
        );

        return template.GenerateText(
            "page-title", scriptGenerator.HtmlPageTitle,
            "javascript-includes", listComposer.ToString(),
            "xeogl-script", scriptGenerator.GetJavaScriptCode()
        );
    }

        
    public static void SaveJavaScriptCode(this IJsCodeHtmlPageGenerator scriptGenerator, string filePath)
    {
        File.WriteAllText(
            filePath, 
            scriptGenerator.GetJavaScriptCode()
        );
    }

    public static void SaveHtmlCode(this IJsCodeHtmlPageGenerator scriptGenerator, string filePath)
    {
        File.WriteAllText(
            filePath, 
            scriptGenerator.GetHtmlCode()
        );
    }
    #endregion
}