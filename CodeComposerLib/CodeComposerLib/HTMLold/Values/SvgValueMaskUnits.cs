namespace CodeComposerLib.HTMLold.Values
{
    public sealed class HtmlValueMaskUnits : HtmlStoredValue
    {
        public static HtmlValueMaskUnits UserSpaceOnUse { get; }
            = new HtmlValueMaskUnits("userSpaceOnUse");

        public static HtmlValueMaskUnits ObjectBoundingBox { get; }
            = new HtmlValueMaskUnits("objectBoundingBox");


        private HtmlValueMaskUnits(string value) : base(value)
        {
        }
    }
}