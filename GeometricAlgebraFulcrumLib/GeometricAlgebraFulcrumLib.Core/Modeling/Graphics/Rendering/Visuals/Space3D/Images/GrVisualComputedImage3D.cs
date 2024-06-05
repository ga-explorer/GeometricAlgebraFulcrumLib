using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Images;

public sealed class GrVisualComputedImage3D :
    GrVisualImage3D
{
    public int Width { get; set; }

    public int Height { get; set; }

    public Func<int, int, Color> ColorFunc { get; set; }

    

    public GrVisualComputedImage3D(string name) 
        : base(name)
    {
    }

        
    public override bool IsValid()
    {
        throw new NotImplementedException();
    }

    public override Pair<int> GetSize()
    {
        return new Pair<int>(Width, Height);
    }

    public override Image GetImage()
    {
        var bitmap = new Image<Rgba32>(Width, Height);

        for (var i = 0; i < Width; i++)
        for (var j = 0; j < Height; j++)
        {
            bitmap[i, j] = ColorFunc(i, j);
        }

        return bitmap;
    }

}