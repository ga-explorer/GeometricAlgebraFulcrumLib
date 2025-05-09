using System.Numerics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Colors;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Constants;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Paths;
using Newtonsoft.Json.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs;

public static class GrKonvaJsUtils
{
    internal static string? GetValueCode<T>(this SparseCodeAttributeValue<T>? value)
    {
        return value is null || value.IsEmpty
            ? null
            : value.GetAttributeValueCode();
    }

    internal static Pair<string>? GetNameValueCodePair(this GrKonvaJsCodeValue? value, string name)
    {
        return value is null || value.IsEmpty
            ? null
            : new Pair<string>(name, value.GetAttributeValueCode());
    }

    internal static Pair<string>? GetNameValueCodePair<T>(this SparseCodeAttributeValue<T>? value, string name)
    {
        return value is null || value.IsEmpty
            ? null
            : new Pair<string>(name, value.GetAttributeValueCode());
    }

    internal static Pair<string>? GetNameValueCodePair<T>(this SparseCodeAttributeValue<T>? value, string name, SparseCodeAttributeValue<T>? defaultValue)
    {
        return value is null || value.IsEmpty
            ? defaultValue.GetNameValueCodePair(name)
            : new Pair<string>(name, value.GetAttributeValueCode());
    }

    public static GrKonvaJsVector3Value ToKonvaJsVector3Value(this ILinFloat64Vector3D value)
    {
        return GrKonvaJsVector3Value.Create(value);
    }

    public static bool HasValue(this SparseCodeAttributeValue? value)
    {
        return value is not null && !value.IsEmpty;
    }

    public static bool IsNullOrEmpty(this SparseCodeAttributeValue? value)
    {
        return value is null || value.IsEmpty;
    }

    public static JToken GetKonvaJsJson(this bool value)
    {
        return JToken.FromObject(value);
    }

    public static JToken GetKonvaJsJson(this int value)
    {
        return JToken.FromObject(value);
    }

    public static JToken GetKonvaJsJson(this float value)
    {
        return JToken.FromObject(value);
    }

    public static JToken GetKonvaJsJson(this double value)
    {
        return JToken.FromObject((float)value);
    }

    public static JToken GetKonvaJsJson(this SizeF value)
    {
        return new JArray(
            value.Width,
            value.Height
        );
    }

    public static JToken GetKonvaJsJson(this IPair<double> value)
    {
        return new JArray(
            (float)value.Item1,
            (float)value.Item2
        );
    }

    public static JToken GetKonvaJsJson(this ILinFloat64Vector2D value)
    {
        return new JArray(
            (float)value.X,
            (float)value.Y
        );
    }

    public static JToken GetKonvaJsJson(this ITriplet<Float64Scalar> value)
    {
        return new JArray(
            (float)value.Item1,
            (float)value.Item2,
            (float)value.Item3
        );
    }

    public static JToken GetKonvaJsJson(this ILinFloat64Vector3D value)
    {
        return new JArray(
            (float)value.X,
            (float)value.Y,
            (float)value.Z
        );
    }

    public static JToken GetKonvaJsJson(this IQuad<double> value)
    {
        return new JArray(
            (float)value.Item1,
            (float)value.Item2,
            (float)value.Item3,
            (float)value.Item4
        );
    }

    public static JToken GetKonvaJsJson(this ILinFloat64Vector4D value)
    {
        return new JArray(
            (float)value.X,
            (float)value.Y,
            (float)value.Z,
            (float)value.W
        );
    }

    public static JToken GetKonvaJsJson(this LinFloat64Quaternion value)
    {
        return new JArray(
            (float)-value.ScalarI,
            (float)-value.ScalarJ,
            (float)-value.ScalarK,
            (float)value.Scalar
        );
    }

    public static JToken GetKonvaJsJson(this Quaternion value)
    {
        return new JArray(
            value.X,
            value.Y,
            value.Z,
            value.W
        );
    }

    public static JToken GetKonvaJsJson(this System.Drawing.Color color, bool useAlpha = false)
    {
        if (useAlpha)
            return new JArray(
                (color.R / 255f).GetKonvaJsJson(),
                (color.G / 255f).GetKonvaJsJson(),
                (color.B / 255f).GetKonvaJsJson(),
                (color.A / 255f).GetKonvaJsJson()
            );

        return new JArray(
            (color.R / 255f).GetKonvaJsJson(),
            (color.G / 255f).GetKonvaJsJson(),
            (color.B / 255f).GetKonvaJsJson()
        );
    }

    public static JToken GetKonvaJsJson(this Color color, bool useAlpha = false)
    {
        if (useAlpha)
        {
            var c = color.ToPixel<Rgba32>();

            return new JArray(
                (c.R / 255f).GetKonvaJsJson(),
                (c.G / 255f).GetKonvaJsJson(),
                (c.B / 255f).GetKonvaJsJson(),
                (c.A / 255f).GetKonvaJsJson()
            );
        }
        else
        {
            var c = color.ToPixel<Rgb24>();

            return new JArray(
                (c.R / 255f).GetKonvaJsJson(),
                (c.G / 255f).GetKonvaJsJson(),
                (c.B / 255f).GetKonvaJsJson()
            );
        }
    }


    public static string GetKonvaJsCode(this bool value)
    {
        return value ? "true" : "false";
    }

    public static string GetKonvaJsCode(this int value)
    {
        return value.ToString();
    }

    public static string GetKonvaJsCode(this float value)
    {
        return value.ToString("G");
    }

    public static string GetKonvaJsCode(this double value)
    {
        return ((float)value).ToString("G");
    }

    public static string GetKonvaJsCode(this SizeF value)
    {
        return value.Width == value.Height
            ? value.Width.GetKonvaJsCode()
            : $"{{ width: {value.Width.GetKonvaJsCode()}, height: {value.Height.GetKonvaJsCode()} }}";
    }

    public static string GetKonvaJsCode(this System.Drawing.Color color, bool useAlpha = false)
    {
        if (useAlpha)
        {
            var alphaText = (color.A / 255d).GetKonvaJsCode();

            return $"'rgba({color.R}, {color.G}, {color.B}, {alphaText})'";
        }
        
        return $"'rgb({color.R}, {color.G}, {color.B})'";
    }

    public static string GetKonvaJsCode(this Color color, bool useAlpha = false)
    {
        if (useAlpha)
        {
            var c = color.ToPixel<Rgba32>();
            var alphaText = (c.A / 255d).GetKonvaJsCode();

            return $"'rgba({c.R}, {c.G}, {c.B}, {alphaText})'";
        }
        else
        {
            var c = color.ToPixel<Rgb24>();

            return $"'rgb({c.R}, {c.G}, {c.B})'";
        }
    }

    public static string GetKonvaJsCode(this LinBasisVector3D axis)
    {
        return axis switch
        {
            LinBasisVector3D.Px => "Konva.Axis.X",
            LinBasisVector3D.Nx => "(-Konva.Axis.X)",
            LinBasisVector3D.Py => "Konva.Axis.Y",
            LinBasisVector3D.Ny => "(-Konva.Axis.Y)",
            LinBasisVector3D.Pz => "Konva.Axis.Z",
            _ => "(-Konva.Axis.Z)"
        };
    }

    public static string GetKonvaJsCode(this IPair<Float64Scalar> vector)
    {
        return $"{{ x: {(float)vector.Item1:G}, y: {(float)vector.Item2:G} }}";
    }

    public static string GetKonvaJsCode(this ITriplet<Float64Scalar> vector)
    {
        return $"new Konva.Vector3({(float)vector.Item1:G}, {(float)vector.Item2:G}, {(float)vector.Item3:G})";
    }

    public static string GetKonvaJsCode(this IQuad<Float64Scalar> vector)
    {
        return $"new Konva.Vector4({(float)vector.Item1:G}, {(float)vector.Item2:G}, {(float)vector.Item3:G}, {(float)vector.Item4:G})";
    }
    
    public static string GetKonvaJsCode(this Float64BoundingBox2D boundingBox)
    {
        return $"{{ x: {(float)boundingBox.MinX:G}, y: {(float)boundingBox.MinY:G}, width: {(float)boundingBox.LengthX:G}, height: {(float)boundingBox.LengthY:G} }}";
    }

    public static string GetKonvaJsCode(this IEnumerable<int> vectorList)
    {
        return vectorList.Select(
            vector => vector.GetKonvaJsCode()
        ).Concatenate(", ", "[", "]");
    }
    
    public static string GetKonvaJsCode(this IEnumerable<float> vectorList)
    {
        return vectorList.Select(
            vector => vector.GetKonvaJsCode()
        ).Concatenate(", ", "[", "]");
    }
    
    public static string GetKonvaJsCode(this IEnumerable<double> vectorList)
    {
        return vectorList.Select(
            vector => vector.GetKonvaJsCode()
        ).Concatenate(", ", "[", "]");
    }

    public static string GetKonvaJsArrayCode(this IEnumerable<string> itemsList)
    {
        var itemsText =
            itemsList.Concatenate($",{Environment.NewLine}");

        return new LinearTextComposer()
            .AppendLine("[")
            .IncreaseIndentation()
            .Append(itemsText)
            .DecreaseIndentation()
            .AppendAtNewLine("]")
            .ToString();
    }

    public static string GetKonvaJsCode(this IEnumerable<Color> colorList, bool useAlpha = false)
    {
        return colorList.Select(
            vector => vector.GetKonvaJsCode(useAlpha)
        ).GetKonvaJsArrayCode();
    }
    
    public static string GetKonvaJsCode(this GrColorLinearGradientList colorList)
    {
        return colorList.Select(
            pair => $"{pair.Key.GetKonvaJsCode()}, {pair.Value.GetKonvaJsCode(true)}"
        ).GetKonvaJsArrayCode();
    }

    public static string GetKonvaJsCode(this IEnumerable<IReadOnlyList<Color>> colorArrayList, bool useAlpha = false)
    {
        return colorArrayList.Select(
            vector => vector.GetKonvaJsCode(useAlpha)
        ).GetKonvaJsArrayCode();
    }

    public static string GetKonvaJsCode(this IEnumerable<IPair<Float64Scalar>> vectorList)
    {
        return vectorList.Select(
            vector => $"{vector.Item1.ScalarValue.GetKonvaJsCode()}, {vector.Item2.ScalarValue.GetKonvaJsCode()}"
        ).GetKonvaJsArrayCode();
    }

    //public static string GetKonvaJsCode(this IEnumerable<ITriplet<Float64Scalar>> vectorList)
    //{
    //    return vectorList.Select(
    //        vector => vector.GetKonvaJsCode()
    //    ).GetKonvaJsArrayCode();
    //}

    //public static string GetKonvaJsCode(this IEnumerable<IReadOnlyList<ITriplet<Float64Scalar>>> vectorList)
    //{
    //    return vectorList.Select(
    //        vector => vector.GetKonvaJsCode()
    //    ).GetKonvaJsArrayCode();
    //}

    //public static string GetKonvaJsCode(this IEnumerable<IQuad<double>> vectorList)
    //{
    //    return vectorList.Select(
    //        vector => vector.GetKonvaJsCode()
    //    ).GetKonvaJsArrayCode();
    //}

    public static string GetAffineMapKonvaJsArrayCode(this double[,] affineMapArray, bool isTransposed = false)
    {
        return affineMapArray
            .GetItems(isTransposed)
            .Select(
                s => s.GetKonvaJsCode()
            ).Concatenate(
                ",",
                "[",
                "]"
            );
    }

    public static string GetAffineMapKonvaJsMatrixCode(this double[,] affineMapArray, bool isTransposed = false)
    {
        var affineMapArrayCode =
            affineMapArray.GetAffineMapKonvaJsArrayCode(isTransposed);

        return $"Konva.Matrix.FromArray({affineMapArrayCode}, 0)";
    }

    public static string GetKonvaJsArrayCode(this IFloat64AffineMap3D affineMap, bool isTransposed = false)
    {
        return affineMap
            .GetArray2D()
            .GetAffineMapKonvaJsArrayCode(isTransposed);
    }

    public static string GetKonvaJsMatrixCode(this IFloat64AffineMap3D affineMap, bool isTransposed = false)
    {
        return affineMap
            .GetArray2D()
            .GetAffineMapKonvaJsMatrixCode(isTransposed);
    }

    public static string GetKonvaJsCode(this IEnumerable<SparseCodeAttributeValue> valueList)
    {
        return valueList.Select(
            value => value.GetAttributeValueCode()
        ).GetKonvaJsArrayCode();
    }

    public static string GetKonvaJsCode(this IPair<ITriplet<Float64Scalar>> vectorList)
    {
        var p1 = vectorList.Item1.GetKonvaJsCode();
        var p2 = vectorList.Item2.GetKonvaJsCode();

        return $"[{p1}, {p2}]";
    }

    public static string GetKonvaJsCode(this ITriplet<ITriplet<Float64Scalar>> vectorList)
    {
        var p1 = vectorList.Item1.GetKonvaJsCode();
        var p2 = vectorList.Item2.GetKonvaJsCode();
        var p3 = vectorList.Item3.GetKonvaJsCode();

        return $"[{p1}, {p2}, {p3}]";
    }

    public static string GetKonvaJsCode(this IQuad<ITriplet<Float64Scalar>> vectorList)
    {
        var p1 = vectorList.Item1.GetKonvaJsCode();
        var p2 = vectorList.Item2.GetKonvaJsCode();
        var p3 = vectorList.Item3.GetKonvaJsCode();
        var p4 = vectorList.Item4.GetKonvaJsCode();

        return $"[{p1}, {p2}, {p3}, {p4}]";
    }
    
    public static string GetKonvaJsCode(this SvgPathCommand value)
    {
        return value.ValueText.SingleQuote();
    }

    
    public static string GetKonvaJsCode(this GrKonvaJsEmbossDirection value)
    {
        return value switch
        {
            GrKonvaJsEmbossDirection.Top => "'top'",
            GrKonvaJsEmbossDirection.Bottom => "'bottom'",
            GrKonvaJsEmbossDirection.Left => "'left'",
            GrKonvaJsEmbossDirection.Right => "'right'",
            GrKonvaJsEmbossDirection.TopLeft => "'top-left'",
            GrKonvaJsEmbossDirection.TopRight => "'top-right'",
            GrKonvaJsEmbossDirection.BottomLeft => "'bottom-left'",
            GrKonvaJsEmbossDirection.BottomRight => "'bottom-right'",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
    
    public static string GetKonvaJsCode(this GrKonvaJsFillPatternRepeat value)
    {
        return value switch
        {
            GrKonvaJsFillPatternRepeat.NoRepeat => "'no-repeat'",
            GrKonvaJsFillPatternRepeat.RepeatX => "'repeat-x'",
            GrKonvaJsFillPatternRepeat.RepeatY => "'repeat-y'",
            GrKonvaJsFillPatternRepeat.RepeatXy => "'repeat'",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
    
    public static string GetKonvaJsCode(this GrKonvaJsFillRule value)
    {
        return value switch
        {
            GrKonvaJsFillRule.NonZero => "'nonzero'",
            GrKonvaJsFillRule.EvenOdd => "'evenodd'",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
    
    public static string GetKonvaJsCode(this GrKonvaJsFillPriority value)
    {
        return value switch
        {
            GrKonvaJsFillPriority.Color => "'color'",
            GrKonvaJsFillPriority.Pattern => "'pattern'",
            GrKonvaJsFillPriority.LinearGradient => "'linear-gradient'",
            GrKonvaJsFillPriority.RadialGradient => "'radial-gradient'",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
    
    public static string GetKonvaJsCode(this GrKonvaJsLineCap value)
    {
        return value switch
        {
            GrKonvaJsLineCap.Square => "'square'",
            GrKonvaJsLineCap.Round => "'round'",
            GrKonvaJsLineCap.Butt => "'butt'",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
    
    public static string GetKonvaJsCode(this GrKonvaJsLineJoin value)
    {
        return value switch
        {
            GrKonvaJsLineJoin.Bevel => "'bevel'",
            GrKonvaJsLineJoin.Round => "'round'",
            GrKonvaJsLineJoin.Miter => "'miter'",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }

    public static string GetKonvaJsCode(this GrKonvaJsTransformsEnabled value)
    {
        return value switch
        {
            GrKonvaJsTransformsEnabled.None => "'none'",
            GrKonvaJsTransformsEnabled.Position => "'position'",
            GrKonvaJsTransformsEnabled.All => "'all'",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
    
    public static string GetKonvaJsCode(this GrKonvaJsGlobalCompositeOperation value)
    {
        return value switch
        {
            GrKonvaJsGlobalCompositeOperation.Color => "'color'",
            GrKonvaJsGlobalCompositeOperation.ColorBurn => "'color-burn'",
            GrKonvaJsGlobalCompositeOperation.ColorDodge => "'color-dodge'",
            GrKonvaJsGlobalCompositeOperation.Copy => "'copy'",
            GrKonvaJsGlobalCompositeOperation.Darken => "'darken'",
            GrKonvaJsGlobalCompositeOperation.DestinationAtop => "'destination-atop'",
            GrKonvaJsGlobalCompositeOperation.DestinationIn => "'destination-in'",
            GrKonvaJsGlobalCompositeOperation.DestinationOut => "'destination-out'",
            GrKonvaJsGlobalCompositeOperation.DestinationOver => "'destination-over'",
            GrKonvaJsGlobalCompositeOperation.Difference => "'difference'",
            GrKonvaJsGlobalCompositeOperation.Exclusion => "'exclusion'",
            GrKonvaJsGlobalCompositeOperation.HardLight => "'hard-light'",
            GrKonvaJsGlobalCompositeOperation.Hue => "'hue'",
            GrKonvaJsGlobalCompositeOperation.Lighten => "'lighten'",
            GrKonvaJsGlobalCompositeOperation.Lighter => "'lighter'",
            GrKonvaJsGlobalCompositeOperation.Luminosity => "'luminosity'",
            GrKonvaJsGlobalCompositeOperation.Multiply => "'multiply'",
            GrKonvaJsGlobalCompositeOperation.Overlay => "'overlay'",
            GrKonvaJsGlobalCompositeOperation.Saturation => "'saturation'",
            GrKonvaJsGlobalCompositeOperation.Screen => "'screen'",
            GrKonvaJsGlobalCompositeOperation.SoftLight => "'soft-light'",
            GrKonvaJsGlobalCompositeOperation.SourceAtop => "'source-atop'",
            GrKonvaJsGlobalCompositeOperation.SourceIn => "'source-in'",
            GrKonvaJsGlobalCompositeOperation.SourceOut => "'source-out'",
            GrKonvaJsGlobalCompositeOperation.SourceOver => "'source-over'",
            GrKonvaJsGlobalCompositeOperation.Xor => "'xor'",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
    
    public static string GetKonvaJsCode(this GrKonvaJsTextAlign value)
    {
        return value switch
        {
            GrKonvaJsTextAlign.Left => "'left'",
            GrKonvaJsTextAlign.Center => "'center'",
            GrKonvaJsTextAlign.Right => "'right'",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
    
    public static string GetKonvaJsCode(this GrKonvaJsTextBaseline value)
    {
        return value switch
        {
            GrKonvaJsTextBaseline.Top => "'top'",
            GrKonvaJsTextBaseline.Middle => "'middle'",
            GrKonvaJsTextBaseline.Bottom => "'bottom'",
            GrKonvaJsTextBaseline.Alphabetic => "'alphabetic'",
            GrKonvaJsTextBaseline.Hanging => "'hanging'",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }

    public static string GetKonvaJsCode(this GrKonvaJsTextDecoration value)
    {
        return value switch
        {
            GrKonvaJsTextDecoration.None => "''",
            GrKonvaJsTextDecoration.LineThrough => "'line-through'",
            GrKonvaJsTextDecoration.Underline => "'underline'",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
    
    public static string GetKonvaJsCode(this GrKonvaJsTextFontStyle value)
    {
        return value switch
        {
            GrKonvaJsTextFontStyle.Normal => "'normal'",
            GrKonvaJsTextFontStyle.Bold => "'bold'",
            GrKonvaJsTextFontStyle.Italic => "'italic'",
            GrKonvaJsTextFontStyle.ItalicBold => "'italic bold'",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
    
    public static string GetKonvaJsCode(this GrKonvaJsTextFontVariant value)
    {
        return value switch
        {
            GrKonvaJsTextFontVariant.Normal => "'normal'",
            GrKonvaJsTextFontVariant.SmallCaps => "'small-caps'",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
    
    public static string GetKonvaJsCode(this GrKonvaJsTextVerticalAlign value)
    {
        return value switch
        {
            GrKonvaJsTextVerticalAlign.Top => "'top'",
            GrKonvaJsTextVerticalAlign.Middle => "'middle'",
            GrKonvaJsTextVerticalAlign.Bottom => "'bottom'",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
    
    public static string GetKonvaJsCode(this GrKonvaJsTextWrap value)
    {
        return value switch
        {
            GrKonvaJsTextWrap.None => "'none'",
            GrKonvaJsTextWrap.Character => "'char'",
            GrKonvaJsTextWrap.Word => "'word'",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }


}