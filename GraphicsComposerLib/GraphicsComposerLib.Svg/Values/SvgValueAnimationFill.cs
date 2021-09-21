namespace GraphicsComposerLib.Svg.Values
{
    public sealed class SvgValueAnimationFill : SvgStoredValue
    {
        public static SvgValueAnimationFill Remove { get; }
            = new SvgValueAnimationFill("remove");

        public static SvgValueAnimationFill Freeze { get; }
            = new SvgValueAnimationFill("freeze");


        private SvgValueAnimationFill(string value) : base(value)
        {
        }
    }
}