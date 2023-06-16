namespace WebComposerLib.Svg.Values
{
    public sealed class SvgValueAnimationAdditive : SvgStoredValue
    {
        public static SvgValueAnimationAdditive Replace { get; }
            = new SvgValueAnimationAdditive("replace");

        public static SvgValueAnimationAdditive Sum { get; }
            = new SvgValueAnimationAdditive("sum");


        private SvgValueAnimationAdditive(string value) : base(value)
        {
        }
    }
}