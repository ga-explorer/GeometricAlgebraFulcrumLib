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

namespace TextComposerLib.Code.JavaScript
{
    public static class JavaScriptCodeUtils
    {
        public static JsBoolean JsSet(this string jsVariableName, JsBoolean jsVariableValue)
        {
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{jsVariableName} = {jsVariableValue.GetJsCode()};");

            return new JsBoolean(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue
            );
        }

        public static JsBoolean JsSet(this JsBoolean jsVariableValue, string jsVariableName)
        {
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{jsVariableName} = {jsVariableValue.GetJsCode()};");

            return new JsBoolean(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue
            );
        }

        public static JsNumber JsSet(this string jsVariableName, JsNumber jsVariableValue)
        {
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{jsVariableName} = {jsVariableValue.GetJsCode()};");

            return new JsNumber(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue
            );
        }
        
        public static JsNumber JsSet(this JsNumber jsVariableValue, string jsVariableName)
        {
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{jsVariableName} = {jsVariableValue.GetJsCode()};");

            return new JsNumber(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue
            );
        }

        public static JsString JsSet(this string jsVariableName, JsString jsVariableValue)
        {
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{jsVariableName} = {jsVariableValue.GetJsCode()};");

            return new JsString(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue
            );
        }
        
        public static JsString JsSet(this JsString jsVariableValue, string jsVariableName)
        {
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{jsVariableName} = {jsVariableValue.GetJsCode()};");

            return new JsString(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue
            );
        }

        public static JsBoolean JsLet(this string jsVariableName, JsBoolean jsVariableValue)
        {
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"let {jsVariableName} = {jsVariableValue.GetJsCode()};");

            return new JsBoolean(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue
            );
        }
        
        public static JsBoolean JsLet(this JsBoolean jsVariableValue, string jsVariableName)
        {
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"let {jsVariableName} = {jsVariableValue.GetJsCode()};");

            return new JsBoolean(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue
            );
        }

        public static JsNumber JsLet(this string jsVariableName, JsNumber jsVariableValue)
        {
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"let {jsVariableName} = {jsVariableValue.GetJsCode()};");

            return new JsNumber(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue
            );
        }
        
        public static JsNumber JsLet(this JsNumber jsVariableValue, string jsVariableName)
        {
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"let {jsVariableName} = {jsVariableValue.GetJsCode()};");

            return new JsNumber(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue
            );
        }

        public static JsString JsLet(this string jsVariableName, JsString jsVariableValue)
        {
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"let {jsVariableName} = {jsVariableValue.GetJsCode()};");

            return new JsString(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue
            );
        }

        public static JsString JsLet(this JsString jsVariableValue, string jsVariableName)
        {
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"let {jsVariableName} = {jsVariableValue.GetJsCode()};");

            return new JsString(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue
            );
        }
        
        public static JsBoolean JsConst(this string jsVariableName, JsBoolean jsVariableValue)
        {
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"const {jsVariableName} = {jsVariableValue.GetJsCode()};");

            return new JsBoolean(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue
            );
        }
        
        public static JsBoolean JsConst(this JsBoolean jsVariableValue, string jsVariableName)
        {
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"const {jsVariableName} = {jsVariableValue.GetJsCode()};");

            return new JsBoolean(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue
            );
        }

        public static JsNumber JsConst(this string jsVariableName, JsNumber jsVariableValue)
        {
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"const {jsVariableName} = {jsVariableValue.GetJsCode()};");

            return new JsNumber(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue
            );
        }
        
        public static JsNumber JsConst(this JsNumber jsVariableValue, string jsVariableName)
        {
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"const {jsVariableName} = {jsVariableValue.GetJsCode()};");

            return new JsNumber(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue
            );
        }

        public static JsString JsConst(this string jsVariableName, JsString jsVariableValue)
        {
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"const {jsVariableName} = {jsVariableValue.GetJsCode()};");

            return new JsString(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue
            );
        }
        
        public static JsString JsConst(this JsString jsVariableValue, string jsVariableName)
        {
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"const {jsVariableName} = {jsVariableValue.GetJsCode()};");

            return new JsString(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue
            );
        }


        public static JsBoolean AsJsBoolean(this bool literalValue)
        {
            return new JsBoolean(literalValue);
        }

        public static JsBoolean AsJsBoolean(this string jsTextCode)
        {
            return jsTextCode;
        }
        
        public static JsBoolean AsJsBoolean(this JsType value)
        {
            return value is JsBoolean typedValue ? typedValue : value.GetJsCode().AsJsBoolean();
        }

        public static JsNumber AsJsNumber(this byte literalValue)
        {
            return new JsNumber(literalValue);
        }
        
        public static JsNumber AsJsNumber(this short literalValue)
        {
            return new JsNumber(literalValue);
        }
        
        public static JsNumber AsJsNumber(this ushort literalValue)
        {
            return new JsNumber(literalValue);
        }
        
        public static JsNumber AsJsNumber(this int literalValue)
        {
            return new JsNumber(literalValue);
        }

        public static JsNumber AsJsNumber(this uint literalValue)
        {
            return new JsNumber(literalValue);
        }

        public static JsNumber AsJsNumber(this long literalValue)
        {
            return new JsNumber(literalValue);
        }

        public static JsNumber AsJsNumber(this ulong literalValue)
        {
            return new JsNumber(literalValue);
        }

        public static JsNumber AsJsNumber(this float literalValue)
        {
            return new JsNumber(literalValue);
        }

        public static JsNumber AsJsNumber(this double literalValue)
        {
            return new JsNumber(literalValue);
        }

        public static JsNumber AsJsNumber(this string jsTextCode)
        {
            return jsTextCode;
        }
        
        public static JsString AsJsString(this string literalValue)
        {
            return new JsString(literalValue);
        }
        
        public static JsObject AsJsObject(this IReadOnlyDictionary<string, JsType> propertiesDictionary)
        {
            return new JsObject(propertiesDictionary);
        }
        
        public static JsTextCode AsJsTextCode(this string jsTextCode)
        {
            return new JsTextCode(jsTextCode);
        }


        public static JsType AsJsTypeVariable(this string jsVariableName, JsType jsVariableValue = null)
        {
            return new JsTextCode(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue?.GetJsCode()
            );
        }

        public static JsBoolean AsJsBooleanVariable(this string jsVariableName, JsString jsVariableValue = null)
        {
            return new JsBoolean(
                new JsVariableConstructor(jsVariableName)
            );
        }

        public static JsNumber AsJsNumberVariable(this string jsVariableName, JsNumber jsVariableValue = null)
        {
            return new JsNumber(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue
            );
        }
        
        public static JsString AsJsStringVariable(this string jsVariableName, JsString jsVariableValue = null)
        {
            return new JsString(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue
            );
        }
        
        public static JsObject AsJsObjectVariable(this string jsVariableName, JsObject jsVariableValue = null)
        {
            return new JsObject(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue
            );
        }
        
        public static JsArray AsJsArrayVariable(this string jsVariableName, JsArray jsVariableValue = null)
        {
            return new JsArray(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue
            );
        }
        
        public static JsFloat32Array AsJsFloat32ArrayVariable(this string jsVariableName, JsFloat32Array jsVariableValue = null)
        {
            return new JsFloat32Array(
                new JsVariableConstructor(jsVariableName),
                jsVariableValue
            );
        }


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
}