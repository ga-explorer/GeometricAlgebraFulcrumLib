namespace CodeComposerLib.HTMLold.Values
{
    public sealed class HtmlValueColorRendering : HtmlStoredValue
    {
        public static HtmlValueColorRendering Auto { get; } 
            = new HtmlValueColorRendering("auto");

        public static HtmlValueColorRendering OptimizeSpeed { get; }
            = new HtmlValueColorRendering("optimizeSpeed");

        public static HtmlValueColorRendering OptimizeQuality { get; }
            = new HtmlValueColorRendering("optimizeQuality");


        private HtmlValueColorRendering(string value) : base(value)
        {
        }
    }
}