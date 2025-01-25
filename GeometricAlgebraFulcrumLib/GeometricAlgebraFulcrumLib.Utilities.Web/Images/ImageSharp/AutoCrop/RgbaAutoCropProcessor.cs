using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using GeometricAlgebraFulcrumLib.Utilities.Web.Images.ImageSharp.AutoCrop.Analyzers;
using GeometricAlgebraFulcrumLib.Utilities.Web.Images.ImageSharp.AutoCrop.Models;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Images.ImageSharp.AutoCrop;

public sealed class RgbaAutoCropProcessor : AutoCropProcessor<Rgba32>
{
    private readonly ICropAnalyzer<Rgba32> _cropAnalyzer;
    private readonly IWeightAnalyzer<Rgba32> _weightAnalyzer;

    public RgbaAutoCropProcessor(Configuration configuration, IAutoCropSettings settings, Image<Rgba32> source) : base(configuration, settings, source)
    {
        _cropAnalyzer = new RgbaThresholdAnalyzer();
        _weightAnalyzer = new RgbaWeightAnalyzer();

        CropAnalysis = _cropAnalyzer.GetAnalysis(source, settings.ColorThreshold, settings.BucketThreshold);

        if (settings.AnalyzeWeights)
            WeightAnalysis = _weightAnalyzer.GetAnalysis(source, CropAnalysis.Background);
    }
}