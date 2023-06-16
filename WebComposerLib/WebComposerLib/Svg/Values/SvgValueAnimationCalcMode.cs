namespace WebComposerLib.Svg.Values
{
    public sealed class SvgValueAnimationCalcMode : SvgStoredValue
    {
        public static SvgValueAnimationCalcMode Discrete { get; }
            = new SvgValueAnimationCalcMode("discrete");

        public static SvgValueAnimationCalcMode Linear { get; }
            = new SvgValueAnimationCalcMode("linear");

        public static SvgValueAnimationCalcMode Paced { get; }
            = new SvgValueAnimationCalcMode("paced");

        public static SvgValueAnimationCalcMode Spline { get; }
            = new SvgValueAnimationCalcMode("spline");


        private SvgValueAnimationCalcMode(string value) : base(value)
        {
        }
    }
}