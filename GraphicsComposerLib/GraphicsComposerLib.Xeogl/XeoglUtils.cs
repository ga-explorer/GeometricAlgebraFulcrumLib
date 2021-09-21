using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using EuclideanGeometryLib.BasicMath.Maps.Space3D;
using EuclideanGeometryLib.BasicMath.Matrices;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicShapes.Triangles;
using EuclideanGeometryLib.Borders.Space3D;
using EuclideanGeometryLib.GraphicsGeometry;
using GraphicsComposerLib.Xeogl.Geometry.Primitives;
using GraphicsComposerLib.Xeogl.Transforms;
using TextComposerLib.Text;
using TextComposerLib.Text.Columns;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Parametric;
using TextComposerLib.Text.Structured;

namespace GraphicsComposerLib.Xeogl
{
    public static class XeoglUtils
    {
        public static string ToXeoglObjectsArrayText(this IEnumerable<IXeoglComponent> elementsList)
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


        #region xeogl Number Values Conversion
        public static string ToXeoglNumbersArrayText(this IEnumerable<int> numbersList)
            => numbersList
                .Select(n => n.ToString())
                .Concatenate(", ", "[", "]");

        public static string ToXeoglNumbersArrayText(this IEnumerable<int> numbersList, int valuesPerLine)
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

        public static string ToXeoglNumbersArrayText(this IEnumerable<int> numbersList, int valuesPerLine, string commentPrefix)
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

        public static string ToXeoglNumbersArrayText(this IEnumerable<double> numbersList)
            => numbersList
                .Select(n => n.ToString("G"))
                .Concatenate(", ", "[", "]");

        public static string ToXeoglNumbersArrayText(this IEnumerable<double> numbersList, int valuesPerLine)
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

        public static string ToXeoglNumbersArrayText(this IEnumerable<double> numbersList, int valuesPerLine, string commentPrefix)
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

        #region xeogl Color Values Conversion
        public static double[] ToXeoglRgbNumbersArray(this Color color)
        {
            const double d = 1.0d / 255;

            return new[]
            {
                d * color.R,
                d * color.G,
                d * color.B
            };
        }

        public static double[] ToXeoglRgbaNumbersArray(this Color color)
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

        public static double[] ToXeoglRgbNumbersArray(this IEnumerable<Color> colorsList)
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

        public static double[] ToXeoglRgbaNumbersArray(this IEnumerable<Color> colorsList)
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


        public static string ToXeoglRgbNumbersArrayText(this Color color)
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

        public static string ToXeoglRgbaNumbersArrayText(this Color color)
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

        public static string ToXeoglRgbNumbersArrayText(this IEnumerable<Color> colorsList)
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

        public static string ToXeoglRgbNumbersArrayText(this IEnumerable<Color> colorsList, string commentPrefix)
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

        public static string ToXeoglRgbaNumbersArrayText(this IEnumerable<Color> colorsList)
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

        public static string ToXeoglRgbaNumbersArrayText(this IEnumerable<Color> colorsList, string commentPrefix)
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

        #region xeogl 2D Tuple Values Conversion
        public static string ToXeoglNumbersArrayText(this ITuple2D tuple)
            => new StringBuilder()
                .Append('[')
                .Append(tuple.X.ToString("G"))
                .Append(',')
                .Append(tuple.Y.ToString("G"))
                .Append(']')
                .ToString();

        public static string ToXeoglNumbersArrayText(this IEnumerable<ITuple2D> tuplesList)
        {
            var tuplesArray = tuplesList.ToArray();

            if (tuplesArray.Length == 0)
                return "[]";

            var textArray = new string[tuplesArray.Length, 2];

            var i = 0;
            var iMax = tuplesArray.Length - 1;
            foreach (var point in tuplesArray)
            {
                textArray[i, 0] = point.X.ToString("G") + ", ";
                textArray[i, 1] = i < iMax
                    ? point.Y.ToString("G") + ","
                    : point.Y.ToString("G");

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

        public static string ToXeoglNumbersArrayText(this IEnumerable<ITuple2D> tuplesList, string commentPrefix)
        {
            var tuplesArray = tuplesList.ToArray();

            if (tuplesArray.Length == 0)
                return "[]";

            var textArray = new string[tuplesArray.Length, 3];

            var i = 0;
            var iMax = tuplesArray.Length - 1;
            foreach (var point in tuplesArray)
            {
                textArray[i, 0] = point.X.ToString("G") + ", ";
                textArray[i, 1] = i < iMax
                    ? point.Y.ToString("G") + ","
                    : point.Y.ToString("G");
                textArray[i, 2] = commentPrefix + i;

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

        #region xeogl 3D Tuple Values Conversion
        public static string ToXeoglNumbersArrayText(this ITuple3D tuple)
            => new StringBuilder()
                .Append('[')
                .Append(tuple.X.ToString("G"))
                .Append(',')
                .Append(tuple.Y.ToString("G"))
                .Append(',')
                .Append(tuple.Z.ToString("G"))
                .Append(']')
                .ToString();

        public static string ToXeoglNumbersArrayText(this IEnumerable<ITuple3D> tuplesList)
        {
            var tuplesArray = tuplesList.ToArray();

            if (tuplesArray.Length == 0)
                return "[]";

            var textArray = new string[tuplesArray.Length, 3];

            var i = 0;
            var iMax = tuplesArray.Length - 1;
            foreach (var point in tuplesArray)
            {
                textArray[i, 0] = point.X.ToString("G") + ", ";
                textArray[i, 1] = point.Y.ToString("G") + ", ";
                textArray[i, 2] = i < iMax
                    ? point.Z.ToString("G") + ","
                    : point.Z.ToString("G");

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

        public static string ToXeoglNumbersArrayText(this IEnumerable<ITuple3D> tuplesList, string commentPrefix)
        {
            var tuplesArray = tuplesList.ToArray();

            if (tuplesArray.Length == 0)
                return "[]";

            var textArray = new string[tuplesArray.Length, 4];

            var i = 0;
            var iMax = tuplesArray.Length - 1;
            foreach (var point in tuplesArray)
            {
                textArray[i, 0] = point.X.ToString("G") + ", ";
                textArray[i, 1] = point.Y.ToString("G") + ", ";
                textArray[i, 2] = i < iMax
                    ? point.Z.ToString("G") + ","
                    : point.Z.ToString("G");
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
        #endregion

        #region xeogl 4D Tuple Values Conversion
        public static string ToXeoglNumbersArrayText(this ITuple4D tuple)
            => new StringBuilder()
                .Append('[')
                .Append(tuple.X.ToString("G"))
                .Append(',')
                .Append(tuple.Y.ToString("G"))
                .Append(',')
                .Append(tuple.Z.ToString("G"))
                .Append(',')
                .Append(tuple.W.ToString("G"))
                .Append(']')
                .ToString();

        public static string ToXeoglNumbersArrayText(this IEnumerable<ITuple4D> tuplesList)
        {
            var tuplesArray = tuplesList.ToArray();

            if (tuplesArray.Length == 0)
                return "[]";

            var textArray = new string[tuplesArray.Length, 4];

            var i = 0;
            var iMax = tuplesArray.Length - 1;
            foreach (var point in tuplesArray)
            {
                textArray[i, 0] = point.X.ToString("G") + ", ";
                textArray[i, 1] = point.Y.ToString("G") + ", ";
                textArray[i, 2] = point.Z.ToString("G") + ", ";
                textArray[i, 3] = i < iMax
                    ? point.W.ToString("G") + ","
                    : point.W.ToString("G");

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

        public static string ToXeoglNumbersArrayText(this IEnumerable<ITuple4D> tuplesList, string commentPrefix)
        {
            var tuplesArray = tuplesList.ToArray();

            if (tuplesArray.Length == 0)
                return "[]";

            var textArray = new string[tuplesArray.Length, 5];

            var i = 0;
            var iMax = tuplesArray.Length - 1;
            foreach (var point in tuplesArray)
            {
                textArray[i, 0] = point.X.ToString("G") + ", ";
                textArray[i, 1] = point.Y.ToString("G") + ", ";
                textArray[i, 2] = point.Z.ToString("G") + ", ";
                textArray[i, 3] = i < iMax
                    ? point.W.ToString("G") + ","
                    : point.W.ToString("G");
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

        #region xeogl Transformation Values Conversion
        public static string ToXeoglNumbersArrayText(this Matrix4X4 matrix)
            => new StringBuilder()
                .Append('[')
                .Append(matrix[0, 0].ToString("G"))
                .Append(',')
                .Append(matrix[0, 1].ToString("G"))
                .Append(',')
                .Append(matrix[0, 2].ToString("G"))
                .Append(',')
                .Append(matrix[0, 3].ToString("G"))
                .Append(',')
                .Append(matrix[1, 0].ToString("G"))
                .Append(',')
                .Append(matrix[1, 1].ToString("G"))
                .Append(',')
                .Append(matrix[1, 2].ToString("G"))
                .Append(',')
                .Append(matrix[1, 3].ToString("G"))
                .Append(',')
                .Append(matrix[2, 0].ToString("G"))
                .Append(',')
                .Append(matrix[2, 1].ToString("G"))
                .Append(',')
                .Append(matrix[2, 2].ToString("G"))
                .Append(',')
                .Append(matrix[2, 3].ToString("G"))
                .Append(',')
                .Append(matrix[3, 0].ToString("G"))
                .Append(',')
                .Append(matrix[3, 1].ToString("G"))
                .Append(',')
                .Append(matrix[3, 2].ToString("G"))
                .Append(',')
                .Append(matrix[3, 3].ToString("G"))
                .Append(']')
                .ToString();

        public static XeoglMatrixTransform ToXeoglTransform(this Matrix4X4 matrix)
            => new XeoglMatrixTransform(matrix);

        public static XeoglQRotateScaleTranslateTransform ToXeoglTransform(this RotateScaleTranslateMap3D affineMap)
        {
            var quaternion = affineMap.RotateQuaternion;

            return new XeoglQRotateScaleTranslateTransform()
            {
                TranslateX = affineMap.TranslateX,
                TranslateY = affineMap.TranslateY,
                TranslateZ = affineMap.TranslateZ,
                ScaleX = affineMap.ScaleX,
                ScaleY = affineMap.ScaleY,
                ScaleZ = affineMap.ScaleZ,
                QuaternionX = quaternion.X,
                QuaternionY = quaternion.Y,
                QuaternionZ = quaternion.Z,
                QuaternionW = quaternion.W
            };
        }
        #endregion


        public static XeoglTrianglesGeometry ToXeoglTrianglesGeometry(this IEnumerable<ITriangle3D> trianglesList, bool reversePoints, bool reverseNormals)
        {
            var geometry = trianglesList.ToGraphicsTrianglesGeometry(reversePoints);
            geometry.ComputeVertexNormals(reverseNormals);

            return new XeoglTrianglesGeometry(geometry);
        }

        public static XeoglTrianglesGeometry ToXeoglTrianglesListGeometry(this IEnumerable<ITriangle3D> trianglesList, bool reversePoints, bool reverseNormals)
        {
            var geometry = trianglesList.ToGraphicsTrianglesListGeometry(reversePoints);
            geometry.ComputeVertexNormals(reverseNormals);

            return new XeoglTrianglesGeometry(geometry);
        }


        public static string ToXeoglNumbersArrayText(this IBoundingBox3D boundingBox)
            => new StringBuilder()
                .Append('[')
                .Append(boundingBox.MinX.ToString("G"))
                .Append(',')
                .Append(boundingBox.MinY.ToString("G"))
                .Append(',')
                .Append(boundingBox.MinZ.ToString("G"))
                .Append(',')
                .Append(boundingBox.MaxX.ToString("G"))
                .Append(',')
                .Append(boundingBox.MaxY.ToString("G"))
                .Append(',')
                .Append(boundingBox.MaxZ.ToString("G"))
                .Append(']')
                .ToString();

        
        public static string GetHtmlCode(this IXeoglScriptGenerator scriptGenerator)
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

        
        public static void SaveJavaScriptCode(this IXeoglScriptGenerator scriptGenerator, string filePath)
        {
            File.WriteAllText(
                filePath, 
                scriptGenerator.GetJavaScriptCode()
            );
        }

        public static void SaveHtmlCode(this IXeoglScriptGenerator scriptGenerator, string filePath)
        {
            File.WriteAllText(
                filePath, 
                scriptGenerator.GetHtmlCode()
            );
        }


    }
}
