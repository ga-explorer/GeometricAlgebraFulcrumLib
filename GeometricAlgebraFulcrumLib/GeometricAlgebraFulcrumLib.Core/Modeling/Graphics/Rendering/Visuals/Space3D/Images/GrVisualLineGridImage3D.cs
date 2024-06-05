using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Images;

public class GrVisualLineGridImage3D :
    GrVisualImage3D
{
    public Color BaseSquareColor { get; set; } = Color.LightYellow;

    public int BaseSquareCount { get; set; } = 4;

    public int BaseSquareSize { get; set; } = 64;
    
    public int TotalSquareSize 
        => BaseSquareSize + BaseLineWidth;
    

    public Color BaseLineColor { get; set; } = Color.BurlyWood;

    public int BaseLineWidth { get; set; } = 2;
    
    public bool BaseLineEnabled 
        => BaseLineWidth > 0;


    public Color MidLineColor { get; set; } = Color.SandyBrown;

    public int MidLineWidth { get; set; } = 4;
    
    public bool MidLineEnabled 
        => MidLineWidth > 0;


    public Color BorderLineColor { get; set; } = Color.SaddleBrown;

    public int BorderLineWidth { get; set; } = 3;

    public bool BorderLineEnabled 
        => BorderLineWidth > 0;

    
    public GrVisualLineGridImage3D(string name) 
        : base(name)
    {
    }

        
    public override bool IsValid()
    {
        throw new NotImplementedException();
    }

    public override Pair<int> GetSize()
    {
        return new Pair<int>(
            (BaseSquareSize + BaseLineWidth) * BaseSquareCount + BaseLineWidth,
            (BaseSquareSize + BaseLineWidth) * BaseSquareCount + BaseLineWidth
        );
    }

    private void AddBaseColor(Image<Rgba32> bitmap)
    {
        var (width, height) = GetSize();

        for (var i = 0; i < width; i++)
        for (var j = 0; j < height; j++)
            bitmap[i, j] = BaseSquareColor;
    }

    private void AddBaseLines(Image<Rgba32> bitmap)
    {
        if (!BaseLineEnabled) 
            return;

        var (width, height) = GetSize();

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

        var (width, height) = GetSize();

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

        var (width, height) = GetSize();

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

    public override Image GetImage()
    {
        var (width, height) = GetSize();
        var bitmap = new Image<Rgba32>(width, height);

        AddBaseColor(bitmap);
        
        AddBaseLines(bitmap);
        
        AddMidLines(bitmap);

        AddBorderLines(bitmap);

        return bitmap;
    }

}