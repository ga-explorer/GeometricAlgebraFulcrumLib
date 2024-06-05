using System.Text;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Text.Attributes;

internal sealed class TextAttribute
{
    public string Name { get; }

    private string _value = string.Empty;
    public string Value
    {
        get => _value;
        set => _value = value ?? string.Empty;
    }

    private string _valueDefault = string.Empty;
    public string ValueDefault
    {
        get => _valueDefault;
        set => _valueDefault = value ?? string.Empty;
    }

    public bool HideDefaultValue { get; set; }

    public bool HasDefaultValue
        => _value == _valueDefault;

    public bool IsHidden 
        => HideDefaultValue && HasDefaultValue;


    internal TextAttribute(string name)
    {
        Name = name ?? string.Empty;
    }

    internal TextAttribute(string name, string valueDefault)
    {
        Name = name ?? string.Empty;
        _valueDefault = valueDefault ?? string.Empty;
    }

    internal TextAttribute(string name, string valueDefault, string value)
    {
        Name = name ?? string.Empty;
        _valueDefault = valueDefault ?? string.Empty;
        _value = value ?? string.Empty;
    }

    public override string ToString()
    {
        return new StringBuilder()
            .Append(Name)
            .Append(": ")
            .Append(_value)
            .ToString();
    }
}