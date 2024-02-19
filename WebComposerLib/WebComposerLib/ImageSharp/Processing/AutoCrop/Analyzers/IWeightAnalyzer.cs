using WebComposerLib.ImageSharp.Processing.AutoCrop.Models;

namespace WebComposerLib.ImageSharp.Processing.AutoCrop.Analyzers;

public interface IWeightAnalyzer<TPixel> where TPixel : unmanaged, IPixel<TPixel>
{
    IWeightAnalysis GetAnalysis(Image<TPixel> image, Color backgroundColor, int sampleResolution = 5);
}