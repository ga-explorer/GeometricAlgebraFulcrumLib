namespace GraphicsComposerLib.Rendering.Svg.Values
{
    public sealed class SvgValueColorRendering : SvgStoredValue
    {
        public static SvgValueColorRendering Auto { get; } 
            = new SvgValueColorRendering("auto");

        public static SvgValueColorRendering OptimizeSpeed { get; }
            = new SvgValueColorRendering("optimizeSpeed");

        public static SvgValueColorRendering OptimizeQuality { get; }
            = new SvgValueColorRendering("optimizeQuality");


        private SvgValueColorRendering(string value) : base(value)
        {
        }
    }
}