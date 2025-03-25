using System.Numerics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Constants;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Finishes;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FPP;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Web.Images;
using Newtonsoft.Json.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay;

public static class GrPovRayUtils
{
    public static bool IsNullOrEmpty(this IGrPovRayCodeElement? codeElement)
    {
        return codeElement is null || codeElement.IsEmptyCodeElement();
    }


    
    public static GrPovRayPolygon CreateImageRectangleZx(this GrVisualImageSetItem textureItem, double rectHeightRatio)
    {
        var rect = GrPovRayPolygon.CreateRectangleZx(1, 1);

        var pigment = new GrPovRayImageMapPigment(
            textureItem.GetPngImageFilePath(),
            GrPovRayImageMapBitmapType.Png
        )
        {
            Properties =
            {
                MapType = GrPovRayImageMapTypeValue.Planar,
                Once = GrPovRayFlagValue.True
            }
        };

        pigment.AffineMap.Translate(-0.5, -0.5, 0);

        rect.SetMaterial(pigment);

        var heightRatio = textureItem.ImageHeight * rectHeightRatio;

        rect.AffineMap
            .Scale(1, 1, textureItem.ImageWidthToHeight)
            .Scale(heightRatio);

        return rect;
    }
    
    public static GrPovRayPolygon CreateImageRectangleZx(this GrVisualImageSet textureSet, string groupName, string imageName, double rectHeight)
    {
        var group = textureSet.GetGroup(groupName);
        var item = group[imageName];

        return item.CreateImageRectangleZx(rectHeight / group.MaxImageHeight);

        //var rect = GrPovRayPolygon.CreateRectangleZx(1, 1);

        //var pigment = new GrPovRayImageMapPigment(
        //    item.PngImageFileName, 
        //    GrPovRayImageMapBitmapType.Png
        //);

        //pigment.AffineMap.Translate(-0.5, -0.5, 0);
        //pigment.Properties.Once = true;

        //rect.SetMaterial(pigment);

        //var heightRatio = rectHeight * (item.Height / (double)group.MaxImageHeight);

        //rect.AffineMap
        //    .Scale(1, 1, item.WidthToHeightRatio)
        //    .Scale(heightRatio);

        //return rect;
    }


    public static GrPovRayMaterial ToPovRayMaterial(this System.Drawing.Color color)
    {
        return GrPovRayMaterial.Create(color);
    }
    
    public static GrPovRayMaterial ToPovRayMaterial(this System.Drawing.Color color, IGrPovRayFinish finish)
    {
        return GrPovRayMaterial.Create(color, finish);
    }

    public static GrPovRayMaterial ToPovRayMaterial(this System.Drawing.Color color, GrPovRayFinishProperties finishProperties)
    {
        return GrPovRayMaterial.Create(color, finishProperties);
    }
    
    public static GrPovRayMaterial ToPovRayMaterial(this System.Drawing.Color color, string baseMaterialName)
    {
        return GrPovRayMaterial.Create(baseMaterialName, color);
    }
    
    public static GrPovRayMaterial ToPovRayMaterial(this System.Drawing.Color color, string baseMaterialName, IGrPovRayFinish finish)
    {
        return GrPovRayMaterial.Create(baseMaterialName, color, finish);
    }

    public static GrPovRayMaterial ToPovRayMaterial(this System.Drawing.Color color, string baseMaterialName, GrPovRayFinishProperties finishProperties)
    {
        return GrPovRayMaterial.Create(baseMaterialName, color, finishProperties);
    }
    
    public static GrPovRayMaterial ToPovRayMaterial(this System.Drawing.Color color, string baseMaterialName, string baseFinishName, GrPovRayFinishProperties? finishProperties = null)
    {
        return GrPovRayMaterial.Create(baseMaterialName, color, baseFinishName, finishProperties);
    }

    public static GrPovRayMaterial ToPovRayMaterial(this Color color)
    {
        return GrPovRayMaterial.Create(color);
    }
    
    public static GrPovRayMaterial ToPovRayMaterial(this Color color, IGrPovRayFinish finish)
    {
        return GrPovRayMaterial.Create(color, finish);
    }

    public static GrPovRayMaterial ToPovRayMaterial(this Color color, GrPovRayFinishProperties finishProperties)
    {
        return GrPovRayMaterial.Create(color, finishProperties);
    }
    
    public static GrPovRayMaterial ToPovRayMaterial(this Color color, string baseMaterialName)
    {
        return GrPovRayMaterial.Create(baseMaterialName, color);
    }
    
    public static GrPovRayMaterial ToPovRayMaterial(this Color color, string baseMaterialName, IGrPovRayFinish finish)
    {
        return GrPovRayMaterial.Create(baseMaterialName, color, finish);
    }

    public static GrPovRayMaterial ToPovRayMaterial(this Color color, string baseMaterialName, GrPovRayFinishProperties finishProperties)
    {
        return GrPovRayMaterial.Create(baseMaterialName, color, finishProperties);
    }
    
    public static GrPovRayMaterial ToPovRayMaterial(this Color color, string baseMaterialName, string baseFinishName, GrPovRayFinishProperties? finishProperties = null)
    {
        return GrPovRayMaterial.Create(baseMaterialName, color, baseFinishName, finishProperties);
    }
    
    public static GrPovRayMaterial ToPovRayMaterial(this IGrPovRayPigment pigment)
    {
        return GrPovRayMaterial.Create(pigment);
    }
    
    public static GrPovRayMaterial ToPovRayMaterial(this IGrPovRayPigment pigment, IGrPovRayFinish finish)
    {
        return GrPovRayMaterial.Create(pigment, finish);
    }

    public static GrPovRayMaterial ToPovRayMaterial(this IGrPovRayPigment pigment, GrPovRayFinishProperties finishProperties)
    {
        return GrPovRayMaterial.Create(pigment, finishProperties);
    }
    
    public static GrPovRayMaterial ToPovRayMaterial(this IGrPovRayPigment pigment, string baseMaterialName)
    {
        return GrPovRayMaterial.Create(baseMaterialName, pigment);
    }
    
    public static GrPovRayMaterial ToPovRayMaterial(this IGrPovRayPigment pigment, string baseMaterialName, IGrPovRayFinish finish)
    {
        return GrPovRayMaterial.Create(baseMaterialName, pigment, finish);
    }

    public static GrPovRayMaterial ToPovRayMaterial(this IGrPovRayPigment pigment, string baseMaterialName, GrPovRayFinishProperties finishProperties)
    {
        return GrPovRayMaterial.Create(baseMaterialName, pigment, finishProperties);
    }
    
    public static GrPovRayMaterial ToPovRayMaterial(this IGrPovRayPigment pigment, string baseMaterialName, string baseFinishName, GrPovRayFinishProperties? finishProperties = null)
    {
        return GrPovRayMaterial.Create(baseMaterialName, pigment, baseFinishName, finishProperties);
    }


    public static JToken GetPovRayJson(this bool value)
    {
        return JToken.FromObject(value);
    }
        
    public static JToken GetPovRayJson(this int value)
    {
        return JToken.FromObject(value);
    }
        
    public static JToken GetPovRayJson(this float value)
    {
        return JToken.FromObject(value);
    }
        
    public static JToken GetPovRayJson(this double value)
    {
        return JToken.FromObject((float) value);
    }
        
    public static JToken GetPovRayJson(this SizeF value)
    {
        return new JArray(
            value.Width,
            value.Height
        );
    }

    public static JToken GetPovRayJson(this IPair<double> value)
    {
        return new JArray(
            (float) value.Item1,
            (float) value.Item2
        );
    }

    public static JToken GetPovRayJson(this ILinFloat64Vector2D value)
    {
        return new JArray(
            (float) value.X,
            (float) value.Y
        );
    }
        
    public static JToken GetPovRayJson(this ITriplet<Float64Scalar> value)
    {
        return new JArray(
            (float) value.Item1,
            (float) value.Item2,
            (float) value.Item3
        );
    }

    public static JToken GetPovRayJson(this ILinFloat64Vector3D value)
    {
        return new JArray(
            (float) value.X,
            (float) value.Y,
            (float) value.Z
        );
    }
        
    public static JToken GetPovRayJson(this IQuad<double> value)
    {
        return new JArray(
            (float) value.Item1,
            (float) value.Item2,
            (float) value.Item3,
            (float) value.Item4
        );
    }

    public static JToken GetPovRayJson(this ILinFloat64Vector4D value)
    {
        return new JArray(
            (float) value.X,
            (float) value.Y,
            (float) value.Z,
            (float) value.W
        );
    }
        
    public static JToken GetPovRayJson(this LinFloat64Quaternion value)
    {
        return new JArray(
            (float) -value.ScalarI,
            (float) -value.ScalarJ,
            (float) -value.ScalarK,
            (float) value.Scalar
        );
    }
        
    public static JToken GetPovRayJson(this Quaternion value)
    {
        return new JArray(
            value.X,
            value.Y,
            value.Z,
            value.W
        );
    }

    public static JToken GetPovRayJson(this System.Drawing.Color color, bool useAlpha = false)
    {
        if (useAlpha)
            return new JArray(
                (color.R / 255f).GetPovRayJson(),
                (color.G / 255f).GetPovRayJson(),
                (color.B / 255f).GetPovRayJson(),
                (color.A / 255f).GetPovRayJson()
            );

        return new JArray(
            (color.R / 255f).GetPovRayJson(),
            (color.G / 255f).GetPovRayJson(),
            (color.B / 255f).GetPovRayJson()
        );
    }

    public static JToken GetPovRayJson(this Color color, bool useAlpha = false)
    {
        if (useAlpha)
        {
            var c = color.ToPixel<Rgba32>();

            return new JArray(
                (c.R / 255f).GetPovRayJson(),
                (c.G / 255f).GetPovRayJson(),
                (c.B / 255f).GetPovRayJson(),
                (c.A / 255f).GetPovRayJson()
            );
        }
        else
        {
            var c = color.ToPixel<Rgb24>();

            return new JArray(
                (c.R / 255f).GetPovRayJson(),
                (c.G / 255f).GetPovRayJson(),
                (c.B / 255f).GetPovRayJson()
            );
        }
    }


    public static string GetPovRayCode(this bool value)
    {
        return value ? "on" : "off";
    }

    public static string GetPovRayCode(this int value)
    {
        return value.ToString();
    }

    public static string GetPovRayCode(this float value)
    {
        return value.ToString("G");
    }

    public static string GetPovRayCode(this double value)
    {
        return ((float) value).ToString("G");
    }
    
    public static string GetPovRayCode(this Float64Scalar value)
    {
        return ((float) value.ScalarValue).ToString("G");
    }
    
    public static string GetPovRayCode(this LinFloat64Angle value)
    {
        return ((float) value.DegreesValue).ToString("G");
    }

    public static string GetPovRayCode(this SizeF value)
    {
        return value.Width == value.Height
            ? value.Width.GetPovRayCode()
            : $"{{ width: {value.Width.GetPovRayCode()}, height: {value.Height.GetPovRayCode()} }}";
    }

    //public static string GetPovRayCode(this System.Drawing.Color color, bool useAlpha = false)
    //{
    //    return useAlpha 
    //        ? $"BABYLON.Color4.FromInts({color.R}, {color.G}, {color.B}, {color.A})" 
    //        : $"FromInts({color.R}, {color.G}, {color.B})";
    //}
    
    public static string GetPovRayCodeRgb(this Color color)
    {
        var c = color.ToPixel<Rgb24>();

        return $"color rgb <{c.R / 255f}, {c.G / 255f}, {c.B / 255f}>";
    }
    
    public static string GetPovRayCodeRgbf(this Color color)
    {
        var c = color.ToPixel<Rgba32>();

        return $"color rgbf <{c.R / 255f}, {c.G / 255f}, {c.B / 255f}, {c.A / 255f}>";
    }
    
    public static string GetPovRayCodeRgbt(this Color color)
    {
        var c = color.ToPixel<Rgba32>();

        return $"color rgbt <{c.R / 255f}, {c.G / 255f}, {c.B / 255f}, {c.A / 255f}>";
    }
    
    public static string GetPovRayCodeRgbft(this IQuint<GrPovRayFloat32Value> color)
    {
        var r = color.Item1.GetAttributeValueCode();
        var g = color.Item2.GetAttributeValueCode();
        var b = color.Item3.GetAttributeValueCode();
        var f = color.Item4.GetAttributeValueCode();
        var t = color.Item5.GetAttributeValueCode();
        
        if (f == "0" && t == "0")
            return $"color rgb <{r}, {g}, {b}>";

        if (t == "0")
            return $"color rgbf <{r}, {g}, {b}, {f}>";

        if (f == "0")
            return $"color rgbt <{r}, {g}, {b}, {t}>";

        return $"color <{r}, {g}, {b}, {f}, {t}>";
    }

    public static string GetPovRayCode(this LinBasisVector3D axis)
    {
        return axis switch
        {
            LinBasisVector3D.Px => "x",
            LinBasisVector3D.Nx => "(-x)",
            LinBasisVector3D.Py => "y",
            LinBasisVector3D.Ny => "(-y)",
            LinBasisVector3D.Pz => "z",
            _ => "(-z)"
        };
    }
    
    public static string GetPovRayCode(this IPair<int> vector)
    {
        return $"{vector.Item1}, {vector.Item2}";
    }

    public static string GetPovRayCode(this IPair<Float64Scalar> vector)
    {
        return $"<{(float) vector.Item1:G}, {(float) vector.Item2:G}>";
    }

    public static string GetPovRayCode(this ITriplet<Float64Scalar> vector)
    {
        return $"<{(float) vector.Item1:G}, {(float) vector.Item2:G}, {(float) vector.Item3:G}>";
    }
        
    public static string GetPovRayCode(this IQuad<Float64Scalar> vector)
    {
        return $"<{(float) vector.Item1:G}, {(float) vector.Item2:G}, {(float) vector.Item3:G}, {(float) vector.Item4:G}>";
    }
        
    public static string GetPovRayCode(this IQuint<Float64Scalar> vector)
    {
        return $"<{(float) vector.Item1:G}, {(float) vector.Item2:G}, {(float) vector.Item3:G}, {(float) vector.Item4:G}, {(float) vector.Item5:G}>";
    }
    
    public static string GetPovRayCode(this IPair<GrPovRayFloat32Value> vector)
    {
        return $"<{vector.Item1.GetPovRayCode()}, {vector.Item2.GetPovRayCode()}>";
    }

    public static string GetPovRayCode(this ITriplet<GrPovRayFloat32Value> vector)
    {
        return $"<{vector.Item1.GetPovRayCode()}, {vector.Item2.GetPovRayCode()}, {vector.Item3.GetPovRayCode()}>";
    }
    
    public static string GetPovRayCode(this IQuad<GrPovRayFloat32Value> vector)
    {
        return $"<{vector.Item1.GetPovRayCode()}, {vector.Item2.GetPovRayCode()}, {vector.Item3.GetPovRayCode()}, {vector.Item4.GetPovRayCode()}>";
    }
    
    public static string GetPovRayCode(this IQuint<GrPovRayFloat32Value> vector)
    {
        return $"<{vector.Item1.GetPovRayCode()}, {vector.Item2.GetPovRayCode()}, {vector.Item3.GetPovRayCode()}, {vector.Item4.GetPovRayCode()}, {vector.Item5.GetPovRayCode()}>";
    }

    //public static string GetPovRayCode(this LinFloat64Quaternion quaternion)
    //{
    //    var q1 = (float) quaternion.Scalar;
    //    var qi = (float) -quaternion.ScalarI;
    //    var qj = (float) -quaternion.ScalarJ;
    //    var qk = (float) -quaternion.ScalarK;

    //    return $"new BABYLON.Quaternion({qi:G}, {qj:G}, {qk:G}, {q1:G})";
    //}

    //public static string GetPovRayCode(this Quaternion quaternion)
    //{
    //    return $"new BABYLON.Quaternion({quaternion.X:G}, {quaternion.Y:G}, {quaternion.Z:G}, {quaternion.W:G})";
    //}
        
    public static string GetPovRayCode(this IEnumerable<int> vectorList)
    {
        return vectorList.Select(
            vector => vector.GetPovRayCode()
        ).Concatenate(", ", "[", "]");
    }
        
    public static string GetPovRayArrayCode(this IEnumerable<string> itemsList)
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
        
    public static string GetPovRayCodeRgb(this IEnumerable<Color> colorList)
    {
        return colorList.Select(
            vector => vector.GetPovRayCodeRgb()
        ).GetPovRayArrayCode();
    }
        
    public static string GetPovRayCodeRgb(this IEnumerable<IReadOnlyList<Color>> colorArrayList)
    {
        return colorArrayList.Select(
            vector => vector.GetPovRayCodeRgb()
        ).GetPovRayArrayCode();
    }

    public static string GetPovRayCode(this IEnumerable<IPair<Float64Scalar>> vectorList)
    {
        return vectorList.Select(
            vector => vector.GetPovRayCode()
        ).GetPovRayArrayCode();
    }

    public static string GetPovRayCode(this IEnumerable<ITriplet<Float64Scalar>> vectorList)
    {
        return vectorList.Select(
            vector => vector.GetPovRayCode()
        ).GetPovRayArrayCode();
    }
        
    public static string GetPovRayCode(this IEnumerable<IReadOnlyList<ITriplet<Float64Scalar>>> vectorList)
    {
        return vectorList.Select(
            vector => vector.GetPovRayCode()
        ).GetPovRayArrayCode();
    }

    public static string GetPovRayCode(this IEnumerable<IQuad<Float64Scalar>> vectorList)
    {
        return vectorList.Select(
            vector => vector.GetPovRayCode()
        ).GetPovRayArrayCode();
    }
    
    public static string GetPovRayMatrixCode(ITriplet<Float64Scalar> affineMapColumn1, ITriplet<Float64Scalar> affineMapColumn2, ITriplet<Float64Scalar> affineMapColumn3)
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("matrix <")
            .IncreaseIndentation()
            .AppendAtNewLine($"{affineMapColumn1.Item1.GetPovRayCode()}, {affineMapColumn1.Item2.GetPovRayCode()}, {affineMapColumn1.Item3.GetPovRayCode()},")
            .AppendAtNewLine($"{affineMapColumn2.Item1.GetPovRayCode()}, {affineMapColumn2.Item2.GetPovRayCode()}, {affineMapColumn2.Item3.GetPovRayCode()},")
            .AppendAtNewLine($"{affineMapColumn3.Item1.GetPovRayCode()}, {affineMapColumn3.Item2.GetPovRayCode()}, {affineMapColumn3.Item3.GetPovRayCode()},")
            .AppendAtNewLine($"{Float64Scalar.Zero.GetPovRayCode()}, {Float64Scalar.Zero.GetPovRayCode()}, {Float64Scalar.Zero.GetPovRayCode()}")
            .DecreaseIndentation()
            .AppendAtNewLine(">");

        return composer.ToString();
    }

    public static string GetPovRayMatrixCode(ITriplet<Float64Scalar> affineMapColumn1, ITriplet<Float64Scalar> affineMapColumn2, ITriplet<Float64Scalar> affineMapColumn3, ITriplet<Float64Scalar> affineMapColumn4)
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("matrix <")
            .IncreaseIndentation()
            .AppendAtNewLine($"{affineMapColumn1.Item1.GetPovRayCode()}, {affineMapColumn1.Item2.GetPovRayCode()}, {affineMapColumn1.Item3.GetPovRayCode()},")
            .AppendAtNewLine($"{affineMapColumn2.Item1.GetPovRayCode()}, {affineMapColumn2.Item2.GetPovRayCode()}, {affineMapColumn2.Item3.GetPovRayCode()},")
            .AppendAtNewLine($"{affineMapColumn3.Item1.GetPovRayCode()}, {affineMapColumn3.Item2.GetPovRayCode()}, {affineMapColumn3.Item3.GetPovRayCode()},")
            .AppendAtNewLine($"{affineMapColumn4.Item1.GetPovRayCode()}, {affineMapColumn4.Item2.GetPovRayCode()}, {affineMapColumn4.Item3.GetPovRayCode()}")
            .DecreaseIndentation()
            .AppendAtNewLine(">");

        return composer.ToString();
    }

    public static string GetPovRayMatrixCode(this IFloat64AffineMap3D affineMap)
    {
        var m = affineMap.GetSquareMatrix4();

        var composer = new LinearTextComposer();

        composer
            .AppendLine("matrix <")
            .IncreaseIndentation()
            .AppendAtNewLine($"{m.Scalar00.GetPovRayCode()}, {m.Scalar10.GetPovRayCode()}, {m.Scalar20.GetPovRayCode()},")
            .AppendAtNewLine($"{m.Scalar01.GetPovRayCode()}, {m.Scalar11.GetPovRayCode()}, {m.Scalar21.GetPovRayCode()},")
            .AppendAtNewLine($"{m.Scalar02.GetPovRayCode()}, {m.Scalar12.GetPovRayCode()}, {m.Scalar22.GetPovRayCode()},")
            .AppendAtNewLine($"{m.Scalar03.GetPovRayCode()}, {m.Scalar13.GetPovRayCode()}, {m.Scalar23.GetPovRayCode()}")
            .DecreaseIndentation()
            .AppendAtNewLine(">");

        return composer.ToString();
    }

    public static string GetPovRayCode(this IEnumerable<SparseCodeAttributeValue> valueList)
    {
        return valueList.Select(
            value => value.GetAttributeValueCode()
        ).GetPovRayArrayCode();
    }

    public static string GetPovRayCode(this IPair<ITriplet<Float64Scalar>> vectorList)
    {
        var p1 = vectorList.Item1.GetPovRayCode();
        var p2 = vectorList.Item2.GetPovRayCode();

        return $"[{p1}, {p2}]";
    }
        
    public static string GetPovRayCode(this ITriplet<ITriplet<Float64Scalar>> vectorList)
    {
        var p1 = vectorList.Item1.GetPovRayCode();
        var p2 = vectorList.Item2.GetPovRayCode();
        var p3 = vectorList.Item3.GetPovRayCode();

        return $"[{p1}, {p2}, {p3}]";
    }
        
    public static string GetPovRayCode(this IQuad<ITriplet<Float64Scalar>> vectorList)
    {
        var p1 = vectorList.Item1.GetPovRayCode();
        var p2 = vectorList.Item2.GetPovRayCode();
        var p3 = vectorList.Item3.GetPovRayCode();
        var p4 = vectorList.Item4.GetPovRayCode();

        return $"[{p1}, {p2}, {p3}, {p4}]";
    }
    
    public static string GetPovRayCode(this GrPovRayNamedColor namedColor)
    {
        return namedColor switch
        {
            GrPovRayNamedColor.Red => "Red",
            GrPovRayNamedColor.Green => "Green",
            GrPovRayNamedColor.Blue => "Blue",
            GrPovRayNamedColor.Yellow => "Yellow",
            GrPovRayNamedColor.Cyan => "Cyan",
            GrPovRayNamedColor.Magenta => "Magenta",
            GrPovRayNamedColor.Clear => "Clear",
            GrPovRayNamedColor.White => "White",
            GrPovRayNamedColor.Black => "Black",
            _ => throw new ArgumentOutOfRangeException(nameof(namedColor), namedColor, null)
        };
    }

    public static string GetPovRayCode(this GrPovRayImageMapBitmapType bitmapType)
    {
        return bitmapType switch
        {
            GrPovRayImageMapBitmapType.Undefined => string.Empty,
            GrPovRayImageMapBitmapType.Exr => "exr",
            GrPovRayImageMapBitmapType.Gif => "gif",
            GrPovRayImageMapBitmapType.Hdr => "hdr",
            GrPovRayImageMapBitmapType.Iff => "iff",
            GrPovRayImageMapBitmapType.Jpeg => "jpeg",
            GrPovRayImageMapBitmapType.Pgm => "pgm",
            GrPovRayImageMapBitmapType.Png => "png",
            GrPovRayImageMapBitmapType.Ppm => "ppm",
            GrPovRayImageMapBitmapType.Sys => "sys",
            GrPovRayImageMapBitmapType.Tga => "tga",
            GrPovRayImageMapBitmapType.Tiff => "tiff",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static string GetPovRayCode(this GrPovRayImageMapType mapType)
    {
        return ((int)mapType).ToString();
    }
    
    public static string GetPovRayCode(this GrPovRayImageMapInterpolateType mapType)
    {
        return ((int)mapType).ToString();
    }
    
    public static string GetPovRayCode(this GrPovRaySamplingMethod samplingMethod)
    {
        return ((int)samplingMethod).ToString();
    }
    
    public static string GetPovRayCode(this GrPovRayOutputFileType outputFileType)
    {
        return outputFileType switch
        {
            GrPovRayOutputFileType.Bitmap => "\"B\"",
            GrPovRayOutputFileType.Png => "\"N\"",
            GrPovRayOutputFileType.Jpeg => "\"J\"",
            GrPovRayOutputFileType.Ppm => "\"P\"",
            GrPovRayOutputFileType.CompressedTarGa24 => "\"C\"",
            GrPovRayOutputFileType.UncompressedTarGa24 => "\"T\"",
            GrPovRayOutputFileType.OpenExrHdr => "\"E\"",
            GrPovRayOutputFileType.RadianceHdr => "\"H\"",
            GrPovRayOutputFileType.SystemSpecific => "\"S\"",
            _ => throw new ArgumentOutOfRangeException()
        };
    }


    //public static string GetPovRayCode(this GrPovRayKeyFrameDictionary<double> keyFrameList)
    //{
    //    var keyFrameArray = keyFrameList.ToImmutableArray();

    //    if (keyFrameArray.Length == 0)
    //        return "[]";

    //    if (keyFrameArray.Length <= 3)
    //        return keyFrameArray
    //            .Select(indexValuePair =>
    //                $"{{frame: {indexValuePair.Key.GetPovRayCode()}, value: {indexValuePair.Value.GetPovRayCode()}}}"
    //            ).Concatenate($", ", "[", "]");

    //    var itemsText =
    //        keyFrameArray
    //            .Select(indexValuePair =>
    //                $"{{frame: {indexValuePair.Key.GetPovRayCode()}, value: {indexValuePair.Value.GetPovRayCode()}}}"
    //            ).Concatenate($",{Environment.NewLine}");

    //    return new LinearTextComposer()
    //        .AppendLine("[")
    //        .IncreaseIndentation()
    //        .Append(itemsText)
    //        .DecreaseIndentation()
    //        .AppendAtNewLine("]")
    //        .ToString();
    //}
        
    //public static string GetBabylonCode(this GrPovRayKeyFrameDictionary<LinFloat64Vector2D> keyFrameList)
    //{
    //    var keyFrameArray = keyFrameList.ToImmutableArray();

    //    if (keyFrameArray.Length == 0)
    //        return "[]";

    //    if (keyFrameArray.Length == 1)
    //        return keyFrameArray
    //            .Select(indexValuePair =>
    //                $"{{frame: {indexValuePair.Key.GetPovRayCode()}, value: {indexValuePair.Value.GetPovRayCode()}}}"
    //            ).Concatenate($", ", "[", "]");

    //    var itemsText =
    //        keyFrameArray
    //            .Select(indexValuePair =>
    //                $"{{frame: {indexValuePair.Key.GetPovRayCode()}, value: {indexValuePair.Value.GetPovRayCode()}}}"
    //            ).Concatenate($",{Environment.NewLine}");

    //    return new LinearTextComposer()
    //        .AppendLine("[")
    //        .IncreaseIndentation()
    //        .Append(itemsText)
    //        .DecreaseIndentation()
    //        .AppendAtNewLine("]")
    //        .ToString();
    //}
        
    //public static string GetBabylonCode(this GrPovRayKeyFrameDictionary<LinFloat64Vector3D> keyFrameList)
    //{
    //    var keyFrameArray = keyFrameList.ToImmutableArray();

    //    if (keyFrameArray.Length == 0)
    //        return "[]";

    //    if (keyFrameArray.Length == 1)
    //        return keyFrameArray
    //            .Select(indexValuePair =>
    //                $"{{frame: {indexValuePair.Key.GetPovRayCode()}, value: {indexValuePair.Value.GetPovRayCode()}}}"
    //            ).Concatenate($", ", "[", "]");

    //    var itemsText =
    //        keyFrameArray
    //            .Select(indexValuePair =>
    //                $"{{frame: {indexValuePair.Key.GetPovRayCode()}, value: {indexValuePair.Value.GetPovRayCode()}}}"
    //            ).Concatenate($",{Environment.NewLine}");

    //    return new LinearTextComposer()
    //        .AppendLine("[")
    //        .IncreaseIndentation()
    //        .Append(itemsText)
    //        .DecreaseIndentation()
    //        .AppendAtNewLine("]")
    //        .ToString();
    //}

    //public static string GetPovRayCode(this GrPovRayKeyFrameDictionary<LinFloat64Quaternion> keyFrameList)
    //{
    //    var keyFrameArray = keyFrameList.ToImmutableArray();

    //    if (keyFrameArray.Length == 0)
    //        return "[]";

    //    if (keyFrameArray.Length == 1)
    //        return keyFrameArray
    //            .Select(indexValuePair =>
    //                $"{{frame: {indexValuePair.Key.GetPovRayCode()}, value: {indexValuePair.Value.GetPovRayCode()}}}"
    //            ).Concatenate($", ", "[", "]");

    //    var itemsText =
    //        keyFrameArray
    //            .Select(indexValuePair =>
    //                $"{{frame: {indexValuePair.Key.GetPovRayCode()}, value: {indexValuePair.Value.GetPovRayCode()}}}"
    //            ).Concatenate($",{Environment.NewLine}");

    //    return new LinearTextComposer()
    //        .AppendLine("[")
    //        .IncreaseIndentation()
    //        .Append(itemsText)
    //        .DecreaseIndentation()
    //        .AppendAtNewLine("]")
    //        .ToString();
    //}


    public static void AddToScene(this IGrPovRayStatement st, GrPovRayScene scene)
    {
        scene.Statements.Add(st);
    }
}