namespace CodeComposerLib.HTMLold.Values
{
    public sealed class HtmlValueFontWeight : HtmlStoredValue
    {
        public static HtmlValueFontWeight Normal { get; } 
            = new HtmlValueFontWeight("normal");

        public static HtmlValueFontWeight Bold { get; } 
            = new HtmlValueFontWeight("bold");

        public static HtmlValueFontWeight Bolder { get; } 
            = new HtmlValueFontWeight("bolder");

        public static HtmlValueFontWeight Lighter { get; } 
            = new HtmlValueFontWeight("lighter");

        public static HtmlValueFontWeight FontWeight100 { get; } 
            = new HtmlValueFontWeight("100");

        public static HtmlValueFontWeight FontWeight200 { get; } 
            = new HtmlValueFontWeight("200");

        public static HtmlValueFontWeight FontWeight300 { get; } 
            = new HtmlValueFontWeight("300");

        public static HtmlValueFontWeight FontWeight400 { get; } 
            = new HtmlValueFontWeight("400");

        public static HtmlValueFontWeight FontWeight500 { get; } 
            = new HtmlValueFontWeight("500");

        public static HtmlValueFontWeight FontWeight600 { get; } 
            = new HtmlValueFontWeight("600");

        public static HtmlValueFontWeight FontWeight700 { get; } 
            = new HtmlValueFontWeight("700");

        public static HtmlValueFontWeight FontWeight800 { get; } 
            = new HtmlValueFontWeight("800");

        public static HtmlValueFontWeight FontWeight900 { get; } 
            = new HtmlValueFontWeight("900");

        public static HtmlValueFontWeight Inherit { get; } 
            = new HtmlValueFontWeight("inherit");


        private static int _idCounter;


        public int ValueId { get; }


        private HtmlValueFontWeight(string value) : base(value)
        {
            ValueId = _idCounter++;
        }


        public bool IsEqualTo(HtmlValueFontWeight value)
        {
            return ValueId == value.ValueId;
        }
    }
}