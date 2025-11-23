using System.Text;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Values;

public class HtmlValueLengthsList : HtmlComputedValue
{
    public static HtmlValueLengthsList Create()
    {
        return new HtmlValueLengthsList();
    }

    public static HtmlValueLengthsList Create(HtmlValueLengthUnit unit)
    {
        return new HtmlValueLengthsList { Unit = unit };
    }

    public static HtmlValueLengthsList Create(double length)
    {
        var lengthsList = new HtmlValueLengthsList();

        lengthsList.AddLength(length);

        return lengthsList;
    }

    public static HtmlValueLengthsList Create(HtmlValueLengthUnit unit, double length)
    {
        var lengthsList = new HtmlValueLengthsList { Unit = unit };

        lengthsList.AddLength(length);

        return lengthsList;
    }

    public static HtmlValueLengthsList Create(IEnumerable<double> lengths)
    {
        var lengthsList = new HtmlValueLengthsList();

        lengthsList.AddLengths(lengths);

        return lengthsList;
    }

    public static HtmlValueLengthsList Create(HtmlValueLengthUnit unit, IEnumerable<double> lengths)
    {
        var lengthsList = new HtmlValueLengthsList { Unit = unit };

        lengthsList.AddLengths(lengths);

        return lengthsList;
    }

    public static HtmlValueLengthsList Create(params double[] lengths)
    {
        var lengthsList = new HtmlValueLengthsList();

        lengthsList.AddLengths(lengths);

        return lengthsList;
    }

    public static HtmlValueLengthsList Create(HtmlValueLengthUnit unit, params double[] lengths)
    {
        var lengthsList = new HtmlValueLengthsList { Unit = unit };

        lengthsList.AddLengths(lengths);

        return lengthsList;
    }


    private readonly List<double> _lengthsList
        = new List<double>();


    public IEnumerable<double> LengthValues
        => _lengthsList;

    private HtmlValueLengthUnit _unit = HtmlValueLengthUnit.None;
    public HtmlValueLengthUnit Unit
    {
        get { return _unit; }
        set { _unit = value ?? HtmlValueLengthUnit.None; }
    }

    public double this[int index]
        => _lengthsList[index];

    public override string ValueText
    {
        get
        {
            if (_lengthsList.Count == 0)
                return string.Empty;

            var composer = new StringBuilder();

            foreach (var length in _lengthsList)
                composer
                    .Append(length.ToHtmlLengthText(_unit))
                    .Append(",");

            composer.Length -= ",".Length;

            return composer.ToString();
        }
    }


    private HtmlValueLengthsList()
    {
    }


    public HtmlValueLengthsList ClearLengths()
    {
        _lengthsList.Clear();

        return this;
    }

    public HtmlValueLengthsList AddLength(double x)
    {
        _lengthsList.Add(x);

        return this;
    }

    public HtmlValueLengthsList AddLengths(IEnumerable<double> lengthsList)
    {
        _lengthsList.AddRange(lengthsList);

        return this;
    }

    public HtmlValueLengthsList AddLengths(params double[] lengthsList)
    {
        _lengthsList.AddRange(lengthsList);

        return this;
    }

    public HtmlValueLengthsList InsertLength(int index, double x)
    {
        _lengthsList.Insert(index, x);

        return this;
    }

    public HtmlValueLengthsList InsertLengths(int index, IEnumerable<double> lengthsList)
    {
        _lengthsList.InsertRange(index, lengthsList);

        return this;
    }

    public HtmlValueLengthsList RemoveLength(int index)
    {
        _lengthsList.RemoveAt(index);

        return this;
    }
}