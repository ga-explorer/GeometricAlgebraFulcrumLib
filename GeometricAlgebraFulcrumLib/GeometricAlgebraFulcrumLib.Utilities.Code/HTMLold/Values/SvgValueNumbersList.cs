using System.Collections.Generic;
using System.Text;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Values;

public class HtmlValueNumbersList : HtmlComputedValue
{
    public static HtmlValueNumbersList Create()
    {
        return new HtmlValueNumbersList();
    }

    //public static HtmlValueNumbersList Create(HtmlValueLengthUnit unit)
    //{
    //    return new HtmlValueNumbersList {Unit = unit};
    //}

    public static HtmlValueNumbersList Create(double number)
    {
        var numbersList = new HtmlValueNumbersList();

        numbersList.AddNumber(number);

        return numbersList;
    }

    //public static HtmlValueNumbersList Create(HtmlValueLengthUnit unit, double number)
    //{
    //    var numbersList = new HtmlValueNumbersList { Unit = unit };

    //    numbersList.AddNumber(number);

    //    return numbersList;
    //}

    public static HtmlValueNumbersList Create(IEnumerable<double> numbers)
    {
        var numbersList = new HtmlValueNumbersList();

        numbersList.AddNumbers(numbers);

        return numbersList;
    }

    //public static HtmlValueNumbersList Create(HtmlValueLengthUnit unit, IEnumerable<double> numbers)
    //{
    //    var numbersList = new HtmlValueNumbersList { Unit = unit };

    //    numbersList.AddNumbers(numbers);

    //    return numbersList;
    //}

    public static HtmlValueNumbersList Create(params double[] numbers)
    {
        var numbersList = new HtmlValueNumbersList();

        numbersList.AddNumbers(numbers);

        return numbersList;
    }

    //public static HtmlValueNumbersList Create(HtmlValueLengthUnit unit, params double[] numbers)
    //{
    //    var numbersList = new HtmlValueNumbersList { Unit = unit };

    //    numbersList.AddNumbers(numbers);

    //    return numbersList;
    //}


    private readonly List<double> _numbersList
        = new List<double>();


    public IEnumerable<double> Numbers
        => _numbersList;

    //private HtmlValueLengthUnit _unit = HtmlValueLengthUnit.None;
    //public HtmlValueLengthUnit Unit
    //{
    //    get { return _unit; }
    //    set { _unit = value ?? HtmlValueLengthUnit.None; }
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
                    .Append(number.ToHtmlNumberText())
                    //.Append(number.ToHtmlLengthText(_unit))
                    .Append(",");

            composer.Length -= ",".Length;

            return composer.ToString();
        }
    }


    private HtmlValueNumbersList()
    {
    }


    public HtmlValueNumbersList ClearNumbers()
    {
        _numbersList.Clear();

        return this;
    }

    public HtmlValueNumbersList AddNumber(double x)
    {
        _numbersList.Add(x);

        return this;
    }

    public HtmlValueNumbersList AddNumbers(IEnumerable<double> numbersList)
    {
        _numbersList.AddRange(numbersList);

        return this;
    }

    public HtmlValueNumbersList AddNumbers(params double[] numbersList)
    {
        _numbersList.AddRange(numbersList);

        return this;
    }

    public HtmlValueNumbersList InsertNumber(int index, double x)
    {
        _numbersList.Insert(index, x);

        return this;
    }

    public HtmlValueNumbersList InsertNumbers(int index, IEnumerable<double> numbersList)
    {
        _numbersList.InsertRange(index, numbersList);

        return this;
    }

    public HtmlValueNumbersList RemoveNumber(int index)
    {
        _numbersList.RemoveAt(index);

        return this;
    }
}