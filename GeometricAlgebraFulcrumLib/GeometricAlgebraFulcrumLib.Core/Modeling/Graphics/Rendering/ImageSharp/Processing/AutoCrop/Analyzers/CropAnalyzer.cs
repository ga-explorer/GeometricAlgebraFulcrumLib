using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Extensions;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Analyzers;

public abstract class CropAnalyzer<TPixel> : ICropAnalyzer<TPixel> where TPixel : unmanaged, IPixel<TPixel>
{
    protected abstract IBorderAnalysis GetBorderAnalysis(Image<TPixel> image, Rectangle rectangle, int? colorThreshold, float? bucketTreshold);
    protected abstract Rectangle GetBoundingBox(Image<TPixel> image, Rectangle rectangle, IBorderAnalysis borderAnalysis, int colorThreshold);

    public virtual ICropAnalysis GetAnalysis(Image<TPixel> image, int? colorThreshold, float? bucketTreshold)
    {
        var outerBox = new Rectangle(0, 0, image.Width, image.Height);
        var imageBox = outerBox;

        if (colorThreshold.HasValue == false && bucketTreshold.HasValue == false)
            colorThreshold = 35;

        var borderInspection = GetBorderAnalysis(image, imageBox, colorThreshold, bucketTreshold);
        if (borderInspection.Success == false)
        {
            if (colorThreshold.HasValue)
                colorThreshold = (int)Math.Round(colorThreshold.Value * 0.5);

            if (colorThreshold.HasValue && bucketTreshold.HasValue)
                bucketTreshold = null;

            imageBox = imageBox.Contract(10);

            var additionalInspection = GetBorderAnalysis(image, imageBox, colorThreshold, bucketTreshold);
            if (additionalInspection.Success)
            {
                borderInspection = additionalInspection;
            }
        }

        Rectangle boundingBox;
        bool foundBoundingBox;

        if (borderInspection.Success == false)
        {
            boundingBox = outerBox;
            foundBoundingBox = false;
        }
        else
        {
            boundingBox = GetBoundingBox(image, imageBox, borderInspection, colorThreshold ?? 35);
            foundBoundingBox = ValidateRectangle(boundingBox);
        }

        return new CropAnalysis
        {
            Background = borderInspection.Background,
            BoundingBox = boundingBox,
            Success = foundBoundingBox
        };
    }

    private static bool ValidateRectangle(Rectangle rectangle)
    {
        if (rectangle.Width < 3) return false;
        if (rectangle.Height < 3) return false;

        return true;
    }
}