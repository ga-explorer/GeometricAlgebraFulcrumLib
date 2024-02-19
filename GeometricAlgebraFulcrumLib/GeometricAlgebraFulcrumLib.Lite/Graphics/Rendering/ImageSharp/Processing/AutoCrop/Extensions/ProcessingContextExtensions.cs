using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Models;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Extensions;

public static class ProcessingContextExtensions
{
    public static void AutoCrop(this IImageProcessingContext context)
    {
        context.ApplyProcessor(new AutoCropProcessor(new AutoCropSettings()));
    }

    public static void AutoCrop(this IImageProcessingContext context, IAutoCropSettings settings)
    {
        context.ApplyProcessor(new AutoCropProcessor(settings));
    }

    public static void AutoCrop(this IImageProcessingContext context, IAutoCropSettings settings, out ICropAnalysis analysis)
    {
        var processor = new AutoCropProcessor(settings);
        context.ApplyProcessor(processor);

        analysis = processor.CropAnalysis;
    }

    public static void AutoCropKnown(this IImageProcessingContext context, IAutoCropSettings settings, ICropAnalysis cropAnalysis, IWeightAnalysis weightAnalysis = null)
    {
        var processor = new PreCalculatedAutoCropProcessor(settings, cropAnalysis, weightAnalysis);
        context.ApplyProcessor(processor);
    }

    public static void AutoCrop(this IImageProcessingContext context, IAutoCropSettings settings, out ICropAnalysis cropAnalysis, out IWeightAnalysis weightAnalysis)
    {
        var processor = new AutoCropProcessor(settings);
        context.ApplyProcessor(processor);

        cropAnalysis = processor.CropAnalysis;
        weightAnalysis = processor.WeightAnalysis;
    }

    public static void AnalyzeCrop(this IImageProcessingContext context, IAutoCropSettings settings, out ICropAnalysis analysis)
    {
        var processor = new AnalysisProcessor(settings);
        context.ApplyProcessor(processor);

        analysis = processor.CropAnalysis;
    }

    public static void AnalyzeCrop(this IImageProcessingContext context, IAutoCropSettings settings, out ICropAnalysis cropAnalysis, out IWeightAnalysis weightAnalysis)
    {
        var processor = new AnalysisProcessor(settings);
        context.ApplyProcessor(processor);

        cropAnalysis = processor.CropAnalysis;
        weightAnalysis = processor.WeightAnalysis;
    }

    public static bool TryAutoCrop(this IImageProcessingContext context, IAutoCropSettings settings)
    {
        try
        {
            context.AutoCrop(settings);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool TryAutoCrop(this IImageProcessingContext context, IAutoCropSettings settings, out ICropAnalysis analysis)
    {
        try
        {
            context.AutoCrop(settings, out analysis);
            return true;
        }
        catch (Exception)
        {
            analysis = null;
            return false;
        }
    }

    public static bool TryAnalyzeCrop(this IImageProcessingContext context, IAutoCropSettings settings, out ICropAnalysis analysis)
    {
        try
        {
            context.AnalyzeCrop(settings, out analysis);
            return true;
        }
        catch (Exception)
        {
            analysis = null;
            return false;
        }
    }

    public static bool TryAnalyzeCrop(this IImageProcessingContext context, IAutoCropSettings settings, out ICropAnalysis cropAnalysis, out IWeightAnalysis weightAnalysis)
    {
        try
        {
            context.AnalyzeCrop(settings, out cropAnalysis, out weightAnalysis);
            return true;
        }
        catch (Exception)
        {
            cropAnalysis = null;
            weightAnalysis = null;
            return false;
        }
    }
}