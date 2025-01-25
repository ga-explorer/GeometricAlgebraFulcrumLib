using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Web.Html.Media;
using GeometricAlgebraFulcrumLib.Utilities.Web.Images;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Textures;

public sealed class GrVisualTexture :
    IGrVisualTexture
{
    public GrVisualTextureSetGroup TextureGroup { get; }

    public GrVisualTextureSet TextureSet 
        => TextureGroup.TextureSet;

    public string GroupName 
        => TextureGroup.GroupName;
    
    public string TextureSetFolder 
        => TextureSet.TextureSetFolder;
    
    public string TextureGroupFolder 
        => TextureGroup.TextureGroupFolder;

    public string ImageName { get; }

    public string ImageId 
        => GroupName + "." + ImageName;

    public string PngImageFileName
        => ImageName + ".png";

    public string PngImageFilePath
        => Path.Combine(TextureGroupFolder, PngImageFileName);

    public string PngDataUrlBase64
        => Image.Load<Rgba32>(PngImageFilePath).PngToHtmlDataUrlBase64();

    public int ImageWidth { get; }

    public int ImageHeight { get; }
    
    public Pair<int> ImageSize 
        => new Pair<int>(ImageWidth, ImageHeight);

    public double ImageWidthToHeight
        => ImageWidth / (double)ImageHeight;

    public double ImageHeightToWidth
        => ImageHeight / (double)ImageWidth;


    internal GrVisualTexture(GrVisualTextureSetGroup textureGroup, string imageName, Image image)
    {
        TextureGroup = textureGroup;
        ImageName = imageName;
        ImageWidth = image.Width;
        ImageHeight = image.Height;
        
        image.SaveAsPng(
            PngImageFilePath,
            new PngEncoder()
            {
                ColorType = PngColorType.RgbWithAlpha
            }
        );
    }
    
    internal GrVisualTexture(GrVisualTextureSetGroup textureGroup, string imageName)
    {
        TextureGroup = textureGroup;
        ImageName = imageName;
        
        var image = Image.Load<Rgba32>(PngImageFilePath);
        ImageWidth = image.Width;
        ImageHeight = image.Height;
    }
    
    internal GrVisualTexture(GrVisualTextureSetGroup textureGroup, string imageName, string imageFilePath)
        : this(textureGroup, imageName, Image.Load<Rgba32>(imageFilePath))
    {
    }


    public Image GetImage()
    {
        return Image.Load<Rgba32>(PngImageFilePath);
    }

    public string GetImageUrl()
    {
        return PngDataUrlBase64;
    }

    public override string ToString()
    {
        return PngImageFilePath;
    }
}