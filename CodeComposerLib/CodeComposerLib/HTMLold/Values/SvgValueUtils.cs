namespace CodeComposerLib.HTMLold.Values
{
    public static class HtmlValueUtils
    {
        public static string Inherit { get; } = "inherit";

        public static string None { get; } = "none";

        public static string CurrentColor { get; } = "currentColor";


        public static bool IsNullOrNone(this HtmlValueLengthUnit unit)
        {
            return ReferenceEquals(unit, null) || unit.UnitId == HtmlValueLengthUnit.None.UnitId;
        }

        public static HtmlValueLength ToHtmlLength(this double lengthValue)
        {
            return HtmlValueLength.Create(lengthValue);
        }

        public static HtmlValueLength ToHtmlLength(this double lengthValue, HtmlValueLengthUnit unit)
        {
            return HtmlValueLength.Create(lengthValue, unit);
        }

        public static HtmlValueAngle ToHtmlAngle(this double angleValue)
        {
            return HtmlValueAngle.Create(angleValue);
        }

        public static HtmlValueAngle ToHtmlAngle(this double angleValue, HtmlValueAngleUnit unit)
        {
            return HtmlValueAngle.Create(angleValue, unit);
        }
    }
}
