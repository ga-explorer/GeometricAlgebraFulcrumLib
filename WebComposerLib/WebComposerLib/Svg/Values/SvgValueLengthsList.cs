using System.Text;
using WebComposerLib.Html.Media;

namespace WebComposerLib.Svg.Values;

public class SvgValueLengthsList : SvgComputedValue
{
    public static SvgValueLengthsList Create()
    {
        return new SvgValueLengthsList();
    }

    public static SvgValueLengthsList Create(SvgLengthUnit unit)
    {
        return new SvgValueLengthsList { Unit = unit };
    }

    public static SvgValueLengthsList Create(double length)
    {
        var lengthsList = new SvgValueLengthsList();

        lengthsList.AddLength(length);

        return lengthsList;
    }

    public static SvgValueLengthsList Create(SvgLengthUnit unit, double length)
    {
        var lengthsList = new SvgValueLengthsList { Unit = unit };

        lengthsList.AddLength(length);

        return lengthsList;
    }

    public static SvgValueLengthsList Create(IEnumerable<double> lengths)
    {
        var lengthsList = new SvgValueLengthsList();

        lengthsList.AddLengths(lengths);

        return lengthsList;
    }

    public static SvgValueLengthsList Create(SvgLengthUnit unit, IEnumerable<double> lengths)
    {
        var lengthsList = new SvgValueLengthsList { Unit = unit };

        lengthsList.AddLengths(lengths);

        return lengthsList;
    }

    public static SvgValueLengthsList Create(params double[] lengths)
    {
        var lengthsList = new SvgValueLengthsList();

        lengthsList.AddLengths(lengths);

        return lengthsList;
    }

    public static SvgValueLengthsList Create(SvgLengthUnit unit, params double[] lengths)
    {
        var lengthsList = new SvgValueLengthsList { Unit = unit };

        lengthsList.AddLengths(lengths);

        return lengthsList;
    }


    private readonly List<double> _lengthsList
        = new List<double>();


    public IEnumerable<double> LengthValues
        => _lengthsList;

    private SvgLengthUnit _unit = SvgLengthUnit.None;
    public SvgLengthUnit Unit
    {
        get => _unit;
        set => _unit = value ?? SvgLengthUnit.None;
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
                    .Append(length.ToSvgLengthText(_unit))
                    .Append(",");

            composer.Length -= ",".Length;

            return composer.ToString();
        }
    }


    private SvgValueLengthsList()
    {
    }


    public SvgValueLengthsList ClearLengths()
    {
        _lengthsList.Clear();

        return this;
    }

    public SvgValueLengthsList AddLength(double x)
    {
        _lengthsList.Add(x);

        return this;
    }

    public SvgValueLengthsList AddLengths(IEnumerable<double> lengthsList)
    {
        _lengthsList.AddRange(lengthsList);

        return this;
    }

    public SvgValueLengthsList AddLengths(params double[] lengthsList)
    {
        _lengthsList.AddRange(lengthsList);

        return this;
    }

    public SvgValueLengthsList InsertLength(int index, double x)
    {
        _lengthsList.Insert(index, x);

        return this;
    }

    public SvgValueLengthsList InsertLengths(int index, IEnumerable<double> lengthsList)
    {
        _lengthsList.InsertRange(index, lengthsList);

        return this;
    }

    public SvgValueLengthsList RemoveLength(int index)
    {
        _lengthsList.RemoveAt(index);

        return this;
    }
}