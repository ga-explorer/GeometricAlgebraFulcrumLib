using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using GeometricAlgebraFulcrumLib.Utilities.Web.ImageSharp.Processing.AutoCrop.Models;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.ImageSharp.Processing.AutoCrop.Analyzers;

public interface ICropAnalyzer<TPixel> where TPixel : unmanaged, IPixel<TPixel>
{
    ICropAnalysis GetAnalysis(Image<TPixel> image, int? colorThreshold, float? bucketTreshold);
}