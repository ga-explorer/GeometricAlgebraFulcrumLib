using System.Text;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Text.Structured;

public sealed class StructuredTextItem
{
    public static StructuredTextItem Empty { get; private set; }

    static StructuredTextItem()
    {
        Empty = new StructuredTextItem(string.Empty);
    }


    public string Prefix { get; }

    public string Suffix { get; }

    public string Text { get; }


    public StructuredTextItem(string text)
    {
        Prefix = string.Empty;
        Suffix = string.Empty;
        Text = text ?? string.Empty;
    }

    public StructuredTextItem(string prefix, string text, string suffix)
    {
        Prefix = prefix ?? string.Empty;
        Suffix = suffix ?? string.Empty;
        Text = text ?? string.Empty;
    }


    public override string ToString()
    {
        return 
            new StringBuilder(Prefix.Length + Text.Length + Suffix.Length)
                .Append(Prefix)
                .Append(Text)
                .Append(Suffix)
                .ToString();
    }
}