namespace WebComposerLib.Svg.Values
{
    public sealed class SvgValueGradientSpreadMethod : SvgStoredValue
    {
        public static SvgValueGradientSpreadMethod Pad { get; }
            = new SvgValueGradientSpreadMethod("pad");

        public static SvgValueGradientSpreadMethod Reflect { get; }
            = new SvgValueGradientSpreadMethod("reflect");

        public static SvgValueGradientSpreadMethod Repeat { get; }
            = new SvgValueGradientSpreadMethod("repeat");


        private SvgValueGradientSpreadMethod(string value) : base(value)
        {
        }
    }
}