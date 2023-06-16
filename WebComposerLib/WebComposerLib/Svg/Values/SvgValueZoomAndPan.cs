namespace WebComposerLib.Svg.Values
{
    public sealed class SvgValueZoomAndPan : SvgStoredValue
    {
        public static SvgValueZoomAndPan Disable { get; }
            = new SvgValueZoomAndPan("disable");

        public static SvgValueZoomAndPan Magnify { get; }
            = new SvgValueZoomAndPan("magnify");


        private SvgValueZoomAndPan(string value) : base(value)
        {
        }
    }
}
