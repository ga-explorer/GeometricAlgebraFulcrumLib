using System.Text;

namespace WebComposerLib.Svg.Values
{
    public sealed class SvgLength : 
        SvgComputedValue
    {
        public static SvgLength Inherit { get; } 
            = new SvgLength();

        public static SvgLength Create(double lengthValue)
        {
            return new SvgLength(lengthValue);
        }

        public static SvgLength Create(double lengthValue, SvgLengthUnit unit)
        {
            return new SvgLength(lengthValue, unit);
        }
        
        public static SvgLength Create(SvgLengthUnit unit, double lengthValue)
        {
            return new SvgLength(lengthValue, unit);
        }


        public bool IsInherit { get; }

        public double LengthValue { get; }


        public SvgLengthUnit Unit { get; set; } 
            = SvgLengthUnit.None;

        public override string ValueText
            => IsInherit
                ? "inherit"
                : new StringBuilder(32)
                    .Append(LengthValue.ToString("G"))
                    .Append(Unit.ValueText)
                    .ToString();


        private SvgLength()
        {
            IsInherit = true;
        }

        private SvgLength(double lengthValue)
        {
            IsInherit = false;
            LengthValue = lengthValue;
        }

        private SvgLength(double lengthValue, SvgLengthUnit unit)
        {
            IsInherit = false;
            LengthValue = lengthValue;
            Unit = unit;
        }


        public bool IsEqualToZero()
        {
            return IsInherit == false &&
                   LengthValue == 0 &&
                   Unit.UnitId == SvgLengthUnit.None.UnitId;
        }

        public bool IsEqualTo(double lengthValue)
        {
            return IsInherit == false &&
                   LengthValue == lengthValue &&
                   Unit.UnitId == SvgLengthUnit.None.UnitId;
        }

        public bool IsEqualTo(double lengthValue, SvgLengthUnit unit)
        {
            return IsInherit == false &&
                   LengthValue == lengthValue &&
                   Unit.UnitId == unit.UnitId;
        }

        public bool IsEqualToInherit()
        {
            return IsInherit;
        }
    }
}