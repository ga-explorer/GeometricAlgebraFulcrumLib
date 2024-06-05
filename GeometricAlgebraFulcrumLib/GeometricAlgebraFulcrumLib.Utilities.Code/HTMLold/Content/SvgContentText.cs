using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Content;

public class HtmlContentText : IHtmlContent
{
    public static HtmlContentText Create(string text)
    {
        return new HtmlContentText(text);
    }


    public bool IsContentText => true;

    public bool IsContentComment => false;

    public bool IsContentElement => false;

    private string _value;
    public string Value
    {
        get { return _value; }
        set { _value = value?.ToHtmlSafeString() ?? string.Empty; }
    }


    private HtmlContentText(string value)
    {
        _value = value?.ToHtmlSafeString() ?? string.Empty;
    }


    public override string ToString()
    {
        return _value;
    }
}