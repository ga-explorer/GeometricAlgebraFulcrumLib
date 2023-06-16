using WebComposerLib.ImageSharp.Processing.AutoCrop.Models;

namespace WebComposerLib.ImageSharp.Processing.AutoCrop.Detection
{
    public interface IBorderAnalyzer<TPixel> where TPixel : unmanaged, IPixel<TPixel>
    {
        IBorderAnalysis Analyze(Image<TPixel> image, Rectangle rectangle, int? colorThreshold, float? bucketThreshold);
    }
}
