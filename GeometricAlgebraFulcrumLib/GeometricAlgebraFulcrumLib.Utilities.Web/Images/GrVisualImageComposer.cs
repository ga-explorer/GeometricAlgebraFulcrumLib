using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Files;
using GeometricAlgebraFulcrumLib.Utilities.Web.Html.Media;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Images;

public class GrVisualImageComposer :
    IGrVisualImageSource
{
    private static PngEncoder DefaultPngEncoder { get; }
        = new PngEncoder()
        {
            ColorType = PngColorType.RgbWithAlpha
        };


    public string WorkingFolder { get; }

    public Color BaseColor { get; set; } 

    public Image<Rgba32> BaseImage { get; private set; } 

    public int ImageWidth 
        => BaseImage.Width;

    public int ImageHeight 
        => BaseImage.Height;

    public double ImageWidthToHeight 
        => ImageWidth / (double)ImageHeight;

    public double ImageHeightToWidth 
        => ImageHeight / (double)ImageWidth;

    public Pair<int> ImageSize 
        => new Pair<int>(BaseImage.Width, BaseImage.Height);

    public string PngImageFileName { get; }


    public GrVisualImageComposer(string workingFolder, string pngImageFileName, int width, int height, Color baseColor)
    {
        WorkingFolder = workingFolder;
        PngImageFileName = pngImageFileName;
        BaseColor = baseColor;
        BaseImage = new Image<Rgba32>(width, height, BaseColor);
    }
    
    public GrVisualImageComposer(string workingFolder, string pngImageFileName, Image baseImage)
    {
        WorkingFolder = workingFolder;
        PngImageFileName = pngImageFileName;
        BaseColor = Color.Transparent;
        BaseImage = baseImage.CloneAs<Rgba32>();
    }
    
    public GrVisualImageComposer(string workingFolder, string pngImageFileName, string baseImageFilePath)
    {
        WorkingFolder = workingFolder;
        PngImageFileName = pngImageFileName;
        BaseColor = Color.Transparent;
        BaseImage = Image.Load<Rgba32>(baseImageFilePath);
    }

    
    public GrVisualImageComposer SetBaseColor(Color baseColor)
    {
        BaseColor = baseColor;

        return this;
    }

    public GrVisualImageComposer SetBaseImage(int width, int height)
    {
        BaseImage = new Image<Rgba32>(width, height, BaseColor);

        return this;
    }

    public GrVisualImageComposer SetBaseImage(int width, int height, Color baseColor)
    {
        BaseImage = new Image<Rgba32>(width, height, baseColor);

        return this;
    }
    
    public GrVisualImageComposer SetBaseImage(Image image)
    {
        BaseImage = image.CloneAs<Rgba32>();

        return this;
    }

    public GrVisualImageComposer SetBaseImageFromFile(string imageFilePath)
    {
        BaseImage = Image.Load<Rgba32>(imageFilePath);

        return this;
    }


    public GrVisualImageComposer Expand(int deltaLeft, int deltaRight, int deltaTop, int deltaBottom)
    {
        return Expand(deltaLeft, deltaRight, deltaTop, deltaBottom, BaseColor);
    }

    public GrVisualImageComposer Expand(int deltaLeft, int deltaRight, int deltaTop, int deltaBottom, Color baseColor)
    {
        if (deltaLeft < 0 || deltaRight < 0 || deltaTop < 0 || deltaBottom < 0)
            throw new InvalidOperationException();

        var width = ImageWidth + deltaLeft + deltaRight;
        var height = ImageHeight + deltaTop + deltaBottom;

        var baseImage = new Image<Rgba32>(width, height, baseColor); 

        baseImage.Mutate(x => 
            x.DrawImage(
                BaseImage,
                new Rectangle(deltaLeft, deltaTop, BaseImage.Width, BaseImage.Height),
                1f
            )
        );

        BaseImage = baseImage;

        return this;
    }
    
    public GrVisualImageComposer ExpandFromLeft(int delta)
    {
        return Expand(delta, 0, 0, 0, BaseColor);
    }

    public GrVisualImageComposer ExpandFromLeft(int delta, Color baseColor)
    {
        return Expand(delta, 0, 0, 0, baseColor);
    }
    
    public GrVisualImageComposer ExpandFromRight(int delta)
    {
        return Expand(0, delta, 0, 0, BaseColor);
    }

    public GrVisualImageComposer ExpandFromRight(int delta, Color baseColor)
    {
        return Expand(0, delta, 0, 0, baseColor);
    }
    
    public GrVisualImageComposer ExpandFromTop(int delta)
    {
        return Expand(0, 0, delta, 0, BaseColor);
    }

    public GrVisualImageComposer ExpandFromTop(int delta, Color baseColor)
    {
        return Expand(0, 0, delta, 0, baseColor);
    }
    
    public GrVisualImageComposer ExpandFromBottom(int delta)
    {
        return Expand(0, 0, 0, delta, BaseColor);
    }

    public GrVisualImageComposer ExpandFromBottom(int delta, Color baseColor)
    {
        return Expand(0, 0, 0, delta, baseColor);
    }
    
    public GrVisualImageComposer ExpandFromLeftRight(int delta)
    {
        return Expand(delta, delta, 0, 0, BaseColor);
    }

    public GrVisualImageComposer ExpandFromLeftRight(int delta, Color baseColor)
    {
        return Expand(delta, delta, 0, 0, baseColor);
    }
    
    public GrVisualImageComposer ExpandFromTopBottom(int delta)
    {
        return Expand(0, 0, delta, delta, BaseColor);
    }

    public GrVisualImageComposer ExpandFromTopBottom(int delta, Color baseColor)
    {
        return Expand(0, 0, delta, delta, baseColor);
    }


    public GrVisualImageComposer ScaleBy(double scalingFactor, IResampler resamplingMethod)
    {
        var width = (int) Math.Floor(scalingFactor * ImageWidth);
        var height = (int) Math.Floor(scalingFactor * ImageHeight);

        BaseImage.Mutate(x =>
            x.Resize(
                width,
                height,
                resamplingMethod
            )
        );

        return this;
    }

    public GrVisualImageComposer SetWidth(int width, IResampler resamplingMethod)
    {
        var height = (int) Math.Floor(width * ImageHeightToWidth);

        BaseImage.Mutate(x =>
            x.Resize(
                width,
                height,
                resamplingMethod
            )
        );

        return this;
    }
    
    public GrVisualImageComposer SetHeight(int height, IResampler resamplingMethod)
    {
        var width = (int) Math.Floor(height * ImageWidthToHeight);

        BaseImage.Mutate(x =>
            x.Resize(
                width,
                height,
                resamplingMethod
            )
        );

        return this;
    }


    public GrVisualImageComposer DrawImage(Image image, int top, int left, double scalingFactor = 1, double opacity = 1)
    {
        var width = (int)Math.Floor(scalingFactor * image.Width);
        var height = (int)Math.Floor(scalingFactor * image.Height);

        if (width == ImageWidth && height == ImageHeight)
        {
            BaseImage.Mutate(x => 
                x.DrawImage(
                    image,
                    new Rectangle(left, top, image.Width, image.Height),
                    (float)opacity
                )
            );

            return this;
        }

        var resizedImage = 
            image.Clone(x => 
                x.Resize(
                    width, 
                    height, 
                    KnownResamplers.CatmullRom
                )
            );

        BaseImage.Mutate(x => 
            x.DrawImage(
                resizedImage,
                new Rectangle(left, top, width, height),
                (float)opacity
            )
        );

        return this;
    }
    
    public GrVisualImageComposer DrawImage(Image image, int top, int left, double scalingFactor, GraphicsOptions options)
    {
        if (scalingFactor == 1)
        {
            BaseImage.Mutate(x => 
                x.DrawImage(
                    image,
                    new Rectangle(left, top, image.Width, image.Height),
                    options
                )
            );

            return this;
        }

        var imageWidth = (int)Math.Floor(scalingFactor * image.Width);
        var imageHeight = (int)Math.Floor(scalingFactor * image.Height);

        var resizedImage = 
            image.Clone(x => 
                x.Resize(
                    imageWidth, 
                    imageHeight, 
                    KnownResamplers.CatmullRom
                )
            );

        BaseImage.Mutate(x => 
            x.DrawImage(
                resizedImage,
                new Rectangle(left, top, imageWidth, imageHeight),
                options
            )
        );

        return this;
    }


    public Image GetImage()
    {
        return BaseImage.Clone();
    }

    public string GetImageDataUrlBase64()
    {
        return BaseImage.PngToHtmlDataUrlBase64();
    }
    
    public string GetPngImageFilePath()
    {
        return WorkingFolder.GetPngFilePath(PngImageFileName);
    }
    
    public GrVisualImageComposer SavePng()
    {
        BaseImage.SaveAsPng(
            WorkingFolder.GetPngFilePath(PngImageFileName),
            DefaultPngEncoder
        );

        return this;
    }

    public GrVisualImageComposer SavePng(string fileName)
    {
        BaseImage.SaveAsPng(
            WorkingFolder.GetPngFilePath(fileName),
            DefaultPngEncoder
        );

        return this;
    }


}