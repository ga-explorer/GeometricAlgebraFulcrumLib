using System.Drawing.Text;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using ImageMagick;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Tiff;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Content;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Values;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Html.Media;

public static class WclHtmlImageUtils
{
    public static string ToSvgNumberText(this double numberValue, bool isPercent = false)
    {
        var numberText = Math.Abs(numberValue % 1) <= 0
            ? numberValue.ToString("#########0")
            : numberValue.ToString("G");

        return isPercent ? numberText + "%" : numberText;
    }

    public static string ToSvgPercentText(this double numberValue)
    {
        return (numberValue * 100).ToSvgNumberText(true);
    }

    public static string ToSvgLengthText(this double lengthValue, SvgLengthUnit? unit)
    {
        return new StringBuilder(32)
            .Append(lengthValue.ToSvgNumberText())
            .Append(unit?.ValueText ?? string.Empty)
            .ToString();
    }

    public static string ToSvgAngleText(this double angleValue, SvgValueAngleUnit? unit)
    {
        return new StringBuilder(32)
            .Append(angleValue.ToSvgNumberText())
            .Append(unit?.ValueText ?? string.Empty)
            .ToString();
    }

    public static SvgContentText ToSvgContentText(this string text)
    {
        return SvgContentText.Create(text);
    }


    public static string ToSvgColorHexText(this Color c)
    {
        //TODO: Test this
        //return c.ToHex()

        var color = c.ToPixel<Rgba32>();

        return $"#{color.R:x2}{color.G:x2}{color.B:x2}{color.A:x2}";
    }

    public static string ToSvgColorHexText(byte gray)
    {
        return $"#{gray:x2}{gray:x2}{gray:x2}";
    }

    public static string ToSvgColorHexText(byte r, byte g, byte b)
    {
        return $"#{r:x2}{g:x2}{b:x2}";
    }

    public static string ToSvgColorHexText(byte r, byte g, byte b, byte a)
    {
        return $"#{r:x2}{g:x2}{b:x2}{a:x2}";
    }

    public static string ToSvgColorRgbText(this Color c)
    {
        var color = c.ToPixel<Rgb24>();

        return $"rgb({color.R}, {color.G}, {color.B})";
    }

    public static string ToSvgColorRgbaText(this Color c)
    {
        var color = c.ToPixel<Rgba32>();

        return $"rgba({color.R}, {color.G}, {color.B}, {color.A})";
    }

    public static string ToSvgColorRgbText(byte r, byte g, byte b)
    {
        return $"rgb({r}, {g}, {b})";
    }

    public static string ToSvgColorRgbaText(byte r, byte g, byte b, byte a)
    {
        return $"rgba({r}, {g}, {b}, {a})";
    }

    public static string ToSvgColorRgbText(double r, double g, double b)
    {
        if (r is < 0 or > 1 || g is < 0 or > 1 || b is < 0 or > 1)
            throw new ArgumentOutOfRangeException();

        var rp = r.ToSvgPercentText();
        var gp = g.ToSvgPercentText();
        var bp = b.ToSvgPercentText();

        return $"rgb({rp}, {gp}, {bp})";
    }

    public static string ToSvgColorRgbaText(double r, double g, double b, double a)
    {
        if (r is < 0 or > 1 || g is < 0 or > 1 || b is < 0 or > 1 || a is < 0 or > 1)
            throw new ArgumentOutOfRangeException();

        var rp = r.ToSvgPercentText();
        var gp = g.ToSvgPercentText();
        var bp = b.ToSvgPercentText();
        var ap = a.ToSvgPercentText();

        return $"rgba({rp}, {gp}, {bp}, {ap})";
    }


    public static string GetMediaTypeString(this WclHtmlImageMediaType mediaType)
    {
        return mediaType switch
        {
            WclHtmlImageMediaType.Bmp => "image/bmp",
            WclHtmlImageMediaType.Gif => "image/gif",
            WclHtmlImageMediaType.Ico => "image/vnd.microsoft.icon",
            WclHtmlImageMediaType.Jpeg => "image/jpeg",
            WclHtmlImageMediaType.Png => "image/png",
            WclHtmlImageMediaType.Svg => "image/svg+xml",
            WclHtmlImageMediaType.Tiff => "image/tiff",
            WclHtmlImageMediaType.Webp => "image/webp",

            _ => throw new NotImplementedException()
        };
    }

    public static IImageEncoder GetImageSharpEncoder(this WclHtmlImageMediaType mediaType)
    {
        return mediaType switch
        {
            WclHtmlImageMediaType.Png => new PngEncoder(),
            WclHtmlImageMediaType.Bmp => new BmpEncoder(),
            WclHtmlImageMediaType.Gif => new GifEncoder(),
            WclHtmlImageMediaType.Jpeg => new JpegEncoder(),
            WclHtmlImageMediaType.Tiff => new TiffEncoder(),
            WclHtmlImageMediaType.Webp => new WebpEncoder(),

            _ => throw new ArgumentOutOfRangeException(nameof(mediaType), mediaType, "Invalid mediaType value")
        };
    }

    public static IImageDecoder GetImageSharpDecoder(this WclHtmlImageMediaType mediaType)
    {
        return mediaType switch
        {
            WclHtmlImageMediaType.Png => PngDecoder.Instance,
            WclHtmlImageMediaType.Bmp => BmpDecoder.Instance,
            WclHtmlImageMediaType.Gif => GifDecoder.Instance,
            WclHtmlImageMediaType.Jpeg => JpegDecoder.Instance,
            WclHtmlImageMediaType.Tiff => TiffDecoder.Instance,
            WclHtmlImageMediaType.Webp => WebpDecoder.Instance,

            _ => throw new ArgumentOutOfRangeException(nameof(mediaType), mediaType, "Invalid mediaType value")
        };
    }

    public static MagickFormat GetImageMagickFormat(this WclHtmlImageMediaType mediaType)
    {
        return mediaType switch
        {
            WclHtmlImageMediaType.Png => MagickFormat.Png32,
            WclHtmlImageMediaType.Bmp => MagickFormat.Bmp,
            WclHtmlImageMediaType.Gif => MagickFormat.Gif,
            WclHtmlImageMediaType.Jpeg => MagickFormat.Jpeg,
            WclHtmlImageMediaType.Tiff => MagickFormat.Tiff,
            WclHtmlImageMediaType.Webp => MagickFormat.WebP,
            WclHtmlImageMediaType.Ico => MagickFormat.Ico,
            WclHtmlImageMediaType.Svg => MagickFormat.Svg,

            _ => throw new ArgumentOutOfRangeException(nameof(mediaType), mediaType, "Invalid mediaType value")
        };
    }


    public static Rectangle GetPngWhiteSpace(this Image<Rgba32> image, Rgba32 whiteSpaceColor)
    {
        var topRowCount = 0;
        for (var y = 0; y < image.Height; y++)
        {
            var isWhitespaceRow = true;
            for (var x = 0; x < image.Width; x++)
            {
                if (image[x, y] == whiteSpaceColor)
                    continue;

                isWhitespaceRow = false;
                break;
            }

            if (isWhitespaceRow)
                topRowCount++;
            else
                break;
        }

        var bottomRowCount = 0;
        for (var y = image.Height - 1; y >= 0; y--)
        {
            var isWhitespaceRow = true;
            for (var x = 0; x < image.Width; x++)
            {
                if (image[x, y] == whiteSpaceColor)
                    continue;

                isWhitespaceRow = false;
                break;
            }

            if (isWhitespaceRow)
                bottomRowCount++;
            else
                break;
        }

        var leftColumnCount = 0;
        for (var x = 0; x < image.Width; x++)
        {
            var isWhitespaceColumn = true;
            for (var y = 0; y < image.Height; y++)
            {
                if (image[x, y] == whiteSpaceColor)
                    continue;

                isWhitespaceColumn = false;
                break;
            }

            if (isWhitespaceColumn)
                leftColumnCount++;
            else
                break;
        }

        var rightColumnCount = 0;
        for (var x = image.Width - 1; x >= 0; x--)
        {
            var isWhitespaceColumn = true;
            for (var y = 0; y < image.Height; y++)
            {
                if (image[x, y] == whiteSpaceColor)
                    continue;

                isWhitespaceColumn = false;
                break;
            }

            if (isWhitespaceColumn)
                rightColumnCount++;
            else
                break;
        }

        var cropWidth = image.Width - (rightColumnCount + leftColumnCount);
        var cropHeight = image.Height - (topRowCount + bottomRowCount);

        return new Rectangle(
            leftColumnCount,
            topRowCount,
            cropWidth,
            cropHeight
        );
    }

    public static Image<Rgba32> CropPngWhiteSpace(this Image<Rgba32> image, Rgba32 whiteSpaceColor, int marginSize)
    {
        var cropRectangle =
            image.GetPngWhiteSpace(whiteSpaceColor);

        if (cropRectangle.Width <= 0 || cropRectangle.Height <= 0)
            return image;

        if (marginSize > 0)
            return image.Clone(context =>
                context.Crop(cropRectangle).Resize(
                    new ResizeOptions
                    {
                        PadColor = whiteSpaceColor,
                        Compand = false,
                        Mode = ResizeMode.BoxPad,
                        Size = new Size(cropRectangle.Width + 2 * marginSize, cropRectangle.Height + 2 * marginSize),
                        TargetRectangle = new Rectangle(marginSize, marginSize, cropRectangle.Width, cropRectangle.Height)
                    }
                )
            );

        return image.Clone(context =>
            context.Crop(cropRectangle)
        );
    }

    public static Image GetImageFromByteArray(this byte[] byteArray)
    {
        using var stream = new MemoryStream();

        stream.Write(byteArray);

        return Image.Load(stream);
    }

    ///// <summary>
    ///// Method that uses the ImageConverter object in .Net Framework to convert a byte array, 
    ///// presumably containing a JPEG or PNG file image, into a Bitmap object, which can also be 
    ///// used as an Image object.
    ///// </summary>
    ///// <param name="byteArray">byte array containing JPEG or PNG file image or similar</param>
    ///// <returns>Bitmap object if it works, else exception is thrown</returns>
    //public static System.Drawing.Bitmap GetImageFromByteArray(byte[] byteArray)
    //{
    //    var tc = TypeDescriptor.GetConverter(typeof(System.Drawing.Bitmap));

    //    //var bm = tc.ConvertFrom(byteArray);

    //    var bm = (System.Drawing.Bitmap) System.Drawing.ImageConverter.ConvertFrom(byteArray);

    //    if (bm != null && (
    //            bm.HorizontalResolution != (int)bm.HorizontalResolution ||
    //            bm.VerticalResolution != (int)bm.VerticalResolution
    //        ))
    //    {
    //        // Correct a strange glitch that has been observed in the test program when converting 
    //        //  from a PNG file image created by CopyImageToByteArray() - the dpi value "drifts" 
    //        //  slightly away from the nominal integer value
    //        bm.SetResolution((int)(bm.HorizontalResolution + 0.5f),
    //            (int)(bm.VerticalResolution + 0.5f));
    //    }

    //    return bm;
    //}

    public static string ImageToBase64String(this Image image, IImageEncoder format)
    {
        using var stream = new MemoryStream();

        image.Save(stream, format);

        return Convert.ToBase64String(
            stream.GetBuffer(),
            Base64FormattingOptions.None
        );
    }

    public static string PngToBase64String(this Image image)
    {
        return image.ImageToBase64String(new PngEncoder());
    }

    public static string ImageFileToBase64String(this string filePath)
    {
        using var inFile =
            new FileStream(filePath, FileMode.Open, FileAccess.Read);

        var binaryData =
            new byte[inFile.Length];

        long bytesRead =
            inFile.Read(binaryData, 0, (int)inFile.Length);

        inFile.Close();

        return Convert.ToBase64String(binaryData, 0, binaryData.Length);
    }

    public static string SvgCodeToBase64String(this string svgCode)
    {
        using var stream = new MemoryStream();
        var writer = new StreamWriter(stream, Encoding.UTF8);
        writer.Write(svgCode);
        writer.Flush();

        return Convert.ToBase64String(
            stream.GetBuffer(),
            Base64FormattingOptions.None
        );
    }

    public static string SvgCodeToPngBase64String(this string svgCode)
    {
        return PngToBase64String(svgCode.SvgCodeToPngImage());
    }


    public static string PngToHtmlDataUrlBase64(this Image image)
    {
        var imageString = image.ImageToBase64String(
            new PngEncoder()
            {
                ColorType = PngColorType.RgbWithAlpha
            }
        );

        return @$"'data:image/png;base64,{imageString}'";
    }

    public static string PngFileToHtmlDataUrlBase64(this string filePath)
    {
        var base64String =
            filePath.ImageFileToBase64String();

        return @$"'data:image/png;base64,{base64String}'";
    }

    public static string OptimizeSvgCode(this string svgCode, bool toHtmlSafeString = false)
    {
        svgCode = svgCode.Trim();

        // remove xml, doctype, generator...
        var i1 = svgCode.IndexOf("<svg", StringComparison.Ordinal);
        if (i1 >= 0)
            svgCode = svgCode[svgCode.IndexOf("<svg", StringComparison.Ordinal)..];

        // soft validate
        if (!svgCode.StartsWith("<svg") || !svgCode.EndsWith("svg>"))
            return string.Empty;

        // add namespace if necessary
        if (!svgCode.Contains("http://www.w3.org/2000/svg"))
            svgCode = svgCode.RegExReplaceAll(
                @"<svg",
                @"<svg xmlns=""http://www.w3.org/2000/svg"""
            );

        // remove comments
        svgCode = svgCode.RegExRemoveAll(
            @"<!--.{1,}-->",
            RegexOptions.ECMAScript
        );

        // remove unnecessary attributes
        svgCode = svgCode.RegExRemoveAll(
            @"version=[\""""\'](.{0,}?)[\""""\'](?=[\s>])",
            RegexOptions.ECMAScript
        );

        // svg = svg.replace(/id=[\""\"](.{0,}?)[\""\"](?=[\s>])/g, "");
        // svg = svg.replace(/class=[\""\"](.{0,}?)[\""\"](?=[\s>])/g, "");

        // replace nested quotes
        svgCode = svgCode.RegExReplaceAll(
            @"""""'(.{1,})'""""",
            @"\'$1\'",
            RegexOptions.ECMAScript
        );

        // replace double quotes
        svgCode = svgCode.RegExReplaceAll(
            @"""",
            "'",
            RegexOptions.ECMAScript
        );

        // remove empty spaces between tags
        svgCode = svgCode.RegExReplaceAll(
            @">\s{1,}<",
            "><",
            RegexOptions.ECMAScript
        );

        // remove duplicate spaces
        svgCode = svgCode.RegExReplaceAll(
            @"\s{2,}",
            " ",
            RegexOptions.ECMAScript
        );

        // trim again
        svgCode = svgCode.Trim();

        // soft validate again
        if (!(svgCode.StartsWith("<svg")) || !(svgCode.EndsWith("svg>")))
            return string.Empty;

        // replace ampersand
        svgCode = svgCode.RegExReplaceAll(
            @"&",
            "&amp;",
            RegexOptions.ECMAScript
        );

        // encode only unsafe symbols
        if (toHtmlSafeString)
            svgCode = svgCode.RegExReplaceAll(
                @"[%#<>?\[\\\]^`{|}]",
                WebUtility.UrlEncode,
                RegexOptions.ECMAScript
            );

        return svgCode;
    }

    public static string SvgCodeToHtmlDataUrl(this string svgCode)
    {
        var svgDataString = OptimizeSvgCode(svgCode);

        return @$"data:image/svg+xml,${svgDataString}";
    }

    public static string SvgCodeToPngHtmlDataUrlBase64(this string svgCode)
    {
        return PngToHtmlDataUrlBase64(svgCode.SvgCodeToPngImage());
    }

    public static string SvgFileToHtmlDataUrlBase64(this string filePath)
    {
        var base64String =
            filePath.ImageFileToBase64String();

        return @$"'data:image/svg+xml;base64,{base64String}'";
    }

    public static string SvgFileToHtmlDataUrl(this string filePath, bool useBase64 = false)
    {
        if (useBase64)
            return filePath.SvgFileToHtmlDataUrlBase64();

        var dataString =
            File.ReadAllText(filePath);

        return @$"'data:image/svg+xml,{dataString}'";
    }


    public static MagickImage SvgCodeToMagickImage(this string svgCode)
    {
        using var stream = new MemoryStream();
        var writer = new StreamWriter(stream, Encoding.Unicode);
        writer.Write(svgCode);
        writer.Flush();
        stream.Position = 0;

        var image = new MagickImage(stream, MagickFormat.Svg);

        image.BackgroundColor = MagickColors.Transparent;
        //image.Format = MagickFormat.Png32;

        return image;
    }

    public static Image SvgCodeToPngImage(this string svgCode)
    {
        using var stream = new MemoryStream();
        var writer = new StreamWriter(stream, Encoding.Unicode);
        writer.Write(svgCode);
        writer.Flush();
        stream.Position = 0;

        var image = new MagickImage(stream, MagickFormat.Svg);

        image.BackgroundColor = MagickColors.Transparent;
        //image.Format = MagickFormat.Png32;

        return image.ToImageSharpPng();
    }

    public static void SvgCodeToPngFile(this string svgCode, string pngFilePath)
    {
        using var stream = new MemoryStream();
        var writer = new StreamWriter(stream, Encoding.Unicode);
        writer.Write(svgCode);
        writer.Flush();
        stream.Position = 0;

        var image = new MagickImage(stream, MagickFormat.Svg);

        image.BackgroundColor = MagickColors.Transparent;
        //image.Format = MagickFormat.Png32;

        image.Write(pngFilePath, MagickFormat.Png32);
    }

    public static Image SvgFileToPngImage(this string svgFilePath)
    {
        var image = new MagickImage(svgFilePath, MagickFormat.Svg);

        image.BackgroundColor = MagickColors.Transparent;
        //image.Format = MagickFormat.Png32;

        return image.ToImageSharpPng();
    }

    public static void SvgFileToPngFile(this string svgFilePath, string pngFilePath)
    {
        var image = new MagickImage(svgFilePath, MagickFormat.Svg);

        image.BackgroundColor = MagickColors.Transparent;
        //image.Format = MagickFormat.Png32;

        image.Write(pngFilePath, MagickFormat.Png32);

        //var svgDocument = SvgDocument.Open(svgFilePath);

        //ISvgRenderer s;
        //using var smallBitmap = svgDocument.Draw();
        //var width = smallBitmap.Width;
        //var height = smallBitmap.Height;

        //if (width != 2000)// I resize my bitmap
        //{
        //    width = 2000;
        //    height = 2000 / smallBitmap.Width * height;
        //}

        //using var bitmap = svgDocument.Draw(width, height);
        //bitmap.Save(pngFilePath, ImageFormat.Png);
    }


    public static Image<Rgba32> ToImageSharpPng(this IMagickImage<ushort> image)
    {
        using var stream = new MemoryStream();
        image.Write(stream, MagickFormat.Png32);
        stream.Position = 0;

        return Image.Load<Rgba32>(stream);
    }

    public static List<string>? GetLocalFilesForFont(this string fontName)
    {
        var fontNameToFiles = new Dictionary<string, List<string>>();

        var fontFileList = Directory.GetFiles(
            Environment.GetFolderPath(Environment.SpecialFolder.Fonts)
        );

        foreach (var fontFile in fontFileList)
        {
            var fc = new PrivateFontCollection();

            if (File.Exists(fontFile))
                fc.AddFontFile(fontFile);

            if (!fc.Families.Any())
                continue;

            var name = fc.Families[0].Name;

            // If you care about bold, italic, etc, you can filter here.
            if (!fontNameToFiles.TryGetValue(name, out var files))
            {
                files = new List<string>();
                fontNameToFiles[name] = files;
            }

            files.Add(fontFile);
        }

        return fontNameToFiles.TryGetValue(fontName, out var result)
            ? result
            : null;
    }
}