namespace CodeComposerLib.HTMLold.Values
{
    public sealed class HtmlValueTextDirection : HtmlStoredValue
    {
        public static HtmlValueTextDirection LeftToRight { get; }
            = new HtmlValueTextDirection("ltr");

        public static HtmlValueTextDirection RightToLeft { get; }
            = new HtmlValueTextDirection("rtl");

        public static HtmlValueTextDirection Inherit { get; }
            = new HtmlValueTextDirection("inherit");


        private HtmlValueTextDirection(string value) : base(value)
        {
        }
    }
}