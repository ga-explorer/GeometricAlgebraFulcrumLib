using System.Collections.Generic;
using System.Text;

namespace GraphicsComposerLib.Svg.Values
{
    public class SvgValueNumbersList : SvgComputedValue
    {
        public static SvgValueNumbersList Create()
        {
            return new SvgValueNumbersList();
        }

        //public static SvgValueNumbersList Create(SvgValueLengthUnit unit)
        //{
        //    return new SvgValueNumbersList {Unit = unit};
        //}

        public static SvgValueNumbersList Create(double number)
        {
            var numbersList = new SvgValueNumbersList();

            numbersList.AddNumber(number);

            return numbersList;
        }

        //public static SvgValueNumbersList Create(SvgValueLengthUnit unit, double number)
        //{
        //    var numbersList = new SvgValueNumbersList { Unit = unit };

        //    numbersList.AddNumber(number);

        //    return numbersList;
        //}

        public static SvgValueNumbersList Create(IEnumerable<double> numbers)
        {
            var numbersList = new SvgValueNumbersList();

            numbersList.AddNumbers(numbers);

            return numbersList;
        }

        //public static SvgValueNumbersList Create(SvgValueLengthUnit unit, IEnumerable<double> numbers)
        //{
        //    var numbersList = new SvgValueNumbersList { Unit = unit };

        //    numbersList.AddNumbers(numbers);

        //    return numbersList;
        //}

        public static SvgValueNumbersList Create(params double[] numbers)
        {
            var numbersList = new SvgValueNumbersList();

            numbersList.AddNumbers(numbers);

            return numbersList;
        }

        //public static SvgValueNumbersList Create(SvgValueLengthUnit unit, params double[] numbers)
        //{
        //    var numbersList = new SvgValueNumbersList { Unit = unit };

        //    numbersList.AddNumbers(numbers);

        //    return numbersList;
        //}


        private readonly List<double> _numbersList
            = new List<double>();


        public IEnumerable<double> Numbers
            => _numbersList;

        //private SvgValueLengthUnit _unit = SvgValueLengthUnit.None;
        //public SvgValueLengthUnit Unit
        //{
        //    get { return _unit; }
        //    set { _unit = value ?? SvgValueLengthUnit.None; }
        //}

        public double this[int index]
            => _numbersList[index];

        public override string ValueText
        {
            get
            {
                if (_numbersList.Count == 0)
                    return string.Empty;

                var composer = new StringBuilder();

                foreach (var number in _numbersList)
                    composer
                        .Append(number.ToSvgNumberText())
                        //.Append(number.ToSvgLengthText(_unit))
                        .Append(",");

                composer.Length -= ",".Length;

                return composer.ToString();
            }
        }


        private SvgValueNumbersList()
        {
        }


        public SvgValueNumbersList ClearNumbers()
        {
            _numbersList.Clear();

            return this;
        }

        public SvgValueNumbersList AddNumber(double x)
        {
            _numbersList.Add(x);

            return this;
        }

        public SvgValueNumbersList AddNumbers(IEnumerable<double> numbersList)
        {
            _numbersList.AddRange(numbersList);

            return this;
        }

        public SvgValueNumbersList AddNumbers(params double[] numbersList)
        {
            _numbersList.AddRange(numbersList);

            return this;
        }

        public SvgValueNumbersList InsertNumber(int index, double x)
        {
            _numbersList.Insert(index, x);

            return this;
        }

        public SvgValueNumbersList InsertNumbers(int index, IEnumerable<double> numbersList)
        {
            _numbersList.InsertRange(index, numbersList);

            return this;
        }

        public SvgValueNumbersList RemoveNumber(int index)
        {
            _numbersList.RemoveAt(index);

            return this;
        }
    }
}