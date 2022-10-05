using ImageMagick;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Tiff;
using SixLabors.ImageSharp.Formats.Webp;

namespace GraphicsComposerLib.Rendering.Images;

public class GrImageBase64String
{
    public string Key { get; }
    
    public int MarginSize { get; }

    public Color BackgroundColor { get; }

    public int Width { get; private set; }

    public int Height { get; private set; }

    public double WidthToHeightRatio 
        => Width / (double) Height;

    public double HeightToWidthRatio
        => Height / (double) Width;

    public GrImageHtmlMimeType HtmlMimeType { get; private set; }

    public string MimeString
        => HtmlMimeType switch
        {
            GrImageHtmlMimeType.Bmp => "image/bmp",
            GrImageHtmlMimeType.Gif => "image/gif",
            GrImageHtmlMimeType.Ico => "image/vnd.microsoft.icon",
            GrImageHtmlMimeType.Jpeg => "image/jpeg",
            GrImageHtmlMimeType.Png => "image/png",
            GrImageHtmlMimeType.Svg => "image/svg+xml",
            GrImageHtmlMimeType.Tiff => "image/tiff",
            GrImageHtmlMimeType.Webp => "image/webp",

            _ => throw new NotImplementedException()
        };

    public string Base64String { get; private set; } = string.Empty;


    internal GrImageBase64String(string key, int marginSize, Color backgroundColor)
    {
        Key = key;
        MarginSize = marginSize;
        BackgroundColor = backgroundColor;
    }

    
    internal GrImageBase64String SetImage(Image image, GrImageHtmlMimeType mimeType)
    {
        IImageEncoder imageEncoder = mimeType switch
        {
            GrImageHtmlMimeType.Png => new PngEncoder(),
            GrImageHtmlMimeType.Bmp => new BmpEncoder(),
            GrImageHtmlMimeType.Gif => new GifEncoder(),
            GrImageHtmlMimeType.Jpeg => new JpegEncoder(),
            GrImageHtmlMimeType.Tiff => new TiffEncoder(),
            GrImageHtmlMimeType.Webp => new WebpEncoder(),

            _ => throw new ArgumentOutOfRangeException(nameof(mimeType), mimeType, "Invalid mimeType value")
        };

        Width = image.Width;
        Height = image.Height;
        Base64String = image.ImageToBase64String(imageEncoder);
        HtmlMimeType = mimeType;

        return this;
    }
    
    internal GrImageBase64String SetImage(IMagickImage<ushort> image, GrImageHtmlMimeType mimeType)
    {
        var imageFormat = mimeType switch
        {
            GrImageHtmlMimeType.Png => MagickFormat.Png32,
            GrImageHtmlMimeType.Bmp => MagickFormat.Bmp,
            GrImageHtmlMimeType.Gif => MagickFormat.Gif,
            GrImageHtmlMimeType.Jpeg => MagickFormat.Jpeg,
            GrImageHtmlMimeType.Tiff => MagickFormat.Tiff,
            GrImageHtmlMimeType.Webp => MagickFormat.WebP,
            GrImageHtmlMimeType.Ico => MagickFormat.Ico,
            GrImageHtmlMimeType.Svg => MagickFormat.Svg,

            _ => throw new ArgumentOutOfRangeException(nameof(mimeType), mimeType, "Invalid mimeType value")
        };

        Width = image.Width;
        Height = image.Height;
        Base64String = image.ToBase64(imageFormat);
        HtmlMimeType = mimeType;

        return this;
    }
    
    internal GrImageBase64String SetImageFromFile(string filePath, GrImageHtmlMimeType mimeType)
    {
        //IImageDecoder imageDecoder = mimeType switch
        //{
        //    GrImageHtmlMimeType.Png => new PngDecoder(),
        //    GrImageHtmlMimeType.Bmp => new BmpDecoder(),
        //    GrImageHtmlMimeType.Gif => new GifDecoder(),
        //    GrImageHtmlMimeType.Jpeg => new JpegDecoder(),
        //    GrImageHtmlMimeType.Tiff => new TiffDecoder(),
        //    GrImageHtmlMimeType.Webp => new WebpDecoder(),

        //    _ => throw new ArgumentOutOfRangeException(nameof(mimeType), mimeType, "Invalid mimeType value")
        //};

        //IImageEncoder imageEncoder = mimeType switch
        //{
        //    GrImageHtmlMimeType.Png => new PngEncoder(),
        //    GrImageHtmlMimeType.Bmp => new BmpEncoder(),
        //    GrImageHtmlMimeType.Gif => new GifEncoder(),
        //    GrImageHtmlMimeType.Jpeg => new JpegEncoder(),
        //    GrImageHtmlMimeType.Tiff => new TiffEncoder(),
        //    GrImageHtmlMimeType.Webp => new WebpEncoder(),

        //    _ => throw new ArgumentOutOfRangeException(nameof(mimeType), mimeType, "Invalid mimeType value")
        //};

        //var image = Image.Load(filePath, imageDecoder);

        var imageFormat = mimeType switch
        {
            GrImageHtmlMimeType.Png => MagickFormat.Png32,
            GrImageHtmlMimeType.Bmp => MagickFormat.Bmp,
            GrImageHtmlMimeType.Gif => MagickFormat.Gif,
            GrImageHtmlMimeType.Jpeg => MagickFormat.Jpeg,
            GrImageHtmlMimeType.Tiff => MagickFormat.Tiff,
            GrImageHtmlMimeType.Webp => MagickFormat.WebP,
            GrImageHtmlMimeType.Ico => MagickFormat.Ico,
            GrImageHtmlMimeType.Svg => MagickFormat.Svg,

            _ => throw new ArgumentOutOfRangeException(nameof(mimeType), mimeType, $"Invalid mimeType value {mimeType}")
        };

        var image = new MagickImage(new FileInfo(filePath));

        Width = image.Width;
        Height = image.Height;
        Base64String = image.ToBase64(imageFormat);
        HtmlMimeType = mimeType;

        return this;
    }
    
    internal GrImageBase64String SetSvgImageFromFile(string filePath, int width, int height)
    {
        Width = width;
        Height = height;
        Base64String = filePath.FileToBase64String();
        HtmlMimeType = GrImageHtmlMimeType.Svg;

        return this;
    }

    public string GetBase64HtmlString()
    {
        return $"'data:{MimeString};base64,{Base64String}'";
    }
}