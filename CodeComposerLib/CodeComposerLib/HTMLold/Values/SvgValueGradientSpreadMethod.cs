namespace CodeComposerLib.HTMLold.Values
{
    public sealed class HtmlValueGradientSpreadMethod : HtmlStoredValue
    {
        public static HtmlValueGradientSpreadMethod Pad { get; }
            = new HtmlValueGradientSpreadMethod("pad");

        public static HtmlValueGradientSpreadMethod Reflect { get; }
            = new HtmlValueGradientSpreadMethod("reflect");

        public static HtmlValueGradientSpreadMethod Repeat { get; }
            = new HtmlValueGradientSpreadMethod("repeat");


        private HtmlValueGradientSpreadMethod(string value) : base(value)
        {
        }
    }
}