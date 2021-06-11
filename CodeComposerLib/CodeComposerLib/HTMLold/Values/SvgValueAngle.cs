using System.Text;

namespace CodeComposerLib.HTMLold.Values
{
    public sealed class HtmlValueAngle : HtmlComputedValue
    {
        public static HtmlValueAngle Inherit { get; }
            = new HtmlValueAngle();

        public static HtmlValueAngle Create(double angleValue)
        {
            return new HtmlValueAngle(angleValue);
        }

        public static HtmlValueAngle Create(double angleValue, HtmlValueAngleUnit unit)
        {
            return new HtmlValueAngle(angleValue, unit);
        }


        public bool IsInherit { get; }

        public double AngleValue { get; set; }


        private HtmlValueAngleUnit _unit = HtmlValueAngleUnit.None;
        public HtmlValueAngleUnit Unit
        {
            get { return _unit; }
            set { _unit = value ?? HtmlValueAngleUnit.None; }
        }

        public override string ValueText
            => IsInherit
                ? "inherit"
                : new StringBuilder(32)
                    .Append(AngleValue.ToString("G"))
                    .Append(_unit.ValueText)
                    .ToString();


        private HtmlValueAngle()
        {
            IsInherit = true;
        }

        private HtmlValueAngle(double angleValue)
        {
            IsInherit = false;
            AngleValue = angleValue;
        }

        private HtmlValueAngle(double angleValue, HtmlValueAngleUnit unit)
        {
            IsInherit = false;
            AngleValue = angleValue;
            _unit = unit ?? HtmlValueAngleUnit.None;
        }


        public bool IsEqualToZero()
        {
            return IsInherit == false &&
                   AngleValue == 0 &&
                   _unit.UnitId == HtmlValueAngleUnit.None.UnitId;
        }

        public bool IsEqualTo(double angleValue)
        {
            return IsInherit == false &&
                   AngleValue == angleValue &&
                   _unit.UnitId == HtmlValueAngleUnit.None.UnitId;
        }

        public bool IsEqualTo(double angleValue, HtmlValueAngleUnit unit)
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