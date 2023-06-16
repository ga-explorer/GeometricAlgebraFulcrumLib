using WebComposerLib.ImageSharp.Processing.AutoCrop.Analyzers;
using WebComposerLib.ImageSharp.Processing.AutoCrop.Models;

namespace WebComposerLib.ImageSharp.Processing.AutoCrop
{
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
}
