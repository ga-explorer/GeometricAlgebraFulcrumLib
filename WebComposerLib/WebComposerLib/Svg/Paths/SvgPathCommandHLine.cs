using System.Text;
using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Paths
{
    public sealed class SvgPathCommandHLine : SvgPathCommand
    {
        public static SvgPathCommandHLine Create()
        {
            return new SvgPathCommandHLine();
        }

        public static SvgPathCommandHLine Create(bool isRelative)
        {
            return new SvgPathCommandHLine {IsRelative = isRelative};
        }

        //public static SvgPathCommandHLine Create(bool isRelative, SvgValueLengthUnit unit)
        //{
        //    var command = new SvgPathCommandHLine { IsRelative = isRelative };

        //    command.Numbers.Unit = unit;

        //    return command;
        //}

        public static SvgPathCommandHLine Create(double x)
        {
            var command = new SvgPathCommandHLine();

            command.Numbers.AddNumber(x);

            return command;
        }

        public static SvgPathCommandHLine Create(bool isRelative, double x)
        {
            var command = new SvgPathCommandHLine { IsRelative = isRelative };

            command.Numbers.AddNumber(x);

            return command;
        }

        //public static SvgPathCommandHLine Create(bool isRelative, SvgValueLengthUnit unit, double x)
        //{
        //    var command = new SvgPathCommandHLine { IsRelative = isRelative };

        //    command.Numbers.Unit = unit;

        //    command.Numbers.AddNumber(x);

        //    return command;
        //}

        public static SvgPathCommandHLine Create(params double[] numbersList)
        {
            var command = new SvgPathCommandHLine();

            command.Numbers.AddNumbers(numbersList);

            return command;
        }

        public static SvgPathCommandHLine Create(IEnumerable<double> numbersList)
        {
            var command = new SvgPathCommandHLine();

            command.Numbers.AddNumbers(numbersList);

            return command;
        }

        public static SvgPathCommandHLine Create(bool isRelative, params double[] numbersList)
        {
            var command = new SvgPathCommandHLine { IsRelative = isRelative };

            command.Numbers.AddNumbers(numbersList);

            return command;
        }

        public static SvgPathCommandHLine Create(bool isRelative, IEnumerable<double> numbersList)
        {
            var command = new SvgPathCommandHLine { IsRelative = isRelative };

            command.Numbers.AddNumbers(numbersList);

            return command;
        }

        //public static SvgPathCommandHLine Create(bool isRelative, SvgValueLengthUnit unit, params double[] numbersList)
        //{
        //    var command = new SvgPathCommandHLine { IsRelative = isRelative };

        //    command.Numbers.Unit = unit;

        //    command.Numbers.AddNumbers(numbersList);

        //    return command;
        //}

        //public static SvgPathCommandHLine Create(bool isRelative, SvgValueLengthUnit unit, IEnumerable<double> numbersList)
        //{
        //    var command = new SvgPathCommandHLine { IsRelative = isRelative };

        //    command.Numbers.Unit = unit;

        //    command.Numbers.AddNumbers(numbersList);

        //    return command;
        //}


        public bool IsRelative { get; set; }

        public bool IsAbsolute
        {
            get => !IsRelative;
            set => IsRelative = !value;
        }

        public SvgValueNumbersList Numbers { get; } 
            = SvgValueNumbersList.Create();

        public override string ValueText
        {
            get
            {
                var composer = new StringBuilder();

                composer
                    .Append(IsRelative ? "h " : "H ")
                    .Append(Numbers.ValueText);

                return composer.ToString();
            }
        }
    }
}