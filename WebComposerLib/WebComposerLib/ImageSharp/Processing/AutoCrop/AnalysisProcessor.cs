using SixLabors.ImageSharp.Processing.Processors;
using WebComposerLib.ImageSharp.Processing.AutoCrop.Models;

namespace WebComposerLib.ImageSharp.Processing.AutoCrop
{
    internal sealed class AnalysisProcessor : IImageProcessor
    {
        private readonly IAutoCropSettings _settings;

        public ICropAnalysis CropAnalysis { get; set; }

        public IWeightAnalysis WeightAnalysis { get; set; }


        public AnalysisProcessor(IAutoCropSettings settings)
        {
            _settings = settings;
        }

        public IImageProcessor<TPixel> CreatePixelSpecificProcessor<TPixel>(Configuration configuration, Image<TPixel> source, Rectangle sourceRectangle) where TPixel : unmanaged, IPixel<TPixel>
        {
            if (source is Image<Rgb24> rgbSource)
            {
                var processor = new RgbAnalysisProcessor(configuration, _settings, rgbSource);

                CropAnalysis = processor.GetCropAnalysis();

                if (_settings.AnalyzeWeights)
                    WeightAnalysis = processor.GetWeightAnalysis(CropAnalysis);

                return processor as IImageProcessor<TPixel>;
            }
            else if (source is Image<Rgba32> rgbaSource)
            {
                var processor = new RgbaAnalysisProcessor(configuration, _settings, rgbaSource);

                CropAnalysis = processor.GetCropAnalysis();

                return processor as IImageProcessor<TPixel>;
            }

            throw new NotSupportedException("Unsupported pixel type");
        }
    }

    public abstract class AnalysisProcessor<TPixel> : IImageProcessor<TPixel> where TPixel : unmanaged, IPixel<TPixel>
    {
        protected readonly Configuration Configuration;
        protected readonly IAutoCropSettings Settings;
        protected readonly Image<TPixel> Source;

        public ICropAnalysis Analysis { get; }

        protected AnalysisProcessor(Configuration configuration, IAutoCropSettings settings, Image<TPixel> source)
        {
            Configuration = configuration;
            Source = source;
            Settings = settings;
        }

        public void Execute()
        {
            // Do nothing
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public abstract ICropAnalysis GetCropAnalysis();
        public abstract IWeightAnalysis GetWeightAnalysis(ICropAnalysis cropAnalysis);

        protected virtual void Dispose(bool disposing)
        {

        }
    }
}
