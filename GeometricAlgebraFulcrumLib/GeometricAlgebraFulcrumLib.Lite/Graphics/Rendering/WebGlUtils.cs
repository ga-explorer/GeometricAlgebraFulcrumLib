using System.Text;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D;
using TextComposerLib.Code.JavaScript;
using TextComposerLib.Text.Columns;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering
{
    public static class WebGlUtils
    {
        public static JavaScriptAttributesDictionary SetNumbersArrayValue(this JavaScriptAttributesDictionary composer, string key, IFloat64Vector2D value, IFloat64Vector3D valueDefault)
        {
            composer.SetTextValue(
                key,
                value.ToJavaScriptNumbersArrayText(),
                valueDefault.ToJavaScriptNumbersArrayText()
            );

            return composer;
        }

        public static JavaScriptAttributesDictionary SetNumbersArrayValue(this JavaScriptAttributesDictionary composer, string key, IFloat64Vector3D value)
        {
            composer.SetTextValue(
                key,
                value.ToJavaScriptNumbersArrayText()
            );

            return composer;
        }

        public static JavaScriptAttributesDictionary SetNumbersArrayValue(this JavaScriptAttributesDictionary composer, string key, IFloat64Vector3D value, IFloat64Vector3D valueDefault)
        {
            composer.SetTextValue(
                key,
                value.ToJavaScriptNumbersArrayText(),
                valueDefault.ToJavaScriptNumbersArrayText()
            );

            return composer;
        }

        public static JavaScriptAttributesDictionary SetNumbersArrayValue(this JavaScriptAttributesDictionary composer, string key, SquareMatrix4 value, IFloat64Vector3D valueDefault)
        {
            composer.SetTextValue(
                key,
                value.ToJavaScriptNumbersArrayText(),
                valueDefault.ToJavaScriptNumbersArrayText()
            );

            return composer;
        }

        public static JavaScriptAttributesDictionary SetNumbersArrayValue(this JavaScriptAttributesDictionary composer, string key, IBoundingBox3D value, IFloat64Vector3D valueDefault)
        {
            composer.SetTextValue(
                key,
                value.ToJavaScriptNumbersArrayText(),
                valueDefault.ToJavaScriptNumbersArrayText()
            );

            return composer;
        }


        
        public static JavaScriptAttributesDictionary SetNumbersArrayValue(this JavaScriptAttributesDictionary composer, string key, IEnumerable<IFloat64Vector3D> value, string commentPrefix, string valueDefault)
        {
            composer.SetTextValue(
                key,
                value.ToJavaScriptNumbersArrayText(commentPrefix),
                valueDefault
            );

            return composer;
        }

        public static JavaScriptAttributesDictionary SetNumbersArrayValue(this JavaScriptAttributesDictionary composer, string key, IEnumerable<Float64Vector3D> value, string commentPrefix, string valueDefault)
        {
            composer.SetTextValue(
                key,
                value.ToJavaScriptNumbersArrayText(commentPrefix),
                valueDefault
            );

            return composer;
        }

        public static JavaScriptAttributesDictionary SetNumbersArrayValue(this JavaScriptAttributesDictionary composer, string key, IEnumerable<IFloat64Vector2D> value, string commentPrefix, string valueDefault)
        {
            composer.SetTextValue(
                key,
                value.ToJavaScriptNumbersArrayText(commentPrefix),
                valueDefault
            );

            return composer;
        }

        public static JavaScriptAttributesDictionary SetNumbersArrayValue(this JavaScriptAttributesDictionary composer, string key, IEnumerable<Float64Vector2D> value, string commentPrefix, string valueDefault)
        {
            composer.SetTextValue(
                key,
                value.ToJavaScriptNumbersArrayText(commentPrefix),
                valueDefault
            );

            return composer;
        }

        #region 2D Tuple Values Conversion
        public static string ToJavaScriptNumbersArrayText(this IFloat64Vector2D tuple)
            => new StringBuilder()
                .Append('[')
                .Append(tuple.X.ToString("G"))
                .Append(',')
                .Append(tuple.Y.ToString("G"))
                .Append(']')
                .ToString();

        public static string ToJavaScriptNumbersArrayText(this IEnumerable<IFloat64Vector2D> tuplesList)
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

        public static string ToJavaScriptNumbersArrayText(this IEnumerable<IFloat64Vector2D> tuplesList, string commentPrefix)
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

        #region 3D Tuple Values Conversion
        public static string ToJavaScriptNumbersArrayText(this IFloat64Vector3D tuple)
            => new StringBuilder()
                .Append('[')
                .Append(tuple.X.ToString("G"))
                .Append(',')
                .Append(tuple.Y.ToString("G"))
                .Append(',')
                .Append(tuple.Z.ToString("G"))
                .Append(']')
                .ToString();

        public static string ToJavaScriptNumbersArrayText(this IEnumerable<IFloat64Vector3D> tuplesList)
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

        public static string ToJavaScriptNumbersArrayText(this IEnumerable<IFloat64Vector3D> tuplesList, string commentPrefix)
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

        #region 4D Tuple Values Conversion
        public static string ToJavaScriptNumbersArrayText(this Float64Quaternion tuple)
        {
            return new StringBuilder()
                .Append('[')
                .Append(tuple.ScalarI.ToString("G"))
                .Append(',')
                .Append(tuple.ScalarJ.ToString("G"))
                .Append(',')
                .Append(tuple.ScalarK.ToString("G"))
                .Append(',')
                .Append(tuple.Scalar.ToString("G"))
                .Append(']')
                .ToString();
        }
        
        public static string ToJavaScriptNumbersArrayText(this IFloat64Vector4D tuple)
        {
            return new StringBuilder()
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
        }

        public static string ToJavaScriptNumbersArrayText(this IEnumerable<IFloat64Vector4D> tuplesList)
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

        public static string ToJavaScriptNumbersArrayText(this IEnumerable<IFloat64Vector4D> tuplesList, string commentPrefix)
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

        public static string ToJavaScriptNumbersArrayText(this SquareMatrix4 matrix)
        {
            return new StringBuilder()
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
        }
        
        public static string ToJavaScriptNumbersArrayText(this IBoundingBox3D boundingBox)
        {
            return new StringBuilder()
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
        }


    }
}