namespace WebComposerLib.Svg.Values
{
    public sealed class SvgValueFontStretch : SvgStoredValue
    {
        public static SvgValueFontStretch Normal { get; } 
            = new SvgValueFontStretch("normal");

        public static SvgValueFontStretch Wider { get; } 
            = new SvgValueFontStretch("wider");

        public static SvgValueFontStretch Narrower { get; } 
            = new SvgValueFontStretch("narrower");

        public static SvgValueFontStretch UltraCondensed { get; } 
            = new SvgValueFontStretch("ultra-condensed");

        public static SvgValueFontStretch ExtraCondensed { get; } 
            = new SvgValueFontStretch("extra-condensed");

        public static SvgValueFontStretch Condensed { get; } 
            = new SvgValueFontStretch("condensed");

        public static SvgValueFontStretch SemiCondensed { get; } 
            = new SvgValueFontStretch("semi-condensed");

        public static SvgValueFontStretch SemiExpanded { get; } 
            = new SvgValueFontStretch("semi-expanded");

        public static SvgValueFontStretch Expanded { get; } 
            = new SvgValueFontStretch("expanded");

        public static SvgValueFontStretch ExtraExpanded { get; } 
            = new SvgValueFontStretch("extra-expanded");

        public static SvgValueFontStretch UltraExpanded { get; } 
            = new SvgValueFontStretch("ultra-expanded");

        public static SvgValueFontStretch Inherit { get; } 
            = new SvgValueFontStretch("inherit");


        private SvgValueFontStretch(string value) : base(value)
        {
        }
    }
}