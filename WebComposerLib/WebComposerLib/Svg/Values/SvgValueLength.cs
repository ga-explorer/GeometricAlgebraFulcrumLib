using System.Text;

namespace WebComposerLib.Svg.Values
{
    public sealed class SvgValueLength : SvgComputedValue
    {
        public static SvgValueLength Inherit { get; } 
            = new SvgValueLength();

        public static SvgValueLength Create(double lengthValue)
        {
            return new SvgValueLength(lengthValue);
        }

        public static SvgValueLength Create(double lengthValue, SvgValueLengthUnit unit)
        {
            return new SvgValueLength(lengthValue, unit);
        }


        public bool IsInherit { get; }

        public double LengthValue { get; set; }


        private SvgValueLengthUnit _unit = SvgValueLengthUnit.None;
        public SvgValueLengthUnit Unit
        {
            get => _unit;
            set => _unit = value ?? SvgValueLengthUnit.None;
        }

        public override string ValueText
            => IsInherit
                ? "inherit"
                : new StringBuilder(32)
                    .Append(LengthValue.ToString("G"))
                    .Append(_unit.ValueText)
                    .ToString();


        private SvgValueLength()
        {
            IsInherit = true;
        }

        private SvgValueLength(double lengthValue)
        {
            IsInherit = false;
            LengthValue = lengthValue;
        }

        private SvgValueLength(double lengthValue, SvgValueLengthUnit unit)
        {
            IsInherit = false;
            LengthValue = lengthValue;
            _unit = unit ?? SvgValueLengthUnit.None;
        }


        public bool IsEqualToZero()
        {
            return IsInherit == false &&
                   LengthValue == 0 &&
                   _unit.UnitId == SvgValueLengthUnit.None.UnitId;
        }

        public bool IsEqualTo(double lengthValue)
        {
            return IsInherit == false &&
                   LengthValue == lengthValue &&
                   _unit.UnitId == SvgValueLengthUnit.None.UnitId;
        }

        public bool IsEqualTo(double lengthValue, SvgValueLengthUnit unit)
        {
            return IsInherit == false &&
                   LengthValue == lengthValue &&
                   _unit.UnitId == unit.UnitId;
        }

        public bool IsEqualToInherit()
        {
            return IsInherit;
        }
    }
}