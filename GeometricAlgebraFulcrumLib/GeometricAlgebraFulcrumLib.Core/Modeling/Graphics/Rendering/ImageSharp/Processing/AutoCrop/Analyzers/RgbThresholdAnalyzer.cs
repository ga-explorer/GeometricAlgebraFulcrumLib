using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Detection;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Analyzers;

public sealed class RgbThresholdAnalyzer : CropAnalyzer<Rgb24>
{
    private readonly IBorderAnalyzer<Rgb24> _inspector;

    public RgbThresholdAnalyzer()
    {
        _inspector = new RgbBorderAnalyzer();
    }

    protected override IBorderAnalysis GetBorderAnalysis(Image<Rgb24> image, Rectangle rectangle, int? colorThreshold, float? bucketTreshold)
    {
        return _inspector.Analyze(image, rectangle, colorThreshold, bucketTreshold);
    }

    protected override Rectangle GetBoundingBox(Image<Rgb24> image, Rectangle rectangle, IBorderAnalysis borderAnalysis, int colorThreshold)
    {
        var w = rectangle.Right;
        var h = rectangle.Bottom;

        var backgroundColor = borderAnalysis.Background.ToPixel<Rgb24>();

        // Calculated x-min
        var xn = w;

        // Calculated x-max
        var xm = rectangle.X;

        // Calculated y-min
        var yn = h;

        // Calculated y-max
        var ym = rectangle.Y;

        image.ProcessPixelRows((accessor) =>
        {
            for (var y = rectangle.Y; y < h; y++)
            {
                var row = accessor.GetRowSpan(y);

                for (var x = rectangle.X; x < w; x++)
                {
                    // Current pixel
                    var c = row[x];

                    // Delta color values
                    var bd = Math.Abs(c.B - backgroundColor.B);
                    var gd = Math.Abs(c.G - backgroundColor.G);
                    var rd = Math.Abs(c.R - backgroundColor.R);

                    // Grayscale operation on color delta
                    // This is done to properly evaluate if 
                    // the perceptual differences are above threshold

                    if (0.299 * rd + 0.587 * gd + 0.114 * bd <= colorThreshold)
                        continue;

                    if (x < xn) xn = x;
                    if (x > xm) xm = x;
                    if (y < yn) yn = y;
                    if (y > ym) ym = y;
                }
            }
        });

        return new Rectangle(xn, yn, xm - xn + 1, ym - yn + 1);
    }
}