using System.Text;

namespace GraphicsComposerLib.Rendering.Svg.Transforms
{
    public sealed class SvgTransformRotate : SvgTransform
    {
        public static SvgTransformRotate Create(double angle, double cx = 0.0d, double cy = 0.0d)
        {
            return new SvgTransformRotate
            {
                AngleValue = angle,
                CxValue = cx,
                CyValue = cy
            };
        }


        public double AngleValue { get; set; }

        public double CxValue { get; set; }

        public double CyValue { get; set; }

        public override string ValueText
            => new StringBuilder()
                .Append("rotate(")
                .Append(AngleValue)
                .Append(", ")
                .Append(CxValue)
                .Append(", ")
                .Append(CyValue)
                .Append(")")
                .ToString();


        private SvgTransformRotate()
        {
        }
    }
}
