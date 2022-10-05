namespace GraphicsComposerLib.Rendering.Svg.Values
{
    public sealed class SvgValueImageRendering : SvgStoredValue
    {
        public static SvgValueImageRendering Auto { get; }
            = new SvgValueImageRendering("auto");

        public static SvgValueImageRendering OptimizeSpeed { get; }
            = new SvgValueImageRendering("optimizeSpeed");

        public static SvgValueImageRendering OptimizeQuality { get; }
            = new SvgValueImageRendering("optimizeQuality");


        private SvgValueImageRendering(string value) : base(value)
        {
        }
    }
}