using GraphicsComposerLib.Rendering.ImageSharp.Processing.AutoCrop.Analyzers;
using GraphicsComposerLib.Rendering.ImageSharp.Processing.AutoCrop.Models;

namespace GraphicsComposerLib.Rendering.ImageSharp.Processing.AutoCrop
{
    public sealed class RgbAnalysisProcessor : AnalysisProcessor<Rgb24>
    {
        private readonly IWeightAnalyzer<Rgb24> _weightAnalyzer;
        private readonly ICropAnalyzer<Rgb24> _cropAnalyzer;

        public RgbAnalysisProcessor(Configuration configuration, IAutoCropSettings settings, Image<Rgb24> source) : base(configuration, settings, source)
        {
            _cropAnalyzer = new RgbThresholdAnalyzer();
            _weightAnalyzer = new RgbWeightAnalyzer();
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
}
