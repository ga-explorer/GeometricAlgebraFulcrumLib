using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Content;

public sealed class HtmlContentComment : IHtmlContent
{
    public static HtmlContentComment Create(string commentText)
    {
        return new HtmlContentComment(commentText);
    }


    public bool IsContentText => false;

    public bool IsContentComment => true;

    public bool IsContentElement => false;

    private string _commentText;
    public string CommentText
    {
        get { return _commentText; }
        set { _commentText = value ?? string.Empty; }
    }

    private HtmlContentComment(string commentText)
    {
        _commentText = commentText ?? string.Empty;
    }


    public override string ToString()
    {
        if (string.IsNullOrEmpty(CommentText))
            return "<!-- -->";

        var composer = new LinearTextComposer();

        return composer
            .AppendLine("<!-- ")
            .IncreaseIndentation()
            .AppendLine(CommentText)
            .DecreaseIndentation()
            .AppendAtNewLine(" -->")
            .ToString();
    }
}