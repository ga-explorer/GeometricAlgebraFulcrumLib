namespace GraphicsComposerLib.Svg.Values
{
    public sealed class SvgValueMaskUnits : SvgStoredValue
    {
        public static SvgValueMaskUnits UserSpaceOnUse { get; }
            = new SvgValueMaskUnits("userSpaceOnUse");

        public static SvgValueMaskUnits ObjectBoundingBox { get; }
            = new SvgValueMaskUnits("objectBoundingBox");


        private SvgValueMaskUnits(string value) : base(value)
        {
        }
    }
}