namespace CodeComposerLib.HTMLold.Values;

public abstract class HtmlStoredValue : IHtmlValue
{
    public string ValueText { get; }

    protected HtmlStoredValue(string value)
    {
        ValueText = value;
    }

    public override string ToString()
    {
        return ValueText;
    }
}