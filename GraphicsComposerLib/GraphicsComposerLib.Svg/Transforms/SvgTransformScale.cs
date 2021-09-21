using System.Text;

namespace GraphicsComposerLib.Svg.Transforms
{
    public sealed class SvgTransformScale : SvgTransform
    {
        public static SvgTransformScale Create(double s)
        {
            return new SvgTransformScale { SxValue = s, SyValue = s };
        }

        public static SvgTransformScale Create(double sx, double sy)
        {
            return new SvgTransformScale { SxValue = sx, SyValue = sy };
        }


        public double SxValue { get; set; }

        public double SyValue { get; set; }

        public override string ValueText
            => new StringBuilder()
                .Append("scale(")
                .Append(SxValue)
                .Append(", ")
                .Append(SyValue)
                .Append(")")
                .ToString();


        private SvgTransformScale()
        {
        }
    }
}
