using DataStructuresLib.Basic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Images;

public sealed class GrVisualColorGridImage3D :
    GrVisualImage3D
{
    public int SquareSize { get; set; }

    public Color[,] ColorArray { get; set; }

    public int RowCount 
        => ColorArray.GetLength(0);

    public int ColumnCount 
        => ColorArray.GetLength(1);

    
    public GrVisualColorGridImage3D(string name) 
        : base(name)
    {
    }


    public override Pair<int> GetSize()
    {
        return new Pair<int>(
            RowCount * SquareSize,
            ColumnCount * SquareSize
        );
    }
    
    public override Image GetImage()
    {
        var (width, height) = GetSize();
        var bitmap = new Image<Rgba32>(width, height);

        for (var i = 0; i < RowCount; i++)
        {
            var i1 = i * SquareSize;

            for (var j = 0; j < ColumnCount; j++)
            {
                var j1 = j * SquareSize;

                var color = ColorArray[i, j];

                for (var x = 0; x < SquareSize; x++)
                for (var y = 0; y < SquareSize; y++)
                    bitmap[x + i1, y + j1] = color;
            }
        }

        return bitmap;
    }

}