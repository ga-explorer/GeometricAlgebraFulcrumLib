using System.Collections;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;

public sealed class GrPovRayColorMap :
    IGrPovRayCodeElement,
    IReadOnlyDictionary<double, Pair<GrPovRayColorValue>>
{
    private readonly SortedDictionary<double, Pair<GrPovRayColorValue>> _valueColorDictionary
        = new SortedDictionary<double, Pair<GrPovRayColorValue>>();


    public int Count
        => _valueColorDictionary.Count;

    public Pair<GrPovRayColorValue> this[double key]
    {
        get => _valueColorDictionary[key];
        set
        {
            if (double.IsNaN(key))
                throw new ArgumentException(nameof(key));

            if (key is < 0 or > 1)
                throw new ArgumentOutOfRangeException(nameof(key));

            if (_valueColorDictionary.ContainsKey(key))
                _valueColorDictionary[key] = value;
            else
                _valueColorDictionary.Add(key, value);
        }
    }

    public IEnumerable<double> Keys
        => _valueColorDictionary.Keys;

    public IEnumerable<Pair<GrPovRayColorValue>> Values
        => _valueColorDictionary.Values;


    public GrPovRayColorMap Clear()
    {
        _valueColorDictionary.Clear();

        return this;
    }

    public GrPovRayColorMap Remove(double key)
    {
        _valueColorDictionary.Remove(key);

        return this;
    }

    public bool ContainsKey(double key)
    {
        return _valueColorDictionary.ContainsKey(key);
    }

    public bool TryGetValue(double key, out Pair<GrPovRayColorValue> value)
    {
        return _valueColorDictionary.TryGetValue(key, out value);
    }

    public bool TryGetColors(double key, out GrPovRayColorValue? color1, out GrPovRayColorValue? color2)
    {
        var flag = _valueColorDictionary.TryGetValue(key, out var colorPair);

        if (flag && colorPair is not null)
        {
            (color1, color2) = colorPair;
            return true;
        }

        color1 = null;
        color2 = null;

        return false;
    }

    public GrPovRayColorMap AddOrSet(double key, GrPovRayColorValue value)
    {
        this[key] = new Pair<GrPovRayColorValue>(value, value);

        return this;
    }

    public GrPovRayColorMap AddOrSet(double key, GrPovRayColorValue color1, GrPovRayColorValue color2)
    {
        this[key] = new Pair<GrPovRayColorValue>(color1, color2);

        return this;
    }

    public GrPovRayColorMap AddOrSet(double key, Pair<GrPovRayColorValue> colorPair)
    {
        this[key] = colorPair;

        return this;
    }

    public GrPovRayColorMap AddOrSetRgbt(double key, GrPovRayFloat32Value rgb, GrPovRayFloat32Value transmit)
    {
        var color = GrPovRayColorValue.Rgbt(rgb, transmit);

        this[key] = new Pair<GrPovRayColorValue>(color, color);

        return this;
    }

    public GrPovRayColorMap AddOrSetRgbt(double key, GrPovRayFloat32Value rgb1, GrPovRayFloat32Value transmit1, GrPovRayFloat32Value rgb2, GrPovRayFloat32Value transmit2)
    {
        var color1 = GrPovRayColorValue.Rgbt(rgb1, transmit1);
        var color2 = GrPovRayColorValue.Rgbt(rgb2, transmit2);

        this[key] = new Pair<GrPovRayColorValue>(color1, color2);

        return this;
    }

    public bool IsEmptyCodeElement()
    {
        return _valueColorDictionary.Count == 0;
    }

    public string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("color_map {")
            .IncreaseIndentation();

        foreach (var (key, (color1, color2)) in _valueColorDictionary)
        {
            var c1 = color1.GetPovRayCode();
            var c2 = color2.GetPovRayCode();

            if (c1 == c2)
            {
                composer.AppendAtNewLine("[ " + key + " " + c1 + " ]");

                continue;
            }

            composer
                .AppendAtNewLine("[ " + key + " " + c1 + " ]")
                .AppendAtNewLine("[ " + key + " " + c2 + " ]");
        }

        composer
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }

    public override string ToString()
    {
        return GetPovRayCode();
    }

    public IEnumerator<KeyValuePair<double, Pair<GrPovRayColorValue>>> GetEnumerator()
    {
        return _valueColorDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}