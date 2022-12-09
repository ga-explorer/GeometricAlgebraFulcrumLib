using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.Images;
using NumericalGeometryLib.BasicMath.Tuples;
using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Images;

public sealed class GrVisualLaTeXText3D :
    GrVisualImage3D
{
    public GrImageBase64StringCache ImageCache { get; }

    public string Key { get; }

    public double ScalingFactor { get; }

    public IFloat64Tuple3D Origin { get; }

    
    public GrVisualLaTeXText3D(string name, GrImageBase64StringCache pngCache, IFloat64Tuple3D origin, double scalingFactor) 
        : base(name)
    {
        ImageCache = pngCache;
        Key = name;
        Origin = origin;
        ScalingFactor = scalingFactor;
    }

    public GrVisualLaTeXText3D(string name, GrImageBase64StringCache pngCache, string key, IFloat64Tuple3D origin, double scalingFactor) 
        : base(name)
    {
        ImageCache = pngCache;
        Key = key;
        Origin = origin;
        ScalingFactor = scalingFactor;
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