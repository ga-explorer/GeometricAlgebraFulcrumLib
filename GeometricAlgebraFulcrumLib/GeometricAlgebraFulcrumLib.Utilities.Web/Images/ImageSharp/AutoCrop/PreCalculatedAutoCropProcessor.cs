using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Web.Images.ImageSharp.AutoCrop.Models;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Images.ImageSharp.AutoCrop;

public sealed class PreCalculatedAutoCropProcessor : AutoCropProcessor
{
    private readonly IAutoCropSettings _settings;

    public PreCalculatedAutoCropProcessor(IAutoCropSettings settings, ICropAnalysis cropAnalysis, IWeightAnalysis weightAnalysis = null) : base(settings)
    {
        _settings = settings;
        CropAnalysis = cropAnalysis;
        WeightAnalysis = weightAnalysis;
    }

    public override ICloningImageProcessor<TPixel> CreatePixelSpecificCloningProcessor<TPixel>(Configuration configuration, Image<TPixel> source, Rectangle sourceRectangle)
    {
        return new PreCalculatedAutoCropProcessor<TPixel>(configuration, _settings, source, CropAnalysis, WeightAnalysis);
    }
}

public sealed class PreCalculatedAutoCropProcessor<TPixel> : AutoCropProcessor<TPixel> where TPixel : unmanaged, IPixel<TPixel>
{
    public PreCalculatedAutoCropProcessor(Configuration configuration, IAutoCropSettings settings, Image<TPixel> source, ICropAnalysis cropAnalysis, IWeightAnalysis weightAnalysis) : base(configuration, settings, source)
    {
        CropAnalysis = cropAnalysis;
        WeightAnalysis = weightAnalysis;
    }
}