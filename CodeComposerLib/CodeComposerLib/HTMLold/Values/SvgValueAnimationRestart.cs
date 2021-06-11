namespace CodeComposerLib.HTMLold.Values
{
    public sealed class HtmlValueAnimationRestart : HtmlStoredValue
    {
        public static HtmlValueAnimationRestart Always { get;}
            = new HtmlValueAnimationRestart("always");

        public static HtmlValueAnimationRestart WhenNotActive { get; }
            = new HtmlValueAnimationRestart("whenNotActive");

        public static HtmlValueAnimationRestart Never { get; }
            = new HtmlValueAnimationRestart("never");


        private HtmlValueAnimationRestart(string value) : base(value)
        {
        }
    }
}