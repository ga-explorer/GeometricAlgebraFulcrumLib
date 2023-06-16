namespace GraphicsComposerLib.Rendering.ImageSharp.Processing.AutoCrop.Analyzers
{
    public sealed class RgbWeightAnalyzer : WeightAnalyzer<Rgb24>
    {
        protected override PointF GetWeight(Image<Rgb24> weightMap, Rgb24 backgroundColor)
        {
            var offset = weightMap.Width / 2.0;
            var average = 1 / (double)(weightMap.Width * weightMap.Height);

            var w = weightMap.Width;
            var h = weightMap.Height;

            var weight = new PointF(0, 0);

            weightMap.ProcessPixelRows((accessor) =>
            {
                for (var y = 0; y < h; y++)
                {
                    // Normalized vector position
                    var yn = (y - offset) / offset;

                    var row = accessor.GetRowSpan(y);

                    for (var x = 0; x < w; x++)
                    {
                        // Normalized vector position
                        var xn = (x - offset) / offset;

                        // Current pixel
                        var c = row[x];

                        // Delta color values
                        var bd = Math.Abs(c.B - backgroundColor.B) * Constants.BytePrecision;
                        var gd = Math.Abs(c.G - backgroundColor.G) * Constants.BytePrecision;
                        var rd = Math.Abs(c.R - backgroundColor.R) * Constants.BytePrecision;

                        var d = 0.299 * rd + 0.587 * gd + 0.114 * bd;
                        var v = new PointF((float)(xn * d * average), (float)(yn * d * average));

                        weight = new PointF(weight.X + v.X, weight.Y + v.Y);
                    }
                }
            });

            return weight;
        }
    }
}
