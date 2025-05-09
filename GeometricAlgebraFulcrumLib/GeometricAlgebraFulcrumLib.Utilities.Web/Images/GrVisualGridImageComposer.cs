using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using GeometricAlgebraFulcrumLib.Utilities.Web.Html.Media;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Images;

public sealed class GrVisualGridImageComposer :
    IGrVisualImageSource
{
    public static GrVisualGridImageComposer Default(double opacity)
    {
        return new GrVisualGridImageComposer()
        {
            BaseSquareColor = System.Drawing.Color.LightYellow.ToImageSharpColor(),
            BaseLineColor = System.Drawing.Color.BurlyWood.ToImageSharpColor(),
            MidLineColor = System.Drawing.Color.SandyBrown.ToImageSharpColor(),
            BorderLineColor = System.Drawing.Color.Black.ToImageSharpColor(),
            BaseSquareCount = 4,
            BaseSquareSize = 64,
            BaseLineWidth = 2,
            MidLineWidth = 4,
            BorderLineWidth = 3
        }.SetGridColorsOpacity(opacity);
    }


    public Color BaseSquareColor { get; set; } = Color.LightYellow;

    public int BaseSquareCount { get; set; } = 4;

    public int BaseSquareSize { get; set; } = 64;

    public double BaseSquareSizeNormalized
        => BaseSquareSize / (double)ImageWidth;

    public int TotalSquareSize
        => BaseSquareSize + BaseLineWidth;


    public Color BaseLineColor { get; set; } = Color.BurlyWood;

    public int BaseLineWidth { get; set; } = 2;

    public double BaseLineWidthNormalized
        => BaseLineWidth / (double)ImageWidth;

    public bool BaseLineEnabled
        => BaseLineWidth > 0;


    public Color MidLineColor { get; set; } = Color.SandyBrown;

    public int MidLineWidth { get; set; } = 4;

    public double MidLineWidthNormalized
        => MidLineWidth / (double)ImageWidth;

    public bool MidLineEnabled
        => MidLineWidth > 0;


    public Color BorderLineColor { get; set; } = Color.SaddleBrown;

    public int BorderLineWidth { get; set; } = 3;

    public double BorderLineWidthNormalized
        => BorderLineWidth / (double)ImageWidth;

    public bool BorderLineEnabled
        => BorderLineWidth > 0;


    public int ImageWidth
        => (BaseSquareSize + BaseLineWidth) * BaseSquareCount + BaseLineWidth;

    public int ImageHeight
        => (BaseSquareSize + BaseLineWidth) * BaseSquareCount + BaseLineWidth;

    public double ImageWidthToHeight 
        => ImageWidth / (double)ImageHeight;

    public double ImageHeightToWidth 
        => ImageHeight / (double)ImageWidth;
    
    public Pair<int> ImageSize 
        => new Pair<int>(
            (BaseSquareSize + BaseLineWidth) * BaseSquareCount + BaseLineWidth,
            (BaseSquareSize + BaseLineWidth) * BaseSquareCount + BaseLineWidth
        );


    public bool IsValid()
    {
        return true;
    }


    public Image GetImage()
    {
        var (width, height) = ImageSize;
        var bitmap = new Image<Rgba32>(width, height);

        AddBaseColor(bitmap);

        AddBaseLines(bitmap);

        AddMidLines(bitmap);

        AddBorderLines(bitmap);

        return bitmap;
    }

    public string GetImageDataUrlBase64()
    {
        return GetImage().PngToHtmlDataUrlBase64();
    }
    
    public string GetPngImageFilePath()
    {
        return string.Empty;
    }


    private void AddBaseColor(Image<Rgba32> bitmap)
    {
        var (width, height) = ImageSize;

        for (var i = 0; i < width; i++)
            for (var j = 0; j < height; j++)
                bitmap[i, j] = BaseSquareColor;
    }

    private void AddBaseLines(Image<Rgba32> bitmap)
    {
        if (!BaseLineEnabled)
            return;

        var (width, height) = ImageSize;

        for (var i1 = 0; i1 < width; i1 += TotalSquareSize)
        {
            for (var i = 0; i < BaseLineWidth; i++)
                for (var j = 0; j < height; j++)
                    bitmap[i + i1, j] = BaseLineColor;
        }

        for (var j1 = 0; j1 < width; j1 += TotalSquareSize)
        {
            for (var i = 0; i < width; i++)
                for (var j = 0; j < BaseLineWidth; j++)
                    bitmap[i, j + j1] = BaseLineColor;
        }
    }

    private void AddMidLines(Image<Rgba32> bitmap)
    {
        if (!MidLineEnabled)
            return;

        var (width, height) = ImageSize;

        if ((width - MidLineWidth).IsOdd())
            throw new InvalidOperationException("(Grid width - Mid line width) must be an even number");

        var midIndex1 = (width - MidLineWidth) / 2;

        for (var i = 0; i < width; i++)
            for (var j = 0; j < MidLineWidth; j++)
                bitmap[i, j + midIndex1] = MidLineColor;

        for (var i = 0; i < MidLineWidth; i++)
            for (var j = 0; j < height; j++)
                bitmap[i + midIndex1, j] = MidLineColor;
    }

    private void AddBorderLines(Image<Rgba32> bitmap)
    {
        if (!BorderLineEnabled)
            return;

        var (width, height) = ImageSize;

        for (var i = 0; i < width; i++)
            for (var j = 0; j < BorderLineWidth; j++)
            {
                bitmap[i, j] = BorderLineColor;
                bitmap[i, height - 1 - j] = BorderLineColor;
            }

        for (var i = 0; i < BorderLineWidth; i++)
            for (var j = 0; j < height; j++)
            {
                bitmap[i, j] = BorderLineColor;
                bitmap[width - 1 - i, j] = BorderLineColor;
            }
    }


    public GrVisualGridImageComposer SetGridColorsOpacity(double opacity)
    {
        BaseSquareColor = BaseSquareColor.SetAlpha(opacity);
        BaseLineColor = BaseLineColor.SetAlpha(opacity);
        MidLineColor = MidLineColor.SetAlpha(opacity);
        BorderLineColor = BorderLineColor.SetAlpha(opacity);

        return this;
    }
}