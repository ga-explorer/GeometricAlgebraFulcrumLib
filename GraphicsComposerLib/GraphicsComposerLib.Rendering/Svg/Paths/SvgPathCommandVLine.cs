using System.Text;
using GraphicsComposerLib.Rendering.Svg.Values;

namespace GraphicsComposerLib.Rendering.Svg.Paths
{
    public sealed class SvgPathCommandVLine : SvgPathCommand
    {
        public static SvgPathCommandVLine Create()
        {
            return new SvgPathCommandVLine();
        }

        public static SvgPathCommandVLine Create(bool isRelative)
        {
            return new SvgPathCommandVLine { IsRelative = isRelative };
        }

        //public static SvgPathCommandVLine Create(bool isRelative, SvgValueLengthUnit unit)
        //{
        //    var command = new SvgPathCommandVLine { IsRelative = isRelative };

        //    command.NumbersValue.Unit = unit;

        //    return command;
        //}

        public static SvgPathCommandVLine Create(double x)
        {
            var command = new SvgPathCommandVLine();

            command.NumbersValue.AddNumber(x);

            return command;
        }

        public static SvgPathCommandVLine Create(bool isRelative, double x)
        {
            var command = new SvgPathCommandVLine { IsRelative = isRelative };

            command.NumbersValue.AddNumber(x);

            return command;
        }

        //public static SvgPathCommandVLine Create(bool isRelative, SvgValueLengthUnit unit, double x)
        //{
        //    var command = new SvgPathCommandVLine { IsRelative = isRelative };

        //    command.NumbersValue.Unit = unit;

        //    command.NumbersValue.AddNumber(x);

        //    return command;
        //}

        public static SvgPathCommandVLine Create(params double[] numbersList)
        {
            var command = new SvgPathCommandVLine();

            command.NumbersValue.AddNumbers(numbersList);

            return command;
        }

        public static SvgPathCommandVLine Create(IEnumerable<double> numbersList)
        {
            var command = new SvgPathCommandVLine();

            command.NumbersValue.AddNumbers(numbersList);

            return command;
        }

        public static SvgPathCommandVLine Create(bool isRelative, params double[] numbersList)
        {
            var command = new SvgPathCommandVLine { IsRelative = isRelative };

            command.NumbersValue.AddNumbers(numbersList);

            return command;
        }

        public static SvgPathCommandVLine Create(bool isRelative, IEnumerable<double> numbersList)
        {
            var command = new SvgPathCommandVLine { IsRelative = isRelative };

            command.NumbersValue.AddNumbers(numbersList);

            return command;
        }

        //public static SvgPathCommandVLine Create(bool isRelative, SvgValueLengthUnit unit, params double[] numbersList)
        //{
        //    var command = new SvgPathCommandVLine { IsRelative = isRelative };

        //    command.NumbersValue.Unit = unit;

        //    command.NumbersValue.AddNumbers(numbersList);

        //    return command;
        //}

        //public static SvgPathCommandVLine Create(bool isRelative, SvgValueLengthUnit unit, IEnumerable<double> numbersList)
        //{
        //    var command = new SvgPathCommandVLine { IsRelative = isRelative };

        //    command.NumbersValue.Unit = unit;

        //    command.NumbersValue.AddNumbers(numbersList);

        //    return command;
        //}


        public bool IsRelative { get; set; }

        public bool IsAbsolute
        {
            get => !IsRelative;
            set => IsRelative = !value;
        }

        public SvgValueNumbersList NumbersValue { get; }
            = SvgValueNumbersList.Create();

        public override string ValueText
        {
            get
            {
                var composer = new StringBuilder();

                composer
                    .Append(IsRelative ? "v " : "V ")
                    .Append(NumbersValue.ValueText);

                return composer.ToString();
            }
        }
    }
}