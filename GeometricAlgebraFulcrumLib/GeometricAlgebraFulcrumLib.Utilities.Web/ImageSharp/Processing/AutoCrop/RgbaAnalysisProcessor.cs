using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using GeometricAlgebraFulcrumLib.Utilities.Web.ImageSharp.Processing.AutoCrop.Analyzers;
using GeometricAlgebraFulcrumLib.Utilities.Web.ImageSharp.Processing.AutoCrop.Models;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.ImageSharp.Processing.AutoCrop;

public sealed class RgbaAnalysisProcessor : AnalysisProcessor<Rgba32>
{
    private readonly IWeightAnalyzer<Rgba32> _weightAnalyzer;
    private readonly ICropAnalyzer<Rgba32> _cropAnalyzer;

    public RgbaAnalysisProcessor(Configuration configuration, IAutoCropSettings settings, Image<Rgba32> source) : base(configuration, settings, source)
    {
        _cropAnalyzer = new RgbaThresholdAnalyzer();
        _weightAnalyzer = new RgbaWeightAnalyzer();
    }

    public override ICropAnalysis GetCropAnalysis()
    {
        return _cropAnalyzer.GetAnalysis(Source, Settings.ColorThreshold, Settings.BucketThreshold);
    }

    public override IWeightAnalysis GetWeightAnalysis(ICropAnalysis cropAnalysis)
    {
        return _weightAnalyzer.GetAnalysis(Source, cropAnalysis.Background);
    }
}