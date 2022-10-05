using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.Images;
using NumericalGeometryLib.BasicMath.Tuples;
using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Images;

public sealed class GrVisualLaTeXText3D :
    GrVisualImage3D
{
    public GrImageBase64StringCache ImageCache { get; }

    public string Key 
        => Name;

    public double ScalingFactor { get; set; } = 1;

    public ITuple3D Origin { get; set; }

    
    public GrVisualLaTeXText3D(GrImageBase64StringCache pngCache, string key) 
        : base(key)
    {
        ImageCache = pngCache;
    }


    public override Pair<int> GetSize()
    {
        var image = ImageCache[Key];

        return new Pair<int>(image.Width, image.Height);
    }

    public GrImageBase64String GetImageData()
    {
        return ImageCache[Key];
    }
    
    public override Image GetImage()
    {
        throw new NotImplementedException();
    }
}