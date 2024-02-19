using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Extensions;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Models;

public sealed class BorderAnalysis : IBorderAnalysis
{
    public int Colors { get; }
    public Color Background { get; }
    public float BucketRatio { get; }
    public bool Success { get; }
}

public sealed class BorderAnalysis<TPixel> : IBorderAnalysis where TPixel : unmanaged, IPixel
{
    public BorderAnalysis(IDictionary<Color, int> colors, IDictionary<int, int> buckets, int? colorThreshold, float? bucketThreshold)
    {
        if (colors == null)
            throw new ArgumentNullException(nameof(colors));

        Colors = colors.Count;

        var mostPresentColor = colors.OrderByDescending(x => x.Value)
            .First();

        var mostPresentBucket = mostPresentColor.Key.ToColorBucket();

        Background = mostPresentColor.Key;

        if (buckets.Count > 0)
        {
            BucketRatio = buckets[mostPresentBucket] / (float)buckets.Sum(x => x.Value);
        }
        else
        {
            BucketRatio = 1;
        }


        bool thresholdSuccess = false;

        if (bucketThreshold.HasValue && BucketRatio >= bucketThreshold.Value)
            thresholdSuccess = true;

        if (colorThreshold.HasValue && colors.Count < colorThreshold.Value)
            thresholdSuccess = true;

        Success = colors.Count > 0 && thresholdSuccess;
    }

    public int Colors { get; }
    public Color Background { get; }
    public float BucketRatio { get; }
    public bool Success { get; }
}