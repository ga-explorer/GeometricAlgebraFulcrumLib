namespace WebComposerLib.Svg.Values
{
    public static class SvgValueUtils
    {
        public static string Inherit { get; } = "inherit";

        public static string None { get; } = "none";

        public static string CurrentColor { get; } = "currentColor";


        public static bool IsNullOrNone(this SvgValueLengthUnit unit)
        {
            return ReferenceEquals(unit, null) || unit.UnitId == SvgValueLengthUnit.None.UnitId;
        }

        public static SvgValueLength ToSvgLength(this double lengthValue)
        {
            return SvgValueLength.Create(lengthValue);
        }

        public static SvgValueLength ToSvgLength(this double lengthValue, SvgValueLengthUnit unit)
        {
            return SvgValueLength.Create(lengthValue, unit);
        }

        public static SvgValueAngle ToSvgAngle(this double angleValue)
        {
            return SvgValueAngle.Create(angleValue);
        }

        public static SvgValueAngle ToSvgAngle(this double angleValue, SvgValueAngleUnit unit)
        {
            return SvgValueAngle.Create(angleValue, unit);
        }
    }
}
