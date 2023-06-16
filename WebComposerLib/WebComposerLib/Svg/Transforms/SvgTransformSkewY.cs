using System.Text;

namespace WebComposerLib.Svg.Transforms
{
    public sealed class SvgTransformSkewY : SvgTransform
    {
        public static SvgTransformSkewY Create(double angle)
        {
            return new SvgTransformSkewY { AngleValue = angle };
        }


        public double AngleValue { get; set; }

        public override string ValueText
            => new StringBuilder()
                .Append("skewY(")
                .Append(AngleValue)
                .Append(")")
                .ToString();


        private SvgTransformSkewY()
        {
        }
    }
}