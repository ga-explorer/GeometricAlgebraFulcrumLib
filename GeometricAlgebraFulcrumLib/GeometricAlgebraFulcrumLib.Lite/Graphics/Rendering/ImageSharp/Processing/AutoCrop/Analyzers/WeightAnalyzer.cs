using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Models;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Analyzers;

public abstract class WeightAnalyzer<TPixel> : IWeightAnalyzer<TPixel> where TPixel : unmanaged, IPixel<TPixel>
{
    public IWeightAnalysis GetAnalysis(Image<TPixel> image, Color backgroundColor, int sampleResolution = 5)
    {
        PointF weight;

        using (var weightMap = GetWeightMap(image, sampleResolution))
        {
            weight = GetWeight(weightMap, backgroundColor.ToPixel<TPixel>());
        }

        return new WeightAnalysis
        {
            Weight = weight
        };
    }

    protected virtual Image<TPixel> GetWeightMap(Image<TPixel> image, int resolution)
    {
        var options = new ResizeOptions
        {
            Mode = ResizeMode.Stretch,
            Size = new Size(image.Width, image.Height),
            Sampler = KnownResamplers.Triangle,
        };

        return image.Clone(x => x.Resize(options));
    }

    protected abstract PointF GetWeight(Image<TPixel> weightMap, TPixel backgroundColor);
}