using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using GeometricAlgebraFulcrumLib.Utilities.Web.Images.ImageSharp.AutoCrop.Models;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Images.ImageSharp.AutoCrop.Detection;

public abstract class BorderAnalyzer<TPixel> : IBorderAnalyzer<TPixel> where TPixel : unmanaged, IPixel<TPixel>
{
    public abstract IBorderAnalysis Analyze(Image<TPixel> image, Rectangle rectangle, int? colorThreshold, float? bucketThreshold);
}