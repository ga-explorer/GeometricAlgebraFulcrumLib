namespace GraphicsComposerLib.Svg.Values
{
    public sealed class SvgValueGradientUnits : SvgStoredValue
    {
        public static SvgValueGradientUnits UserSpaceOnUse { get; }
            = new SvgValueGradientUnits("userSpaceOnUse");

        public static SvgValueGradientUnits ObjectBoundingBox { get; }
            = new SvgValueGradientUnits("objectBoundingBox");


        private SvgValueGradientUnits(string value) : base(value)
        {
        }
    }
}