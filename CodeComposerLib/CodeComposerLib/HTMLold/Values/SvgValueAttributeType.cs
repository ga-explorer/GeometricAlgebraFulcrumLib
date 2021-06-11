namespace CodeComposerLib.HTMLold.Values
{
    public sealed class HtmlValueAttributeType : HtmlStoredValue
    {
        public static HtmlValueAttributeType Auto { get; }
            = new HtmlValueAttributeType("auto");

        public static HtmlValueAttributeType Css { get; }
            = new HtmlValueAttributeType("css");

        public static HtmlValueAttributeType Xml { get; }
            = new HtmlValueAttributeType("xml");


        private HtmlValueAttributeType(string value) : base(value)
        {
        }
    }
}