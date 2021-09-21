namespace GraphicsComposerLib.Svg.Values
{
    public sealed class SvgValueTextDecoration : SvgStoredValue
    {
        public static SvgValueTextDecoration None { get; }
            = new SvgValueTextDecoration("none");

        public static SvgValueTextDecoration Underline { get; }
            = new SvgValueTextDecoration("underline");

        public static SvgValueTextDecoration Overline { get; }
            = new SvgValueTextDecoration("overline");

        public static SvgValueTextDecoration LineThrough { get; }
            = new SvgValueTextDecoration("line-through");

        public static SvgValueTextDecoration Blink { get; }
            = new SvgValueTextDecoration("blink");

        public static SvgValueTextDecoration Inherit { get; }
            = new SvgValueTextDecoration("inherit");


        private SvgValueTextDecoration(string value) : base(value)
        {
        }
    }
}