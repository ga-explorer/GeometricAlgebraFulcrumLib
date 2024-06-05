using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using SixLabors.ImageSharp;
using GeometricAlgebraFulcrumLib.Utilities.Web.Html.Media;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space2D.Images;

public sealed class GrVisualLaTeXText2D :
    GrVisualImage2D
{
    public static GrVisualLaTeXText2D Create(string name, WclHtmlImageDataUrlCache pngCache, ILinFloat64Vector2D position, double scalingFactor)
    {
        return new GrVisualLaTeXText2D(
            name, 
            pngCache, 
            position, 
            scalingFactor
        );
    }
        

    public WclHtmlImageDataUrlCache ImageCache { get; }

    public string Key { get; }

    public double ScalingFactor { get; }

    public ILinFloat64Vector2D Position { get; }
        

    private GrVisualLaTeXText2D(string name, WclHtmlImageDataUrlCache pngCache, ILinFloat64Vector2D position, double scalingFactor) 
        : base(name)
    {
        ImageCache = pngCache;
        Key = name;
        Position = position;
        ScalingFactor = scalingFactor;
    }

    private GrVisualLaTeXText2D(string name, WclHtmlImageDataUrlCache pngCache, string key, ILinFloat64Vector2D position, double scalingFactor) 
        : base(name)
    {
        ImageCache = pngCache;
        Key = key;
        Position = position;
        ScalingFactor = scalingFactor;
    }


    public override Pair<int> GetSize()
    {
        var image = ImageCache[Key];

        return new Pair<int>(image.Width, image.Height);
    }

    public WclHtmlImageUrl GetImageData()
    {
        return ImageCache[Key];
    }
    
    public override Image GetImage()
    {
        throw new NotImplementedException();
    }
        
    public override bool IsValid()
    {
        return Position.IsValid();
    }
        
}