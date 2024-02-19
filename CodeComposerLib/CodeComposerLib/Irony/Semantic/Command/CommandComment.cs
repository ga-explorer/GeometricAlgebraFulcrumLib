using System.Text;
using CodeComposerLib.Irony.Semantic.Scope;
using TextComposerLib;

namespace CodeComposerLib.Irony.Semantic.Command;

/// <summary>
/// This class represents a comment command
/// </summary>
public class CommandComment : LanguageCommand
{
    public string CommentPrefix { get; }

    //public string CommentPostfix { get; private set; }

    private readonly string[] _commentTextLines;

    /// <summary>
    /// The text of the comment
    /// </summary>
    public string CommentText 
    { 
        get 
        {
            var s = new StringBuilder();

            foreach (var line in _commentTextLines)
                s.Append(CommentPrefix).AppendLine(line);

            return s.ToString();
        } 
    }


    public bool IsSingleLineComment;


    public CommandComment(LanguageScope parentScope, string commentText)
        : base(parentScope)
    {
        CommentPrefix = @"//";

        _commentTextLines = commentText.SplitLines();
    }

    public CommandComment(LanguageScope parentScope, string commentPrefix, string commentText)
        : base(parentScope)
    {
        CommentPrefix = commentPrefix;

        _commentTextLines = commentText.SplitLines();
    }
}