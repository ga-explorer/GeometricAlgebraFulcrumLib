using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Detection;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Analyzers;

public sealed class RgbaThresholdAnalyzer : CropAnalyzer<Rgba32>
{
    private readonly IBorderAnalyzer<Rgba32> _inspector;

    public RgbaThresholdAnalyzer()
    {
        _inspector = new RgbaBorderAnalyzer();
    }

    protected override IBorderAnalysis GetBorderAnalysis(Image<Rgba32> image, Rectangle rectangle, int? colorThreshold, float? bucketTreshold)
    {
        return _inspector.Analyze(image, rectangle, colorThreshold, bucketTreshold);
    }

    protected override Rectangle GetBoundingBox(Image<Rgba32> image, Rectangle rectangle, IBorderAnalysis borderAnalysis, int colorThreshold)
    {
        var h = rectangle.Bottom;
        var w = rectangle.Right;

        var backgroundColor = borderAnalysis.Background.ToPixel<Rgba32>();

        var xn = w;
        var xm = rectangle.X;
        var yn = h;
        var ym = rectangle.Y;

        image.ProcessPixelRows((accessor) =>
        {
            for (var y = rectangle.Y; y < h; y++)
            {
                var row = accessor.GetRowSpan(y);

                for (var x = rectangle.X; x < w; x++)
                {
                    var c = row[x];
                    var ac = Math.Max(c.A * 0.003921568627451, 1);
                    var bd = Math.Abs(c.B - backgroundColor.B) * ac;
                    var gd = Math.Abs(c.G - backgroundColor.G) * ac;
                    var rd = Math.Abs(c.R - backgroundColor.R) * ac;

                    if (0.299 * rd + 0.587 * gd + 0.114 * bd <= colorThreshold)
                    {
                        var ad = Math.Abs(c.A - backgroundColor.A);
                        if (ad < colorThreshold)
                        {
                            continue;
                        }
                    }

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