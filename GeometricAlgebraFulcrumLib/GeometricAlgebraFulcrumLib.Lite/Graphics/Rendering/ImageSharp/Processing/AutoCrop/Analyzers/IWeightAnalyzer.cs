﻿using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Models;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Analyzers;

public interface IWeightAnalyzer<TPixel> where TPixel : unmanaged, IPixel<TPixel>
{
    IWeightAnalysis GetAnalysis(Image<TPixel> image, Color backgroundColor, int sampleResolution = 5);
}