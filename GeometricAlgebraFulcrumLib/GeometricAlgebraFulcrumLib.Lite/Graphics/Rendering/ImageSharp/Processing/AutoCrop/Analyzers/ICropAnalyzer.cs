using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Models;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Analyzers;

public interface ICropAnalyzer<TPixel> where TPixel : unmanaged, IPixel<TPixel>
{
    ICropAnalysis GetAnalysis(Image<TPixel> image, int? colorThreshold, float? bucketTreshold);
}