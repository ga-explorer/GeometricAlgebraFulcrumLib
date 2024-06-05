using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using GeometricAlgebraFulcrumLib.Utilities.Web.ImageSharp.Processing.AutoCrop.Models;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.ImageSharp.Processing.AutoCrop.Analyzers;

public interface IWeightAnalyzer<TPixel> where TPixel : unmanaged, IPixel<TPixel>
{
    IWeightAnalysis GetAnalysis(Image<TPixel> image, Color backgroundColor, int sampleResolution = 5);
}