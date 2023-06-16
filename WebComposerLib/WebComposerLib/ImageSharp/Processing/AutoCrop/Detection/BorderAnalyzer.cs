using WebComposerLib.ImageSharp.Processing.AutoCrop.Models;

namespace WebComposerLib.ImageSharp.Processing.AutoCrop.Detection
{
    public abstract class BorderAnalyzer<TPixel> : IBorderAnalyzer<TPixel> where TPixel : unmanaged, IPixel<TPixel>
    {
        public abstract IBorderAnalysis Analyze(Image<TPixel> image, Rectangle rectangle, int? colorThreshold, float? bucketThreshold);
    }
}