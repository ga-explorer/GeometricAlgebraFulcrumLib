using System;
using System.Drawing;
using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Content;
using GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Values;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold;

public static class HtmlUtils
{
        
    //public static void ConvertHtmlFileToPng(string svgFilePath, string pngFilePath)
    //{
    //    var svgDocument = HtmlDocument.Open(svgFilePath);
            
    //    using (var smallBitmap = svgDocument.Draw())
    //    {
    //        var width = smallBitmap.Width;
    //        var height = smallBitmap.Height;

    //        if (width != 2000)// I resize my bitmap
    //        {
    //            width = 2000;
    //            height = 2000 / smallBitmap.Width * height;
    //        }

    //        using (var bitmap = svgDocument.Draw(width, height))//I render again
    //        {
    //            bitmap.Save(pngFilePath, ImageFormat.Png);
    //        }
    //    }
    //}

    public static string ToHtmlNumberText(this double numberValue, bool isPercent = false)
    {
        var numberText = Math.Abs(numberValue % 1) <= 0
            ? numberValue.ToString("#########0")
            : numberValue.ToString("G");

        return isPercent ? numberText + "%" : numberText;
    }

    public static string ToHtmlLengthText(this double lengthValue, HtmlValueLengthUnit unit)
    {
        return new StringBuilder(32)
            .Append(lengthValue.ToHtmlNumberText())
            .Append(unit?.ValueText ?? string.Empty)
            .ToString();
    }

    public static string ToHtmlAngleText(this double angleValue, HtmlValueAngleUnit unit)
    {
        return new StringBuilder(32)
            .Append(angleValue.ToHtmlNumberText())
            .Append(unit?.ValueText ?? string.Empty)
            .ToString();
    }

    public static HtmlContentText ToHtmlContentText(this string text)
    {
        return HtmlContentText.Create(text);
    }


    public static string ToHtmlColorHexText(this Color c)
    {
        return new StringBuilder()
            .Append("#")
            .Append(c.R.ToString("x2"))
            .Append(c.G.ToString("x2"))
            .Append(c.B.ToString("x2"))
            .ToString();
    }

    public static string ToHtmlColorHexText(byte r, byte g, byte b)
    {
        return new StringBuilder()
            .Append("#")
            .Append(r.ToString("x2"))
            .Append(g.ToString("x2"))
            .Append(b.ToString("x2"))
            .ToString();
    }

    public static string ToHtmlColorRgbText(this Color c)
    {
        return new StringBuilder()
            .Append("rgb(")
            .Append(c.R)
            .Append(",")
            .Append(c.G)
            .Append(",")
            .Append(c.B)
            .Append(")")
            .ToString();
    }

    public static string ToHtmlColorRgbText(byte r, byte g, byte b)
    {
        return new StringBuilder()
            .Append("rgb(")
            .Append(r)
            .Append(",")
            .Append(g)
            .Append(",")
            .Append(b)
            .Append(")")
            .ToString();
    }

    public static string ToHtmlColorRgbText(double r, double g, double b)
    {
        if (r < 0 || r > 1 || g < 0 || g > 1 || b < 0 || b > 1)
            throw new ArgumentOutOfRangeException();

        return new StringBuilder()
            .Append("rgb(")
            .Append((r * 100).ToHtmlNumberText())
            .Append("%, ")
            .Append((g * 100).ToHtmlNumberText())
            .Append("%, ")
            .Append((b * 100).ToHtmlNumberText())
            .Append("%)")
            .ToString();
    }
}