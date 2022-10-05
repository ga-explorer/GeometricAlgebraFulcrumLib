namespace GraphicsComposerLib.Rendering.Svg.Values
{
    public sealed class SvgValueFontVariant : SvgStoredValue
    {
        public static SvgValueFontVariant Normal { get; } 
            = new SvgValueFontVariant("normal");

        public static SvgValueFontVariant SmallCaps { get; } 
            = new SvgValueFontVariant("small-caps");

        public static SvgValueFontVariant Inherit { get; } 
            = new SvgValueFontVariant("inherit");


        private SvgValueFontVariant(string value) : base(value)
        {
        }
    }
}