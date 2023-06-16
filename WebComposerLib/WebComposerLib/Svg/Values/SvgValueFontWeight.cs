namespace WebComposerLib.Svg.Values
{
    public sealed class SvgValueFontWeight : SvgStoredValue
    {
        public static SvgValueFontWeight Normal { get; } 
            = new SvgValueFontWeight("normal");

        public static SvgValueFontWeight Bold { get; } 
            = new SvgValueFontWeight("bold");

        public static SvgValueFontWeight Bolder { get; } 
            = new SvgValueFontWeight("bolder");

        public static SvgValueFontWeight Lighter { get; } 
            = new SvgValueFontWeight("lighter");

        public static SvgValueFontWeight FontWeight100 { get; } 
            = new SvgValueFontWeight("100");

        public static SvgValueFontWeight FontWeight200 { get; } 
            = new SvgValueFontWeight("200");

        public static SvgValueFontWeight FontWeight300 { get; } 
            = new SvgValueFontWeight("300");

        public static SvgValueFontWeight FontWeight400 { get; } 
            = new SvgValueFontWeight("400");

        public static SvgValueFontWeight FontWeight500 { get; } 
            = new SvgValueFontWeight("500");

        public static SvgValueFontWeight FontWeight600 { get; } 
            = new SvgValueFontWeight("600");

        public static SvgValueFontWeight FontWeight700 { get; } 
            = new SvgValueFontWeight("700");

        public static SvgValueFontWeight FontWeight800 { get; } 
            = new SvgValueFontWeight("800");

        public static SvgValueFontWeight FontWeight900 { get; } 
            = new SvgValueFontWeight("900");

        public static SvgValueFontWeight Inherit { get; } 
            = new SvgValueFontWeight("inherit");


        private static int _idCounter;


        public int ValueId { get; }


        private SvgValueFontWeight(string value) : base(value)
        {
            ValueId = _idCounter++;
        }


        public bool IsEqualTo(SvgValueFontWeight value)
        {
            return ValueId == value.ValueId;
        }
    }
}