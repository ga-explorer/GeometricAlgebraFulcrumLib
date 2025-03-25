using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Web.Html.Media;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Images;

public sealed class GrVisualStoredImage(Image storedImage) :
    IGrVisualImageSource
{
    public Image StoredImage { get; } = storedImage;

    public int ImageWidth
        => StoredImage.Width;

    public int ImageHeight
        => StoredImage.Height;

    public Pair<int> ImageSize
        => new Pair<int>(StoredImage.Width, StoredImage.Height);

    public double ImageWidthToHeight
        => ImageWidth / (double)ImageHeight;

    public double ImageHeightToWidth
        => ImageHeight / (double)ImageWidth;


    public Image GetImage()
    {
        return StoredImage;
    }
    
    public string GetImageDataUrlBase64()
    {
        return StoredImage.PngToHtmlDataUrlBase64();
    }
    
    public string GetPngImageFilePath()
    {
        return string.Empty;
    }

}