namespace CodeComposerLib.HTMLold.Values
{
    public sealed class HtmlValueAnimationCalcMode : HtmlStoredValue
    {
        public static HtmlValueAnimationCalcMode Discrete { get; }
            = new HtmlValueAnimationCalcMode("discrete");

        public static HtmlValueAnimationCalcMode Linear { get; }
            = new HtmlValueAnimationCalcMode("linear");

        public static HtmlValueAnimationCalcMode Paced { get; }
            = new HtmlValueAnimationCalcMode("paced");

        public static HtmlValueAnimationCalcMode Spline { get; }
            = new HtmlValueAnimationCalcMode("spline");


        private HtmlValueAnimationCalcMode(string value) : base(value)
        {
        }
    }
}