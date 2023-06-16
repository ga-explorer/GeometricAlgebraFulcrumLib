namespace WebComposerLib.Svg.Values
{
    public sealed class SvgValueUnicodeBidi : SvgStoredValue
    {
        public static SvgValueUnicodeBidi Normal { get; }
            = new SvgValueUnicodeBidi("normal");

        public static SvgValueUnicodeBidi Embed { get; }
            = new SvgValueUnicodeBidi("embed");

        public static SvgValueUnicodeBidi BidiOverride { get; }
            = new SvgValueUnicodeBidi("bidi-override");

        public static SvgValueUnicodeBidi Inherit { get; }
            = new SvgValueUnicodeBidi("inherit");


        private SvgValueUnicodeBidi(string value) : base(value)
        {
        }
    }
}