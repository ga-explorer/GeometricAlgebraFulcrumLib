using System.Text;

namespace GraphicsComposerLib.Rendering.Svg.Values
{
    public sealed class SvgValueAngle : SvgComputedValue
    {
        public static SvgValueAngle Inherit { get; }
            = new SvgValueAngle();

        public static SvgValueAngle Create(double angleValue)
        {
            return new SvgValueAngle(angleValue);
        }

        public static SvgValueAngle Create(double angleValue, SvgValueAngleUnit unit)
        {
            return new SvgValueAngle(angleValue, unit);
        }


        public bool IsInherit { get; }

        public double AngleValue { get; set; }


        private SvgValueAngleUnit _unit = SvgValueAngleUnit.None;
        public SvgValueAngleUnit Unit
        {
            get => _unit;
            set => _unit = value ?? SvgValueAngleUnit.None;
        }

        public override string ValueText
            => IsInherit
                ? "inherit"
                : new StringBuilder(32)
                    .Append(AngleValue.ToString("G"))
                    .Append(_unit.ValueText)
                    .ToString();


        private SvgValueAngle()
        {
            IsInherit = true;
        }

        private SvgValueAngle(double angleValue)
        {
            IsInherit = false;
            AngleValue = angleValue;
        }

        private SvgValueAngle(double angleValue, SvgValueAngleUnit unit)
        {
            IsInherit = false;
            AngleValue = angleValue;
            _unit = unit ?? SvgValueAngleUnit.None;
        }


        public bool IsEqualToZero()
        {
            return IsInherit == false &&
                   AngleValue == 0 &&
                   _unit.UnitId == SvgValueAngleUnit.None.UnitId;
        }

        public bool IsEqualTo(double angleValue)
        {
            return IsInherit == false &&
                   AngleValue == angleValue &&
                   _unit.UnitId == SvgValueAngleUnit.None.UnitId;
        }

        public bool IsEqualTo(double angleValue, SvgValueAngleUnit unit)
        {
            return IsInherit == false &&
                   AngleValue == angleValue &&
                   _unit.UnitId == unit.UnitId;
        }

        public bool IsEqualToInherit()
        {
            return IsInherit;
        }
    }
}