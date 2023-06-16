using GraphicsComposerLib.Rendering.ImageSharp.Processing.AutoCrop.Extensions;
using GraphicsComposerLib.Rendering.ImageSharp.Processing.AutoCrop.Models;

namespace GraphicsComposerLib.Rendering.ImageSharp.Processing.AutoCrop.Detection
{
    public sealed class RgbBorderAnalyzer : BorderAnalyzer<Rgb24>
    {
        public override IBorderAnalysis Analyze(Image<Rgb24> image, Rectangle rectangle, int? colorThreshold, float? bucketThreshold)
        {
            var h = image.Height;
            var w = image.Width;

            // A list of counted colors found along the rectangle border
            var colors = new Dictionary<Color, int>(colorThreshold ?? 100);

            // A list of counted color buckets
            var buckets = new Dictionary<int, int>(PixelExtensions.GetMaxColorBuckets());

            // A loop of pixels along the left edge from top to bottom
            for (var y = 0; y < h; y++)
            {
                // Get the color value
                var c = image[0, y];
                var ce = colors.ContainsKey(c);

                if (ce)
                {
                    colors[c]++;
                }
                else if (colors.Count < colorThreshold)
                {
                    colors.Add(c, 1);
                }

                if (ce || colors.Count >= colorThreshold)
                {
                    var cb = c.ToColorBucket();
                    if (buckets.ContainsKey(cb))
                    {
                        buckets[cb]++;
                    }
                    else
                    {
                        buckets.Add(cb, 1);
                    }
                }
            }

            // A loop of pixels along the right edge from top to bottom
            for (var y = 0; y < h; y++)
            {
                var c = image[rectangle.Right - 1, y];
                var ce = colors.ContainsKey(c);

                if (ce)
                {
                    colors[c]++;
                }
                else if (colors.Count < colorThreshold)
                {
                    colors.Add(c, 1);
                }

                if (ce || colors.Count >= colorThreshold)
                {
                    var cb = c.ToColorBucket();
                    if (buckets.ContainsKey(cb))
                    {
                        buckets[cb]++;
                    }
                    else
                    {
                        buckets.Add(cb, 1);
                    }
                }
            }

            image.ProcessPixelRows((accessor) =>
            {
                // A loop of pixels along the top edge from left to right
                var row = accessor.GetRowSpan(0);

                for (var x = 0; x < w; x++)
                {
                    var c = row[x];
                    var ce = colors.ContainsKey(c);

                    if (ce)
                    {
                        colors[c]++;
                    }
                    else if (colors.Count < colorThreshold)
                    {
                        colors.Add(c, 1);
                    }

                    if (ce || colors.Count >= colorThreshold)
                    {
                        var cb = c.ToColorBucket();
                        if (buckets.ContainsKey(cb))
                        {
                            buckets[cb]++;
                        }
                        else
                        {
                            buckets.Add(cb, 1);
                        }
                    }
                }

                // A loop of pixels along the bottom edge from left to right
                row = accessor.GetRowSpan(rectangle.Bottom - 1);

                for (var x = 0; x < w; x++)
                {
                    var c = row[x];
                    var ce = colors.ContainsKey(c);

                    if (ce)
                    {
                        colors[c]++;
                    }
                    else if (colors.Count < colorThreshold)
                    {
                        colors.Add(c, 1);
                    }

                    if (ce || colors.Count >= colorThreshold)
                    {
                        var cb = c.ToColorBucket();
                        if (buckets.ContainsKey(cb))
                        {
                            buckets[cb]++;
                        }
                        else
                        {
                            buckets.Add(cb, 1);
                        }
                    }
                }
            });

            return new BorderAnalysis<Rgb24>(colors, buckets, colorThreshold, bucketThreshold);
        }
    }
}
