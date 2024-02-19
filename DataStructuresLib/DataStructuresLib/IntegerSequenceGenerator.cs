namespace DataStructuresLib;

public sealed class IntegerSequenceGenerator
{
    private int _increment = 1;

    public string StringIdTemplate = "#";
    public string StringIdCountPlaceholder = "#";
    public string StringIdCountFomatting = "";


    public int NextCountValue { get; private set; }

    public int IncrementValue => _increment;


    public IntegerSequenceGenerator()
    {
    }

    public IntegerSequenceGenerator(int initialValue, int increment)
    {
        NextCountValue = initialValue;
        _increment = increment;
    }

    public IntegerSequenceGenerator(string template, string templatePlaceholder, string integerFormatting)
    {
        SetStringIdTemplate(template, templatePlaceholder, integerFormatting);
    }

    public int GetNewCountId()
    {
        var value = NextCountValue;
        NextCountValue += _increment;
        return value;
    }

    public string GetNewStringId(string prefix)
    {
        var newCount = GetNewCountId();

        var newCountStr = (StringIdCountFomatting == "") ? newCount.ToString() : newCount.ToString(StringIdCountFomatting);

        return prefix + newCountStr;
    }

    public string GetNewStringId()
    {
        var newCount = GetNewCountId();

        var newCountStr = (StringIdCountFomatting == "") ? newCount.ToString() : newCount.ToString(StringIdCountFomatting);

        return StringIdTemplate.Replace(StringIdCountPlaceholder, newCountStr);
    }

    public void ResetCounter()
    {
        NextCountValue = 0;
    }

    public void ResetCounter(int value)
    {
        NextCountValue = value;
    }

    public void SetIncrement(int value)
    {
        _increment = value;
    }

    public void SetStringIdTemplate(string template, string templatePlaceholder, string integerFormatting)
    {
        StringIdTemplate = template;
        StringIdCountPlaceholder = templatePlaceholder;
        StringIdCountFomatting = integerFormatting;
    }
}