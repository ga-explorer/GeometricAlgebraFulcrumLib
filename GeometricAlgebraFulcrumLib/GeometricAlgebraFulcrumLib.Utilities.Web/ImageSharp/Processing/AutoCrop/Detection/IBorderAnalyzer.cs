﻿using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using GeometricAlgebraFulcrumLib.Utilities.Web.ImageSharp.Processing.AutoCrop.Models;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.ImageSharp.Processing.AutoCrop.Detection;

public interface IBorderAnalyzer<TPixel> where TPixel : unmanaged, IPixel<TPixel>
{
    IBorderAnalysis Analyze(Image<TPixel> image, Rectangle rectangle, int? colorThreshold, float? bucketThreshold);
}