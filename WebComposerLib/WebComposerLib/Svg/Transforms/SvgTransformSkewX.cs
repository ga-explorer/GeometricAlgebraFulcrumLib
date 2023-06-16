using System.Text;

namespace WebComposerLib.Svg.Transforms
{
    public sealed class SvgTransformSkewX : SvgTransform
    {
        public static SvgTransformSkewX Create(double angle)
        {
            return new SvgTransformSkewX { AngleValue = angle };
        }


        public double AngleValue { get; set; }

        public override string ValueText
            => new StringBuilder()
                .Append("skewX(")
                .Append(AngleValue)
                .Append(")")
                .ToString();


        private SvgTransformSkewX()
        {
        }
    }
}
