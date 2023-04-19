using System.Text;
using GraphicsComposerLib.Rendering.Images;
using GraphicsComposerLib.Rendering.Svg.Content;
using GraphicsComposerLib.Rendering.Svg.Values;
using ImageMagick;

namespace GraphicsComposerLib.Rendering.Svg
{
    public static class SvgUtils
    {
        public static string SvgCodeToBase64String(this string svgCode)
        {
            using var stream = new MemoryStream();
            var writer = new StreamWriter(stream, Encoding.Unicode);
            writer.Write(svgCode);
            writer.Flush();

            return Convert.ToBase64String(
                stream.GetBuffer(), 
                Base64FormattingOptions.None
            );
        }

        public static string SvgCodeToBase64HtmlString(this string svgCode)
        {
            var base64String = svgCode.SvgCodeToBase64String();

            return $"'data:image/svg+xml;base64,{base64String}'";
        }

        public static string SvgCodeToPngBase64String(this string svgCode)
        {
            return svgCode.SvgCodeToPngImage().PngToBase64String();
        }

        public static string SvgCodeToPngBase64HtmlString(this string svgCode)
        {
            return svgCode.SvgCodeToPngImage().PngToBase64HtmlString();
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

        public static string ToSvgLengthText(this double lengthValue, SvgValueLengthUnit unit)
        {
            return new StringBuilder(32)
                .Append(lengthValue.ToSvgNumberText())
                .Append(unit?.ValueText ?? string.Empty)
                .ToString();
        }

        public static string ToSvgAngleText(this double angleValue, SvgValueAngleUnit unit)
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
    }
}
