using System.Text;
using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Paths
{
    public sealed class SvgPathCommandVerticalLineTo : 
        SvgPathCommandSimple
    {
        public static SvgPathCommandVerticalLineTo CreateAbsolute(double value)
        {
            return new SvgPathCommandVerticalLineTo(false, SvgLengthUnit.None, value);
        }

        public static SvgPathCommandVerticalLineTo CreateRelative(double value)
        {
            return new SvgPathCommandVerticalLineTo(true, SvgLengthUnit.None, value);
        }

        public static SvgPathCommandVerticalLineTo Create(bool isRelative, double value)
        {
            return new SvgPathCommandVerticalLineTo(isRelative, SvgLengthUnit.None, value);
        }


        public static SvgPathCommandVerticalLineTo CreateAbsolute(SvgLengthUnit unit, double value)
        {
            return new SvgPathCommandVerticalLineTo(false, unit, value);
        }

        public static SvgPathCommandVerticalLineTo CreateRelative(SvgLengthUnit unit, double value)
        {
            return new SvgPathCommandVerticalLineTo(true, unit, value);
        }

        public static SvgPathCommandVerticalLineTo Create(bool isRelative, SvgLengthUnit unit, double value)
        {
            return new SvgPathCommandVerticalLineTo(isRelative, unit, value);
        }

        
        public override char CommandSymbol 
            => IsRelative ? 'v' : 'V';

        public SvgLength Value { get; }

        public override string ValueText
        {
            get
            {
                var composer = new StringBuilder();

                composer
                    .Append(CommandSymbol)
                    .Append(' ')
                    .Append(Value.ValueText);

                return composer.ToString();
            }
        }


        private SvgPathCommandVerticalLineTo(bool isRelative, SvgLengthUnit unit, double value) 
            : base(isRelative)
        {
            Value = SvgLength.Create(unit, value);
        }

    }
}