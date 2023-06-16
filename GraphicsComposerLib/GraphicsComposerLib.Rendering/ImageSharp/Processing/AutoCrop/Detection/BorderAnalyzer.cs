using GraphicsComposerLib.Rendering.ImageSharp.Processing.AutoCrop.Models;

namespace GraphicsComposerLib.Rendering.ImageSharp.Processing.AutoCrop.Detection
{
    public abstract class BorderAnalyzer<TPixel> : IBorderAnalyzer<TPixel> where TPixel : unmanaged, IPixel<TPixel>
    {
        public abstract IBorderAnalysis Analyze(Image<TPixel> image, Rectangle rectangle, int? colorThreshold, float? bucketThreshold);
    }
}