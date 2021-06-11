namespace CodeComposerLib.HTMLold.Values
{
    public sealed class HtmlValueTextAnchor : HtmlStoredValue
    {
        public static HtmlValueTextAnchor Start { get; }
            = new HtmlValueTextAnchor("start");

        public static HtmlValueTextAnchor Middle { get; }
            = new HtmlValueTextAnchor("middle");

        public static HtmlValueTextAnchor End { get; }
            = new HtmlValueTextAnchor("end");


        private HtmlValueTextAnchor(string value) : base(value)
        {
        }
    }
}