using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;

public class SteComment : SteSyntaxElement
{
    private bool _singleLineComment = true;

    public bool SingleLineComment
    {
        get { return _singleLineComment; }
        set { _singleLineComment = value; }
    }

    public bool MultiLineComment 
    {
        get { return !_singleLineComment; }
        set { _singleLineComment = !value; }
    }

    public string[] CommentedTextLines { get; }

    public int CommentedTextLinesCount => CommentedTextLines.Length;


    public SteComment()
    {
        CommentedTextLines = new[] {string.Empty};
    }

    public SteComment(int emptyLinesCount)
    {
        CommentedTextLines = new string[emptyLinesCount];

        for (var i = 0; i < emptyLinesCount; i++)
            CommentedTextLines[i] = string.Empty;
    }

    public SteComment(string commentedText)
    {
        CommentedTextLines = 
            string.IsNullOrEmpty(commentedText)
                ? new[] { string.Empty }
                : commentedText.SplitLines();
    }

    public SteComment(IEnumerable<string> commentedTextStrings)
    {
        var lines = new List<string>();

        foreach (var textString in commentedTextStrings)
            if (string.IsNullOrEmpty(textString))
                lines.Add(string.Empty);
            else
                lines.AddRange(textString.SplitLines());

        CommentedTextLines = 
            lines.Count == 0
                ? new[] { string.Empty }
                : lines.ToArray();
    }

    public SteComment(params string[] commentedTextStrings)
    {
        var lines = new List<string>();

        foreach (var textString in commentedTextStrings)
            if (string.IsNullOrEmpty(textString))
                lines.Add(string.Empty);
            else
                lines.AddRange(textString.SplitLines());

        CommentedTextLines =
            lines.Count == 0
                ? new[] { string.Empty }
                : lines.ToArray();
    }
}