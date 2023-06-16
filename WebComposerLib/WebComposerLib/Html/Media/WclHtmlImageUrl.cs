using ImageMagick;
using TextComposerLib;

namespace WebComposerLib.Html.Media
{
    /// <summary>
    /// https://developer.mozilla.org/en-US/docs/web/http/basics_of_http/data_urls
    /// </summary>
    public class WclHtmlImageUrl
    {
        public string Key { get; }

        public int MarginSize { get; }

        public Color BackgroundColor { get; }

        public bool IsFileUrl 
            => !IsDataUrl;

        public bool IsDataUrl { get; private set; }

        public bool IsBase64 { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public double WidthToHeightRatio
            => Width / (double)Height;

        public double HeightToWidthRatio
            => Height / (double)Width;

        public WclHtmlImageMediaType MediaType { get; private set; }

        public string MediaTypeString
            => MediaType.GetMediaTypeString();

        public string UrlString { get; private set; }
            = string.Empty;


        public WclHtmlImageUrl(string key, int marginSize, Color backgroundColor)
        {
            Key = key;
            MarginSize = marginSize;
            BackgroundColor = backgroundColor;
        }

        
        public WclHtmlImageUrl SetFileUrl(WclHtmlImageMediaType mediaType, int width, int height, string dataString)
        {
            MediaType = mediaType;
            Width = width;
            Height = height;
            IsDataUrl = false;
            IsBase64 = false;
            UrlString = dataString;

            return this;
        }

        public WclHtmlImageUrl SetDataUrl(WclHtmlImageMediaType mediaType, int width, int height, string dataString, bool isBase64)
        {
            MediaType = mediaType;
            Width = width;
            Height = height;
            IsDataUrl = true;
            IsBase64 = isBase64;
            UrlString = dataString;

            return this;
        }
        
        public WclHtmlImageUrl SetDataUrlBase64(WclHtmlImageMediaType mediaType, int width, int height, string dataString)
        {
            MediaType = mediaType;
            Width = width;
            Height = height;
            IsDataUrl = true;
            IsBase64 = true;
            UrlString = dataString;

            return this;
        }

        public WclHtmlImageUrl SetDataUrlBase64FromImage(Image image, WclHtmlImageMediaType mediaType)
        {
            var imageEncoder = 
                mediaType.GetImageSharpEncoder();

            Width = image.Width;
            Height = image.Height;
            MediaType = mediaType;
            IsDataUrl = true;
            IsBase64 = true;
            UrlString = image.ImageToBase64String(imageEncoder);

            return this;
        }

        public WclHtmlImageUrl SetDataUrlBase64FromImage(IMagickImage<ushort> image, WclHtmlImageMediaType mediaType)
        {
            var imageFormat = 
                mediaType.GetImageMagickFormat();

            Width = image.Width;
            Height = image.Height;
            MediaType = mediaType;
            IsDataUrl = true;
            IsBase64 = true;
            UrlString = image.ToBase64(imageFormat);
            
            return this;
        }

        public WclHtmlImageUrl SetDataUrlBase64FromImageFile(string filePath, WclHtmlImageMediaType mediaType)
        {
            var imageFormat = 
                mediaType.GetImageMagickFormat();;

            var image = new MagickImage(new FileInfo(filePath));

            Width = image.Width;
            Height = image.Height;
            MediaType = mediaType;
            IsDataUrl = true;
            IsBase64 = true;
            UrlString = image.ToBase64(imageFormat);
            
            return this;
        }
        
        public WclHtmlImageUrl SetDataUrlFromSvgFile(string filePath, int width, int height, bool useBase64)
        {
            Width = width;
            Height = height;
            MediaType = WclHtmlImageMediaType.Svg;
            IsDataUrl = true;
            IsBase64 = useBase64;
            UrlString = filePath.SvgFileToHtmlDataUrl(useBase64);

            return this;
        }
        
        public WclHtmlImageUrl SetDataUrlBase64FromSvgFile(string filePath, int width, int height)
        {
            Width = width;
            Height = height;
            MediaType = WclHtmlImageMediaType.Svg;
            IsDataUrl = true;
            IsBase64 = true;
            UrlString = filePath.SvgFileToHtmlDataUrl(true);

            return this;
        }


        public string GetUrl()
        {
            //return $"data:{MediaTypeString};base64,{DataString}".DoubleQuote();

            if (IsDataUrl)
            {
                return IsBase64
                    ? $"data:{MediaTypeString};base64,{UrlString}".DoubleQuote()
                    : $"data:{MediaTypeString},{UrlString}".DoubleQuote();
            }

            return UrlString.DoubleQuote();
        }
    }
}