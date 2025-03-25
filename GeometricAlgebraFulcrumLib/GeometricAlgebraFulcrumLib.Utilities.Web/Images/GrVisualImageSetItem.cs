using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Web.Html.Media;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Images;

public sealed class GrVisualImageSetItem :
    IGrVisualImageSource
{
    public GrVisualImageSetGroup ImageSetGroup { get; }

    public GrVisualImageSet ImageSet
        => ImageSetGroup.ImageSet;

    public string GroupName
        => ImageSetGroup.GroupName;

    public string ImageSetFolder
        => ImageSet.ImageSetFolder;

    public string ImageSetGroupFolder
        => ImageSetGroup.ImageSetGroupFolder;

    public string ImageName { get; }

    public string ImageId
        => GroupName + "." + ImageName;

    public int ImageWidth { get; }

    public int ImageHeight { get; }

    public Pair<int> ImageSize
        => new Pair<int>(ImageWidth, ImageHeight);

    public double ImageWidthToHeight
        => ImageWidth / (double)ImageHeight;

    public double ImageHeightToWidth
        => ImageHeight / (double)ImageWidth;


    internal GrVisualImageSetItem(GrVisualImageSetGroup imageGroup, string imageName, Image image)
    {
        ImageSetGroup = imageGroup;
        ImageName = imageName;
        ImageWidth = image.Width;
        ImageHeight = image.Height;

        image.SaveAsPng(
            GetPngImageFilePath(),
            new PngEncoder()
            {
                ColorType = PngColorType.RgbWithAlpha
            }
        );
    }

    internal GrVisualImageSetItem(GrVisualImageSetGroup imageGroup, string imageName)
    {
        ImageSetGroup = imageGroup;
        ImageName = imageName;

        var image = Image.Load<Rgba32>(GetPngImageFilePath());
        ImageWidth = image.Width;
        ImageHeight = image.Height;
    }

    internal GrVisualImageSetItem(GrVisualImageSetGroup imageGroup, string imageName, string imageFilePath)
        : this(imageGroup, imageName, Image.Load<Rgba32>(imageFilePath))
    {
    }


    public Image GetImage()
    {
        return Image.Load<Rgba32>(GetPngImageFilePath());
    }

    public string GetImageDataUrlBase64()
    {
        return Image.Load<Rgba32>(GetPngImageFilePath()).PngToHtmlDataUrlBase64();
    }
    
    public string GetPngImageFilePath()
    {
        return Path.Combine(ImageSetGroupFolder, ImageName + ".png");
    }

    public override string ToString()
    {
        return GetPngImageFilePath();
    }
}