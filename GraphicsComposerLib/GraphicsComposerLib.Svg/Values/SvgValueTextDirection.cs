namespace GraphicsComposerLib.Svg.Values
{
    public sealed class SvgValueTextDirection : SvgStoredValue
    {
        public static SvgValueTextDirection LeftToRight { get; }
            = new SvgValueTextDirection("ltr");

        public static SvgValueTextDirection RightToLeft { get; }
            = new SvgValueTextDirection("rtl");

        public static SvgValueTextDirection Inherit { get; }
            = new SvgValueTextDirection("inherit");


        private SvgValueTextDirection(string value) : base(value)
        {
        }
    }
}