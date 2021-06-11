namespace CodeComposerLib.HTMLold.Values
{
    public sealed class HtmlValueZoomAndPan : HtmlStoredValue
    {
        public static HtmlValueZoomAndPan Disable { get; }
            = new HtmlValueZoomAndPan("disable");

        public static HtmlValueZoomAndPan Magnify { get; }
            = new HtmlValueZoomAndPan("magnify");


        private HtmlValueZoomAndPan(string value) : base(value)
        {
        }
    }
}
