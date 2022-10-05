namespace GraphicsComposerLib.Rendering.Svg.Values
{
    public sealed class SvgValueTextAnchor : SvgStoredValue
    {
        public static SvgValueTextAnchor Start { get; }
            = new SvgValueTextAnchor("start");

        public static SvgValueTextAnchor Middle { get; }
            = new SvgValueTextAnchor("middle");

        public static SvgValueTextAnchor End { get; }
            = new SvgValueTextAnchor("end");


        private SvgValueTextAnchor(string value) : base(value)
        {
        }
    }
}