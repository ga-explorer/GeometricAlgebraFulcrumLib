namespace GraphicsComposerLib.Rendering.Svg.Values
{
    public sealed class SvgValueFillRule : SvgStoredValue
    {
        public static SvgValueFillRule NonZero { get; }
            = new SvgValueFillRule("nonzero");

        public static SvgValueFillRule EvenOdd { get; }
            = new SvgValueFillRule("evenodd");

        public static SvgValueFillRule Inherit { get; }
            = new SvgValueFillRule("inherit");


        private SvgValueFillRule(string value) : base(value)
        {
        }
    }
}