namespace WebComposerLib.Svg.Values
{
    public static class SvgValueUtils
    {
        public static string Inherit { get; } = "inherit";

        public static string None { get; } = "none";

        public static string CurrentColor { get; } = "currentColor";


        public static bool IsNullOrNone(this SvgLengthUnit unit)
        {
            return ReferenceEquals(unit, null) || unit.UnitId == SvgLengthUnit.None.UnitId;
        }

        public static SvgLength ToSvgLength(this double lengthValue)
        {
            return SvgLength.Create(lengthValue);
        }

        public static SvgLength ToSvgLength(this double lengthValue, SvgLengthUnit unit)
        {
            return SvgLength.Create(lengthValue, unit);
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
