//using ImageMagick;
//using SixLabors.ImageSharp;
//using GeometricAlgebraFulcrumLib.Utilities.Text;
//using SixLabors.ImageSharp.PixelFormats;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
//using GeometricAlgebraFulcrumLib.Utilities.Web.Images;

//namespace GeometricAlgebraFulcrumLib.Utilities.Web.Html.Media;

///// <summary>
///// https://developer.mozilla.org/en-US/docs/web/http/basics_of_http/data_urls
///// </summary>
//public class WclHtmlImageUrl : 
//    IGrVisualTexture
//{
//    public string Key { get; }

//    public int MarginSize { get; }

//    public Color BackgroundColor { get; }

//    public bool IsFileUrl 
//        => !IsDataUrl;

//    public bool IsDataUrl { get; private set; }

//    public bool IsBase64 { get; private set; }

//    public Image Image { get; private set; }

//    public string PngImageFilePath 
//        => Key + ".png";

//    public int ImageWidth { get; private set; }

//    public int ImageHeight { get; private set; }
    
//    public Pair<int> ImageSize 
//        => new Pair<int>(ImageWidth, ImageHeight);

//    public double ImageWidthToHeight
//        => ImageWidth / (double)ImageHeight;

//    public double ImageHeightToWidth
//        => ImageHeight / (double)ImageWidth;

//    public WclHtmlImageMediaType MediaType { get; private set; }

//    public string MediaTypeString
//        => MediaType.GetMediaTypeString();

//    public string UrlString { get; private set; }
//        = string.Empty;


//    public WclHtmlImageUrl(string key, int marginSize, Color backgroundColor)
//    {
//        Key = key;
//        MarginSize = marginSize;
//        BackgroundColor = backgroundColor;
//    }

        
//    public WclHtmlImageUrl SetFileUrl(WclHtmlImageMediaType mediaType, int width, int height, string dataString)
//    {
//        Image = new Image<Rgba32>(width, height);
//        MediaType = mediaType;
//        ImageWidth = width;
//        ImageHeight = height;
//        IsDataUrl = false;
//        IsBase64 = false;
//        UrlString = dataString;

//        return this;
//    }

//    public WclHtmlImageUrl SetDataUrl(WclHtmlImageMediaType mediaType, int width, int height, string dataString, bool isBase64)
//    {
//        Image = null;
//        MediaType = mediaType;
//        ImageWidth = width;
//        ImageHeight = height;
//        IsDataUrl = true;
//        IsBase64 = isBase64;
//        UrlString = dataString;

//        return this;
//    }
        
//    public WclHtmlImageUrl SetDataUrlBase64(WclHtmlImageMediaType mediaType, int width, int height, string dataString)
//    {
//        Image = null;
//        MediaType = mediaType;
//        ImageWidth = width;
//        ImageHeight = height;
//        IsDataUrl = true;
//        IsBase64 = true;
//        UrlString = dataString;

//        return this;
//    }

//    public WclHtmlImageUrl SetDataUrlBase64FromImage(Image image, WclHtmlImageMediaType mediaType)
//    {
//        var imageEncoder = 
//            mediaType.GetImageSharpEncoder();

//        Image = image;
//        ImageWidth = image.Width;
//        ImageHeight = image.Height;
//        MediaType = mediaType;
//        IsDataUrl = true;
//        IsBase64 = true;
//        UrlString = image.ImageToBase64String(imageEncoder);

//        return this;
//    }

//    public WclHtmlImageUrl SetDataUrlBase64FromImage(IMagickImage<ushort> image, WclHtmlImageMediaType mediaType)
//    {
//        var imageFormat = 
//            mediaType.GetImageMagickFormat();

//        Image = image.ToImageSharpPng();
//        ImageWidth = (int)image.Width;
//        ImageHeight = (int)image.Height;
//        MediaType = mediaType;
//        IsDataUrl = true;
//        IsBase64 = true;
//        UrlString = image.ToBase64(imageFormat);
            
//        return this;
//    }

//    public WclHtmlImageUrl SetDataUrlBase64FromImageFile(string filePath, WclHtmlImageMediaType mediaType)
//    {
//        var imageFormat = 
//            mediaType.GetImageMagickFormat();;

//        var image = new MagickImage(new FileInfo(filePath));

//        Image = image.ToImageSharpPng();
//        ImageWidth = (int)image.Width;
//        ImageHeight = (int)image.Height;
//        MediaType = mediaType;
//        IsDataUrl = true;
//        IsBase64 = true;
//        UrlString = image.ToBase64(imageFormat);
            
//        return this;
//    }
        
//    public WclHtmlImageUrl SetDataUrlFromSvgFile(string filePath, int width, int height, bool useBase64)
//    {
//        Image = Image.Load(filePath);
//        ImageWidth = width;
//        ImageHeight = height;
//        MediaType = WclHtmlImageMediaType.Svg;
//        IsDataUrl = true;
//        IsBase64 = useBase64;
//        UrlString = filePath.SvgFileToHtmlDataUrl(useBase64);

//        return this;
//    }
        
//    public WclHtmlImageUrl SetDataUrlBase64FromSvgFile(string filePath, int width, int height)
//    {
//        Image = Image.Load(filePath);
//        ImageWidth = width;
//        ImageHeight = height;
//        MediaType = WclHtmlImageMediaType.Svg;
//        IsDataUrl = true;
//        IsBase64 = true;
//        UrlString = filePath.SvgFileToHtmlDataUrl(true);

//        return this;
//    }

    
//    public Image GetImage()
//    {
//        return Image;
//    }

//    public string GetImageUrl()
//    {
//        //return $"data:{MediaTypeString};base64,{DataString}".DoubleQuote();

//        if (IsDataUrl)
//        {
//            return IsBase64
//                ? $"data:{MediaTypeString};base64,{UrlString}".DoubleQuote()
//                : $"data:{MediaTypeString},{UrlString}".DoubleQuote();
//        }

//        return UrlString.DoubleQuote();
//    }
//}